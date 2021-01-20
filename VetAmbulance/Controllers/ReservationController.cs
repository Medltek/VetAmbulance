using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VetAmbulance.BL;
using VetAmbulance.BL.Models;
using VetAmbulance.Helpers;

namespace VetAmbulance.Controllers
{
    public class ReservationController : Controller
    {
        private IHttpContextAccessor httpContextAccessor;
        private Patient patien;
        private VetAmbulance.BL.Models.Vet vet;
        private Ambulance ambulance;
        private Reservation reservation;

        public ReservationController(IHttpContextAccessor httpContextAccessor, Patient patient, VetAmbulance.BL.Models.Vet vet, Ambulance ambulance, Reservation reservation)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.patien = patient;
            this.vet = vet;
            this.ambulance = ambulance;
            this.reservation = reservation;
        }

        public IActionResult Index()
        {
            if (!httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToRoute(new
                {
                    controller = "Auth",
                    action = "Login"
                });
            }

            var reservations = patien.GetReservations(UserInfoHelper.GetId(httpContextAccessor));

            foreach (var reservation in reservations)
            {
                vet.GetById(reservation.VetId);
                ambulance.GetById(vet.AmbulanceId);

                reservation.Vet = new VetAmbulance.BL.Models.Vet()
                {
                    Id = vet.Id,
                    Name = vet.Name,
                    
                    AmbulanceId = vet.AmbulanceId,
                    PasswordHash = vet.PasswordHash,
                    PasswordSalt = vet.PasswordSalt
                };

                reservation.Vet.Ambulance = new Ambulance()
                {
                    Id = ambulance.Id,
                    Address = ambulance.Address,
                    ClosingHour = ambulance.ClosingHour,
                    OpeningHour = ambulance.OpeningHour
                  
                };
            }

            return View(reservations);
        }

        public IActionResult Create()
        {
            if (!httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToRoute(new
                {
                    controller = "Auth",
                    action = "Login"
                });
            }

            ViewBag.Doctors = vet.GetAll();

            return View();
        }

        [HttpPost]
        public IActionResult Create(ReservationDTO reservationDto)
        {
            if (!httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToRoute(new
                {
                    controller = "Auth",
                    action = "Login"
                });
            }

            reservationDto.PatientId = UserInfoHelper.GetId(httpContextAccessor);

            ViewBag.Doctors = vet.GetAll();

            if (reservationDto.Date == new DateTime())
            {
                ViewBag.Error = "Vyberte den";
                return View(reservationDto);
            }

            // Check if not in past
            reservationDto.Date = reservationDto.Date;
            if (reservationDto.Date < DateTime.Now)
            {
                ViewBag.Error = "Date is wrong";
                return View(reservationDto);
            }

            // Check for opening time
            vet.GetById(reservationDto.VetId);
            if (vet.Name == null)
            {
                ViewBag.Error = "Vyberte doktora";
                return View(reservationDto);
            }
            ambulance.GetById(vet.AmbulanceId);
            vet.Ambulance = new Ambulance()
            {
                Id = ambulance.Id,
                Address = ambulance.Address,
                ClosingHour = ambulance.ClosingHour,
                OpeningHour = ambulance.OpeningHour
            };

            

            // Check if not busy
            var reservations = reservation.GetByDate(reservationDto.Date);

            if (reservations.Count(r => r.VetId == reservationDto.VetId && r.Date == reservationDto.Date) > 0)
            {
                ViewBag.Error = "Ambulance already reserved";
                return View(reservationDto);
            }

            // Everything OK, adding
            if (!reservation.Insert(reservationDto))
            {
                ViewBag.Error = "error on insert";
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            try
            {
                var reservationToDelete = reservation.GetById(id);

                if (reservationToDelete.PatientId == UserInfoHelper.GetId(httpContextAccessor))
                {
                    reservation.Delete(id);
                }
            }
            catch
            {
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
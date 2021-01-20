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
    public class MedicinesController : Controller
    {
        private IHttpContextAccessor httpContextAccessor;
        private Patient patient;
        private Drug drug;

        public MedicinesController(IHttpContextAccessor httpContextAccessor, Patient patient, Drug drug)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.patient = patient;
            this.drug = drug;
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

            var medicines = patient.GetDrugs(UserInfoHelper.GetId(httpContextAccessor));

            return View(medicines);
        }

        public IActionResult Detail(int id)
        {
            if (!httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToRoute(new
                {
                    controller = "Auth",
                    action = "Login"
                });
            }

            var drugModel = drug.GetById(id);

            if (drugModel == null || drugModel.PatientId != UserInfoHelper.GetId(httpContextAccessor))
            {
                return NotFound();
            }

            return View(drugModel);
        }
    }
}
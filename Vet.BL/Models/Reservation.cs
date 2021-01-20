using System;
using System.Collections.Generic;
using System.Text;
using VetAmbulance.DAL.Mappers;
namespace VetAmbulance.BL.Models
{
    public class Reservation
    {
        private readonly ReservationMapper reservationMapper;

        public Reservation()
        {
        }

        public Reservation(ReservationMapper reservationMapper)
        {
            this.reservationMapper = reservationMapper;
        }

        public int Id { get; set; }

        public int PatientId { get; set; }
        public Patient Patient { get; set; }

        public int VetId { get; set; }
        public Vet Vet { get; set; }

        public DateTime Date { get; set; }

        public Patient Patient1
        {
            get => default;
            set
            {
            }
        }

        public Vet Vet1
        {
            get => default;
            set
            {
            }
        }

        public Reservation GetById(int id)
        {
            var reservation = reservationMapper.GetById(id);

            if (reservation == null)
            {
                return null;
            }

            Id = reservation.Id;
            PatientId = reservation.PatientId;
            VetId = reservation.VetId;
            Date = reservation.Date;

            return this;
        }

        public List<Reservation> GetByDate(DateTime date)
        {
            var reservations = reservationMapper.GetByDate(date);

            var reservationsList = new List<Reservation>();

            if (reservations == null)
            {
                return null;
            }

            foreach (var reservation in reservations)
            {
                reservationsList.Add(new Reservation()
                {
                    Id = reservation.Id,
                    PatientId = reservation.PatientId,
                    VetId = reservation.VetId,
                    Date = reservation.Date
                });
            }

            return reservationsList;
        }

        public bool Insert(ReservationDTO reservationDto)
        {
            try
            {
                var reservation = new DAL.Reservation()
                {
                    Date = reservationDto.Date,
                    VetId = reservationDto.VetId,
                    PatientId = reservationDto.PatientId
                };

                reservationMapper.Insert(reservation);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Delete(int id)
        {
            reservationMapper.Delete(id);
        }
    }
}

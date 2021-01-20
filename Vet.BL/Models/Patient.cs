using System;
using System.Collections.Generic;
using System.Text;
using VetAmbulance.DAL.Mappers;
using VetAmbulance.BL.Security;
using VetAmbulance.BL;

namespace VetAmbulance.BL.Models
{
    public class Patient
    {
        private readonly PatientMapper patientMapper;

        public Patient(PatientMapper patientMapper)
        {
            this.patientMapper = patientMapper;
        }

        public int Id { get; set; }

        public int ChipId { get; set; }

        public string PasswordSalt { get; set; }

        public string PasswordHash { get; set; }

        public Patient GetById(int id)
        {
            var patient = patientMapper.GetById(id);

            if (patient == null)
            {
                return null;
            }

            Id = patient.Id;

            

            PasswordSalt = patient.PasswordSalt;
            PasswordHash = patient.PasswordHash;
            ChipId = patient.ChipId;
            return this;
        }

        public Patient GetByChipId(int chipId)
        {
            var patient = patientMapper.GetByChipId(chipId);

            if (patient == null)
            {
                return null;
            }

            Id = patient.Id;

           

            PasswordSalt = patient.PasswordSalt;
            PasswordHash = patient.PasswordHash;
            ChipId = patient.ChipId;
            return this;
        }

        public List<Drug> GetDrugs(int patientId)
        {
            var drugs = patientMapper.GetDrugs(patientId);

            if (drugs == null)
            {
                return null;
            }

            var drugsList = new List<Drug>();

            foreach (var drug in drugs)
            {
                drugsList.Add(new Drug()
                {
                    Id = drug.Id,
                    PatientId = drug.PatientId,

                    Name = drug.Name
                });
            }

            return drugsList;
        }

        public List<Diagnosis> GetDiagnoses(int patientId)
        {
            var diagnoses = patientMapper.GetDiagnoses(patientId);

            if (diagnoses == null)
            {
                return null;
            }

            var diagnosesList = new List<Diagnosis>();

            foreach (var diagnosis in diagnoses)
            {
                diagnosesList.Add(new Diagnosis()
                {
                    Id = diagnosis.Id,
                    PatientId = diagnosis.PatientId,
                    Date = diagnosis.Date,
                    DisName = diagnosis.DisName,
                    Symptoms = diagnosis.Symptoms,
                    Therapy = diagnosis.Therapy
                });
            }

            return diagnosesList;
        }

        public List<Reservation> GetReservations(int patientId)
        {
            var reservations = patientMapper.GetReservations(patientId);

            if (reservations == null)
            {
                return null;
            }

            var reservationsList = new List<Reservation>();

            foreach (var reservation in reservations)
            {
                reservationsList.Add(new Reservation()
                {
                    Date = reservation.Date,
                    VetId = reservation.VetId,
                    PatientId = reservation.PatientId,
                    Id = reservation.Id
                });
            }

            return reservationsList;
        }

        public bool Insert(RegisterDTO register)
        {
            try
            {
                var password = PasswordHelper.CreateHash(register.Password);

                var patient = new DAL.Patient()
                {
                    ChipId = register.ChipId,


                    PasswordHash = password.PasswordHash,
                    PasswordSalt = password.PasswordSalt
                };

                patientMapper.Insert(patient);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Delete(int id)
        {
            patientMapper.Delete(id);
        }
    }
}

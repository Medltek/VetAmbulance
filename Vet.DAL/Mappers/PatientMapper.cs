using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace VetAmbulance.DAL.Mappers
{
    public class PatientMapper
    {
        private readonly DatabaseContext dbContext;

        public PatientMapper(DatabaseContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Patient GetById(int id)
        {
            return dbContext.Patients.FirstOrDefault(p => p.Id == id);
        }

        public Patient GetByChipId(int chipId)
        {
            return dbContext.Patients.FirstOrDefault(p => p.ChipId == chipId);
        }

        public List<Drug> GetDrugs(int patientId)
        {
            return dbContext.Drugs.Where(m => m.PatientId == patientId).AsNoTracking().ToList();
        }

        public List<Diagnosis> GetDiagnoses(int patientId)
        {
            return dbContext.Diagnoses.Where(d => d.PatientId == patientId).AsNoTracking().ToList();
        }

        public List<Reservation> GetReservations(int patientId)
        {
            return dbContext.Reservations.Where(r => r.PatientId == patientId).ToList();
        }

        public void Insert(Patient patient)
        {
            dbContext.Add(patient);
            dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var item = dbContext.Patients.Find(id);
            dbContext.Patients.Remove(item);
            dbContext.SaveChanges();
            
        }
    }
}

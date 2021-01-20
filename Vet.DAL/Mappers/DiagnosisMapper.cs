using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace VetAmbulance.DAL.Mappers
{
    public class DiagnosisMapper
    {
        private readonly DatabaseContext dbContext;

        public DiagnosisMapper(DatabaseContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Diagnosis GetById(int id)
        {
            return dbContext.Diagnoses.FirstOrDefault(d => d.Id == id);
        }

        public List<Diagnosis> GetAll()
        {
            return dbContext.Diagnoses.AsNoTracking().ToList();
        }

        public void Insert(Diagnosis diagnosis)
        {
            dbContext.Add(diagnosis);
            dbContext.SaveChanges();
        }

        public void Edit(Diagnosis diagnosis)
        {
            var edit = dbContext.Diagnoses.Find(diagnosis.Id);

            edit.Date = diagnosis.Date;
            edit.DisName = diagnosis.DisName;
            edit.Symptoms = diagnosis.Symptoms;
            edit.Therapy = diagnosis.Therapy;

            dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var item = dbContext.Diagnoses.Find(id);
            dbContext.Diagnoses.Remove(item);
            dbContext.SaveChanges();
        }
    }
}

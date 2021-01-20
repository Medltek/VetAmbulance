using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace VetAmbulance.DAL.Mappers
{
    public class AmbulanceMapper
    {
        private readonly DatabaseContext dbContext;

        public AmbulanceMapper(DatabaseContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Ambulance GetById(int id)
        {
            return dbContext.Ambulances.FirstOrDefault(h => h.Id == id);
        }

        public List<Ambulance> GetAll()
        {
            return dbContext.Ambulances.ToList();
        }

        public List<Vet> GetVets(int ambulanceId)
        {
            return dbContext.Vets.Where(h => h.AmbulanceId == ambulanceId).ToList();
        }

        public void Insert(Ambulance ambulace)
        {
            dbContext.Add(ambulace);
            dbContext.SaveChanges();
        }

        public void Edit(Ambulance ambulance)
        {
            var edit = dbContext.Ambulances.Find(ambulance.Id);

            edit.Address = ambulance.Address;
            edit.OpeningHour = ambulance.OpeningHour;
            edit.ClosingHour = ambulance.ClosingHour;

            dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var item = dbContext.Ambulances.Find(id);
            dbContext.Ambulances.Remove(item);
            dbContext.SaveChanges();
        }
    }   
}

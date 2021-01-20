using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace VetAmbulance.DAL.Mappers
{
    public class VetMapper
    {
        private readonly DatabaseContext dbContext;

        public VetMapper(DatabaseContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Vet GetById(int id)
        {
            return dbContext.Vets.FirstOrDefault(d => d.Id == id);
        }

        public Vet GetByName(string name)
        {
            return dbContext.Vets.FirstOrDefault(d => d.Name == name);
        }

        public List<Vet> GetAll()
        {
            return dbContext.Vets.ToList();
        }

        public void Insert(Vet vet)
        {
            dbContext.Add(vet);
            dbContext.SaveChanges();
        }

        public void Edit(Vet vet)
        {
            var edit = dbContext.Vets.Find(vet.Id);

            edit.Name = vet.Name;

            edit.AmbulanceId = vet.AmbulanceId;

            if (!string.IsNullOrEmpty(vet.PasswordSalt) && !string.IsNullOrEmpty(vet.PasswordHash))
            {
                edit.PasswordSalt = vet.PasswordSalt;
                edit.PasswordHash = vet.PasswordHash;
            }

            dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var item = dbContext.Vets.Find(id);
            dbContext.Vets.Remove(item);
            dbContext.SaveChanges();
        }
    }
}

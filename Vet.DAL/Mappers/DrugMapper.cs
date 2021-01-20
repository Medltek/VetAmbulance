using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;
namespace VetAmbulance.DAL.Mappers
{
    public class DrugMapper
    {
        private readonly DatabaseContext dbContext;

        public DrugMapper(DatabaseContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Drug GetById(int id)
        {
            return dbContext.Drugs.FirstOrDefault(m => m.Id == id);
        }

        public List<Drug> GetAll()
        {
            return dbContext.Drugs.AsNoTracking().ToList();
        }

        public void Insert(Drug drug)
        {
            dbContext.Add(drug);
            dbContext.SaveChanges();
        }

        public void Edit(Drug drug)
        {
            var edit = dbContext.Drugs.Find(drug.Id);

            edit.Name = drug.Name;

            dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var item = dbContext.Drugs.Find(id);
            dbContext.Drugs.Remove(item);
            dbContext.SaveChanges();
        }
    }
}

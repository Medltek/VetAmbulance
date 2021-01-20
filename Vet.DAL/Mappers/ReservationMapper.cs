using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace VetAmbulance.DAL.Mappers
{
    public class ReservationMapper
    {
        private readonly DatabaseContext dbContext;

        public ReservationMapper(DatabaseContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Reservation GetById(int id)
        {
            return dbContext.Reservations.FirstOrDefault(r => r.Id == id);
        }

        public List<Reservation> GetByDate(DateTime date)
        {
            var dateFrom = date.Date + new TimeSpan(0, 0, 0);
            var dateTo = date.Date + new TimeSpan(23, 59, 59);
            return dbContext.Reservations.Where(r => r.Date >= dateFrom && r.Date <= dateTo).ToList();
        }

        public void Insert(Reservation reservation)
        {
            dbContext.Add(reservation);
            dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var item = dbContext.Reservations.Find(id);
            dbContext.Reservations.Remove(item);
            dbContext.SaveChanges();
        }
    }
}

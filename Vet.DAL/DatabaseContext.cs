using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace VetAmbulance.DAL
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
        }

        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<Vet> Vets { get; set; }

        public DbSet<Ambulance> Ambulances { get; set; }

        public DbSet<Diagnosis> Diagnoses { get; set; }

        public DbSet<Drug> Drugs { get; set; }

        public DbSet<Reservation> Reservations { get; set; }
    }
}

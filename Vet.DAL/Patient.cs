using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetAmbulance.DAL
{
    public class Patient
    {
        public int Id { get; set; }

        public string PasswordSalt { get; set; }

        public string PasswordHash { get; set; }

        public int ChipId { get; set; }

        public List<Drug> Drugs { get; set; }

        public List<Diagnosis> Diagnoses { get; set; }

        public List<Reservation> Reservations { get; set; }
    }
}

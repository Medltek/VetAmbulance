using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetAmbulance.DAL
{
    public class Vet
    {
        public int Id { get; set; }

        public string PasswordSalt { get; set; }

        public string PasswordHash { get; set; }

        public string Name { get; set; }

        public int AmbulanceId { get; set; }
        public Ambulance Ambulance { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetAmbulance.DAL
{
    public class Ambulance
    {
        public int Id { get; set; }

        public string Address { get; set; }

        public List<Vet> Vets { get; set; }

        public int OpeningHour { get; set; }

        public int ClosingHour { get; set; }
    }
}

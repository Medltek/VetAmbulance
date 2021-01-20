using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetAmbulance.DAL
{
    public class Reservation
    {
        public int Id { get; set; }

        public int PatientId { get; set; }
        public Patient Patient { get; set; }

        public int VetId { get; set; }
        public Vet Vet { get; set; }

        public DateTime Date { get; set; }
    }
}

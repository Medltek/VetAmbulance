using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetAmbulance.DAL
{
    public class Diagnosis
    {
        public int Id { get; set; }

        public int PatientId { get; set; }
        public Patient Patient { get; set; }

        public DateTime Date { get; set; }

        public string DisName { get; set; }

        public string Symptoms { get; set; }

        public string Therapy { get; set; }
    }
}

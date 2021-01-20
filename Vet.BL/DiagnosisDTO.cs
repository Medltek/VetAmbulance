using System;
using System.Collections.Generic;
using System.Text;

namespace VetAmbulance.BL
{
    public class DiagnosisDTO
    {
        public int Id { get; set; }

        public int PatientId { get; set; }


        public DateTime Date { get; set; }

        public string DisName { get; set; }

        public string Symptoms { get; set; }

        public string Therapy { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace VetAmbulance.BL
{
    public class ReservationDTO
    {
        public int Id { get; set; }

        public int PatientId { get; set; }


        public int VetId { get; set; }


        public DateTime Date { get; set; }
    }
}

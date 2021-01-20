using System;
using System.Collections.Generic;
using System.Text;

namespace VetAmbulance.BL
{
    public class AmbulanceDTO
    {
        public int Id { get; set; }

        public string Address { get; set; }

        public int OpeningHour { get; set; }

        public int ClosingHour { get; set; }
    }
}

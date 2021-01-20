using System;
using System.Collections.Generic;
using System.Text;

namespace VetAmbulance.BL
{
    public class PatientRegistrationDTO
    {
        public int Id { get; set; }

        public string PasswordSalt { get; set; }

        public string PasswordHash { get; set; }

        public int ChipId { get; set; }


    }
}

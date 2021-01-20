using System;
using System.Collections.Generic;
using System.Text;

namespace VetAmbulance.BL.Security
{
    public class PasswordData
    {
        public string PasswordSalt { get; set; }

        public string PasswordHash { get; set; }
    }
}

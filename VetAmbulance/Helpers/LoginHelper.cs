using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using VetAmbulance.BL.Models;
using VetAmbulance.BL.Security;

namespace VetAmbulance.Helpers
{
    public class LoginHelper
    {
        private readonly Patient patient;

        public LoginHelper(Patient patient)
        {
            this.patient = patient;
        }

        public ClaimsIdentity GetClaimsIdentity(int chipId, string password)
        {
            // try to find the user
            var user = patient.GetByChipId((chipId));
            if (user == null)
            {
                return null;
            }

            // verify the password
            if (!PasswordHelper.VerifyHashedPassword(user.PasswordHash, user.PasswordSalt, password))
            {
                return null;
            }

            // build the user identity
            return new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.ChipId.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.AuthenticationMethod, CookieAuthenticationDefaults.AuthenticationScheme)
            }, CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}

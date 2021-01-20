using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace VetAmbulance.Helpers
{
    public static class UserInfoHelper
    {
        public static string GetName(IHttpContextAccessor httpContextAccessor)
        {
            return httpContextAccessor.HttpContext.User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.Name)
                ?.Value;
        }

        public static int GetId(IHttpContextAccessor httpContextAccessor)
        {
            return Convert.ToInt32(httpContextAccessor.HttpContext.User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)
                ?.Value);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VetAmbulance.BL;
using VetAmbulance.BL.Models;
using VetAmbulance.Helpers;

namespace VetAmbulance.Controllers
{
    public class AuthController : Controller
    {
        private readonly LoginHelper loginHelper;

        private Patient patient;

        private IHttpContextAccessor httpContextAccessor;

        public AuthController(LoginHelper loginHelper, Patient patient, IHttpContextAccessor httpContextAccessor)
        {
            this.loginHelper = loginHelper;
            this.patient = patient;
            this.httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginDto)
        {
            var identity = loginHelper.GetClaimsIdentity(loginDto.ChipId, loginDto.Password);
            if (identity == null)
            {
                ViewBag.Error = "Wrooong!";
            }
            else
            {
                // issue an authentication cookie
                var properties = new AuthenticationProperties()
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddMonths(1)
                };
                await AuthenticationHttpContextExtensions.SignInAsync(HttpContext, new ClaimsPrincipal(identity), properties);

                // redirect to the home page
                return RedirectToRoute(new
                {
                    controller = "Home",
                    action = "Index"
                });
            }
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO registerDto)
        {
            var result = patient.Insert(registerDto);

            if (!result)
            {
                ViewBag.Error = "Registation failed!";
                return View(registerDto);
            }

            // redirect to the home page
            return RedirectToRoute(new
            {
                controller = "Home",
                action = "Index"
            });
        }

        public async Task<IActionResult> Logout()
        {
            var authenticated = httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;

            if (authenticated)
            {
                await AuthenticationHttpContextExtensions.SignOutAsync(HttpContext);
            }

            // redirect to the home page
            return RedirectToRoute(new
            {
                controller = "Home",
                action = "Index"
            });
        }
    }
}
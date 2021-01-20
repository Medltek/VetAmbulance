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
    public class DiagnosesController : Controller
    {
        private IHttpContextAccessor httpContextAccessor;
        private Patient patient;
        private Diagnosis diagnose;

        public DiagnosesController(IHttpContextAccessor httpContextAccessor, Patient patient, Diagnosis diagnose)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.patient = patient;
            this.diagnose = diagnose;
        }

        public IActionResult Index()
        {
            if (!httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToRoute(new
                {
                    controller = "Auth",
                    action = "Login"
                });
            }

            var diagnoses = patient.GetDiagnoses(UserInfoHelper.GetId(httpContextAccessor));

            return View(diagnoses);
        }

        public IActionResult Detail(int id)
        {
            if (!httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToRoute(new
                {
                    controller = "Auth",
                    action = "Login"
                });
            }

            var diagnoseModel = diagnose.GetById(id);

            if (diagnoseModel == null || diagnoseModel.PatientId != UserInfoHelper.GetId(httpContextAccessor))
            {
                return NotFound();
            }

            return View(diagnoseModel);
        }
    }
}
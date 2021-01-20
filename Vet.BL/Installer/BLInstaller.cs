
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using VetAmbulance.BL.Models;

namespace VetAmbulance.BL.Installer
{

    public class BLInstaler : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<VetAmbulance.BL.Models.Vet>()
                   .LifestyleTransient(),
               Component.For<Ambulance>()
                   .LifestyleTransient(),
               Component.For<Patient>()
                   .LifestyleTransient(),
               Component.For<Diagnosis>()
                   .LifestyleTransient(),
               Component.For<Reservation>()
                   .LifestyleTransient(),
               Component.For<Drug>()
                   .LifestyleTransient());
        }
    }
    
}

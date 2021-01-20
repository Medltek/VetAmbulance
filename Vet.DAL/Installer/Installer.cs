using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using VetAmbulance.DAL.Mappers;
using Microsoft.EntityFrameworkCore;
namespace VetAmbulance.DAL.Installer
{
    public class Installer : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            var builder = new DbContextOptionsBuilder();
            builder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=VetAmbulance;Trusted_Connection=True;MultipleActiveResultSets=true");

            container.Register(Component.For<DatabaseContext>()
                    .UsingFactoryMethod(() => new DatabaseContext(builder.Options))
                    .LifestyleTransient(),

                Component.For<PatientMapper>()
                    .LifestyleTransient(),
                Component.For<VetMapper>()
                    .LifestyleTransient(),
                Component.For<AmbulanceMapper>()
                    .LifestyleTransient(),
                Component.For<DiagnosisMapper>()
                    .LifestyleTransient(),
                Component.For<DrugMapper>()
                    .LifestyleTransient(),
                Component.For<ReservationMapper>()
                    .LifestyleTransient());
        }
    }
}

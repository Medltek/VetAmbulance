using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Castle.Windsor;
using VetAmbulance.BL.Installer;
using VetAmbulance.DAL;
using VetAmbulance.DAL.Installer;

namespace VetAmbulance.DesktopApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Configuration.File = "configuration.xml";

            //var loginWindow = new MainWindow();
            //loginWindow.Show();
        }
    }

    public static class Container
    {
        private static WindsorContainer container;

        static Container()
        {
            container = new WindsorContainer();

            container.Install(new Installer());
            container.Install(new BLInstaler());
        }

        public static WindsorContainer Get()
        {
            return container;
        }
    }
}

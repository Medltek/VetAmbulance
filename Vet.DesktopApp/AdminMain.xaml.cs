using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace VetAmbulance.DesktopApp
{
    /// <summary>
    /// Interaction logic for AdminMain.xaml
    /// </summary>
    public partial class AdminMain : Window
    {
        public AdminMain()
        {
            InitializeComponent();
        }
        private void Vets_Click(object sender, RoutedEventArgs e)
        {
            var w = new Vets();
            w.Show();
            this.Close();
        }

        private void Ambulances_Click(object sender, RoutedEventArgs e)
        {
            var w = new Ambulances();
            w.Show();
            this.Close();
        }

        private void ButtonLogout_OnClick(object sender, RoutedEventArgs e)
        {
            Configuration.Save(new ConfigurationModel()
            {
                IsAdmin = false,
                Name = "",
                Password = ""
            });

            var loginWindow = new MainWindow();
            loginWindow.Show();
            this.Close();
        }

    }
}

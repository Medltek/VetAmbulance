using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using VetAmbulance.BL;
using VetAmbulance.BL.Models;

namespace VetAmbulance.DesktopApp
{
    /// <summary>
    /// Interaction logic for Ambulances.xaml
    /// </summary>
    public partial class Ambulances : Window
    {
        private Ambulance ambulance;
        public Ambulances()
        {
            InitializeComponent();

            ambulance = Container.Get().Resolve<Ambulance>();


            GetAmbulances();

        }

        public Ambulances(AmbulanceDTO amb)
        {
            InitializeComponent();

            ambulance = Container.Get().Resolve<Ambulance>();
            ambulance.Insert(amb);

            GetAmbulances();

        }

        public void GetAmbulances()
        {
            var ambulances = ambulance.GetAll();
            dataGridAmbulances.ItemsSource = ambulances;
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            var w = new AdminMain();
            w.Show();
            this.Close();
        }

        private void ButtonCreateAmbulance_Click(object sender, RoutedEventArgs e)
        {
            var ambulanceWindow = new AmbulanceForm(new AmbulanceDTO());
            ambulanceWindow.Owner = this;
            ambulanceWindow.ShowDialog();

            if (ambulanceWindow.DialogResult == true)
            {
                ambulance.Insert(ambulanceWindow.Ambulance);
                GetAmbulances();
            }
        }

        private void ButtonEditAmbulance_Click(object sender, RoutedEventArgs e)
        {
            var item = (Ambulance)dataGridAmbulances.SelectedItem;

            if (item == null)
            {
                return;
            }

            var ambulanceWindow = new AmbulanceForm(new AmbulanceDTO()
            {
                Address = item.Address,
                OpeningHour = item.OpeningHour,
                ClosingHour = item.ClosingHour,

            });

            ambulanceWindow.Owner = this;
            ambulanceWindow.ShowDialog();

            if (ambulanceWindow.DialogResult == true)
            {
                ambulance.Edit(ambulanceWindow.Ambulance, item.Id);
                GetAmbulances();
            }
        }

        private void ButtonDeleteAmbulance_Click(object sender, RoutedEventArgs e)
        {
            var item = (Ambulance)dataGridAmbulances.SelectedItem;

            if (item == null)
            {
                return;
            }

            if (MessageBox.Show("Do you want to delete this Amb?", "Delete", MessageBoxButton.YesNo,
                    MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                ambulance.Delete(item.Id);
                GetAmbulances();
            }
        }
    }
}

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
using VetAmbulance.DAL;
using VetAmbulance.BL.Models;


namespace VetAmbulance.DesktopApp
{
    /// <summary>
    /// Interaction logic for Vets.xaml
    /// </summary>
    public partial class Vets : Window
    {
        private VetAmbulance.BL.Models.Vet vet;
        private VetAmbulance.BL.Models.Ambulance ambulance;
        public Vets()
        {
            InitializeComponent();

            ambulance = Container.Get().Resolve<VetAmbulance.BL.Models.Ambulance>();
            vet = Container.Get().Resolve<VetAmbulance.BL.Models.Vet>();

            GetAmbulances();
            GetVets();
        }
        public void GetAmbulances()
        {
            var ambulances = ambulance.GetAll();
            dataGridVets.ItemsSource = ambulances;
        }
        public void GetVets()
        {
            var vets = vet.GetAll();

            foreach (var veti in vets)
            {
                ambulance.GetById(veti.AmbulanceId);
                veti.Ambulance = new VetAmbulance.BL.Models.Ambulance()
                {
                    Id = ambulance.Id,
                    Address = ambulance.Address
                };
            }

            dataGridVets.ItemsSource = vets;
        }

        private void ButtonCreateVet_OnClick(object sender, RoutedEventArgs e)
        {
            var vetWindow = new VetAmbulance.DesktopApp.VetWindow(new VetDTO());
            vetWindow.Owner = this;
            vetWindow.ShowDialog();

            if (vetWindow.DialogResult == true)
            {
                vet.Insert(vetWindow.Vet);
                GetVets();
            }
        }

        private void ButtonEditVet_OnClick(object sender, RoutedEventArgs e)
        {
            var item = (VetAmbulance.BL.Models.Vet)dataGridVets.SelectedItem;

            if (item == null)
            {
                return;
            }

            var vetWindows = new VetAmbulance.DesktopApp.VetWindow(new VetDTO()
            {
                Name = item.Name,
                AmbulanceId = item.AmbulanceId,

                Password = ""
            });

            vetWindows.Owner = this;
            vetWindows.ShowDialog();

            if (vetWindows.DialogResult == true)
            {
                vet.Edit(vetWindows.Vet, item.Id);
                GetVets();
            }
        }



        private void ButtonDeleteVet_OnClick(object sender, RoutedEventArgs e)
        {
            var item = (VetAmbulance.BL.Models.Vet)dataGridVets.SelectedItem;

            if (item == null)
            {
                return;
            }

            if (MessageBox.Show("Do you want to delete this Vet?", "Delete", MessageBoxButton.YesNo,
                    MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                vet.Delete(item.Id);
                GetVets();
            }
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            var adminMain = new AdminMain();
            adminMain.Show();
            this.Close();
        }
    }
}

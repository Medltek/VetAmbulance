using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Interaction logic for VetWindow.xaml
    /// </summary>
    public partial class VetWindow : Window
    {
        public VetDTO Vet { get; }

        private Ambulance ambulance;

        public VetWindow(VetDTO vet)
        {
            InitializeComponent();

            ambulance = Container.Get().Resolve<Ambulance>();
            this.Vet = vet;

            var ambulances = ambulance.GetAll();
            var ambulancesDictionary = new Dictionary<int, string>();

            foreach (var ambulancei in ambulances)
            {
                ambulancesDictionary.Add(ambulancei.Id, ambulancei.Address);
            }

            comboBoxAmbulance.ItemsSource = ambulancesDictionary;
            comboBoxAmbulance.DisplayMemberPath = "Value";
            comboBoxAmbulance.SelectedValuePath = "Key";

            textBoxName.Text = vet.Name;
            
            comboBoxAmbulance.SelectedValue = vet.AmbulanceId;
        }


        private void ButtonSave_OnClick(object sender, RoutedEventArgs e)
        {
            if (textBoxName.Text.Length == 0)
            {
                MessageBox.Show("Fill Name");
                return;
            }

            this.Vet.Name = textBoxName.Text;
            this.Vet.AmbulanceId = (int)comboBoxAmbulance.SelectedValue;
            
            this.Vet.Password = passwordBox.Password;

            DialogResult = true;
        }

        private void ButtonCancel_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}

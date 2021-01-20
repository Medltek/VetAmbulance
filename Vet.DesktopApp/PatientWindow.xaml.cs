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
    /// Interaction logic for PatientWindow.xaml
    /// </summary>
    public partial class PatientWindow : Window
    {

        public RegisterDTO Patient { get; }

        public PatientWindow(RegisterDTO patient)
        {
            InitializeComponent();


            this.Patient = patient;

        }


        private void ButtonSave_OnClick(object sender, RoutedEventArgs e)
        {

            this.Patient.ChipId = Int32.Parse(textBoxChipId.Text);
            this.Patient.Password = passwordBox.Password;

            DialogResult = true;
        }

        private void ButtonCancel_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

    }
}
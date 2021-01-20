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
using VetAmbulance.BL;
using VetAmbulance.BL.Models;
using VetAmbulance.DesktopApp;
using VetAmbulance.BL.Installer;


namespace VetAmbulance.DesktopApp
{
    /// <summary>
    /// Interaction logic for VetMain.xaml
    /// </summary>
    public partial class VetMain : Window
    {
        private VetAmbulance.BL.Models.Vet vet;
        private Ambulance ambulance;
        private Patient patient;
        private Diagnosis diagnosis;
        private Drug drug;

        private bool patientSelected = false;

        public VetMain(VetAmbulance.BL.Models.Vet vet)
        {
            InitializeComponent();

            ambulance = Container.Get().Resolve<VetAmbulance.BL.Models.Ambulance>();
            this.vet = Container.Get().Resolve<VetAmbulance.BL.Models.Vet>();
            patient = Container.Get().Resolve<Patient>();
            diagnosis = Container.Get().Resolve<Diagnosis>();
            drug = Container.Get().Resolve<Drug>();

            this.vet.GetById(this.vet.Id);
            ambulance.GetById(this.vet.AmbulanceId);

        }

        private void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            patient = Container.Get().Resolve<Patient>();
            patient.GetByChipId(Int32.Parse(textBoxSearch.Text));

            if (patient.ChipId == 0)
            {
                MessageBox.Show("Patient not found");


                GetDiagnoses();
                dataGridDiagnoses.ItemsSource = null;

                patientSelected = false;
                return;
            }

            

            patientSelected = true;
            MessageBox.Show("Patient selected");
            GetDiagnoses();

        }



        #region Diagnoses
        public void GetDiagnoses()
        {
            

            var diagnoses = patient.GetDiagnoses(patient.Id);
            dataGridDiagnoses.ItemsSource = diagnoses;
        }

        private void ButtonCreateDiagnoses_Click(object sender, RoutedEventArgs e)
        {
            if (patientSelected == false)
            {
                MessageBox.Show("Patient not selected");
            }
            var diagnosisWindow = new DiagnosisWindow(new DiagnosisDTO()
            {
                PatientId = patient.Id
            });  
            diagnosisWindow.Owner = this;
            diagnosisWindow.ShowDialog();

            if (diagnosisWindow.DialogResult == true)
            {
                diagnosis.Insert(diagnosisWindow.Diagnosis);
                GetDiagnoses();
            }
        }

        private void ButtonRegisterPatient_Click(object sender, RoutedEventArgs e)
        {

            var patientWindow = new PatientWindow(new RegisterDTO()
            {
                ChipId = patient.ChipId
            });
            patientWindow.Owner = this;
            patientWindow.ShowDialog();

            if (patientWindow.DialogResult == true)
            {
                patient.Insert(patientWindow.Patient);
                
            }
        }





        private void ButtonDeleteDiagnoses_Click(object sender, RoutedEventArgs e)
        {
            /*if (!patientSelected)
            {
                return;
            }*/

            var item = (Diagnosis)dataGridDiagnoses.SelectedItem;

            if (item == null)
            {
                return;
            }

            if (MessageBox.Show("Do you want to delete this diagnosis?", "Delete", MessageBoxButton.YesNo,
                    MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                diagnosis.Delete(item.Id);
                GetDiagnoses();
            }
        }



        #endregion

        
    }
}

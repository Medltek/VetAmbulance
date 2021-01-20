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

namespace VetAmbulance.DesktopApp
{
    /// <summary>
    /// Interaction logic for DiagnoseWindow.xaml
    /// </summary>
    public partial class DiagnosisWindow : Window
    {
        public DiagnosisDTO Diagnosis { get; }

        public DiagnosisWindow(DiagnosisDTO diagnosis)
        {
            InitializeComponent();

            this.Diagnosis = diagnosis;

            datePicker.SelectedDate = diagnosis.Date;
            if (diagnosis.Date == new DateTime())
            {
                datePicker.SelectedDate = DateTime.Now;
            }
            textBoxDisName.Text = diagnosis.DisName;
            textBoxSymptoms.Text = diagnosis.Symptoms;
            textBoxTherapy.Text = diagnosis.Therapy;
        }

        private void ButtonSave_OnClick(object sender, RoutedEventArgs e)
        {
            if (textBoxDisName.Text.Length == 0 || textBoxSymptoms.Text.Length == 0 || textBoxTherapy.Text.Length == 0 || datePicker.DisplayDate == new DateTime())
            {
                MessageBox.Show("Fill All");
                return;
            }

            if (datePicker.SelectedDate > DateTime.Now)
            {
                MessageBox.Show("Date > this date");
                return;
            }

            this.Diagnosis.Date = datePicker.SelectedDate ?? new DateTime();
            this.Diagnosis.DisName = textBoxDisName.Text;
            this.Diagnosis.Symptoms = textBoxSymptoms.Text;
            this.Diagnosis.Therapy = textBoxTherapy.Text;

            DialogResult = true;
        }

        private void ButtonCancel_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}

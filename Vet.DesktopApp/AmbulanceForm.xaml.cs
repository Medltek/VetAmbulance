using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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

namespace VetAmbulance.DesktopApp
{
    /// <summary>
    /// Interaction logic for AmbulanceForm.xaml
    /// </summary>
    public partial class AmbulanceForm : Window
    {
        public AmbulanceDTO Ambulance { get; }

        public AmbulanceForm(AmbulanceDTO ambulance)
        {
            InitializeComponent();

            this.Ambulance = ambulance;

            textBoxAddress.Text = ambulance.Address;

            textBoxOH.Text = ambulance.OpeningHour.ToString();
            textBoxCH.Text = ambulance.ClosingHour.ToString();
        }


        private void ButtonSave_OnClick(object sender, RoutedEventArgs e)
        {
            if (textBoxAddress.Text.Length == 0)
            {
                MessageBox.Show("Fill Adress");
                return;
            }

            if (textBoxOH.Text == "0")
                textBoxOH.Text = "700";

            if (textBoxCH.Text == "0")
                textBoxCH.Text = "1600";

            if (Convert.ToInt32(textBoxOH.Text) <= 2400 && Convert.ToInt32(textBoxOH.Text) >= 0 &&
                Convert.ToInt32(textBoxCH.Text) <= 2400 && Convert.ToInt32(textBoxCH.Text) >= 0 &&
                Convert.ToInt32(textBoxOH.Text) < Convert.ToInt32(textBoxCH.Text))
            {
                this.Ambulance.Address = textBoxAddress.Text;
                this.Ambulance.OpeningHour = Convert.ToInt32(textBoxOH.Text);
                this.Ambulance.ClosingHour = Convert.ToInt32(textBoxCH.Text);

                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Problem occured");
            }
        }

        private void ButtonCancel_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}

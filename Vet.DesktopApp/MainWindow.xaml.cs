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
using System.Windows.Navigation;
using System.Windows.Shapes;
using VetAmbulance.BL.Models;
using VetAmbulance.BL;
using VetAmbulance.BL.Security;

namespace VetAmbulance.DesktopApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private VetAmbulance.BL.Models.Vet vet;

        public MainWindow()
        {
            InitializeComponent();

            vet = Container.Get().Resolve<VetAmbulance.BL.Models.Vet>();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Login();
        }

        private void Login()
        {
            if (radioButtonVet.IsChecked == true)
            {
                // try to find the user
                var user = vet.GetByName(textBoxName.Text);
                if (user == null)
                {
                    MessageBox.Show("Wrooong!");
                    return;
                }

                if (!PasswordHelper.VerifyHashedPassword(user.PasswordHash, user.PasswordSalt, passwordBox.Password))
                {
                    MessageBox.Show("Wrooong!");
                    return;
                }

                Configuration.Save(new ConfigurationModel()
                {
                    IsAdmin = false,
                    Name = textBoxName.Text,
                    Password = passwordBox.Password
                });

                var vetMain = new VetMain(vet);
                vetMain.Show();
                this.Close();
            }
            else
            {
                if (textBoxName.Text == "admin" && passwordBox.Password == "123456")
                {
                    Configuration.Save(new ConfigurationModel()
                    {
                        IsAdmin = true,
                        Name = textBoxName.Text,
                        Password = passwordBox.Password
                    });

                    var adminMain = new AdminMain();
                    adminMain.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Wrooong!");
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var configuration = Configuration.Load();
            if (!(string.IsNullOrEmpty(configuration.Name) && string.IsNullOrEmpty(configuration.Password)))
            {
                radioButtonVet.IsChecked = !configuration.IsAdmin;
                radioButtonAdmin.IsChecked = configuration.IsAdmin;
                textBoxName.Text = configuration.Name;
                passwordBox.Password = configuration.Password;
                Login();
            }
        }

        
    }
}

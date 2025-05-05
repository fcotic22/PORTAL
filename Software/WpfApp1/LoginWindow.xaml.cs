using Bussiness_Logic_Layer;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IO;
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
using WpfApp1;

namespace Presentation_Layer
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }
        private UserService userService = new UserService();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var username = txtUsername.Text;
            var password = txtPassword.Password;

            var isLogged = userService.Login(username, password);
            
            if (username.IsNullOrEmpty() || password.IsNullOrEmpty()) 
            {
                txtWarning.Visibility = Visibility.Visible;
                txtWarning.Text = "Molimo unesi sve podatke";
            }
            else if(!isLogged) 
            {
                txtWarning.Visibility = Visibility.Visible;
                txtWarning.Text = "Unijeli ste pogrešne podatke";
            }
            else
            {
                var window = new MainWindow();
                window.Show();
                this.Close();
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) 
            { 
                Button_Click(sender, e);
            }
        }
    }
}


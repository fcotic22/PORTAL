using Bussiness_Logic_Layer.Services;
using Entities;
using Entities.Models;
using Microsoft.IdentityModel.Tokens;
using Notifications.Wpf.Core;
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

namespace Presentation_Layer.UserControls
{
    /// <summary>
    /// Interaction logic for BuyersUC.xaml
    /// </summary>
    public partial class BuyersUC : UserControl
    {
        private BuyerService buyerService;
        public BuyersUC()
        {
            InitializeComponent();
            buyerService = new BuyerService();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            dgBuyers.ItemsSource = BuyerService.GetAllBuyers();

            dgBuyers.Columns[0].Visibility = Visibility.Hidden;
            dgBuyers.Columns[11].Visibility = Visibility.Hidden;

            dgBuyers.Columns[1].Header = "Ime/Prezime/Firma";
            dgBuyers.Columns[2].Header = "OIB";
            dgBuyers.Columns[3].Header = "Email";
            dgBuyers.Columns[4].Header = "Telefon";
            dgBuyers.Columns[5].Header = "Adresa";
            dgBuyers.Columns[6].Header = "Grad";
            dgBuyers.Columns[7].Header = "Zemlja";
            dgBuyers.Columns[8].Header = "Poštanski broj";
            dgBuyers.Columns[9].Header = "Naziv tvrtke";
            dgBuyers.Columns[10].Header = "Broj računa";

            dgBuyers.Columns[5].DisplayIndex = 2;
            dgBuyers.Columns[6].DisplayIndex = 3;
            dgBuyers.Columns[7].DisplayIndex = 4;
            dgBuyers.Columns[8].DisplayIndex = 5;
            dgBuyers.Columns[9].DisplayIndex = 6;


        }
        private void btnAddBuyer_Click(object sender, RoutedEventArgs e)
        {
            formForAddingAndEditing.Visibility = Visibility.Visible;
            txtHeader.Text = "Dodavanje novog kupca";
            btnAdd.Visibility = Visibility.Visible;
            btnEdit.Visibility = Visibility.Hidden;
        }

        private void btnEditBuyer_Click(object sender, RoutedEventArgs e)
        {
            var selected = dgBuyers.SelectedItem as Buyer;
            if(selected != null)
            {
                formForAddingAndEditing.Visibility = Visibility.Visible;
                txtHeader.Text = "Uređivanje kupca";
                btnEdit.Visibility = Visibility.Visible;
                btnAdd.Visibility = Visibility.Hidden;
                formForAddingAndEditing.DataContext = selected;
            }
            else 
            {
                UCHelper.DisplayNotification("KUPCI", "Morate odabrati kupca za uređivanje", NotificationType.Warning);
            }
        }

        private void btnDeleteBuyer_Click(object sender, RoutedEventArgs e)
        {
            var selected = dgBuyers.SelectedItem as Buyer;
            if (selected != null)
            {
                var result = MessageBox.Show("Da li ste sigurni da želite obrisati odabranog kupca?", "Potvrda brisanja", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    buyerService.RemoveBuyer(selected);
                    UCHelper.DisplayNotification("KUPCI", "Uspješno izbrisan kupac", NotificationType.Success);
                    UserControl_Loaded(sender, e);
                }
            }
            else
            {
                UCHelper.DisplayNotification("KUPCI", "Molimo odaberite kupca", NotificationType.Warning);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ClearAndCloseForm();
            UserControl_Loaded(sender, e);
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateForm() == false) return;

            var selected = dgBuyers.SelectedItem as Buyer;

            Buyer newBuyer = new Buyer()
            {
                id = selected.id,
                name = txtName.Text,
                adress = txtAdress.Text,
                city = txtCity.Text,
                country = txtCountry.Text,
                zipCode = int.Parse(txtZipcode.Text),
                company = txtCompanyName.Text,
                oib = int.Parse(txtOib.Text),
                email = txtEmail.Text,
                phone = txtPhone.Text,
                bankAccountNumber = txtBankAccountNumber.Text
            };
            buyerService.UpdateBuyer(newBuyer);
            ClearAndCloseForm();
            UCHelper.DisplayNotification("KUPCI", "Uspješno dodana izmjena kupca", NotificationType.Success);
            UserControl_Loaded(sender, e);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateForm() == false) return;

            Buyer newBuyer = new Buyer()
            {
                name = txtName.Text,
                adress = txtAdress.Text,
                city = txtCity.Text,
                country = txtCountry.Text,
                zipCode = int.Parse(txtZipcode.Text),
                company = txtCompanyName.Text,
                oib = int.Parse(txtOib.Text),
                email = txtEmail.Text,
                phone = txtPhone.Text,
                bankAccountNumber = txtBankAccountNumber.Text
            };
            buyerService.AddNewBuyer(newBuyer);
            ClearAndCloseForm();
            UCHelper.DisplayNotification("KUPCI", "Uspješno dodan novi kupac", NotificationType.Success);
            UserControl_Loaded(sender, e);
        }

        private bool ValidateForm()
        {
            txtMessage.Text = string.Empty;
            if (txtName.Text.IsNullOrEmpty() || txtCity.Text.IsNullOrEmpty() || txtCountry.Text.IsNullOrEmpty() || txtAdress.Text.IsNullOrEmpty())
            {
                txtMessage.Text = "Molimo popunite sva obavezna polja";
                return false;
            }
            else if (!txtOib.Text.IsNullOrEmpty() && !int.TryParse(txtOib.Text, out _))
            {
                txtMessage.Text = "OIB kupca mora biti broj";
                return false;
            }
            else if (!txtZipcode.Text.IsNullOrEmpty() && !int.TryParse(txtZipcode.Text, out _))
            {
                txtMessage.Text = "Poštanski broj mora biti broj";
                return false;
            }
            else
            {
                return true;
            }

        }

        private void ClearAndCloseForm()
        {
            txtHeader.Text = string.Empty;
            txtMessage.Text = string.Empty;
            txtName.Text = string.Empty;
            txtAdress.Text = string.Empty;
            txtCity.Text = string.Empty;
            txtCountry.Text = string.Empty;
            txtZipcode.Text = string.Empty;
            txtCompanyName.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtBankAccountNumber.Text = string.Empty;
            formForAddingAndEditing.Visibility = Visibility.Hidden;
            dgBuyers.SelectedItem = null;
        }
    }
}

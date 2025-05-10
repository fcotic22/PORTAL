using Bussiness_Logic_Layer.Services;
using Entities.Models;
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
    /// Interaction logic for EmployeesUC.xaml
    /// </summary>
    public partial class EmployeesUC : UserControl
    {
        private EmployeeService employeeService;
        private NotificationManager notificationManager;
        private NotificationService notificationService;
        private UCHelper UCHelper;
        private LeaveService leaveService;
        public EmployeesUC()
        {
            InitializeComponent();
            employeeService = new EmployeeService();
            notificationManager = new NotificationManager();
            notificationService = new NotificationService();
            UCHelper = new UCHelper();
            leaveService = new LeaveService();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            dgEmployees.ItemsSource = employeeService.GetAllEmployees();

            dgEmployees.Columns[0].Visibility = Visibility.Hidden;
            dgEmployees.Columns[14].Visibility = Visibility.Hidden;
            dgEmployees.Columns[15].Visibility = Visibility.Hidden;
            dgEmployees.Columns[16].Visibility = Visibility.Hidden;
            dgEmployees.Columns[17].Visibility = Visibility.Hidden;
        
            dgEmployees.Columns[1].Header = "Ime";
            dgEmployees.Columns[2].Header = "Prezime";
            dgEmployees.Columns[3].Header = "OIB/JMBG";
            dgEmployees.Columns[4].Header = "Telefon";
            dgEmployees.Columns[5].Header = "Adresa";
            dgEmployees.Columns[6].Header = "Grad";
            dgEmployees.Columns[7].Header = "Zemlja";
            dgEmployees.Columns[8].Header = "Poštanski broj";
            dgEmployees.Columns[9].Header = "Broj računa";
            dgEmployees.Columns[10].Header = "Zabilješke";
            dgEmployees.Columns[11].Header = "Plaća po satu";
            dgEmployees.Columns[12].Header = "Status zaposlenja";
            dgEmployees.Columns[13].Header = "Trenutno na bolovanju";
        }

        private void btnAddEmployee_Click(object sender, RoutedEventArgs e)
        {
            formForAddingAndEditing.Visibility = Visibility.Visible;
            txtHeader.Text = "Dodavanje zaposlenika";
            btnAdd.Visibility = Visibility.Visible;
            btnEdit.Visibility = Visibility.Hidden;
            MakeLeaveInputsHidden();
            cmbStatus.ItemsSource = new List<string> { "Stalno", "Ispomoć" };
        }

        private void btnEditEmployee_Click(object sender, RoutedEventArgs e)
        {
            var selectedEmployee = dgEmployees.SelectedItem as Employee;
            cmbStatus.ItemsSource = new List<string> { "Stalno", "Ispomoć", "Stalno - na bolovanju", "Otpušten" };

            if (selectedEmployee != null)
            {
                formForAddingAndEditing.Visibility = Visibility.Visible;
                txtHeader.Text = "Uređivanje podataka zaposlenika";
                btnEdit.Visibility = Visibility.Visible;
                btnAdd.Visibility = Visibility.Hidden;

                txtName.Text = selectedEmployee.name;
                txtSurname.Text = selectedEmployee.surname;
                txtOib.Text = selectedEmployee.oib.ToString();
                txtTelephone.Text = selectedEmployee.phone;
                txtAdress.Text = selectedEmployee.adress;
                txtCity.Text = selectedEmployee.city;
                txtCountry.Text = selectedEmployee.country;
                txtZipcode.Text = selectedEmployee.zipCode.ToString();
                txtBankAccountNumber.Text = selectedEmployee.bankAccountNumber;
                txtNote.Text = selectedEmployee.notes;
                txtHourlyPay.Text = selectedEmployee.hourlyPay.ToString();
                cmbStatus.Text = selectedEmployee.status.ToString();
                if(selectedEmployee.isOnLeave == 1)
                {
                    MakeLeaveInputsVisible();
                    var currentLeave = leaveService.GetCurrentLeaveForEmployeeId(selectedEmployee.id);
                    dtpLeaveStart.SelectedDate = currentLeave.startDate;
                    txtLeaveReason.Text = currentLeave.reasonForLeave;
                }
            }
            else
            {
                UCHelper.DisplayNotification("ZAPOSLENICI", "Molimo odaberite zaposlenika", NotificationType.Warning);
            }
        }

        private void btnRemoveEmployee_Click(object sender, RoutedEventArgs e)
        {
            var selectedEmployee = dgEmployees.SelectedItem as Employee;
            if (selectedEmployee != null)
            {
                var result = MessageBox.Show("Da li ste sigurni da želite obrisati odabranog zaposlenika?", "Potvrda brisanja", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    employeeService.RemoveEmployee(selectedEmployee);
                    leaveService.RemoveAllLeavesForEmployeeId(selectedEmployee.id);
                    UCHelper.DisplayNotification("ZAPOSLENICI", "Uspješno izbrisan zaposlenik", NotificationType.Success);
                    UserControl_Loaded(sender, e);
                }
            }
            else
            {
                UCHelper.DisplayNotification("ZAPOSLENICI", "Molimo odaberite zaposlenika", NotificationType.Warning);
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateForm() == false) return;

            Employee newEmployee = new Employee()
            {
                name = txtName.Text,
                surname = txtSurname.Text,
                oib = int.Parse(txtOib.Text),
                phone = txtTelephone.Text,
                adress = txtAdress.Text,
                city = txtCity.Text,
                country = txtCountry.Text,
                zipCode = int.Parse(txtZipcode.Text),
                bankAccountNumber = txtBankAccountNumber.Text,
                notes = txtNote.Text,
                hourlyPay = int.Parse(txtHourlyPay.Text),
                status = cmbStatus.Text,
                isOnLeave = 0
            };
            employeeService.AddNewEmployee(newEmployee);
            ClearAndCloseForm();
            UCHelper.DisplayNotification("ZAPOSLENICI", "Uspješno dodan novi zaposlenik", NotificationType.Success);
            UserControl_Loaded(sender, e);
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateForm() == false) return;

            var isOnLeave = false;
            if(cmbStatus.SelectedIndex == 2)
            {
                if(ValidateLeaveForm() == false) return;

                var selectedEmployee = dgEmployees.SelectedItem as Employee;
                isOnLeave = true;
                var currentLeave = leaveService.GetCurrentLeaveForEmployeeId(selectedEmployee.id);

                if (currentLeave != null)
                {
                    currentLeave.endDate = dtpLeaveEnd.SelectedDate;
                    currentLeave.reasonForLeave = txtLeaveReason.Text;

                    if (dtpLeaveEnd.SelectedDate.HasValue)
                    {
                        isOnLeave = false;
                        cmbStatus.SelectedIndex = 0;
                        leaveService.UpdateLeave(currentLeave);
                    }
                    else if (selectedEmployee.isOnLeave == 1)
                    {
                        leaveService.UpdateLeave(currentLeave);
                    }
                }
                else
                {
                    var newLeave = new Leave()
                    {
                        employee_id = selectedEmployee.id,
                        startDate = dtpLeaveStart.SelectedDate.Value,
                        endDate = dtpLeaveEnd.SelectedDate,
                        reasonForLeave = txtLeaveReason.Text,
                    };
                    leaveService.AddNewLeave(newLeave);
                }
            }

            var oldEmployee = dgEmployees.SelectedItem as Employee;
            Employee employee = new Employee()
            {
                id = oldEmployee.id,
                name = txtName.Text,
                surname = txtSurname.Text,
                oib = int.Parse(txtOib.Text),
                phone = txtTelephone.Text,
                adress = txtAdress.Text,
                city = txtCity.Text,
                country = txtCountry.Text,
                zipCode = int.Parse(txtZipcode.Text),
                bankAccountNumber = txtBankAccountNumber.Text,
                notes = txtNote.Text,
                hourlyPay = int.Parse(txtHourlyPay.Text),
                status = cmbStatus.Text,
                isOnLeave = isOnLeave ? 1 : 0, 
            };
            employeeService.UpdateEmployee(employee);
            ClearAndCloseForm();
            UCHelper.DisplayNotification("ZAPOSLENICI", "Uspješno dodana izmjena zaposlenika", NotificationType.Success);
            UserControl_Loaded(sender, e);
        }

        private bool ValidateLeaveForm()
        {
            if (dtpLeaveStart.Text == string.Empty)
            {
                txtMessage.Text = "Molimo popunite datum početka bolovanja";
                return false;
            }
            else if (txtLeaveReason.Text == string.Empty)
            {
                txtMessage.Text = "Molimo popunite razlog bolovanja";
                return false;
            }
            else if (dtpLeaveStart.Text != string.Empty && dtpLeaveStart.SelectedDate > dtpLeaveEnd.SelectedDate)
            {
                txtMessage.Text = "Datum početka bolovanja ne može biti veći od datuma završetka bolovanja";
                return false;
            }

            else
                return true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ClearAndCloseForm();
        }
        private bool ValidateForm()
        {
            if (txtName.Text == string.Empty || txtSurname.Text == string.Empty || txtCity.Text == string.Empty || txtCountry.Text == string.Empty || txtHourlyPay.Text == string.Empty)
            {
                txtMessage.Text = "Molimo popunite sva polja označena sa zvjezdicom";
                return false;
            }
            else if(int.TryParse(txtOib.Text.ToString(), out int s) == false)
            {
                txtMessage.Text = "OIB/JMBG mora biti broj";
                return false;
            }
            else if (txtZipcode.Text != string.Empty && int.TryParse(txtOib.Text.ToString(), out int n) == false)
            {
                txtMessage.Text = "Poštanski broj mora biti broj";
                return false;
            }
            else if (txtBankAccountNumber.Text != string.Empty && int.TryParse(txtBankAccountNumber.Text.ToString(), out int d) == false)
            {
                txtMessage.Text = "Broj računa mora biti broj";
                return false;
            }
            else if (int.TryParse(txtHourlyPay.Text.ToString(), out int f) == false)
            {
                txtMessage.Text = "Plaća po satu mora biti broj";
                return false;
            }
            else return true;
        }

        private void ClearAndCloseForm()
        {
            txtMessage.Text = string.Empty;
            txtName.Text = string.Empty;
            txtSurname.Text = string.Empty;
            txtOib.Text = string.Empty;
            txtTelephone.Text = string.Empty;
            txtAdress.Text = string.Empty;
            txtCity.Text = string.Empty;
            txtCountry.Text = string.Empty;
            txtZipcode.Text = string.Empty;
            txtBankAccountNumber.Text = string.Empty;
            txtNote.Text = string.Empty;
            txtHourlyPay.Text = string.Empty;
            cmbStatus.Text = string.Empty;
            dtpLeaveStart.Text = string.Empty;
            dtpLeaveEnd.Text = string.Empty;
            formForAddingAndEditing.Visibility = Visibility.Hidden;
        }


        private void cmbStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbStatus.SelectedIndex == 2)
            {
                MakeLeaveInputsVisible();
            }
            else
            {
                MakeLeaveInputsHidden();
            }
        }

        private void MakeLeaveInputsHidden()
        {
            lblLeaveStart.Visibility = Visibility.Hidden;
            dtpLeaveStart.Visibility = Visibility.Hidden;
            lblLeaveEnd.Visibility = Visibility.Hidden;
            dtpLeaveEnd.Visibility = Visibility.Hidden; 
            lblLeaveReason.Visibility = Visibility.Hidden; 
            txtLeaveReason.Visibility = Visibility.Hidden; 
        }

        private void MakeLeaveInputsVisible()
        {
            lblLeaveStart.Visibility = Visibility.Visible;
            dtpLeaveStart.Visibility = Visibility.Visible;
            lblLeaveEnd.Visibility = Visibility.Visible;
            dtpLeaveEnd.Visibility = Visibility.Visible; 
            lblLeaveReason.Visibility = Visibility.Visible;
            txtLeaveReason.Visibility = Visibility.Visible;
        }
    }
    }
using Bussiness_Logic_Layer.Services;
using Entities;
using Entities.Models;
using Microsoft.IdentityModel.Tokens;
using Notifications.Wpf.Core;
using Notifications.Wpf.Core.Controls;
using System.Windows;
using System.Windows.Controls;

namespace Presentation_Layer.UserControls
{
    /// <summary>
    /// Interaction logic for VehiclesUC.xaml
    /// </summary>
    public partial class VehiclesUC : UserControl
    {
        private VehicleService vehicleService;
        private EmployeeService employeeService;
        private NotificationManager notificationManager;
        private NotificationService notificationService;
        private UCHelper UCHelper;
        public VehiclesUC()
        {
            InitializeComponent();
            vehicleService = new VehicleService();
            employeeService = new EmployeeService();
            notificationManager = new NotificationManager();
            notificationService = new NotificationService();
            UCHelper = new UCHelper();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            var allVehicles = vehicleService.GetAllVehicles();
            dgVehicles.ItemsSource = allVehicles;
            CheckRegistrationExpiry(allVehicles);
            
            dgVehicles.RowHeight = 30;
            dgVehicles.ColumnWidth = 150;

            dgVehicles.Columns[0].Visibility = Visibility.Hidden;
            dgVehicles.Columns[11].Visibility = Visibility.Hidden;
            dgVehicles.Columns[1].Header = "Naziv vozila";
            dgVehicles.Columns[2].Header = "Model";
            dgVehicles.Columns[3].Header = "Registarska oznaka";
            dgVehicles.Columns[4].Header = "Registracija vrijedi do";
            dgVehicles.Columns[5].Header = "Proizvođač";
            dgVehicles.Columns[6].Header = "Tip goriva";
            dgVehicles.Columns[7].Header = "Broj sjedišta";
            dgVehicles.Columns[8].Header = "Godina proizvodnje";
            dgVehicles.Columns[9].Header = "Trenutno dodjeljen";
            dgVehicles.Columns[10].Header = "Dodjeljen osobi ";
        }

        private void btnRemoveVehicle_Click(object sender, RoutedEventArgs e)
        {
            var selectedVehicle = dgVehicles.SelectedItem as Vehicle;
            if (selectedVehicle != null)
            {
                var result = MessageBox.Show("Da li ste sigurni da želite obrisati odabrano vozilo?", "Potvrda brisanja", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    vehicleService.RemoveVehicle(selectedVehicle);
                    UCHelper.DisplayNotification("VOZILA", "Uspješno izbrisano vozilo", NotificationType.Success);
                    UserControl_Loaded(sender, e);
                }
            }
            else
            {
                UCHelper.DisplayNotification("VOZILA","Molimo odaberite vozilo", NotificationType.Warning);
            }
        }

        private void btnAddVehicle_Click(object sender, RoutedEventArgs e)
        {
            formForAddingAndEditing.Visibility = Visibility.Visible;
            txtHeader.Text = "Dodavanje vozila";
            btnAdd.Visibility = Visibility.Visible;
            btnEdit.Visibility = Visibility.Hidden;
            cmbEmployee.ItemsSource = employeeService.GetAllEmployeeNames();
        }

        private void btnEditVehicle_Click(object sender, RoutedEventArgs e)
        {
            var selectedVehicle = dgVehicles.SelectedItem as Vehicle;
            if (selectedVehicle != null)
            {
                formForAddingAndEditing.Visibility = Visibility.Visible;
                txtHeader.Text = "Uređivanje vozila";
                btnEdit.Visibility = Visibility.Visible;
                btnAdd.Visibility = Visibility.Hidden;

                cmbEmployee.ItemsSource = employeeService.GetAllEmployeeNames();
                txtName.Text = selectedVehicle.name;
                txtModel.Text = selectedVehicle.model;
                txtRegistration.Text = selectedVehicle.licensePlate;
                dtpRegistrationValidTo.Text = selectedVehicle.registrationValidTo.ToString();
                txtNoOfSeats.Text = selectedVehicle.noOfSeats.ToString();
                txtManufacturer.Text = selectedVehicle.manufacturer;
                cmbFuelType.Text = selectedVehicle.fuelType;
                txtProductionYear.Text = selectedVehicle.productionYear.ToString();
            }
            else
            {
                UCHelper.DisplayNotification("VOZILA", "Molimo odaberite vozilo", NotificationType.Warning);
            }
        }

        private void btnAction_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateForm() == false) return;
           
            var isAssigned = 0;
            string assignedTo = null;

            if (cmbEmployee.SelectedIndex != -1)
            {
                isAssigned = 1;
                assignedTo = cmbEmployee.Text;
                cmbEmployee.Width = 320;
                btnRemoveAssignment.Visibility = Visibility.Visible;
            }

            var vehicle = dgVehicles.SelectedItem as Vehicle;
            Vehicle newVehicle = new Vehicle()
            {
                id = vehicle.id,
                name = txtName.Text,
                model = txtModel.Text,
                licensePlate = txtRegistration.Text,
                registrationValidTo = dtpRegistrationValidTo.SelectedDate.HasValue
                ? dtpRegistrationValidTo.SelectedDate.Value
                : DateTime.MinValue,
                noOfSeats = int.Parse(txtNoOfSeats.Text.ToString()),
                manufacturer = txtManufacturer.Text,
                fuelType = cmbFuelType.Text,
                productionYear = int.Parse(txtProductionYear.Text),
                isCurrentlyAssigned = isAssigned,
                assignedTo = assignedTo
            };
            vehicleService.UpdateVehicle(newVehicle);
            ClearAndCloseForm();
            UCHelper.DisplayNotification("VOZILA", "Uspješno dodana izmjena vozila", NotificationType.Success);
            UserControl_Loaded(sender, e);
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if(ValidateForm() == false) return;
            txtHeader.Text = "Dodavanje vozila";

            cmbEmployee.ItemsSource = employeeService.GetAllEmployeeNames();
            var isAssigned = 0;
            string assignedTo = null;

            if (cmbEmployee.SelectedIndex != -1)
            {
                isAssigned = 1;
                assignedTo = cmbEmployee.Text;
                cmbEmployee.Width = 320;
                btnRemoveAssignment.Visibility = Visibility.Visible;
            }

            Vehicle newVehicle = new Vehicle()
            {
                name = txtName.Text,
                model = txtModel.Text,
                licensePlate = txtRegistration.Text,
                registrationValidTo = dtpRegistrationValidTo.SelectedDate.HasValue
                ? dtpRegistrationValidTo.SelectedDate.Value
                : DateTime.MinValue,
                noOfSeats = int.Parse(txtNoOfSeats.Text.ToString()),
                manufacturer = txtManufacturer.Text,
                fuelType = cmbFuelType.Text,
                productionYear = int.Parse(txtProductionYear.Text),
                isCurrentlyAssigned = isAssigned,
                assignedTo = assignedTo
            };
            vehicleService.AddNewVehicle(newVehicle);
            ClearAndCloseForm();
            UCHelper.DisplayNotification("VOZILA", "Uspješno dodano novo vozilo", NotificationType.Success);
            UserControl_Loaded(sender, e);
        }

        private void formForAddingAndEditing_Loaded(object sender, RoutedEventArgs e)
        {
            cmbFuelType.ItemsSource = new List<string>
            {
                "Benzin",
                "Diesel",
                "Hibrid",
                "Električni"
            };
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ClearAndCloseForm();
        }

        private void ClearAndCloseForm()
        {
            txtName.Text = string.Empty;
            txtModel.Text = string.Empty;
            txtRegistration.Text = string.Empty;
            dtpRegistrationValidTo.Text = string.Empty;
            txtNoOfSeats.Text = string.Empty;
            txtManufacturer.Text = string.Empty;
            cmbFuelType.Text = string.Empty;
            txtProductionYear.Text = string.Empty;
            formForAddingAndEditing.Visibility = Visibility.Hidden;
        }

        private bool ValidateForm()
        {
            txtMessage.Text = string.Empty;
            if (txtName.Text.IsNullOrEmpty() || txtModel.Text.IsNullOrEmpty() || txtRegistration.Text.IsNullOrEmpty() || dtpRegistrationValidTo.Text.IsNullOrEmpty() || txtNoOfSeats.Text.IsNullOrEmpty() || txtManufacturer.Text.IsNullOrEmpty() || cmbFuelType.Text.IsNullOrEmpty() || txtProductionYear.Text.IsNullOrEmpty())
            {
                txtMessage.Text = "Molimo popunite sva polja";
                return false;
            }
            else if (!int.TryParse(txtNoOfSeats.Text, out _))
            {
                txtMessage.Text = "Broj sjedišta mora biti broj";
                return false;
            }
            else if (!int.TryParse(txtProductionYear.Text, out _))
            {
                txtMessage.Text = "Godina proizvodnje mora biti broj";
                return false;
            }
            else
            {
                return true;
            }
        }

        private void btnRemoveAssignment_Click(object sender, RoutedEventArgs e)
        {
            cmbEmployee.SelectedIndex = -1;
            cmbEmployee.Width = 350;
            btnRemoveAssignment.IsEnabled = false;
            btnRemoveAssignment.Visibility = Visibility.Hidden;
        }

        private void cmbEmployee_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cmbEmployee.Width = 320;
            btnRemoveAssignment.Visibility = Visibility.Visible;
        }

        private void CheckRegistrationExpiry(List<Vehicle> vehicles)
        {
            foreach (Vehicle vehicle in vehicles) 
            {
                var daysUntilExpiry = (vehicle.registrationValidTo - DateTime.Now).TotalDays;
                if (daysUntilExpiry < 0)
                {   
                    UCHelper.DisplayNotification("VOZILA", $"Registracija vozila {vehicle.name} je istekla", NotificationType.Error);

                    if (notificationService.ContainsNotificationWithTitle($"Istek registracije vozila {vehicle.name}") == false)
                    {
                        var systemNotification = new Entities.Models.Notification
                        {
                            title = $"Istek registracije vozila {vehicle.name}",
                            message = $"Vozilu {vehicle.name} registarske oznake {vehicle.licensePlate} je istekla registracija",
                            priority = Enumerations.Priority.High.ToString(),
                            status = Enumerations.Status.Open.ToString(),
                            date = DateTime.Now,
                        };
                        notificationService.AddNewNotification(systemNotification);
                    }
                }
                else if(daysUntilExpiry < 15)
                {
                    UCHelper.DisplayNotification("VOZILA", $"Registracija vozila {vehicle.name} ističe za {((int)daysUntilExpiry)} dana", NotificationType.Warning);
                }
            }
        }
    }
}

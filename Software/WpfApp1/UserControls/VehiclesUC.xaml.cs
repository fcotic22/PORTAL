using Bussiness_Logic_Layer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
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
using Color = System.Windows.Media.Color;

namespace Presentation_Layer.UserControls
{
    /// <summary>
    /// Interaction logic for VehiclesUC.xaml
    /// </summary>
    public partial class VehiclesUC : UserControl
    {
        private VehicleService vehicleService;
        public VehiclesUC()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            vehicleService = new VehicleService();
            dgVehicles.ItemsSource = vehicleService.GetAllVehicles();
            
            dgVehicles.Columns[0].Visibility = Visibility.Hidden;
            dgVehicles.Columns[11].Visibility = Visibility.Hidden;
            dgVehicles.Columns[0].Header = "Naziv vozila";
            dgVehicles.Columns[1].Header = "Model";
            dgVehicles.Columns[2].Header = "Registarska oznaka";
            dgVehicles.Columns[3].Header = "Registracija vrijedi do";
            dgVehicles.Columns[4].Header = "Proizvođač";
            dgVehicles.Columns[5].Header = "Tip goriva";
            dgVehicles.Columns[6].Header = "Broj sjedišta";
            dgVehicles.Columns[7].Header = "Broj sjedala";
            dgVehicles.Columns[8].Header = "Godina proizvodnje";
            dgVehicles.Columns[9].Header = "Trenutno dodjeljen";
            dgVehicles.Columns[10].Header = "Dodjeljen osobi ";
        }
    }
}

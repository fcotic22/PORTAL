using Bussiness_Logic_Layer;
using Entities.Models;
using Presentation_Layer;
using Presentation_Layer.UserControls;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            GUIManager.MainWindow = this;
        }

        private void btnVehicles_Click(object sender, RoutedEventArgs e)
        {
            GUIManager.Open(new VehiclesUC());
        }

        private void btnProjects_Click(object sender, RoutedEventArgs e)
        {
            GUIManager.Open(new ProjectsUC());
        }

        private void btnConstructionSites_Click(object sender, RoutedEventArgs e)
        {
            GUIManager.Open(new ConstructionSitesUC());
        }

        private void btnTools_Click(object sender, RoutedEventArgs e)
        {
            GUIManager.Open(new ToolsUC());
        }

        private void btnResources_Click(object sender, RoutedEventArgs e)
        {
            GUIManager.Open(new ResourcesUC());
        }

        private void btnEmployees_Click(object sender, RoutedEventArgs e)
        {
            GUIManager.Open(new EmployeesUC());
        }

        private void btnBuyers_Click(object sender, RoutedEventArgs e)
        {
            GUIManager.Open(new BuyersUC());
        }

        private void btnOrders_Click(object sender, RoutedEventArgs e)
        {
            GUIManager.Open(new OrdersUC());
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GUIManager.Open(new ProjectsUC());
        }
        private void btnChatbot_Click(object sender, RoutedEventArgs e)
        {
            GUIManager.Open(new ChatbotUC());
        }
    }
}
using Bussiness_Logic_Layer;
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
            GuiManager.MainWindow = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GuiManager.Open(new ProjectsUC());
        }
    }
}
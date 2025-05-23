using Bussiness_Logic_Layer.Services;
using Entities.Models;
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
    /// Interaction logic for ConstructionSiteUC.xaml
    /// </summary>
    public partial class ConstructionSiteUC : UserControl
    {
        private Project Project;
        public ConstructionSiteUC(int project_id)
        {
            InitializeComponent();
            Project = ProjectService.GetProjectById(project_id);
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            GUIManager.Close();
        }
    }
}

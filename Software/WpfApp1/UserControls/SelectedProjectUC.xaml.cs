using Entities.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
    /// Interaction logic for SelectedProjectUC.xaml
    /// </summary>
    public partial class SelectedProjectUC : UserControl
    {
        private Project Project;
        public SelectedProjectUC(Project project)
        {
            InitializeComponent();
            Project = project;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
        }
        private void btnFacadeProject_Click(object sender, RoutedEventArgs e)
        {
            GUIManager.Open(new FacadeProjectUC(Project.id));
        }

        private void btnALUproject_Click(object sender, RoutedEventArgs e)
        {
            GUIManager.Open(new ALUProjectUC(Project.id));
        }

        private void btnPVCproject_Click(object sender, RoutedEventArgs e)
        {
            GUIManager.Open(new PVCProjectUC(Project.id));
        }   
    }
}

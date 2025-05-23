using Bussiness_Logic_Layer.Services;
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
        private Buyer Buyer;
        public SelectedProjectUC(Project project)
        {
            InitializeComponent();
            Project = project;
            Buyer = BuyerService.GetBuyerById(project.buyer_id);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            txtName.Text += " " + Project.name;
            txtDescription.Text += " " + Project.description;
            txtBuyer.Text += " " + Buyer.name;
            txtStartDate.Text += " " + Project.startDate.ToString("dd/MM/yyyy");
            txtDueDate.Text += " " + Project.dueDate.ToString("dd/MM/yyyy");
            txtEndDate.Text += " " + Project.endDate?.ToString("dd/MM/yyyy");
            txtStatus.Text += " " + Project.status.ToString();
            txtPriority.Text += " " + Project.priority.ToString();
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

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            GUIManager.Open(new ProjectsUC());
        }
        private void btnBuyer_Click(object sender, RoutedEventArgs e)
        {
            info.Visibility = Visibility.Visible;
            info.DataContext = Buyer;
        }
        private void BtnCloseBuyerInfo_Click(object sender, RoutedEventArgs e)
        {
            info.Visibility = Visibility.Collapsed;
        }

        private void btnCSite_Click(object sender, RoutedEventArgs e)
        {
            GUIManager.Open(new ConstructionSiteUC(Project.id));
        }

        private void btnFiles_Click(object sender, RoutedEventArgs e)
        {
            GUIManager.Open(new ProjectFilesUC(Project.id));
        }

        private void btnNotes_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

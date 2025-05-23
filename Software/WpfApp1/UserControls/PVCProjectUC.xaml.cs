using Bussiness_Logic_Layer.Services;
using Data_Access_Layer.Repositories;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
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
    /// Interaction logic for PVCProjectUC.xaml
    /// </summary>
    public partial class PVCProjectUC : UserControl
    {
        private Project project;
        private Subproject pvcSubproject;
        public PVCProjectUC(int project_id)
        {
            InitializeComponent();
            project = ProjectService.GetProjectById(project_id);
            pvcSubproject = SubprojectService.GetSubrojectByProjectIdAndSubprojectType(project_id, ((int)Enumerations.ProjectType.PVC));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            GUIManager.Close();
        }
    }
}

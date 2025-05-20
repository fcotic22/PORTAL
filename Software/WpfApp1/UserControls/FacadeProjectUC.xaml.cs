using Bussiness_Logic_Layer.Services;
using Entities;
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
    /// Interaction logic for FacadeProjectUC.xaml
    /// </summary>
    public partial class FacadeProjectUC : UserControl
    {
        private Project project;
        private Subproject facadeSubproject;
        public FacadeProjectUC(int project_id)
        {
            InitializeComponent();
            project = ProjectService.GetProjectById(project_id);
            facadeSubproject = SubprojectService.GetSubrojectByProjectIdAndSubprojectType(project_id, ((int)Enumerations.ProjectType.FACADE));
        }
    }
}

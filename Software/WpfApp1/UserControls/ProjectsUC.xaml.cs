using Data_Access_Layer.Repositories;
using Entities.Models;
using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Presentation_Layer.UserControls
{
    /// <summary>
    /// Interaction logic for ProjectsUC.xaml
    /// </summary>
    public partial class ProjectsUC : UserControl
    {
        private ProjectRepository projectRepository { get; set; }
        private List<Project> projects;

        public ProjectsUC()
        {
            InitializeComponent();
            projectRepository = new ProjectRepository();
            projects = projectRepository.GetAll().ToList();
            this.DataContext = projects;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
           
        }

        private void dgProjects_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var selected = dgProjects.SelectedItem as Project;
            if (selected != null)
            {
                GUIManager.Open(new SelectedProjectUC(selected));
            }
        }
    }
}

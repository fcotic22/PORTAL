using Bussiness_Logic_Layer.Services;
using Data_Access_Layer.Repositories;
using Entities;
using Entities.Models;
using Notifications.Wpf.Core;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Path = System.IO.Path;

namespace Presentation_Layer.UserControls
{
    /// <summary>
    /// Interaction logic for ProjectFilesUC.xaml
    /// </summary>
    public partial class ProjectFilesUC : UserControl
    {
        private Project Project;
        private File File;
        
        public ProjectFilesUC(int project_id)
        {
            InitializeComponent();
            Project = ProjectService.GetProjectById(project_id);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            dgFiles.ItemsSource = FileService.GetFilesByProjectId(Project.id);
            cmbSubproject.ItemsSource = Enum.GetValues(typeof(Enumerations.ProjectType));
               
        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            GUIManager.Close();
        }

        private void btnSelectDocument_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.Filter = "Sve datoteke (*.*)|*.*"; 

            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                string filename = dialog.FileName;
                string extension = Path.GetExtension(filename);
                byte[] fileData = System.IO.File.ReadAllBytes(filename);

                txtFileName.Text += Path.GetFileName(filename);

                File = new Entities.Models.File
                {
                    project_id = Project.id,
                    name = txtFileName.Text,
                    projectType = (int)(Enumerations.ProjectType)cmbSubproject.SelectedIndex,
                    filePath = filename,
                    fileType = extension,
                    fileData = fileData,
                    uploadDate = DateTime.Now,
                };

                txtPath.Text += Path.GetFileName(filename);
                txtPath.Visibility = Visibility.Visible;
                btnRemoveFile.Visibility = Visibility.Visible;
                
                btnSelectDocument.IsEnabled = false;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if(File != null)
            {
                try
                {
                    FileService.AddNewFile(File);
                    UCHelper.DisplayNotification("DATOTEKE PROJEKTA", "Uspješno spremljena datoteka na server", NotificationType.Success);
                    
                    ClearForm();
                    dgFiles.ItemsSource = FileService.GetFilesByProjectId(Project.id);
                }
                catch 
                {
                    UCHelper.DisplayNotification("DATOTEKE PROJEKTA", "Došlo je do pogreške prilikom zapisivanja datoteke na server", NotificationType.Error);
                }
            }
            else
            {
                UCHelper.DisplayNotification("DATOTEKE PROJEKTA", "Molimo odaberite datoteku za učitavanje", NotificationType.Warning);
            }
        }
        private async void dgFiles_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedFile = dgFiles.SelectedItem as Entities.Models.File;
            if (selectedFile != null)
            {
                try
                {
                    await FileService.DownloadFile(selectedFile);
                    UCHelper.DisplayNotification("DATOTEKE PROJEKTA", "Uspješno preuzeta datoteka", NotificationType.Success);
                }
                catch
                {
                    UCHelper.DisplayNotification("DATOTEKE PROJEKTA", "Došlo je do pogreške prilikom preuzimanja datoteke", NotificationType.Error);
                }
            }
        }

        private void btnRemoveFile_Click(object sender, RoutedEventArgs e)
        {
            File = null;
            txtPath.Text = string.Empty;
            txtPath.Visibility = Visibility.Hidden;
            btnRemoveFile.Visibility = Visibility.Hidden;
            btnSelectDocument.IsEnabled = true;
        }

        private void ClearForm()
        {
            txtFileName.Text = string.Empty;
            cmbSubproject.SelectedIndex = -1;
            txtDescription.Text = string.Empty;
            btnSelectDocument.IsEnabled = true;

            File = null;
            txtPath.Text = string.Empty;
            txtPath.Visibility = Visibility.Hidden;
            btnRemoveFile.Visibility = Visibility.Hidden;
            
            dgFiles.ItemsSource = FileService.GetFilesByProjectId(Project.id);
        }

        private async void btnDownload_Click(object sender, RoutedEventArgs e)
        {
            var selectedFile = dgFiles.SelectedItem as Entities.Models.File;
            if (selectedFile != null)
            {
                try
                {
                    await FileService.DownloadFile(selectedFile);
                    UCHelper.DisplayNotification("DATOTEKE PROJEKTA", "Uspješno preuzeta datoteka", NotificationType.Success);
                }
                catch
                {
                    UCHelper.DisplayNotification("DATOTEKE PROJEKTA", "Došlo je do pogreške prilikom preuzimanja datoteke", NotificationType.Error);
                }
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var selectedFile = dgFiles.SelectedItem as Entities.Models.File;
            if (selectedFile != null)
            {
                try
                {
                    //await FileService.DownloadFile(selectedFile);
                    UCHelper.DisplayNotification("DATOTEKE PROJEKTA", "Uspješno izbrisana datoteka", NotificationType.Success);
                }
                catch
                {
                    UCHelper.DisplayNotification("DATOTEKE PROJEKTA", "Došlo je do pogreške prilikom brisanja datoteke", NotificationType.Error);
                }
            }
        }
    }
}

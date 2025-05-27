using Bussiness_Logic_Layer.Services;
using Entities;
using Entities.Models;
using Notifications.Wpf.Core;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WPFCustomMessageBox;
using static Entities.Enumerations;
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
            dgFiles.ItemsSource = null;
            dgFiles.ItemsSource = FileService.GetFilesByProjectId(Project.id);
            cmbSubproject.ItemsSource = Enum.GetValues(typeof(Enumerations.ProjectType));
            //PROVJERITI TREE VIEW MOGUĆNOSTI

            dgFiles.Columns[0].Visibility = Visibility.Hidden;
            dgFiles.Columns[1].Visibility = Visibility.Hidden;
            dgFiles.Columns[7].Visibility = Visibility.Hidden;
            dgFiles.Columns[8].Visibility = Visibility.Hidden;

            dgFiles.Columns[2].Header = "Tip podprojekta";
            dgFiles.Columns[3].Header = "Naziv datoteke";
            dgFiles.Columns[4].Header = "Opis";
            dgFiles.Columns[5].Header = "Tip";
            dgFiles.Columns[6].Header = "Datum prijenosa";
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
                
                txtFileName.Text = Path.GetFileNameWithoutExtension(filename);

                File = new Entities.Models.File
                {
                    project_id = Project.id,
                    name = txtFileName.Text,
                    description = txtDescription.Text,
                    projectType = (int)(Enumerations.ProjectType)cmbSubproject.SelectedIndex,
                    fileType = extension,
                    filePath = filename,
                    uploadDate = DateTime.Now,
                };

                txtPath.Text += Path.GetFileName(filename);
                txtPath.Visibility = Visibility.Visible;
                btnRemoveFile.Visibility = Visibility.Visible;
                
                btnSelectDocument.IsEnabled = false;
            }
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            txtFileName.Background = null;

            if(ValidateForm() == false) return;

            //UKOLIKO DOĐE DO PROMJENE NAKON UPLOADANJA FILE-a
            File.name = txtFileName.Text;
            File.description = txtDescription.Text;
            File.projectType = (int)(Enumerations.ProjectType)cmbSubproject.SelectedIndex;

            var fileDb = FileService.GetFilesByNameAndType(File.name, File.fileType);

            try
            {
                if (fileDb != null) 
                {
                    var result = CustomMessageBox.ShowYesNoCancel("Već postoji datoteka sa ovim nazivom i tipom. Želite li je prepisati ili promijeniti ime?", "Postojeća datoteka", "Promjeni ime", "Prepiši staru datoteku", "Odustani");
                    if (result == MessageBoxResult.Yes) // PROMJENA IMENA
                    {
                        txtFileName.Background = Brushes.DarkRed;
                        txtFileName.Focus(); 
                        txtFileName.ScrollToEnd();
                        return;
                    }
                    else if (result == MessageBoxResult.No) // PREPISIVANJE DATOTEKE
                    {
                        File.uploadDate = DateTime.Now;
                        File.id = fileDb.id;
                        File.projectType = (int)(Enumerations.ProjectType)cmbSubproject.SelectedIndex;

                        await FileService.AddFile(File, true); // TRUE za postojanje zapisa u bazi
                        UCHelper.DisplayNotification("DATOTEKE PROJEKTA", "Uspješno prepisana datoteka na serveru", NotificationType.Success);
                    }
                    else
                    {
                        ClearForm();
                    }
                }
                else
                {
                    await FileService.AddFile(File, false); // FALSE za nepostojanje zapisa u bazi i dodavanje novog
                    UCHelper.DisplayNotification("DATOTEKE PROJEKTA", "Uspješno spremljena datoteka na server", NotificationType.Success);
                }                    
                ClearForm();
                UserControl_Loaded(sender, e);
            }
            catch 
            {
                UCHelper.DisplayNotification("DATOTEKE PROJEKTA", "Došlo je do pogreške prilikom zapisivanja datoteke na server", NotificationType.Error);
            }
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

                    Process.Start("explorer.exe", KnownFolders.GetPath(KnownFolder.Downloads));
                }
                catch
                {
                    UCHelper.DisplayNotification("DATOTEKE PROJEKTA", "Došlo je do pogreške prilikom preuzimanja datoteke", NotificationType.Error);
                }
            }
            else
            {
                UCHelper.DisplayNotification("DATOTEKE PROJEKTA", "Molimo odaberite datoteku za preuzimanje", NotificationType.Warning);
            }
        }
        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var selectedFile = dgFiles.SelectedItem as Entities.Models.File;
            if (selectedFile != null)
            {
                try
                {
                    await FileService.DeleteFile(selectedFile);
                    UCHelper.DisplayNotification("DATOTEKE PROJEKTA", "Uspješno izbrisana datoteka", NotificationType.Success);
                    
                    ClearForm();
                    UserControl_Loaded(sender, e);
                }
                catch
                {
                    UCHelper.DisplayNotification("DATOTEKE PROJEKTA", "Došlo je do pogreške prilikom brisanja datoteke", NotificationType.Error);
                }
            }
            else
            {
                UCHelper.DisplayNotification("DATOTEKE PROJEKTA", "Molimo odaberite datoteku za brisanje", NotificationType.Warning);
            }
        }

        private void btnRemoveFile_Click(object sender, RoutedEventArgs e)
        {
            File = null;
            txtFileName.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtPath.Text = string.Empty;
            txtPath.Visibility = Visibility.Hidden;
            btnRemoveFile.Visibility = Visibility.Hidden;
            btnSelectDocument.IsEnabled = true;
            txtFileName.Background = null;
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
        }
        

        private bool ValidateForm()
        {
            if(txtFileName.Text == string.Empty || cmbSubproject.SelectedIndex == -1)
            {
                UCHelper.DisplayNotification("DATOTEKE PROJEKTA", "Molimo popunite obavezna polja", NotificationType.Warning);
                return false;
            }
            else if(File == null)
            {
                UCHelper.DisplayNotification("DATOTEKE PROJEKTA", "Molimo odaberite datoteku za učitavanje", NotificationType.Warning);
                return false;
            }
            else return true;
        }
    }
}

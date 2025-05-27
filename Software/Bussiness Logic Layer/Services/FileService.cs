using Azure.Storage;
using Azure.Storage.Files.Shares;
using Azure.Storage.Files.Shares.Models;
using Data_Access_Layer.Repositories;
using Entities.Models;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Entities.Enumerations;
using File = Entities.Models.File;
using Task = System.Threading.Tasks.Task;

namespace Bussiness_Logic_Layer.Services
{
    public static class FileService
    {
        private static string connectionString = "DefaultEndpointsProtocol=https;AccountName=portalfiles1;AccountKey=yKjraClCZvUMPj2MVMlTldfZVT2by1VBEiMCcdAQ3qUcwwRokjDHNkuy0SPVilikO6zIaLKylTjn+AStoAO6+g==;EndpointSuffix=core.windows.net";
        public async static Task AddFile(File file, bool fileExists)
        { 
            string shareName = "portalfiles";

            MemoryStream stream = new MemoryStream(file.fileData);
            ShareFileClient fileClient = new ShareFileClient(connectionString, shareName, (file.name + file.fileType));

            ShareFileUploadOptions uploadOptions = new ShareFileUploadOptions
            {
                TransferOptions = new StorageTransferOptions
                {
                    // Specify the initial and maximum transfer size for the upload
                    InitialTransferSize = 1024 * 1024 * 4,
                    MaximumTransferSize = 1024 * 1024 * 4
                }
            };
            await fileClient.CreateAsync(stream.Length);
            await fileClient.UploadAsync(stream, uploadOptions);

            using (var repo = new FileRepository())
            {
                if(fileExists) repo.Update(file);
                else repo.Add(file);
            }
        }
        public async static Task DownloadFile(File file)
        {
            string shareName = "portalfiles";
            ShareFileClient fileClient = new ShareFileClient(connectionString, shareName, (file.name + file.fileType));
            ShareFileDownloadInfo download = await fileClient.DownloadAsync();
            string downloadsPath = KnownFolders.GetPath(KnownFolder.Downloads);

            using (var fileStream = System.IO.File.OpenWrite(downloadsPath + Path.DirectorySeparatorChar + (file.name + file.fileType)))
            {
                await download.Content.CopyToAsync(fileStream);
            }
        }
        public async static Task DeleteFile(File file)
        {
            string shareName = "portalfiles";
            ShareFileClient fileClient = new ShareFileClient(connectionString, shareName, (file.name + file.fileType));
            ShareFileDownloadInfo download = await fileClient.DownloadAsync();

            //BRISANJE SA SERVERA
            await fileClient.DeleteIfExistsAsync(default, default);

            //BRISANJE IZ BAZE
            using (var repo = new FileRepository())
            {
                repo.Remove(file);
            }
        }
        public static List<File> GetFilesByProjectId(int id)
        {
            using (var repo = new FileRepository())
            {
                return repo.GetFilesByProjectId(id).ToList();
            }
        }

        public static File GetFilesByNameAndType(string name, string type)
        {
            using (var repo = new FileRepository())
            {
                return repo.GetFilesByNameAndType(name, type).FirstOrDefault();
            }
        }
    }
}

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
        public async static void AddNewFile(File file)
        {  
            string shareName = "portalfiles";

            MemoryStream stream = new MemoryStream(file.fileData);
            ShareFileClient fileClient = new ShareFileClient(connectionString, shareName, file.name);

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
                repo.Add(file);
            }
        }

        public async static Task DownloadFile(File file)
        {
            string shareName = "portalfiles";
            ShareFileClient fileClient = new ShareFileClient(connectionString, shareName, file.name);
            ShareFileDownloadInfo download = await fileClient.DownloadAsync();
            string downloadsPath = KnownFolders.GetPath(KnownFolder.Downloads);

            using (var fileStream = System.IO.File.OpenWrite(downloadsPath + Path.DirectorySeparatorChar + file.name))
            {
                await download.Content.CopyToAsync(fileStream);
            }
        }


        public static List<File> GetFilesByProjectId(int id)
        {
            using (var repo = new FileRepository())
            {
                return repo.GetFilesByProjectId(id).ToList();
            }
        }
    }
}

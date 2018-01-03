using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using MimeTypes;
using File = Google.Apis.Drive.v3.Data.File;

namespace oleksandrbachkai.Adapters
{
    public class GoogleDriveAdapter
    {
        private readonly DriveService _service;

        public GoogleDriveAdapter()
        {
            UserCredential credential;

            using (var stream = new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = AppDomain.CurrentDomain.BaseDirectory;
                credPath = Path.Combine(credPath, "drive-dotnet-quickstart.json");
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    new[]
                    {
                        DriveService.Scope.Drive
                    },
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }

            _service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
            });
        }

        public IEnumerable<File> GetAllFiles()
        {
            
            var listRequest = _service.Files.List();

            listRequest.Fields = "nextPageToken, files";

            return  listRequest.Execute().Files;            
        }

        public static string GetUrlByFileId(string fileId)
        {
            return $"https://docs.google.com/document/d/{fileId}/edit?usp=sharing";            
        }

        public string UploadFile(string filename)
        {            
            const string folderId = "1kpz_Tw2iJzq4iIKLWfv0g-fx3zUNPE5m";

            var fileMetadata = new File()
            {
                Name = "file",
                Parents = new []
                {
                    folderId
                }                
            };

            using (var stream = new FileStream(filename, FileMode.Open))
            {
                var request = _service.Files.Create(fileMetadata, stream, MimeTypeMap.GetMimeType(Path.GetExtension(filename)));                
                request.Fields = "id";
                request.Upload();

                return request.ResponseBody.Id;
            }
        }
    }
}

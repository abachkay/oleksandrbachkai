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
    public class GoogleDriveAdapter: IDisposable
    {
        private readonly DriveService _service;

        public GoogleDriveAdapter()
        {
            UserCredential credential;
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            using (var stream = new FileStream(Path.Combine(basePath, "client_secret.json"), FileMode.Open,
                FileAccess.Read))
            {

                var credPath = Path.Combine(basePath, "drive-dotnet-quickstart.json");
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
            return $"https://docs.google.com/file/d/{fileId}/view?usp=sharing";            
        }

        public string UploadFile(string filename, byte[] bytes)
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

            using (var stream = new MemoryStream(bytes))
            {
                var request = _service.Files.Create(fileMetadata, stream, MimeTypeMap.GetMimeType(Path.GetExtension(filename)));                
                request.Fields = "id";
                request.Upload();

                return request.ResponseBody.Id;
            }
        }

        public void DeleteFile(string fileId)
        {
            try
            {
                _service.Files.Delete(fileId).Execute();
            }
            catch
            {
            }
        }

        public void Dispose()
        {
            _service?.Dispose();
        }
    }
}

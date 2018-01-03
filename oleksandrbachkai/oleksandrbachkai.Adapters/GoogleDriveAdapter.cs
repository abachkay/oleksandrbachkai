using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
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
                        DriveService.Scope.DriveReadonly
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
    }
}

using System;
using Microsoft.AspNet.Identity;
using oleksandrbachkai.Services;

namespace oleksandrbachkai.Adapters
{
    public class Program
    {
        private static void Main(string[] args)
        {
            new EmailService().SendAsync(new IdentityMessage()
            {
                Body = "aaa",
                Destination = "abachkayspare@gmail.com",
                Subject = "aaa"
            }).Wait();

            //Console.WriteLine(GoogleDriveAdapter.GetUrlByFileId("1MkjG02EKWJViu_IQidEnGGHZCs7pXWxIMg0Fdslehms"));

            //var adapter = new GoogleDriveAdapter();

            //adapter.UploadFile("USGA Git configuration.pdf", System.IO.File.ReadAllBytes("C:\\Users\\Oleksandr_Bachkai\\Desktop\\Training\\Sources\\USGA Git configuration.pdf"));

            //foreach (var file in new GoogleDriveAdapter().GetAllFiles())
            //{
            //    Console.WriteLine(file.WebViewLink);
            //}            
        }
    }
}
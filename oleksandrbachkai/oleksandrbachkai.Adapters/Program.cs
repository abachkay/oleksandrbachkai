using System;

namespace oleksandrbachkai.Adapters
{
    public class Program
    {
        private static void Main(string[] args)
        {
            //Console.WriteLine(GoogleDriveAdapter.GetUrlByFileId("1MkjG02EKWJViu_IQidEnGGHZCs7pXWxIMg0Fdslehms"));

            foreach (var file in new GoogleDriveAdapter().GetAllFiles())
            {
                Console.WriteLine(file.WebViewLink);
            }

            Console.Read();
        }
    }
}
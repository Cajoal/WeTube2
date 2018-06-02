using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Threading.Tasks;

namespace WeTube.Controllers
{
    public class BlobsController : Controller
    {
        CloudStorageAccount storageAccount;
        CloudBlobContainer container;

        public BlobsController()
        {
            System.Console.WriteLine("testing constructor");
            initAsync();
            
    
        }

        private async Task initAsync()
        {
            System.Console.WriteLine("testing init");

            storageAccount = new CloudStorageAccount( new Microsoft.WindowsAzure.Storage.Auth.StorageCredentials("wetubemediastore", "kZ6bg7vY0HqC9lf4zcc46evyjD7nm9LUXS3iQjolVEKGFYVNzzMlUoICyqqWUc31hQ0coSMpbawRJslMdB0b / A =="), true);
            // Create a blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Get a reference to a container named "mycontainer."
            container = blobClient.GetContainerReference("container1");

            // Get a reference to a blob named "myblob".
            CloudBlockBlob blockBlob = container.GetBlockBlobReference("myblob");

            // Create or overwrite the "myblob" blob with the contents of a local file
            // named "myfile".
            using (var fileStream = System.IO.File.OpenRead(@"E:\testVideos\SampleAudio_0.4mb.mp3"))
            {

                await blockBlob.UploadFromStreamAsync(fileStream);
                System.Console.WriteLine("Finished upload");
            }

            BlobContinuationToken token = null;
            do
            {
                System.Console.WriteLine("test list");
                BlobResultSegment resultSegment = await container.ListBlobsSegmentedAsync(token);
                token = resultSegment.ContinuationToken;

                foreach (IListBlobItem item in resultSegment.Results)
                {
                    if (item.GetType() == typeof(CloudBlockBlob))
                    {
                        CloudBlockBlob blob = (CloudBlockBlob)item;
                        System.Console.WriteLine("Block blob of length {0}: {1}", blob.Properties.Length, blob.Uri);
                    }

                    else if (item.GetType() == typeof(CloudPageBlob))
                    {
                        CloudPageBlob pageBlob = (CloudPageBlob)item;

                        System.Console.WriteLine("Page blob of length {0}: {1}", pageBlob.Properties.Length, pageBlob.Uri);
                    }

                    else if (item.GetType() == typeof(CloudBlobDirectory))
                    {
                        CloudBlobDirectory directory = (CloudBlobDirectory)item;

                        System.Console.WriteLine("Directory: {0}", directory.Uri);
                    }
                }
            } while (token != null);
        }
    }
}
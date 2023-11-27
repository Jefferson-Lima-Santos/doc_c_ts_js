using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using System;
using Azure.Storage.Blobs.Specialized;
using System.Configuration;
using Microsoft.Rest;

namespace FCNuvem.SEB.PortalBI.Storage
{
    public class AzureStorage
    {
        public CloudStorageAccount StorageAccount { get; set; }
        public CloudBlobClient BlobClient { get; set; }
        public CloudBlobContainer BlobUrlContainer { get; set; }
        public BlobServiceClient serviceClient { get; set; }
        private static string connectionString = ConfigurationManager.AppSettings["SasUri"];

        public AzureStorage()
        {
            StorageAccount = CloudStorageAccount.Parse(connectionString);
            BlobClient = StorageAccount.CreateCloudBlobClient();
            serviceClient = new BlobServiceClient(connectionString);
        }

        public async Task<string> UploadFile(string pathFileName, string container, Stream stream)
        {
            if (!string.IsNullOrWhiteSpace(container))
            {
                BlobUrlContainer = BlobClient.GetContainerReference(container);
                var pathBlob = pathFileName.Substring(container.Length + 1, (pathFileName.Length - container.Length) - 1);
                CloudBlockBlob blob = BlobUrlContainer.GetBlockBlobReference(pathBlob);

                var te = await blob.ExistsAsync();
                if (!te)
                {
                    await blob.UploadFromStreamAsync(stream);
                    return blob.SnapshotQualifiedStorageUri.PrimaryUri.ToString();
                }
            }
            return string.Empty;
        }
        public async Task<bool> DeleteFile(string pathFileName, string container)
        {
            if (!string.IsNullOrWhiteSpace(container))
            {
                BlobUrlContainer = BlobClient.GetContainerReference(container);
                var pathBlob = pathFileName.Substring(container.Length + 1, (pathFileName.Length - container.Length) - 1);
                CloudBlockBlob blob = BlobUrlContainer.GetBlockBlobReference(pathBlob);

                var blobExists = await blob.ExistsAsync();
                if (blobExists)
                {
                    await blob.DeleteAsync();
                    return true;
                }
            }
            return false;
        }
        public async Task<Uri> CreateServiceSASBlob(
            string containerName,
            string imgName,
            string storedPolicyName = null)
        {
            BlobContainerClient containerClient = serviceClient.GetBlobContainerClient(containerName);
            BlobClient blobClient = containerClient.GetBlobClient(imgName);
            if (blobClient.CanGenerateSasUri)
            {
                BlobSasBuilder sasBuilder = new BlobSasBuilder()
                {
                    BlobContainerName = blobClient.GetParentBlobContainerClient().Name,
                    BlobName = blobClient.Name,
                    Resource = "b"
                };
                if (storedPolicyName == null)
                {
                    sasBuilder.ExpiresOn = DateTimeOffset.UtcNow.AddDays(1);
                    sasBuilder.SetPermissions(BlobContainerSasPermissions.All);
                }
                else
                {
                    sasBuilder.Identifier = storedPolicyName;
                }
                Uri sasURI = blobClient.GenerateSasUri(sasBuilder);

                return sasURI;
            }
            else
            {
                return null;
            }
        }
    }
}
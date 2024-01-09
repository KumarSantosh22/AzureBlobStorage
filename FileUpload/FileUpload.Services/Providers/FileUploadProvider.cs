using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using FileUpload.Concerns.Config;
using FileUpload.Concerns.Core;
using FileUpload.Services.Contracts;

namespace FileUpload.Services.Providers
{
    public class FileUploadProvider: IFileUploadContract
    {
        private readonly BlobContainerClient blobContainerClient;
        public FileUploadProvider(AzureBlobConfig blobConfig)
        {
            blobContainerClient = new BlobContainerClient(blobConfig.ConnectionUrl, blobConfig.Container);
        }

        public string UploadFile(BlobFile file)
        {
            BlobClient blob = blobContainerClient.GetBlobClient(file.Name);
            BlobContainerInfo response = blobContainerClient.CreateIfNotExists();
            //blobContainerClient.SetAccessPolicy(PublicAccessType.Blob);

            blob.Upload(GetStream(file.Content), new BlobHttpHeaders { ContentType = file.ContentType });
            return blob.Uri.ToString();
        }

        public List<string> UploadFiles(List<BlobFile> files)
        {
            List<string> result = new List<string>();
            files.ForEach(file =>
            {
                result.Add(UploadFile(file));
            });
            return result;
        }

        public async Task<string> UploadFileAsync(BlobFile file)
        {
            BlobClient blob = blobContainerClient.GetBlobClient(file.Name);
            Azure.Response<BlobContainerInfo> response = await blobContainerClient.CreateIfNotExistsAsync();
            //await blobContainerClient.SetAccessPolicyAsync(PublicAccessType.Blob);
            
            await blob.UploadAsync(GetStream(file.Content), new BlobHttpHeaders { ContentType = file.ContentType });
            return blob.Uri.ToString();
        }        

        public async Task<List<string>> UploadFilesAsync(List<BlobFile> files)
        {
            List<string> result = new List<string>();
            files.ForEach(file =>
            {
                result.Add(UploadFileAsync(file).GetAwaiter().GetResult());
            });
            return result;
        }

        public async Task<bool> DeleteFileAsync(string name)
        {
            BlobClient blob = blobContainerClient.GetBlobClient(name);
            Azure.Response<bool> response = await blob.DeleteIfExistsAsync();
            return response.Value;
        }

        public async Task<Stream> GetBlobAsync(string name)
        {
            BlobClient client = blobContainerClient.GetBlobClient(name);
            Azure.Response<BlobDownloadResult> response = await client.DownloadContentAsync();
            BlobDownloadResult value = response.Value;
            return value.Content.ToStream();
        }

        private Stream GetStream(string content)
        {
            byte[] b = Convert.FromBase64String(content);
            return new MemoryStream(b);
        }
    }
}

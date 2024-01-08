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
            BlobClient blob = blobContainerClient.GetBlobClient(file.FileName);
            blob.Upload(file.FileStream, new BlobHttpHeaders { ContentType = file.ContentType });
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
            BlobClient blob = blobContainerClient.GetBlobClient(file.FileName);
            await blob.UploadAsync(file.FileStream, new BlobHttpHeaders { ContentType = file.ContentType });
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
    }
}

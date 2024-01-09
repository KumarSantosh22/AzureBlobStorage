using FileUpload.Concerns.Core;

namespace FileUpload.Services.Contracts
{
    public interface IFileUploadContract
    {
        string UploadFile(BlobFile file);

        List<string> UploadFiles(List<BlobFile> files);

        Task<string> UploadFileAsync(BlobFile file);

        Task<List<string>> UploadFilesAsync(List<BlobFile> files);

        Task<bool> DeleteFileAsync(string name);

        Task<Stream> GetBlobAsync(string name);
    }
}

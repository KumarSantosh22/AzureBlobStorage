namespace FileUpload.Concerns.Core
{
    public class BlobFile
    {
        public Stream FileStream { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
    }
}

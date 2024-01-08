namespace FileUpload.Concerns.Core
{
    public class BlobFile
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public string ContentType { get; set; }
        public long Length {  get; set; }   
    }
}

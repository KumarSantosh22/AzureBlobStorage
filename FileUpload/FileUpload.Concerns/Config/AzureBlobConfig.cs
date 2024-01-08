

namespace FileUpload.Concerns.Config
{
    public class AzureBlobConfig
    {
        public string ConnectionUrl { get; set; }
        public string AccountName { get; set; }
        public string AccountKey { get; set; }
        public string EndpointSuffix { get; set; }
        public string Container { get; set; }        
    }
}

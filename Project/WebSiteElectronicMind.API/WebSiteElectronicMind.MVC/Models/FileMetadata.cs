namespace WebSiteElectronicMind.MVC.Models
{
    public class FileMetadata
    {
        public string? OriginalFileName { get; set; }
        public string? UniqueFileName { get; set; }
        public string? FilePath { get; set; }
        public DateTime UploadTime { get; set; }
    }
}

using CSharpFunctionalExtensions;
using System.Text.Json;
using WebSiteElectronicMind.Core.Models;

namespace WebSiteElectronicMind.Application.Services
{
    public class FileMetadataService
    {
        private readonly string _metadataFilePath =
            Path.Combine(Directory.GetCurrentDirectory(), "Files/Metadata/metadata.json");

        public Result CreateAndSaveMetadata(string originalFileName, string uniqueFileName, string filePath)
        {
            var metadataResult = FileMetadata.Create(originalFileName, uniqueFileName, filePath);
            if (metadataResult.IsFailure)
            {
                return Result.Failure(metadataResult.Error);
            }

            return SaveMetadata(metadataResult.Value);
        }


        public Result SaveMetadata(FileMetadata metadata)
        {
            try
            {
                var metadataList = LoadMetadataList();
                metadataList.Add(metadata);
                var json = JsonSerializer.Serialize(metadataList, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_metadataFilePath, json);
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure($"Error saving metadata: {ex.Message}");
            }
        }

        public List<FileMetadata> LoadMetadataList()
        {
            if (!File.Exists(_metadataFilePath))
            {
                return new List<FileMetadata>();
            }

            var json = File.ReadAllText(_metadataFilePath);
            if (string.IsNullOrWhiteSpace(json))
            {
                return new List<FileMetadata>();
            }

            return JsonSerializer.Deserialize<List<FileMetadata>>(json) ?? new List<FileMetadata>();
        }

    }
}

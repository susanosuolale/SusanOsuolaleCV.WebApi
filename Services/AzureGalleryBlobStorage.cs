using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

//handles communication with the azure blob storage
namespace SusanOsuolaleCV.Api.Services
{
    public class AzureGalleryBlobStorage : IGalleryBlobStorage
    {
        private readonly BlobContainerClient _container;

        public AzureGalleryBlobStorage(BlobContainerClient container)
        {
            _container = container;
        }

        public async Task<string> UploadAsync(IFormFile file, string contentType)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("Invalid file");

            var ext = Path.GetExtension(file.FileName);
            var blobName = $"files/{System.Guid.NewGuid()}{ext}";
            //creates c# object configured with this filename for uploads, 
            //modification, deletion e.t.c
            var blobClient = _container.GetBlobClient(blobName);

            using (var stream = file.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = contentType });
            }
            return blobName;
        }

        public async Task<(Stream Content, string ContentType)> DownloadAsync(string blobName)
        {
            var blobClient = _container.GetBlobClient(blobName);
            if (!await blobClient.ExistsAsync())
                throw new FileNotFoundException(blobName);
            // DownloadAsync returns a Response<BlobDownloadInfo>; extract the Value to access content
            var response = await blobClient.DownloadAsync();
            var blobInfo = response.Value;
            return (blobInfo.Content, blobInfo.ContentType);
        }

        public async Task<bool> ExistsAsync(string blobName)
        {
            var blobClient = _container.GetBlobClient(blobName);
            return await blobClient.ExistsAsync();
        }

        public async Task DeleteAsync(string blobName)
        {
            var blobClient = _container.GetBlobClient(blobName);
            await blobClient.DeleteIfExistsAsync();
        }
    }
}

using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace SusanOsuolaleCV.Api.Services
{
    public interface IGalleryBlobStorage
    {
        Task<string> UploadAsync(IFormFile file, string contentType);
        Task<(Stream Content, string ContentType)> DownloadAsync(string blobName);
        Task<bool> ExistsAsync(string blobName);
        Task DeleteAsync(string blobName);
    }
}

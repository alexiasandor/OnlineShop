using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Laroa.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laroa.Application
{
    public class ImageStorageService : IImageStorageService
    {
        private readonly BlobServiceClient _blobClient;

        public ImageStorageService(BlobServiceClient blobClient)
        {
            _blobClient = blobClient;
        }
        public async Task DeleteImage(string name, string containerName)
        {
            var containerClient = _blobClient.GetBlobContainerClient(containerName);
            var blobClient = containerClient.GetBlobClient(name);

            await blobClient.DeleteIfExistsAsync();
        }

        public async Task<string> UploadImage(string name, IFormFile file, string containerName)
        {
            var containerClient = _blobClient.GetBlobContainerClient(containerName);
            var blobClient = containerClient.GetBlobClient(name);

            var httpHeaders = new BlobHttpHeaders()
            {
                ContentType = file.ContentType
            };

            await blobClient.UploadAsync(file.OpenReadStream(), httpHeaders);
            var blobUrl = blobClient.Uri.AbsoluteUri;

            return blobUrl;
        }
    }
}

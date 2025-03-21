using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Handmade.Application.Contracts;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handmade.Infrastructure
{
    public class CloudinaryRepository : ICloudinaryRepository
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryRepository(IConfiguration configuration)
        {
            var account = new Account(
                configuration["Cloudinary:CloudName"],
                configuration["Cloudinary:ApiKey"],
                configuration["Cloudinary:ApiSecret"]
            );
            _cloudinary = new Cloudinary(account);
        }

        public async Task<string> UploadImageAsync(Stream fileStream, string fileName)
        {
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(fileName, fileStream),
                Folder = "account_images",
                Transformation = new Transformation().Width(500).Height(500).Crop("fill")
            };

            ImageUploadResult uploadResult = await _cloudinary.UploadAsync(uploadParams);
            Console.WriteLine(uploadResult.SecureUri);
            Console.WriteLine(uploadResult.SecureUrl);
            Console.WriteLine(uploadResult.SecureUrl.AbsoluteUri);
            return uploadResult.SecureUrl.AbsoluteUri;
        }

        public async Task DeleteImageAsync(string imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl)) return;

            var uriSegments = new Uri(imageUrl).Segments;
            var fileName = uriSegments[^1];
            var publicId = Path.GetFileNameWithoutExtension(fileName);

            var deleteParams = new DeletionParams(publicId);
            await _cloudinary.DestroyAsync(deleteParams);
        }
    }
}

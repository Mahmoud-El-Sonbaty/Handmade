using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handmade.Application.Contracts
{
    public interface ICloudinaryRepository
    {
        Task<string> UploadImageAsync(Stream fileStream, string fileName);
        Task DeleteImageAsync(string imageUrl);
    }
}

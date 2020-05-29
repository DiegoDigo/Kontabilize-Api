using System;
using System.IO;
using System.Threading.Tasks;

namespace Kontabilize.Domain.UserContext.Services
{
    public interface IUploadService
    {
        public Task<string> UploadImageProfile(Guid userId, Stream image);
    }
}
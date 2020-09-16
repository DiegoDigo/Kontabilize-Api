using System;
using System.IO;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Kontabilize.Domain.UserContext.Services;

namespace Kontabilize.Infra.Services
{
    public class UploadService : IUploadService
    {
        public async Task<string> UploadImageProfile(Guid userId, Stream image)
        {
            var imageParams = new ImageUploadParams()
            {
                File = new FileDescription(userId.ToString(), image),
                PublicId = $"kontabilize/avatar/{userId.ToString()}",
                Overwrite = true,
                Transformation = new Transformation().Width(200).Height(200).Crop("thumb").Gravity("face")
            };
            var result = await Init().UploadAsync(imageParams);
            return result.SecureUrl.AbsoluteUri;
        }

        private static Cloudinary Init()
        {
            var myAccount = new Account
                {ApiKey = "523226973273465", ApiSecret = "ikvZJlMbXCaNzyxlLrhBjp_0yWQ", Cloud = "dzcvxohec"};
            return new Cloudinary(myAccount);
        }
    }

 
}
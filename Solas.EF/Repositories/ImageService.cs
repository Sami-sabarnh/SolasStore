using Microsoft.AspNetCore.Http;
using Solas.BL.IRepositories;

namespace Solas.EF.Repositories
{
    public class ImageService : IImageService
    {
        public string AddImage(IFormFile image)
        {
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ProductImages", fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                image.CopyTo(stream);
            }


            return fileName;
        }

        public IEnumerable<string> AddImages(List<IFormFile> images)
        {
            List<string> files = new List<string>();

            foreach (var image in images)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ProductImages", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    image.CopyTo(stream);
                }

                files.Add(fileName);
            }
            return files;
        }
    }
}

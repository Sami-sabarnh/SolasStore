using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;


namespace Solas.BL.IRepositories
{
    public interface IImageService 
    {
        //  private IWebHostEnvironment webHost;
        IEnumerable<string> AddImages(List<IFormFile> images);
        string AddImage(IFormFile image);


    }
}

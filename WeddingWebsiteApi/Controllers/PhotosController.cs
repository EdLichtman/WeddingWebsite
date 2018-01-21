using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WeddingWebsiteApi.Controllers
{
    [Route("api/[controller]/[action]")]
    public class PhotosController : Controller
    {
        private readonly string _clientApplicationPhotoLocationRoot;
        private readonly string _clientApplicationLocationRoot;

        public PhotosController(IConfiguration configuration)
        {
            _clientApplicationLocationRoot = configuration.GetSection("ClientApplicationLocationRoot").Value;

            _clientApplicationPhotoLocationRoot = _clientApplicationLocationRoot + "\\assets\\photos";
        }

        // GET api/values
        [HttpGet]
        public IList<string> Iris()
        {
            return GetPictureUrlsByFolder("iris");   
        }

        [HttpGet]
        public IList<string> Engagement()
        {
            return GetPictureUrlsByFolder("engagement");
        }

        private IList<string> GetPictureUrlsByFolder(string folderName)
        {
            var location = _clientApplicationPhotoLocationRoot + "\\" + folderName;
            var jpgs = Directory.GetFiles(location, "*.jpg", SearchOption.TopDirectoryOnly).ToList();
            var jpegs = Directory.GetFiles(location, "*.jpeg", SearchOption.TopDirectoryOnly).ToList();
            var pngs = Directory.GetFiles(location, "*.png", SearchOption.TopDirectoryOnly).ToList();

            return jpgs.Concat(jpegs).Concat(pngs)
                .Select(image => image.Replace(_clientApplicationLocationRoot, "")).ToList();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WeddingWebsiteApi.Controllers.Photos;

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
        [IsApproved]
        public IList<string> Iris()
        {
            return GetPictureUrlsByFolder("iris");   
        }

        [HttpGet]
        [IsApproved]
        public IList<string> Engagement()
        {
            return GetPictureUrlsByFolder("engagement");
        }

        [HttpGet]
        public IEnumerable<string> GetApprovedRoutes()
        {
            var methods = typeof(PhotosController).GetMethods();
            var approvedMethods = methods
                .Where(m =>
                        m.GetCustomAttributes(typeof(IsApprovedAttribute), false).Length > 0).ToList();
                        
            foreach (var method in approvedMethods)
            {
                yield return method.Name.ToLower();
            };
        }

        private IList<string> GetPictureUrlsByFolder(string folderName)
        {
            var location = _clientApplicationPhotoLocationRoot + "\\" + folderName;
            var jpgs = Directory.GetFiles(location, "*.jpg", SearchOption.AllDirectories).ToList();
            var jpegs = Directory.GetFiles(location, "*.jpeg", SearchOption.AllDirectories).ToList();
            var pngs = Directory.GetFiles(location, "*.png", SearchOption.AllDirectories).ToList();

            return jpgs.Concat(jpegs).Concat(pngs)
                .Select(image => image.Replace(_clientApplicationLocationRoot, "")).ToList();
        }
    }
}

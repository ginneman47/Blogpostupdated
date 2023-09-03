using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly Account account;
        public ImagesController(IConfiguration configuration)
        {
            Configuration = configuration;
            account = new Account(configuration.GetSection("Cloudinary")["CloudName"], configuration.GetSection("Cloudinary")["ApiKey"], configuration.GetSection("Cloudinary")["ApiSecret"]);
        }

        public IConfiguration Configuration { get; }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok("This get image api call");
        }
        [HttpPost]
        
        public IActionResult Upload(IFormFile file)
        {
            //call a repository
            var client = new Cloudinary(account);
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                DisplayName = file.FileName
            };
            var uploadResult = client.Upload(uploadParams);
            if(uploadResult != null && uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var url =  uploadResult.SecureUri.ToString();
                return new JsonResult(new { link = url });
            }
            
            return new JsonResult(new  {link = "null" });
            
        }
    }
}

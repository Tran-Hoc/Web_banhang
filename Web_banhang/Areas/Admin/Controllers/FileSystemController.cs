using elFinder.NetCore;
using elFinder.NetCore.Drivers.FileSystem;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Web_banhang.Config;

namespace Web_banhang.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("el-finder-file-system")]
    public class FileSystemController : Controller
    {
        private readonly IWebHostEnvironment environment;
        private readonly FileSystemConfig fileConfig;

        public FileSystemController(IWebHostEnvironment environment, IOptions<FileSystemConfig> fileSystemConfig)
        {
            this.environment = environment;
            fileConfig = fileSystemConfig.Value;
        }
        public IActionResult Index()
        {
            return View();
        }
        [Route("connector")]
        public async Task<IActionResult> Connector()
        {
            var connector = Getconector();
            return await connector.ProcessAsync(Request);
             
        }
        [Route("thumb/{hash}")]
        public async Task<IActionResult> Thumbs(string hash)
        {
            var connector = Getconector();
            return await connector.GetThumbnailAsync(HttpContext.Request, HttpContext.Response, hash);
        }
        private Connector Getconector()
        {
            string pathRoot = fileConfig.RootFolder;
            var driver = new FileSystemDriver();

            string absolutePath = UriHelper.BuildAbsolute(Request.Scheme, Request.Host);
            var uri = new Uri(absolutePath);    

            string rootDirectory = Path.Combine(environment.WebRootPath, pathRoot);
            string url = $"{uri.Scheme}://{uri.Authority}/{pathRoot}/";
            string urlthumb = $"{uri.Scheme}://{uri.Authority}/el-finder-file-system/thumb/";

            var root = new RootVolume(rootDirectory, url, urlthumb)
            {
                IsReadOnly = false,
                IsLocked= false,
                Alias = "files",
                ThumbnailSize = 100
            };
            driver.AddRoot(root);
            return new Connector(driver)
            {
                MimeDetect = MimeDetectOption.Internal
            };
        }
    }
}

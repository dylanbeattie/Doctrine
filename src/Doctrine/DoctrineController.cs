using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Doctrine {
    public class DoctrineController : Controller {
        private readonly IHostingEnvironment host;

        public DoctrineController(IHostingEnvironment host) {
            this.host = host;
        }
        public ActionResult Index(string path) {
            var htmlSourcePath = $"Pages/{path}.html";
            var filePath = Path.Combine(host.WebRootPath, htmlSourcePath);
            var html = HtmlPage.FromPath(filePath, htmlSourcePath);
            return View("Page", html);
        }
    }
}
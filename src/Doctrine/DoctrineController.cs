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
            var filePath = Path.Combine(host.WebRootPath, "Pages", path + ".html");
            var html = HtmlPage.FromPath(filePath);
            return View("Page", html);
        }
    }
}
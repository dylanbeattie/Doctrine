using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Doctrine {
    public class DoctrineController : Controller {
        public ActionResult Index(string path) {
            var filePath = Server.MapPath("~/Pages/" + path + ".html");
            var html = HtmlPage.FromPath(filePath);
            return View("Page", html);
        }
    }
}

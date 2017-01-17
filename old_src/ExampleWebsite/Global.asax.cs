using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Doctrine;

namespace ExampleWebsite {
    public class MvcApplication : System.Web.HttpApplication {
        protected void Application_Start() {
            var path = Server.MapPath("~/Pages");
            DoctrineSite.Init(path, RouteTable.Routes);
        }
    }
}

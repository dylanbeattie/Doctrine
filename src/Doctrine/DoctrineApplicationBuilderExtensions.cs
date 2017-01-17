using Microsoft.AspNetCore.Builder;

namespace Doctrine {
    public static class DoctrineApplicationBuilderExtensions {
        public static void UseDoctrine(this IApplicationBuilder app, string documentPath) {
            app.UseMvc(routes => routes.MapRoute(
                "doctrine", "{*path}",
                defaults: new { controller = "Doctrine", action = "Index", path = "index" }));
            DoctrineSite.Init(documentPath);
        }
    }
}
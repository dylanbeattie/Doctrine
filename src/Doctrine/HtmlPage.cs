using System.Text;
using HtmlAgilityPack;

namespace Doctrine {
    public class HtmlPage {
        public string Head { get; set; }
        public string Body { get; set; }

        public static HtmlPage FromPath(string path) {
            var html = new HtmlDocument();
            html.Load(path, Encoding.UTF8);
            var headNode = html.DocumentNode.SelectSingleNode("/html/head");
            var bodyNode = html.DocumentNode.SelectSingleNode("/html/body");
            var head = headNode == null ? string.Empty : headNode.InnerHtml;
            var body = bodyNode == null ? string.Empty : bodyNode.InnerHtml;
            return new HtmlPage {
                Head = head,
                Body = body
            };
        }
    }
}

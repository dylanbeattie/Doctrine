using System;
using System.Collections.Generic;
using HtmlAgilityPack;

namespace Doctrine {
    public class HtmlPage {
        public string Head { get; set; }
        public string Body { get; set; }

        public static HtmlPage FromPath(string path) {
            var html = new HtmlDocument();
            html.Load(path);
            var headNode = html.DocumentNode.SelectSingleNode("/html/head");
            var bodyNode = html.DocumentNode.SelectSingleNode("/html/body");
            var head = headNode == null ? String.Empty : headNode.InnerHtml;
            var body = bodyNode == null ? String.Empty : bodyNode.InnerHtml;
            return new HtmlPage() {
                Head = head,
                Body = body
            };
        }
    }

    public class TocEntry {
        public string Sort { get; set; }
        public string Text { get; set; }
        public string Href { get; set; }
        public string Name { get; set; }
        public IList<TocEntry> Children { get; set; }
    }
}
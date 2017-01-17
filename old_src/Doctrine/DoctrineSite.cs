using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using System.Web.Routing;
using HtmlAgilityPack;

namespace Doctrine {
    public class DoctrineSite {
        public static List<TocEntry> Contents { get; set; }

        /// <summary>Initialise the Doctrine application based on the specified HTML folder path and ASP.NET MVC routing table.</summary>
        /// <param name="pathToHtmlDocs">The local qualified filesystem path where HTML documents are stored.</param>
        /// <param name="routes">The ASP.NET MVC route collection used by the hosting application</param>
        public static void Init(string pathToHtmlDocs, RouteCollection routes) {
            routes.MapRoute("Doctrine", "{*path}", new { controller = "Doctrine", action = "Index", path = "index" });
            BeginWatchingFiles(pathToHtmlDocs);
            Contents = BuildTableOfContents(pathToHtmlDocs);
        }

        private static void BeginWatchingFiles(string path) {
            const NotifyFilters filter =
                NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName |
                NotifyFilters.DirectoryName;
            var watcher = new FileSystemWatcher(path, "*.html") {
                NotifyFilter = filter,
                IncludeSubdirectories = true
            };
            FileSystemEventHandler change = (sender, args) => Contents = BuildTableOfContents(path);
            RenamedEventHandler rename = (sender, args) => Contents = BuildTableOfContents(path);
            watcher.Changed += change;
            watcher.Created += change;
            watcher.Deleted += change;
            watcher.Renamed += rename;
            watcher.EnableRaisingEvents = true;
        }

        private static List<TocEntry> BuildTableOfContents(string folderPath) {
            var entries = new List<TocEntry>();

            foreach (var filePath in Directory.GetFiles(folderPath, "*.html", SearchOption.AllDirectories)) {
                for (var attempt = 0; attempt < 5; attempt++) {
                    try {
                        var entry = BuildTocEntry(filePath, folderPath);
                        entries.Add(entry);
                        break;
                    } catch (IOException) {
                        Thread.Sleep(TimeSpan.FromSeconds(2));
                    }
                }
            }
            return entries.OrderBy(entry => entry.Sort).ThenBy(entry => entry.Text).ToList();
        }

        private static string MakeHref(string filePath, string folderPath) {
            return filePath.Remove(filePath.Length - 5).Substring(folderPath.Length).Replace('\\', '/');
        }

        private static TocEntry BuildTocEntry(string filePath, string folderPath) {
            var html = new HtmlDocument();
            html.Load(filePath);
            var titleNode = html.DocumentNode.SelectSingleNode("/html/head/title");
            var href = MakeHref(filePath, folderPath);
            var entry = new TocEntry {
                Href = href,
                Text = titleNode?.InnerText ?? href
            };

            var sortNode = html.DocumentNode.SelectSingleNode("//meta[@name='sort']");
            entry.Sort = sortNode?.Attributes["value"]?.Value ?? "ZZZZZZZZ";
            var headings = html.DocumentNode.SelectNodes("//h1[@id] | //h2[@id]");
            entry.Children = headings == null
                ? new List<TocEntry>()
                : headings.Select(node => new TocEntry {
                    Text = node.InnerText,
                    Href = href + "#" + node.Attributes["id"].Value,
                    Name = node.Name
                }).ToList();
            return entry;
        }
    }
}

using System.Collections.Generic;

namespace Doctrine {
    public class TocEntry {
        public string Sort { get; set; }
        public string Text { get; set; }
        public string Href { get; set; }
        public string Name { get; set; }
        public IList<TocEntry> Children { get; set; }
    }
}
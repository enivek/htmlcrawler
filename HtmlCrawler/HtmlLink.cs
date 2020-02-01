using System;
using System.Collections.Generic;
using System.Text;

namespace HtmlCrawler
{
    class HtmlLink
    {

        internal HtmlLink(string name, string value)
        {
            Name = name ?? string.Empty;
            Name = Name.Trim();

            Link = value ?? string.Empty;
            Link = Link.ToLower();

            UniqueId = Link.Replace("/", "");
        }

        internal string Name { get; }

        internal string Link { get; }

        internal bool IsDocument()
        {
            if( !Link.EndsWith(".aspx") && !Link.EndsWith(".htm") && !Link.EndsWith(".html"))
            {
                return true;
            }
            return false;
        }

        private string UniqueId { get; }

        public override string ToString()
        {
            return Name + " (" + Link + ")";
        }

        public override int GetHashCode()
        {
            return UniqueId.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var link = obj as HtmlLink;
            if (link != null)
            {
                return (link.UniqueId == UniqueId);
            }
            return false;
        }
    }
}

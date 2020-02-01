using System;
using System.Collections.Generic;

namespace HtmlCrawler
{
    class HtmlPage
    {
        internal HtmlPage(HtmlLink url, string title, string content, string sharePointContent, List<HtmlLink> links )
        {
            Url = url ?? throw new ArgumentNullException();
            Title = title ?? string.Empty;
            Content = content ?? string.Empty;
            SharePointContent = sharePointContent ?? string.Empty;
            Links = links ?? new List<HtmlLink>();
        }

        internal List<HtmlLink> Links { get; }

        internal string Content { get; }

        internal string SharePointContent { get; }

        internal HtmlLink Url { get; }

        internal bool IsSharePoint {
            get
            {
                return !string.IsNullOrWhiteSpace(SharePointContent);
            }
        }

        internal string Title { get; }

        public override string ToString()
        {
            return Url.Link;
        }
    }
}

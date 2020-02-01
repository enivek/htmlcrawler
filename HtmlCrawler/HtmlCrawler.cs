using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace HtmlCrawler
{
    class HtmlCrawler
    {
        private readonly HtmlRetriever _retriever = new HtmlRetriever();
        private readonly HtmlPageParser _pageParser = new HtmlPageParser();
        private readonly List<HtmlLink> _linksToCrawl = new List<HtmlLink>();
        private readonly HashSet<HtmlLink> _linksCrawled = new HashSet<HtmlLink>();
        private readonly List<HtmlPage> _pagesCrawled = new List<HtmlPage>();
        private readonly HashSet<HtmlLink> _linksToDownload = new HashSet<HtmlLink>();
        private readonly HtmlCrawlerProgress _progress;

        private string _rootUrl = string.Empty;
        private string _subSite = string.Empty;
        private int _numberOfPagesToCrawl = Int32.MaxValue;

        internal HtmlCrawler(string rootUrl, string subSite, HtmlCrawlerProgress progress)
        {
            _rootUrl = rootUrl ?? throw new ArgumentNullException();
            _subSite = subSite ?? throw new ArgumentNullException();
            _progress = progress ?? throw new ArgumentNullException();
            _linksToCrawl.Add(new HtmlLink("", subSite));
        }

        internal int NumberOfLinksCrawled
        {
            get { return _linksCrawled.Count; }
        }

        internal int NumberOfLinksToCrawl
        {
            get { return _linksToCrawl.Count; }
        }

        internal int NumberOfPagesCrawled
        {
            get { return _pagesCrawled.Count; }
        }

        internal void Crawl(int pagesToCrawl)
        {
            _numberOfPagesToCrawl = pagesToCrawl;
            Crawl();
        }

        internal void Crawl()
        {
            while (_linksToCrawl.Any() && _pagesCrawled.Count < _numberOfPagesToCrawl)
            {
                var link = _linksToCrawl[0];
                _linksToCrawl.RemoveAt(0);

                if (!_linksCrawled.Contains(link) && link.Link.StartsWith(_subSite))
                {
                    var url = _rootUrl + link.Link;
                    var document = _retriever.GetHtmlDocument(url);
                    var page = _pageParser.GetHtmlPage(link, document);

                    _linksCrawled.Add(page.Url);
                    _pagesCrawled.Add(page);
                    foreach( var linkInPage in page.Links )
                    {
                        _linksToCrawl.Add(linkInPage);
                    }

                    _cullLinksNotGoingToCrawl();

                    _progress.PrintProgress(this);
                }
            }
        }

        private void _cullLinksNotGoingToCrawl()
        {
            _linksToCrawl.RemoveAll(k => !k.Link.StartsWith(_subSite));

            var list = _linksToCrawl.ToList();
            foreach( var link in list)
            {
                if( link.IsDocument() && link.Link != _subSite )
                {
                    _linksToDownload.Add(link);
                    _linksToCrawl.Remove(link);
                }
            }
        }

    }
}

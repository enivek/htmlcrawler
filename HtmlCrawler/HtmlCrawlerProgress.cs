using System;

namespace HtmlCrawler
{
    class HtmlCrawlerProgress
    {
        private DateTime _timeLastLogged = DateTime.Now;

        internal void PrintProgress(HtmlCrawler crawler)
        {
            if(_timeLastLogged < DateTime.Now.AddSeconds(-10) )
            {
                string template = "Crawled: {0}: {1} pages, {2} links, {3} links remaining";
                string progress = string.Format(template, DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"), crawler.NumberOfPagesCrawled, crawler.NumberOfLinksCrawled, crawler.NumberOfLinksToCrawl);
                Console.WriteLine(progress);
                _timeLastLogged = DateTime.Now;
            }
        }
    }
}

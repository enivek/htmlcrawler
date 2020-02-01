using System;

namespace HtmlCrawler
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Started Crawling at " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));

            var crawlerProgress = new HtmlCrawlerProgress();
            var crawler = new HtmlCrawler("http://www.leg.wa.gov", "/house", crawlerProgress);
            crawler.Crawl(200);

            Console.WriteLine("Press the Enter key to exit anytime... ");
            Console.ReadLine();
        }
    }
}

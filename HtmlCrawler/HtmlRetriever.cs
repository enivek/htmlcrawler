using HtmlAgilityPack;
using System;
using System.Net;
using System.Text;

namespace HtmlCrawler
{
    public class HtmlRetriever
    {
        internal HtmlDocument GetHtmlDocument(string url)
        {
            var htmlString = _getHtmlData(url);
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(htmlString);
            return document;
        }

        string _getHtmlData(string url)
        {
            string result = string.Empty;
            using (var client = new WebClient())
            {
                try
                {
                    var byteArray = client.DownloadData(url);
                    result = Encoding.UTF8.GetString(byteArray);
                }
                catch( WebException ex )
                {
                    Console.WriteLine("Warning: _getHtmlData(): " + url);
                }
            }
            return result;
        }

    }
}

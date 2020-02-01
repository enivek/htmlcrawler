using HtmlAgilityPack;
using Fizzler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace HtmlCrawler
{
    class HtmlPageParser
    {

        internal HtmlPage GetHtmlPage(HtmlLink url, HtmlDocument document)
        {
            var title = _getTitle(document.DocumentNode);
            var links = _getLinks(document.DocumentNode);
            var isSharePoint = _isSharePoint(document.DocumentNode);
            var content = document.DocumentNode.OuterHtml;
            var sharePointContent = _getSharePointContent(document.DocumentNode);
            var page = new HtmlPage(url, title, content, sharePointContent, links);
            return page;
        }

        string _getTitle(HtmlNode documentNode)
        {
            try
            {
                var titleText = documentNode.SelectSingleNode("//title").InnerText;
                titleText = Regex.Replace(titleText, @"\s+", " ");
                return titleText.Trim();
            }
            catch( Exception ex )
            {
                Console.WriteLine("Warning: " + "_getTitle() " + ex.Message.Trim());
                return string.Empty;
            }
        }

        bool _isSharePoint(HtmlNode documentNode)
        {
            var nodes = documentNode.SelectNodes("//*[contains(@class, 'contentContainer')]");
            if (nodes != null)
            {
                return true;
            }
            return false;
        }

        string _getSharePointContent(HtmlNode documentNode)
        {
            string result = string.Empty;
            try
            {
                var nodes = documentNode.SelectNodes("//*[contains(@class, 'contentContainer')]");
                if( nodes != null )
                {
                    var oneContainerNode = nodes.FirstOrDefault();
                    if (oneContainerNode != null)
                    {
                        result = oneContainerNode.InnerHtml.Trim();
                    }
                }
            }
            catch( Exception ex )
            {
                Console.WriteLine("Warning: " + "_getSharePointContent() " + ex.Message.Trim());
            }
            return result;
        }

        List<HtmlLink> _getLinks(HtmlNode documentNode)
        {
            var result = new List<HtmlLink>();

            try
            {
                HtmlNode[] nodes = documentNode.SelectNodes("//a").ToArray();
                foreach (HtmlNode item in nodes)
                {
                    var name = item.InnerText;
                    var value = string.Empty;

                    foreach (var attrib in item.Attributes)
                    {
                        if (attrib.Name == "href")
                        {
                            value = attrib.Value;
                        }
                    }

                    result.Add(new HtmlLink(name, value));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Warning: " + "_getLinks() " + ex.Message.Trim());
            }

            return result;
        }
    }
}

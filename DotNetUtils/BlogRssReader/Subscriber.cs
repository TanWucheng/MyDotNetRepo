using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace DotNetUtils.BlogRssReader
{
    /// <summary>
    /// RSS订阅器
    /// </summary>
    public class Subscriber
    {
        /// <summary>
        /// 订阅RSS
        /// </summary>
        /// <param name="rssUrl">RSS订阅源URL，文件系统路径或者HTTP链接</param>
        /// <param name="articleCount">文章数量</param>
        /// <returns></returns>
        public static Feed Subscribe(string rssUrl, int articleCount = int.MaxValue)
        {
            var xmlDoc = new XmlDocument();
            if (string.IsNullOrWhiteSpace(rssUrl)) return null;
            try
            {
                xmlDoc.Load(rssUrl);
                if (!xmlDoc.HasChildNodes) return null;
                //var entryNodeList = xmlDoc.GetElementsByTagName("entry");
                var childList = xmlDoc.ChildNodes;
                foreach (XmlNode child in childList)
                {
                    switch (child.Name.ToLower())
                    {
                        case "feed":
                            {
                                return ReadFeed(child, articleCount);
                            }
                    }
                }

                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// 读取RSS订阅信息XML节点
        /// </summary>
        /// <param name="feedNode">RSS Feed节点</param>
        /// <param name="articleCount">文章数量</param>
        /// <returns></returns>
        private static Feed ReadFeed(XmlNode feedNode, int articleCount)
        {
            var feed = new Feed();
            if (!feedNode.HasChildNodes) return feed;
            var feedChildNodes = feedNode.ChildNodes;
            var entryChildNodes = new List<XmlNode>();
            var authors = new List<Author>();
            foreach (XmlNode feedChildNode in feedChildNodes)
            {
                switch (feedChildNode.Name.ToLower())
                {
                    case "title":
                        {
                            feed.Title = feedChildNode.InnerText;
                            break;
                        }
                    case "icon":
                        {
                            feed.Icon = feedChildNode.InnerText;
                            break;
                        }
                    case "subtitle":
                        {
                            feed.Subtitle = feedChildNode.InnerText;
                            break;
                        }
                    case "link":
                        {
                            if (feedChildNode.Attributes != null)
                            {
                                if (feedChildNode.Attributes["rel"] != null && feedChildNode.Attributes["rel"].Value == "self")
                                {
                                    feed.SelfLink = feedChildNode.Attributes["href"]?.Value;
                                }
                                else
                                {
                                    feed.Link = feedChildNode.Attributes["href"]?.Value;
                                }
                            }
                            break;
                        }
                    case "id":
                        {
                            feed.Id = feedChildNode.InnerText;
                            break;
                        }
                    case "author":
                        {
                            authors.Add(new Author
                            {
                                Name = feedChildNode.InnerText
                            });
                            break;
                        }
                    case "entry":
                        {
                            entryChildNodes.Add(feedChildNode);
                            break;
                        }
                }
            }

            var entries = ReadEntries(entryChildNodes, articleCount);
            feed.Entries = entries;
            feed.Author = authors.Any() ? authors[0] : new Author();

            return feed;
        }

        /// <summary>
        /// 读取文章信息
        /// </summary>
        /// <param name="entryNodes">文章节点清单</param>
        /// <param name="articleCount">文章数量</param>
        /// <returns></returns>
        private static List<Entry> ReadEntries(List<XmlNode> entryNodes, int articleCount)
        {
            try
            {
                var count = 1;
                var entries = new List<Entry>();
                foreach (var entryNode in entryNodes)
                {
                    var entry = new Entry();
                    if (entryNode.HasChildNodes)
                    {
                        var entryChildNodes = entryNode.ChildNodes;
                        var categories = new List<Category>();
                        foreach (XmlNode entryChildNode in entryChildNodes)
                        {
                            switch (entryChildNode.Name.ToLower())
                            {
                                case "title":
                                    {
                                        entry.Title = entryChildNode.InnerText;
                                        break;
                                    }
                                case "link":
                                    {
                                        if (entryChildNode.Attributes != null)
                                        {
                                            var href = entryChildNode.Attributes["href"]?.Value;
                                            entry.Link = href;
                                        }

                                        break;
                                    }
                                case "id":
                                    {
                                        entry.Id = entryChildNode.InnerText;
                                        break;
                                    }
                                case "published":
                                    {
                                        if (DateTime.TryParse(entryChildNode.InnerText, out var published))
                                        {
                                            entry.Published = published;
                                        }
                                        break;
                                    }
                                case "updated":
                                    {
                                        if (DateTime.TryParse(entryChildNode.InnerText, out var updated))
                                        {
                                            entry.Updated = updated;
                                        }
                                        break;
                                    }
                                case "summary":
                                    {
                                        var summaryType = SummaryType.Plain;
                                        if (entryChildNode.Attributes != null)
                                        {
                                            var type = entryChildNode.Attributes["type"]?.Value;
                                            summaryType =
                                               EnumParser<SummaryType>.Parse(type, SummaryType.Plain);
                                        }

                                        var summary = new Summary
                                        {
                                            Content = entryChildNode.InnerText,
                                            SummaryType = summaryType
                                        };
                                        entry.Summary = summary;
                                        break;
                                    }
                                case "category":
                                    {
                                        if (entryChildNode.Attributes != null)
                                        {
                                            var term = entryChildNode.Attributes["term"]?.Value;
                                            var scheme = entryChildNode.Attributes["scheme"]?.Value;
                                            var category = new Category
                                            {
                                                Scheme = scheme,
                                                Term = term
                                            };
                                            categories.Add(category);
                                        }

                                        break;
                                    }
                            }
                        }

                        entry.Categories = categories;
                        entries.Add(entry);
                    }

                    count += 1;
                    if (count > articleCount) break;
                }

                return entries;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}

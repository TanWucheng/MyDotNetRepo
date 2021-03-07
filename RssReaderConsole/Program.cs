using System;
using DotNetUtils.BlogRssReader;

namespace RssReaderConsole
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            const string url = "https://tanwucheng.github.io/atom.xml";
            Console.WriteLine($"Feed from {url}");
            var feed = Subscriber.Subscribe(url, 10);
            Console.WriteLine(feed.Title);
            Console.WriteLine(feed.Link);
            Console.WriteLine(feed.Author.Name);
        }
    }
}

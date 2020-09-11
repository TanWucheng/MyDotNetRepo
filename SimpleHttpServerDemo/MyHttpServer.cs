using System;
using System.IO;

namespace SimpleHttpServerDemo
{
    internal class MyHttpServer : HttpServer
    {
        public MyHttpServer(int port) : base(port) { }

        public override void HandleGetRequest(HttpProcessor p)
        {
            Console.WriteLine("request: {0}", p.HttpUrl);
            p.WriteSuccess();
            // p.outputStream.WriteLine(@"<html>
            //     <head>
            //         <meta charset='UTF-8'>
            //         <meta http-equiv='X-UA-Compatible' content='IE=edge,chrome=1'>
            //     </head>
            //     <body>
            //     <h1>测试Http Server</h1>");
            // p.outputStream.WriteLine("当前日期时间: " + DateTime.Now.ToString());
            // p.outputStream.WriteLine("当前请求的URL : {0}", p.HttpUrl);

            // p.outputStream.WriteLine(@"<form method='post' action='/form'>
            //     <input type='text' name='foo' value='foovalue'/>
            //     <input type='submit' name='bar' value='barvalue'/>
            //     </form>
            //     </body></html>");

            p.outputStream.WriteLine("<html><body><h1>test server</h1>");
            p.outputStream.WriteLine("Current Time: " + DateTime.Now.ToString());
            p.outputStream.WriteLine("url : {0}", p.HttpUrl);
            p.outputStream.WriteLine("<form method=post action=/form>");
            p.outputStream.WriteLine("<input type=text name=foo value=foovalue>");
            p.outputStream.WriteLine("<input type=submit name=bar value=barvalue>");
            p.outputStream.WriteLine("</form>");
        }

        public override void HandlePostRequest(HttpProcessor p, StreamReader inputData)
        {
            Console.WriteLine("POST request: {0}", p.HttpUrl);
            string data = inputData.ReadToEnd();

            // p.outputStream.WriteLine(@"<html><body><h1>测试Http Server</h1>
            //     <a href='/test'>return</a>
            //     <p> Post body: <pre>{0}</pre></p>
            //     </body></html>", data);

            p.outputStream.WriteLine("<html><body><h1>test server</h1>");
            p.outputStream.WriteLine("<a href=/test>return</a><p>");
            p.outputStream.WriteLine("postbody: <pre>{0}</pre>", data);
        }
    }
}
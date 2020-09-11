using System;
using System.Collections;
using System.IO;
using System.Net.Sockets;

namespace SimpleHttpServerDemo
{
    internal class HttpProcessor
    {
        public TcpClient Socket;
        public HttpServer Server;

        private StreamReader inputStream;
        public StreamWriter outputStream;

        public string HttpMethod;
        public string HttpUrl;
        public string HttpProtocolVersionstring;
        public Hashtable HttpHeaders = new Hashtable();
        private static int MaxPostSize = 10 * 1024 * 1024; // 10MB

        public HttpProcessor(TcpClient socket, HttpServer server)
        {
            this.Socket = socket;
            this.Server = server;
        }

        public void Process()
        {
            // bs = new BufferedStream(s.GetStream());
            inputStream = new StreamReader(Socket.GetStream());
            outputStream = new StreamWriter(new BufferedStream(Socket.GetStream()));
            try
            {
                ParseRequest();
                ReadHeaders();
                if (HttpMethod.Equals("GET"))
                {
                    HandleGetRequest();
                }
                else if (HttpMethod.Equals("POST"))
                {
                    HandlePostRequest();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.ToString());
                WriteFailure();
            }
            outputStream.Flush();
            // bs.Flush(); 
            // flush any remaining output
            inputStream = null;
            outputStream = null;
            // bs = null;            
            Socket.Close();
        }

        public void ParseRequest()
        {
            string request = inputStream.ReadLine();
            string[] tokens = request.Split(' ');
            if (tokens.Length != 3)
            {
                throw new Exception("invalid http request line");
            }
            HttpMethod = tokens[0].ToUpper();
            HttpUrl = tokens[1];
            HttpProtocolVersionstring = tokens[2];

            Console.WriteLine("starting: " + request);
        }

        public void ReadHeaders()
        {
            Console.WriteLine("readHeaders()");
            string line;
            while ((line = inputStream.ReadLine()) != null)
            {
                if (line.Equals(""))
                {
                    Console.WriteLine("got headers");
                    return;
                }

                int separator = line.IndexOf(':');
                if (separator == -1)
                {
                    throw new Exception("invalid http header line: " + line);
                }
                string name = line.Substring(0, separator);
                int pos = separator + 1;
                while ((pos < line.Length) && (line[pos] == ' '))
                {
                    pos++; // strip any spaces
                }

                string value = line.Substring(pos, line.Length - pos);
                Console.WriteLine("header: {0}:{1}", name, value);
                HttpHeaders[name] = value;
            }
        }

        public void HandleGetRequest()
        {
            Server.HandleGetRequest(this);
        }

        public void HandlePostRequest()
        {
            // this post data processing just reads everything into a memory stream.
            // this is fine for smallish things, but for large stuff we should really
            // hand an input stream to the request processor. However, the input stream 
            // we hand him needs to let him see the "end of the stream" at this content 
            // length, because otherwise he won't know when he's seen it all! 

            Console.WriteLine("get post data start");
            int contentLength = 0;
            MemoryStream ms = new MemoryStream();
            if (this.HttpHeaders.ContainsKey("Content-Length"))
            {
                contentLength = Convert.ToInt32(this.HttpHeaders["Content-Length"]);
                if (contentLength > MaxPostSize)
                {
                    throw new Exception(string.Format("POST Content-Length({0}) too big for this simple server", contentLength));
                }
                byte[] buf = new byte[4096];
                int toRead = contentLength;
                while (toRead > 0)
                {
                    int numread = this.inputStream.BaseStream.Read(buf, 0, Math.Min(4096, toRead));
                    toRead -= numread;
                    ms.Write(buf, 0, numread);
                }
                ms.Seek(0, SeekOrigin.Begin);
            }
            Console.WriteLine("get post data end");
            Server.HandlePostRequest(this, new StreamReader(ms));

        }

        public void WriteSuccess()
        {
            outputStream.Write("HTTP/1.0 200 OK\n");
            outputStream.Write("Content-Type: text/html\n");
            outputStream.Write("Connection: close\n");
            outputStream.Write("\n");
        }

        public void WriteFailure()
        {
            outputStream.Write("HTTP/1.0 404 File not found\n");
            outputStream.Write("Connection: close\n");
            outputStream.Write("\n");
        }
    }

}
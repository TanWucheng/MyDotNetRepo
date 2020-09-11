using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace SimpleHttpServerDemo
{
    internal abstract class HttpServer
    {
        protected int Port;
        private TcpListener _listener;
        private bool _isActive = true;

        public HttpServer(int port)
        {
            Port = port;
        }

        public void Listen()
        {
            _listener = new TcpListener(Port);
            _listener.Start();
            while (_isActive)
            {
                TcpClient socket = _listener.AcceptTcpClient();
                HttpProcessor processor = new HttpProcessor(socket, this);
                Thread thread = new Thread(new ThreadStart(processor.Process));
                thread.Start();
                Thread.Sleep(1);
            }
        }

        public abstract void HandleGetRequest(HttpProcessor p);
        public abstract void HandlePostRequest(HttpProcessor p, StreamReader inputData);
    }
}
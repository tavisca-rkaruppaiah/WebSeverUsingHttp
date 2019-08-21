using System;
using System.Net;

namespace WebServer
{
    public class WebServer
    {
        private readonly HttpListener _listener = new HttpListener();
        public WebServer(params string[] prefixes)
        {
            foreach (var item in prefixes)
                _listener.Prefixes.Add(item);
        }

        public void StartServer()
        {
            Console.WriteLine("Server Starting...");
            _listener.Start();
            new Request(_listener).ProcessRequest();
        }

        public void StopServer()
        {
            _listener.Stop();
            Console.WriteLine("Server Stopped");
        }
        
    }
}

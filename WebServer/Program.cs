using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WebServer
{
    class Program
    {
        static void Main(string[] args)
        {
           while(true)
            {
                WebServer webServer = new WebServer("http://localhost:8080/");
                webServer.StartServer();
                webServer.StopServer();
            } 
        }
    }
}

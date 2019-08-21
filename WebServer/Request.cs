using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace WebServer
{
    public class Request
    {
        private HttpListenerContext _context;
        private readonly HttpListenerRequest _request;
        private FileHandler _fileHandle = new FileHandler();
        private string _method;
        private HttpListener _httpListener;
        public Request(HttpListener httpListener)
        {
            _context = httpListener.GetContext();
            _request = _context.Request;
            _httpListener = httpListener;
            _method = _request.HttpMethod;
            
        }

       
        public void ProcessRequest()
        {
           if(_method == "GET")
            {
                Console.WriteLine("This is a GET Method");
                string url = _request.Url.OriginalString.ToString().Split('/')[3];
                new Response(_context).GetResponse(File.ReadAllText(_fileHandle.GetFilePath(url)));
            }
           else if(_method == "POST")
            {
                Console.WriteLine("This is a POST Method");
                PostMethod();
            }
        }

        public void PostMethod()
        { 
            Stream body = _request.InputStream;
            Encoding encoding = _request.ContentEncoding;
            StreamReader reader = new StreamReader(body, encoding);
            if (_request.ContentType != null)
            {
                Console.WriteLine("Client data content type {0}", _request.ContentType);
            }
            Console.WriteLine("Client data content length {0}", _request.ContentLength64);
            Console.WriteLine("Start of client JSON data:");
            string s = reader.ReadToEnd();
            Console.WriteLine(s);
            Console.WriteLine("End of client data:");
            Console.WriteLine("Parsing the JSON Request Body.....");  
            var jsonObj = JObject.Parse(s);
            Console.WriteLine(jsonObj["year"]);
            int _year;
            int.TryParse(jsonObj["year"].ToString(), out _year);

            if (((_year % 4 == 0) && (_year % 100 != 0)) || (_year % 400 == 0))
            {
                new Response(_context).GetResponse("{\n" +
                    "\tisLeap : true" +
                    "}");
                Console.WriteLine("{0} is a Leap Year.", _year);
            }
            else
            {
                Console.WriteLine("{0} is not a Leap Year.", _year);
                new Response(_context).GetResponse("{\n" +
                    "\tisLeap : false" +
                    "}");
            }
   
        }

    }
}

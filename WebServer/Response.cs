using System.IO;
using System.Net;
using System.Text;

namespace WebServer
{
    public class Response
    {
        private HttpListenerResponse _response;
        private HttpListenerContext _context;
        
        public Response(HttpListenerContext context)
        {
            _context = context;
            _response = _context.Response;
            
            
        }
        public void GetResponse(string file)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(file);
            _response.ContentLength64 = buffer.Length;
            _response.ContentType = _context.Request.ContentType;
            Stream output = _response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            output.Close();
        }
    }
}

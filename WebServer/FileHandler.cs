using System;

namespace WebServer
{
    public class FileHandler
    {
        private const string WEB_DIR = @"\root\web\";

        public string GetFilePath(string file)
        {
            return Environment.CurrentDirectory + WEB_DIR +file.ToString();
        }
    }
}

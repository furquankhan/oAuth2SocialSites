using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.IO;
using Newtonsoft.Json;
using System.Net;
using System.Net.Mail;



namespace oauth2.services
{
    public partial class service
    {
        StringBuilder retval = new StringBuilder();
        string error = "\"status\":\"0\",\"error\":\"{0}\", \"msg\":\"{1}\"";
        public string returnblank(string status)
        {
            return "{\"status\":\"" + status + "\", \"fields\": [], \"records\": []}";
        }
        public string testservice(HttpContext context)
        {
            return "{\"status\":\"1\", \"Message\": \"Success\"}";
        }
        
        public string Hello(HttpContext context)
        {
            return "{\"status\":\"1\", \"Message\": \"Success\"}";
        }
        private Dictionary<string, string> jsondata()
        {
            string jsonString = String.Empty;
            var dict = new Dictionary<string, string>();
            HttpContext.Current.Request.InputStream.Position = 0;
            using (StreamReader inputStream = new StreamReader(HttpContext.Current.Request.InputStream))
            {
                jsonString = inputStream.ReadToEnd();
            }
            dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonString);
            return dict;
            //return inputValues;
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}

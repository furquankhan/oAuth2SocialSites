using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using System.Text;
using System.Web.Script.Serialization;
//using oauth2.entity.Entities;
using System.IO;
//using oauth2.services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;


namespace oauth2.web.services
{
    /// <summary>
    /// Summary description for data
    /// </summary>
    public class data : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json; charset=utf-8";
            var method = context.Request["method"];
            //var accessToken = context.Request.data["access_token"]
            var retval = string.Empty;
            if (method == "GetUserInfo")
            {
                retval = GetUserInfo();

            }
            //service program = new service();
            //Type thisType = program.GetType();
            //MethodInfo theMethod = thisType.GetMethod(method);
            //JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            //var retval = javaScriptSerializer.Serialize(theMethod.Invoke(program, new object[] { context }));
            //Console.WriteLine(retval);
            context.Response.Write(retval);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
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
        }
        public string GetUserInfo()
        {
            Dictionary<string, string> data = jsondata();
            var accessToken = data["accessToken"];
            var expiresIn = data["expireIn"];
            string HitURL = string.Format("https://graph.facebook.com/me?access_token={0}&fields=email", accessToken);
             WebRequest request = HttpWebRequest.Create(HitURL);
                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        string urlText = reader.ReadToEnd();
                        return urlText;
                    }
                }
        }
    }
}
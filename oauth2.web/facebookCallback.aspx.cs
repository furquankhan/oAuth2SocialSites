using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace oauth2.web
{
    public partial class facebookCallback : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //var code = HttpContext.Current.Request.QueryString["code"];
            //var accessToken = getAccessTokenFromCode(code);

        }
        protected string getAccessTokenFromCode(string code)
        {
            //Write a get api to get accesstoken and refresh token using code sent by fb
            //deserialize the response and fetch access token
            var result = string.Empty;
            var url = "";


            return result;
        }
        protected string GetRestAPI(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            var html = string.Empty;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                html = reader.ReadToEnd();
            }
            return html;
        }
    }
}
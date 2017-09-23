using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Text;

namespace oauth2.services
{
    public static class GridConfig
    {
        private static XmlDocument gridxml;
        public static void loadconfig()
        {
            gridxml = new XmlDocument();
            if (ExtensionMethods.installpath == "")
                ExtensionMethods.installpath = HttpContext.Current.Server.MapPath("~");
            gridxml.Load(ExtensionMethods.installpath + "\\App_Data\\grid.xml");

        }
        public static string getgrid(string table, out string[] fieldarray) {
            if (gridxml == null)
                loadconfig();
            StringBuilder oStr = new StringBuilder();
            XmlNodeList oFields = gridxml.SelectNodes("//grid//" + table + "//field");
            fieldarray = new string[oFields.Count];
            for (int i = 0; i <= oFields.Count - 1; i++)
            {
                if (i > 0) oStr.Append(",");
                var ENUM = "";
                if (oFields[i].Attributes["enum"] != null)
                    ENUM = oFields[i].Attributes["enum"].Value;
                oStr.Append("{");
                if (oFields[i].Attributes["isdate"] != null)
                    oStr.Append(string.Format("\"dbfield\": \"{0}\", \"visible\":\"{1}\",\"label\":\"{2}\",\"isdate\": \"{3}\",\"enum\": \"{4}\"", oFields[i].Attributes["dbname"].Value, oFields[i].Attributes["isvisible"].Value, oFields[i].Attributes["label"].Value, oFields[i].Attributes["isdate"].Value, ENUM));
                else if (oFields[i].Attributes["isdatetime"] != null)
                    oStr.Append(string.Format("\"dbfield\": \"{0}\", \"visible\":\"{1}\",\"label\":\"{2}\",\"isdatetime\": \"{3}\",\"enum\": \"{4}\"", oFields[i].Attributes["dbname"].Value, oFields[i].Attributes["isvisible"].Value, oFields[i].Attributes["label"].Value, oFields[i].Attributes["isdatetime"].Value, ENUM));
                else
                    oStr.Append(string.Format("\"dbfield\": \"{0}\", \"visible\":\"{1}\",\"label\":\"{2}\",\"enum\": \"{3}\"", oFields[i].Attributes["dbname"].Value, oFields[i].Attributes["isvisible"].Value, oFields[i].Attributes["label"].Value, ENUM));
                oStr.Append("}");
                fieldarray[i] = oFields[i].Attributes["dbname"].Value;
            }
            return oStr.ToString();
        }
    }
}
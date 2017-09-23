using System;
using System.Configuration;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace reflections.ilb.entity
{
	public class ILBConnection
	{
		public static SqlConnection CreateConnection()
		{
			var connection = new SqlConnection(GetConnectionString());
			
			return connection;
		}
		public static string GetConnectionString()
		{
			return ConfigurationManager.ConnectionStrings["ILBConnectionString"].ConnectionString;
		}
	}
}

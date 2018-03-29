using System;
using System.Data;
using System.Data.SqlClient;

namespace Utils {
	public class Db {
		public SqlConnection Connection;
		public String Url;

		public Db (String url){
			Url = url;
		}
		public void Connect () {
			if(Connection != null){
				Console.WriteLine("Already Connected");
			} else {	
				Connection = new SqlConnection(Url);
				Connection.Open();
			}
		}

		public void Disconnect () {
			if(Connection == null){
				Console.WriteLine("There is no connections");
			}else{			
				Connection.Close();
				Connection = null;
			}
		}
	}
}
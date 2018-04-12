using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Utils;

namespace Models {
	
	public class Person {
		public Int32  Id;
		public String Name;
		public String Password;
		public String Email;
		public String Phone;

		public Int32 Role;

		public Person (  String name
						,String password 
						,String email
						,String phone )
		{
			Name 			= name;
			Password 	= password;
			Email 		= email;
			Phone		= phone;
		}

		public Person (Int32  id
									,String name
									,String password
									,String email
									,Int32 role
									,String phone)
		{
			Id 			 = id;
			Name 	 	 = name;
			Password = password;
			Email 	 = email;
			Phone 	= phone;
			Role = role;
		}

		public Person ( Int32 id,
						String name,
						String email,
						String phone) 
		{
			Id = id;
			Name = name;
			Email = email;
			Phone = phone;
		}
		public Person (){}

		public List<Person> List () {
			List<Person> persons = new List<Person>();
			String query = "select id_user, name, email, phone from persons.user_data";
			String conn = "Data Source=localhost; Initial Catalog=test; Integrated Security=false; User Id=sa; Password=abc123##";
			Db db = new Db(conn);
			db.Connect();
			SqlCommand cmd = new SqlCommand(query, db.Connection);
			SqlDataReader reader = cmd.ExecuteReader();
			try {
				while(reader.Read()){
					persons.Add(
						new Person ( reader.GetInt32(0)
                                	,reader.GetString(1)
                                	,reader.GetString(2)
                                	,reader.GetString(3))
					);
				}
			} catch (System.Exception e) {
				Console.WriteLine("Database error");
			} finally {
				reader.Close();
			}
			db.Disconnect();
			return persons;
		}

		public void Create () {
			String query = "Insert into persons.user_data (name, password, email, phone) values(@Name, hashbytes('MD5', @Password), @Email, @Phone)";
			String conn =	"Data Source=localhost; Initial Catalog=test; Integrated Security=false; User Id=sa; Password=abc123##";
			Db db = new Db(conn); 	
			db.Connect();
			SqlCommand cmd = new SqlCommand(query, db.Connection);
			cmd.Parameters.Add(new SqlParameter("Name", Name));
			cmd.Parameters.Add(new SqlParameter("Password", Password));
			cmd.Parameters.Add(new SqlParameter("Email", Email));
			cmd.Parameters.Add(new SqlParameter("Phone", Phone));
			var i = cmd.ExecuteNonQuery();
			if(i > 0){
				Console.WriteLine("Data inserted");
			} else {
				Console.WriteLine("Error");
			}
			db.Disconnect();
		}

		public Person Read( Int32 id ) {
			Person p = null;
			String query = "select id_user, name, email, phone from persons.user_data where @Id = id_user";
			String conn = "Data Source=localhost; Initial Catalog=test; Integrated Security=false; User Id=sa; Password=abc123##";
			Db db = new Db(conn);
			db.Connect();
			SqlCommand cmd = new SqlCommand(query, db.Connection);
			cmd.Parameters.Add(new SqlParameter("Id", id));
			SqlDataReader reader = cmd.ExecuteReader();
			try {
				while(reader.Read()){
					p = new Person
							(
								reader.GetInt32(0),
								reader.GetString(1),
								reader.GetString(2),
								reader.GetString(3)
							);
				}
			}catch(SystemException e) {
				Console.WriteLine("Database error");
			}

			return p;
		}

		public void Update ( Int32 id ) {
			String query = $"update persons.user_data set name= @Name, email=@Email, phone=@Phone  where id_user = @id";
			String conn =	"Data Source=localhost; Initial Catalog=test; Integrated Security=false; User Id=sa; Password=abc123##";
			Db db = new Db(conn);
			db.Connect();
			SqlCommand cmd = new SqlCommand(query, db.Connection);
			cmd.Parameters.Add(new SqlParameter("Name", Name));
			cmd.Parameters.Add(new SqlParameter("Email", Email));
			cmd.Parameters.Add(new SqlParameter("id", id));
			cmd.Parameters.Add(new SqlParameter("Phone", Phone));
			var i = cmd.ExecuteNonQuery();
			if(i > 0){
				Console.WriteLine("Data updated");
			} else {
				Console.WriteLine("Error");
			}
			db.Disconnect();
		}

		public void Delete ( Int32 id ) {
			String query = $"delete from persons.user_data where id_user=@id";
			String conn =	"Data Source=localhost; Initial Catalog=test; Integrated Security=false; User Id=sa; Password=abc123##";
			Db db = new Db(conn);
			db.Connect();
			SqlCommand cmd = new SqlCommand(query, db.Connection);
			cmd.Parameters.Add(new SqlParameter("id", id));
			var i = cmd.ExecuteNonQuery();
			if(i > 0){
				Console.WriteLine("Data deleted");
			} else {
				Console.WriteLine("Error");
			}
			db.Disconnect();
		}

		public bool Login () {
			String query = $"select id_user, name, role, phone from persons.user_data as p where p.email = @Email and hashbytes('MD5', @Password) = p.password";
			String conn = "Data Source=localhost; Initial Catalog=test; Integrated Security=false; User Id=sa; Password=abc123##";
			Db db = new Db(conn);
			db.Connect();
			SqlCommand cmd = new SqlCommand(query, db.Connection);
			cmd.Parameters.Add(new SqlParameter("Email", Email));
            cmd.Parameters.Add(new SqlParameter("Password", Password));
            SqlDataReader reader = cmd.ExecuteReader();
			try{
				while(reader.Read()){
					this.Id = reader.GetInt32(0);
					this.Name = reader.GetString(1);
					this.Role = reader.GetInt32(2);	
					this.Phone = reader.GetString(3);	
				}
            }catch(Exception e){
				Console.WriteLine("Database error");
			}
			Console.WriteLine("Name: " + Name + " Id: " + Id + " Email: " + Email + " Senha: " + Password);
			return (Name != null);
		}

	}

}
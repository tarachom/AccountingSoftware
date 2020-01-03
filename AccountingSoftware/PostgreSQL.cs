using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Npgsql;

namespace WebServerTestErlang.AccountingSoftware
{
	public class PostgreSQL: IDataBase
	{
		public PostgreSQL()
		{

		}

		~PostgreSQL()
		{

		}

		public void Open()
		{
			Connection = new NpgsqlConnection(ConnectionString);
			Connection.Open();
		}

		public void Close()
		{
			Connection.Close();
		}

		public string ConnectionString { get; set; }

		private NpgsqlConnection Connection { get; set; }

		public void SelectDirectory(string query)
		{
			NpgsqlCommand nCommand = new NpgsqlCommand(query, Connection);

			NpgsqlDataReader reader = nCommand.ExecuteReader();
			while (reader.Read())
			{
				//Console.WriteLine(reader["uid"]);
			}
			reader.Close();
		}
	}
}

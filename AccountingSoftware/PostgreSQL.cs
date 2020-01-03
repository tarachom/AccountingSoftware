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

		public List<DirectoryPointer> SelectDirectory(DirectorySelect sender)
		{
			List<DirectoryPointer> listDirectoryPointer = new List<DirectoryPointer>();

			string query = sender.QueryConstructor.Construct();

			NpgsqlCommand nCommand = new NpgsqlCommand(query, Connection);

			NpgsqlDataReader reader = nCommand.ExecuteReader();
			while (reader.Read())
			{
				DirectoryPointer elementPointer = new DirectoryPointer();

				List<KeyValuePair<string, object>> fields = new List<KeyValuePair<string, object>>();

				//Console.WriteLine(reader["uid"]);
				foreach (KeyValuePair<string, string> field in sender.QueryConstructor.Field)
				{
					fields.Add(new KeyValuePair<string, object>(field.Key, reader[field.Key]));

					//Console.WriteLine(field.Key + " = " + reader[field.Key]);
				}

				elementPointer.Init(new UnigueID(reader["uid"].ToString()), fields);

				listDirectoryPointer.Add(elementPointer);
			}
			reader.Close();

			return listDirectoryPointer;
		}
	}
}

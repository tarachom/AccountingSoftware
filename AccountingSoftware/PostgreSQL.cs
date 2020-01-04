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

			string query = sender.QuerySelect.Construct();

			NpgsqlCommand nCommand = new NpgsqlCommand(query, Connection);

			if (sender.QuerySelect.Where.Count > 0)
			{
				foreach (Where field in sender.QuerySelect.Where)
					nCommand.Parameters.Add(new NpgsqlParameter(field.Name, field.Value));
			}

			NpgsqlDataReader reader = nCommand.ExecuteReader();
			while (reader.Read())
			{
				DirectoryPointer elementPointer = new DirectoryPointer();

				List<FieldValue> fields = new List<FieldValue>();

				foreach (KeyValuePair<string, string> field in sender.QuerySelect.Field)
				{
					fields.Add(new FieldValue(field.Key, reader[field.Key]));
				}

				elementPointer.Init(new UnigueID(reader["uid"].ToString()), fields);

				listDirectoryPointer.Add(elementPointer);
			}
			reader.Close();

			return listDirectoryPointer;
		}
	}
}

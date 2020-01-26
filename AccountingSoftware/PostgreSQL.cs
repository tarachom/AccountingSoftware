using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Npgsql;

namespace AccountingSoftware
{
	public class PostgreSQL: IDataBase
	{
		public PostgreSQL()
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

		public void SelectDirectory(DirectorySelect sender, List<DirectoryPointer> listDirectoryPointer)
		{
			string query = sender.QuerySelect.Construct();
			Console.WriteLine(query);

			NpgsqlCommand nCommand = new NpgsqlCommand(query, Connection);

			if (sender.QuerySelect.Where.Count > 0)
			{
				foreach (Where field in sender.QuerySelect.Where)
					nCommand.Parameters.Add(new NpgsqlParameter(field.Name, field.Value));
			}

			NpgsqlDataReader reader = nCommand.ExecuteReader();
			while (reader.Read())
			{
				List<FieldValue> fields = null;

				if (sender.QuerySelect.Field.Count > 0)
				{
					fields = new List<FieldValue>();

					foreach (KeyValuePair<string, string> field in sender.QuerySelect.Field)
					{
						fields.Add(new FieldValue(field.Key, reader[field.Key]));
					}
				}

				DirectoryPointer elementPointer = new DirectoryPointer();
				elementPointer.Init(new UnigueID(reader["uid"].ToString()), fields);

				listDirectoryPointer.Add(elementPointer);
			}
			reader.Close();
		}
	}
}

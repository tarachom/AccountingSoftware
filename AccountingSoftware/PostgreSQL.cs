using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

		public void SelectDirectoryObject(DirectoryObject sender, Dictionary<string, object> fields)
		{
			string query = "SELECT uid ";

			foreach (string field in sender.FieldList)
			{
				query += ", " + field;
			}

			query += " FROM " + sender.Table + " WHERE uid = @uid";

			//Console.WriteLine(query);

			NpgsqlCommand nCommand = new NpgsqlCommand(query, Connection);
			nCommand.Parameters.Add(new NpgsqlParameter("uid", sender.UnigueID.UGuid));

			NpgsqlDataReader reader = nCommand.ExecuteReader();
			while (reader.Read())
			{
				foreach (string field in sender.FieldList)
				{
					fields[field] = reader[field];
				}
			}
			reader.Close();
		}

		public void InsertDirectoryObject(DirectoryObject sender, Dictionary<string, object> fields)
		{
			string query_field = "uid";
			string query_values = "@uid";

			foreach (string field in sender.FieldList)
			{
				query_field += ", " + field; 
				query_values += ", @" + field;
			}

			string query = "INSERT INTO " + sender.Table + "(" + query_field + ") VALUES(" + query_values + ")";

			NpgsqlCommand nCommand = new NpgsqlCommand(query, Connection);
			nCommand.Parameters.Add(new NpgsqlParameter("uid", sender.UnigueID.UGuid));

			//Console.WriteLine(Guid.Parse(sender.UID.UID));

			foreach (string field in sender.FieldList)
			{
				nCommand.Parameters.Add(new NpgsqlParameter(field, fields[field]));

				//Console.WriteLine(field + " = " + fields[field]);
			}

			//Console.WriteLine(query);

			nCommand.ExecuteNonQuery();
		}

		public void SaveDirectoryObject(DirectoryObject sender, Dictionary<string, object> fields)
		{
			string query = "UPDATE " + sender.Table + " SET ";

			int count = 0;

			foreach (string field in sender.FieldList)
			{
				if (count > 0) query += ", ";
				query += field + " = @" + field;

				count++;
			}

			query += " WHERE uid = @uid";

			NpgsqlCommand nCommand = new NpgsqlCommand(query, Connection);
			nCommand.Parameters.Add(new NpgsqlParameter("uid", sender.UnigueID.UGuid));

			foreach (string field in sender.FieldList)
			{
				nCommand.Parameters.Add(new NpgsqlParameter(field, fields[field]));
			}

			//Console.WriteLine(query);

			nCommand.ExecuteNonQuery();
		}

		public void SelectDirectoryPointer(DirectorySelect sender, List<DirectoryPointer> listDirectoryPointer)
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
				Dictionary<string, object> fields = null;

				if (sender.QuerySelect.Field.Count > 0)
				{
					fields = new Dictionary<string, object>();

					foreach (string field in sender.QuerySelect.Field)
						fields.Add(field, reader[field]);
				}

				DirectoryPointer elementPointer = new DirectoryPointer();
				elementPointer.Init(new UnigueID((Guid)reader["uid"], ""), fields);

				listDirectoryPointer.Add(elementPointer);
			}
			reader.Close();
		}
	}
}

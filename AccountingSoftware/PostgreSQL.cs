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

		private NpgsqlConnection Connection { get; set; }

		private NpgsqlTransaction Transaction { get; set; }

		public void Open(string connectionString)
		{
			Connection = new NpgsqlConnection(connectionString);
			Connection.Open();
		}

		public void Close()
		{
			Connection.Close();
		}

		public void BeginTransaction()
		{
			Transaction = Connection.BeginTransaction();
		}

		public void CommitTransaction()
		{
			Transaction.Commit();
		}

		public void RollbackTransaction()
		{
			Transaction.Rollback();
		}

		public void InsertDirectoryObject(DirectoryObject directoryObject, string table, string[] fieldArray, Dictionary<string, object> fieldValue)
		{
			string query_field = "uid";
			string query_values = "@uid";

			foreach (string field in fieldArray)
			{
				query_field += ", " + field; 
				query_values += ", @" + field;
			}

			string query = "INSERT INTO " + table + " (" + query_field + ") VALUES (" + query_values + ")";

			NpgsqlCommand nCommand = new NpgsqlCommand(query, Connection);
			nCommand.Parameters.Add(new NpgsqlParameter("uid", directoryObject.UnigueID.UGuid));

			//Console.WriteLine(Guid.Parse(sender.UID.UID));

			foreach (string field in fieldArray)
			{
				nCommand.Parameters.Add(new NpgsqlParameter(field, fieldValue[field]));

				//Console.WriteLine(field + " = " + fields[field]);
			}

			//Console.WriteLine(query);

			nCommand.ExecuteNonQuery();
		}

		public void SaveDirectoryObject(DirectoryObject directoryObject, string table, string[] fieldArray, Dictionary<string, object> fieldValue)
		{
			string query = "UPDATE " + table + " SET ";

			int count = 0;

			foreach (string field in fieldArray)
			{
				if (count > 0) query += ", ";
				query += field + " = @" + field;

				count++;
			}

			query += " WHERE uid = @uid";

			NpgsqlCommand nCommand = new NpgsqlCommand(query, Connection);
			nCommand.Parameters.Add(new NpgsqlParameter("uid", directoryObject.UnigueID.UGuid));

			foreach (string field in fieldArray)
			{
				nCommand.Parameters.Add(new NpgsqlParameter(field, fieldValue[field]));
			}

			//Console.WriteLine(query);

			nCommand.ExecuteNonQuery();
		}

		public bool SelectDirectoryObject(DirectoryObject directoryObject, UnigueID unigueID, string table, string[] fieldArray, Dictionary<string, object> fieldValue)
		{
			string query = "SELECT uid ";

			foreach (string field in fieldArray)
			{
				query += ", " + field;
			}

			query += " FROM " + table + " WHERE uid = @uid";

			//Console.WriteLine(query);

			NpgsqlCommand nCommand = new NpgsqlCommand(query, Connection);
			nCommand.Parameters.Add(new NpgsqlParameter("uid", unigueID.UGuid));

			bool isSelectDirectoryObject = false;

			NpgsqlDataReader reader = nCommand.ExecuteReader();
			while (reader.Read())
			{
				foreach (string field in fieldArray)
				{
					fieldValue[field] = reader[field];
				}

				isSelectDirectoryObject = true;
			}
			reader.Close();

			return isSelectDirectoryObject;
		}

		public void DeleteDirectoryObject(UnigueID unigueID, string table)
		{
			string query = "DELETE FROM " + table + " WHERE uid = @uid";

			NpgsqlCommand nCommand = new NpgsqlCommand(query, Connection);
			nCommand.Parameters.Add(new NpgsqlParameter("uid", unigueID.UGuid));

			nCommand.ExecuteNonQuery();
		}

		public void SelectDirectoryPointer(DirectorySelect select, List<DirectoryPointer> listDirectoryPointer)
		{
			string query = select.QuerySelect.Construct();
			//Console.WriteLine(query);

			NpgsqlCommand nCommand = new NpgsqlCommand(query, Connection);

			if (select.QuerySelect.Where.Count > 0)
			{
				foreach (Where field in select.QuerySelect.Where)
					nCommand.Parameters.Add(new NpgsqlParameter(field.Name, field.Value));
			}

			NpgsqlDataReader reader = nCommand.ExecuteReader();
			while (reader.Read())
			{
				Dictionary<string, object> fields = null;

				if (select.QuerySelect.Field.Count > 0)
				{
					fields = new Dictionary<string, object>();

					foreach (string field in select.QuerySelect.Field)
						fields.Add(field, reader[field]);
				}

				DirectoryPointer elementPointer = new DirectoryPointer();
				elementPointer.Init(new UnigueID((Guid)reader["uid"], ""), fields);

				listDirectoryPointer.Add(elementPointer);
			}
			reader.Close();
		}

		public void SelectDirectoryTablePartRecords(UnigueID ownerUnigueID, string table, string[] fieldArray, List<Dictionary<string, object>> fieldValueList)
		{
			bool is_first = true;
			
			string query = "SELECT ";

			foreach (string field in fieldArray)
			{
				if (!is_first) query += ", "; else is_first = false;
				query += field;
			}

			query += " FROM " + table + " WHERE owner = @owner";

			//Console.WriteLine(query);

			NpgsqlCommand nCommand = new NpgsqlCommand(query, Connection);
			nCommand.Parameters.Add(new NpgsqlParameter("owner", ownerUnigueID.UGuid));

			NpgsqlDataReader reader = nCommand.ExecuteReader();
			while (reader.Read())
			{
				Dictionary<string, object> fieldValue = new Dictionary<string, object>();
				fieldValueList.Add(fieldValue);

				foreach (string field in fieldArray)
				{
					fieldValue.Add(field, reader[field]);
				}
			}
			reader.Close();
		}

		public void InsertDirectoryTablePartRecords(UnigueID ownerUnigueID, string table, string[] fieldArray, Dictionary<string, object> fieldValue)
		{
			string query_field = "owner";
			string query_values = "@owner";

			foreach (string field in fieldArray)
			{
				query_field += ", " + field;
				query_values += ", @" + field;
			}

			string query = "INSERT INTO " + table + " (" + query_field + ") VALUES (" + query_values + ")";

			//Console.WriteLine(query);

			NpgsqlCommand nCommand = new NpgsqlCommand(query, Connection);
			nCommand.Parameters.Add(new NpgsqlParameter("owner", ownerUnigueID.UGuid));

			foreach (string field in fieldArray)
			{
				nCommand.Parameters.Add(new NpgsqlParameter(field, fieldValue[field]));
			}

			nCommand.ExecuteNonQuery();
		}

		public void DeleteDirectoryTablePartRecords(UnigueID ownerUnigueID, string table)
		{
			string query = "DELETE FROM " + table + " WHERE owner = @owner";

			NpgsqlCommand nCommand = new NpgsqlCommand(query, Connection);
			nCommand.Parameters.Add(new NpgsqlParameter("owner", ownerUnigueID.UGuid));

			nCommand.ExecuteNonQuery();
		}

		public void Test()
		{
			string query = "select table_name, column_name, data_type, udt_name from information_schema.columns where table_schema = 'public'";

			NpgsqlCommand nCommand = new NpgsqlCommand(query, Connection);

			NpgsqlDataReader reader = nCommand.ExecuteReader();
			while (reader.Read())
			{
				Console.WriteLine(reader["table_name"] + " " + 
					              reader["column_name"] + " " + 
								  reader["data_type"] + " " + 
								  reader["udt_name"]);
			}
			reader.Close();
		}
	}
}

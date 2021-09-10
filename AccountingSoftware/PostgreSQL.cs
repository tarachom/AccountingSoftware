/*
Copyright (C) 2019-2020 TARAKHOMYN YURIY IVANOVYCH
All rights reserved.

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

/*
Автор:    Тарахомин Юрій Іванович
Адреса:   Україна, м. Львів
Сайт:     accounting.org.ua
*/

using System;
using System.Collections.Generic;
using Npgsql;
using NpgsqlTypes;

namespace AccountingSoftware
{
	public class PostgreSQL : IDataBase
	{
		public PostgreSQL() { }

		private NpgsqlConnection Connection { get; set; }

		public void Open(string connectionString)
		{
			Connection = new NpgsqlConnection(connectionString);
			Connection.Open();

			//Start();
		}

		public bool Open2(string Server, string UserId, string Password, int Port, string Database, out Exception exception)
		{
			exception = null;

			string conString = $"Server={Server};User Id={UserId};Password={Password};Port={Port};Database={Database};";
			Console.WriteLine(conString);

			Connection = new NpgsqlConnection(conString);

			try
			{
				Connection.Open();

				//Start();

				return true;
			}
			catch (Exception e)
			{
				exception = e;
				return false;
			}
		}

		public bool TryConnectToServer(string Server, string UserId, string Password, int Port, string Database, out Exception exception)
		{
			string conString = $"Server={Server};User Id={UserId};Password={Password};Port={Port};Database={Database};";
			//Console.WriteLine(conString);

			Connection = new NpgsqlConnection(conString);

			exception = null;

			try
			{
				Connection.Open();
				//Connection.Close();

				return true;
			}
			catch (Exception e)
			{
				exception = e;
				return false;
			}
		}

		public bool CreateDatabaseIfNotExist(string Server, string UserId, string Password, int Port, string Database, out Exception exception, out bool IsExistsDatabase)
		{
			exception = null;
			IsExistsDatabase = false;

			string conString = $"Server={Server};User Id={UserId};Password={Password};Port={Port};Database={Database};";
			//Console.WriteLine(conString);

			Connection = new NpgsqlConnection(conString);

			try
			{
				Connection.Open();
			}
			catch (Exception e)
			{
				exception = e;
				return false;
			}

			string sql = "SELECT EXISTS(" +
				"SELECT datname FROM pg_catalog.pg_database WHERE lower(datname) = lower('" + Database + "'));";

			NpgsqlCommand nCommand = new NpgsqlCommand(sql, Connection);

			bool resultSql = false;

			try
			{
				IsExistsDatabase = resultSql = (bool)nCommand.ExecuteScalar();
			}
			catch (Exception e)
			{
				exception = e;
				return false;
			}

			if (!resultSql)
			{
				sql = "CREATE DATABASE " + Database;
				nCommand = new NpgsqlCommand(sql, Connection);

				try
				{
					nCommand.ExecuteNonQuery();
				}
				catch (Exception e)
				{
					exception = e;
					return false;
				}
			}

			return true;
		}

		/*
		private void Start()
		{
			//List<string> StartSQL = new List<string>();

			string query = "SELECT 'Exist' FROM pg_type WHERE typname = 'uuid_and_string'";
			NpgsqlCommand nCommand = new NpgsqlCommand(query, Connection);
			object result = nCommand.ExecuteScalar();

			if (!(result != null && result.ToString() == "Exist"))
				ExecuteSQL("CREATE TYPE public.uuid_and_string AS(uuid uuid, string text)");

			//foreach (string sqlQuery in StartSQL)
			//	ExecuteSQL(sqlQuery);

			Connection.MapComposite<uuid_and_string>("uuid_and_string");
		}
		*/
		public void Close()
		{
			try
			{
				Connection.Close();
			}
			catch { }
		}

		#region UserType
		/*
		public class uuid_and_string
		{
			public uuid_and_string() { }

			public uuid_and_string(Guid _uuid, string _text)
			{
				uuid = _uuid;
				text = _text;
			}

			[PgName("uuid")]
			public Guid uuid { get; set; }

			[PgName("string")]
			public string text { get; set; }

			public override string ToString()
			{
				return "('" + uuid.ToString() + "', '" + text + "')";
			}
		}
		*/
		#endregion

		#region Transaction

		private NpgsqlTransaction Transaction { get; set; }

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

		#endregion

		#region Constants

		public bool SelectAllConstants(string table, string[] fieldArray, Dictionary<string, object> fieldValue)
		{
			string query = "SELECT ";
			bool is_first = true;

			foreach (string field in fieldArray)
			{
				if (!is_first)
					query += ", ";
				else
					is_first = false;

				query += field;
			}

			query += " FROM " + table + " WHERE uid = @uid";
			//Console.WriteLine(query);

			NpgsqlCommand nCommand = new NpgsqlCommand(query, Connection);
			nCommand.Parameters.Add(new NpgsqlParameter("uid", Guid.Empty));

			bool isSelect = false;

			NpgsqlDataReader reader = nCommand.ExecuteReader();
			if (reader.Read())
			{
				foreach (string field in fieldArray)
					fieldValue.Add(field, reader[field]);

				isSelect = true;
			}
			reader.Close();

			return isSelect;
		}

		public bool SelectConstants(string table, string field, Dictionary<string, object> fieldValue)
		{
			string query = "SELECT " + field + " FROM " + table + " WHERE uid = @uid";

			NpgsqlCommand nCommand = new NpgsqlCommand(query, Connection);
			nCommand.Parameters.Add(new NpgsqlParameter("uid", Guid.Empty));

			bool isSelect = false;

			NpgsqlDataReader reader = nCommand.ExecuteReader();
			if (reader.Read())
			{
				fieldValue.Add(field, reader[field]);
				isSelect = true;
			}
			reader.Close();

			return isSelect;
		}

		public void SaveConstants(string table, string field, object fieldValue)
		{
			string query = "INSERT INTO " + table + " (uid, " + field + ") VALUES (@uid, @" + field + ") " +
						   " ON CONFLICT (uid) DO UPDATE SET " + field + " = @" + field;

			NpgsqlCommand nCommand = new NpgsqlCommand(query, Connection);
			nCommand.Parameters.Add(new NpgsqlParameter("uid", Guid.Empty));
			nCommand.Parameters.Add(new NpgsqlParameter(field, fieldValue));

			nCommand.ExecuteNonQuery();
		}

		public void SelectConstantsTablePartRecords(string table, string[] fieldArray, List<Dictionary<string, object>> fieldValueList)
		{
			string query = "SELECT uid";

			foreach (string field in fieldArray)
				query += ", " + field;

			query += " FROM " + table;
			//Console.WriteLine(query);

			NpgsqlCommand nCommand = new NpgsqlCommand(query, Connection);

			NpgsqlDataReader reader = nCommand.ExecuteReader();
			while (reader.Read())
			{
				Dictionary<string, object> fieldValue = new Dictionary<string, object>();
				fieldValueList.Add(fieldValue);

				fieldValue.Add("uid", reader["uid"]);

				foreach (string field in fieldArray)
					fieldValue.Add(field, reader[field]);
			}
			reader.Close();
		}

		public void InsertConstantsTablePartRecords(Guid UID, string table, string[] fieldArray, Dictionary<string, object> fieldValue)
		{
			string query_field = "uid";
			string query_values = "@uid";

			foreach (string field in fieldArray)
			{
				query_field += ", " + field;
				query_values += ", @" + field;
			}

			string query = "INSERT INTO " + table + " (" + query_field + ") VALUES (" + query_values + ")";
			//Console.WriteLine(query);

			NpgsqlCommand nCommand = new NpgsqlCommand(query, Connection);
			nCommand.Parameters.Add(new NpgsqlParameter("uid", UID));

			foreach (string field in fieldArray)
			{
				nCommand.Parameters.Add(new NpgsqlParameter(field, fieldValue[field]));
			}

			nCommand.ExecuteNonQuery();
		}

		public void DeleteConstantsTablePartRecords(string table)
		{
			string query = "DELETE FROM " + table;

			NpgsqlCommand nCommand = new NpgsqlCommand(query, Connection);

			nCommand.ExecuteNonQuery();
		}

		#endregion

		#region Directory

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

			Console.WriteLine(query);

			nCommand.ExecuteNonQuery();
		}

		public void UpdateDirectoryObject(DirectoryObject directoryObject, string table, string[] fieldArray, Dictionary<string, object> fieldValue)
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

			Console.WriteLine(query);

			nCommand.ExecuteNonQuery();
		}

		public bool SelectDirectoryObject(DirectoryObject directoryObject/*??*/, UnigueID unigueID, string table, string[] fieldArray, Dictionary<string, object> fieldValue)
		{
			string query = "SELECT uid ";

			foreach (string field in fieldArray)
			{
				query += ", " + field;
			}

			query += " FROM " + table + " WHERE uid = @uid";

			Console.WriteLine(query);

			NpgsqlCommand nCommand = new NpgsqlCommand(query, Connection);
			nCommand.Parameters.Add(new NpgsqlParameter("uid", unigueID.UGuid));

			NpgsqlDataReader reader = nCommand.ExecuteReader();

			bool isSelectDirectoryObject = reader.HasRows;

			while (reader.Read())
			{
				foreach (string field in fieldArray)
				{
					fieldValue[field] = reader[field];
				}
			}
			reader.Close();

			return isSelectDirectoryObject;
		}

		public void DeleteDirectoryObject(UnigueID unigueID, string table)
		{
			string query = "DELETE FROM " + table + " WHERE uid = @uid";
			Console.WriteLine(query);

			NpgsqlCommand nCommand = new NpgsqlCommand(query, Connection);
			nCommand.Parameters.Add(new NpgsqlParameter("uid", unigueID.UGuid));

			nCommand.ExecuteNonQuery();
		}

		public void SelectDirectoryPointers(Query QuerySelect, List<DirectoryPointer> listDirectoryPointer)
		{
			string query = QuerySelect.Construct();
			Console.WriteLine(query);

			NpgsqlCommand nCommand = new NpgsqlCommand(query, Connection);

			foreach (Where field in QuerySelect.Where)
				nCommand.Parameters.Add(new NpgsqlParameter(field.Alias, field.Value));

			NpgsqlDataReader reader = nCommand.ExecuteReader();
			while (reader.Read())
			{
				Dictionary<string, object> fields = null;

				if (QuerySelect.Field.Count > 0 || QuerySelect.FieldAndAlias.Count > 0)
				{
					fields = new Dictionary<string, object>();

					foreach (string field in QuerySelect.Field)
						fields.Add(field, reader[field]);

					foreach(KeyValuePair<string,string> field in QuerySelect.FieldAndAlias)
						fields.Add(field.Value, reader[field.Value]);
				}

				DirectoryPointer elementPointer = new DirectoryPointer();
				elementPointer.Init(new UnigueID((Guid)reader["uid"]), fields); //!!! Подумати як зробити Init protect

				listDirectoryPointer.Add(elementPointer);
			}
			reader.Close();
		}

		public string GetViewDirectoryPointers(Query QuerySelect, Guid uid, string field)
		{
			QuerySelect.Field.Add(field);
			QuerySelect.Where.Add(new Where("uid", Comparison.EQ, uid));

			string query = QuerySelect.Construct();
			Console.WriteLine(query);

			NpgsqlCommand nCommand = new NpgsqlCommand(query, Connection);
			nCommand.Parameters.Add(new NpgsqlParameter("uid", uid));

			Console.WriteLine("uid = " + uid.ToString());

			string value = "";

			NpgsqlDataReader reader = nCommand.ExecuteReader();
			if (reader.Read())
			{
				value = reader[field].ToString();
				Console.WriteLine(field);
				Console.WriteLine(value);
			}
			reader.Close();

			return value;
		}

		public bool FindDirectoryPointer(Query QuerySelect, ref DirectoryPointer directoryPointer)
		{
			QuerySelect.Limit = 1;

			string query = QuerySelect.Construct();
			Console.WriteLine(query);

			NpgsqlCommand nCommand = new NpgsqlCommand(query, Connection);

			if (QuerySelect.Where.Count > 0)
			{
				foreach (Where field in QuerySelect.Where)
					nCommand.Parameters.Add(new NpgsqlParameter(field.Alias, field.Value));
			}

			bool isFind = false;

			NpgsqlDataReader reader = nCommand.ExecuteReader();
			if (reader.Read())
			{
				isFind = true;
				directoryPointer.Init(new UnigueID((Guid)reader["uid"], ""), null);
			}
			reader.Close();

			return isFind;
		}

		/// <summary>
        /// Вибирає значення полів по вказівнику для представлення
        /// </summary>
        /// <param name="QuerySelect">Запит</param>
        /// <param name="fieldPresentation">Поля які використовуються для представлення</param>
        /// <returns></returns>
		public string GetPresentation(Query QuerySelect, string[] fieldPresentation)
        {
			string query = QuerySelect.Construct();
			Console.WriteLine(query);

			NpgsqlCommand nCommand = new NpgsqlCommand(query, Connection);

			foreach (Where field in QuerySelect.Where)
				nCommand.Parameters.Add(new NpgsqlParameter(field.Alias, field.Value));

			string presentation = "";

			NpgsqlDataReader reader = nCommand.ExecuteReader();
			if (reader.Read())
			{
				for (int i = 0; i < fieldPresentation.Length; i++)
					presentation += (i > 0 ? "/" : "") + reader[fieldPresentation[i]].ToString();
			}
			reader.Close();

			return presentation;
		}

		public void DeleteDirectoryTempTable(DirectorySelect directorySelect)
		{
			if (directorySelect.QuerySelect.CreateTempTable == true &&
				directorySelect.QuerySelect.TempTable != "" &&
			 	directorySelect.QuerySelect.TempTable.Substring(0, 4) == "tmp_")
			{
				string query = "DROP TABLE IF EXISTS " + directorySelect.QuerySelect.TempTable;
				Console.WriteLine(query);

				NpgsqlCommand nCommand = new NpgsqlCommand(query, Connection);
				nCommand.ExecuteNonQuery();
			}
		}

		public void SelectDirectoryTablePartRecords(UnigueID ownerUnigueID, string table, string[] fieldArray, List<Dictionary<string, object>> fieldValueList)
		{
			string query = "SELECT uid ";

			foreach (string field in fieldArray)
				query += ", " + field;

			query += " FROM " + table + " WHERE owner = @owner";

			//Console.WriteLine(query);

			NpgsqlCommand nCommand = new NpgsqlCommand(query, Connection);
			nCommand.Parameters.Add(new NpgsqlParameter("owner", ownerUnigueID.UGuid));

			NpgsqlDataReader reader = nCommand.ExecuteReader();
			while (reader.Read())
			{
				Dictionary<string, object> fieldValue = new Dictionary<string, object>();
				fieldValueList.Add(fieldValue);

				fieldValue.Add("uid", reader["uid"]);

				foreach (string field in fieldArray)
					fieldValue.Add(field, reader[field]);
			}
			reader.Close();
		}

		public void InsertDirectoryTablePartRecords(Guid UID, UnigueID ownerUnigueID, string table, string[] fieldArray, Dictionary<string, object> fieldValue)
		{
			string query_field = "uid, owner";
			string query_values = "@uid, @owner";

			foreach (string field in fieldArray)
			{
				query_field += ", " + field;
				query_values += ", @" + field;
			}

			string query = "INSERT INTO " + table + " (" + query_field + ") VALUES (" + query_values + ")";

			//Console.WriteLine(query);

			NpgsqlCommand nCommand = new NpgsqlCommand(query, Connection);
			nCommand.Parameters.Add(new NpgsqlParameter("uid", UID));
			nCommand.Parameters.Add(new NpgsqlParameter("owner", ownerUnigueID.UGuid));

			foreach (string field in fieldArray)
				nCommand.Parameters.Add(new NpgsqlParameter(field, fieldValue[field]));

			nCommand.ExecuteNonQuery();
		}

		public void DeleteDirectoryTablePartRecords(UnigueID ownerUnigueID, string table)
		{
			string query = "DELETE FROM " + table + " WHERE owner = @owner";

			NpgsqlCommand nCommand = new NpgsqlCommand(query, Connection);
			nCommand.Parameters.Add(new NpgsqlParameter("owner", ownerUnigueID.UGuid));

			nCommand.ExecuteNonQuery();
		}

		public string SelectDirectoryView(DirectoryView directoryView)
		{
			string query = directoryView.QuerySelect.Construct();
			Console.WriteLine(query);

			NpgsqlCommand nCommand = new NpgsqlCommand(query, Connection);

			foreach (Where field in directoryView.QuerySelect.Where)
			{
				if (field.UsingSQLToValue == false)
				{
					nCommand.Parameters.Add(new NpgsqlParameter(field.Alias, field.Value));
					Console.WriteLine(field.Name + " = " + field.Value);
				}
			}

			if (directoryView.QuerySelect.CreateTempTable == true)
			{
				nCommand.ExecuteNonQuery();

				query = "SELECT * FROM " + directoryView.QuerySelect.TempTable;
				nCommand = new NpgsqlCommand(query, Connection);

				Console.WriteLine(query);
			}

			string xml = "<" + directoryView.Name + ">\n";

			NpgsqlDataReader reader = nCommand.ExecuteReader();
			while (reader.Read())
			{
				xml += "  <row>\n";
				xml += "    <uid>" + reader["uid"].ToString() + "</uid>\n";

				foreach (string field in directoryView.QuerySelect.Field)
				{
					xml += "    <" + directoryView.AliasRevers[field] + ">";

					switch (directoryView.AliasFieldType[field])
					{
						case "string":
							{
								xml += "<![CDATA[" + reader[field].ToString() + "]]>";
								break;
							}
						case "integer":
						case "numeric":
						case "boolean":
						case "date":
						case "datetime":
						case "time":
						case "pointer":
						case "empty_pointer":
						case "enum":
							{
								xml += reader[field].ToString();
								break;
							}
						case "string[]":
							{
								string[] mas = (string[])reader[field];
								foreach (string elem in mas) xml += "<e><![CDATA[" + elem + "]]></e>";
								break;
							}
						case "integer[]":
							{
								int[] mas = (int[])reader[field];
								foreach (int elem in mas) xml += "<e>" + elem.ToString() + "</e>";
								break;
							}
						case "numeric[]":
							{
								decimal[] mas = (decimal[])reader[field];
								foreach (decimal elem in mas) xml += "<e>" + elem.ToString() + "</e>";
								break;
							}
						default:
							{
								xml += "<![CDATA[" + reader[field].ToString() + "]]>";
								break;
							}
					}

					xml += "</" + directoryView.AliasRevers[field] + ">\n";
				}

				xml += "  </row>\n";
			}
			reader.Close();

			xml += "</" + directoryView.Name + ">\n";

			return xml;
		}

		public void DeleteDirectoryViewTempTable(DirectoryView directoryView)
		{
			if (directoryView.QuerySelect.CreateTempTable == true &&
				directoryView.QuerySelect.TempTable != "" &&
			 	directoryView.QuerySelect.TempTable.Substring(0, 4) == "tmp_")
			{
				string query = "DROP TABLE IF EXISTS " + directoryView.QuerySelect.TempTable;
				Console.WriteLine(query);

				NpgsqlCommand nCommand = new NpgsqlCommand(query, Connection);
				nCommand.ExecuteNonQueryAsync();
			}
		}

		#endregion

		#region Document

		public bool SelectDocumentObject(DocumentObject documentObject/*??*/, UnigueID unigueID, string table, string[] fieldArray, Dictionary<string, object> fieldValue)
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

			NpgsqlDataReader reader = nCommand.ExecuteReader();

			bool isSelectDocumentObject = reader.HasRows;

			while (reader.Read())
			{
				foreach (string field in fieldArray)
				{
					fieldValue[field] = reader[field];
				}
			}
			reader.Close();

			return isSelectDocumentObject;
		}

		public void InsertDocumentObject(DocumentObject documentObject, string table, string[] fieldArray, Dictionary<string, object> fieldValue)
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
			nCommand.Parameters.Add(new NpgsqlParameter("uid", documentObject.UnigueID.UGuid));

			foreach (string field in fieldArray)
			{
				nCommand.Parameters.Add(new NpgsqlParameter(field, fieldValue[field]));
			}

			//Console.WriteLine(query);
			nCommand.ExecuteNonQuery();
		}

		public void UpdateDocumentObject(DocumentObject documentObject, string table, string[] fieldArray, Dictionary<string, object> fieldValue)
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
			nCommand.Parameters.Add(new NpgsqlParameter("uid", documentObject.UnigueID.UGuid));

			foreach (string field in fieldArray)
			{
				nCommand.Parameters.Add(new NpgsqlParameter(field, fieldValue[field]));
			}

			//Console.WriteLine(query);

			nCommand.ExecuteNonQuery();
		}

		public void DeleteDocumentObject(UnigueID unigueID, string table)
		{
			string query = "DELETE FROM " + table + " WHERE uid = @uid";

			NpgsqlCommand nCommand = new NpgsqlCommand(query, Connection);
			nCommand.Parameters.Add(new NpgsqlParameter("uid", unigueID.UGuid));

			nCommand.ExecuteNonQuery();
		}

		public void SelectDocumentPointer(Query QuerySelect, List<DocumentPointer> listDocumentPointer)
		{
			string query = QuerySelect.Construct();
			Console.WriteLine(query);

			NpgsqlCommand nCommand = new NpgsqlCommand(query, Connection);

			foreach (Where field in QuerySelect.Where)
				nCommand.Parameters.Add(new NpgsqlParameter(field.Alias, field.Value));

			NpgsqlDataReader reader = nCommand.ExecuteReader();
			while (reader.Read())
			{
				Dictionary<string, object> fields = null;

				if (QuerySelect.Field.Count > 0 || QuerySelect.FieldAndAlias.Count > 0)
				{
					fields = new Dictionary<string, object>();

					foreach (string field in QuerySelect.Field)
						fields.Add(field, reader[field]);

					foreach (KeyValuePair<string, string> field in QuerySelect.FieldAndAlias)
						fields.Add(field.Value, reader[field.Value]);
				}

				DocumentPointer elementPointer = new DocumentPointer();
				elementPointer.Init(new UnigueID((Guid)reader["uid"], ""), fields);

				listDocumentPointer.Add(elementPointer);
			}
			reader.Close();
		}

		public void SelectDocumentTablePartRecords(UnigueID ownerUnigueID, string table, string[] fieldArray, List<Dictionary<string, object>> fieldValueList)
		{
			string query = "SELECT uid";

			foreach (string field in fieldArray)
			{
				query += ", " + field;
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

				fieldValue.Add("uid", reader["uid"]);

				foreach (string field in fieldArray)
				{
					fieldValue.Add(field, reader[field]);
				}
			}
			reader.Close();
		}

		public void SelectDocumentTablePartRecords(Query QuerySelect, List<Dictionary<string, object>> fieldValueList)
		{
			string query = QuerySelect.Construct();
			Console.WriteLine(query);

			NpgsqlCommand nCommand = new NpgsqlCommand(query, Connection);

			foreach (Where field in QuerySelect.Where)
				nCommand.Parameters.Add(new NpgsqlParameter(field.Alias, field.Value));

			NpgsqlDataReader reader = nCommand.ExecuteReader();
			while (reader.Read())
			{
				Dictionary<string, object> fieldValue = new Dictionary<string, object>();
				fieldValueList.Add(fieldValue);

				fieldValue.Add("uid", reader["uid"]);

				foreach (string field in QuerySelect.Field)
					fieldValue.Add(field, reader[field]);

				foreach (KeyValuePair<string, string> field in QuerySelect.FieldAndAlias)
					fieldValue.Add(field.Value, reader[field.Value]);
			}
			reader.Close();
		}

		public void InsertDocumentTablePartRecords(Guid UID, UnigueID ownerUnigueID, string table, string[] fieldArray, Dictionary<string, object> fieldValue)
		{
			string query_field = "uid, owner";
			string query_values = "@uid, @owner";

			foreach (string field in fieldArray)
			{
				query_field += ", " + field;
				query_values += ", @" + field;
			}

			string query = "INSERT INTO " + table + " (" + query_field + ") VALUES (" + query_values + ")";
			//Console.WriteLine(query);

			NpgsqlCommand nCommand = new NpgsqlCommand(query, Connection);
			nCommand.Parameters.Add(new NpgsqlParameter("uid", UID));
			nCommand.Parameters.Add(new NpgsqlParameter("owner", ownerUnigueID.UGuid));

			foreach (string field in fieldArray)
			{
				nCommand.Parameters.Add(new NpgsqlParameter(field, fieldValue[field]));
			}

			nCommand.ExecuteNonQuery();
		}

		public void DeleteDocumentTablePartRecords(UnigueID ownerUnigueID, string table)
		{
			string query = "DELETE FROM " + table + " WHERE owner = @owner";

			NpgsqlCommand nCommand = new NpgsqlCommand(query, Connection);
			nCommand.Parameters.Add(new NpgsqlParameter("owner", ownerUnigueID.UGuid));

			nCommand.ExecuteNonQuery();
		}

		#endregion

		#region RegistersInformation

		public void SelectRegisterInformationRecords(string table, string[] fieldArray, List<Where> Filter, List<Dictionary<string, object>> fieldValueList)
		{
			Query QuerySelect = new Query(table);

			foreach (string fieldItem in fieldArray)
				QuerySelect.Field.Add(fieldItem);

			QuerySelect.Where = Filter;

			string query = QuerySelect.Construct();

			NpgsqlCommand nCommand = new NpgsqlCommand(query, Connection);

			if (Filter.Count > 0)
			{
				foreach (Where ItemFilter in Filter)
					nCommand.Parameters.Add(new NpgsqlParameter(ItemFilter.Alias, ItemFilter.Value));
			}

			NpgsqlDataReader reader = nCommand.ExecuteReader();
			while (reader.Read())
			{
				Dictionary<string, object> fieldValue = new Dictionary<string, object>();
				fieldValueList.Add(fieldValue);

				fieldValue.Add("uid", reader["uid"]);

				foreach (string field in fieldArray)
				{
					fieldValue.Add(field, reader[field]);
				}
			}
			reader.Close();
		}

		public void InsertRegisterInformationRecords(Guid UID, string table, string[] fieldArray, Dictionary<string, object> fieldValue)
		{
			string query_field = "uid";
			string query_values = "@uid";

			foreach (string field in fieldArray)
			{
				query_field += ", " + field;
				query_values += ", @" + field;
			}

			string query = "INSERT INTO " + table + " (" + query_field + ") VALUES (" + query_values + ")";
			//Console.WriteLine(query);

			NpgsqlCommand nCommand = new NpgsqlCommand(query, Connection);
			nCommand.Parameters.Add(new NpgsqlParameter("uid", UID));

			foreach (string field in fieldArray)
			{
				nCommand.Parameters.Add(new NpgsqlParameter(field, fieldValue[field]));
			}

			nCommand.ExecuteNonQuery();
		}

		public void DeleteRegisterInformationRecords(string table, List<Where> Filter)
		{
			Query QuerySelect = new Query(table);
			QuerySelect.Where = Filter;

			string query = "DELETE FROM " + table + " WHERE uid IN (\n" + QuerySelect.Construct() + "\n)";

			NpgsqlCommand nCommand = new NpgsqlCommand(query, Connection);

			if (Filter.Count > 0)
			{
				foreach (Where ItemFilter in Filter)
					nCommand.Parameters.Add(new NpgsqlParameter(ItemFilter.Alias, ItemFilter.Value));
			}

			nCommand.ExecuteNonQuery();
		}

		#endregion

		#region RegistersAccumulation

		public void SelectRegisterAccumulationRecords(string table, string[] fieldArray, List<Where> Filter, List<Dictionary<string, object>> fieldValueList)
		{
			Query QuerySelect = new Query(table);

			//Прибуток true, Витрата false
			QuerySelect.Field.Add("period");
			QuerySelect.Field.Add("income");
			QuerySelect.Field.Add("owner");

			foreach (string fieldItem in fieldArray)
				QuerySelect.Field.Add(fieldItem);

			QuerySelect.Where = Filter;

			string query = QuerySelect.Construct();

			NpgsqlCommand nCommand = new NpgsqlCommand(query, Connection);

			if (Filter.Count > 0)
			{
				foreach (Where ItemFilter in Filter)
					nCommand.Parameters.Add(new NpgsqlParameter(ItemFilter.Alias, ItemFilter.Value));
			}

			NpgsqlDataReader reader = nCommand.ExecuteReader();
			while (reader.Read())
			{
				Dictionary<string, object> fieldValue = new Dictionary<string, object>();
				fieldValueList.Add(fieldValue);

				fieldValue.Add("uid", reader["uid"]);
				fieldValue.Add("period", reader["period"]);
				fieldValue.Add("income", reader["income"]);
				fieldValue.Add("owner", reader["owner"]);

				foreach (string field in fieldArray)
				{
					fieldValue.Add(field, reader[field]);
				}
			}
			reader.Close();
		}

		public void InsertRegisterAccumulationRecords(Guid UID, string table, DateTime period, bool income, Guid owner, string[] fieldArray, Dictionary<string, object> fieldValue)
		{
			string query_field = "uid, period, income, owner";
			string query_values = "@uid, @period, @income, @owner";

			foreach (string field in fieldArray)
			{
				query_field += ", " + field;
				query_values += ", @" + field;
			}

			string query = "INSERT INTO " + table + " (" + query_field + ") VALUES (" + query_values + ")";
			//Console.WriteLine(query);

			NpgsqlCommand nCommand = new NpgsqlCommand(query, Connection);
			nCommand.Parameters.Add(new NpgsqlParameter("uid", UID));
			nCommand.Parameters.Add(new NpgsqlParameter("period", period));
			nCommand.Parameters.Add(new NpgsqlParameter("income", income));
			nCommand.Parameters.Add(new NpgsqlParameter("owner", owner));

			foreach (string field in fieldArray)
			{
				nCommand.Parameters.Add(new NpgsqlParameter(field, fieldValue[field]));
			}

			nCommand.ExecuteNonQuery();
		}

		public void DeleteRegisterAccumulationRecords(string table, Guid owner)
		{
			string query = "DELETE FROM " + table + " WHERE owner = @owner";

			NpgsqlCommand nCommand = new NpgsqlCommand(query, Connection);
			nCommand.Parameters.Add(new NpgsqlParameter("owner", owner));

			nCommand.ExecuteNonQuery();
		}

		#endregion

		#region InformationShema

		public bool IfExistsTable(string tableName)
		{
			string query = "SELECT table_name " +
						   "FROM information_schema.tables " +
						   "WHERE table_schema = 'public' AND table_type = 'BASE TABLE' AND table_name = @table_name";

			NpgsqlCommand nCommand = new NpgsqlCommand(query, Connection);
			nCommand.Parameters.Add(new NpgsqlParameter("table_name", tableName));

			NpgsqlDataReader reader = nCommand.ExecuteReader();

			bool ifExists = reader.HasRows;

			reader.Close();

			return ifExists;
		}

		public bool IfExistsColumn(string tableName, string columnName)
		{
			string query = "SELECT column_name " +
						   "FROM information_schema.columns " +
						   "WHERE table_schema = 'public' AND table_name = @table_name AND column_name = @column_name";

			NpgsqlCommand nCommand = new NpgsqlCommand(query, Connection);
			nCommand.Parameters.Add(new NpgsqlParameter("table_name", tableName));
			nCommand.Parameters.Add(new NpgsqlParameter("column_name", columnName));

			NpgsqlDataReader reader = nCommand.ExecuteReader();

			bool ifExists = reader.HasRows;

			reader.Close();

			return ifExists;
		}

		public ConfigurationInformationSchema SelectInformationSchema()
		{
			ConfigurationInformationSchema informationSchema = new ConfigurationInformationSchema();

			string query = "SELECT table_name, column_name, data_type, udt_name " +
						   "FROM information_schema.columns " +
						   "WHERE table_schema = 'public'";

			NpgsqlCommand nCommand = new NpgsqlCommand(query, Connection);

			NpgsqlDataReader reader = nCommand.ExecuteReader();
			while (reader.Read())
			{
				informationSchema.Append(
					reader["table_name"].ToString().ToLower(),
					reader["column_name"].ToString().ToLower(),
					reader["data_type"].ToString(),
					reader["udt_name"].ToString());
			}
			reader.Close();

			return informationSchema;
		}

		public int ExecuteSQL(string SqlQuery)
		{
			NpgsqlCommand nCommand = new NpgsqlCommand(SqlQuery, Connection);
			return nCommand.ExecuteNonQuery();
		}

		#endregion

		public int InsertSQL(string table, Dictionary<string, object> paramQuery)
        {
			string query_field = "";
			string query_values = "";

			foreach (string field in paramQuery.Keys)
			{
				query_field += (String.IsNullOrEmpty(query_field) ? "" : ", ") + field;
				query_values += (String.IsNullOrEmpty(query_values) ? "" : ", ") + "@" + field;
			}

			string insertQuery = "INSERT INTO " + table + " (" + query_field + ") VALUES (" + query_values + ")"; ;

			NpgsqlCommand Command = new NpgsqlCommand(insertQuery, Connection);

			if (paramQuery != null)
				foreach (KeyValuePair<string, object> param in paramQuery)
					Command.Parameters.Add(new NpgsqlParameter(param.Key, param.Value));

			return Command.ExecuteNonQuery();
		}

		public void SelectRequest(string selectQuery, Dictionary<string, object> paramQuery, out string[] columnsName, out List<object[]> listRow)
		{
			NpgsqlCommand Command = new NpgsqlCommand(selectQuery, Connection);

			if (paramQuery != null)
				foreach (KeyValuePair<string, object> param in paramQuery)
					Command.Parameters.Add(new NpgsqlParameter(param.Key, param.Value));

			NpgsqlDataReader reader = Command.ExecuteReader();

			int columnsCount = reader.FieldCount;
			columnsName = new string[columnsCount];

			for (int n = 0; n < columnsCount; n++)
				columnsName[n] = reader.GetName(n);

			listRow = new List<object[]>();

			while (reader.Read())
			{
				object[] objRow = new object[columnsCount];

				for (int i = 0; i < columnsCount; i++)
					objRow[i] = reader[i];

				listRow.Add(objRow);
			}
			reader.Close();
		}

	}
}

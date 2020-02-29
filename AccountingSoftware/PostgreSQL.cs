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
Сайт:     find.org.ua
*/

using System;
using System.Collections.Generic;
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

			//Console.WriteLine(query);

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

			//Console.WriteLine(query);

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

			//Console.WriteLine(query);

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

			// ?? Можна одним інсертом записати всі рядки VALUES(...), VALUES (...), ...

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

		public string SelectDirectoryView(DirectoryView directoryView)
		{
			string query = directoryView.QuerySelect.Construct();
			Console.WriteLine(query);

			NpgsqlCommand nCommand = new NpgsqlCommand(query, Connection);

			foreach (Where field in directoryView.QuerySelect.Where)
			{
				if (field.UsingSQLToValue == false)
				{
					nCommand.Parameters.Add(new NpgsqlParameter(field.Name, field.Value));
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
								foreach (string elem in mas) xml += "<elem><![CDATA[" + elem + "]]></elem>";
								break;
							}
						case "integer[]":
							{
								int[] mas = (int[])reader[field];
								foreach (int elem in mas) xml += "<elem>" + elem.ToString() + "</elem>";
								break;
							}
						case "numeric[]":
							{
								decimal[] mas = (decimal[])reader[field];
								foreach (decimal elem in mas) xml += "<elem>" + elem.ToString() + "</elem>";
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

		public void SelectDocumentPointer(DocumentSelect select, List<DocumentPointer> listDocumentPointer)
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

				DocumentPointer elementPointer = new DocumentPointer();
				elementPointer.Init(new UnigueID((Guid)reader["uid"], ""), fields);

				listDocumentPointer.Add(elementPointer);
			}
			reader.Close();
		}

		public void SelectDocumentTablePartRecords(UnigueID ownerUnigueID, string table, string[] fieldArray, List<Dictionary<string, object>> fieldValueList)
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

		public void InsertDocumentTablePartRecords(UnigueID ownerUnigueID, string table, string[] fieldArray, Dictionary<string, object> fieldValue)
		{
			string query_field = "owner";
			string query_values = "@owner";

			foreach (string field in fieldArray)
			{
				query_field += ", " + field;
				query_values += ", @" + field;
			}

			string query = "INSERT INTO " + table + " (" + query_field + ") VALUES (" + query_values + ")";

			// ?? Можна одним інсертом записати всі рядки VALUES(...), VALUES (...), ...

			//Console.WriteLine(query);

			NpgsqlCommand nCommand = new NpgsqlCommand(query, Connection);
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

		public void DeleteConfigurationDirectory(ConfigurationDirectories configurationDirectory)
		{
			BeginTransaction();
			string baseQuery = "DROP TABLE IF EXISTS ";
			string SqlQuery = baseQuery + configurationDirectory.Table;

			NpgsqlCommand nCommand = new NpgsqlCommand(SqlQuery, Connection);
			nCommand.ExecuteNonQuery();

			foreach (KeyValuePair<string, ConfigurationObjectTablePart> configurationObjectTablePart in configurationDirectory.TabularParts)
			{
				SqlQuery = baseQuery + configurationObjectTablePart.Value.Table;

				nCommand = new NpgsqlCommand(SqlQuery, Connection);
				nCommand.ExecuteNonQuery();
			}

			CommitTransaction();
		}

		public int ExecuteSQL(string SqlQuery)
		{
			NpgsqlCommand nCommand = new NpgsqlCommand(SqlQuery, Connection);
			return nCommand.ExecuteNonQuery();
		}
	}
}

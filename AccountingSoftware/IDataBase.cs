using System;
using System.Collections.Generic;

namespace AccountingSoftware
{
	public interface IDataBase
	{
		void Open(string connectionString);
		void Close();

		void BeginTransaction();
		void CommitTransaction();
		void RollbackTransaction();

		#region Directory

		void InsertDirectoryObject(DirectoryObject directoryObject, string table, string[] fieldArray, Dictionary<string, object> fieldValue);
		void UpdateDirectoryObject(DirectoryObject directoryObject, string table, string[] fieldArray, Dictionary<string, object> fieldValue);
		bool SelectDirectoryObject(DirectoryObject directoryObject, UnigueID unigueID, string table, string[] fieldArray, Dictionary<string, object> fieldValue);
		void DeleteDirectoryObject(UnigueID unigueID, string table);

		void SelectDirectoryPointer(DirectorySelect select, List<DirectoryPointer> listDirectoryPointer);

		void SelectDirectoryTablePartRecords(UnigueID ownerUnigueID, string table, string[] fieldArray, List<Dictionary<string, object>> fieldValueList);
		void InsertDirectoryTablePartRecords(UnigueID ownerUnigueID, string table, string[] fieldArray, Dictionary<string, object> fieldValue);
		void DeleteDirectoryTablePartRecords(UnigueID ownerUnigueID, string table);

		string SelectDirectoryView(DirectoryView directoryView);

		#endregion

		#region Document

		void InsertDocumentObject(DocumentObject documentObject, string table, string[] fieldArray, Dictionary<string, object> fieldValue);
		void UpdateDocumentObject(DocumentObject documentObject, string table, string[] fieldArray, Dictionary<string, object> fieldValue);
		bool SelectDocumentObject(DocumentObject documentObject/*??*/, UnigueID unigueID, string table, string[] fieldArray, Dictionary<string, object> fieldValue);
		void DeleteDocumentObject(UnigueID unigueID, string table);

		#endregion



		void DeleteConfigurationDirectory(ConfigurationDirectories configurationDirectory);
		ConfigurationInformationSchema SelectInformationSchema();
		bool IfExistsTable(string tableName);
		bool IfExistsColumn(string tableName, string columnName);
		int ExecuteSQL(string SqlQuery);

		
		
	}
}

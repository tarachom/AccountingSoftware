﻿using System;
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

		void SelectDirectoryPointer(DirectorySelect select, List<DirectoryPointer> listDirectoryPointer);

		bool SelectDirectoryObject(DirectoryObject directoryObject, UnigueID unigueID, string table, string[] fieldArray, Dictionary<string, object> fieldValue);
		void SaveDirectoryObject(DirectoryObject directoryObject, string table, string[] fieldArray, Dictionary<string, object> fieldValue);
		void InsertDirectoryObject(DirectoryObject directoryObject, string table, string[] fieldArray, Dictionary<string, object> fieldValue);
		void DeleteDirectoryObject(UnigueID unigueID, string table);

		void SelectDirectoryTablePartRecords(UnigueID ownerUnigueID, string table, string[] fieldArray, List<Dictionary<string, object>> fieldValueList);
		void InsertDirectoryTablePartRecords(UnigueID ownerUnigueID, string table, string[] fieldArray, Dictionary<string, object> fieldValue);
		void DeleteDirectoryTablePartRecords(UnigueID ownerUnigueID, string table);

		ConfigurationInformationSchema SelectInformationSchema(string databaseName);
		int ExecuteSQL(string SqlQuery);

		string SelectDirectoryView(DirectoryView directoryView);
	}
}

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

namespace AccountingSoftware
{
	public interface IDataBase
	{
		void Open(string connectionString);
		void Close();


		bool TryConnectToServer(string Server, string UserId, string Password, int Port, out Exception exception);
		bool CreateDatabaseIfNotExist(string Server, string UserId, string Password, int Port, string Database, out Exception exception);

		void BeginTransaction();
		void CommitTransaction();
		void RollbackTransaction();

		#region Constants

		bool SelectAllConstants(string table, string[] fieldArray, Dictionary<string, object> fieldValue);
		bool SelectConstants(string table, string field, Dictionary<string, object> fieldValue);
		void SaveConstants(string table, string field, object fieldValue);

		void SelectConstantsTablePartRecords(string table, string[] fieldArray, List<Dictionary<string, object>> fieldValueList);
		void InsertConstantsTablePartRecords(Guid UID, string table, string[] fieldArray, Dictionary<string, object> fieldValue);
		void DeleteConstantsTablePartRecords(string table);

		#endregion

		#region Directory

		void InsertDirectoryObject(DirectoryObject directoryObject, string table, string[] fieldArray, Dictionary<string, object> fieldValue);
		void UpdateDirectoryObject(DirectoryObject directoryObject, string table, string[] fieldArray, Dictionary<string, object> fieldValue);
		bool SelectDirectoryObject(DirectoryObject directoryObject, UnigueID unigueID, string table, string[] fieldArray, Dictionary<string, object> fieldValue);
		void DeleteDirectoryObject(UnigueID unigueID, string table);

		void SelectDirectoryPointers(Query QuerySelect, List<DirectoryPointer> listDirectoryPointer);
		string GetViewDirectoryPointers(Query QuerySelect, Guid uid, string field);
		bool FindDirectoryPointer(Query QuerySelect, ref DirectoryPointer directoryPointer);

		void SelectDirectoryTablePartRecords(UnigueID ownerUnigueID, string table, string[] fieldArray, List<Dictionary<string, object>> fieldValueList);
		void InsertDirectoryTablePartRecords(Guid UID, UnigueID ownerUnigueID, string table, string[] fieldArray, Dictionary<string, object> fieldValue);
		void DeleteDirectoryTablePartRecords(UnigueID ownerUnigueID, string table);

		string SelectDirectoryView(DirectoryView directoryView);
		void DeleteDirectoryViewTempTable(DirectoryView directoryView);

		#endregion

		#region Document

		void InsertDocumentObject(DocumentObject documentObject, string table, string[] fieldArray, Dictionary<string, object> fieldValue);
		void UpdateDocumentObject(DocumentObject documentObject, string table, string[] fieldArray, Dictionary<string, object> fieldValue);
		bool SelectDocumentObject(DocumentObject documentObject/*??*/, UnigueID unigueID, string table, string[] fieldArray, Dictionary<string, object> fieldValue);
		void DeleteDocumentObject(UnigueID unigueID, string table);

		void SelectDocumentPointer(DocumentSelect select, List<DocumentPointer> listDocumentPointer);

		void SelectDocumentTablePartRecords(UnigueID ownerUnigueID, string table, string[] fieldArray, List<Dictionary<string, object>> fieldValueList);
		void InsertDocumentTablePartRecords(Guid UID, UnigueID ownerUnigueID, string table, string[] fieldArray, Dictionary<string, object> fieldValue);
		void DeleteDocumentTablePartRecords(UnigueID ownerUnigueID, string table);

		#endregion

		#region RegisterInformation

		void SelectRegisterInformationRecords(string table, string[] fieldArray, List<Where> Filter, List<Dictionary<string, object>> fieldValueList);
		void InsertRegisterInformationRecords(Guid UID, string table, string[] fieldArray, Dictionary<string, object> fieldValue);
		void DeleteRegisterInformationRecords(string table, List<Where> Filter);

		#endregion

		#region RegisterAccumulation

		void SelectRegisterAccumulationRecords(string table, string[] fieldArray, List<Where> Filter, List<Dictionary<string, object>> fieldValueList);
		void InsertRegisterAccumulationRecords(Guid UID, string table, string[] fieldArray, Dictionary<string, object> fieldValue);
		void DeleteRegisterAccumulationRecords(string table, List<Where> Filter);

		#endregion

		#region InformationShema

		ConfigurationInformationSchema SelectInformationSchema();
		bool IfExistsTable(string tableName);
		bool IfExistsColumn(string tableName, string columnName);
		int ExecuteSQL(string SqlQuery);

		#endregion

		string Test();
	}
}

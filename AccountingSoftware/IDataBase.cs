using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSoftware
{
	public interface IDataBase
	{
		void Open();

		void Close();

		string ConnectionString { get; set; }

		void SelectDirectoryPointer(DirectorySelect sender, List<DirectoryPointer> listDirectoryPointer);

		void SelectDirectoryObject(DirectoryObject directoryObject, string table, string[] fieldArray, Dictionary<string, object> fieldValue);
		void SaveDirectoryObject(DirectoryObject directoryObject, string table, string[] fieldArray, Dictionary<string, object> fieldValue);
		void InsertDirectoryObject(DirectoryObject directoryObject, string table, string[] fieldArray, Dictionary<string, object> fieldValue);
		void DeleteDirectoryObject(DirectoryObject directoryObject, string table);
	}
}

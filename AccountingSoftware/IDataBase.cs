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
		void SelectDirectoryObject(DirectoryObject sender, Dictionary<string, object> fields);
		void SaveDirectoryObject(DirectoryObject sender, Dictionary<string, object> fields);
		void InsertDirectoryObject(DirectoryObject sender, Dictionary<string, object> fields);
		void DeleteDirectoryObject(DirectoryObject sender);
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServerTestErlang.AccountingSoftware
{
	//Довідник Вказівник - Ссилка на елемент довідника 
	//
	public class DirectoryPointer
	{
		public DirectoryPointer()
		{
			
		}

		public void Init(UnigueID uid, List<KeyValuePair<string, object>> fields = null)
		{
			UID = uid;
			Fields = fields;
		}

		public UnigueID UID { get; private set; }

		/// <summary>
		/// Список додаткових полів та їх значень
		/// </summary>
		public List<KeyValuePair<string, object>> Fields { get; private set; }
	}

	//public interface IDirectoryPointer
	//{
	//	UnigueID UID { get; }

	//	List<KeyValuePair<string, object>> Fields { get; }

	//	DirectoryPointer NewEmpty();
	//}
}

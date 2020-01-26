using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSoftware
{
	//Довідник Вказівник - Ссилка на елемент довідника 
	//
	public class DirectoryPointer
	{
		public DirectoryPointer()
		{
			
		}

		public void Init(UnigueID uid, Dictionary<string, object> fields = null)
		{
			UID = uid;
			Fields = fields;
		}

		public UnigueID UID { get; private set; }

		public Dictionary<string, object> Fields { get; private set; }
	}
}

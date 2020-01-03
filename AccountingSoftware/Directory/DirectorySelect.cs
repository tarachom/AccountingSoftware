using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServerTestErlang.AccountingSoftware
{
	//Довідник Вибірка
	//
	public abstract class DirectorySelect
	{
		//Вибірка результат
		//protected List<KeyValuePair<string, object>> ResultSelect { get; private set; }

		//Які поля вибирати
		public Dictionary<string, string> FieldSelect { get; set; }

		//Умови
		public Dictionary<string, string> FieldWhere { get; set; } //? AND або OR між полями як задавати

		//Сортування
		public Dictionary<string, SelectOrder> FieldOrder { get; set; }

		//Обмеження вибірки
		public int Limit { get; set; }

		protected void AbstractSelect()
		{
			DirectoryPointer test = new DirectoryPointer();
			
			AbstractDirectoryPointers.Add(test);
		}

		protected List<DirectoryPointer> AbstractDirectoryPointers { get; }
	}

	public enum SelectOrder
	{
		ASC,
		DESC
	}
}
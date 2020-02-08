using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configurator
{
	class FieldType
	{
		public FieldType(string confTypeName, string viewTypeName)
		{
			ConfTypeName = confTypeName;
			ViewTypeName = viewTypeName;
		}

		public string ConfTypeName { get; set; }

		public string ViewTypeName { get; set; }

		public override string ToString()
		{
			return ViewTypeName;
		}

		public static List<FieldType> DefaultList()
		{
			List<FieldType> fieldTypes = new List<FieldType>();

			fieldTypes.Add(new FieldType("string", "Текст"));
			fieldTypes.Add(new FieldType("string[]", "Текст масив"));
			fieldTypes.Add(new FieldType("integer", "Ціле число"));
			fieldTypes.Add(new FieldType("integer[]", "Ціле число масив"));
			fieldTypes.Add(new FieldType("numeric", "Число з комою"));
			fieldTypes.Add(new FieldType("numeric[]", "Число з комою масив"));
			fieldTypes.Add(new FieldType("boolean", "Логічне значення"));
			fieldTypes.Add(new FieldType("date", "Дата"));
			fieldTypes.Add(new FieldType("datetime", "Дата та час"));
			fieldTypes.Add(new FieldType("time", "Час"));
			fieldTypes.Add(new FieldType("pointer", "Вказівник на елемент конфігурації"));

			return fieldTypes;
		}
	}
}

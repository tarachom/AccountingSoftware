using System;
using System.Collections.Generic;

namespace AccountingSoftware
{
	public class FieldType
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

			fieldTypes.Add(new FieldType("string",        "[ string ] - Текст"));
			fieldTypes.Add(new FieldType("integer",       "[ integer ] - Ціле число"));
			fieldTypes.Add(new FieldType("numeric",       "[ numeric ] - Число з комою"));
			fieldTypes.Add(new FieldType("boolean",       "[ boolean ] - Логічне значення"));
			fieldTypes.Add(new FieldType("date",          "[ date ] - Дата"));
			fieldTypes.Add(new FieldType("datetime",      "[ datetime ] - Дата та час"));
			fieldTypes.Add(new FieldType("time",          "[ time ] - Час"));
			fieldTypes.Add(new FieldType("pointer",       "[ pointer ] - Вказівник на елемент конфігурації"));
			fieldTypes.Add(new FieldType("empty_pointer", "[ empty_pointer ] - Пустий вказівник"));
			fieldTypes.Add(new FieldType("string[]",      "[ string[ .. ] ] - Текст масив"));
			fieldTypes.Add(new FieldType("integer[]",     "[ integer[ .. ] ] - Ціле число масив"));
			fieldTypes.Add(new FieldType("numeric[]",     "[ numeric[ .. ] ] - Число з комою масив"));

			return fieldTypes;
		}
	}
}

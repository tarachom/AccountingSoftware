/*
Copyright (C) 2019-2020 Tarakhomin Yuri Ivanovich
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
			fieldTypes.Add(new FieldType("enum",          "[ enum ] - Перелічення"));
			fieldTypes.Add(new FieldType("pointer",       "[ pointer ] - Вказівник на елемент конфігурації"));
			fieldTypes.Add(new FieldType("empty_pointer", "[ empty_pointer ] - Пустий вказівник"));
			fieldTypes.Add(new FieldType("string[]",      "[ string[ .. ] ] - Текст масив"));
			fieldTypes.Add(new FieldType("integer[]",     "[ integer[ .. ] ] - Ціле число масив"));
			fieldTypes.Add(new FieldType("numeric[]",     "[ numeric[ .. ] ] - Число з комою масив"));

			return fieldTypes;
		}
	}
}

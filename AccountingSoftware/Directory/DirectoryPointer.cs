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
	/// <summary>
	/// Довідник Вказівник
	/// </summary>
	public class DirectoryPointer
	{
		public DirectoryPointer()
		{
			UnigueID = new UnigueID(Guid.Empty);
		}

		public DirectoryPointer(Kernel kernel, string table) : this()
		{
			Table = table;
			Kernel = kernel;
		}

		public void Init(UnigueID uid, Dictionary<string, object> fields = null)
		{
			UnigueID = uid;
			Fields = fields;
		}

		private Kernel Kernel { get; set; }

		private string Table { get; set; }

		public UnigueID UnigueID { get; private set; }

		public Dictionary<string, object> Fields { get; private set; }

		public bool IsEmpty()
		{
			return (UnigueID.UGuid == Guid.Empty);
		}

		public Guid GetPointer()
		{
			return UnigueID.UGuid;
		}

		protected string GetView(Guid uid, string field)
		{
			return Kernel.DataBase.GetViewDirectoryPointers(new Query(Table), uid, field);
		}

		public override string ToString()
		{
			return UnigueID.UGuid.ToString();
		}
	}
}

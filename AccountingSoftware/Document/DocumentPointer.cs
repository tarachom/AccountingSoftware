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
Сайт:     accounting.org.ua
*/

using System;
using System.Collections.Generic;

namespace AccountingSoftware
{
	public class DocumentPointer
	{
		public DocumentPointer()
		{
			UnigueID = new UnigueID(Guid.Empty);
		}

		public DocumentPointer(Kernel kernel, string table) : this()
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

		protected string BasePresentation(string[] fieldPresentation)
		{
			if (!IsEmpty() && fieldPresentation.Length != 0)
			{
				Query query = new Query(Table);
				query.Field.AddRange(fieldPresentation);

				query.Where.Add(new Where("uid", Comparison.EQ, UnigueID.UGuid));

				string presentatio = Kernel.DataBase.GetDocumentPresentation(query, fieldPresentation);
				
				string[] presentatio_split = presentatio.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
				if (presentatio_split.Length == 2)
				{
					return $"№{presentatio_split[0]} від {DateTime.Parse(presentatio_split[1]).ToShortDateString()}";
				}
				else if (presentatio_split.Length > 0)
					return presentatio_split[0];
				else
					return "";
			}
			else
				return "";
		}

		public void Delete()
		{
			Kernel.DataBase.DeleteDocumentObject(UnigueID, Table);
		}
	}
}

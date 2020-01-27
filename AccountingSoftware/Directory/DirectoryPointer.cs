﻿using System;
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
			UnigueID = new UnigueID(Guid.Empty);
		}

		public DirectoryPointer(Kernel kernel, string table)
		{
			Table = table;
			Kernel = kernel;

			UnigueID = new UnigueID(Guid.Empty);
		}

		public void Init(UnigueID uid, Dictionary<string, object> fields = null)
		{
			UnigueID = uid;
			Fields = fields;
		}

		protected Kernel Kernel { get; private set; }

		protected string Table { get; private set; }		

		public UnigueID UnigueID { get; private set; }

		public Dictionary<string, object> Fields { get; private set; }
	}
}

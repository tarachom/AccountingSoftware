﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServerTestErlang.AccountingSoftware
{
	//Довідник Вказівник - Ссилка на елемент довідника 
	//
	abstract class DirectoryPointer
	{
		public DirectoryPointer()
		{
			
		}

		protected void Init(UnigueID id)
		{
			UID = id;
		}

		public UnigueID UID { get; private set; }
	}
}

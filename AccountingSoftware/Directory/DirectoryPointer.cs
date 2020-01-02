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

		public void Init(UnigueID id)
		{
			UID = id;
		}

		public UnigueID UID { get; private set; }

		
	}

	interface IDirectoryPointer
	{
		UnigueID UID { get; }
	}
}
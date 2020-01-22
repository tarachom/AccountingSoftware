using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServerTestErlang.AccountingSoftware
{
	//Довідник Обєкт
	//
	public abstract class DirectoryObject
	{
		public UnigueID UID { get; private set; }

		public string Code { get; set; }

		public string Name { get; set; }

		public string Desc { get; set; }

		public void Init(UnigueID uid)
		{
			UID = uid;
		}
	}
}

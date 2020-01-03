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
		public Kernel kernel { get; set; }

		public Query QueryConstructor { get; set; }

		protected void AbstractSelect()
		{

			kernel.DataBase.SelectDirectory(QueryConstructor.Construct());
		}

		protected List<DirectoryPointer> AbstractDirectoryPointers { get; }
	}

	
}
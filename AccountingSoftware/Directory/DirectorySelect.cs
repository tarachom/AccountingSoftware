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

		protected List<DirectoryPointer> AbstractSelect()
		{
			return kernel.DataBase.SelectDirectory(this);
		}

		//protected List<DirectoryPointer> AbstractDirectoryPointers { get; }
	}

	
}
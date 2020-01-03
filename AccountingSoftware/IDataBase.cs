using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServerTestErlang.AccountingSoftware
{
	public interface IDataBase
	{
		void Open();

		void Close();

		string ConnectionString { get; set; }

		List<DirectoryPointer> SelectDirectory(DirectorySelect sender);
	}
}

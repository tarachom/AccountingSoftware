using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServerTestErlang.AccountingSoftware
{
	public class UnigueID
	{
		public UnigueID(string id, string table)
		{
			ID = id;
			Table = table;
		}

		public string ID { get; }
		public string Table { get; }
	}
}

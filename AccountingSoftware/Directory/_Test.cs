using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServerTestErlang.AccountingSoftware.Directory
{
	class Test
	{
		void A()
		{
			TestPointer TP = new TestPointer();
			
		}
	}

	class TestPointer : DirectoryPointer
	{
		public TestPointer()
		{
			base.Init(UnigueID id);
		}


	}


}

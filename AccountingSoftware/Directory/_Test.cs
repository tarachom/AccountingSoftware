using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServerTestErlang.AccountingSoftware
{
	class Test
	{
		void A()
		{
			TestPointer TP = new TestPointer();


			TestSelect TS = new TestSelect();
			TS.Read();
			
		}
	}

	class TestPointer : DirectoryPointer, IDirectoryPointer
	{
		public TestPointer()
		{
			//base.Init(UnigueID id);
		}
	}

	class TestSelect : DirectorySelect
	{
		public TestSelect()
		{
			//base.Init(UnigueID id);
		}

		public void Read()
		{
			List<IDirectoryPointer> listTestPointer = new List<IDirectoryPointer>();

			for (int i = 0; i < 10; i++) {
				TestPointer elementTestPointer = new TestPointer();
				elementTestPointer.Init(new UnigueID(i.ToString(), "Test"));

				listTestPointer.Add(elementTestPointer);
			}

			base.Init(listTestPointer);
		}
	}
}

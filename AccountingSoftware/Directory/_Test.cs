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

			//TS.DirectoryPointers[0].GetDirectoryObject().;

		}
	}

	class TestObjest : DirectoryObject
	{
		public TestPointer GetDirectoryPointer()
		{
			TestPointer TestPointerItem = new TestPointer();
			TestPointerItem.Init(base.UID);

			return TestPointerItem;
		}
	}

	class TestPointer : DirectoryPointer, IDirectoryPointer
	{
		public TestObjest GetDirectoryObject()
		{
			//Запрос в базу по base.UID
			TestObjest TestObjestItem = new TestObjest();

			TestObjestItem.Init(base.UID);

			TestObjestItem.Code = "1";
			TestObjestItem.Name = "1 Name";

			return TestObjestItem;
		}
	}

	class TestSelect : DirectorySelect
	{
		public TestSelect()
		{
			
		}

		public void Read()
		{
			for (int i = 0; i < 10; i++) {
				TestPointer elementTestPointer = new TestPointer();
				elementTestPointer.Init(new UnigueID(i.ToString(), "Test"));

				DirectoryPointers.Add(elementTestPointer);
			}
		}

		public List<TestPointer> DirectoryPointers { get; }
	}
}

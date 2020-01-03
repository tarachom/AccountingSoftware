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

			TestObjest TObj = TS.DirectoryPointers[0].GetDirectoryObject();

			TestATablePartRecord record = new TestATablePartRecord();
			record.Desc = "";

			TObj.ATablePart.RecordCollection.Add(record);
		}
	}

	class TestObjest : DirectoryObject
	{
		public TestObjest()
		{
			ATablePart = new TestATablePart(this);
		}

		public TestPointer GetDirectoryPointer()
		{
			TestPointer TestPointerItem = new TestPointer();
			TestPointerItem.Init(base.UID);

			return TestPointerItem;
		}

		public void Save()
		{

		}

		public TestPointer Field1 { get; set; }

		public TestPointer Field2 { get; set; }

		public TestPointer Field3 { get; set; }

		public string Field4 { get; set; }

		public TestATablePart ATablePart { get; }
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

		//Вибірка результат
		public List<KeyValuePair<string, object>> ResultSelect { get; private set; }

	    //Які поля вибирати
		public Dictionary<string, object> FieldSelect { get; set; }

		//Умови
		public Dictionary<string, string> FieldWhere { get; set; } //? AND або OR між полями як задавати

		//Сортування
		public Dictionary<string, SelectOrder> FieldOrder { get; set; }

		//Обмеження вибірки
		public int Limit { get; set; }

		public void Select()
		{
			KeyValuePair<string, object> a = new KeyValuePair<string, object>();

		}

		public void Read()
		{
			//for (int i = 0; i < 10; i++) 
			//{
				//TestPointer elementTestPointer = new TestPointer();
			//elementTestPointer.Init(

			//	elementTestPointer.Init(new UnigueID(i.ToString(), "Test"));

			//	DirectoryPointers.Add(elementTestPointer);
			//}

			//
		}

		public List<TestPointer> DirectoryPointers { get; }
	}

	class TestATablePart : DirectoryTablePart
	{
		public TestATablePart(TestObjest owner)
		{
			Owner = owner;
		}

		public TestObjest Owner { get; }

		public void Read()
		{
			for (int i = 0; i < 10; i++)
			{
				TestATablePartRecord record = new TestATablePartRecord();
				RecordCollection.Add(record);
			}
		}

		public List<TestATablePartRecord> RecordCollection { get; }
	}

	class TestATablePartRecord : DirectoryTablePartRecord
	{
		public string Name { get; set; }
		public string Desc { get; set; }
		public int Info { get; set; }
	}
}

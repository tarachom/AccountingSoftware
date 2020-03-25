
/*
Copyright (C) 2019-2020 TARAKHOMYN YURIY IVANOVYCH
All rights reserved.

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

/*
Автор:    Тарахомин Юрій Іванович
Адреса:   Україна, м. Львів
Сайт:     find.org.ua
*/
  
/*
 *
 * Конфігурації "Нова конфігурація"
 * Автор 
  
 * Дата конфігурації: 25.03.2020 14:59:40
 *
 */

using System;
using System.Collections.Generic;
using AccountingSoftware;

namespace ConfTrade_v1_1
{
    static class Config
    {
        public static Kernel Kernel { get; set; }
        
        public static bool StartInit { get; set; }
        
        public static void InitAllConstants()
        {
            
            Dictionary<string, object> fieldValue = new Dictionary<string, object>();
            bool IsSelect = Kernel.DataBase.SelectAllConstants("tab_constants",
                 new string[] { "col_a1" }, fieldValue);
            
            if (IsSelect)
            {
                StartInit = true;
                Константи.Test.dfsdfs_Const = new Довідники.Test_Pointer(fieldValue["col_a1"]);
                
                StartInit = false;
            }
        }
    }
}

namespace ConfTrade_v1_1.Константи
{
    
    static class Test
    {
        private static Довідники.Test_Pointer _dfsdfs_Const;
        public static Довідники.Test_Pointer dfsdfs_Const
        {
            get { return _dfsdfs_Const; }
            set
            {
                _dfsdfs_Const = value;
                if (!Config.StartInit)
                    Config.Kernel.DataBase.SaveConstants("tab_constants", "col_a1", _dfsdfs_Const.ToString());
            }
        }
        
        public class dfsdfs_Історія_TablePart : ConstantsTablePart
        {
            public dfsdfs_Історія_TablePart() : base(Config.Kernel, "tab_a01",
                 new string[] { "col_a1", "col_a2" }) 
            {
                Records = new List<Історія_Record>();
            }
                
            public List<Історія_Record> Records { get; set; }
        
            public void Read()
            {
                Records.Clear();
                base.BaseRead();

                foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
                {
                    Історія_Record record = new Історія_Record();
                    
                    record.UID = (Guid)fieldValue["uid"];
                    
                    record.Дата = (fieldValue["col_a1"] != DBNull.Value) ? DateTime.Parse(fieldValue["col_a1"].ToString()) : DateTime.MinValue;
                    record.Значенн = fieldValue["col_a2"].ToString();
                    
                    Records.Add(record);
                }
            
                base.BaseClear();
            }
        
            public void Save(bool clear_all_before_save /*= true*/) 
            {
                if (Records.Count > 0)
                {
                    base.BaseBeginTransaction();
                
                    if (clear_all_before_save)
                        base.BaseDelete();

                    foreach (Історія_Record record in Records)
                    {
                        Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                        fieldValue.Add("col_a1", record.Дата);
                        fieldValue.Add("col_a2", record.Значенн);
                        
                        base.BaseSave(record.UID, fieldValue);
                    }
                
                    base.BaseCommitTransaction();
                }
            }
        
            public void Delete()
            {
                base.BaseBeginTransaction();
                base.BaseCommitTransaction();
            }
            
            public class Історія_Record : ConstantsTablePartRecord
            {
                public Історія_Record()
                {
                    Дата = DateTime.MinValue;
                    Значенн = "";
                    
                }
        
                
                public Історія_Record(
                    DateTime?  _Дата = null, string _Значенн = "")
                {
                    Дата = _Дата ?? DateTime.MinValue;
                    Значенн = _Значенн;
                    
                }
                public DateTime Дата { get; set; }
                public string Значенн { get; set; }
                
            }            
        }
          
        public class dfsdfs_sdsdsd_TablePart : ConstantsTablePart
        {
            public dfsdfs_sdsdsd_TablePart() : base(Config.Kernel, "tab_a02",
                 new string[] { "col_a1" }) 
            {
                Records = new List<sdsdsd_Record>();
            }
                
            public List<sdsdsd_Record> Records { get; set; }
        
            public void Read()
            {
                Records.Clear();
                base.BaseRead();

                foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
                {
                    sdsdsd_Record record = new sdsdsd_Record();
                    
                    record.UID = (Guid)fieldValue["uid"];
                    
                    record.ssdsdsd = fieldValue["col_a1"].ToString();
                    
                    Records.Add(record);
                }
            
                base.BaseClear();
            }
        
            public void Save(bool clear_all_before_save /*= true*/) 
            {
                if (Records.Count > 0)
                {
                    base.BaseBeginTransaction();
                
                    if (clear_all_before_save)
                        base.BaseDelete();

                    foreach (sdsdsd_Record record in Records)
                    {
                        Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                        fieldValue.Add("col_a1", record.ssdsdsd);
                        
                        base.BaseSave(record.UID, fieldValue);
                    }
                
                    base.BaseCommitTransaction();
                }
            }
        
            public void Delete()
            {
                base.BaseBeginTransaction();
                base.BaseCommitTransaction();
            }
            
            public class sdsdsd_Record : ConstantsTablePartRecord
            {
                public sdsdsd_Record()
                {
                    ssdsdsd = "";
                    
                }
        
                
                public sdsdsd_Record(
                    string _ssdsdsd = "")
                {
                    ssdsdsd = _ssdsdsd;
                    
                }
                public string ssdsdsd { get; set; }
                
            }            
        }
          
        public class dfsdfs_asdAS_TablePart : ConstantsTablePart
        {
            public dfsdfs_asdAS_TablePart() : base(Config.Kernel, "tab_a05",
                 new string[] { "col_a2" }) 
            {
                Records = new List<asdAS_Record>();
            }
                
            public List<asdAS_Record> Records { get; set; }
        
            public void Read()
            {
                Records.Clear();
                base.BaseRead();

                foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
                {
                    asdAS_Record record = new asdAS_Record();
                    
                    record.UID = (Guid)fieldValue["uid"];
                    
                    record.AsdASDA = fieldValue["col_a2"].ToString();
                    
                    Records.Add(record);
                }
            
                base.BaseClear();
            }
        
            public void Save(bool clear_all_before_save /*= true*/) 
            {
                if (Records.Count > 0)
                {
                    base.BaseBeginTransaction();
                
                    if (clear_all_before_save)
                        base.BaseDelete();

                    foreach (asdAS_Record record in Records)
                    {
                        Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                        fieldValue.Add("col_a2", record.AsdASDA);
                        
                        base.BaseSave(record.UID, fieldValue);
                    }
                
                    base.BaseCommitTransaction();
                }
            }
        
            public void Delete()
            {
                base.BaseBeginTransaction();
                base.BaseCommitTransaction();
            }
            
            public class asdAS_Record : ConstantsTablePartRecord
            {
                public asdAS_Record()
                {
                    AsdASDA = "";
                    
                }
        
                
                public asdAS_Record(
                    string _AsdASDA = "")
                {
                    AsdASDA = _AsdASDA;
                    
                }
                public string AsdASDA { get; set; }
                
            }            
        }
               
    }
    
}

namespace ConfTrade_v1_1.Довідники
{
    
    #region DIRECTORY "Test"
    
    class Test_Objest : DirectoryObject
    {
        public Test_Objest() : base(Config.Kernel, "tab_a49",
             new string[] { "col_a1", "col_a2", "col_a3" }) 
        {
            Назва = "";
            Код = "";
            івфіва = 0;
            
            //Табличні частини
            Test_TablePart = new Test_Test_TablePart(this);
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Назва = base.FieldValue["col_a1"].ToString();
                Код = base.FieldValue["col_a2"].ToString();
                івфіва = (base.FieldValue["col_a3"] != DBNull.Value) ? (Перелічення.Test)base.FieldValue["col_a3"] : 0;
                
                BaseClear();
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["col_a1"] = Назва;
            base.FieldValue["col_a2"] = Код;
            base.FieldValue["col_a3"] = (int)івфіва;
            
            BaseSave();
        }

        public string Serialize()
        {
            return 
            "<Test>" +
               "<uid>" + base.UnigueID.ToString() + "</uid>" +
               "<Назва>" + "<![CDATA[" + Назва + "]]>" + "</Назва>"  +
               "<Код>" + "<![CDATA[" + Код + "]]>" + "</Код>"  +
               "<івфіва>" + ((int)івфіва).ToString() + "</івфіва>"  +
               "</Test>";
        }

        public void Delete()
        {
            base.BaseDelete();
        }
        
        public Test_Pointer GetDirectoryPointer()
        {
            Test_Pointer directoryPointer = new Test_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public string Назва { get; set; }
        public string Код { get; set; }
        public Перелічення.Test івфіва { get; set; }
        
        //Табличні частини
        public Test_Test_TablePart Test_TablePart { get; set; }
        
    }
    
    
    class Test_Pointer : DirectoryPointer
    {
        public Test_Pointer(object uid = null) : base(Config.Kernel, "tab_a49")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public Test_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a49")
        {
            base.Init(uid, fields);
        }
        
        public Test_Objest GetDirectoryObject()
        {
            Test_Objest TestObjestItem = new Test_Objest();
            TestObjestItem.Read(base.UnigueID);
            return TestObjestItem;
        }
    }
    
    
    class Test_Select : DirectorySelect, IDisposable
    {
        public Test_Select() : base(Config.Kernel, "tab_a49",
            new string[] { "col_a1", "col_a2", "col_a3" },
            new string[] { "Назва", "Код", "івфіва" }) { }
    
        public bool Select() { return base.BaseSelect(); }
        
        public bool SelectSingle() { if (base.BaseSelectSingle()) { MoveNext(); return true; } else { Current = null; return false; } }
        
        public bool MoveNext() { if (MoveToPosition()) { Current = new Test_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields); return true; } else { Current = null; return false; } }

        public Test_Pointer Current { get; private set; }
        
        public Test_Pointer FindByField(string name, object value)
        {
            Test_Pointer itemPointer = new Test_Pointer();
            DirectoryPointer directoryPointer = base.BaseFindByField(base.Alias[name], value);
            if (!directoryPointer.IsEmpty()) itemPointer.Init(directoryPointer.UnigueID);
            return itemPointer;
        }
        
        public List<Test_Pointer> FindListByField(string name, object value, int limit = 0, int offset = 0)
        {
            List<Test_Pointer> directoryPointerList = new List<Test_Pointer>();
            foreach (DirectoryPointer directoryPointer in base.BaseFindListByField(base.Alias[name], value, limit, offset)) 
                directoryPointerList.Add(new Test_Pointer(directoryPointer.UnigueID));
            return directoryPointerList;
        }
    }
    
      
    class Test_Test_TablePart : DirectoryTablePart
    {
        public Test_Test_TablePart(Test_Objest owner) : base(Config.Kernel, "tab_a56",
             new string[] { "col_a1", "col_a2" }) 
        {
            if (owner == null) throw new Exception("owner null");
            
            Owner = owner;
            Records = new List<Record>();
        }
        
        public Test_Objest Owner { get; private set; }
        
        public List<Record> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Record record = new Record();
                record.UID = (Guid)fieldValue["uid"];
                
                record.Test = fieldValue["col_a1"].ToString();
                record.кцукц = new Довідники.Test_Pointer(fieldValue["col_a2"]);
                
                Records.Add(record);
            }
            
            base.BaseClear();
        }
        
        public void Save(bool clear_all_before_save /*= true*/) 
        {
            if (Records.Count > 0)
            {
                base.BaseBeginTransaction();
                
                if (clear_all_before_save)
                    base.BaseDelete(Owner.UnigueID);

                foreach (Record record in Records)
                {
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                    fieldValue.Add("col_a1", record.Test);
                    fieldValue.Add("col_a2", record.кцукц.ToString());
                    
                    base.BaseSave(record.UID, Owner.UnigueID, fieldValue);
                }
                
                base.BaseCommitTransaction();
            }
        }
        
        public void Delete()
        {
            base.BaseBeginTransaction();
            base.BaseDelete(Owner.UnigueID);
            base.BaseCommitTransaction();
        }
        
        
        public class Record : DirectoryTablePartRecord
        {
            public Record()
            {
                Test = "";
                кцукц = new Довідники.Test_Pointer();
                
            }
        
            
            public Record(
                string _Test = "", Довідники.Test_Pointer _кцукц = null)
            {
                Test = _Test;
                кцукц = _кцукц ?? new Довідники.Test_Pointer();
                
            }
            public string Test { get; set; }
            public Довідники.Test_Pointer кцукц { get; set; }
            
        }
    }
      ///<summary>
    ///Список.
    ///</summary>
    class Test_Список_View : DirectoryView
    {
        public Test_Список_View() : base(Config.Kernel, "tab_a49", 
             new string[] { "col_a1", "col_a2", "col_a3" },
             new string[] { "Назва", "Код", "івфіва" },
             new string[] { "string", "string", "enum" },
             "Довідники.Test_Список")
        {
            
        }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "Номенклатура"
    
    class Номенклатура_Objest : DirectoryObject
    {
        public Номенклатура_Objest() : base(Config.Kernel, "tab_a14",
             new string[] { "col_a1", "col_a2", "col_a3", "col_a4", "col_a5", "col_a6", "col_a7", "col_a8", "col_a9", "col_b1" }) 
        {
            Назва = "";
            Код = "";
            Ціна = 0;
            Кво = 0;
            Перелічення1 = 0;
            Дата = DateTime.MinValue;
            ДатаЧас = DateTime.MinValue;
            Час = DateTime.MinValue.TimeOfDay;
            Логічний = false;
            Вказівник = new Довідники.Test_Pointer();
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Назва = base.FieldValue["col_a1"].ToString();
                Код = base.FieldValue["col_a2"].ToString();
                Ціна = (base.FieldValue["col_a3"] != DBNull.Value) ? (decimal)base.FieldValue["col_a3"] : 0;
                Кво = (base.FieldValue["col_a4"] != DBNull.Value) ? (int)base.FieldValue["col_a4"] : 0;
                Перелічення1 = (base.FieldValue["col_a5"] != DBNull.Value) ? (Перелічення.Test)base.FieldValue["col_a5"] : 0;
                Дата = (base.FieldValue["col_a6"] != DBNull.Value) ? DateTime.Parse(base.FieldValue["col_a6"].ToString()) : DateTime.MinValue;
                ДатаЧас = (base.FieldValue["col_a7"] != DBNull.Value) ? DateTime.Parse(base.FieldValue["col_a7"].ToString()) : DateTime.MinValue;
                Час = (base.FieldValue["col_a8"] != DBNull.Value) ? TimeSpan.Parse(base.FieldValue["col_a8"].ToString()) : DateTime.MinValue.TimeOfDay;
                Логічний = (base.FieldValue["col_a9"] != DBNull.Value) ? bool.Parse(base.FieldValue["col_a9"].ToString()) : false;
                Вказівник = new Довідники.Test_Pointer(base.FieldValue["col_b1"]);
                
                BaseClear();
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["col_a1"] = Назва;
            base.FieldValue["col_a2"] = Код;
            base.FieldValue["col_a3"] = Ціна;
            base.FieldValue["col_a4"] = Кво;
            base.FieldValue["col_a5"] = (int)Перелічення1;
            base.FieldValue["col_a6"] = Дата;
            base.FieldValue["col_a7"] = ДатаЧас;
            base.FieldValue["col_a8"] = Час;
            base.FieldValue["col_a9"] = Логічний;
            base.FieldValue["col_b1"] = Вказівник.UnigueID.UGuid;
            
            BaseSave();
        }

        public string Serialize()
        {
            return 
            "<Номенклатура>" +
               "<uid>" + base.UnigueID.ToString() + "</uid>" +
               "<Назва>" + "<![CDATA[" + Назва + "]]>" + "</Назва>"  +
               "<Код>" + "<![CDATA[" + Код + "]]>" + "</Код>"  +
               "<Ціна>" + Ціна.ToString() + "</Ціна>"  +
               "<Кво>" + Кво.ToString() + "</Кво>"  +
               "<Перелічення1>" + ((int)Перелічення1).ToString() + "</Перелічення1>"  +
               "<Дата>" + Дата.ToString() + "</Дата>"  +
               "<ДатаЧас>" + ДатаЧас.ToString() + "</ДатаЧас>"  +
               "<Час>" + Час.ToString() + "</Час>"  +
               "<Логічний>" + (Логічний == true ? "1" : "0") + "</Логічний>"  +
               "<Вказівник>" + Вказівник.ToString() + "</Вказівник>"  +
               "</Номенклатура>";
        }

        public void Delete()
        {
            base.BaseDelete();
        }
        
        public Номенклатура_Pointer GetDirectoryPointer()
        {
            Номенклатура_Pointer directoryPointer = new Номенклатура_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public string Назва { get; set; }
        public string Код { get; set; }
        public decimal Ціна { get; set; }
        public int Кво { get; set; }
        public Перелічення.Test Перелічення1 { get; set; }
        public DateTime Дата { get; set; }
        public DateTime ДатаЧас { get; set; }
        public TimeSpan Час { get; set; }
        public bool Логічний { get; set; }
        public Довідники.Test_Pointer Вказівник { get; set; }
        
    }
    
    
    class Номенклатура_Pointer : DirectoryPointer
    {
        public Номенклатура_Pointer(object uid = null) : base(Config.Kernel, "tab_a14")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public Номенклатура_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a14")
        {
            base.Init(uid, fields);
        }
        
        public Номенклатура_Objest GetDirectoryObject()
        {
            Номенклатура_Objest НоменклатураObjestItem = new Номенклатура_Objest();
            НоменклатураObjestItem.Read(base.UnigueID);
            return НоменклатураObjestItem;
        }
    }
    
    
    class Номенклатура_Select : DirectorySelect, IDisposable
    {
        public Номенклатура_Select() : base(Config.Kernel, "tab_a14",
            new string[] { "col_a1", "col_a2", "col_a3", "col_a4", "col_a5", "col_a6", "col_a7", "col_a8", "col_a9", "col_b1" },
            new string[] { "Назва", "Код", "Ціна", "Кво", "Перелічення1", "Дата", "ДатаЧас", "Час", "Логічний", "Вказівник" }) { }
    
        public bool Select() { return base.BaseSelect(); }
        
        public bool SelectSingle() { if (base.BaseSelectSingle()) { MoveNext(); return true; } else { Current = null; return false; } }
        
        public bool MoveNext() { if (MoveToPosition()) { Current = new Номенклатура_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields); return true; } else { Current = null; return false; } }

        public Номенклатура_Pointer Current { get; private set; }
        
        public Номенклатура_Pointer FindByField(string name, object value)
        {
            Номенклатура_Pointer itemPointer = new Номенклатура_Pointer();
            DirectoryPointer directoryPointer = base.BaseFindByField(base.Alias[name], value);
            if (!directoryPointer.IsEmpty()) itemPointer.Init(directoryPointer.UnigueID);
            return itemPointer;
        }
        
        public List<Номенклатура_Pointer> FindListByField(string name, object value, int limit = 0, int offset = 0)
        {
            List<Номенклатура_Pointer> directoryPointerList = new List<Номенклатура_Pointer>();
            foreach (DirectoryPointer directoryPointer in base.BaseFindListByField(base.Alias[name], value, limit, offset)) 
                directoryPointerList.Add(new Номенклатура_Pointer(directoryPointer.UnigueID));
            return directoryPointerList;
        }
    }
    
      ///<summary>
    ///Список.
    ///</summary>
    class Номенклатура_Список_View : DirectoryView
    {
        public Номенклатура_Список_View() : base(Config.Kernel, "tab_a14", 
             new string[] { "col_a1", "col_a2", "col_a3", "col_a4" },
             new string[] { "Назва", "Код", "Ціна", "Кво" },
             new string[] { "string", "string", "numeric", "integer" },
             "Довідники.Номенклатура_Список")
        {
            
        }
        
    }
      
    
    #endregion
    
}

namespace ConfTrade_v1_1.Перелічення
{
    
    public enum Test
    {
         
    }
    
    
    public enum qq
    {
         qq = 1
    }
    
    
}

namespace ConfTrade_v1_1.Документи
{
    
    #region DOCUMENT "Test"
    
    
    class Test_Objest : DocumentObject
    {
        public Test_Objest() : base(Config.Kernel, "tab_a50",
             new string[] { "col_a3", "col_a4" }) 
        {
            ДатаДок = DateTime.MinValue;
            НомерДок = 0;
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                ДатаДок = (base.FieldValue["col_a3"] != DBNull.Value) ? DateTime.Parse(base.FieldValue["col_a3"].ToString()) : DateTime.MinValue;
                НомерДок = (base.FieldValue["col_a4"] != DBNull.Value) ? (int)base.FieldValue["col_a4"] : 0;
                
                BaseClear();
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["col_a3"] = ДатаДок;
            base.FieldValue["col_a4"] = НомерДок;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public Test_Pointer GetDocumentPointer()
        {
            Test_Pointer directoryPointer = new Test_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public DateTime ДатаДок { get; set; }
        public int НомерДок { get; set; }
        
    }
    
    
    class Test_Pointer : DocumentPointer
    {
        public Test_Pointer(object uid = null) : base(Config.Kernel, "tab_a50")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public Test_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a50")
        {
            base.Init(uid, fields);
        } 
        
        public Test_Objest GetDocumentObject()
        {
            Test_Objest TestObjestItem = new Test_Objest();
            TestObjestItem.Read(base.UnigueID);
            return TestObjestItem;
        }
    }
    
    
    class Test_Select : DocumentSelect, IDisposable
    {
        public Test_Select() : base(Config.Kernel, "tab_a50") { }
        
        public bool Select() { return base.BaseSelect(); }
        
        public bool SelectSingle() { if (base.BaseSelectSingle()) { MoveNext(); return true; } else { Current = null; return false; } }
        
        public bool MoveNext() { if (MoveToPosition()) { Current = new Test_Pointer(base.DocumentPointerPosition.UnigueID, base.DocumentPointerPosition.Fields); return true; } else { Current = null; return false; } }
        
        public Test_Pointer Current { get; private set; }
    }
    
      
    
    #endregion
    
}

namespace ConfTrade_v1_1.Журнали
{

}

namespace ConfTrade_v1_1.РегістриВідомостей
{
    
    #region REGISTER "Test"
    
    
    class Test_RecordsSet : RegisterInformationRecordsSet
    {
        public Test_RecordsSet() : base(Config.Kernel, "tab_a51",
             new string[] {  }) 
        {
            Records = new List<Record>();
            Filter = new SelectFilter();
        }
        
        public List<Record> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            
            

            base.BaseRead();
            
            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Record record = new Record();
                
                record.UID = (Guid)fieldValue["uid"];
                  
                
                Records.Add(record);
            }
            
            base.BaseClear();
        }
        
        public void Save(bool clear_all_before_save = true) 
        {
            if (Records.Count > 0)
            {
                base.BaseBeginTransaction();
                
                if (clear_all_before_save)
                    base.BaseDelete();

                foreach (Record record in Records)
                {
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                    
                    base.BaseSave(record.UID, fieldValue);
                }
                
                base.BaseCommitTransaction();
            }
        }
        
        public void Delete()
        {
            base.BaseBeginTransaction();
            base.BaseDelete();
            base.BaseCommitTransaction();
        }
        
        public SelectFilter Filter { get; set; }
        
        
        public class Record : RegisterRecord
        {
            public Record()
            {
                
            }
        
            
        }
    
        public class SelectFilter
        {
            public SelectFilter()
            {
                 
            }
        
            
        }
    }
    
    #endregion
  
}

namespace ConfTrade_v1_1.РегістриНакопичення
{
    
    #region REGISTER "Test"
    
    
    class Test_RecordsSet : RegisterAccumulationRecordsSet
    {
        public Test_RecordsSet() : base(Config.Kernel, "tab_a54",
             new string[] {  }) 
        {
            Records = new List<Record>();
            Filter = new SelectFilter();
        }
        
        public List<Record> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            
            

            base.BaseRead();
            
            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Record record = new Record();
                
                record.UID = (Guid)fieldValue["uid"];
                  
                
                Records.Add(record);
            }
            
            base.BaseClear();
        }
        
        public void Save(bool clear_all_before_save = true) 
        {
            if (Records.Count > 0)
            {
                base.BaseBeginTransaction();
                
                if (clear_all_before_save)
                    base.BaseDelete();

                foreach (Record record in Records)
                {
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                    
                    base.BaseSave(record.UID, fieldValue);
                }
                
                base.BaseCommitTransaction();
            }
        }
        
        public void Delete()
        {
            base.BaseBeginTransaction();
            base.BaseDelete();
            base.BaseCommitTransaction();
        }
        
        public SelectFilter Filter { get; set; }
        
        
        public class Record : RegisterRecord
        {
            public Record()
            {
                
            }
        
            
        }
    
        public class SelectFilter
        {
            public SelectFilter()
            {
                 
            }
        
            
        }
    }
    
    #endregion
  
}
  

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
  
 * Дата конфігурації: 22.03.2020 10:47:27
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
    
    #region DIRECTORY "Test2"
    
    class Test2_Objest : DirectoryObject
    {
        public Test2_Objest() : base(Config.Kernel, "tab_a03",
             new string[] { "col_a1", "col_a2", "col_a3" }) 
        {
            Назва = "";
            Код = "";
            івфіва = 0;
            
            //Табличні частини
            Test_TablePart = new Test2_Test_TablePart(this);
            
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
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public Test2_Pointer GetDirectoryPointer()
        {
            Test2_Pointer directoryPointer = new Test2_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public string Назва { get; set; }
        public string Код { get; set; }
        public Перелічення.Test івфіва { get; set; }
        
        //Табличні частини
        public Test2_Test_TablePart Test_TablePart { get; set; }
        
    }
    
    
    class Test2_Pointer : DirectoryPointer
    {
        public Test2_Pointer(object uid = null) : base(Config.Kernel, "tab_a03")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public Test2_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a03")
        {
            base.Init(uid, fields);
        }
        
        public Test2_Objest GetDirectoryObject()
        {
            Test2_Objest Test2ObjestItem = new Test2_Objest();
            Test2ObjestItem.Read(base.UnigueID);
            return Test2ObjestItem;
        }
    }
    
    
    class Test2_Select : DirectorySelect, IDisposable
    {
        public Test2_Select() : base(Config.Kernel, "tab_a03",
            new string[] { "col_a1", "col_a2", "col_a3" },
            new string[] { "Назва", "Код", "івфіва" }) { }
    
        public bool Select() { return base.BaseSelect(); }
        
        public bool SelectSingle() { if (base.BaseSelectSingle()) { MoveNext(); return true; } else { Current = null; return false; } }
        
        public bool MoveNext() { if (MoveToPosition()) { Current = new Test2_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields); return true; } else { Current = null; return false; } }

        public Test2_Pointer Current { get; private set; }
        
        public Test2_Pointer FindByField(string name, object value)
        {
            Test2_Pointer itemPointer = new Test2_Pointer();
            DirectoryPointer directoryPointer = base.BaseFindByField(base.Alias[name], value);
            if (!directoryPointer.IsEmpty()) itemPointer.Init(directoryPointer.UnigueID);
            return itemPointer;
        }
        
        public List<Test2_Pointer> FindListByField(string name, object value, int limit = 0, int offset = 0)
        {
            List<Test2_Pointer> directoryPointerList = new List<Test2_Pointer>();
            foreach (DirectoryPointer directoryPointer in base.BaseFindListByField(base.Alias[name], value, limit, offset)) 
                directoryPointerList.Add(new Test2_Pointer(directoryPointer.UnigueID));
            return directoryPointerList;
        }
    }
    
      
    class Test2_Test_TablePart : DirectoryTablePart
    {
        public Test2_Test_TablePart(Test2_Objest owner) : base(Config.Kernel, "tab_a04",
             new string[] { "col_a1", "col_a2" }) 
        {
            if (owner == null) throw new Exception("owner null");
            
            Owner = owner;
            Records = new List<Record>();
        }
        
        public Test2_Objest Owner { get; private set; }
        
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
    class Test2_Список_View : DirectoryView
    {
        public Test2_Список_View() : base(Config.Kernel, "tab_a49", 
             new string[] { "col_a1", "col_a2", "col_a3" },
             new string[] { "Назва", "Код", "івфіва" },
             new string[] { "string", "string", "enum" },
             "Довідники.Test2_Список")
        {
            
        }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "Test2_Копія_1"
    
    class Test2_Копія_1_Objest : DirectoryObject
    {
        public Test2_Копія_1_Objest() : base(Config.Kernel, "tab_a06",
             new string[] { "col_a1", "col_a2", "col_a3" }) 
        {
            Назва = "";
            Код = "";
            івфіва = 0;
            
            //Табличні частини
            Test_TablePart = new Test2_Копія_1_Test_TablePart(this);
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Назва = base.FieldValue["col_a1"].ToString();
                Код = base.FieldValue["col_a2"].ToString();
                івфіва = (base.FieldValue["col_a3"] != DBNull.Value) ? (Перелічення.qq)base.FieldValue["col_a3"] : 0;
                
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
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public Test2_Копія_1_Pointer GetDirectoryPointer()
        {
            Test2_Копія_1_Pointer directoryPointer = new Test2_Копія_1_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public string Назва { get; set; }
        public string Код { get; set; }
        public Перелічення.qq івфіва { get; set; }
        
        //Табличні частини
        public Test2_Копія_1_Test_TablePart Test_TablePart { get; set; }
        
    }
    
    
    class Test2_Копія_1_Pointer : DirectoryPointer
    {
        public Test2_Копія_1_Pointer(object uid = null) : base(Config.Kernel, "tab_a06")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public Test2_Копія_1_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a06")
        {
            base.Init(uid, fields);
        }
        
        public Test2_Копія_1_Objest GetDirectoryObject()
        {
            Test2_Копія_1_Objest Test2_Копія_1ObjestItem = new Test2_Копія_1_Objest();
            Test2_Копія_1ObjestItem.Read(base.UnigueID);
            return Test2_Копія_1ObjestItem;
        }
    }
    
    
    class Test2_Копія_1_Select : DirectorySelect, IDisposable
    {
        public Test2_Копія_1_Select() : base(Config.Kernel, "tab_a06",
            new string[] { "col_a1", "col_a2", "col_a3" },
            new string[] { "Назва", "Код", "івфіва" }) { }
    
        public bool Select() { return base.BaseSelect(); }
        
        public bool SelectSingle() { if (base.BaseSelectSingle()) { MoveNext(); return true; } else { Current = null; return false; } }
        
        public bool MoveNext() { if (MoveToPosition()) { Current = new Test2_Копія_1_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields); return true; } else { Current = null; return false; } }

        public Test2_Копія_1_Pointer Current { get; private set; }
        
        public Test2_Копія_1_Pointer FindByField(string name, object value)
        {
            Test2_Копія_1_Pointer itemPointer = new Test2_Копія_1_Pointer();
            DirectoryPointer directoryPointer = base.BaseFindByField(base.Alias[name], value);
            if (!directoryPointer.IsEmpty()) itemPointer.Init(directoryPointer.UnigueID);
            return itemPointer;
        }
        
        public List<Test2_Копія_1_Pointer> FindListByField(string name, object value, int limit = 0, int offset = 0)
        {
            List<Test2_Копія_1_Pointer> directoryPointerList = new List<Test2_Копія_1_Pointer>();
            foreach (DirectoryPointer directoryPointer in base.BaseFindListByField(base.Alias[name], value, limit, offset)) 
                directoryPointerList.Add(new Test2_Копія_1_Pointer(directoryPointer.UnigueID));
            return directoryPointerList;
        }
    }
    
      
    class Test2_Копія_1_Test_TablePart : DirectoryTablePart
    {
        public Test2_Копія_1_Test_TablePart(Test2_Копія_1_Objest owner) : base(Config.Kernel, "tab_a07",
             new string[] { "col_a1", "col_a2" }) 
        {
            if (owner == null) throw new Exception("owner null");
            
            Owner = owner;
            Records = new List<Record>();
        }
        
        public Test2_Копія_1_Objest Owner { get; private set; }
        
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
    class Test2_Копія_1_Список_View : DirectoryView
    {
        public Test2_Копія_1_Список_View() : base(Config.Kernel, "tab_a49", 
             new string[] { "col_a1", "col_a2", "col_a3" },
             new string[] { "Назва", "Код", "івфіва" },
             new string[] { "string", "string", "enum" },
             "Довідники.Test2_Копія_1_Список")
        {
            
        }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "Test_Копія_1"
    
    class Test_Копія_1_Objest : DirectoryObject
    {
        public Test_Копія_1_Objest() : base(Config.Kernel, "tab_a08",
             new string[] { "col_a1", "col_a2", "col_a3" }) 
        {
            Назва = "";
            Код = "";
            івфіва = 0;
            
            //Табличні частини
            Test_TablePart = new Test_Копія_1_Test_TablePart(this);
            
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
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public Test_Копія_1_Pointer GetDirectoryPointer()
        {
            Test_Копія_1_Pointer directoryPointer = new Test_Копія_1_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public string Назва { get; set; }
        public string Код { get; set; }
        public Перелічення.Test івфіва { get; set; }
        
        //Табличні частини
        public Test_Копія_1_Test_TablePart Test_TablePart { get; set; }
        
    }
    
    
    class Test_Копія_1_Pointer : DirectoryPointer
    {
        public Test_Копія_1_Pointer(object uid = null) : base(Config.Kernel, "tab_a08")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public Test_Копія_1_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a08")
        {
            base.Init(uid, fields);
        }
        
        public Test_Копія_1_Objest GetDirectoryObject()
        {
            Test_Копія_1_Objest Test_Копія_1ObjestItem = new Test_Копія_1_Objest();
            Test_Копія_1ObjestItem.Read(base.UnigueID);
            return Test_Копія_1ObjestItem;
        }
    }
    
    
    class Test_Копія_1_Select : DirectorySelect, IDisposable
    {
        public Test_Копія_1_Select() : base(Config.Kernel, "tab_a08",
            new string[] { "col_a1", "col_a2", "col_a3" },
            new string[] { "Назва", "Код", "івфіва" }) { }
    
        public bool Select() { return base.BaseSelect(); }
        
        public bool SelectSingle() { if (base.BaseSelectSingle()) { MoveNext(); return true; } else { Current = null; return false; } }
        
        public bool MoveNext() { if (MoveToPosition()) { Current = new Test_Копія_1_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields); return true; } else { Current = null; return false; } }

        public Test_Копія_1_Pointer Current { get; private set; }
        
        public Test_Копія_1_Pointer FindByField(string name, object value)
        {
            Test_Копія_1_Pointer itemPointer = new Test_Копія_1_Pointer();
            DirectoryPointer directoryPointer = base.BaseFindByField(base.Alias[name], value);
            if (!directoryPointer.IsEmpty()) itemPointer.Init(directoryPointer.UnigueID);
            return itemPointer;
        }
        
        public List<Test_Копія_1_Pointer> FindListByField(string name, object value, int limit = 0, int offset = 0)
        {
            List<Test_Копія_1_Pointer> directoryPointerList = new List<Test_Копія_1_Pointer>();
            foreach (DirectoryPointer directoryPointer in base.BaseFindListByField(base.Alias[name], value, limit, offset)) 
                directoryPointerList.Add(new Test_Копія_1_Pointer(directoryPointer.UnigueID));
            return directoryPointerList;
        }
    }
    
      
    class Test_Копія_1_Test_TablePart : DirectoryTablePart
    {
        public Test_Копія_1_Test_TablePart(Test_Копія_1_Objest owner) : base(Config.Kernel, "tab_a09",
             new string[] { "col_a1", "col_a2" }) 
        {
            if (owner == null) throw new Exception("owner null");
            
            Owner = owner;
            Records = new List<Record>();
        }
        
        public Test_Копія_1_Objest Owner { get; private set; }
        
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
    class Test_Копія_1_Список_View : DirectoryView
    {
        public Test_Копія_1_Список_View() : base(Config.Kernel, "tab_a49", 
             new string[] { "col_a1", "col_a2", "col_a3" },
             new string[] { "Назва", "Код", "івфіва" },
             new string[] { "string", "string", "enum" },
             "Довідники.Test_Копія_1_Список")
        {
            
        }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "Test_Копія_2"
    
    class Test_Копія_2_Objest : DirectoryObject
    {
        public Test_Копія_2_Objest() : base(Config.Kernel, "tab_a10",
             new string[] { "col_a1", "col_a2", "col_a3" }) 
        {
            Назва = "";
            Код = "";
            івфіва = 0;
            
            //Табличні частини
            Test_TablePart = new Test_Копія_2_Test_TablePart(this);
            
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
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public Test_Копія_2_Pointer GetDirectoryPointer()
        {
            Test_Копія_2_Pointer directoryPointer = new Test_Копія_2_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public string Назва { get; set; }
        public string Код { get; set; }
        public Перелічення.Test івфіва { get; set; }
        
        //Табличні частини
        public Test_Копія_2_Test_TablePart Test_TablePart { get; set; }
        
    }
    
    
    class Test_Копія_2_Pointer : DirectoryPointer
    {
        public Test_Копія_2_Pointer(object uid = null) : base(Config.Kernel, "tab_a10")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public Test_Копія_2_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a10")
        {
            base.Init(uid, fields);
        }
        
        public Test_Копія_2_Objest GetDirectoryObject()
        {
            Test_Копія_2_Objest Test_Копія_2ObjestItem = new Test_Копія_2_Objest();
            Test_Копія_2ObjestItem.Read(base.UnigueID);
            return Test_Копія_2ObjestItem;
        }
    }
    
    
    class Test_Копія_2_Select : DirectorySelect, IDisposable
    {
        public Test_Копія_2_Select() : base(Config.Kernel, "tab_a10",
            new string[] { "col_a1", "col_a2", "col_a3" },
            new string[] { "Назва", "Код", "івфіва" }) { }
    
        public bool Select() { return base.BaseSelect(); }
        
        public bool SelectSingle() { if (base.BaseSelectSingle()) { MoveNext(); return true; } else { Current = null; return false; } }
        
        public bool MoveNext() { if (MoveToPosition()) { Current = new Test_Копія_2_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields); return true; } else { Current = null; return false; } }

        public Test_Копія_2_Pointer Current { get; private set; }
        
        public Test_Копія_2_Pointer FindByField(string name, object value)
        {
            Test_Копія_2_Pointer itemPointer = new Test_Копія_2_Pointer();
            DirectoryPointer directoryPointer = base.BaseFindByField(base.Alias[name], value);
            if (!directoryPointer.IsEmpty()) itemPointer.Init(directoryPointer.UnigueID);
            return itemPointer;
        }
        
        public List<Test_Копія_2_Pointer> FindListByField(string name, object value, int limit = 0, int offset = 0)
        {
            List<Test_Копія_2_Pointer> directoryPointerList = new List<Test_Копія_2_Pointer>();
            foreach (DirectoryPointer directoryPointer in base.BaseFindListByField(base.Alias[name], value, limit, offset)) 
                directoryPointerList.Add(new Test_Копія_2_Pointer(directoryPointer.UnigueID));
            return directoryPointerList;
        }
    }
    
      
    class Test_Копія_2_Test_TablePart : DirectoryTablePart
    {
        public Test_Копія_2_Test_TablePart(Test_Копія_2_Objest owner) : base(Config.Kernel, "tab_a11",
             new string[] { "col_a1", "col_a2" }) 
        {
            if (owner == null) throw new Exception("owner null");
            
            Owner = owner;
            Records = new List<Record>();
        }
        
        public Test_Копія_2_Objest Owner { get; private set; }
        
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
    class Test_Копія_2_Список_View : DirectoryView
    {
        public Test_Копія_2_Список_View() : base(Config.Kernel, "tab_a49", 
             new string[] { "col_a1", "col_a2", "col_a3" },
             new string[] { "Назва", "Код", "івфіва" },
             new string[] { "string", "string", "enum" },
             "Довідники.Test_Копія_2_Список")
        {
            
        }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "Test2_Копія_2"
    
    class Test2_Копія_2_Objest : DirectoryObject
    {
        public Test2_Копія_2_Objest() : base(Config.Kernel, "tab_a12",
             new string[] { "col_a1", "col_a2", "col_a3" }) 
        {
            Назва = "";
            Код = "";
            івфіва = 0;
            
            //Табличні частини
            Test_TablePart = new Test2_Копія_2_Test_TablePart(this);
            
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
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public Test2_Копія_2_Pointer GetDirectoryPointer()
        {
            Test2_Копія_2_Pointer directoryPointer = new Test2_Копія_2_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public string Назва { get; set; }
        public string Код { get; set; }
        public Перелічення.Test івфіва { get; set; }
        
        //Табличні частини
        public Test2_Копія_2_Test_TablePart Test_TablePart { get; set; }
        
    }
    
    
    class Test2_Копія_2_Pointer : DirectoryPointer
    {
        public Test2_Копія_2_Pointer(object uid = null) : base(Config.Kernel, "tab_a12")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public Test2_Копія_2_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a12")
        {
            base.Init(uid, fields);
        }
        
        public Test2_Копія_2_Objest GetDirectoryObject()
        {
            Test2_Копія_2_Objest Test2_Копія_2ObjestItem = new Test2_Копія_2_Objest();
            Test2_Копія_2ObjestItem.Read(base.UnigueID);
            return Test2_Копія_2ObjestItem;
        }
    }
    
    
    class Test2_Копія_2_Select : DirectorySelect, IDisposable
    {
        public Test2_Копія_2_Select() : base(Config.Kernel, "tab_a12",
            new string[] { "col_a1", "col_a2", "col_a3" },
            new string[] { "Назва", "Код", "івфіва" }) { }
    
        public bool Select() { return base.BaseSelect(); }
        
        public bool SelectSingle() { if (base.BaseSelectSingle()) { MoveNext(); return true; } else { Current = null; return false; } }
        
        public bool MoveNext() { if (MoveToPosition()) { Current = new Test2_Копія_2_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields); return true; } else { Current = null; return false; } }

        public Test2_Копія_2_Pointer Current { get; private set; }
        
        public Test2_Копія_2_Pointer FindByField(string name, object value)
        {
            Test2_Копія_2_Pointer itemPointer = new Test2_Копія_2_Pointer();
            DirectoryPointer directoryPointer = base.BaseFindByField(base.Alias[name], value);
            if (!directoryPointer.IsEmpty()) itemPointer.Init(directoryPointer.UnigueID);
            return itemPointer;
        }
        
        public List<Test2_Копія_2_Pointer> FindListByField(string name, object value, int limit = 0, int offset = 0)
        {
            List<Test2_Копія_2_Pointer> directoryPointerList = new List<Test2_Копія_2_Pointer>();
            foreach (DirectoryPointer directoryPointer in base.BaseFindListByField(base.Alias[name], value, limit, offset)) 
                directoryPointerList.Add(new Test2_Копія_2_Pointer(directoryPointer.UnigueID));
            return directoryPointerList;
        }
    }
    
      
    class Test2_Копія_2_Test_TablePart : DirectoryTablePart
    {
        public Test2_Копія_2_Test_TablePart(Test2_Копія_2_Objest owner) : base(Config.Kernel, "tab_a13",
             new string[] { "col_a1", "col_a2" }) 
        {
            if (owner == null) throw new Exception("owner null");
            
            Owner = owner;
            Records = new List<Record>();
        }
        
        public Test2_Копія_2_Objest Owner { get; private set; }
        
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
    class Test2_Копія_2_Список_View : DirectoryView
    {
        public Test2_Копія_2_Список_View() : base(Config.Kernel, "tab_a49", 
             new string[] { "col_a1", "col_a2", "col_a3" },
             new string[] { "Назва", "Код", "івфіва" },
             new string[] { "string", "string", "enum" },
             "Довідники.Test2_Копія_2_Список")
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
  
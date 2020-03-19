
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
  
 * Дата конфігурації: 19.03.2020 15:43:06
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
                 new string[] { "col_a2", "col_a1" }, fieldValue);
            
            if (IsSelect)
            {
                StartInit = true;
                Константи.Test.Test_Const = new Довідники.Test_Pointer(fieldValue["col_a2"]);
                Константи.Test2.Test_Const = fieldValue["col_a1"].ToString();
                
                StartInit = false;
            }
        }
    }
}

namespace ConfTrade_v1_1.Константи
{
    
    static class Test
    {
        private static Довідники.Test_Pointer _Test_Const;
        public static Довідники.Test_Pointer Test_Const
        {
            get { return _Test_Const; }
            set
            {
                _Test_Const = value;
                if (!Config.StartInit)
                    Config.Kernel.DataBase.SaveConstants("tab_constants", "col_a2", _Test_Const.ToString());
            }
        }
        
        public class Test_цукц_TablePart : ConstantsTablePart
        {
            public Test_цукц_TablePart() : base(Config.Kernel, "tab_a71",
                 new string[] { "col_a1", "col_a2" }) 
            {
                Records = new List<цукц_Record>();
            }
                
            public List<цукц_Record> Records { get; set; }
        
            public void Read()
            {
                Records.Clear();
                base.BaseRead();

                foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
                {
                    цукц_Record record = new цукц_Record();
                    
                    record.UID = (Guid)fieldValue["uid"];
                    
                    record.цукцу1 = fieldValue["col_a1"].ToString();
                    record.цкцук2 = fieldValue["col_a2"].ToString();
                    
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

                    foreach (цукц_Record record in Records)
                    {
                        Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                        fieldValue.Add("col_a1", record.цукцу1);
                        fieldValue.Add("col_a2", record.цкцук2);
                        
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
            
            public class цукц_Record : ConstantsTablePartRecord
            {
                public цукц_Record()
                {
                    цукцу1 = "";
                    цкцук2 = "";
                    
                }
        
                
                public цукц_Record(
                    string _цукцу1 = "", string _цкцук2 = "")
                {
                    цукцу1 = _цукцу1;
                    цкцук2 = _цкцук2;
                    
                }
                public string цукцу1 { get; set; }
                public string цкцук2 { get; set; }
                
            }            
        }
               
    }
    
    static class Test2
    {
        private static string _Test_Const;
        public static string Test_Const
        {
            get { return _Test_Const; }
            set
            {
                _Test_Const = value;
                if (!Config.StartInit)
                    Config.Kernel.DataBase.SaveConstants("tab_constants", "col_a1", _Test_Const);
            }
        }
        
        public class Test_sasdfs_TablePart : ConstantsTablePart
        {
            public Test_sasdfs_TablePart() : base(Config.Kernel, "tab_a01",
                 new string[] { "col_a1", "col_a2", "col_a3" }) 
            {
                Records = new List<sasdfs_Record>();
            }
                
            public List<sasdfs_Record> Records { get; set; }
        
            public void Read()
            {
                Records.Clear();
                base.BaseRead();

                foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
                {
                    sasdfs_Record record = new sasdfs_Record();
                    
                    record.UID = (Guid)fieldValue["uid"];
                    
                    record.s2 = fieldValue["col_a1"].ToString();
                    record.w1 = fieldValue["col_a2"].ToString();
                    record.we = fieldValue["col_a3"].ToString();
                    
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

                    foreach (sasdfs_Record record in Records)
                    {
                        Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                        fieldValue.Add("col_a1", record.s2);
                        fieldValue.Add("col_a2", record.w1);
                        fieldValue.Add("col_a3", record.we);
                        
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
            
            public class sasdfs_Record : ConstantsTablePartRecord
            {
                public sasdfs_Record()
                {
                    s2 = "";
                    w1 = "";
                    we = "";
                    
                }
        
                
                public sasdfs_Record(
                    string _s2 = "", string _w1 = "", string _we = "")
                {
                    s2 = _s2;
                    w1 = _w1;
                    we = _we;
                    
                }
                public string s2 { get; set; }
                public string w1 { get; set; }
                public string we { get; set; }
                
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
             new string[] { "col_a1", "col_a2" },
             new string[] { "Назва", "Код" },
             new string[] { "string", "string" },
             "Довідники.Test_Список")
        {
            
        }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "Test_Копія_1"
    
    class Test_Копія_1_Objest : DirectoryObject
    {
        public Test_Копія_1_Objest() : base(Config.Kernel, "tab_a57",
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
        public Test_Копія_1_Pointer(object uid = null) : base(Config.Kernel, "tab_a57")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public Test_Копія_1_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a57")
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
        public Test_Копія_1_Select() : base(Config.Kernel, "tab_a57",
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
        public Test_Копія_1_Test_TablePart(Test_Копія_1_Objest owner) : base(Config.Kernel, "tab_a58",
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
             new string[] { "col_a1", "col_a2" },
             new string[] { "Назва", "Код" },
             new string[] { "string", "string" },
             "Довідники.Test_Копія_1_Список")
        {
            
        }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "Test_Копія_2"
    
    class Test_Копія_2_Objest : DirectoryObject
    {
        public Test_Копія_2_Objest() : base(Config.Kernel, "tab_a59",
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
        public Test_Копія_2_Pointer(object uid = null) : base(Config.Kernel, "tab_a59")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public Test_Копія_2_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a59")
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
        public Test_Копія_2_Select() : base(Config.Kernel, "tab_a59",
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
        public Test_Копія_2_Test_TablePart(Test_Копія_2_Objest owner) : base(Config.Kernel, "tab_a61",
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
             new string[] { "col_a1", "col_a2" },
             new string[] { "Назва", "Код" },
             new string[] { "string", "string" },
             "Довідники.Test_Копія_2_Список")
        {
            
        }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "Test_Копія_3"
    
    class Test_Копія_3_Objest : DirectoryObject
    {
        public Test_Копія_3_Objest() : base(Config.Kernel, "tab_a65",
             new string[] { "col_a1", "col_a2", "col_a3", "col_a4" }) 
        {
            Назва = "";
            Код = "";
            івфіва = 0;
            івфіва2 = 0;
            
            //Табличні частини
            Test_TablePart = new Test_Копія_3_Test_TablePart(this);
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Назва = base.FieldValue["col_a1"].ToString();
                Код = base.FieldValue["col_a2"].ToString();
                івфіва = (base.FieldValue["col_a3"] != DBNull.Value) ? (Перелічення.Test)base.FieldValue["col_a3"] : 0;
                івфіва2 = (base.FieldValue["col_a4"] != DBNull.Value) ? (Перелічення.Test)base.FieldValue["col_a4"] : 0;
                
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
            base.FieldValue["col_a4"] = (int)івфіва2;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public Test_Копія_3_Pointer GetDirectoryPointer()
        {
            Test_Копія_3_Pointer directoryPointer = new Test_Копія_3_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public string Назва { get; set; }
        public string Код { get; set; }
        public Перелічення.Test івфіва { get; set; }
        public Перелічення.Test івфіва2 { get; set; }
        
        //Табличні частини
        public Test_Копія_3_Test_TablePart Test_TablePart { get; set; }
        
    }
    
    
    class Test_Копія_3_Pointer : DirectoryPointer
    {
        public Test_Копія_3_Pointer(object uid = null) : base(Config.Kernel, "tab_a65")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public Test_Копія_3_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a65")
        {
            base.Init(uid, fields);
        }
        
        public Test_Копія_3_Objest GetDirectoryObject()
        {
            Test_Копія_3_Objest Test_Копія_3ObjestItem = new Test_Копія_3_Objest();
            Test_Копія_3ObjestItem.Read(base.UnigueID);
            return Test_Копія_3ObjestItem;
        }
    }
    
    
    class Test_Копія_3_Select : DirectorySelect, IDisposable
    {
        public Test_Копія_3_Select() : base(Config.Kernel, "tab_a65",
            new string[] { "col_a1", "col_a2", "col_a3", "col_a4" },
            new string[] { "Назва", "Код", "івфіва", "івфіва2" }) { }
    
        public bool Select() { return base.BaseSelect(); }
        
        public bool SelectSingle() { if (base.BaseSelectSingle()) { MoveNext(); return true; } else { Current = null; return false; } }
        
        public bool MoveNext() { if (MoveToPosition()) { Current = new Test_Копія_3_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields); return true; } else { Current = null; return false; } }

        public Test_Копія_3_Pointer Current { get; private set; }
        
        public Test_Копія_3_Pointer FindByField(string name, object value)
        {
            Test_Копія_3_Pointer itemPointer = new Test_Копія_3_Pointer();
            DirectoryPointer directoryPointer = base.BaseFindByField(base.Alias[name], value);
            if (!directoryPointer.IsEmpty()) itemPointer.Init(directoryPointer.UnigueID);
            return itemPointer;
        }
        
        public List<Test_Копія_3_Pointer> FindListByField(string name, object value, int limit = 0, int offset = 0)
        {
            List<Test_Копія_3_Pointer> directoryPointerList = new List<Test_Копія_3_Pointer>();
            foreach (DirectoryPointer directoryPointer in base.BaseFindListByField(base.Alias[name], value, limit, offset)) 
                directoryPointerList.Add(new Test_Копія_3_Pointer(directoryPointer.UnigueID));
            return directoryPointerList;
        }
    }
    
      
    class Test_Копія_3_Test_TablePart : DirectoryTablePart
    {
        public Test_Копія_3_Test_TablePart(Test_Копія_3_Objest owner) : base(Config.Kernel, "tab_a66",
             new string[] { "col_a1", "col_a2" }) 
        {
            if (owner == null) throw new Exception("owner null");
            
            Owner = owner;
            Records = new List<Record>();
        }
        
        public Test_Копія_3_Objest Owner { get; private set; }
        
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
    class Test_Копія_3_Список_View : DirectoryView
    {
        public Test_Копія_3_Список_View() : base(Config.Kernel, "tab_a49", 
             new string[] { "col_a1", "col_a2" },
             new string[] { "Назва", "Код" },
             new string[] { "string", "string" },
             "Довідники.Test_Копія_3_Список")
        {
            
        }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "Test_Копія_4"
    
    class Test_Копія_4_Objest : DirectoryObject
    {
        public Test_Копія_4_Objest() : base(Config.Kernel, "tab_a67",
             new string[] { "col_a1", "col_a2", "col_a3", "col_a4", "col_a5" }) 
        {
            Назва = "";
            Код = "";
            івфіва = 0;
            івфіва_Копія_1 = 0;
            Код_Копія_1 = 0;
            
            //Табличні частини
            Test_TablePart = new Test_Копія_4_Test_TablePart(this);
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Назва = base.FieldValue["col_a1"].ToString();
                Код = base.FieldValue["col_a2"].ToString();
                івфіва = (base.FieldValue["col_a3"] != DBNull.Value) ? (Перелічення.Test)base.FieldValue["col_a3"] : 0;
                івфіва_Копія_1 = (base.FieldValue["col_a4"] != DBNull.Value) ? (Перелічення.Test)base.FieldValue["col_a4"] : 0;
                Код_Копія_1 = (base.FieldValue["col_a5"] != DBNull.Value) ? (int)base.FieldValue["col_a5"] : 0;
                
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
            base.FieldValue["col_a4"] = (int)івфіва_Копія_1;
            base.FieldValue["col_a5"] = Код_Копія_1;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public Test_Копія_4_Pointer GetDirectoryPointer()
        {
            Test_Копія_4_Pointer directoryPointer = new Test_Копія_4_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public string Назва { get; set; }
        public string Код { get; set; }
        public Перелічення.Test івфіва { get; set; }
        public Перелічення.Test івфіва_Копія_1 { get; set; }
        public int Код_Копія_1 { get; set; }
        
        //Табличні частини
        public Test_Копія_4_Test_TablePart Test_TablePart { get; set; }
        
    }
    
    
    class Test_Копія_4_Pointer : DirectoryPointer
    {
        public Test_Копія_4_Pointer(object uid = null) : base(Config.Kernel, "tab_a67")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public Test_Копія_4_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a67")
        {
            base.Init(uid, fields);
        }
        
        public Test_Копія_4_Objest GetDirectoryObject()
        {
            Test_Копія_4_Objest Test_Копія_4ObjestItem = new Test_Копія_4_Objest();
            Test_Копія_4ObjestItem.Read(base.UnigueID);
            return Test_Копія_4ObjestItem;
        }
    }
    
    
    class Test_Копія_4_Select : DirectorySelect, IDisposable
    {
        public Test_Копія_4_Select() : base(Config.Kernel, "tab_a67",
            new string[] { "col_a1", "col_a2", "col_a3", "col_a4", "col_a5" },
            new string[] { "Назва", "Код", "івфіва", "івфіва_Копія_1", "Код_Копія_1" }) { }
    
        public bool Select() { return base.BaseSelect(); }
        
        public bool SelectSingle() { if (base.BaseSelectSingle()) { MoveNext(); return true; } else { Current = null; return false; } }
        
        public bool MoveNext() { if (MoveToPosition()) { Current = new Test_Копія_4_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields); return true; } else { Current = null; return false; } }

        public Test_Копія_4_Pointer Current { get; private set; }
        
        public Test_Копія_4_Pointer FindByField(string name, object value)
        {
            Test_Копія_4_Pointer itemPointer = new Test_Копія_4_Pointer();
            DirectoryPointer directoryPointer = base.BaseFindByField(base.Alias[name], value);
            if (!directoryPointer.IsEmpty()) itemPointer.Init(directoryPointer.UnigueID);
            return itemPointer;
        }
        
        public List<Test_Копія_4_Pointer> FindListByField(string name, object value, int limit = 0, int offset = 0)
        {
            List<Test_Копія_4_Pointer> directoryPointerList = new List<Test_Копія_4_Pointer>();
            foreach (DirectoryPointer directoryPointer in base.BaseFindListByField(base.Alias[name], value, limit, offset)) 
                directoryPointerList.Add(new Test_Копія_4_Pointer(directoryPointer.UnigueID));
            return directoryPointerList;
        }
    }
    
      
    class Test_Копія_4_Test_TablePart : DirectoryTablePart
    {
        public Test_Копія_4_Test_TablePart(Test_Копія_4_Objest owner) : base(Config.Kernel, "tab_a68",
             new string[] { "col_a1", "col_a2" }) 
        {
            if (owner == null) throw new Exception("owner null");
            
            Owner = owner;
            Records = new List<Record>();
        }
        
        public Test_Копія_4_Objest Owner { get; private set; }
        
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
    class Test_Копія_4_Список_View : DirectoryView
    {
        public Test_Копія_4_Список_View() : base(Config.Kernel, "tab_a49", 
             new string[] { "col_a1", "col_a2" },
             new string[] { "Назва", "Код" },
             new string[] { "string", "string" },
             "Довідники.Test_Копія_4_Список")
        {
            
        }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "Test_Копія_5"
    
    class Test_Копія_5_Objest : DirectoryObject
    {
        public Test_Копія_5_Objest() : base(Config.Kernel, "tab_a69",
             new string[] { "col_a1", "col_a2", "col_a3" }) 
        {
            Назва = "";
            Код = "";
            івфіва = 0;
            
            //Табличні частини
            Test_TablePart = new Test_Копія_5_Test_TablePart(this);
            
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
        
        public Test_Копія_5_Pointer GetDirectoryPointer()
        {
            Test_Копія_5_Pointer directoryPointer = new Test_Копія_5_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public string Назва { get; set; }
        public string Код { get; set; }
        public Перелічення.Test івфіва { get; set; }
        
        //Табличні частини
        public Test_Копія_5_Test_TablePart Test_TablePart { get; set; }
        
    }
    
    
    class Test_Копія_5_Pointer : DirectoryPointer
    {
        public Test_Копія_5_Pointer(object uid = null) : base(Config.Kernel, "tab_a69")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public Test_Копія_5_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a69")
        {
            base.Init(uid, fields);
        }
        
        public Test_Копія_5_Objest GetDirectoryObject()
        {
            Test_Копія_5_Objest Test_Копія_5ObjestItem = new Test_Копія_5_Objest();
            Test_Копія_5ObjestItem.Read(base.UnigueID);
            return Test_Копія_5ObjestItem;
        }
    }
    
    
    class Test_Копія_5_Select : DirectorySelect, IDisposable
    {
        public Test_Копія_5_Select() : base(Config.Kernel, "tab_a69",
            new string[] { "col_a1", "col_a2", "col_a3" },
            new string[] { "Назва", "Код", "івфіва" }) { }
    
        public bool Select() { return base.BaseSelect(); }
        
        public bool SelectSingle() { if (base.BaseSelectSingle()) { MoveNext(); return true; } else { Current = null; return false; } }
        
        public bool MoveNext() { if (MoveToPosition()) { Current = new Test_Копія_5_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields); return true; } else { Current = null; return false; } }

        public Test_Копія_5_Pointer Current { get; private set; }
        
        public Test_Копія_5_Pointer FindByField(string name, object value)
        {
            Test_Копія_5_Pointer itemPointer = new Test_Копія_5_Pointer();
            DirectoryPointer directoryPointer = base.BaseFindByField(base.Alias[name], value);
            if (!directoryPointer.IsEmpty()) itemPointer.Init(directoryPointer.UnigueID);
            return itemPointer;
        }
        
        public List<Test_Копія_5_Pointer> FindListByField(string name, object value, int limit = 0, int offset = 0)
        {
            List<Test_Копія_5_Pointer> directoryPointerList = new List<Test_Копія_5_Pointer>();
            foreach (DirectoryPointer directoryPointer in base.BaseFindListByField(base.Alias[name], value, limit, offset)) 
                directoryPointerList.Add(new Test_Копія_5_Pointer(directoryPointer.UnigueID));
            return directoryPointerList;
        }
    }
    
      
    class Test_Копія_5_Test_TablePart : DirectoryTablePart
    {
        public Test_Копія_5_Test_TablePart(Test_Копія_5_Objest owner) : base(Config.Kernel, "tab_a70",
             new string[] { "col_a1", "col_a2" }) 
        {
            if (owner == null) throw new Exception("owner null");
            
            Owner = owner;
            Records = new List<Record>();
        }
        
        public Test_Копія_5_Objest Owner { get; private set; }
        
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
    class Test_Копія_5_Список_View : DirectoryView
    {
        public Test_Копія_5_Список_View() : base(Config.Kernel, "tab_a49", 
             new string[] { "col_a1", "col_a2" },
             new string[] { "Назва", "Код" },
             new string[] { "string", "string" },
             "Довідники.Test_Копія_5_Список")
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
  
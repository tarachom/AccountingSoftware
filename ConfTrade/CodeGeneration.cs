
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
  
 * Дата конфігурації: 18.03.2020 05:05:55
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
                 new string[] { "col_a2" }, fieldValue);
            
            if (IsSelect)
            {
                StartInit = true;
                Константи.Test2.Test_Const = fieldValue["col_a2"].ToString();
                
                StartInit = false;
            }
        }
    }
}

namespace ConfTrade_v1_1.Константи
{
    
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
                    Config.Kernel.DataBase.SaveConstants("tab_constants", "col_a2", _Test_Const);
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
             new string[] { "col_a1", "col_a2" }) 
        {
            Назва = "";
            Код = "";
            
            //Табличні частини
            Test_TablePart = new Test_Test_TablePart(this);
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Назва = base.FieldValue["col_a1"].ToString();
                Код = base.FieldValue["col_a2"].ToString();
                
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
            new string[] { "col_a1", "col_a2" },
            new string[] { "Назва", "Код" }) { }
    
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
             new string[] { "col_a1" }) 
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
                
            }
        
            
            public Record(
                string _Test = "")
            {
                Test = _Test;
                
            }
            public string Test { get; set; }
            
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
  
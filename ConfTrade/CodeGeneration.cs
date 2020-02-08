

/*
 *
 * Конфігурації "ConfTrade 1.1"
 * Автор Yurik
 * Дата конфігурації: 08.02.2020 13:51:16
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
    }
    
    #region DIRECTORY "Tovary"
    
    /// <summary> 
    /// Довідник Товари
    /// </summary>
    class Tovary_Objest : DirectoryObject
    {
        public Tovary_Objest() : base(Config.Kernel, "tovary",
             new string[] { "name", "code", "count", "numer" }) 
        {
            Name = "";
            Code = "";
            Count = 0;
            Numer = 0;
            
            //Табличні частини
            Ceny_TablePart = new Tovary_Ceny_TablePart(this);
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Name = base.FieldValue["name"].ToString();
                Code = base.FieldValue["code"].ToString();
                Count = (int)base.FieldValue["count"];
                Numer = (base.FieldValue["numer"] != DBNull.Value) ? (decimal)base.FieldValue["numer"] : 0;
                
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["name"] = Name;
            base.FieldValue["code"] = Code;
            base.FieldValue["count"] = Count;
            base.FieldValue["numer"] = Numer;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public Tovary_Pointer GetDirectoryPointer()
        {
            Tovary_Pointer directoryPointer = new Tovary_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public string Name { get; set; }
        public string Code { get; set; }
        public int Count { get; set; }
        public decimal Numer { get; set; }
        
        //Табличні частини
        public Tovary_Ceny_TablePart Ceny_TablePart { get; set; }
        
    }
    
    /// <summary> 
    /// Довідник Товари
    /// </summary>
    class Tovary_Pointer : DirectoryPointer
    {
        public Tovary_Pointer(object uid = null) : base(Config.Kernel, "tovary")
        {
            if (uid != null && uid != DBNull.Value) base.Init(new UnigueID((Guid)uid), null);
        }

        public Tovary_Objest GetDirectoryObject()
        {
            Tovary_Objest TovaryObjestItem = new Tovary_Objest();
            TovaryObjestItem.Read(base.UnigueID);
            return TovaryObjestItem;
        }
    }
    
    /// <summary> 
    /// Довідник Товари
    /// </summary>
    class Tovary_Select : DirectorySelect
    {
        public Tovary_Select() : base(Config.Kernel, "tovary") { }
    
        public bool Select() 
        { 
            return base.BaseSelect();
        }
        
        public bool SelectSingle()
        {
            if (base.BaseSelectSingle())
            {
                MoveNext();
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }
        
        public bool MoveNext()
        {
            if (MoveToPosition())
            {
                Current = new Tovary_Pointer();
                Current.Init(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields);
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }

        public Tovary_Pointer Current { get; private set; }
    }
    
      
    /// <summary>
    /// Таблична частина Ціни
    /// </summary>
    class Tovary_Ceny_TablePart : DirectoryTablePart
    {
        public Tovary_Ceny_TablePart(Tovary_Objest owner) : base(Config.Kernel, "tovary_ceny_tablepart",
             new string[] { "name", "cena", "isnew" }) 
        {
            Owner = owner;
            Records = new List<Tovary_Ceny_TablePartRecord>();
        }
        
        public Tovary_Objest Owner { get; private set; }
        
        public List<Tovary_Ceny_TablePartRecord> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.FieldValueList.Clear();

            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Tovary_Ceny_TablePartRecord record = new Tovary_Ceny_TablePartRecord();

                record.Name = fieldValue["name"].ToString();
                record.Cena = (fieldValue["cena"] != DBNull.Value) ? (decimal)fieldValue["cena"] : 0;
                record.IsNew = (int)fieldValue["isnew"];
                
                Records.Add(record);
            }
        }
        
        /// <summary>
        /// Зберегти колекцію Records в базу.
        /// </summary>
        /// <param name="clear_all_before_save">
        /// Перед записом колекції, попередні записи видаляються з бази даних.
        /// Щоб не видаляти треба поставити clear_all_before_save = false.
        /// Це корисно коли потрібно добавити нові записи без зчитування всієї колекції.
        /// </param>
        public void Save(bool clear_all_before_save = true) 
        {
            if (Records.Count > 0)
            {
                base.BaseBeginTransaction();
                
                if (clear_all_before_save)
                    base.BaseDelete(Owner.UnigueID);

                foreach (Tovary_Ceny_TablePartRecord record in Records)
                {
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                    fieldValue.Add("name", record.Name);
                    fieldValue.Add("cena", record.Cena);
                    fieldValue.Add("isnew", record.IsNew);
                    
                    base.BaseSave(Owner.UnigueID, fieldValue);
                }
                
                base.BaseCommitTransaction();
            }
        }
        
        public void Clear()
        {
            base.BaseDelete(Owner.UnigueID);
        }
    }
    
    /// <summary> 
    /// Таблична частина Ціни
    /// </summary>
    class Tovary_Ceny_TablePartRecord : DirectoryTablePartRecord
    {
        public Tovary_Ceny_TablePartRecord()
        {
            Name = "";
            Cena = 0;
            IsNew = 0;
            
        }
        
        public Tovary_Ceny_TablePartRecord(
            string _Name = "", decimal _Cena = 0, int _IsNew = 0)
        {
            Name = _Name;
            Cena = _Cena;
            IsNew = _IsNew;
            
        }
        
        public string Name { get; set; }
        public decimal Cena { get; set; }
        public int IsNew { get; set; }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "test"
    
    /// <summary> 
    /// test
    /// </summary>
    class test_Objest : DirectoryObject
    {
        public test_Objest() : base(Config.Kernel, "test",
             new string[] {  }) 
        {
            
            //Табличні частини
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public test_Pointer GetDirectoryPointer()
        {
            test_Pointer directoryPointer = new test_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        
        //Табличні частини
        
    }
    
    /// <summary> 
    /// test
    /// </summary>
    class test_Pointer : DirectoryPointer
    {
        public test_Pointer(object uid = null) : base(Config.Kernel, "test")
        {
            if (uid != null && uid != DBNull.Value) base.Init(new UnigueID((Guid)uid), null);
        }

        public test_Objest GetDirectoryObject()
        {
            test_Objest testObjestItem = new test_Objest();
            testObjestItem.Read(base.UnigueID);
            return testObjestItem;
        }
    }
    
    /// <summary> 
    /// test
    /// </summary>
    class test_Select : DirectorySelect
    {
        public test_Select() : base(Config.Kernel, "test") { }
    
        public bool Select() 
        { 
            return base.BaseSelect();
        }
        
        public bool SelectSingle()
        {
            if (base.BaseSelectSingle())
            {
                MoveNext();
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }
        
        public bool MoveNext()
        {
            if (MoveToPosition())
            {
                Current = new test_Pointer();
                Current.Init(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields);
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }

        public test_Pointer Current { get; private set; }
    }
    
      
    
    #endregion
    
    #region DIRECTORY "test2"
    
    /// <summary> 
    /// test2
    /// </summary>
    class test2_Objest : DirectoryObject
    {
        public test2_Objest() : base(Config.Kernel, "test2",
             new string[] {  }) 
        {
            
            //Табличні частини
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public test2_Pointer GetDirectoryPointer()
        {
            test2_Pointer directoryPointer = new test2_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        
        //Табличні частини
        
    }
    
    /// <summary> 
    /// test2
    /// </summary>
    class test2_Pointer : DirectoryPointer
    {
        public test2_Pointer(object uid = null) : base(Config.Kernel, "test2")
        {
            if (uid != null && uid != DBNull.Value) base.Init(new UnigueID((Guid)uid), null);
        }

        public test2_Objest GetDirectoryObject()
        {
            test2_Objest test2ObjestItem = new test2_Objest();
            test2ObjestItem.Read(base.UnigueID);
            return test2ObjestItem;
        }
    }
    
    /// <summary> 
    /// test2
    /// </summary>
    class test2_Select : DirectorySelect
    {
        public test2_Select() : base(Config.Kernel, "test2") { }
    
        public bool Select() 
        { 
            return base.BaseSelect();
        }
        
        public bool SelectSingle()
        {
            if (base.BaseSelectSingle())
            {
                MoveNext();
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }
        
        public bool MoveNext()
        {
            if (MoveToPosition())
            {
                Current = new test2_Pointer();
                Current.Init(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields);
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }

        public test2_Pointer Current { get; private set; }
    }
    
      
    
    #endregion
    
    
    
}
  
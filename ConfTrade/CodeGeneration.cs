

/*
 *
 * Конфігурації "ConfTrade 1.1"
 * Автор Yurik
 * Дата конфігурації: 08.02.2020 21:36:28
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
    /// Довідник Товари Desc
    /// </summary>
    class Tovary_Objest : DirectoryObject
    {
        public Tovary_Objest() : base(Config.Kernel, "tovary",
             new string[] { "name", "code", "count", "numer", "masiv", "artikul", "pointer1", "pointer2" }) 
        {
            Назва = "";
            Код = "";
            Кількість = 0;
            Номер = 0;
            Масив = "";
            Артикул = "";
            Вказівник1 = new ТМЦ_Pointer();
            Вказівник2 = new test2_Pointer();
            
            //Табличні частини
            Ціни_TablePart = new Tovary_Ціни_TablePart(this);
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Назва = base.FieldValue["name"].ToString();
                Код = base.FieldValue["code"].ToString();
                Кількість = (int)base.FieldValue["count"];
                Номер = (base.FieldValue["numer"] != DBNull.Value) ? (decimal)base.FieldValue["numer"] : 0;
                Масив = base.FieldValue["masiv"].ToString();
                Артикул = base.FieldValue["artikul"].ToString();
                Вказівник1 = new ТМЦ_Pointer(base.FieldValue["pointer1"]);
                Вказівник2 = new test2_Pointer(base.FieldValue["pointer2"]);
                
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["name"] = Назва;
            base.FieldValue["code"] = Код;
            base.FieldValue["count"] = Кількість;
            base.FieldValue["numer"] = Номер;
            base.FieldValue["masiv"] = Масив;
            base.FieldValue["artikul"] = Артикул;
            base.FieldValue["pointer1"] = Вказівник1.UnigueID.UGuid;
            base.FieldValue["pointer2"] = Вказівник2.UnigueID.UGuid;
            
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
        
        public string Назва { get; set; }
        public string Код { get; set; }
        public int Кількість { get; set; }
        public decimal Номер { get; set; }
        public string Масив { get; set; }
        public string Артикул { get; set; }
        public ТМЦ_Pointer Вказівник1 { get; set; }
        public test2_Pointer Вказівник2 { get; set; }
        
        //Табличні частини
        public Tovary_Ціни_TablePart Ціни_TablePart { get; set; }
        
    }
    
    /// <summary> 
    /// Довідник Товари Desc
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
    /// Довідник Товари Desc
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
    class Tovary_Ціни_TablePart : DirectoryTablePart
    {
        public Tovary_Ціни_TablePart(Tovary_Objest owner) : base(Config.Kernel, "tovary_ceny_tablepart",
             new string[] { "name", "cena", "isnew" }) 
        {
            Owner = owner;
            Records = new List<Tovary_Ціни_TablePartRecord>();
        }
        
        public Tovary_Objest Owner { get; private set; }
        
        public List<Tovary_Ціни_TablePartRecord> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.FieldValueList.Clear();

            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Tovary_Ціни_TablePartRecord record = new Tovary_Ціни_TablePartRecord();

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

                foreach (Tovary_Ціни_TablePartRecord record in Records)
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
    class Tovary_Ціни_TablePartRecord : DirectoryTablePartRecord
    {
        public Tovary_Ціни_TablePartRecord()
        {
            Name = "";
            Cena = 0;
            IsNew = 0;
            
        }
        
        public Tovary_Ціни_TablePartRecord(
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
             new string[] { "count", "sum" }) 
        {
            Count = 0;
            Sum = 0;
            
            //Табличні частини
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Count = (int)base.FieldValue["count"];
                Sum = (base.FieldValue["sum"] != DBNull.Value) ? (decimal)base.FieldValue["sum"] : 0;
                
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["count"] = Count;
            base.FieldValue["sum"] = Sum;
            
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
        
        public int Count { get; set; }
        public decimal Sum { get; set; }
        
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
             new string[] { "name", "code", "desc" }) 
        {
            Name = "";
            Code = "";
            Desc = "";
            
            //Табличні частини
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Name = base.FieldValue["name"].ToString();
                Code = base.FieldValue["code"].ToString();
                Desc = base.FieldValue["desc"].ToString();
                
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["name"] = Name;
            base.FieldValue["code"] = Code;
            base.FieldValue["desc"] = Desc;
            
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
        
        public string Name { get; set; }
        public string Code { get; set; }
        public string Desc { get; set; }
        
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
    
    #region DIRECTORY "New"
    
    /// <summary> 
    /// new new
    /// </summary>
    class New_Objest : DirectoryObject
    {
        public New_Objest() : base(Config.Kernel, "new",
             new string[] { "id", "name" }) 
        {
            Код = "";
            Назва = "";
            
            //Табличні частини
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Код = base.FieldValue["id"].ToString();
                Назва = base.FieldValue["name"].ToString();
                
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["id"] = Код;
            base.FieldValue["name"] = Назва;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public New_Pointer GetDirectoryPointer()
        {
            New_Pointer directoryPointer = new New_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public string Код { get; set; }
        public string Назва { get; set; }
        
        //Табличні частини
        
    }
    
    /// <summary> 
    /// new new
    /// </summary>
    class New_Pointer : DirectoryPointer
    {
        public New_Pointer(object uid = null) : base(Config.Kernel, "new")
        {
            if (uid != null && uid != DBNull.Value) base.Init(new UnigueID((Guid)uid), null);
        }

        public New_Objest GetDirectoryObject()
        {
            New_Objest NewObjestItem = new New_Objest();
            NewObjestItem.Read(base.UnigueID);
            return NewObjestItem;
        }
    }
    
    /// <summary> 
    /// new new
    /// </summary>
    class New_Select : DirectorySelect
    {
        public New_Select() : base(Config.Kernel, "new") { }
    
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
                Current = new New_Pointer();
                Current.Init(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields);
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }

        public New_Pointer Current { get; private set; }
    }
    
      
    
    #endregion
    
    #region DIRECTORY "ТМЦ"
    
    /// <summary> 
    /// Товаро-матеріальні цінності
    /// </summary>
    class ТМЦ_Objest : DirectoryObject
    {
        public ТМЦ_Objest() : base(Config.Kernel, "tmc",
             new string[] { "name", "description", "od", "data_add", "base_cena", "autor" }) 
        {
            Назва = "";
            Опис = "";
            ОдиницяВиміру = new string[] { };
            ДатаДобавлення = DateTime.MinValue;
            ОсновнаЦіна = 0;
            Автор = "";
            
            //Табличні частини
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Назва = base.FieldValue["name"].ToString();
                Опис = base.FieldValue["description"].ToString();
                ОдиницяВиміру = (base.FieldValue["od"] != DBNull.Value) ? (string[])base.FieldValue["od"] : new string[] { };
                ДатаДобавлення = (base.FieldValue["data_add"] != DBNull.Value) ? DateTime.Parse(base.FieldValue["data_add"].ToString()) : DateTime.MinValue;
                ОсновнаЦіна = (base.FieldValue["base_cena"] != DBNull.Value) ? (decimal)base.FieldValue["base_cena"] : 0;
                Автор = base.FieldValue["autor"].ToString();
                
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["name"] = Назва;
            base.FieldValue["description"] = Опис;
            base.FieldValue["od"] = ОдиницяВиміру;
            base.FieldValue["data_add"] = ДатаДобавлення;
            base.FieldValue["base_cena"] = ОсновнаЦіна;
            base.FieldValue["autor"] = Автор;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public ТМЦ_Pointer GetDirectoryPointer()
        {
            ТМЦ_Pointer directoryPointer = new ТМЦ_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public string Назва { get; set; }
        public string Опис { get; set; }
        public string[] ОдиницяВиміру { get; set; }
        public DateTime ДатаДобавлення { get; set; }
        public decimal ОсновнаЦіна { get; set; }
        public string Автор { get; set; }
        
        //Табличні частини
        
    }
    
    /// <summary> 
    /// Товаро-матеріальні цінності
    /// </summary>
    class ТМЦ_Pointer : DirectoryPointer
    {
        public ТМЦ_Pointer(object uid = null) : base(Config.Kernel, "tmc")
        {
            if (uid != null && uid != DBNull.Value) base.Init(new UnigueID((Guid)uid), null);
        }

        public ТМЦ_Objest GetDirectoryObject()
        {
            ТМЦ_Objest ТМЦObjestItem = new ТМЦ_Objest();
            ТМЦObjestItem.Read(base.UnigueID);
            return ТМЦObjestItem;
        }
    }
    
    /// <summary> 
    /// Товаро-матеріальні цінності
    /// </summary>
    class ТМЦ_Select : DirectorySelect
    {
        public ТМЦ_Select() : base(Config.Kernel, "tmc") { }
    
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
                Current = new ТМЦ_Pointer();
                Current.Init(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields);
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }

        public ТМЦ_Pointer Current { get; private set; }
    }
    
      
    
    #endregion
    
}
  
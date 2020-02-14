

/*
 *
 * Конфігурації "ConfTrade 1.1"
 * Автор Yurik
 * Дата конфігурації: 14.02.2020 08:09:52
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
}

namespace ConfTrade_v1_1.Directory
{
    
    #region DIRECTORY "Товари"
    
    ///<summary>
    ///Довідник Товари.
    ///</summary>
    class Товари_Objest : DirectoryObject
    {
        public Товари_Objest() : base(Config.Kernel, "tovary",
             new string[] { "name", "code", "count", "numer", "masiv", "artikul", "pointer1", "pointer2", "pointer3", "link_empty", "od2" }) 
        {
            Назва = "";
            Код = "";
            Кількість = 0;
            Номер = 0;
            Масив = new string[] { };
            Артикул = "";
            Вказівник1 = new ТМЦ_Pointer();
            Вказівник2 = new Товари_Pointer();
            Вказівник3 = new НовийДокумент_Pointer();
            Вказівник4 = new НовийДокумент_Pointer();
            od2 = new ОдиниціВиміру_Pointer();
            
            //Табличні частини
            Ціни_TablePart = new Товари_Ціни_TablePart(this);
            ОдиниціВиміру_TablePart = new Товари_ОдиниціВиміру_TablePart(this);
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Назва = base.FieldValue["name"].ToString();
                Код = base.FieldValue["code"].ToString();
                Кількість = (base.FieldValue["count"] != DBNull.Value) ? (int)base.FieldValue["count"] : 0;
                Номер = (base.FieldValue["numer"] != DBNull.Value) ? (decimal)base.FieldValue["numer"] : 0;
                Масив = (base.FieldValue["masiv"] != DBNull.Value) ? (string[])base.FieldValue["masiv"] : new string[] { };
                Артикул = base.FieldValue["artikul"].ToString();
                Вказівник1 = new ТМЦ_Pointer(base.FieldValue["pointer1"]);
                Вказівник2 = new Товари_Pointer(base.FieldValue["pointer2"]);
                Вказівник3 = new НовийДокумент_Pointer(base.FieldValue["pointer3"]);
                Вказівник4 = new НовийДокумент_Pointer(base.FieldValue["link_empty"]);
                od2 = new ОдиниціВиміру_Pointer(base.FieldValue["od2"]);
                
                BaseClear();
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
            base.FieldValue["pointer3"] = Вказівник3.UnigueID.UGuid;
            base.FieldValue["link_empty"] = Вказівник4.UnigueID.UGuid;
            base.FieldValue["od2"] = od2.UnigueID.UGuid;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public Товари_Pointer GetDirectoryPointer()
        {
            Товари_Pointer directoryPointer = new Товари_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public string Назва { get; set; }
        public string Код { get; set; }
        public int Кількість { get; set; }
        public decimal Номер { get; set; }
        public string[] Масив { get; set; }
        public string Артикул { get; set; }
        public ТМЦ_Pointer Вказівник1 { get; set; }
        public Товари_Pointer Вказівник2 { get; set; }
        public НовийДокумент_Pointer Вказівник3 { get; set; }
        public НовийДокумент_Pointer Вказівник4 { get; set; }
        public ОдиниціВиміру_Pointer od2 { get; set; }
        
        //Табличні частини
        public Товари_Ціни_TablePart Ціни_TablePart { get; set; }
        public Товари_ОдиниціВиміру_TablePart ОдиниціВиміру_TablePart { get; set; }
        
    }
    
    ///<summary>
    ///Довідник Товари.
    ///</summary>
    class Товари_Pointer : DirectoryPointer
    {
        public Товари_Pointer(object uid = null) : base(Config.Kernel, "tovary")
        {
            if (uid != null && uid != DBNull.Value) base.Init(new UnigueID((Guid)uid), null);
        }

        public Товари_Objest GetDirectoryObject()
        {
            Товари_Objest ТовариObjestItem = new Товари_Objest();
            ТовариObjestItem.Read(base.UnigueID);
            return ТовариObjestItem;
        }
    }
    
    ///<summary>
    ///Довідник Товари.
    ///</summary>
    class Товари_Select : DirectorySelect, IDisposable
    {
        public Товари_Select() : base(Config.Kernel, "tovary") { }
    
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
                Current = new Товари_Pointer();
                Current.Init(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields);
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }

        public Товари_Pointer Current { get; private set; }
    }
    
      ///<summary>
    ///Таблична частина Ціни.
    ///</summary>
    class Товари_Ціни_TablePart : DirectoryTablePart
    {
        public Товари_Ціни_TablePart(Товари_Objest owner) : base(Config.Kernel, "tovary_ceny_tablepart",
             new string[] { "name", "cena", "isnew", "date_update", "date_test", "times", "pointer_od", "new_doc" }) 
        {
            Owner = owner;
            Records = new List<Товари_Ціни_TablePartRecord>();
        }
        
        public Товари_Objest Owner { get; private set; }
        
        public List<Товари_Ціни_TablePartRecord> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Товари_Ціни_TablePartRecord record = new Товари_Ціни_TablePartRecord();

                record.Name = fieldValue["name"].ToString();
                record.Cena = (fieldValue["cena"] != DBNull.Value) ? (decimal)fieldValue["cena"] : 0;
                record.IsNew = (fieldValue["isnew"] != DBNull.Value) ? (int)fieldValue["isnew"] : 0;
                record.ДатаОбновлення = (fieldValue["date_update"] != DBNull.Value) ? DateTime.Parse(fieldValue["date_update"].ToString()) : DateTime.MinValue;
                record.Дата = (fieldValue["date_test"] != DBNull.Value) ? DateTime.Parse(fieldValue["date_test"].ToString()) : DateTime.MinValue;
                record.Час = (fieldValue["times"] != DBNull.Value) ? TimeSpan.Parse(fieldValue["times"].ToString()) : DateTime.MinValue.TimeOfDay;
                record.ОдВиміру = new ОдиниціВиміру_Pointer(fieldValue["pointer_od"]);
                record.NewDok = new НовийДокумент_Pointer(fieldValue["new_doc"]);
                
                Records.Add(record);
            }
            
            base.BaseClear();
        }
        
        /// <summary>
        /// Зберегти колекцію Records в базу.
        /// </summary>
        /// <param name="clear_all_before_save">
        /// Щоб не очищати всю колекцію в базі перед записом треба поставити clear_all_before_save = false.
        /// </param>
        public void Save(bool clear_all_before_save /*= true*/) 
        {
            if (Records.Count > 0)
            {
                base.BaseBeginTransaction();
                
                if (clear_all_before_save)
                    base.BaseDelete(Owner.UnigueID);

                foreach (Товари_Ціни_TablePartRecord record in Records)
                {
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                    fieldValue.Add("name", record.Name);
                    fieldValue.Add("cena", record.Cena);
                    fieldValue.Add("isnew", record.IsNew);
                    fieldValue.Add("date_update", record.ДатаОбновлення);
                    fieldValue.Add("date_test", record.Дата);
                    fieldValue.Add("times", record.Час);
                    fieldValue.Add("pointer_od", record.ОдВиміру.UnigueID.UGuid);
                    fieldValue.Add("new_doc", record.NewDok.UnigueID.UGuid);
                    
                    base.BaseSave(Owner.UnigueID, fieldValue);
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
    }
    
    ///<summary>
    ///Таблична частина Ціни.
    ///</summary>
    class Товари_Ціни_TablePartRecord : DirectoryTablePartRecord
    {
        public Товари_Ціни_TablePartRecord()
        {
            Name = "";
            Cena = 0;
            IsNew = 0;
            ДатаОбновлення = DateTime.MinValue;
            Дата = DateTime.MinValue;
            Час = DateTime.MinValue.TimeOfDay;
            ОдВиміру = new ОдиниціВиміру_Pointer();
            NewDok = new НовийДокумент_Pointer();
            
        }
        
        
        public Товари_Ціни_TablePartRecord(
            string _Name = "", decimal _Cena = 0, int _IsNew = 0, DateTime?  _ДатаОбновлення = null, DateTime?  _Дата = null, TimeSpan?  _Час = null, ОдиниціВиміру_Pointer _ОдВиміру = null, НовийДокумент_Pointer _NewDok = null)
        {
            Name = _Name;
            Cena = _Cena;
            IsNew = _IsNew;
            ДатаОбновлення = _ДатаОбновлення ?? DateTime.MinValue;
            Дата = _Дата ?? DateTime.MinValue;
            Час = _Час ?? DateTime.MinValue.TimeOfDay;
            ОдВиміру = _ОдВиміру ?? new ОдиниціВиміру_Pointer();
            NewDok = _NewDok ?? new НовийДокумент_Pointer();
            
        }
        public string Name { get; set; }
        public decimal Cena { get; set; }
        public int IsNew { get; set; }
        public DateTime ДатаОбновлення { get; set; }
        public DateTime Дата { get; set; }
        public TimeSpan Час { get; set; }
        public ОдиниціВиміру_Pointer ОдВиміру { get; set; }
        public НовийДокумент_Pointer NewDok { get; set; }
        
    }
      
    class Товари_ОдиниціВиміру_TablePart : DirectoryTablePart
    {
        public Товари_ОдиниціВиміру_TablePart(Товари_Objest owner) : base(Config.Kernel, "od_list",
             new string[] { "od_pointer" }) 
        {
            Owner = owner;
            Records = new List<Товари_ОдиниціВиміру_TablePartRecord>();
        }
        
        public Товари_Objest Owner { get; private set; }
        
        public List<Товари_ОдиниціВиміру_TablePartRecord> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Товари_ОдиниціВиміру_TablePartRecord record = new Товари_ОдиниціВиміру_TablePartRecord();

                record.Одиниця = new ОдиниціВиміру_Pointer(fieldValue["od_pointer"]);
                
                Records.Add(record);
            }
            
            base.BaseClear();
        }
        
        /// <summary>
        /// Зберегти колекцію Records в базу.
        /// </summary>
        /// <param name="clear_all_before_save">
        /// Щоб не очищати всю колекцію в базі перед записом треба поставити clear_all_before_save = false.
        /// </param>
        public void Save(bool clear_all_before_save /*= true*/) 
        {
            if (Records.Count > 0)
            {
                base.BaseBeginTransaction();
                
                if (clear_all_before_save)
                    base.BaseDelete(Owner.UnigueID);

                foreach (Товари_ОдиниціВиміру_TablePartRecord record in Records)
                {
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                    fieldValue.Add("od_pointer", record.Одиниця.UnigueID.UGuid);
                    
                    base.BaseSave(Owner.UnigueID, fieldValue);
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
    }
    
    
    class Товари_ОдиниціВиміру_TablePartRecord : DirectoryTablePartRecord
    {
        public Товари_ОдиниціВиміру_TablePartRecord()
        {
            Одиниця = new ОдиниціВиміру_Pointer();
            
        }
        
        
        public Товари_ОдиниціВиміру_TablePartRecord(
            ОдиниціВиміру_Pointer _Одиниця = null)
        {
            Одиниця = _Одиниця ?? new ОдиниціВиміру_Pointer();
            
        }
        public ОдиниціВиміру_Pointer Одиниця { get; set; }
        
    }
      ///<summary>
    ///Візуалізація 1.
    ///</summary>
    class Товари_ВибіркаТовари_View : DirectoryView
    {
        public Товари_ВибіркаТовари_View() : base(Config.Kernel, "tovary", 
             new string[] { "name", "code", "od2", "count", "numer", "masiv", "artikul", "pointer1", "pointer2", "pointer3", "link_empty", "od2" },
             new string[] { "Назва", "Код", "Одиниця", "Кількість", "Номер", "Масив", "Артикул", "Вказівник1", "Вказівник2", "Вказівник3", "Вказівник4", "od2" },
             "Товари_ВибіркаТовари")
        {
            base.QuerySelect.PrimaryField = "uid";
            
            
        }
        
    }
      ///<summary>
    ///Візуалізація Табличної частини Ціни елементу довідника.
    ///</summary>
    class Товари_ВибіркаЦіни_View : DirectoryView
    {
        public Товари_ВибіркаЦіни_View() : base(Config.Kernel, "tovary_ceny_tablepart", 
             new string[] { "name", "cena" },
             new string[] { "Назва", "Ціна" },
             "Товари_ВибіркаЦіни")
        {
            base.QuerySelect.PrimaryField = "owner";
            
            
        }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "Новий"
    
    ///<summary>
    ///new new.
    ///</summary>
    class Новий_Objest : DirectoryObject
    {
        public Новий_Objest() : base(Config.Kernel, "new",
             new string[] { "id", "name", "opys" }) 
        {
            Код = "";
            Назва = "";
            Опис = "";
            
            //Табличні частини
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Код = base.FieldValue["id"].ToString();
                Назва = base.FieldValue["name"].ToString();
                Опис = base.FieldValue["opys"].ToString();
                
                BaseClear();
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["id"] = Код;
            base.FieldValue["name"] = Назва;
            base.FieldValue["opys"] = Опис;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public Новий_Pointer GetDirectoryPointer()
        {
            Новий_Pointer directoryPointer = new Новий_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public string Код { get; set; }
        public string Назва { get; set; }
        public string Опис { get; set; }
        
        //Табличні частини
        
    }
    
    ///<summary>
    ///new new.
    ///</summary>
    class Новий_Pointer : DirectoryPointer
    {
        public Новий_Pointer(object uid = null) : base(Config.Kernel, "new")
        {
            if (uid != null && uid != DBNull.Value) base.Init(new UnigueID((Guid)uid), null);
        }

        public Новий_Objest GetDirectoryObject()
        {
            Новий_Objest НовийObjestItem = new Новий_Objest();
            НовийObjestItem.Read(base.UnigueID);
            return НовийObjestItem;
        }
    }
    
    ///<summary>
    ///new new.
    ///</summary>
    class Новий_Select : DirectorySelect, IDisposable
    {
        public Новий_Select() : base(Config.Kernel, "new") { }
    
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
                Current = new Новий_Pointer();
                Current.Init(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields);
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }

        public Новий_Pointer Current { get; private set; }
    }
    
      
    
    #endregion
    
    #region DIRECTORY "ТМЦ"
    
    ///<summary>
    ///Товаро-матеріальні цінності.
    ///</summary>
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
                
                BaseClear();
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
    
    ///<summary>
    ///Товаро-матеріальні цінності.
    ///</summary>
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
    
    ///<summary>
    ///Товаро-матеріальні цінності.
    ///</summary>
    class ТМЦ_Select : DirectorySelect, IDisposable
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
    
    #region DIRECTORY "Записки"
    
    
    class Записки_Objest : DirectoryObject
    {
        public Записки_Objest() : base(Config.Kernel, "zpysky_info",
             new string[] { "date_add", "zapyska", "site" }) 
        {
            ДатаЗапису = DateTime.MinValue;
            Записка = "";
            Сайт = "";
            
            //Табличні частини
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                ДатаЗапису = (base.FieldValue["date_add"] != DBNull.Value) ? DateTime.Parse(base.FieldValue["date_add"].ToString()) : DateTime.MinValue;
                Записка = base.FieldValue["zapyska"].ToString();
                Сайт = base.FieldValue["site"].ToString();
                
                BaseClear();
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["date_add"] = ДатаЗапису;
            base.FieldValue["zapyska"] = Записка;
            base.FieldValue["site"] = Сайт;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public Записки_Pointer GetDirectoryPointer()
        {
            Записки_Pointer directoryPointer = new Записки_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public DateTime ДатаЗапису { get; set; }
        public string Записка { get; set; }
        public string Сайт { get; set; }
        
        //Табличні частини
        
    }
    
    
    class Записки_Pointer : DirectoryPointer
    {
        public Записки_Pointer(object uid = null) : base(Config.Kernel, "zpysky_info")
        {
            if (uid != null && uid != DBNull.Value) base.Init(new UnigueID((Guid)uid), null);
        }

        public Записки_Objest GetDirectoryObject()
        {
            Записки_Objest ЗапискиObjestItem = new Записки_Objest();
            ЗапискиObjestItem.Read(base.UnigueID);
            return ЗапискиObjestItem;
        }
    }
    
    
    class Записки_Select : DirectorySelect, IDisposable
    {
        public Записки_Select() : base(Config.Kernel, "zpysky_info") { }
    
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
                Current = new Записки_Pointer();
                Current.Init(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields);
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }

        public Записки_Pointer Current { get; private set; }
    }
    
      
    
    #endregion
    
    #region DIRECTORY "ОдиниціВиміру"
    
    
    class ОдиниціВиміру_Objest : DirectoryObject
    {
        public ОдиниціВиміру_Objest() : base(Config.Kernel, "od_vimir",
             new string[] { "name", "code", "small_name" }) 
        {
            Назва = "";
            Код = "";
            КороткаНазва = "";
            
            //Табличні частини
            Історія_TablePart = new ОдиниціВиміру_Історія_TablePart(this);
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Назва = base.FieldValue["name"].ToString();
                Код = base.FieldValue["code"].ToString();
                КороткаНазва = base.FieldValue["small_name"].ToString();
                
                BaseClear();
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["name"] = Назва;
            base.FieldValue["code"] = Код;
            base.FieldValue["small_name"] = КороткаНазва;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public ОдиниціВиміру_Pointer GetDirectoryPointer()
        {
            ОдиниціВиміру_Pointer directoryPointer = new ОдиниціВиміру_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public string Назва { get; set; }
        public string Код { get; set; }
        public string КороткаНазва { get; set; }
        
        //Табличні частини
        public ОдиниціВиміру_Історія_TablePart Історія_TablePart { get; set; }
        
    }
    
    
    class ОдиниціВиміру_Pointer : DirectoryPointer
    {
        public ОдиниціВиміру_Pointer(object uid = null) : base(Config.Kernel, "od_vimir")
        {
            if (uid != null && uid != DBNull.Value) base.Init(new UnigueID((Guid)uid), null);
        }

        public ОдиниціВиміру_Objest GetDirectoryObject()
        {
            ОдиниціВиміру_Objest ОдиниціВиміруObjestItem = new ОдиниціВиміру_Objest();
            ОдиниціВиміруObjestItem.Read(base.UnigueID);
            return ОдиниціВиміруObjestItem;
        }
    }
    
    
    class ОдиниціВиміру_Select : DirectorySelect, IDisposable
    {
        public ОдиниціВиміру_Select() : base(Config.Kernel, "od_vimir") { }
    
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
                Current = new ОдиниціВиміру_Pointer();
                Current.Init(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields);
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }

        public ОдиниціВиміру_Pointer Current { get; private set; }
    }
    
      ///<summary>
    ///Історія.
    ///</summary>
    class ОдиниціВиміру_Історія_TablePart : DirectoryTablePart
    {
        public ОдиниціВиміру_Історія_TablePart(ОдиниціВиміру_Objest owner) : base(Config.Kernel, "history",
             new string[] { "date_save", "value" }) 
        {
            Owner = owner;
            Records = new List<ОдиниціВиміру_Історія_TablePartRecord>();
        }
        
        public ОдиниціВиміру_Objest Owner { get; private set; }
        
        public List<ОдиниціВиміру_Історія_TablePartRecord> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                ОдиниціВиміру_Історія_TablePartRecord record = new ОдиниціВиміру_Історія_TablePartRecord();

                record.ДатаЗапису = (fieldValue["date_save"] != DBNull.Value) ? DateTime.Parse(fieldValue["date_save"].ToString()) : DateTime.MinValue;
                record.Значення = fieldValue["value"].ToString();
                
                Records.Add(record);
            }
            
            base.BaseClear();
        }
        
        /// <summary>
        /// Зберегти колекцію Records в базу.
        /// </summary>
        /// <param name="clear_all_before_save">
        /// Щоб не очищати всю колекцію в базі перед записом треба поставити clear_all_before_save = false.
        /// </param>
        public void Save(bool clear_all_before_save /*= true*/) 
        {
            if (Records.Count > 0)
            {
                base.BaseBeginTransaction();
                
                if (clear_all_before_save)
                    base.BaseDelete(Owner.UnigueID);

                foreach (ОдиниціВиміру_Історія_TablePartRecord record in Records)
                {
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                    fieldValue.Add("date_save", record.ДатаЗапису);
                    fieldValue.Add("value", record.Значення);
                    
                    base.BaseSave(Owner.UnigueID, fieldValue);
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
    }
    
    ///<summary>
    ///Історія.
    ///</summary>
    class ОдиниціВиміру_Історія_TablePartRecord : DirectoryTablePartRecord
    {
        public ОдиниціВиміру_Історія_TablePartRecord()
        {
            ДатаЗапису = DateTime.MinValue;
            Значення = "";
            
        }
        
        
        public ОдиниціВиміру_Історія_TablePartRecord(
            DateTime?  _ДатаЗапису = null, string _Значення = "")
        {
            ДатаЗапису = _ДатаЗапису ?? DateTime.MinValue;
            Значення = _Значення;
            
        }
        public DateTime ДатаЗапису { get; set; }
        public string Значення { get; set; }
        
    }
      ///<summary>
    ///Візуалізація 1.
    ///</summary>
    class ОдиниціВиміру_Вибірка_View : DirectoryView
    {
        public ОдиниціВиміру_Вибірка_View() : base(Config.Kernel, "od_vimir", 
             new string[] { "name", "code" },
             new string[] { "Назва", "Код" },
             "ОдиниціВиміру_Вибірка")
        {
            base.QuerySelect.PrimaryField = "uid";
            
            
        }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "НовийДокумент"
    
    
    class НовийДокумент_Objest : DirectoryObject
    {
        public НовийДокумент_Objest() : base(Config.Kernel, "new_document",
             new string[] { "name" }) 
        {
            Назва = "";
            
            //Табличні частини
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Назва = base.FieldValue["name"].ToString();
                
                BaseClear();
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["name"] = Назва;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public НовийДокумент_Pointer GetDirectoryPointer()
        {
            НовийДокумент_Pointer directoryPointer = new НовийДокумент_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public string Назва { get; set; }
        
        //Табличні частини
        
    }
    
    
    class НовийДокумент_Pointer : DirectoryPointer
    {
        public НовийДокумент_Pointer(object uid = null) : base(Config.Kernel, "new_document")
        {
            if (uid != null && uid != DBNull.Value) base.Init(new UnigueID((Guid)uid), null);
        }

        public НовийДокумент_Objest GetDirectoryObject()
        {
            НовийДокумент_Objest НовийДокументObjestItem = new НовийДокумент_Objest();
            НовийДокументObjestItem.Read(base.UnigueID);
            return НовийДокументObjestItem;
        }
    }
    
    
    class НовийДокумент_Select : DirectorySelect, IDisposable
    {
        public НовийДокумент_Select() : base(Config.Kernel, "new_document") { }
    
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
                Current = new НовийДокумент_Pointer();
                Current.Init(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields);
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }

        public НовийДокумент_Pointer Current { get; private set; }
    }
    
      
    
    #endregion
    
    #region DIRECTORY "AAA"
    
    
    class AAA_Objest : DirectoryObject
    {
        public AAA_Objest() : base(Config.Kernel, "tab_b2",
             new string[] { "as", "asd", "col_a01", "col_a1", "col_a2" }) 
        {
            A2 = new bbb_Pointer();
            a2 = new bbb_Pointer();
            werqwer = "";
            A11 = new ccc_Pointer();
            A1 = "";
            
            //Табличні частини
            CCC_TablePart = new AAA_CCC_TablePart(this);
            bbb_TablePart = new AAA_bbb_TablePart(this);
            cccw_TablePart = new AAA_cccw_TablePart(this);
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                A2 = new bbb_Pointer(base.FieldValue["as"]);
                a2 = new bbb_Pointer(base.FieldValue["asd"]);
                werqwer = base.FieldValue["col_a01"].ToString();
                A11 = new ccc_Pointer(base.FieldValue["col_a1"]);
                A1 = base.FieldValue["col_a2"].ToString();
                
                BaseClear();
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["as"] = A2.UnigueID.UGuid;
            base.FieldValue["asd"] = a2.UnigueID.UGuid;
            base.FieldValue["col_a01"] = werqwer;
            base.FieldValue["col_a1"] = A11.UnigueID.UGuid;
            base.FieldValue["col_a2"] = A1;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public AAA_Pointer GetDirectoryPointer()
        {
            AAA_Pointer directoryPointer = new AAA_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public bbb_Pointer A2 { get; set; }
        public bbb_Pointer a2 { get; set; }
        public string werqwer { get; set; }
        public ccc_Pointer A11 { get; set; }
        public string A1 { get; set; }
        
        //Табличні частини
        public AAA_CCC_TablePart CCC_TablePart { get; set; }
        public AAA_bbb_TablePart bbb_TablePart { get; set; }
        public AAA_cccw_TablePart cccw_TablePart { get; set; }
        
    }
    
    
    class AAA_Pointer : DirectoryPointer
    {
        public AAA_Pointer(object uid = null) : base(Config.Kernel, "tab_b2")
        {
            if (uid != null && uid != DBNull.Value) base.Init(new UnigueID((Guid)uid), null);
        }

        public AAA_Objest GetDirectoryObject()
        {
            AAA_Objest AAAObjestItem = new AAA_Objest();
            AAAObjestItem.Read(base.UnigueID);
            return AAAObjestItem;
        }
    }
    
    
    class AAA_Select : DirectorySelect, IDisposable
    {
        public AAA_Select() : base(Config.Kernel, "tab_b2") { }
    
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
                Current = new AAA_Pointer();
                Current.Init(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields);
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }

        public AAA_Pointer Current { get; private set; }
    }
    
      
    class AAA_CCC_TablePart : DirectoryTablePart
    {
        public AAA_CCC_TablePart(AAA_Objest owner) : base(Config.Kernel, "tab_b4",
             new string[] { "aaa", "bbb" }) 
        {
            Owner = owner;
            Records = new List<AAA_CCC_TablePartRecord>();
        }
        
        public AAA_Objest Owner { get; private set; }
        
        public List<AAA_CCC_TablePartRecord> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                AAA_CCC_TablePartRecord record = new AAA_CCC_TablePartRecord();

                record.aaa = fieldValue["aaa"].ToString();
                record.bbbb = fieldValue["bbb"].ToString();
                
                Records.Add(record);
            }
            
            base.BaseClear();
        }
        
        /// <summary>
        /// Зберегти колекцію Records в базу.
        /// </summary>
        /// <param name="clear_all_before_save">
        /// Щоб не очищати всю колекцію в базі перед записом треба поставити clear_all_before_save = false.
        /// </param>
        public void Save(bool clear_all_before_save /*= true*/) 
        {
            if (Records.Count > 0)
            {
                base.BaseBeginTransaction();
                
                if (clear_all_before_save)
                    base.BaseDelete(Owner.UnigueID);

                foreach (AAA_CCC_TablePartRecord record in Records)
                {
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                    fieldValue.Add("aaa", record.aaa);
                    fieldValue.Add("bbb", record.bbbb);
                    
                    base.BaseSave(Owner.UnigueID, fieldValue);
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
    }
    
    
    class AAA_CCC_TablePartRecord : DirectoryTablePartRecord
    {
        public AAA_CCC_TablePartRecord()
        {
            aaa = "";
            bbbb = "";
            
        }
        
        
        public AAA_CCC_TablePartRecord(
            string _aaa = "", string _bbbb = "")
        {
            aaa = _aaa;
            bbbb = _bbbb;
            
        }
        public string aaa { get; set; }
        public string bbbb { get; set; }
        
    }
      
    class AAA_bbb_TablePart : DirectoryTablePart
    {
        public AAA_bbb_TablePart(AAA_Objest owner) : base(Config.Kernel, "tab_b5",
             new string[] {  }) 
        {
            Owner = owner;
            Records = new List<AAA_bbb_TablePartRecord>();
        }
        
        public AAA_Objest Owner { get; private set; }
        
        public List<AAA_bbb_TablePartRecord> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                AAA_bbb_TablePartRecord record = new AAA_bbb_TablePartRecord();

                
                Records.Add(record);
            }
            
            base.BaseClear();
        }
        
        /// <summary>
        /// Зберегти колекцію Records в базу.
        /// </summary>
        /// <param name="clear_all_before_save">
        /// Щоб не очищати всю колекцію в базі перед записом треба поставити clear_all_before_save = false.
        /// </param>
        public void Save(bool clear_all_before_save /*= true*/) 
        {
            if (Records.Count > 0)
            {
                base.BaseBeginTransaction();
                
                if (clear_all_before_save)
                    base.BaseDelete(Owner.UnigueID);

                foreach (AAA_bbb_TablePartRecord record in Records)
                {
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                    
                    base.BaseSave(Owner.UnigueID, fieldValue);
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
    }
    
    
    class AAA_bbb_TablePartRecord : DirectoryTablePartRecord
    {
        public AAA_bbb_TablePartRecord()
        {
            
        }
        
        
    }
      
    class AAA_cccw_TablePart : DirectoryTablePart
    {
        public AAA_cccw_TablePart(AAA_Objest owner) : base(Config.Kernel, "tab_a09",
             new string[] {  }) 
        {
            Owner = owner;
            Records = new List<AAA_cccw_TablePartRecord>();
        }
        
        public AAA_Objest Owner { get; private set; }
        
        public List<AAA_cccw_TablePartRecord> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                AAA_cccw_TablePartRecord record = new AAA_cccw_TablePartRecord();

                
                Records.Add(record);
            }
            
            base.BaseClear();
        }
        
        /// <summary>
        /// Зберегти колекцію Records в базу.
        /// </summary>
        /// <param name="clear_all_before_save">
        /// Щоб не очищати всю колекцію в базі перед записом треба поставити clear_all_before_save = false.
        /// </param>
        public void Save(bool clear_all_before_save /*= true*/) 
        {
            if (Records.Count > 0)
            {
                base.BaseBeginTransaction();
                
                if (clear_all_before_save)
                    base.BaseDelete(Owner.UnigueID);

                foreach (AAA_cccw_TablePartRecord record in Records)
                {
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                    
                    base.BaseSave(Owner.UnigueID, fieldValue);
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
    }
    
    
    class AAA_cccw_TablePartRecord : DirectoryTablePartRecord
    {
        public AAA_cccw_TablePartRecord()
        {
            
        }
        
        
    }
      
    
    #endregion
    
    #region DIRECTORY "bbb"
    
    
    class bbb_Objest : DirectoryObject
    {
        public bbb_Objest() : base(Config.Kernel, "tab_b6",
             new string[] { "aaa" }) 
        {
            aaa = "";
            
            //Табличні частини
            aaa_TablePart = new bbb_aaa_TablePart(this);
            bbb_TablePart = new bbb_bbb_TablePart(this);
            ccc_TablePart = new bbb_ccc_TablePart(this);
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                aaa = base.FieldValue["aaa"].ToString();
                
                BaseClear();
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["aaa"] = aaa;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public bbb_Pointer GetDirectoryPointer()
        {
            bbb_Pointer directoryPointer = new bbb_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public string aaa { get; set; }
        
        //Табличні частини
        public bbb_aaa_TablePart aaa_TablePart { get; set; }
        public bbb_bbb_TablePart bbb_TablePart { get; set; }
        public bbb_ccc_TablePart ccc_TablePart { get; set; }
        
    }
    
    
    class bbb_Pointer : DirectoryPointer
    {
        public bbb_Pointer(object uid = null) : base(Config.Kernel, "tab_b6")
        {
            if (uid != null && uid != DBNull.Value) base.Init(new UnigueID((Guid)uid), null);
        }

        public bbb_Objest GetDirectoryObject()
        {
            bbb_Objest bbbObjestItem = new bbb_Objest();
            bbbObjestItem.Read(base.UnigueID);
            return bbbObjestItem;
        }
    }
    
    
    class bbb_Select : DirectorySelect, IDisposable
    {
        public bbb_Select() : base(Config.Kernel, "tab_b6") { }
    
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
                Current = new bbb_Pointer();
                Current.Init(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields);
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }

        public bbb_Pointer Current { get; private set; }
    }
    
      
    class bbb_aaa_TablePart : DirectoryTablePart
    {
        public bbb_aaa_TablePart(bbb_Objest owner) : base(Config.Kernel, "tab_b7",
             new string[] { "aaa", "bbb" }) 
        {
            Owner = owner;
            Records = new List<bbb_aaa_TablePartRecord>();
        }
        
        public bbb_Objest Owner { get; private set; }
        
        public List<bbb_aaa_TablePartRecord> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                bbb_aaa_TablePartRecord record = new bbb_aaa_TablePartRecord();

                record.aaa = fieldValue["aaa"].ToString();
                record.bbb = fieldValue["bbb"].ToString();
                
                Records.Add(record);
            }
            
            base.BaseClear();
        }
        
        /// <summary>
        /// Зберегти колекцію Records в базу.
        /// </summary>
        /// <param name="clear_all_before_save">
        /// Щоб не очищати всю колекцію в базі перед записом треба поставити clear_all_before_save = false.
        /// </param>
        public void Save(bool clear_all_before_save /*= true*/) 
        {
            if (Records.Count > 0)
            {
                base.BaseBeginTransaction();
                
                if (clear_all_before_save)
                    base.BaseDelete(Owner.UnigueID);

                foreach (bbb_aaa_TablePartRecord record in Records)
                {
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                    fieldValue.Add("aaa", record.aaa);
                    fieldValue.Add("bbb", record.bbb);
                    
                    base.BaseSave(Owner.UnigueID, fieldValue);
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
    }
    
    
    class bbb_aaa_TablePartRecord : DirectoryTablePartRecord
    {
        public bbb_aaa_TablePartRecord()
        {
            aaa = "";
            bbb = "";
            
        }
        
        
        public bbb_aaa_TablePartRecord(
            string _aaa = "", string _bbb = "")
        {
            aaa = _aaa;
            bbb = _bbb;
            
        }
        public string aaa { get; set; }
        public string bbb { get; set; }
        
    }
      
    class bbb_bbb_TablePart : DirectoryTablePart
    {
        public bbb_bbb_TablePart(bbb_Objest owner) : base(Config.Kernel, "tab_b8",
             new string[] { "fff" }) 
        {
            Owner = owner;
            Records = new List<bbb_bbb_TablePartRecord>();
        }
        
        public bbb_Objest Owner { get; private set; }
        
        public List<bbb_bbb_TablePartRecord> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                bbb_bbb_TablePartRecord record = new bbb_bbb_TablePartRecord();

                record.fff = fieldValue["fff"].ToString();
                
                Records.Add(record);
            }
            
            base.BaseClear();
        }
        
        /// <summary>
        /// Зберегти колекцію Records в базу.
        /// </summary>
        /// <param name="clear_all_before_save">
        /// Щоб не очищати всю колекцію в базі перед записом треба поставити clear_all_before_save = false.
        /// </param>
        public void Save(bool clear_all_before_save /*= true*/) 
        {
            if (Records.Count > 0)
            {
                base.BaseBeginTransaction();
                
                if (clear_all_before_save)
                    base.BaseDelete(Owner.UnigueID);

                foreach (bbb_bbb_TablePartRecord record in Records)
                {
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                    fieldValue.Add("fff", record.fff);
                    
                    base.BaseSave(Owner.UnigueID, fieldValue);
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
    }
    
    
    class bbb_bbb_TablePartRecord : DirectoryTablePartRecord
    {
        public bbb_bbb_TablePartRecord()
        {
            fff = "";
            
        }
        
        
        public bbb_bbb_TablePartRecord(
            string _fff = "")
        {
            fff = _fff;
            
        }
        public string fff { get; set; }
        
    }
      
    class bbb_ccc_TablePart : DirectoryTablePart
    {
        public bbb_ccc_TablePart(bbb_Objest owner) : base(Config.Kernel, "tab_b9",
             new string[] { "aaa", "bbb" }) 
        {
            Owner = owner;
            Records = new List<bbb_ccc_TablePartRecord>();
        }
        
        public bbb_Objest Owner { get; private set; }
        
        public List<bbb_ccc_TablePartRecord> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                bbb_ccc_TablePartRecord record = new bbb_ccc_TablePartRecord();

                record.aaa = fieldValue["aaa"].ToString();
                record.bbb = fieldValue["bbb"].ToString();
                
                Records.Add(record);
            }
            
            base.BaseClear();
        }
        
        /// <summary>
        /// Зберегти колекцію Records в базу.
        /// </summary>
        /// <param name="clear_all_before_save">
        /// Щоб не очищати всю колекцію в базі перед записом треба поставити clear_all_before_save = false.
        /// </param>
        public void Save(bool clear_all_before_save /*= true*/) 
        {
            if (Records.Count > 0)
            {
                base.BaseBeginTransaction();
                
                if (clear_all_before_save)
                    base.BaseDelete(Owner.UnigueID);

                foreach (bbb_ccc_TablePartRecord record in Records)
                {
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                    fieldValue.Add("aaa", record.aaa);
                    fieldValue.Add("bbb", record.bbb);
                    
                    base.BaseSave(Owner.UnigueID, fieldValue);
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
    }
    
    
    class bbb_ccc_TablePartRecord : DirectoryTablePartRecord
    {
        public bbb_ccc_TablePartRecord()
        {
            aaa = "";
            bbb = "";
            
        }
        
        
        public bbb_ccc_TablePartRecord(
            string _aaa = "", string _bbb = "")
        {
            aaa = _aaa;
            bbb = _bbb;
            
        }
        public string aaa { get; set; }
        public string bbb { get; set; }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "ccc"
    
    
    class ccc_Objest : DirectoryObject
    {
        public ccc_Objest() : base(Config.Kernel, "tab_c1",
             new string[] { "aaa" }) 
        {
            aaa = "";
            
            //Табличні частини
            aaa_TablePart = new ccc_aaa_TablePart(this);
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                aaa = base.FieldValue["aaa"].ToString();
                
                BaseClear();
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["aaa"] = aaa;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public ccc_Pointer GetDirectoryPointer()
        {
            ccc_Pointer directoryPointer = new ccc_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public string aaa { get; set; }
        
        //Табличні частини
        public ccc_aaa_TablePart aaa_TablePart { get; set; }
        
    }
    
    
    class ccc_Pointer : DirectoryPointer
    {
        public ccc_Pointer(object uid = null) : base(Config.Kernel, "tab_c1")
        {
            if (uid != null && uid != DBNull.Value) base.Init(new UnigueID((Guid)uid), null);
        }

        public ccc_Objest GetDirectoryObject()
        {
            ccc_Objest cccObjestItem = new ccc_Objest();
            cccObjestItem.Read(base.UnigueID);
            return cccObjestItem;
        }
    }
    
    
    class ccc_Select : DirectorySelect, IDisposable
    {
        public ccc_Select() : base(Config.Kernel, "tab_c1") { }
    
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
                Current = new ccc_Pointer();
                Current.Init(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields);
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }

        public ccc_Pointer Current { get; private set; }
    }
    
      
    class ccc_aaa_TablePart : DirectoryTablePart
    {
        public ccc_aaa_TablePart(ccc_Objest owner) : base(Config.Kernel, "tab_c2",
             new string[] { "aaa" }) 
        {
            Owner = owner;
            Records = new List<ccc_aaa_TablePartRecord>();
        }
        
        public ccc_Objest Owner { get; private set; }
        
        public List<ccc_aaa_TablePartRecord> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                ccc_aaa_TablePartRecord record = new ccc_aaa_TablePartRecord();

                record.aaa = fieldValue["aaa"].ToString();
                
                Records.Add(record);
            }
            
            base.BaseClear();
        }
        
        /// <summary>
        /// Зберегти колекцію Records в базу.
        /// </summary>
        /// <param name="clear_all_before_save">
        /// Щоб не очищати всю колекцію в базі перед записом треба поставити clear_all_before_save = false.
        /// </param>
        public void Save(bool clear_all_before_save /*= true*/) 
        {
            if (Records.Count > 0)
            {
                base.BaseBeginTransaction();
                
                if (clear_all_before_save)
                    base.BaseDelete(Owner.UnigueID);

                foreach (ccc_aaa_TablePartRecord record in Records)
                {
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                    fieldValue.Add("aaa", record.aaa);
                    
                    base.BaseSave(Owner.UnigueID, fieldValue);
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
    }
    
    
    class ccc_aaa_TablePartRecord : DirectoryTablePartRecord
    {
        public ccc_aaa_TablePartRecord()
        {
            aaa = "";
            
        }
        
        
        public ccc_aaa_TablePartRecord(
            string _aaa = "")
        {
            aaa = _aaa;
            
        }
        public string aaa { get; set; }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "dddd"
    
    
    class dddd_Objest : DirectoryObject
    {
        public dddd_Objest() : base(Config.Kernel, "tab_c3",
             new string[] { "col_a01", "col_a02", "col_a03" }) 
        {
            werwqer = "";
            werqwerq = "";
            werwer = "";
            
            //Табличні частини
            dddd_TablePart = new dddd_dddd_TablePart(this);
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                werwqer = base.FieldValue["col_a01"].ToString();
                werqwerq = base.FieldValue["col_a02"].ToString();
                werwer = base.FieldValue["col_a03"].ToString();
                
                BaseClear();
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["col_a01"] = werwqer;
            base.FieldValue["col_a02"] = werqwerq;
            base.FieldValue["col_a03"] = werwer;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public dddd_Pointer GetDirectoryPointer()
        {
            dddd_Pointer directoryPointer = new dddd_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public string werwqer { get; set; }
        public string werqwerq { get; set; }
        public string werwer { get; set; }
        
        //Табличні частини
        public dddd_dddd_TablePart dddd_TablePart { get; set; }
        
    }
    
    
    class dddd_Pointer : DirectoryPointer
    {
        public dddd_Pointer(object uid = null) : base(Config.Kernel, "tab_c3")
        {
            if (uid != null && uid != DBNull.Value) base.Init(new UnigueID((Guid)uid), null);
        }

        public dddd_Objest GetDirectoryObject()
        {
            dddd_Objest ddddObjestItem = new dddd_Objest();
            ddddObjestItem.Read(base.UnigueID);
            return ddddObjestItem;
        }
    }
    
    
    class dddd_Select : DirectorySelect, IDisposable
    {
        public dddd_Select() : base(Config.Kernel, "tab_c3") { }
    
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
                Current = new dddd_Pointer();
                Current.Init(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields);
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }

        public dddd_Pointer Current { get; private set; }
    }
    
      
    class dddd_dddd_TablePart : DirectoryTablePart
    {
        public dddd_dddd_TablePart(dddd_Objest owner) : base(Config.Kernel, "tab_c4",
             new string[] { "dddd" }) 
        {
            Owner = owner;
            Records = new List<dddd_dddd_TablePartRecord>();
        }
        
        public dddd_Objest Owner { get; private set; }
        
        public List<dddd_dddd_TablePartRecord> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                dddd_dddd_TablePartRecord record = new dddd_dddd_TablePartRecord();

                record.dddd = fieldValue["dddd"].ToString();
                
                Records.Add(record);
            }
            
            base.BaseClear();
        }
        
        /// <summary>
        /// Зберегти колекцію Records в базу.
        /// </summary>
        /// <param name="clear_all_before_save">
        /// Щоб не очищати всю колекцію в базі перед записом треба поставити clear_all_before_save = false.
        /// </param>
        public void Save(bool clear_all_before_save /*= true*/) 
        {
            if (Records.Count > 0)
            {
                base.BaseBeginTransaction();
                
                if (clear_all_before_save)
                    base.BaseDelete(Owner.UnigueID);

                foreach (dddd_dddd_TablePartRecord record in Records)
                {
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                    fieldValue.Add("dddd", record.dddd);
                    
                    base.BaseSave(Owner.UnigueID, fieldValue);
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
    }
    
    
    class dddd_dddd_TablePartRecord : DirectoryTablePartRecord
    {
        public dddd_dddd_TablePartRecord()
        {
            dddd = "";
            
        }
        
        
        public dddd_dddd_TablePartRecord(
            string _dddd = "")
        {
            dddd = _dddd;
            
        }
        public string dddd { get; set; }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "werwer"
    
    
    class werwer_Objest : DirectoryObject
    {
        public werwer_Objest() : base(Config.Kernel, "tab_a01",
             new string[] { "col_a01", "col_a02" }) 
        {
            erwerqwe = "";
            wrwerqwer = "";
            
            //Табличні частини
            werwe_TablePart = new werwer_werwe_TablePart(this);
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                erwerqwe = base.FieldValue["col_a01"].ToString();
                wrwerqwer = base.FieldValue["col_a02"].ToString();
                
                BaseClear();
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["col_a01"] = erwerqwe;
            base.FieldValue["col_a02"] = wrwerqwer;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public werwer_Pointer GetDirectoryPointer()
        {
            werwer_Pointer directoryPointer = new werwer_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public string erwerqwe { get; set; }
        public string wrwerqwer { get; set; }
        
        //Табличні частини
        public werwer_werwe_TablePart werwe_TablePart { get; set; }
        
    }
    
    
    class werwer_Pointer : DirectoryPointer
    {
        public werwer_Pointer(object uid = null) : base(Config.Kernel, "tab_a01")
        {
            if (uid != null && uid != DBNull.Value) base.Init(new UnigueID((Guid)uid), null);
        }

        public werwer_Objest GetDirectoryObject()
        {
            werwer_Objest werwerObjestItem = new werwer_Objest();
            werwerObjestItem.Read(base.UnigueID);
            return werwerObjestItem;
        }
    }
    
    
    class werwer_Select : DirectorySelect, IDisposable
    {
        public werwer_Select() : base(Config.Kernel, "tab_a01") { }
    
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
                Current = new werwer_Pointer();
                Current.Init(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields);
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }

        public werwer_Pointer Current { get; private set; }
    }
    
      
    class werwer_werwe_TablePart : DirectoryTablePart
    {
        public werwer_werwe_TablePart(werwer_Objest owner) : base(Config.Kernel, "tab_a02",
             new string[] {  }) 
        {
            Owner = owner;
            Records = new List<werwer_werwe_TablePartRecord>();
        }
        
        public werwer_Objest Owner { get; private set; }
        
        public List<werwer_werwe_TablePartRecord> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                werwer_werwe_TablePartRecord record = new werwer_werwe_TablePartRecord();

                
                Records.Add(record);
            }
            
            base.BaseClear();
        }
        
        /// <summary>
        /// Зберегти колекцію Records в базу.
        /// </summary>
        /// <param name="clear_all_before_save">
        /// Щоб не очищати всю колекцію в базі перед записом треба поставити clear_all_before_save = false.
        /// </param>
        public void Save(bool clear_all_before_save /*= true*/) 
        {
            if (Records.Count > 0)
            {
                base.BaseBeginTransaction();
                
                if (clear_all_before_save)
                    base.BaseDelete(Owner.UnigueID);

                foreach (werwer_werwe_TablePartRecord record in Records)
                {
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                    
                    base.BaseSave(Owner.UnigueID, fieldValue);
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
    }
    
    
    class werwer_werwe_TablePartRecord : DirectoryTablePartRecord
    {
        public werwer_werwe_TablePartRecord()
        {
            
        }
        
        
    }
      
    
    #endregion
    
    #region DIRECTORY "sdfsdfs"
    
    
    class sdfsdfs_Objest : DirectoryObject
    {
        public sdfsdfs_Objest() : base(Config.Kernel, "tab_a04",
             new string[] {  }) 
        {
            
            //Табличні частини
            werw_TablePart = new sdfsdfs_werw_TablePart(this);
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                
                BaseClear();
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
        
        public sdfsdfs_Pointer GetDirectoryPointer()
        {
            sdfsdfs_Pointer directoryPointer = new sdfsdfs_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        
        //Табличні частини
        public sdfsdfs_werw_TablePart werw_TablePart { get; set; }
        
    }
    
    
    class sdfsdfs_Pointer : DirectoryPointer
    {
        public sdfsdfs_Pointer(object uid = null) : base(Config.Kernel, "tab_a04")
        {
            if (uid != null && uid != DBNull.Value) base.Init(new UnigueID((Guid)uid), null);
        }

        public sdfsdfs_Objest GetDirectoryObject()
        {
            sdfsdfs_Objest sdfsdfsObjestItem = new sdfsdfs_Objest();
            sdfsdfsObjestItem.Read(base.UnigueID);
            return sdfsdfsObjestItem;
        }
    }
    
    
    class sdfsdfs_Select : DirectorySelect, IDisposable
    {
        public sdfsdfs_Select() : base(Config.Kernel, "tab_a04") { }
    
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
                Current = new sdfsdfs_Pointer();
                Current.Init(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields);
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }

        public sdfsdfs_Pointer Current { get; private set; }
    }
    
      
    class sdfsdfs_werw_TablePart : DirectoryTablePart
    {
        public sdfsdfs_werw_TablePart(sdfsdfs_Objest owner) : base(Config.Kernel, "tab_a10",
             new string[] { "col_a1" }) 
        {
            Owner = owner;
            Records = new List<sdfsdfs_werw_TablePartRecord>();
        }
        
        public sdfsdfs_Objest Owner { get; private set; }
        
        public List<sdfsdfs_werw_TablePartRecord> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                sdfsdfs_werw_TablePartRecord record = new sdfsdfs_werw_TablePartRecord();

                record.werwe = fieldValue["col_a1"].ToString();
                
                Records.Add(record);
            }
            
            base.BaseClear();
        }
        
        /// <summary>
        /// Зберегти колекцію Records в базу.
        /// </summary>
        /// <param name="clear_all_before_save">
        /// Щоб не очищати всю колекцію в базі перед записом треба поставити clear_all_before_save = false.
        /// </param>
        public void Save(bool clear_all_before_save /*= true*/) 
        {
            if (Records.Count > 0)
            {
                base.BaseBeginTransaction();
                
                if (clear_all_before_save)
                    base.BaseDelete(Owner.UnigueID);

                foreach (sdfsdfs_werw_TablePartRecord record in Records)
                {
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                    fieldValue.Add("col_a1", record.werwe);
                    
                    base.BaseSave(Owner.UnigueID, fieldValue);
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
    }
    
    
    class sdfsdfs_werw_TablePartRecord : DirectoryTablePartRecord
    {
        public sdfsdfs_werw_TablePartRecord()
        {
            werwe = "";
            
        }
        
        
        public sdfsdfs_werw_TablePartRecord(
            string _werwe = "")
        {
            werwe = _werwe;
            
        }
        public string werwe { get; set; }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "dfsdfs"
    
    
    class dfsdfs_Objest : DirectoryObject
    {
        public dfsdfs_Objest() : base(Config.Kernel, "tab_a03",
             new string[] {  }) 
        {
            
            //Табличні частини
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                
                BaseClear();
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
        
        public dfsdfs_Pointer GetDirectoryPointer()
        {
            dfsdfs_Pointer directoryPointer = new dfsdfs_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        
        //Табличні частини
        
    }
    
    
    class dfsdfs_Pointer : DirectoryPointer
    {
        public dfsdfs_Pointer(object uid = null) : base(Config.Kernel, "tab_a03")
        {
            if (uid != null && uid != DBNull.Value) base.Init(new UnigueID((Guid)uid), null);
        }

        public dfsdfs_Objest GetDirectoryObject()
        {
            dfsdfs_Objest dfsdfsObjestItem = new dfsdfs_Objest();
            dfsdfsObjestItem.Read(base.UnigueID);
            return dfsdfsObjestItem;
        }
    }
    
    
    class dfsdfs_Select : DirectorySelect, IDisposable
    {
        public dfsdfs_Select() : base(Config.Kernel, "tab_a03") { }
    
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
                Current = new dfsdfs_Pointer();
                Current.Init(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields);
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }

        public dfsdfs_Pointer Current { get; private set; }
    }
    
      
    
    #endregion
    
    #region DIRECTORY "werw1"
    
    
    class werw1_Objest : DirectoryObject
    {
        public werw1_Objest() : base(Config.Kernel, "tab_a05",
             new string[] { "col_a01", "col_a02", "col_a1", "col_a2", "col_a3" }) 
        {
            werqwerq = "";
            werqwerqw = "";
            w = "";
            ww = "";
            wwwe = "";
            
            //Табличні частини
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                werqwerq = base.FieldValue["col_a01"].ToString();
                werqwerqw = base.FieldValue["col_a02"].ToString();
                w = base.FieldValue["col_a1"].ToString();
                ww = base.FieldValue["col_a2"].ToString();
                wwwe = base.FieldValue["col_a3"].ToString();
                
                BaseClear();
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["col_a01"] = werqwerq;
            base.FieldValue["col_a02"] = werqwerqw;
            base.FieldValue["col_a1"] = w;
            base.FieldValue["col_a2"] = ww;
            base.FieldValue["col_a3"] = wwwe;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public werw1_Pointer GetDirectoryPointer()
        {
            werw1_Pointer directoryPointer = new werw1_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public string werqwerq { get; set; }
        public string werqwerqw { get; set; }
        public string w { get; set; }
        public string ww { get; set; }
        public string wwwe { get; set; }
        
        //Табличні частини
        
    }
    
    
    class werw1_Pointer : DirectoryPointer
    {
        public werw1_Pointer(object uid = null) : base(Config.Kernel, "tab_a05")
        {
            if (uid != null && uid != DBNull.Value) base.Init(new UnigueID((Guid)uid), null);
        }

        public werw1_Objest GetDirectoryObject()
        {
            werw1_Objest werw1ObjestItem = new werw1_Objest();
            werw1ObjestItem.Read(base.UnigueID);
            return werw1ObjestItem;
        }
    }
    
    
    class werw1_Select : DirectorySelect, IDisposable
    {
        public werw1_Select() : base(Config.Kernel, "tab_a05") { }
    
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
                Current = new werw1_Pointer();
                Current.Init(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields);
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }

        public werw1_Pointer Current { get; private set; }
    }
    
      
    
    #endregion
    
    #region DIRECTORY "dfasdfa"
    
    
    class dfasdfa_Objest : DirectoryObject
    {
        public dfasdfa_Objest() : base(Config.Kernel, "tab_a07",
             new string[] { "col_a3", "col_a1", "col_a7", "col_a8", "col_a9", "col_b1", "col_b2", "col_b3", "col_a2", "col_a4", "col_a5", "col_a6" }) 
        {
            erter = "";
            ertwertw = "";
            ertwertwsdfas = "";
            sdfsdf = "";
            sdfasdfa = "";
            fasdfa = "";
            sdfasda = "";
            sdfasdfaf = "";
            еуіе = "";
            уек = "";
            test = "";
            test2 = "";
            
            //Табличні частини
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                erter = base.FieldValue["col_a3"].ToString();
                ertwertw = base.FieldValue["col_a1"].ToString();
                ertwertwsdfas = base.FieldValue["col_a7"].ToString();
                sdfsdf = base.FieldValue["col_a8"].ToString();
                sdfasdfa = base.FieldValue["col_a9"].ToString();
                fasdfa = base.FieldValue["col_b1"].ToString();
                sdfasda = base.FieldValue["col_b2"].ToString();
                sdfasdfaf = base.FieldValue["col_b3"].ToString();
                еуіе = base.FieldValue["col_a2"].ToString();
                уек = base.FieldValue["col_a4"].ToString();
                test = base.FieldValue["col_a5"].ToString();
                test2 = base.FieldValue["col_a6"].ToString();
                
                BaseClear();
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["col_a3"] = erter;
            base.FieldValue["col_a1"] = ertwertw;
            base.FieldValue["col_a7"] = ertwertwsdfas;
            base.FieldValue["col_a8"] = sdfsdf;
            base.FieldValue["col_a9"] = sdfasdfa;
            base.FieldValue["col_b1"] = fasdfa;
            base.FieldValue["col_b2"] = sdfasda;
            base.FieldValue["col_b3"] = sdfasdfaf;
            base.FieldValue["col_a2"] = еуіе;
            base.FieldValue["col_a4"] = уек;
            base.FieldValue["col_a5"] = test;
            base.FieldValue["col_a6"] = test2;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public dfasdfa_Pointer GetDirectoryPointer()
        {
            dfasdfa_Pointer directoryPointer = new dfasdfa_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public string erter { get; set; }
        public string ertwertw { get; set; }
        public string ertwertwsdfas { get; set; }
        public string sdfsdf { get; set; }
        public string sdfasdfa { get; set; }
        public string fasdfa { get; set; }
        public string sdfasda { get; set; }
        public string sdfasdfaf { get; set; }
        public string еуіе { get; set; }
        public string уек { get; set; }
        public string test { get; set; }
        public string test2 { get; set; }
        
        //Табличні частини
        
    }
    
    
    class dfasdfa_Pointer : DirectoryPointer
    {
        public dfasdfa_Pointer(object uid = null) : base(Config.Kernel, "tab_a07")
        {
            if (uid != null && uid != DBNull.Value) base.Init(new UnigueID((Guid)uid), null);
        }

        public dfasdfa_Objest GetDirectoryObject()
        {
            dfasdfa_Objest dfasdfaObjestItem = new dfasdfa_Objest();
            dfasdfaObjestItem.Read(base.UnigueID);
            return dfasdfaObjestItem;
        }
    }
    
    
    class dfasdfa_Select : DirectorySelect, IDisposable
    {
        public dfasdfa_Select() : base(Config.Kernel, "tab_a07") { }
    
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
                Current = new dfasdfa_Pointer();
                Current.Init(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields);
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }

        public dfasdfa_Pointer Current { get; private set; }
    }
    
      
    
    #endregion
    
    #region DIRECTORY "sdfasdf"
    
    
    class sdfasdf_Objest : DirectoryObject
    {
        public sdfasdf_Objest() : base(Config.Kernel, "tab_a06",
             new string[] { "col_a4", "col_a2", "col_a6" }) 
        {
            retert = "";
            ertwert = "";
            ertwertw = "";
            
            //Табличні частини
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                retert = base.FieldValue["col_a4"].ToString();
                ertwert = base.FieldValue["col_a2"].ToString();
                ertwertw = base.FieldValue["col_a6"].ToString();
                
                BaseClear();
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["col_a4"] = retert;
            base.FieldValue["col_a2"] = ertwert;
            base.FieldValue["col_a6"] = ertwertw;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public sdfasdf_Pointer GetDirectoryPointer()
        {
            sdfasdf_Pointer directoryPointer = new sdfasdf_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public string retert { get; set; }
        public string ertwert { get; set; }
        public string ertwertw { get; set; }
        
        //Табличні частини
        
    }
    
    
    class sdfasdf_Pointer : DirectoryPointer
    {
        public sdfasdf_Pointer(object uid = null) : base(Config.Kernel, "tab_a06")
        {
            if (uid != null && uid != DBNull.Value) base.Init(new UnigueID((Guid)uid), null);
        }

        public sdfasdf_Objest GetDirectoryObject()
        {
            sdfasdf_Objest sdfasdfObjestItem = new sdfasdf_Objest();
            sdfasdfObjestItem.Read(base.UnigueID);
            return sdfasdfObjestItem;
        }
    }
    
    
    class sdfasdf_Select : DirectorySelect, IDisposable
    {
        public sdfasdf_Select() : base(Config.Kernel, "tab_a06") { }
    
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
                Current = new sdfasdf_Pointer();
                Current.Init(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields);
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }

        public sdfasdf_Pointer Current { get; private set; }
    }
    
      
    
    #endregion
    
    #region DIRECTORY "aaa"
    
    
    class aaa_Objest : DirectoryObject
    {
        public aaa_Objest() : base(Config.Kernel, "tab_a08",
             new string[] { "col_a1", "col_a2", "col_a3", "col_a4", "col_a5", "col_a6", "col_a7", "col_a8", "col_a9", "col_b1", "col_b2" }) 
        {
            a = "";
            b = "";
            c = "";
            wreqw = "";
            werqwe = "";
            werq = "";
            werqwee = "";
            werqwer = "";
            werqwerq = "";
            erqwerq = "";
            werqwerw = "";
            
            //Табличні частини
            werqw_TablePart = new aaa_werqw_TablePart(this);
            werqwe_TablePart = new aaa_werqwe_TablePart(this);
            wer_TablePart = new aaa_wer_TablePart(this);
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                a = base.FieldValue["col_a1"].ToString();
                b = base.FieldValue["col_a2"].ToString();
                c = base.FieldValue["col_a3"].ToString();
                wreqw = base.FieldValue["col_a4"].ToString();
                werqwe = base.FieldValue["col_a5"].ToString();
                werq = base.FieldValue["col_a6"].ToString();
                werqwee = base.FieldValue["col_a7"].ToString();
                werqwer = base.FieldValue["col_a8"].ToString();
                werqwerq = base.FieldValue["col_a9"].ToString();
                erqwerq = base.FieldValue["col_b1"].ToString();
                werqwerw = base.FieldValue["col_b2"].ToString();
                
                BaseClear();
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["col_a1"] = a;
            base.FieldValue["col_a2"] = b;
            base.FieldValue["col_a3"] = c;
            base.FieldValue["col_a4"] = wreqw;
            base.FieldValue["col_a5"] = werqwe;
            base.FieldValue["col_a6"] = werq;
            base.FieldValue["col_a7"] = werqwee;
            base.FieldValue["col_a8"] = werqwer;
            base.FieldValue["col_a9"] = werqwerq;
            base.FieldValue["col_b1"] = erqwerq;
            base.FieldValue["col_b2"] = werqwerw;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public aaa_Pointer GetDirectoryPointer()
        {
            aaa_Pointer directoryPointer = new aaa_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public string a { get; set; }
        public string b { get; set; }
        public string c { get; set; }
        public string wreqw { get; set; }
        public string werqwe { get; set; }
        public string werq { get; set; }
        public string werqwee { get; set; }
        public string werqwer { get; set; }
        public string werqwerq { get; set; }
        public string erqwerq { get; set; }
        public string werqwerw { get; set; }
        
        //Табличні частини
        public aaa_werqw_TablePart werqw_TablePart { get; set; }
        public aaa_werqwe_TablePart werqwe_TablePart { get; set; }
        public aaa_wer_TablePart wer_TablePart { get; set; }
        
    }
    
    
    class aaa_Pointer : DirectoryPointer
    {
        public aaa_Pointer(object uid = null) : base(Config.Kernel, "tab_a08")
        {
            if (uid != null && uid != DBNull.Value) base.Init(new UnigueID((Guid)uid), null);
        }

        public aaa_Objest GetDirectoryObject()
        {
            aaa_Objest aaaObjestItem = new aaa_Objest();
            aaaObjestItem.Read(base.UnigueID);
            return aaaObjestItem;
        }
    }
    
    
    class aaa_Select : DirectorySelect, IDisposable
    {
        public aaa_Select() : base(Config.Kernel, "tab_a08") { }
    
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
                Current = new aaa_Pointer();
                Current.Init(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields);
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }

        public aaa_Pointer Current { get; private set; }
    }
    
      
    class aaa_werqw_TablePart : DirectoryTablePart
    {
        public aaa_werqw_TablePart(aaa_Objest owner) : base(Config.Kernel, "tab_a11",
             new string[] { "col_a1", "col_a2", "col_a3", "col_a4", "col_a5" }) 
        {
            Owner = owner;
            Records = new List<aaa_werqw_TablePartRecord>();
        }
        
        public aaa_Objest Owner { get; private set; }
        
        public List<aaa_werqw_TablePartRecord> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                aaa_werqw_TablePartRecord record = new aaa_werqw_TablePartRecord();

                record.werqwerq = fieldValue["col_a1"].ToString();
                record.werqwe = fieldValue["col_a2"].ToString();
                record.werqwerqwe = fieldValue["col_a3"].ToString();
                record.sdfas = fieldValue["col_a4"].ToString();
                record.w = fieldValue["col_a5"].ToString();
                
                Records.Add(record);
            }
            
            base.BaseClear();
        }
        
        /// <summary>
        /// Зберегти колекцію Records в базу.
        /// </summary>
        /// <param name="clear_all_before_save">
        /// Щоб не очищати всю колекцію в базі перед записом треба поставити clear_all_before_save = false.
        /// </param>
        public void Save(bool clear_all_before_save /*= true*/) 
        {
            if (Records.Count > 0)
            {
                base.BaseBeginTransaction();
                
                if (clear_all_before_save)
                    base.BaseDelete(Owner.UnigueID);

                foreach (aaa_werqw_TablePartRecord record in Records)
                {
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                    fieldValue.Add("col_a1", record.werqwerq);
                    fieldValue.Add("col_a2", record.werqwe);
                    fieldValue.Add("col_a3", record.werqwerqwe);
                    fieldValue.Add("col_a4", record.sdfas);
                    fieldValue.Add("col_a5", record.w);
                    
                    base.BaseSave(Owner.UnigueID, fieldValue);
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
    }
    
    
    class aaa_werqw_TablePartRecord : DirectoryTablePartRecord
    {
        public aaa_werqw_TablePartRecord()
        {
            werqwerq = "";
            werqwe = "";
            werqwerqwe = "";
            sdfas = "";
            w = "";
            
        }
        
        
        public aaa_werqw_TablePartRecord(
            string _werqwerq = "", string _werqwe = "", string _werqwerqwe = "", string _sdfas = "", string _w = "")
        {
            werqwerq = _werqwerq;
            werqwe = _werqwe;
            werqwerqwe = _werqwerqwe;
            sdfas = _sdfas;
            w = _w;
            
        }
        public string werqwerq { get; set; }
        public string werqwe { get; set; }
        public string werqwerqwe { get; set; }
        public string sdfas { get; set; }
        public string w { get; set; }
        
    }
      
    class aaa_werqwe_TablePart : DirectoryTablePart
    {
        public aaa_werqwe_TablePart(aaa_Objest owner) : base(Config.Kernel, "tab_a12",
             new string[] { "col_a4", "col_a1" }) 
        {
            Owner = owner;
            Records = new List<aaa_werqwe_TablePartRecord>();
        }
        
        public aaa_Objest Owner { get; private set; }
        
        public List<aaa_werqwe_TablePartRecord> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                aaa_werqwe_TablePartRecord record = new aaa_werqwe_TablePartRecord();

                record.werwqe = fieldValue["col_a4"].ToString();
                record.xfgasd = fieldValue["col_a1"].ToString();
                
                Records.Add(record);
            }
            
            base.BaseClear();
        }
        
        /// <summary>
        /// Зберегти колекцію Records в базу.
        /// </summary>
        /// <param name="clear_all_before_save">
        /// Щоб не очищати всю колекцію в базі перед записом треба поставити clear_all_before_save = false.
        /// </param>
        public void Save(bool clear_all_before_save /*= true*/) 
        {
            if (Records.Count > 0)
            {
                base.BaseBeginTransaction();
                
                if (clear_all_before_save)
                    base.BaseDelete(Owner.UnigueID);

                foreach (aaa_werqwe_TablePartRecord record in Records)
                {
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                    fieldValue.Add("col_a4", record.werwqe);
                    fieldValue.Add("col_a1", record.xfgasd);
                    
                    base.BaseSave(Owner.UnigueID, fieldValue);
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
    }
    
    
    class aaa_werqwe_TablePartRecord : DirectoryTablePartRecord
    {
        public aaa_werqwe_TablePartRecord()
        {
            werwqe = "";
            xfgasd = "";
            
        }
        
        
        public aaa_werqwe_TablePartRecord(
            string _werwqe = "", string _xfgasd = "")
        {
            werwqe = _werwqe;
            xfgasd = _xfgasd;
            
        }
        public string werwqe { get; set; }
        public string xfgasd { get; set; }
        
    }
      
    class aaa_wer_TablePart : DirectoryTablePart
    {
        public aaa_wer_TablePart(aaa_Objest owner) : base(Config.Kernel, "tab_a13",
             new string[] { "col_a1" }) 
        {
            Owner = owner;
            Records = new List<aaa_wer_TablePartRecord>();
        }
        
        public aaa_Objest Owner { get; private set; }
        
        public List<aaa_wer_TablePartRecord> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                aaa_wer_TablePartRecord record = new aaa_wer_TablePartRecord();

                record.werw = fieldValue["col_a1"].ToString();
                
                Records.Add(record);
            }
            
            base.BaseClear();
        }
        
        /// <summary>
        /// Зберегти колекцію Records в базу.
        /// </summary>
        /// <param name="clear_all_before_save">
        /// Щоб не очищати всю колекцію в базі перед записом треба поставити clear_all_before_save = false.
        /// </param>
        public void Save(bool clear_all_before_save /*= true*/) 
        {
            if (Records.Count > 0)
            {
                base.BaseBeginTransaction();
                
                if (clear_all_before_save)
                    base.BaseDelete(Owner.UnigueID);

                foreach (aaa_wer_TablePartRecord record in Records)
                {
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                    fieldValue.Add("col_a1", record.werw);
                    
                    base.BaseSave(Owner.UnigueID, fieldValue);
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
    }
    
    
    class aaa_wer_TablePartRecord : DirectoryTablePartRecord
    {
        public aaa_wer_TablePartRecord()
        {
            werw = "";
            
        }
        
        
        public aaa_wer_TablePartRecord(
            string _werw = "")
        {
            werw = _werw;
            
        }
        public string werw { get; set; }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "Довідник"
    
    
    class Довідник_Objest : DirectoryObject
    {
        public Довідник_Objest() : base(Config.Kernel, "tab_a14",
             new string[] { "col_a1", "col_a2", "col_a3", "col_a4" }) 
        {
            Назва = "";
            Код = "";
            Опис = "";
            Опис2 = "";
            
            //Табличні частини
            ТабЧастина1_TablePart = new Довідник_ТабЧастина1_TablePart(this);
            ТабЧастина2_TablePart = new Довідник_ТабЧастина2_TablePart(this);
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Назва = base.FieldValue["col_a1"].ToString();
                Код = base.FieldValue["col_a2"].ToString();
                Опис = base.FieldValue["col_a3"].ToString();
                Опис2 = base.FieldValue["col_a4"].ToString();
                
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
            base.FieldValue["col_a3"] = Опис;
            base.FieldValue["col_a4"] = Опис2;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public Довідник_Pointer GetDirectoryPointer()
        {
            Довідник_Pointer directoryPointer = new Довідник_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public string Назва { get; set; }
        public string Код { get; set; }
        public string Опис { get; set; }
        public string Опис2 { get; set; }
        
        //Табличні частини
        public Довідник_ТабЧастина1_TablePart ТабЧастина1_TablePart { get; set; }
        public Довідник_ТабЧастина2_TablePart ТабЧастина2_TablePart { get; set; }
        
    }
    
    
    class Довідник_Pointer : DirectoryPointer
    {
        public Довідник_Pointer(object uid = null) : base(Config.Kernel, "tab_a14")
        {
            if (uid != null && uid != DBNull.Value) base.Init(new UnigueID((Guid)uid), null);
        }

        public Довідник_Objest GetDirectoryObject()
        {
            Довідник_Objest ДовідникObjestItem = new Довідник_Objest();
            ДовідникObjestItem.Read(base.UnigueID);
            return ДовідникObjestItem;
        }
    }
    
    
    class Довідник_Select : DirectorySelect, IDisposable
    {
        public Довідник_Select() : base(Config.Kernel, "tab_a14") { }
    
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
                Current = new Довідник_Pointer();
                Current.Init(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields);
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }

        public Довідник_Pointer Current { get; private set; }
    }
    
      
    class Довідник_ТабЧастина1_TablePart : DirectoryTablePart
    {
        public Довідник_ТабЧастина1_TablePart(Довідник_Objest owner) : base(Config.Kernel, "tab_a15",
             new string[] { "col_a4", "col_a5" }) 
        {
            Owner = owner;
            Records = new List<Довідник_ТабЧастина1_TablePartRecord>();
        }
        
        public Довідник_Objest Owner { get; private set; }
        
        public List<Довідник_ТабЧастина1_TablePartRecord> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Довідник_ТабЧастина1_TablePartRecord record = new Довідник_ТабЧастина1_TablePartRecord();

                record.Назва = fieldValue["col_a4"].ToString();
                record.Код = fieldValue["col_a5"].ToString();
                
                Records.Add(record);
            }
            
            base.BaseClear();
        }
        
        /// <summary>
        /// Зберегти колекцію Records в базу.
        /// </summary>
        /// <param name="clear_all_before_save">
        /// Щоб не очищати всю колекцію в базі перед записом треба поставити clear_all_before_save = false.
        /// </param>
        public void Save(bool clear_all_before_save /*= true*/) 
        {
            if (Records.Count > 0)
            {
                base.BaseBeginTransaction();
                
                if (clear_all_before_save)
                    base.BaseDelete(Owner.UnigueID);

                foreach (Довідник_ТабЧастина1_TablePartRecord record in Records)
                {
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                    fieldValue.Add("col_a4", record.Назва);
                    fieldValue.Add("col_a5", record.Код);
                    
                    base.BaseSave(Owner.UnigueID, fieldValue);
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
    }
    
    
    class Довідник_ТабЧастина1_TablePartRecord : DirectoryTablePartRecord
    {
        public Довідник_ТабЧастина1_TablePartRecord()
        {
            Назва = "";
            Код = "";
            
        }
        
        
        public Довідник_ТабЧастина1_TablePartRecord(
            string _Назва = "", string _Код = "")
        {
            Назва = _Назва;
            Код = _Код;
            
        }
        public string Назва { get; set; }
        public string Код { get; set; }
        
    }
      
    class Довідник_ТабЧастина2_TablePart : DirectoryTablePart
    {
        public Довідник_ТабЧастина2_TablePart(Довідник_Objest owner) : base(Config.Kernel, "tab_a16",
             new string[] { "col_a1", "col_a2" }) 
        {
            Owner = owner;
            Records = new List<Довідник_ТабЧастина2_TablePartRecord>();
        }
        
        public Довідник_Objest Owner { get; private set; }
        
        public List<Довідник_ТабЧастина2_TablePartRecord> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Довідник_ТабЧастина2_TablePartRecord record = new Довідник_ТабЧастина2_TablePartRecord();

                record.Назва = fieldValue["col_a1"].ToString();
                record.Код = fieldValue["col_a2"].ToString();
                
                Records.Add(record);
            }
            
            base.BaseClear();
        }
        
        /// <summary>
        /// Зберегти колекцію Records в базу.
        /// </summary>
        /// <param name="clear_all_before_save">
        /// Щоб не очищати всю колекцію в базі перед записом треба поставити clear_all_before_save = false.
        /// </param>
        public void Save(bool clear_all_before_save /*= true*/) 
        {
            if (Records.Count > 0)
            {
                base.BaseBeginTransaction();
                
                if (clear_all_before_save)
                    base.BaseDelete(Owner.UnigueID);

                foreach (Довідник_ТабЧастина2_TablePartRecord record in Records)
                {
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                    fieldValue.Add("col_a1", record.Назва);
                    fieldValue.Add("col_a2", record.Код);
                    
                    base.BaseSave(Owner.UnigueID, fieldValue);
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
    }
    
    
    class Довідник_ТабЧастина2_TablePartRecord : DirectoryTablePartRecord
    {
        public Довідник_ТабЧастина2_TablePartRecord()
        {
            Назва = "";
            Код = "";
            
        }
        
        
        public Довідник_ТабЧастина2_TablePartRecord(
            string _Назва = "", string _Код = "")
        {
            Назва = _Назва;
            Код = _Код;
            
        }
        public string Назва { get; set; }
        public string Код { get; set; }
        
    }
      
    
    #endregion
    
}

namespace ConfTrade_v1_1.Document
{

}

namespace ConfTrade_v1_1.Journal
{

}

namespace ConfTrade_v1_1.Register
{

}
  
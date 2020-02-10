

/*
 *
 * Конфігурації "ConfTrade 1.1"
 * Автор Yurik
 * Дата конфігурації: 10.02.2020 14:55:35
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
    
    #region DIRECTORY "Товари"
    
    /// <summary> 
    /// Довідник Товари
    /// </summary>
    class Товари_Objest : DirectoryObject
    {
        public Товари_Objest() : base(Config.Kernel, "tovary",
             new string[] { "name", "code", "count", "numer", "masiv", "artikul", "pointer1", "pointer2" }) 
        {
            Назва = "";
            Код = "";
            Кількість = 0;
            Номер = 0;
            Масив = new string[] { };
            Артикул = "";
            Вказівник1 = new ТМЦ_Pointer();
            Вказівник2 = new Товари_Pointer();
            
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
                Кількість = (int)base.FieldValue["count"];
                Номер = (base.FieldValue["numer"] != DBNull.Value) ? (decimal)base.FieldValue["numer"] : 0;
                Масив = (base.FieldValue["masiv"] != DBNull.Value) ? (string[])base.FieldValue["masiv"] : new string[] { };
                Артикул = base.FieldValue["artikul"].ToString();
                Вказівник1 = new ТМЦ_Pointer(base.FieldValue["pointer1"]);
                Вказівник2 = new Товари_Pointer(base.FieldValue["pointer2"]);
                
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
        
        //Табличні частини
        public Товари_Ціни_TablePart Ціни_TablePart { get; set; }
        public Товари_ОдиниціВиміру_TablePart ОдиниціВиміру_TablePart { get; set; }
        
    }
    
    /// <summary> 
    /// Довідник Товари
    /// </summary>
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
    
    /// <summary> 
    /// Довідник Товари
    /// </summary>
    class Товари_Select : DirectorySelect
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
    
      
    /// <summary>
    /// Таблична частина Ціни
    /// </summary>
    class Товари_Ціни_TablePart : DirectoryTablePart
    {
        public Товари_Ціни_TablePart(Товари_Objest owner) : base(Config.Kernel, "tovary_ceny_tablepart",
             new string[] { "name", "cena", "isnew", "date_update", "date_test", "times", "pointer_od" }) 
        {
            Owner = owner;
            Records = new List<Товари_Ціни_TablePartRecord>();
        }
        
        public Товари_Objest Owner { get; private set; }
        
        public List<Товари_Ціни_TablePartRecord> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.FieldValueList.Clear();

            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Товари_Ціни_TablePartRecord record = new Товари_Ціни_TablePartRecord();

                record.Name = fieldValue["name"].ToString();
                record.Cena = (fieldValue["cena"] != DBNull.Value) ? (decimal)fieldValue["cena"] : 0;
                record.IsNew = (int)fieldValue["isnew"];
                record.ДатаОбновлення = (fieldValue["date_update"] != DBNull.Value) ? DateTime.Parse(fieldValue["date_update"].ToString()) : DateTime.MinValue;
                record.Дата = (fieldValue["date_test"] != DBNull.Value) ? DateTime.Parse(fieldValue["date_test"].ToString()) : DateTime.MinValue;
                record.Час = (fieldValue["times"] != DBNull.Value) ? TimeSpan.Parse(fieldValue["times"].ToString()) : DateTime.MinValue.TimeOfDay;
                record.ОдВиміру = new ОдиниціВиміру_Pointer(fieldValue["pointer_od"]);
                
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
            
        }
        
        public Товари_Ціни_TablePartRecord(
            string _Name = "", decimal _Cena = 0, int _IsNew = 0, DateTime?  _ДатаОбновлення = null, DateTime?  _Дата = null, TimeSpan?  _Час = null, ОдиниціВиміру_Pointer _ОдВиміру = null)
        {
            Name = _Name;
            Cena = _Cena;
            IsNew = _IsNew;
            ДатаОбновлення = _ДатаОбновлення ?? DateTime.MinValue;
            Дата = _Дата ?? DateTime.MinValue;
            Час = _Час ?? DateTime.MinValue.TimeOfDay;
            ОдВиміру = _ОдВиміру;
            
        }
        
        public string Name { get; set; }
        public decimal Cena { get; set; }
        public int IsNew { get; set; }
        public DateTime ДатаОбновлення { get; set; }
        public DateTime Дата { get; set; }
        public TimeSpan Час { get; set; }
        public ОдиниціВиміру_Pointer ОдВиміру { get; set; }
        
    }
      
    /// <summary>
    /// 
          
    /// </summary>
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
            base.FieldValueList.Clear();

            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Товари_ОдиниціВиміру_TablePartRecord record = new Товари_ОдиниціВиміру_TablePartRecord();

                record.Одиниця = new ОдиниціВиміру_Pointer(fieldValue["od_pointer"]);
                
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

                foreach (Товари_ОдиниціВиміру_TablePartRecord record in Records)
                {
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                    fieldValue.Add("od_pointer", record.Одиниця.UnigueID.UGuid);
                    
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
    /// 
          
    /// </summary>
    class Товари_ОдиниціВиміру_TablePartRecord : DirectoryTablePartRecord
    {
        public Товари_ОдиниціВиміру_TablePartRecord()
        {
            Одиниця = new ОдиниціВиміру_Pointer();
            
        }
        
        public Товари_ОдиниціВиміру_TablePartRecord(
            ОдиниціВиміру_Pointer _Одиниця = null)
        {
            Одиниця = _Одиниця;
            
        }
        
        public ОдиниціВиміру_Pointer Одиниця { get; set; }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "Новий"
    
    /// <summary> 
    /// new new
    /// </summary>
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
    
    /// <summary> 
    /// new new
    /// </summary>
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
    
    /// <summary> 
    /// new new
    /// </summary>
    class Новий_Select : DirectorySelect
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
    
    #region DIRECTORY "Записки"
    
    /// <summary> 
    /// 
      
    /// </summary>
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
    
    /// <summary> 
    /// 
      
    /// </summary>
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
    
    /// <summary> 
    /// 
      
    /// </summary>
    class Записки_Select : DirectorySelect
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
    
    /// <summary> 
    /// 
      
    /// </summary>
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
    
    /// <summary> 
    /// 
      
    /// </summary>
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
    
    /// <summary> 
    /// 
      
    /// </summary>
    class ОдиниціВиміру_Select : DirectorySelect
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
    
      
    /// <summary>
    /// Історія
    /// </summary>
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
            base.FieldValueList.Clear();

            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                ОдиниціВиміру_Історія_TablePartRecord record = new ОдиниціВиміру_Історія_TablePartRecord();

                record.ДатаЗапису = (fieldValue["date_save"] != DBNull.Value) ? DateTime.Parse(fieldValue["date_save"].ToString()) : DateTime.MinValue;
                record.Значення = fieldValue["value"].ToString();
                
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
        
        public void Clear()
        {
            base.BaseDelete(Owner.UnigueID);
        }
    }
    
    /// <summary> 
    /// Історія
    /// </summary>
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
      
    
    #endregion
    
}
  
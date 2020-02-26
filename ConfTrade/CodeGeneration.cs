

/*
 *
 * Конфігурації "ConfTrade 1.1"
 * Автор Yurik
 * Дата конфігурації: 26.02.2020 16:39:25
 *
 */

using System;
using System.Collections.Generic;
using AccountingSoftware;
using Перелічення = ConfTrade_v1_1.Перелічення;

namespace ConfTrade_v1_1
{
    static class Config
    {
        public static Kernel Kernel { get; set; }
    }
}

namespace ConfTrade_v1_1.Довідники
{
    
    #region DIRECTORY "Валюти"
    
    
    class Валюти_Objest : DirectoryObject
    {
        public Валюти_Objest() : base(Config.Kernel, "tab_a02",
             new string[] { "col_a1", "col_a2", "col_a3", "col_a4", "col_a5" }) 
        {
            Курс = 0;
            Кратность = 0;
            Кратко = "";
            Назва = "";
            Код = "";
            
            //Табличні частини
            КурсНаДату_TablePart = new Валюти_КурсНаДату_TablePart(this);
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Курс = (base.FieldValue["col_a1"] != DBNull.Value) ? (decimal)base.FieldValue["col_a1"] : 0;
                Кратность = (base.FieldValue["col_a2"] != DBNull.Value) ? (int)base.FieldValue["col_a2"] : 0;
                Кратко = base.FieldValue["col_a3"].ToString();
                Назва = base.FieldValue["col_a4"].ToString();
                Код = base.FieldValue["col_a5"].ToString();
                
                BaseClear();
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["col_a1"] = Курс;
            base.FieldValue["col_a2"] = Кратность;
            base.FieldValue["col_a3"] = Кратко;
            base.FieldValue["col_a4"] = Назва;
            base.FieldValue["col_a5"] = Код;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public Валюти_Pointer GetDirectoryPointer()
        {
            Валюти_Pointer directoryPointer = new Валюти_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public decimal Курс { get; set; }
        public int Кратность { get; set; }
        public string Кратко { get; set; }
        public string Назва { get; set; }
        public string Код { get; set; }
        
        //Табличні частини
        public Валюти_КурсНаДату_TablePart КурсНаДату_TablePart { get; set; }
        
    }
    
    
    class Валюти_Pointer : DirectoryPointer
    {
        public Валюти_Pointer(object uid = null) : base(Config.Kernel, "tab_a02")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public Валюти_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a02")
        {
            base.Init(uid, fields);
        } 
        
        public Валюти_Objest GetDirectoryObject()
        {
            Валюти_Objest ВалютиObjestItem = new Валюти_Objest();
            ВалютиObjestItem.Read(base.UnigueID);
            return ВалютиObjestItem;
        }
    }
    
    
    class Валюти_Select : DirectorySelect, IDisposable
    {
        public Валюти_Select() : base(Config.Kernel, "tab_a02") { }
    
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
                Current = new Валюти_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields);
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }

        public Валюти_Pointer Current { get; private set; }
    }
    
      
    class Валюти_КурсНаДату_TablePart : DirectoryTablePart
    {
        public Валюти_КурсНаДату_TablePart(Валюти_Objest owner) : base(Config.Kernel, "tab_a03",
             new string[] { "col_a4", "col_a5", "col_a6" }) 
        {
            Owner = owner;
            Records = new List<Валюти_КурсНаДату_TablePartRecord>();
        }
        
        public Валюти_Objest Owner { get; private set; }
        
        public List<Валюти_КурсНаДату_TablePartRecord> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Валюти_КурсНаДату_TablePartRecord record = new Валюти_КурсНаДату_TablePartRecord();

                record.Дата = (fieldValue["col_a4"] != DBNull.Value) ? DateTime.Parse(fieldValue["col_a4"].ToString()) : DateTime.MinValue;
                record.Курс = (fieldValue["col_a5"] != DBNull.Value) ? (decimal)fieldValue["col_a5"] : 0;
                record.Кратность = (fieldValue["col_a6"] != DBNull.Value) ? (int)fieldValue["col_a6"] : 0;
                
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

                foreach (Валюти_КурсНаДату_TablePartRecord record in Records)
                {
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                    fieldValue.Add("col_a4", record.Дата);
                    fieldValue.Add("col_a5", record.Курс);
                    fieldValue.Add("col_a6", record.Кратность);
                    
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
    
    
    class Валюти_КурсНаДату_TablePartRecord : DirectoryTablePartRecord
    {
        public Валюти_КурсНаДату_TablePartRecord()
        {
            Дата = DateTime.MinValue;
            Курс = 0;
            Кратность = 0;
            
        }
        
        
        public Валюти_КурсНаДату_TablePartRecord(
            DateTime?  _Дата = null, decimal _Курс = 0, int _Кратность = 0)
        {
            Дата = _Дата ?? DateTime.MinValue;
            Курс = _Курс;
            Кратность = _Кратность;
            
        }
        public DateTime Дата { get; set; }
        public decimal Курс { get; set; }
        public int Кратность { get; set; }
        
    }
      
    class Валюти_Список_View : DirectoryView
    {
        public Валюти_Список_View() : base(Config.Kernel, "tab_a02", 
             new string[] { "col_a4", "col_a1" },
             new string[] { "Назва", "Курс" },
             new string[] { "string", "numeric" },
             "Валюти_Список")
        {
            
        }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "Единици"
    
    ///<summary>
    ///Единицы измерения товара.
    ///</summary>
    class Единици_Objest : DirectoryObject
    {
        public Единици_Objest() : base(Config.Kernel, "tab_a04",
             new string[] { "col_a7", "col_a8", "col_a9", "col_b1", "col_a1", "col_a2" }) 
        {
            Вес = 0;
            Коеффициент = 0;
            Единица = new Довідники.КлассификаторЕдИзм_Pointer();
            ШтрихКод = 0;
            Назва = "";
            Код = "";
            
            //Табличні частини
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Вес = (base.FieldValue["col_a7"] != DBNull.Value) ? (decimal)base.FieldValue["col_a7"] : 0;
                Коеффициент = (base.FieldValue["col_a8"] != DBNull.Value) ? (decimal)base.FieldValue["col_a8"] : 0;
                Единица = new Довідники.КлассификаторЕдИзм_Pointer(base.FieldValue["col_a9"]);
                ШтрихКод = (base.FieldValue["col_b1"] != DBNull.Value) ? (int)base.FieldValue["col_b1"] : 0;
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
            base.FieldValue["col_a7"] = Вес;
            base.FieldValue["col_a8"] = Коеффициент;
            base.FieldValue["col_a9"] = Единица.UnigueID.UGuid;
            base.FieldValue["col_b1"] = ШтрихКод;
            base.FieldValue["col_a1"] = Назва;
            base.FieldValue["col_a2"] = Код;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public Единици_Pointer GetDirectoryPointer()
        {
            Единици_Pointer directoryPointer = new Единици_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public decimal Вес { get; set; }
        public decimal Коеффициент { get; set; }
        public Довідники.КлассификаторЕдИзм_Pointer Единица { get; set; }
        public int ШтрихКод { get; set; }
        public string Назва { get; set; }
        public string Код { get; set; }
        
        //Табличні частини
        
    }
    
    ///<summary>
    ///Единицы измерения товара.
    ///</summary>
    class Единици_Pointer : DirectoryPointer
    {
        public Единици_Pointer(object uid = null) : base(Config.Kernel, "tab_a04")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public Единици_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a04")
        {
            base.Init(uid, fields);
        } 
        
        public Единици_Objest GetDirectoryObject()
        {
            Единици_Objest ЕдинициObjestItem = new Единици_Objest();
            ЕдинициObjestItem.Read(base.UnigueID);
            return ЕдинициObjestItem;
        }
    }
    
    ///<summary>
    ///Единицы измерения товара.
    ///</summary>
    class Единици_Select : DirectorySelect, IDisposable
    {
        public Единици_Select() : base(Config.Kernel, "tab_a04") { }
    
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
                Current = new Единици_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields);
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }

        public Единици_Pointer Current { get; private set; }
    }
    
      
    class Единици_Список_View : DirectoryView
    {
        public Единици_Список_View() : base(Config.Kernel, "tab_a04", 
             new string[] { "col_a9", "col_a1" },
             new string[] { "Единица", "Назва" },
             new string[] { "pointer", "string" },
             "Единици_Список")
        {
            
        }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "КлассификаторЕдИзм"
    
    ///<summary>
    ///Классификатор единиц измерений.
    ///</summary>
    class КлассификаторЕдИзм_Objest : DirectoryObject
    {
        public КлассификаторЕдИзм_Objest() : base(Config.Kernel, "tab_a05",
             new string[] { "col_b2", "col_b3", "col_a1", "col_a2" }) 
        {
            ПолнНаименование = "";
            КодЕдИзмерения = "";
            Назва = "";
            Код = "";
            
            //Табличні частини
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                ПолнНаименование = base.FieldValue["col_b2"].ToString();
                КодЕдИзмерения = base.FieldValue["col_b3"].ToString();
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
            base.FieldValue["col_b2"] = ПолнНаименование;
            base.FieldValue["col_b3"] = КодЕдИзмерения;
            base.FieldValue["col_a1"] = Назва;
            base.FieldValue["col_a2"] = Код;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public КлассификаторЕдИзм_Pointer GetDirectoryPointer()
        {
            КлассификаторЕдИзм_Pointer directoryPointer = new КлассификаторЕдИзм_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public string ПолнНаименование { get; set; }
        public string КодЕдИзмерения { get; set; }
        public string Назва { get; set; }
        public string Код { get; set; }
        
        //Табличні частини
        
    }
    
    ///<summary>
    ///Классификатор единиц измерений.
    ///</summary>
    class КлассификаторЕдИзм_Pointer : DirectoryPointer
    {
        public КлассификаторЕдИзм_Pointer(object uid = null) : base(Config.Kernel, "tab_a05")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public КлассификаторЕдИзм_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a05")
        {
            base.Init(uid, fields);
        } 
        
        public КлассификаторЕдИзм_Objest GetDirectoryObject()
        {
            КлассификаторЕдИзм_Objest КлассификаторЕдИзмObjestItem = new КлассификаторЕдИзм_Objest();
            КлассификаторЕдИзмObjestItem.Read(base.UnigueID);
            return КлассификаторЕдИзмObjestItem;
        }
    }
    
    ///<summary>
    ///Классификатор единиц измерений.
    ///</summary>
    class КлассификаторЕдИзм_Select : DirectorySelect, IDisposable
    {
        public КлассификаторЕдИзм_Select() : base(Config.Kernel, "tab_a05") { }
    
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
                Current = new КлассификаторЕдИзм_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields);
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }

        public КлассификаторЕдИзм_Pointer Current { get; private set; }
    }
    
      
    class КлассификаторЕдИзм_Список_View : DirectoryView
    {
        public КлассификаторЕдИзм_Список_View() : base(Config.Kernel, "tab_a05", 
             new string[] { "col_a1", "col_b3" },
             new string[] { "Назва", "КодЕдИзмерения" },
             new string[] { "string", "string" },
             "КлассификаторЕдИзм_Список")
        {
            
        }
        
    }
      
    class КлассификаторЕдИзм_Список2_View : DirectoryView
    {
        public КлассификаторЕдИзм_Список2_View() : base(Config.Kernel, "tab_a05", 
             new string[] { "col_b2", "col_b3", "col_a1", "col_a2" },
             new string[] { "ПолнНаименование", "КодЕдИзмерения", "Назва", "Код" },
             new string[] { "string", "string", "string", "string" },
             "КлассификаторЕдИзм_Список2")
        {
            
        }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "Категории"
    
    ///<summary>
    ///Категории товаров и контрагентов.
    ///</summary>
    class Категории_Objest : DirectoryObject
    {
        public Категории_Objest() : base(Config.Kernel, "tab_a06",
             new string[] { "col_b4", "col_a1", "col_a2" }) 
        {
            Комментарий = "";
            Назва = "";
            Код = "";
            
            //Табличні частини
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Комментарий = base.FieldValue["col_b4"].ToString();
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
            base.FieldValue["col_b4"] = Комментарий;
            base.FieldValue["col_a1"] = Назва;
            base.FieldValue["col_a2"] = Код;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public Категории_Pointer GetDirectoryPointer()
        {
            Категории_Pointer directoryPointer = new Категории_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public string Комментарий { get; set; }
        public string Назва { get; set; }
        public string Код { get; set; }
        
        //Табличні частини
        
    }
    
    ///<summary>
    ///Категории товаров и контрагентов.
    ///</summary>
    class Категории_Pointer : DirectoryPointer
    {
        public Категории_Pointer(object uid = null) : base(Config.Kernel, "tab_a06")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public Категории_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a06")
        {
            base.Init(uid, fields);
        } 
        
        public Категории_Objest GetDirectoryObject()
        {
            Категории_Objest КатегорииObjestItem = new Категории_Objest();
            КатегорииObjestItem.Read(base.UnigueID);
            return КатегорииObjestItem;
        }
    }
    
    ///<summary>
    ///Категории товаров и контрагентов.
    ///</summary>
    class Категории_Select : DirectorySelect, IDisposable
    {
        public Категории_Select() : base(Config.Kernel, "tab_a06") { }
    
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
                Current = new Категории_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields);
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }

        public Категории_Pointer Current { get; private set; }
    }
    
      
    class Категории_Список_View : DirectoryView
    {
        public Категории_Список_View() : base(Config.Kernel, "tab_a06", 
             new string[] { "col_a1" },
             new string[] { "Назва" },
             new string[] { "string" },
             "Категории_Список")
        {
            
        }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "КатегорииКонтрагентов"
    
    ///<summary>
    ///Категории контрагентов.
    ///</summary>
    class КатегорииКонтрагентов_Objest : DirectoryObject
    {
        public КатегорииКонтрагентов_Objest() : base(Config.Kernel, "tab_a07",
             new string[] { "col_b5", "col_a1", "col_a2" }) 
        {
            Категория = new Довідники.Категории_Pointer();
            Назва = "";
            Код = "";
            
            //Табличні частини
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Категория = new Довідники.Категории_Pointer(base.FieldValue["col_b5"]);
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
            base.FieldValue["col_b5"] = Категория.UnigueID.UGuid;
            base.FieldValue["col_a1"] = Назва;
            base.FieldValue["col_a2"] = Код;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public КатегорииКонтрагентов_Pointer GetDirectoryPointer()
        {
            КатегорииКонтрагентов_Pointer directoryPointer = new КатегорииКонтрагентов_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public Довідники.Категории_Pointer Категория { get; set; }
        public string Назва { get; set; }
        public string Код { get; set; }
        
        //Табличні частини
        
    }
    
    ///<summary>
    ///Категории контрагентов.
    ///</summary>
    class КатегорииКонтрагентов_Pointer : DirectoryPointer
    {
        public КатегорииКонтрагентов_Pointer(object uid = null) : base(Config.Kernel, "tab_a07")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public КатегорииКонтрагентов_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a07")
        {
            base.Init(uid, fields);
        } 
        
        public КатегорииКонтрагентов_Objest GetDirectoryObject()
        {
            КатегорииКонтрагентов_Objest КатегорииКонтрагентовObjestItem = new КатегорииКонтрагентов_Objest();
            КатегорииКонтрагентовObjestItem.Read(base.UnigueID);
            return КатегорииКонтрагентовObjestItem;
        }
    }
    
    ///<summary>
    ///Категории контрагентов.
    ///</summary>
    class КатегорииКонтрагентов_Select : DirectorySelect, IDisposable
    {
        public КатегорииКонтрагентов_Select() : base(Config.Kernel, "tab_a07") { }
    
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
                Current = new КатегорииКонтрагентов_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields);
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }

        public КатегорииКонтрагентов_Pointer Current { get; private set; }
    }
    
      
    class КатегорииКонтрагентов_Список_View : DirectoryView
    {
        public КатегорииКонтрагентов_Список_View() : base(Config.Kernel, "tab_a07", 
             new string[] { "col_a1" },
             new string[] { "Назва" },
             new string[] { "string" },
             "КатегорииКонтрагентов_Список")
        {
            
        }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "КатегорииТоваров"
    
    ///<summary>
    ///КатегорииТоваров.
    ///</summary>
    class КатегорииТоваров_Objest : DirectoryObject
    {
        public КатегорииТоваров_Objest() : base(Config.Kernel, "tab_a08",
             new string[] { "col_b6", "col_a1", "col_a2" }) 
        {
            Категория = new Довідники.Категории_Pointer();
            Назва = "";
            Код = "";
            
            //Табличні частини
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Категория = new Довідники.Категории_Pointer(base.FieldValue["col_b6"]);
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
            base.FieldValue["col_b6"] = Категория.UnigueID.UGuid;
            base.FieldValue["col_a1"] = Назва;
            base.FieldValue["col_a2"] = Код;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public КатегорииТоваров_Pointer GetDirectoryPointer()
        {
            КатегорииТоваров_Pointer directoryPointer = new КатегорииТоваров_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public Довідники.Категории_Pointer Категория { get; set; }
        public string Назва { get; set; }
        public string Код { get; set; }
        
        //Табличні частини
        
    }
    
    ///<summary>
    ///КатегорииТоваров.
    ///</summary>
    class КатегорииТоваров_Pointer : DirectoryPointer
    {
        public КатегорииТоваров_Pointer(object uid = null) : base(Config.Kernel, "tab_a08")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public КатегорииТоваров_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a08")
        {
            base.Init(uid, fields);
        } 
        
        public КатегорииТоваров_Objest GetDirectoryObject()
        {
            КатегорииТоваров_Objest КатегорииТоваровObjestItem = new КатегорииТоваров_Objest();
            КатегорииТоваровObjestItem.Read(base.UnigueID);
            return КатегорииТоваровObjestItem;
        }
    }
    
    ///<summary>
    ///КатегорииТоваров.
    ///</summary>
    class КатегорииТоваров_Select : DirectorySelect, IDisposable
    {
        public КатегорииТоваров_Select() : base(Config.Kernel, "tab_a08") { }
    
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
                Current = new КатегорииТоваров_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields);
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }

        public КатегорииТоваров_Pointer Current { get; private set; }
    }
    
      
    class КатегорииТоваров_Список_View : DirectoryView
    {
        public КатегорииТоваров_Список_View() : base(Config.Kernel, "tab_a08", 
             new string[] { "col_a1" },
             new string[] { "Назва" },
             new string[] { "string" },
             "КатегорииТоваров_Список")
        {
            
        }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "КатегорииЦен"
    
    ///<summary>
    ///Типы цен.
    ///</summary>
    class КатегорииЦен_Objest : DirectoryObject
    {
        public КатегорииЦен_Objest() : base(Config.Kernel, "tab_a09",
             new string[] { "col_b7", "col_b8", "col_a1", "col_a2" }) 
        {
            Комментарий = "";
            ТорговаяНаценка = 0;
            Назва = "";
            Код = "";
            
            //Табличні частини
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Комментарий = base.FieldValue["col_b7"].ToString();
                ТорговаяНаценка = (base.FieldValue["col_b8"] != DBNull.Value) ? (decimal)base.FieldValue["col_b8"] : 0;
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
            base.FieldValue["col_b7"] = Комментарий;
            base.FieldValue["col_b8"] = ТорговаяНаценка;
            base.FieldValue["col_a1"] = Назва;
            base.FieldValue["col_a2"] = Код;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public КатегорииЦен_Pointer GetDirectoryPointer()
        {
            КатегорииЦен_Pointer directoryPointer = new КатегорииЦен_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public string Комментарий { get; set; }
        public decimal ТорговаяНаценка { get; set; }
        public string Назва { get; set; }
        public string Код { get; set; }
        
        //Табличні частини
        
    }
    
    ///<summary>
    ///Типы цен.
    ///</summary>
    class КатегорииЦен_Pointer : DirectoryPointer
    {
        public КатегорииЦен_Pointer(object uid = null) : base(Config.Kernel, "tab_a09")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public КатегорииЦен_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a09")
        {
            base.Init(uid, fields);
        } 
        
        public КатегорииЦен_Objest GetDirectoryObject()
        {
            КатегорииЦен_Objest КатегорииЦенObjestItem = new КатегорииЦен_Objest();
            КатегорииЦенObjestItem.Read(base.UnigueID);
            return КатегорииЦенObjestItem;
        }
    }
    
    ///<summary>
    ///Типы цен.
    ///</summary>
    class КатегорииЦен_Select : DirectorySelect, IDisposable
    {
        public КатегорииЦен_Select() : base(Config.Kernel, "tab_a09") { }
    
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
                Current = new КатегорииЦен_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields);
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }

        public КатегорииЦен_Pointer Current { get; private set; }
    }
    
      
    class КатегорииЦен_Список_View : DirectoryView
    {
        public КатегорииЦен_Список_View() : base(Config.Kernel, "tab_a09", 
             new string[] { "col_a1" },
             new string[] { "Назва" },
             new string[] { "string" },
             "КатегорииЦен_Список")
        {
            
        }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "КодиУКТВЕД"
    
    ///<summary>
    ///Классификатор кодов для НН.
    ///</summary>
    class КодиУКТВЕД_Objest : DirectoryObject
    {
        public КодиУКТВЕД_Objest() : base(Config.Kernel, "tab_a10",
             new string[] { "col_b9", "col_c1", "col_a1", "col_a2" }) 
        {
            ПолноеНаименование = "";
            Вид = "";
            Назва = "";
            Код = "";
            
            //Табличні частини
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                ПолноеНаименование = base.FieldValue["col_b9"].ToString();
                Вид = base.FieldValue["col_c1"].ToString();
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
            base.FieldValue["col_b9"] = ПолноеНаименование;
            base.FieldValue["col_c1"] = Вид;
            base.FieldValue["col_a1"] = Назва;
            base.FieldValue["col_a2"] = Код;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public КодиУКТВЕД_Pointer GetDirectoryPointer()
        {
            КодиУКТВЕД_Pointer directoryPointer = new КодиУКТВЕД_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public string ПолноеНаименование { get; set; }
        public string Вид { get; set; }
        public string Назва { get; set; }
        public string Код { get; set; }
        
        //Табличні частини
        
    }
    
    ///<summary>
    ///Классификатор кодов для НН.
    ///</summary>
    class КодиУКТВЕД_Pointer : DirectoryPointer
    {
        public КодиУКТВЕД_Pointer(object uid = null) : base(Config.Kernel, "tab_a10")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public КодиУКТВЕД_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a10")
        {
            base.Init(uid, fields);
        } 
        
        public КодиУКТВЕД_Objest GetDirectoryObject()
        {
            КодиУКТВЕД_Objest КодиУКТВЕДObjestItem = new КодиУКТВЕД_Objest();
            КодиУКТВЕДObjestItem.Read(base.UnigueID);
            return КодиУКТВЕДObjestItem;
        }
    }
    
    ///<summary>
    ///Классификатор кодов для НН.
    ///</summary>
    class КодиУКТВЕД_Select : DirectorySelect, IDisposable
    {
        public КодиУКТВЕД_Select() : base(Config.Kernel, "tab_a10") { }
    
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
                Current = new КодиУКТВЕД_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields);
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }

        public КодиУКТВЕД_Pointer Current { get; private set; }
    }
    
      
    class КодиУКТВЕД_Список_View : DirectoryView
    {
        public КодиУКТВЕД_Список_View() : base(Config.Kernel, "tab_a10", 
             new string[] { "col_a1" },
             new string[] { "Назва" },
             new string[] { "string" },
             "КодиУКТВЕД_Список")
        {
            
        }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "Комплектация"
    
    ///<summary>
    ///Состав наборов.
    ///</summary>
    class Комплектация_Objest : DirectoryObject
    {
        public Комплектация_Objest() : base(Config.Kernel, "tab_a11",
             new string[] { "col_c2", "col_c3", "col_a1", "col_a2" }) 
        {
            Кво = 0;
            Товар = new DirectoryEmptyPointer();
            Назва = "";
            Код = "";
            
            //Табличні частини
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Кво = (base.FieldValue["col_c2"] != DBNull.Value) ? (decimal)base.FieldValue["col_c2"] : 0;
                Товар = new DirectoryEmptyPointer();
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
            base.FieldValue["col_c2"] = Кво;
            base.FieldValue["col_c3"] = Товар.UnigueID.UGuid;
            base.FieldValue["col_a1"] = Назва;
            base.FieldValue["col_a2"] = Код;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public Комплектация_Pointer GetDirectoryPointer()
        {
            Комплектация_Pointer directoryPointer = new Комплектация_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public decimal Кво { get; set; }
        public DirectoryEmptyPointer Товар { get; set; }
        public string Назва { get; set; }
        public string Код { get; set; }
        
        //Табличні частини
        
    }
    
    ///<summary>
    ///Состав наборов.
    ///</summary>
    class Комплектация_Pointer : DirectoryPointer
    {
        public Комплектация_Pointer(object uid = null) : base(Config.Kernel, "tab_a11")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public Комплектация_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a11")
        {
            base.Init(uid, fields);
        } 
        
        public Комплектация_Objest GetDirectoryObject()
        {
            Комплектация_Objest КомплектацияObjestItem = new Комплектация_Objest();
            КомплектацияObjestItem.Read(base.UnigueID);
            return КомплектацияObjestItem;
        }
    }
    
    ///<summary>
    ///Состав наборов.
    ///</summary>
    class Комплектация_Select : DirectorySelect, IDisposable
    {
        public Комплектация_Select() : base(Config.Kernel, "tab_a11") { }
    
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
                Current = new Комплектация_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields);
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }

        public Комплектация_Pointer Current { get; private set; }
    }
    
      
    class Комплектация_Список_View : DirectoryView
    {
        public Комплектация_Список_View() : base(Config.Kernel, "tab_a11", 
             new string[] { "col_a1" },
             new string[] { "Назва" },
             new string[] { "string" },
             "Комплектация_Список")
        {
            
        }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "Контрагенти"
    
    ///<summary>
    ///Поставщики и покупатели.
    ///</summary>
    class Контрагенти_Objest : DirectoryObject
    {
        public Контрагенти_Objest() : base(Config.Kernel, "tab_a12",
             new string[] { "col_c4", "col_a1", "col_a2", "col_a3", "col_a4", "col_a5", "col_a6", "col_a7", "col_a8", "col_a9", "col_b1", "col_b2", "col_b3", "col_b4", "col_b5", "col_b6", "col_b7", "col_b8", "col_b9", "col_c1", "col_c2", "col_c3", "col_c5", "col_c6", "col_c7", "col_c8", "col_c9", "col_d1" }) 
        {
            ВалютаВзаиморасчетов = new Довідники.Валюти_Pointer();
            ВалютаКредита = new Довідники.Валюти_Pointer();
            ВалютаКредитаПоставщика = new Довідники.Валюти_Pointer();
            ВидКонтрагента = 0;
            Глубина = 0;
            ГлубинаКредитаПоставщика = 0;
            ДокументДатаВидачи = DateTime.MinValue;
            ДокументКемВидан = "";
            ДокументНомер = "";
            ДокументСерия = "";
            ЕГРПОУ = "";
            ИНН = "";
            КатегорияЦен = new Довідники.КатегорииЦен_Pointer();
            КатегорияЦенПоставщика = new Довідники.КатегорииЦен_Pointer();
            Комментарий = "";
            НомерСвидетельства = "";
            ОсновнойДоговорТорг = new DirectoryEmptyPointer();
            ПолнНаименование = "";
            ПочтовийАдрес = "";
            СлужебнийДоговорТорг = new DirectoryEmptyPointer();
            СуммаКредита = 0;
            СуммаКредитаПоставщика = 0;
            Телефони = "";
            ЮридическийАдрес = "";
            Страна = "";
            ПлательщикНалогаНаПрибиль = 0;
            Назва = "";
            Код = "";
            
            //Табличні частини
            Глубина_TablePart = new Контрагенти_Глубина_TablePart(this);
            ГлубинаКредитаПоставщика_TablePart = new Контрагенти_ГлубинаКредитаПоставщика_TablePart(this);
            СуммаКредита_TablePart = new Контрагенти_СуммаКредита_TablePart(this);
            СуммаКредитаПоставщика_TablePart = new Контрагенти_СуммаКредитаПоставщика_TablePart(this);
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                ВалютаВзаиморасчетов = new Довідники.Валюти_Pointer(base.FieldValue["col_c4"]);
                ВалютаКредита = new Довідники.Валюти_Pointer(base.FieldValue["col_a1"]);
                ВалютаКредитаПоставщика = new Довідники.Валюти_Pointer(base.FieldValue["col_a2"]);
                ВидКонтрагента = (Перелічення.ВидиКонтрагентов)base.FieldValue["col_a3"];
                Глубина = (base.FieldValue["col_a4"] != DBNull.Value) ? (int)base.FieldValue["col_a4"] : 0;
                ГлубинаКредитаПоставщика = (base.FieldValue["col_a5"] != DBNull.Value) ? (int)base.FieldValue["col_a5"] : 0;
                ДокументДатаВидачи = (base.FieldValue["col_a6"] != DBNull.Value) ? DateTime.Parse(base.FieldValue["col_a6"].ToString()) : DateTime.MinValue;
                ДокументКемВидан = base.FieldValue["col_a7"].ToString();
                ДокументНомер = base.FieldValue["col_a8"].ToString();
                ДокументСерия = base.FieldValue["col_a9"].ToString();
                ЕГРПОУ = base.FieldValue["col_b1"].ToString();
                ИНН = base.FieldValue["col_b2"].ToString();
                КатегорияЦен = new Довідники.КатегорииЦен_Pointer(base.FieldValue["col_b3"]);
                КатегорияЦенПоставщика = new Довідники.КатегорииЦен_Pointer(base.FieldValue["col_b4"]);
                Комментарий = base.FieldValue["col_b5"].ToString();
                НомерСвидетельства = base.FieldValue["col_b6"].ToString();
                ОсновнойДоговорТорг = new DirectoryEmptyPointer();
                ПолнНаименование = base.FieldValue["col_b8"].ToString();
                ПочтовийАдрес = base.FieldValue["col_b9"].ToString();
                СлужебнийДоговорТорг = new DirectoryEmptyPointer();
                СуммаКредита = (base.FieldValue["col_c2"] != DBNull.Value) ? (decimal)base.FieldValue["col_c2"] : 0;
                СуммаКредитаПоставщика = (base.FieldValue["col_c3"] != DBNull.Value) ? (decimal)base.FieldValue["col_c3"] : 0;
                Телефони = base.FieldValue["col_c5"].ToString();
                ЮридическийАдрес = base.FieldValue["col_c6"].ToString();
                Страна = base.FieldValue["col_c7"].ToString();
                ПлательщикНалогаНаПрибиль = (base.FieldValue["col_c8"] != DBNull.Value) ? (int)base.FieldValue["col_c8"] : 0;
                Назва = base.FieldValue["col_c9"].ToString();
                Код = base.FieldValue["col_d1"].ToString();
                
                BaseClear();
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["col_c4"] = ВалютаВзаиморасчетов.UnigueID.UGuid;
            base.FieldValue["col_a1"] = ВалютаКредита.UnigueID.UGuid;
            base.FieldValue["col_a2"] = ВалютаКредитаПоставщика.UnigueID.UGuid;
            base.FieldValue["col_a3"] = (int)ВидКонтрагента;
            base.FieldValue["col_a4"] = Глубина;
            base.FieldValue["col_a5"] = ГлубинаКредитаПоставщика;
            base.FieldValue["col_a6"] = ДокументДатаВидачи;
            base.FieldValue["col_a7"] = ДокументКемВидан;
            base.FieldValue["col_a8"] = ДокументНомер;
            base.FieldValue["col_a9"] = ДокументСерия;
            base.FieldValue["col_b1"] = ЕГРПОУ;
            base.FieldValue["col_b2"] = ИНН;
            base.FieldValue["col_b3"] = КатегорияЦен.UnigueID.UGuid;
            base.FieldValue["col_b4"] = КатегорияЦенПоставщика.UnigueID.UGuid;
            base.FieldValue["col_b5"] = Комментарий;
            base.FieldValue["col_b6"] = НомерСвидетельства;
            base.FieldValue["col_b7"] = ОсновнойДоговорТорг.UnigueID.UGuid;
            base.FieldValue["col_b8"] = ПолнНаименование;
            base.FieldValue["col_b9"] = ПочтовийАдрес;
            base.FieldValue["col_c1"] = СлужебнийДоговорТорг.UnigueID.UGuid;
            base.FieldValue["col_c2"] = СуммаКредита;
            base.FieldValue["col_c3"] = СуммаКредитаПоставщика;
            base.FieldValue["col_c5"] = Телефони;
            base.FieldValue["col_c6"] = ЮридическийАдрес;
            base.FieldValue["col_c7"] = Страна;
            base.FieldValue["col_c8"] = ПлательщикНалогаНаПрибиль;
            base.FieldValue["col_c9"] = Назва;
            base.FieldValue["col_d1"] = Код;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public Контрагенти_Pointer GetDirectoryPointer()
        {
            Контрагенти_Pointer directoryPointer = new Контрагенти_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public Довідники.Валюти_Pointer ВалютаВзаиморасчетов { get; set; }
        public Довідники.Валюти_Pointer ВалютаКредита { get; set; }
        public Довідники.Валюти_Pointer ВалютаКредитаПоставщика { get; set; }
        public Перелічення.ВидиКонтрагентов ВидКонтрагента { get; set; }
        public int Глубина { get; set; }
        public int ГлубинаКредитаПоставщика { get; set; }
        public DateTime ДокументДатаВидачи { get; set; }
        public string ДокументКемВидан { get; set; }
        public string ДокументНомер { get; set; }
        public string ДокументСерия { get; set; }
        public string ЕГРПОУ { get; set; }
        public string ИНН { get; set; }
        public Довідники.КатегорииЦен_Pointer КатегорияЦен { get; set; }
        public Довідники.КатегорииЦен_Pointer КатегорияЦенПоставщика { get; set; }
        public string Комментарий { get; set; }
        public string НомерСвидетельства { get; set; }
        public DirectoryEmptyPointer ОсновнойДоговорТорг { get; set; }
        public string ПолнНаименование { get; set; }
        public string ПочтовийАдрес { get; set; }
        public DirectoryEmptyPointer СлужебнийДоговорТорг { get; set; }
        public decimal СуммаКредита { get; set; }
        public decimal СуммаКредитаПоставщика { get; set; }
        public string Телефони { get; set; }
        public string ЮридическийАдрес { get; set; }
        public string Страна { get; set; }
        public int ПлательщикНалогаНаПрибиль { get; set; }
        public string Назва { get; set; }
        public string Код { get; set; }
        
        //Табличні частини
        public Контрагенти_Глубина_TablePart Глубина_TablePart { get; set; }
        public Контрагенти_ГлубинаКредитаПоставщика_TablePart ГлубинаКредитаПоставщика_TablePart { get; set; }
        public Контрагенти_СуммаКредита_TablePart СуммаКредита_TablePart { get; set; }
        public Контрагенти_СуммаКредитаПоставщика_TablePart СуммаКредитаПоставщика_TablePart { get; set; }
        
    }
    
    ///<summary>
    ///Поставщики и покупатели.
    ///</summary>
    class Контрагенти_Pointer : DirectoryPointer
    {
        public Контрагенти_Pointer(object uid = null) : base(Config.Kernel, "tab_a12")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public Контрагенти_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a12")
        {
            base.Init(uid, fields);
        } 
        
        public Контрагенти_Objest GetDirectoryObject()
        {
            Контрагенти_Objest КонтрагентиObjestItem = new Контрагенти_Objest();
            КонтрагентиObjestItem.Read(base.UnigueID);
            return КонтрагентиObjestItem;
        }
    }
    
    ///<summary>
    ///Поставщики и покупатели.
    ///</summary>
    class Контрагенти_Select : DirectorySelect, IDisposable
    {
        public Контрагенти_Select() : base(Config.Kernel, "tab_a12") { }
    
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
                Current = new Контрагенти_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields);
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }

        public Контрагенти_Pointer Current { get; private set; }
    }
    
      
    class Контрагенти_Глубина_TablePart : DirectoryTablePart
    {
        public Контрагенти_Глубина_TablePart(Контрагенти_Objest owner) : base(Config.Kernel, "tab_a13",
             new string[] { "col_a1", "col_a2" }) 
        {
            Owner = owner;
            Records = new List<Контрагенти_Глубина_TablePartRecord>();
        }
        
        public Контрагенти_Objest Owner { get; private set; }
        
        public List<Контрагенти_Глубина_TablePartRecord> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Контрагенти_Глубина_TablePartRecord record = new Контрагенти_Глубина_TablePartRecord();

                record.Дата = (fieldValue["col_a1"] != DBNull.Value) ? DateTime.Parse(fieldValue["col_a1"].ToString()) : DateTime.MinValue;
                record.Глубина = (fieldValue["col_a2"] != DBNull.Value) ? (int)fieldValue["col_a2"] : 0;
                
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

                foreach (Контрагенти_Глубина_TablePartRecord record in Records)
                {
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                    fieldValue.Add("col_a1", record.Дата);
                    fieldValue.Add("col_a2", record.Глубина);
                    
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
    
    
    class Контрагенти_Глубина_TablePartRecord : DirectoryTablePartRecord
    {
        public Контрагенти_Глубина_TablePartRecord()
        {
            Дата = DateTime.MinValue;
            Глубина = 0;
            
        }
        
        
        public Контрагенти_Глубина_TablePartRecord(
            DateTime?  _Дата = null, int _Глубина = 0)
        {
            Дата = _Дата ?? DateTime.MinValue;
            Глубина = _Глубина;
            
        }
        public DateTime Дата { get; set; }
        public int Глубина { get; set; }
        
    }
      
    class Контрагенти_ГлубинаКредитаПоставщика_TablePart : DirectoryTablePart
    {
        public Контрагенти_ГлубинаКредитаПоставщика_TablePart(Контрагенти_Objest owner) : base(Config.Kernel, "tab_a17",
             new string[] { "col_a3", "col_a4" }) 
        {
            Owner = owner;
            Records = new List<Контрагенти_ГлубинаКредитаПоставщика_TablePartRecord>();
        }
        
        public Контрагенти_Objest Owner { get; private set; }
        
        public List<Контрагенти_ГлубинаКредитаПоставщика_TablePartRecord> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Контрагенти_ГлубинаКредитаПоставщика_TablePartRecord record = new Контрагенти_ГлубинаКредитаПоставщика_TablePartRecord();

                record.Дата = (fieldValue["col_a3"] != DBNull.Value) ? DateTime.Parse(fieldValue["col_a3"].ToString()) : DateTime.MinValue;
                record.ГлубинаКредитаПоставщика = (fieldValue["col_a4"] != DBNull.Value) ? (int)fieldValue["col_a4"] : 0;
                
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

                foreach (Контрагенти_ГлубинаКредитаПоставщика_TablePartRecord record in Records)
                {
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                    fieldValue.Add("col_a3", record.Дата);
                    fieldValue.Add("col_a4", record.ГлубинаКредитаПоставщика);
                    
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
    
    
    class Контрагенти_ГлубинаКредитаПоставщика_TablePartRecord : DirectoryTablePartRecord
    {
        public Контрагенти_ГлубинаКредитаПоставщика_TablePartRecord()
        {
            Дата = DateTime.MinValue;
            ГлубинаКредитаПоставщика = 0;
            
        }
        
        
        public Контрагенти_ГлубинаКредитаПоставщика_TablePartRecord(
            DateTime?  _Дата = null, int _ГлубинаКредитаПоставщика = 0)
        {
            Дата = _Дата ?? DateTime.MinValue;
            ГлубинаКредитаПоставщика = _ГлубинаКредитаПоставщика;
            
        }
        public DateTime Дата { get; set; }
        public int ГлубинаКредитаПоставщика { get; set; }
        
    }
      
    class Контрагенти_СуммаКредита_TablePart : DirectoryTablePart
    {
        public Контрагенти_СуммаКредита_TablePart(Контрагенти_Objest owner) : base(Config.Kernel, "tab_a18",
             new string[] { "col_a1", "col_a2" }) 
        {
            Owner = owner;
            Records = new List<Контрагенти_СуммаКредита_TablePartRecord>();
        }
        
        public Контрагенти_Objest Owner { get; private set; }
        
        public List<Контрагенти_СуммаКредита_TablePartRecord> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Контрагенти_СуммаКредита_TablePartRecord record = new Контрагенти_СуммаКредита_TablePartRecord();

                record.Дата = (fieldValue["col_a1"] != DBNull.Value) ? DateTime.Parse(fieldValue["col_a1"].ToString()) : DateTime.MinValue;
                record.СуммаКредита = (fieldValue["col_a2"] != DBNull.Value) ? (decimal)fieldValue["col_a2"] : 0;
                
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

                foreach (Контрагенти_СуммаКредита_TablePartRecord record in Records)
                {
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                    fieldValue.Add("col_a1", record.Дата);
                    fieldValue.Add("col_a2", record.СуммаКредита);
                    
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
    
    
    class Контрагенти_СуммаКредита_TablePartRecord : DirectoryTablePartRecord
    {
        public Контрагенти_СуммаКредита_TablePartRecord()
        {
            Дата = DateTime.MinValue;
            СуммаКредита = 0;
            
        }
        
        
        public Контрагенти_СуммаКредита_TablePartRecord(
            DateTime?  _Дата = null, decimal _СуммаКредита = 0)
        {
            Дата = _Дата ?? DateTime.MinValue;
            СуммаКредита = _СуммаКредита;
            
        }
        public DateTime Дата { get; set; }
        public decimal СуммаКредита { get; set; }
        
    }
      
    class Контрагенти_СуммаКредитаПоставщика_TablePart : DirectoryTablePart
    {
        public Контрагенти_СуммаКредитаПоставщика_TablePart(Контрагенти_Objest owner) : base(Config.Kernel, "tab_a19",
             new string[] { "col_a3", "col_a4" }) 
        {
            Owner = owner;
            Records = new List<Контрагенти_СуммаКредитаПоставщика_TablePartRecord>();
        }
        
        public Контрагенти_Objest Owner { get; private set; }
        
        public List<Контрагенти_СуммаКредитаПоставщика_TablePartRecord> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Контрагенти_СуммаКредитаПоставщика_TablePartRecord record = new Контрагенти_СуммаКредитаПоставщика_TablePartRecord();

                record.Дата = (fieldValue["col_a3"] != DBNull.Value) ? DateTime.Parse(fieldValue["col_a3"].ToString()) : DateTime.MinValue;
                record.СуммаКредитаПоставщика = (fieldValue["col_a4"] != DBNull.Value) ? (decimal)fieldValue["col_a4"] : 0;
                
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

                foreach (Контрагенти_СуммаКредитаПоставщика_TablePartRecord record in Records)
                {
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                    fieldValue.Add("col_a3", record.Дата);
                    fieldValue.Add("col_a4", record.СуммаКредитаПоставщика);
                    
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
    
    
    class Контрагенти_СуммаКредитаПоставщика_TablePartRecord : DirectoryTablePartRecord
    {
        public Контрагенти_СуммаКредитаПоставщика_TablePartRecord()
        {
            Дата = DateTime.MinValue;
            СуммаКредитаПоставщика = 0;
            
        }
        
        
        public Контрагенти_СуммаКредитаПоставщика_TablePartRecord(
            DateTime?  _Дата = null, decimal _СуммаКредитаПоставщика = 0)
        {
            Дата = _Дата ?? DateTime.MinValue;
            СуммаКредитаПоставщика = _СуммаКредитаПоставщика;
            
        }
        public DateTime Дата { get; set; }
        public decimal СуммаКредитаПоставщика { get; set; }
        
    }
      
    class Контрагенти_Список_View : DirectoryView
    {
        public Контрагенти_Список_View() : base(Config.Kernel, "tab_a12", 
             new string[] { "col_c9", "col_a3" },
             new string[] { "Назва", "ВидКонтрагента" },
             new string[] { "string", "enum" },
             "Контрагенти_Список")
        {
            
        }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "МестаХранения"
    
    ///<summary>
    ///Склады или МОЛ.
    ///</summary>
    class МестаХранения_Objest : DirectoryObject
    {
        public МестаХранения_Objest() : base(Config.Kernel, "tab_a20",
             new string[] { "col_a1", "col_a2", "col_a3", "col_a4", "col_a5", "col_a6" }) 
        {
            ВидСклада = new DirectoryEmptyPointer();
            МОЛ = new DirectoryEmptyPointer();
            Комментарий = "";
            Назва = "";
            Код = "";
            Група = new Довідники.Групи_МестаХранения_Pointer();
            
            //Табличні частини
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                ВидСклада = new DirectoryEmptyPointer();
                МОЛ = new DirectoryEmptyPointer();
                Комментарий = base.FieldValue["col_a3"].ToString();
                Назва = base.FieldValue["col_a4"].ToString();
                Код = base.FieldValue["col_a5"].ToString();
                Група = new Довідники.Групи_МестаХранения_Pointer(base.FieldValue["col_a6"]);
                
                BaseClear();
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["col_a1"] = ВидСклада.UnigueID.UGuid;
            base.FieldValue["col_a2"] = МОЛ.UnigueID.UGuid;
            base.FieldValue["col_a3"] = Комментарий;
            base.FieldValue["col_a4"] = Назва;
            base.FieldValue["col_a5"] = Код;
            base.FieldValue["col_a6"] = Група.UnigueID.UGuid;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public МестаХранения_Pointer GetDirectoryPointer()
        {
            МестаХранения_Pointer directoryPointer = new МестаХранения_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public DirectoryEmptyPointer ВидСклада { get; set; }
        public DirectoryEmptyPointer МОЛ { get; set; }
        public string Комментарий { get; set; }
        public string Назва { get; set; }
        public string Код { get; set; }
        public Довідники.Групи_МестаХранения_Pointer Група { get; set; }
        
        //Табличні частини
        
    }
    
    ///<summary>
    ///Склады или МОЛ.
    ///</summary>
    class МестаХранения_Pointer : DirectoryPointer
    {
        public МестаХранения_Pointer(object uid = null) : base(Config.Kernel, "tab_a20")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public МестаХранения_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a20")
        {
            base.Init(uid, fields);
        } 
        
        public МестаХранения_Objest GetDirectoryObject()
        {
            МестаХранения_Objest МестаХраненияObjestItem = new МестаХранения_Objest();
            МестаХраненияObjestItem.Read(base.UnigueID);
            return МестаХраненияObjestItem;
        }
    }
    
    ///<summary>
    ///Склады или МОЛ.
    ///</summary>
    class МестаХранения_Select : DirectorySelect, IDisposable
    {
        public МестаХранения_Select() : base(Config.Kernel, "tab_a20") { }
    
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
                Current = new МестаХранения_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields);
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }

        public МестаХранения_Pointer Current { get; private set; }
    }
    
      
    class МестаХранения_Список_View : DirectoryView
    {
        public МестаХранения_Список_View() : base(Config.Kernel, "tab_a20", 
             new string[] { "col_a4" },
             new string[] { "Назва" },
             new string[] { "string" },
             "МестаХранения_Список")
        {
            
        }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "НалоговиеИнспекции"
    
    ///<summary>
    ///Содержит список налоговых инспекций.
    ///</summary>
    class НалоговиеИнспекции_Objest : DirectoryObject
    {
        public НалоговиеИнспекции_Objest() : base(Config.Kernel, "tab_a21",
             new string[] { "col_a6", "col_a7", "col_a8", "col_a9", "col_b1", "col_b2", "col_b3", "col_b4" }) 
        {
            ЕДРПОУ = "";
            Адрес = "";
            ТипДПИ = "";
            КодАдмРайона = "";
            КодДляПоиска = "";
            НаименованиеАдмРайона = "";
            Назва = "";
            Код = "";
            
            //Табличні частини
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                ЕДРПОУ = base.FieldValue["col_a6"].ToString();
                Адрес = base.FieldValue["col_a7"].ToString();
                ТипДПИ = base.FieldValue["col_a8"].ToString();
                КодАдмРайона = base.FieldValue["col_a9"].ToString();
                КодДляПоиска = base.FieldValue["col_b1"].ToString();
                НаименованиеАдмРайона = base.FieldValue["col_b2"].ToString();
                Назва = base.FieldValue["col_b3"].ToString();
                Код = base.FieldValue["col_b4"].ToString();
                
                BaseClear();
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["col_a6"] = ЕДРПОУ;
            base.FieldValue["col_a7"] = Адрес;
            base.FieldValue["col_a8"] = ТипДПИ;
            base.FieldValue["col_a9"] = КодАдмРайона;
            base.FieldValue["col_b1"] = КодДляПоиска;
            base.FieldValue["col_b2"] = НаименованиеАдмРайона;
            base.FieldValue["col_b3"] = Назва;
            base.FieldValue["col_b4"] = Код;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public НалоговиеИнспекции_Pointer GetDirectoryPointer()
        {
            НалоговиеИнспекции_Pointer directoryPointer = new НалоговиеИнспекции_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public string ЕДРПОУ { get; set; }
        public string Адрес { get; set; }
        public string ТипДПИ { get; set; }
        public string КодАдмРайона { get; set; }
        public string КодДляПоиска { get; set; }
        public string НаименованиеАдмРайона { get; set; }
        public string Назва { get; set; }
        public string Код { get; set; }
        
        //Табличні частини
        
    }
    
    ///<summary>
    ///Содержит список налоговых инспекций.
    ///</summary>
    class НалоговиеИнспекции_Pointer : DirectoryPointer
    {
        public НалоговиеИнспекции_Pointer(object uid = null) : base(Config.Kernel, "tab_a21")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public НалоговиеИнспекции_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a21")
        {
            base.Init(uid, fields);
        } 
        
        public НалоговиеИнспекции_Objest GetDirectoryObject()
        {
            НалоговиеИнспекции_Objest НалоговиеИнспекцииObjestItem = new НалоговиеИнспекции_Objest();
            НалоговиеИнспекцииObjestItem.Read(base.UnigueID);
            return НалоговиеИнспекцииObjestItem;
        }
    }
    
    ///<summary>
    ///Содержит список налоговых инспекций.
    ///</summary>
    class НалоговиеИнспекции_Select : DirectorySelect, IDisposable
    {
        public НалоговиеИнспекции_Select() : base(Config.Kernel, "tab_a21") { }
    
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
                Current = new НалоговиеИнспекции_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields);
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }

        public НалоговиеИнспекции_Pointer Current { get; private set; }
    }
    
      
    class НалоговиеИнспекции_Список_View : DirectoryView
    {
        public НалоговиеИнспекции_Список_View() : base(Config.Kernel, "tab_a21", 
             new string[] { "col_b3" },
             new string[] { "Назва" },
             new string[] { "string" },
             "НалоговиеИнспекции_Список")
        {
            
        }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "НашиДенежниеСчета"
    
    ///<summary>
    ///Банковские счета.
    ///</summary>
    class НашиДенежниеСчета_Objest : DirectoryObject
    {
        public НашиДенежниеСчета_Objest() : base(Config.Kernel, "tab_a22",
             new string[] { "col_b5", "col_b6", "col_b7", "col_b8", "col_b9", "col_c1", "col_c2", "col_c3", "col_c4", "col_c5", "col_a1", "col_a2" }) 
        {
            БанкНазвание = "";
            БанкАдрес = "";
            БанкМФО = "";
            БанкСчет = "";
            Телефони = "";
            БезНал = false;
            Валюта = new Довідники.Валюти_Pointer();
            Комментарий = "";
            ПоследнийРасхДок = 0;
            ПоследнийПрихДок = 0;
            Назва = "";
            Код = "";
            
            //Табличні частини
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                БанкНазвание = base.FieldValue["col_b5"].ToString();
                БанкАдрес = base.FieldValue["col_b6"].ToString();
                БанкМФО = base.FieldValue["col_b7"].ToString();
                БанкСчет = base.FieldValue["col_b8"].ToString();
                Телефони = base.FieldValue["col_b9"].ToString();
                БезНал = (bool)base.FieldValue["col_c1"];
                Валюта = new Довідники.Валюти_Pointer(base.FieldValue["col_c2"]);
                Комментарий = base.FieldValue["col_c3"].ToString();
                ПоследнийРасхДок = (base.FieldValue["col_c4"] != DBNull.Value) ? (int)base.FieldValue["col_c4"] : 0;
                ПоследнийПрихДок = (base.FieldValue["col_c5"] != DBNull.Value) ? (int)base.FieldValue["col_c5"] : 0;
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
            base.FieldValue["col_b5"] = БанкНазвание;
            base.FieldValue["col_b6"] = БанкАдрес;
            base.FieldValue["col_b7"] = БанкМФО;
            base.FieldValue["col_b8"] = БанкСчет;
            base.FieldValue["col_b9"] = Телефони;
            base.FieldValue["col_c1"] = БезНал;
            base.FieldValue["col_c2"] = Валюта.UnigueID.UGuid;
            base.FieldValue["col_c3"] = Комментарий;
            base.FieldValue["col_c4"] = ПоследнийРасхДок;
            base.FieldValue["col_c5"] = ПоследнийПрихДок;
            base.FieldValue["col_a1"] = Назва;
            base.FieldValue["col_a2"] = Код;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public НашиДенежниеСчета_Pointer GetDirectoryPointer()
        {
            НашиДенежниеСчета_Pointer directoryPointer = new НашиДенежниеСчета_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public string БанкНазвание { get; set; }
        public string БанкАдрес { get; set; }
        public string БанкМФО { get; set; }
        public string БанкСчет { get; set; }
        public string Телефони { get; set; }
        public bool БезНал { get; set; }
        public Довідники.Валюти_Pointer Валюта { get; set; }
        public string Комментарий { get; set; }
        public int ПоследнийРасхДок { get; set; }
        public int ПоследнийПрихДок { get; set; }
        public string Назва { get; set; }
        public string Код { get; set; }
        
        //Табличні частини
        
    }
    
    ///<summary>
    ///Банковские счета.
    ///</summary>
    class НашиДенежниеСчета_Pointer : DirectoryPointer
    {
        public НашиДенежниеСчета_Pointer(object uid = null) : base(Config.Kernel, "tab_a22")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public НашиДенежниеСчета_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a22")
        {
            base.Init(uid, fields);
        } 
        
        public НашиДенежниеСчета_Objest GetDirectoryObject()
        {
            НашиДенежниеСчета_Objest НашиДенежниеСчетаObjestItem = new НашиДенежниеСчета_Objest();
            НашиДенежниеСчетаObjestItem.Read(base.UnigueID);
            return НашиДенежниеСчетаObjestItem;
        }
    }
    
    ///<summary>
    ///Банковские счета.
    ///</summary>
    class НашиДенежниеСчета_Select : DirectorySelect, IDisposable
    {
        public НашиДенежниеСчета_Select() : base(Config.Kernel, "tab_a22") { }
    
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
                Current = new НашиДенежниеСчета_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields);
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }

        public НашиДенежниеСчета_Pointer Current { get; private set; }
    }
    
      
    class НашиДенежниеСчета_Список_View : DirectoryView
    {
        public НашиДенежниеСчета_Список_View() : base(Config.Kernel, "tab_a22", 
             new string[] { "col_a1" },
             new string[] { "Назва" },
             new string[] { "string" },
             "НашиДенежниеСчета_Список")
        {
            
        }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "Номенклатура"
    
    ///<summary>
    ///Товары, услуги, наборы.
    ///</summary>
    class Номенклатура_Objest : DirectoryObject
    {
        public Номенклатура_Objest() : base(Config.Kernel, "tab_a23",
             new string[] { "col_c6", "col_c7", "col_c8", "col_c9", "col_d1", "col_d2", "col_d3", "col_d4", "col_d5", "col_d6", "col_d7", "col_d8", "col_d9", "col_e1", "col_e2", "col_e3", "col_e4", "col_e5", "col_e6", "col_e7", "col_e8", "col_a1", "col_a2", "col_a3" }) 
        {
            ПолнНаименование = "";
            ВидТовара = 0;
            Артикул = "";
            БазоваяЕдиница = new Довідники.КлассификаторЕдИзм_Pointer();
            Вес = 0;
            ЕдиницаПоУмолчанию = new Довідники.Единици_Pointer();
            ВалютаУчета = new Довідники.Валюти_Pointer();
            УчетнаяЦена = 0;
            МинимальнийОстаток = 0;
            СтавкаНДС = new DirectoryEmptyPointer();
            СтатьяИздержекУслуги = new DirectoryEmptyPointer();
            ТипТовара = new DirectoryEmptyPointer();
            ТорговаяНаценка = 0;
            ШтрихКод = 0;
            Комментарий = "";
            Транспорт = false;
            УслугиНаСебестоимость = false;
            ЛьготаНДС = 0;
            КодЛьготи = "";
            КвоДляНН = "";
            КодУКТВЕД = new Довідники.КодиУКТВЕД_Pointer();
            Назва = "";
            Код = "";
            Група = new Довідники.Групи_Номенклатура_Pointer();
            
            //Табличні частини
            СтавкаНДС_TablePart = new Номенклатура_СтавкаНДС_TablePart(this);
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                ПолнНаименование = base.FieldValue["col_c6"].ToString();
                ВидТовара = (Перелічення.ВидиТоварів)base.FieldValue["col_c7"];
                Артикул = base.FieldValue["col_c8"].ToString();
                БазоваяЕдиница = new Довідники.КлассификаторЕдИзм_Pointer(base.FieldValue["col_c9"]);
                Вес = (base.FieldValue["col_d1"] != DBNull.Value) ? (decimal)base.FieldValue["col_d1"] : 0;
                ЕдиницаПоУмолчанию = new Довідники.Единици_Pointer(base.FieldValue["col_d2"]);
                ВалютаУчета = new Довідники.Валюти_Pointer(base.FieldValue["col_d3"]);
                УчетнаяЦена = (base.FieldValue["col_d4"] != DBNull.Value) ? (decimal)base.FieldValue["col_d4"] : 0;
                МинимальнийОстаток = (base.FieldValue["col_d5"] != DBNull.Value) ? (decimal)base.FieldValue["col_d5"] : 0;
                СтавкаНДС = new DirectoryEmptyPointer();
                СтатьяИздержекУслуги = new DirectoryEmptyPointer();
                ТипТовара = new DirectoryEmptyPointer();
                ТорговаяНаценка = (base.FieldValue["col_d9"] != DBNull.Value) ? (decimal)base.FieldValue["col_d9"] : 0;
                ШтрихКод = (base.FieldValue["col_e1"] != DBNull.Value) ? (int)base.FieldValue["col_e1"] : 0;
                Комментарий = base.FieldValue["col_e2"].ToString();
                Транспорт = (bool)base.FieldValue["col_e3"];
                УслугиНаСебестоимость = (bool)base.FieldValue["col_e4"];
                ЛьготаНДС = (base.FieldValue["col_e5"] != DBNull.Value) ? (int)base.FieldValue["col_e5"] : 0;
                КодЛьготи = base.FieldValue["col_e6"].ToString();
                КвоДляНН = base.FieldValue["col_e7"].ToString();
                КодУКТВЕД = new Довідники.КодиУКТВЕД_Pointer(base.FieldValue["col_e8"]);
                Назва = base.FieldValue["col_a1"].ToString();
                Код = base.FieldValue["col_a2"].ToString();
                Група = new Довідники.Групи_Номенклатура_Pointer(base.FieldValue["col_a3"]);
                
                BaseClear();
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["col_c6"] = ПолнНаименование;
            base.FieldValue["col_c7"] = (int)ВидТовара;
            base.FieldValue["col_c8"] = Артикул;
            base.FieldValue["col_c9"] = БазоваяЕдиница.UnigueID.UGuid;
            base.FieldValue["col_d1"] = Вес;
            base.FieldValue["col_d2"] = ЕдиницаПоУмолчанию.UnigueID.UGuid;
            base.FieldValue["col_d3"] = ВалютаУчета.UnigueID.UGuid;
            base.FieldValue["col_d4"] = УчетнаяЦена;
            base.FieldValue["col_d5"] = МинимальнийОстаток;
            base.FieldValue["col_d6"] = СтавкаНДС.UnigueID.UGuid;
            base.FieldValue["col_d7"] = СтатьяИздержекУслуги.UnigueID.UGuid;
            base.FieldValue["col_d8"] = ТипТовара.UnigueID.UGuid;
            base.FieldValue["col_d9"] = ТорговаяНаценка;
            base.FieldValue["col_e1"] = ШтрихКод;
            base.FieldValue["col_e2"] = Комментарий;
            base.FieldValue["col_e3"] = Транспорт;
            base.FieldValue["col_e4"] = УслугиНаСебестоимость;
            base.FieldValue["col_e5"] = ЛьготаНДС;
            base.FieldValue["col_e6"] = КодЛьготи;
            base.FieldValue["col_e7"] = КвоДляНН;
            base.FieldValue["col_e8"] = КодУКТВЕД.UnigueID.UGuid;
            base.FieldValue["col_a1"] = Назва;
            base.FieldValue["col_a2"] = Код;
            base.FieldValue["col_a3"] = Група.UnigueID.UGuid;
            
            BaseSave();
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
        
        public string ПолнНаименование { get; set; }
        public Перелічення.ВидиТоварів ВидТовара { get; set; }
        public string Артикул { get; set; }
        public Довідники.КлассификаторЕдИзм_Pointer БазоваяЕдиница { get; set; }
        public decimal Вес { get; set; }
        public Довідники.Единици_Pointer ЕдиницаПоУмолчанию { get; set; }
        public Довідники.Валюти_Pointer ВалютаУчета { get; set; }
        public decimal УчетнаяЦена { get; set; }
        public decimal МинимальнийОстаток { get; set; }
        public DirectoryEmptyPointer СтавкаНДС { get; set; }
        public DirectoryEmptyPointer СтатьяИздержекУслуги { get; set; }
        public DirectoryEmptyPointer ТипТовара { get; set; }
        public decimal ТорговаяНаценка { get; set; }
        public int ШтрихКод { get; set; }
        public string Комментарий { get; set; }
        public bool Транспорт { get; set; }
        public bool УслугиНаСебестоимость { get; set; }
        public int ЛьготаНДС { get; set; }
        public string КодЛьготи { get; set; }
        public string КвоДляНН { get; set; }
        public Довідники.КодиУКТВЕД_Pointer КодУКТВЕД { get; set; }
        public string Назва { get; set; }
        public string Код { get; set; }
        public Довідники.Групи_Номенклатура_Pointer Група { get; set; }
        
        //Табличні частини
        public Номенклатура_СтавкаНДС_TablePart СтавкаНДС_TablePart { get; set; }
        
    }
    
    ///<summary>
    ///Товары, услуги, наборы.
    ///</summary>
    class Номенклатура_Pointer : DirectoryPointer
    {
        public Номенклатура_Pointer(object uid = null) : base(Config.Kernel, "tab_a23")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public Номенклатура_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a23")
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
    
    ///<summary>
    ///Товары, услуги, наборы.
    ///</summary>
    class Номенклатура_Select : DirectorySelect, IDisposable
    {
        public Номенклатура_Select() : base(Config.Kernel, "tab_a23") { }
    
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
                Current = new Номенклатура_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields);
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }

        public Номенклатура_Pointer Current { get; private set; }
    }
    
      
    class Номенклатура_СтавкаНДС_TablePart : DirectoryTablePart
    {
        public Номенклатура_СтавкаНДС_TablePart(Номенклатура_Objest owner) : base(Config.Kernel, "tab_a14",
             new string[] { "col_a1", "col_a2" }) 
        {
            Owner = owner;
            Records = new List<Номенклатура_СтавкаНДС_TablePartRecord>();
        }
        
        public Номенклатура_Objest Owner { get; private set; }
        
        public List<Номенклатура_СтавкаНДС_TablePartRecord> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Номенклатура_СтавкаНДС_TablePartRecord record = new Номенклатура_СтавкаНДС_TablePartRecord();

                record.Дата = (fieldValue["col_a1"] != DBNull.Value) ? DateTime.Parse(fieldValue["col_a1"].ToString()) : DateTime.MinValue;
                record.СтавкаНДС = new DirectoryEmptyPointer();
                
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

                foreach (Номенклатура_СтавкаНДС_TablePartRecord record in Records)
                {
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                    fieldValue.Add("col_a1", record.Дата);
                    fieldValue.Add("col_a2", record.СтавкаНДС.UnigueID.UGuid);
                    
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
    
    
    class Номенклатура_СтавкаНДС_TablePartRecord : DirectoryTablePartRecord
    {
        public Номенклатура_СтавкаНДС_TablePartRecord()
        {
            Дата = DateTime.MinValue;
            СтавкаНДС = new DirectoryEmptyPointer();
            
        }
        
        
        public Номенклатура_СтавкаНДС_TablePartRecord(
            DateTime?  _Дата = null, DirectoryEmptyPointer _СтавкаНДС = null)
        {
            Дата = _Дата ?? DateTime.MinValue;
            СтавкаНДС = _СтавкаНДС ?? new DirectoryEmptyPointer();
            
        }
        public DateTime Дата { get; set; }
        public DirectoryEmptyPointer СтавкаНДС { get; set; }
        
    }
      
    class Номенклатура_Список_View : DirectoryView
    {
        public Номенклатура_Список_View() : base(Config.Kernel, "tab_a23", 
             new string[] { "col_a1", "col_a2", "col_d3" },
             new string[] { "Назва", "Код", "ВалютаУчета" },
             new string[] { "string", "string", "pointer" },
             "Номенклатура_Список")
        {
            
        }
        
    }
      
    class Номенклатура_Список2_View : DirectoryView
    {
        public Номенклатура_Список2_View() : base(Config.Kernel, "tab_a23", 
             new string[] { "col_c7", "col_c8", "col_c9", "col_d1", "col_d3", "col_a1", "col_a2", "col_a3" },
             new string[] { "ВидТовара", "Артикул", "БазоваяЕдиница", "Вес", "ВалютаУчета", "Назва", "Код", "Група" },
             new string[] { "enum", "string", "pointer", "numeric", "pointer", "string", "string", "pointer" },
             "Номенклатура_Список2")
        {
            
        }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "НомераГТД"
    
    ///<summary>
    ///Номера ГТД.
    ///</summary>
    class НомераГТД_Objest : DirectoryObject
    {
        public НомераГТД_Objest() : base(Config.Kernel, "tab_a24",
             new string[] { "col_e9", "col_f1", "col_f2", "col_f3" }) 
        {
            ДатаГТД = DateTime.MinValue;
            Комментарий = "";
            Назва = "";
            Код = "";
            
            //Табличні частини
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                ДатаГТД = (base.FieldValue["col_e9"] != DBNull.Value) ? DateTime.Parse(base.FieldValue["col_e9"].ToString()) : DateTime.MinValue;
                Комментарий = base.FieldValue["col_f1"].ToString();
                Назва = base.FieldValue["col_f2"].ToString();
                Код = base.FieldValue["col_f3"].ToString();
                
                BaseClear();
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["col_e9"] = ДатаГТД;
            base.FieldValue["col_f1"] = Комментарий;
            base.FieldValue["col_f2"] = Назва;
            base.FieldValue["col_f3"] = Код;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public НомераГТД_Pointer GetDirectoryPointer()
        {
            НомераГТД_Pointer directoryPointer = new НомераГТД_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public DateTime ДатаГТД { get; set; }
        public string Комментарий { get; set; }
        public string Назва { get; set; }
        public string Код { get; set; }
        
        //Табличні частини
        
    }
    
    ///<summary>
    ///Номера ГТД.
    ///</summary>
    class НомераГТД_Pointer : DirectoryPointer
    {
        public НомераГТД_Pointer(object uid = null) : base(Config.Kernel, "tab_a24")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public НомераГТД_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a24")
        {
            base.Init(uid, fields);
        } 
        
        public НомераГТД_Objest GetDirectoryObject()
        {
            НомераГТД_Objest НомераГТДObjestItem = new НомераГТД_Objest();
            НомераГТДObjestItem.Read(base.UnigueID);
            return НомераГТДObjestItem;
        }
    }
    
    ///<summary>
    ///Номера ГТД.
    ///</summary>
    class НомераГТД_Select : DirectorySelect, IDisposable
    {
        public НомераГТД_Select() : base(Config.Kernel, "tab_a24") { }
    
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
                Current = new НомераГТД_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields);
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }

        public НомераГТД_Pointer Current { get; private set; }
    }
    
      
    class НомераГТД_Список_View : DirectoryView
    {
        public НомераГТД_Список_View() : base(Config.Kernel, "tab_a24", 
             new string[] { "col_f2" },
             new string[] { "Назва" },
             new string[] { "string" },
             "НомераГТД_Список")
        {
            
        }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "Пользователи"
    
    ///<summary>
    ///Пользователи.
    ///</summary>
    class Пользователи_Objest : DirectoryObject
    {
        public Пользователи_Objest() : base(Config.Kernel, "tab_a25",
             new string[] { "col_f4", "col_f5", "col_f6", "col_f7", "col_f8" }) 
        {
            ОсновнаяФирма = new DirectoryEmptyPointer();
            КатегорияЦен = new Довідники.КатегорииЦен_Pointer();
            Отпустил = new DirectoryEmptyPointer();
            Назва = "";
            Код = "";
            
            //Табличні частини
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                ОсновнаяФирма = new DirectoryEmptyPointer();
                КатегорияЦен = new Довідники.КатегорииЦен_Pointer(base.FieldValue["col_f5"]);
                Отпустил = new DirectoryEmptyPointer();
                Назва = base.FieldValue["col_f7"].ToString();
                Код = base.FieldValue["col_f8"].ToString();
                
                BaseClear();
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["col_f4"] = ОсновнаяФирма.UnigueID.UGuid;
            base.FieldValue["col_f5"] = КатегорияЦен.UnigueID.UGuid;
            base.FieldValue["col_f6"] = Отпустил.UnigueID.UGuid;
            base.FieldValue["col_f7"] = Назва;
            base.FieldValue["col_f8"] = Код;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public Пользователи_Pointer GetDirectoryPointer()
        {
            Пользователи_Pointer directoryPointer = new Пользователи_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public DirectoryEmptyPointer ОсновнаяФирма { get; set; }
        public Довідники.КатегорииЦен_Pointer КатегорияЦен { get; set; }
        public DirectoryEmptyPointer Отпустил { get; set; }
        public string Назва { get; set; }
        public string Код { get; set; }
        
        //Табличні частини
        
    }
    
    ///<summary>
    ///Пользователи.
    ///</summary>
    class Пользователи_Pointer : DirectoryPointer
    {
        public Пользователи_Pointer(object uid = null) : base(Config.Kernel, "tab_a25")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public Пользователи_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a25")
        {
            base.Init(uid, fields);
        } 
        
        public Пользователи_Objest GetDirectoryObject()
        {
            Пользователи_Objest ПользователиObjestItem = new Пользователи_Objest();
            ПользователиObjestItem.Read(base.UnigueID);
            return ПользователиObjestItem;
        }
    }
    
    ///<summary>
    ///Пользователи.
    ///</summary>
    class Пользователи_Select : DirectorySelect, IDisposable
    {
        public Пользователи_Select() : base(Config.Kernel, "tab_a25") { }
    
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
                Current = new Пользователи_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields);
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }

        public Пользователи_Pointer Current { get; private set; }
    }
    
      
    class Пользователи_Список_View : DirectoryView
    {
        public Пользователи_Список_View() : base(Config.Kernel, "tab_a25", 
             new string[] { "col_f7" },
             new string[] { "Назва" },
             new string[] { "string" },
             "Пользователи_Список")
        {
            
        }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "Прайс_лист"
    
    
    class Прайс_лист_Objest : DirectoryObject
    {
        public Прайс_лист_Objest() : base(Config.Kernel, "tab_a26",
             new string[] { "col_a1", "col_a2", "col_a3", "col_a4" }) 
        {
            Товар = new Довідники.Номенклатура_Pointer();
            Комментарий = "";
            Назва = "";
            Код = "";
            
            //Табличні частини
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Товар = new Довідники.Номенклатура_Pointer(base.FieldValue["col_a1"]);
                Комментарий = base.FieldValue["col_a2"].ToString();
                Назва = base.FieldValue["col_a3"].ToString();
                Код = base.FieldValue["col_a4"].ToString();
                
                BaseClear();
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["col_a1"] = Товар.UnigueID.UGuid;
            base.FieldValue["col_a2"] = Комментарий;
            base.FieldValue["col_a3"] = Назва;
            base.FieldValue["col_a4"] = Код;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public Прайс_лист_Pointer GetDirectoryPointer()
        {
            Прайс_лист_Pointer directoryPointer = new Прайс_лист_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public Довідники.Номенклатура_Pointer Товар { get; set; }
        public string Комментарий { get; set; }
        public string Назва { get; set; }
        public string Код { get; set; }
        
        //Табличні частини
        
    }
    
    
    class Прайс_лист_Pointer : DirectoryPointer
    {
        public Прайс_лист_Pointer(object uid = null) : base(Config.Kernel, "tab_a26")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public Прайс_лист_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a26")
        {
            base.Init(uid, fields);
        } 
        
        public Прайс_лист_Objest GetDirectoryObject()
        {
            Прайс_лист_Objest Прайс_листObjestItem = new Прайс_лист_Objest();
            Прайс_листObjestItem.Read(base.UnigueID);
            return Прайс_листObjestItem;
        }
    }
    
    
    class Прайс_лист_Select : DirectorySelect, IDisposable
    {
        public Прайс_лист_Select() : base(Config.Kernel, "tab_a26") { }
    
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
                Current = new Прайс_лист_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields);
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }

        public Прайс_лист_Pointer Current { get; private set; }
    }
    
      
    class Прайс_лист_Список_View : DirectoryView
    {
        public Прайс_лист_Список_View() : base(Config.Kernel, "tab_a26", 
             new string[] { "col_a3", "col_a1" },
             new string[] { "Назва", "Товар" },
             new string[] { "string", "pointer" },
             "Прайс_лист_Список")
        {
            
        }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "РасчетниеСчета"
    
    ///<summary>
    ///Расчетные счета контрагентов.
    ///</summary>
    class РасчетниеСчета_Objest : DirectoryObject
    {
        public РасчетниеСчета_Objest() : base(Config.Kernel, "tab_a27",
             new string[] { "col_a3", "col_a4", "col_a5", "col_a6", "col_a7", "col_a8" }) 
        {
            БанкНазвание = "";
            БанкМФО = "";
            БанкСчет = "";
            НомерСчетаУстаревший = "";
            Назва = "";
            Код = "";
            
            //Табличні частини
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                БанкНазвание = base.FieldValue["col_a3"].ToString();
                БанкМФО = base.FieldValue["col_a4"].ToString();
                БанкСчет = base.FieldValue["col_a5"].ToString();
                НомерСчетаУстаревший = base.FieldValue["col_a6"].ToString();
                Назва = base.FieldValue["col_a7"].ToString();
                Код = base.FieldValue["col_a8"].ToString();
                
                BaseClear();
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["col_a3"] = БанкНазвание;
            base.FieldValue["col_a4"] = БанкМФО;
            base.FieldValue["col_a5"] = БанкСчет;
            base.FieldValue["col_a6"] = НомерСчетаУстаревший;
            base.FieldValue["col_a7"] = Назва;
            base.FieldValue["col_a8"] = Код;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public РасчетниеСчета_Pointer GetDirectoryPointer()
        {
            РасчетниеСчета_Pointer directoryPointer = new РасчетниеСчета_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public string БанкНазвание { get; set; }
        public string БанкМФО { get; set; }
        public string БанкСчет { get; set; }
        public string НомерСчетаУстаревший { get; set; }
        public string Назва { get; set; }
        public string Код { get; set; }
        
        //Табличні частини
        
    }
    
    ///<summary>
    ///Расчетные счета контрагентов.
    ///</summary>
    class РасчетниеСчета_Pointer : DirectoryPointer
    {
        public РасчетниеСчета_Pointer(object uid = null) : base(Config.Kernel, "tab_a27")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public РасчетниеСчета_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a27")
        {
            base.Init(uid, fields);
        } 
        
        public РасчетниеСчета_Objest GetDirectoryObject()
        {
            РасчетниеСчета_Objest РасчетниеСчетаObjestItem = new РасчетниеСчета_Objest();
            РасчетниеСчетаObjestItem.Read(base.UnigueID);
            return РасчетниеСчетаObjestItem;
        }
    }
    
    ///<summary>
    ///Расчетные счета контрагентов.
    ///</summary>
    class РасчетниеСчета_Select : DirectorySelect, IDisposable
    {
        public РасчетниеСчета_Select() : base(Config.Kernel, "tab_a27") { }
    
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
                Current = new РасчетниеСчета_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields);
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }

        public РасчетниеСчета_Pointer Current { get; private set; }
    }
    
      
    class РасчетниеСчета_Список_View : DirectoryView
    {
        public РасчетниеСчета_Список_View() : base(Config.Kernel, "tab_a27", 
             new string[] { "col_a7", "col_a3" },
             new string[] { "Назва", "БанкНазвание" },
             new string[] { "string", "string" },
             "РасчетниеСчета_Список")
        {
            
        }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "Словарь"
    
    ///<summary>
    ///Содержит переводы ключевых слов и фраз с русского на украинский язык.
    ///</summary>
    class Словарь_Objest : DirectoryObject
    {
        public Словарь_Objest() : base(Config.Kernel, "tab_a28",
             new string[] { "col_a9", "col_b1", "col_b2" }) 
        {
            Перевод = "";
            Назва = "";
            Код = "";
            
            //Табличні частини
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Перевод = base.FieldValue["col_a9"].ToString();
                Назва = base.FieldValue["col_b1"].ToString();
                Код = base.FieldValue["col_b2"].ToString();
                
                BaseClear();
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["col_a9"] = Перевод;
            base.FieldValue["col_b1"] = Назва;
            base.FieldValue["col_b2"] = Код;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public Словарь_Pointer GetDirectoryPointer()
        {
            Словарь_Pointer directoryPointer = new Словарь_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public string Перевод { get; set; }
        public string Назва { get; set; }
        public string Код { get; set; }
        
        //Табличні частини
        
    }
    
    ///<summary>
    ///Содержит переводы ключевых слов и фраз с русского на украинский язык.
    ///</summary>
    class Словарь_Pointer : DirectoryPointer
    {
        public Словарь_Pointer(object uid = null) : base(Config.Kernel, "tab_a28")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public Словарь_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a28")
        {
            base.Init(uid, fields);
        } 
        
        public Словарь_Objest GetDirectoryObject()
        {
            Словарь_Objest СловарьObjestItem = new Словарь_Objest();
            СловарьObjestItem.Read(base.UnigueID);
            return СловарьObjestItem;
        }
    }
    
    ///<summary>
    ///Содержит переводы ключевых слов и фраз с русского на украинский язык.
    ///</summary>
    class Словарь_Select : DirectorySelect, IDisposable
    {
        public Словарь_Select() : base(Config.Kernel, "tab_a28") { }
    
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
                Current = new Словарь_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields);
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }

        public Словарь_Pointer Current { get; private set; }
    }
    
      
    class Словарь_Список_View : DirectoryView
    {
        public Словарь_Список_View() : base(Config.Kernel, "tab_a28", 
             new string[] { "col_b1" },
             new string[] { "Назва" },
             new string[] { "string" },
             "Словарь_Список")
        {
            
        }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "Сотрудники"
    
    ///<summary>
    ///Сотрудники.
    ///</summary>
    class Сотрудники_Objest : DirectoryObject
    {
        public Сотрудники_Objest() : base(Config.Kernel, "tab_a29",
             new string[] { "col_b3", "col_b4", "col_b5", "col_b6", "col_b7", "col_b8", "col_b9", "col_c1" }) 
        {
            ДатаВидачиПаспорта = DateTime.MinValue;
            КемВиданПаспорт = "";
            НомерПаспорта = "";
            СерияПаспорта = "";
            Должность = "";
            ИНН = "";
            Назва = "";
            Код = "";
            
            //Табличні частини
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                ДатаВидачиПаспорта = (base.FieldValue["col_b3"] != DBNull.Value) ? DateTime.Parse(base.FieldValue["col_b3"].ToString()) : DateTime.MinValue;
                КемВиданПаспорт = base.FieldValue["col_b4"].ToString();
                НомерПаспорта = base.FieldValue["col_b5"].ToString();
                СерияПаспорта = base.FieldValue["col_b6"].ToString();
                Должность = base.FieldValue["col_b7"].ToString();
                ИНН = base.FieldValue["col_b8"].ToString();
                Назва = base.FieldValue["col_b9"].ToString();
                Код = base.FieldValue["col_c1"].ToString();
                
                BaseClear();
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["col_b3"] = ДатаВидачиПаспорта;
            base.FieldValue["col_b4"] = КемВиданПаспорт;
            base.FieldValue["col_b5"] = НомерПаспорта;
            base.FieldValue["col_b6"] = СерияПаспорта;
            base.FieldValue["col_b7"] = Должность;
            base.FieldValue["col_b8"] = ИНН;
            base.FieldValue["col_b9"] = Назва;
            base.FieldValue["col_c1"] = Код;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public Сотрудники_Pointer GetDirectoryPointer()
        {
            Сотрудники_Pointer directoryPointer = new Сотрудники_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public DateTime ДатаВидачиПаспорта { get; set; }
        public string КемВиданПаспорт { get; set; }
        public string НомерПаспорта { get; set; }
        public string СерияПаспорта { get; set; }
        public string Должность { get; set; }
        public string ИНН { get; set; }
        public string Назва { get; set; }
        public string Код { get; set; }
        
        //Табличні частини
        
    }
    
    ///<summary>
    ///Сотрудники.
    ///</summary>
    class Сотрудники_Pointer : DirectoryPointer
    {
        public Сотрудники_Pointer(object uid = null) : base(Config.Kernel, "tab_a29")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public Сотрудники_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a29")
        {
            base.Init(uid, fields);
        } 
        
        public Сотрудники_Objest GetDirectoryObject()
        {
            Сотрудники_Objest СотрудникиObjestItem = new Сотрудники_Objest();
            СотрудникиObjestItem.Read(base.UnigueID);
            return СотрудникиObjestItem;
        }
    }
    
    ///<summary>
    ///Сотрудники.
    ///</summary>
    class Сотрудники_Select : DirectorySelect, IDisposable
    {
        public Сотрудники_Select() : base(Config.Kernel, "tab_a29") { }
    
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
                Current = new Сотрудники_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields);
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }

        public Сотрудники_Pointer Current { get; private set; }
    }
    
      
    class Сотрудники_Список_View : DirectoryView
    {
        public Сотрудники_Список_View() : base(Config.Kernel, "tab_a29", 
             new string[] { "col_b9" },
             new string[] { "Назва" },
             new string[] { "string" },
             "Сотрудники_Список")
        {
            
        }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "ТорговоеОборудование"
    
    
    class ТорговоеОборудование_Objest : DirectoryObject
    {
        public ТорговоеОборудование_Objest() : base(Config.Kernel, "tab_a30",
             new string[] { "col_c2", "col_c3", "col_c4", "col_c5", "col_c6" }) 
        {
            РаботаСК = false;
            ТипСканера = false;
            ЕстьПрефикс = false;
            Назва = "";
            Код = "";
            
            //Табличні частини
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                РаботаСК = (bool)base.FieldValue["col_c2"];
                ТипСканера = (bool)base.FieldValue["col_c3"];
                ЕстьПрефикс = (bool)base.FieldValue["col_c4"];
                Назва = base.FieldValue["col_c5"].ToString();
                Код = base.FieldValue["col_c6"].ToString();
                
                BaseClear();
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["col_c2"] = РаботаСК;
            base.FieldValue["col_c3"] = ТипСканера;
            base.FieldValue["col_c4"] = ЕстьПрефикс;
            base.FieldValue["col_c5"] = Назва;
            base.FieldValue["col_c6"] = Код;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public ТорговоеОборудование_Pointer GetDirectoryPointer()
        {
            ТорговоеОборудование_Pointer directoryPointer = new ТорговоеОборудование_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public bool РаботаСК { get; set; }
        public bool ТипСканера { get; set; }
        public bool ЕстьПрефикс { get; set; }
        public string Назва { get; set; }
        public string Код { get; set; }
        
        //Табличні частини
        
    }
    
    
    class ТорговоеОборудование_Pointer : DirectoryPointer
    {
        public ТорговоеОборудование_Pointer(object uid = null) : base(Config.Kernel, "tab_a30")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public ТорговоеОборудование_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a30")
        {
            base.Init(uid, fields);
        } 
        
        public ТорговоеОборудование_Objest GetDirectoryObject()
        {
            ТорговоеОборудование_Objest ТорговоеОборудованиеObjestItem = new ТорговоеОборудование_Objest();
            ТорговоеОборудованиеObjestItem.Read(base.UnigueID);
            return ТорговоеОборудованиеObjestItem;
        }
    }
    
    
    class ТорговоеОборудование_Select : DirectorySelect, IDisposable
    {
        public ТорговоеОборудование_Select() : base(Config.Kernel, "tab_a30") { }
    
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
                Current = new ТорговоеОборудование_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields);
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }

        public ТорговоеОборудование_Pointer Current { get; private set; }
    }
    
      
    class ТорговоеОборудование_Список_View : DirectoryView
    {
        public ТорговоеОборудование_Список_View() : base(Config.Kernel, "tab_a30", 
             new string[] { "col_c5" },
             new string[] { "Назва" },
             new string[] { "string" },
             "ТорговоеОборудование_Список")
        {
            
        }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "Фирми"
    
    ///<summary>
    ///Справочник собственных фирм.
    ///</summary>
    class Фирми_Objest : DirectoryObject
    {
        public Фирми_Objest() : base(Config.Kernel, "tab_a31",
             new string[] { "col_c7", "col_c8", "col_c9", "col_d1", "col_d2", "col_d3", "col_d4", "col_d5", "col_d6", "col_d7", "col_d8", "col_d9", "col_e1", "col_e2", "col_e3", "col_e4", "col_e5", "col_e6", "col_e7", "col_e8", "col_e9", "col_f1" }) 
        {
            ГлавнийБухгалтер = new Довідники.Сотрудники_Pointer();
            ГНИ = "";
            ДатаРегистрации = DateTime.MinValue;
            ЕГРПОУ = "";
            ИНН = "";
            Кассир = new Довідники.Сотрудники_Pointer();
            Комментарий = "";
            МетодРасчетаСебестоимостиФинансовогоУчета = new DirectoryEmptyPointer();
            НалоговаяИнспекция = new Довідники.НалоговиеИнспекции_Pointer();
            НомерСвидетельства = "";
            ОфициальноеНаименование = "";
            ПлательщикНалогаНаПрибиль = false;
            ПолнНаименование = "";
            ПочтовийАдрес = "";
            ПрефиксНомеровДокументов = "";
            Руководитель = new Довідники.Сотрудники_Pointer();
            СчетПоУмолчанию = new Довідники.НашиДенежниеСчета_Pointer();
            Телефони = "";
            ЮридическийАдрес = "";
            ИнфОСтатусеПлательщикаНалогов = "";
            Назва = "";
            Код = "";
            
            //Табличні частини
            ГлавнийБухгалтер_TablePart = new Фирми_ГлавнийБухгалтер_TablePart(this);
            ГНИ_TablePart = new Фирми_ГНИ_TablePart(this);
            Кассир_TablePart = new Фирми_Кассир_TablePart(this);
            МетодРасчетаСебестоимостиФинансовогоУчета_TablePart = new Фирми_МетодРасчетаСебестоимостиФинансовогоУчета_TablePart(this);
            НалоговаяИнспекция_TablePart = new Фирми_НалоговаяИнспекция_TablePart(this);
            ОфициальноеНаименование_TablePart = new Фирми_ОфициальноеНаименование_TablePart(this);
            ПлательщикНалогаНаПрибиль_TablePart = new Фирми_ПлательщикНалогаНаПрибиль_TablePart(this);
            ПолнНаименование_TablePart = new Фирми_ПолнНаименование_TablePart(this);
            ПочтовийАдрес_TablePart = new Фирми_ПочтовийАдрес_TablePart(this);
            Руководитель_TablePart = new Фирми_Руководитель_TablePart(this);
            ЮридическийАдрес_TablePart = new Фирми_ЮридическийАдрес_TablePart(this);
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                ГлавнийБухгалтер = new Довідники.Сотрудники_Pointer(base.FieldValue["col_c7"]);
                ГНИ = base.FieldValue["col_c8"].ToString();
                ДатаРегистрации = (base.FieldValue["col_c9"] != DBNull.Value) ? DateTime.Parse(base.FieldValue["col_c9"].ToString()) : DateTime.MinValue;
                ЕГРПОУ = base.FieldValue["col_d1"].ToString();
                ИНН = base.FieldValue["col_d2"].ToString();
                Кассир = new Довідники.Сотрудники_Pointer(base.FieldValue["col_d3"]);
                Комментарий = base.FieldValue["col_d4"].ToString();
                МетодРасчетаСебестоимостиФинансовогоУчета = new DirectoryEmptyPointer();
                НалоговаяИнспекция = new Довідники.НалоговиеИнспекции_Pointer(base.FieldValue["col_d6"]);
                НомерСвидетельства = base.FieldValue["col_d7"].ToString();
                ОфициальноеНаименование = base.FieldValue["col_d8"].ToString();
                ПлательщикНалогаНаПрибиль = (bool)base.FieldValue["col_d9"];
                ПолнНаименование = base.FieldValue["col_e1"].ToString();
                ПочтовийАдрес = base.FieldValue["col_e2"].ToString();
                ПрефиксНомеровДокументов = base.FieldValue["col_e3"].ToString();
                Руководитель = new Довідники.Сотрудники_Pointer(base.FieldValue["col_e4"]);
                СчетПоУмолчанию = new Довідники.НашиДенежниеСчета_Pointer(base.FieldValue["col_e5"]);
                Телефони = base.FieldValue["col_e6"].ToString();
                ЮридическийАдрес = base.FieldValue["col_e7"].ToString();
                ИнфОСтатусеПлательщикаНалогов = base.FieldValue["col_e8"].ToString();
                Назва = base.FieldValue["col_e9"].ToString();
                Код = base.FieldValue["col_f1"].ToString();
                
                BaseClear();
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["col_c7"] = ГлавнийБухгалтер.UnigueID.UGuid;
            base.FieldValue["col_c8"] = ГНИ;
            base.FieldValue["col_c9"] = ДатаРегистрации;
            base.FieldValue["col_d1"] = ЕГРПОУ;
            base.FieldValue["col_d2"] = ИНН;
            base.FieldValue["col_d3"] = Кассир.UnigueID.UGuid;
            base.FieldValue["col_d4"] = Комментарий;
            base.FieldValue["col_d5"] = МетодРасчетаСебестоимостиФинансовогоУчета.UnigueID.UGuid;
            base.FieldValue["col_d6"] = НалоговаяИнспекция.UnigueID.UGuid;
            base.FieldValue["col_d7"] = НомерСвидетельства;
            base.FieldValue["col_d8"] = ОфициальноеНаименование;
            base.FieldValue["col_d9"] = ПлательщикНалогаНаПрибиль;
            base.FieldValue["col_e1"] = ПолнНаименование;
            base.FieldValue["col_e2"] = ПочтовийАдрес;
            base.FieldValue["col_e3"] = ПрефиксНомеровДокументов;
            base.FieldValue["col_e4"] = Руководитель.UnigueID.UGuid;
            base.FieldValue["col_e5"] = СчетПоУмолчанию.UnigueID.UGuid;
            base.FieldValue["col_e6"] = Телефони;
            base.FieldValue["col_e7"] = ЮридическийАдрес;
            base.FieldValue["col_e8"] = ИнфОСтатусеПлательщикаНалогов;
            base.FieldValue["col_e9"] = Назва;
            base.FieldValue["col_f1"] = Код;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public Фирми_Pointer GetDirectoryPointer()
        {
            Фирми_Pointer directoryPointer = new Фирми_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public Довідники.Сотрудники_Pointer ГлавнийБухгалтер { get; set; }
        public string ГНИ { get; set; }
        public DateTime ДатаРегистрации { get; set; }
        public string ЕГРПОУ { get; set; }
        public string ИНН { get; set; }
        public Довідники.Сотрудники_Pointer Кассир { get; set; }
        public string Комментарий { get; set; }
        public DirectoryEmptyPointer МетодРасчетаСебестоимостиФинансовогоУчета { get; set; }
        public Довідники.НалоговиеИнспекции_Pointer НалоговаяИнспекция { get; set; }
        public string НомерСвидетельства { get; set; }
        public string ОфициальноеНаименование { get; set; }
        public bool ПлательщикНалогаНаПрибиль { get; set; }
        public string ПолнНаименование { get; set; }
        public string ПочтовийАдрес { get; set; }
        public string ПрефиксНомеровДокументов { get; set; }
        public Довідники.Сотрудники_Pointer Руководитель { get; set; }
        public Довідники.НашиДенежниеСчета_Pointer СчетПоУмолчанию { get; set; }
        public string Телефони { get; set; }
        public string ЮридическийАдрес { get; set; }
        public string ИнфОСтатусеПлательщикаНалогов { get; set; }
        public string Назва { get; set; }
        public string Код { get; set; }
        
        //Табличні частини
        public Фирми_ГлавнийБухгалтер_TablePart ГлавнийБухгалтер_TablePart { get; set; }
        public Фирми_ГНИ_TablePart ГНИ_TablePart { get; set; }
        public Фирми_Кассир_TablePart Кассир_TablePart { get; set; }
        public Фирми_МетодРасчетаСебестоимостиФинансовогоУчета_TablePart МетодРасчетаСебестоимостиФинансовогоУчета_TablePart { get; set; }
        public Фирми_НалоговаяИнспекция_TablePart НалоговаяИнспекция_TablePart { get; set; }
        public Фирми_ОфициальноеНаименование_TablePart ОфициальноеНаименование_TablePart { get; set; }
        public Фирми_ПлательщикНалогаНаПрибиль_TablePart ПлательщикНалогаНаПрибиль_TablePart { get; set; }
        public Фирми_ПолнНаименование_TablePart ПолнНаименование_TablePart { get; set; }
        public Фирми_ПочтовийАдрес_TablePart ПочтовийАдрес_TablePart { get; set; }
        public Фирми_Руководитель_TablePart Руководитель_TablePart { get; set; }
        public Фирми_ЮридическийАдрес_TablePart ЮридическийАдрес_TablePart { get; set; }
        
    }
    
    ///<summary>
    ///Справочник собственных фирм.
    ///</summary>
    class Фирми_Pointer : DirectoryPointer
    {
        public Фирми_Pointer(object uid = null) : base(Config.Kernel, "tab_a31")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public Фирми_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a31")
        {
            base.Init(uid, fields);
        } 
        
        public Фирми_Objest GetDirectoryObject()
        {
            Фирми_Objest ФирмиObjestItem = new Фирми_Objest();
            ФирмиObjestItem.Read(base.UnigueID);
            return ФирмиObjestItem;
        }
    }
    
    ///<summary>
    ///Справочник собственных фирм.
    ///</summary>
    class Фирми_Select : DirectorySelect, IDisposable
    {
        public Фирми_Select() : base(Config.Kernel, "tab_a31") { }
    
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
                Current = new Фирми_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields);
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }

        public Фирми_Pointer Current { get; private set; }
    }
    
      
    class Фирми_ГлавнийБухгалтер_TablePart : DirectoryTablePart
    {
        public Фирми_ГлавнийБухгалтер_TablePart(Фирми_Objest owner) : base(Config.Kernel, "tab_a32",
             new string[] { "col_f2", "col_f3" }) 
        {
            Owner = owner;
            Records = new List<Фирми_ГлавнийБухгалтер_TablePartRecord>();
        }
        
        public Фирми_Objest Owner { get; private set; }
        
        public List<Фирми_ГлавнийБухгалтер_TablePartRecord> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Фирми_ГлавнийБухгалтер_TablePartRecord record = new Фирми_ГлавнийБухгалтер_TablePartRecord();

                record.Дата = (fieldValue["col_f2"] != DBNull.Value) ? DateTime.Parse(fieldValue["col_f2"].ToString()) : DateTime.MinValue;
                record.ГлавнийБухгалтер = fieldValue["col_f3"].ToString();
                
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

                foreach (Фирми_ГлавнийБухгалтер_TablePartRecord record in Records)
                {
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                    fieldValue.Add("col_f2", record.Дата);
                    fieldValue.Add("col_f3", record.ГлавнийБухгалтер);
                    
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
    
    
    class Фирми_ГлавнийБухгалтер_TablePartRecord : DirectoryTablePartRecord
    {
        public Фирми_ГлавнийБухгалтер_TablePartRecord()
        {
            Дата = DateTime.MinValue;
            ГлавнийБухгалтер = "";
            
        }
        
        
        public Фирми_ГлавнийБухгалтер_TablePartRecord(
            DateTime?  _Дата = null, string _ГлавнийБухгалтер = "")
        {
            Дата = _Дата ?? DateTime.MinValue;
            ГлавнийБухгалтер = _ГлавнийБухгалтер;
            
        }
        public DateTime Дата { get; set; }
        public string ГлавнийБухгалтер { get; set; }
        
    }
      
    class Фирми_ГНИ_TablePart : DirectoryTablePart
    {
        public Фирми_ГНИ_TablePart(Фирми_Objest owner) : base(Config.Kernel, "tab_a33",
             new string[] { "col_f4", "col_f5" }) 
        {
            Owner = owner;
            Records = new List<Фирми_ГНИ_TablePartRecord>();
        }
        
        public Фирми_Objest Owner { get; private set; }
        
        public List<Фирми_ГНИ_TablePartRecord> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Фирми_ГНИ_TablePartRecord record = new Фирми_ГНИ_TablePartRecord();

                record.Дата = (fieldValue["col_f4"] != DBNull.Value) ? DateTime.Parse(fieldValue["col_f4"].ToString()) : DateTime.MinValue;
                record.ГНИ = fieldValue["col_f5"].ToString();
                
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

                foreach (Фирми_ГНИ_TablePartRecord record in Records)
                {
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                    fieldValue.Add("col_f4", record.Дата);
                    fieldValue.Add("col_f5", record.ГНИ);
                    
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
    
    
    class Фирми_ГНИ_TablePartRecord : DirectoryTablePartRecord
    {
        public Фирми_ГНИ_TablePartRecord()
        {
            Дата = DateTime.MinValue;
            ГНИ = "";
            
        }
        
        
        public Фирми_ГНИ_TablePartRecord(
            DateTime?  _Дата = null, string _ГНИ = "")
        {
            Дата = _Дата ?? DateTime.MinValue;
            ГНИ = _ГНИ;
            
        }
        public DateTime Дата { get; set; }
        public string ГНИ { get; set; }
        
    }
      ///<summary>
    ///Кассир фирмы.
    ///</summary>
    class Фирми_Кассир_TablePart : DirectoryTablePart
    {
        public Фирми_Кассир_TablePart(Фирми_Objest owner) : base(Config.Kernel, "tab_a34",
             new string[] { "col_f6", "col_f7" }) 
        {
            Owner = owner;
            Records = new List<Фирми_Кассир_TablePartRecord>();
        }
        
        public Фирми_Objest Owner { get; private set; }
        
        public List<Фирми_Кассир_TablePartRecord> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Фирми_Кассир_TablePartRecord record = new Фирми_Кассир_TablePartRecord();

                record.Дата = (fieldValue["col_f6"] != DBNull.Value) ? DateTime.Parse(fieldValue["col_f6"].ToString()) : DateTime.MinValue;
                record.Кассир = new Довідники.Сотрудники_Pointer(fieldValue["col_f7"]);
                
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

                foreach (Фирми_Кассир_TablePartRecord record in Records)
                {
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                    fieldValue.Add("col_f6", record.Дата);
                    fieldValue.Add("col_f7", record.Кассир.UnigueID.UGuid);
                    
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
    ///Кассир фирмы.
    ///</summary>
    class Фирми_Кассир_TablePartRecord : DirectoryTablePartRecord
    {
        public Фирми_Кассир_TablePartRecord()
        {
            Дата = DateTime.MinValue;
            Кассир = new Довідники.Сотрудники_Pointer();
            
        }
        
        
        public Фирми_Кассир_TablePartRecord(
            DateTime?  _Дата = null, Довідники.Сотрудники_Pointer _Кассир = null)
        {
            Дата = _Дата ?? DateTime.MinValue;
            Кассир = _Кассир ?? new Довідники.Сотрудники_Pointer();
            
        }
        public DateTime Дата { get; set; }
        public Довідники.Сотрудники_Pointer Кассир { get; set; }
        
    }
      
    class Фирми_МетодРасчетаСебестоимостиФинансовогоУчета_TablePart : DirectoryTablePart
    {
        public Фирми_МетодРасчетаСебестоимостиФинансовогоУчета_TablePart(Фирми_Objest owner) : base(Config.Kernel, "tab_a35",
             new string[] { "col_f8", "col_f9" }) 
        {
            Owner = owner;
            Records = new List<Фирми_МетодРасчетаСебестоимостиФинансовогоУчета_TablePartRecord>();
        }
        
        public Фирми_Objest Owner { get; private set; }
        
        public List<Фирми_МетодРасчетаСебестоимостиФинансовогоУчета_TablePartRecord> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Фирми_МетодРасчетаСебестоимостиФинансовогоУчета_TablePartRecord record = new Фирми_МетодРасчетаСебестоимостиФинансовогоУчета_TablePartRecord();

                record.Дата = (fieldValue["col_f8"] != DBNull.Value) ? DateTime.Parse(fieldValue["col_f8"].ToString()) : DateTime.MinValue;
                record.МетодРасчетаСебестоимостиФинансовогоУчета = new DirectoryEmptyPointer();
                
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

                foreach (Фирми_МетодРасчетаСебестоимостиФинансовогоУчета_TablePartRecord record in Records)
                {
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                    fieldValue.Add("col_f8", record.Дата);
                    fieldValue.Add("col_f9", record.МетодРасчетаСебестоимостиФинансовогоУчета.UnigueID.UGuid);
                    
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
    
    
    class Фирми_МетодРасчетаСебестоимостиФинансовогоУчета_TablePartRecord : DirectoryTablePartRecord
    {
        public Фирми_МетодРасчетаСебестоимостиФинансовогоУчета_TablePartRecord()
        {
            Дата = DateTime.MinValue;
            МетодРасчетаСебестоимостиФинансовогоУчета = new DirectoryEmptyPointer();
            
        }
        
        
        public Фирми_МетодРасчетаСебестоимостиФинансовогоУчета_TablePartRecord(
            DateTime?  _Дата = null, DirectoryEmptyPointer _МетодРасчетаСебестоимостиФинансовогоУчета = null)
        {
            Дата = _Дата ?? DateTime.MinValue;
            МетодРасчетаСебестоимостиФинансовогоУчета = _МетодРасчетаСебестоимостиФинансовогоУчета ?? new DirectoryEmptyPointer();
            
        }
        public DateTime Дата { get; set; }
        public DirectoryEmptyPointer МетодРасчетаСебестоимостиФинансовогоУчета { get; set; }
        
    }
      
    class Фирми_НалоговаяИнспекция_TablePart : DirectoryTablePart
    {
        public Фирми_НалоговаяИнспекция_TablePart(Фирми_Objest owner) : base(Config.Kernel, "tab_a36",
             new string[] { "col_g1", "col_g2" }) 
        {
            Owner = owner;
            Records = new List<Фирми_НалоговаяИнспекция_TablePartRecord>();
        }
        
        public Фирми_Objest Owner { get; private set; }
        
        public List<Фирми_НалоговаяИнспекция_TablePartRecord> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Фирми_НалоговаяИнспекция_TablePartRecord record = new Фирми_НалоговаяИнспекция_TablePartRecord();

                record.Дата = (fieldValue["col_g1"] != DBNull.Value) ? DateTime.Parse(fieldValue["col_g1"].ToString()) : DateTime.MinValue;
                record.НалоговаяИнспекция = new Довідники.НалоговиеИнспекции_Pointer(fieldValue["col_g2"]);
                
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

                foreach (Фирми_НалоговаяИнспекция_TablePartRecord record in Records)
                {
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                    fieldValue.Add("col_g1", record.Дата);
                    fieldValue.Add("col_g2", record.НалоговаяИнспекция.UnigueID.UGuid);
                    
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
    
    
    class Фирми_НалоговаяИнспекция_TablePartRecord : DirectoryTablePartRecord
    {
        public Фирми_НалоговаяИнспекция_TablePartRecord()
        {
            Дата = DateTime.MinValue;
            НалоговаяИнспекция = new Довідники.НалоговиеИнспекции_Pointer();
            
        }
        
        
        public Фирми_НалоговаяИнспекция_TablePartRecord(
            DateTime?  _Дата = null, Довідники.НалоговиеИнспекции_Pointer _НалоговаяИнспекция = null)
        {
            Дата = _Дата ?? DateTime.MinValue;
            НалоговаяИнспекция = _НалоговаяИнспекция ?? new Довідники.НалоговиеИнспекции_Pointer();
            
        }
        public DateTime Дата { get; set; }
        public Довідники.НалоговиеИнспекции_Pointer НалоговаяИнспекция { get; set; }
        
    }
      
    class Фирми_ОфициальноеНаименование_TablePart : DirectoryTablePart
    {
        public Фирми_ОфициальноеНаименование_TablePart(Фирми_Objest owner) : base(Config.Kernel, "tab_a37",
             new string[] { "col_g3", "col_g4" }) 
        {
            Owner = owner;
            Records = new List<Фирми_ОфициальноеНаименование_TablePartRecord>();
        }
        
        public Фирми_Objest Owner { get; private set; }
        
        public List<Фирми_ОфициальноеНаименование_TablePartRecord> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Фирми_ОфициальноеНаименование_TablePartRecord record = new Фирми_ОфициальноеНаименование_TablePartRecord();

                record.Дата = (fieldValue["col_g3"] != DBNull.Value) ? DateTime.Parse(fieldValue["col_g3"].ToString()) : DateTime.MinValue;
                record.ОфициальноеНаименование = fieldValue["col_g4"].ToString();
                
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

                foreach (Фирми_ОфициальноеНаименование_TablePartRecord record in Records)
                {
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                    fieldValue.Add("col_g3", record.Дата);
                    fieldValue.Add("col_g4", record.ОфициальноеНаименование);
                    
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
    
    
    class Фирми_ОфициальноеНаименование_TablePartRecord : DirectoryTablePartRecord
    {
        public Фирми_ОфициальноеНаименование_TablePartRecord()
        {
            Дата = DateTime.MinValue;
            ОфициальноеНаименование = "";
            
        }
        
        
        public Фирми_ОфициальноеНаименование_TablePartRecord(
            DateTime?  _Дата = null, string _ОфициальноеНаименование = "")
        {
            Дата = _Дата ?? DateTime.MinValue;
            ОфициальноеНаименование = _ОфициальноеНаименование;
            
        }
        public DateTime Дата { get; set; }
        public string ОфициальноеНаименование { get; set; }
        
    }
      
    class Фирми_ПлательщикНалогаНаПрибиль_TablePart : DirectoryTablePart
    {
        public Фирми_ПлательщикНалогаНаПрибиль_TablePart(Фирми_Objest owner) : base(Config.Kernel, "tab_a38",
             new string[] { "col_g5", "col_g6" }) 
        {
            Owner = owner;
            Records = new List<Фирми_ПлательщикНалогаНаПрибиль_TablePartRecord>();
        }
        
        public Фирми_Objest Owner { get; private set; }
        
        public List<Фирми_ПлательщикНалогаНаПрибиль_TablePartRecord> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Фирми_ПлательщикНалогаНаПрибиль_TablePartRecord record = new Фирми_ПлательщикНалогаНаПрибиль_TablePartRecord();

                record.Дата = (fieldValue["col_g5"] != DBNull.Value) ? DateTime.Parse(fieldValue["col_g5"].ToString()) : DateTime.MinValue;
                record.ПлательщикНалогаНаПрибиль = (bool)fieldValue["col_g6"];
                
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

                foreach (Фирми_ПлательщикНалогаНаПрибиль_TablePartRecord record in Records)
                {
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                    fieldValue.Add("col_g5", record.Дата);
                    fieldValue.Add("col_g6", record.ПлательщикНалогаНаПрибиль);
                    
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
    
    
    class Фирми_ПлательщикНалогаНаПрибиль_TablePartRecord : DirectoryTablePartRecord
    {
        public Фирми_ПлательщикНалогаНаПрибиль_TablePartRecord()
        {
            Дата = DateTime.MinValue;
            ПлательщикНалогаНаПрибиль = false;
            
        }
        
        
        public Фирми_ПлательщикНалогаНаПрибиль_TablePartRecord(
            DateTime?  _Дата = null, bool _ПлательщикНалогаНаПрибиль = false)
        {
            Дата = _Дата ?? DateTime.MinValue;
            ПлательщикНалогаНаПрибиль = _ПлательщикНалогаНаПрибиль;
            
        }
        public DateTime Дата { get; set; }
        public bool ПлательщикНалогаНаПрибиль { get; set; }
        
    }
      
    class Фирми_ПолнНаименование_TablePart : DirectoryTablePart
    {
        public Фирми_ПолнНаименование_TablePart(Фирми_Objest owner) : base(Config.Kernel, "tab_a39",
             new string[] { "col_g7", "col_g8" }) 
        {
            Owner = owner;
            Records = new List<Фирми_ПолнНаименование_TablePartRecord>();
        }
        
        public Фирми_Objest Owner { get; private set; }
        
        public List<Фирми_ПолнНаименование_TablePartRecord> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Фирми_ПолнНаименование_TablePartRecord record = new Фирми_ПолнНаименование_TablePartRecord();

                record.Дата = (fieldValue["col_g7"] != DBNull.Value) ? DateTime.Parse(fieldValue["col_g7"].ToString()) : DateTime.MinValue;
                record.ПолнНаименование = fieldValue["col_g8"].ToString();
                
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

                foreach (Фирми_ПолнНаименование_TablePartRecord record in Records)
                {
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                    fieldValue.Add("col_g7", record.Дата);
                    fieldValue.Add("col_g8", record.ПолнНаименование);
                    
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
    
    
    class Фирми_ПолнНаименование_TablePartRecord : DirectoryTablePartRecord
    {
        public Фирми_ПолнНаименование_TablePartRecord()
        {
            Дата = DateTime.MinValue;
            ПолнНаименование = "";
            
        }
        
        
        public Фирми_ПолнНаименование_TablePartRecord(
            DateTime?  _Дата = null, string _ПолнНаименование = "")
        {
            Дата = _Дата ?? DateTime.MinValue;
            ПолнНаименование = _ПолнНаименование;
            
        }
        public DateTime Дата { get; set; }
        public string ПолнНаименование { get; set; }
        
    }
      
    class Фирми_ПочтовийАдрес_TablePart : DirectoryTablePart
    {
        public Фирми_ПочтовийАдрес_TablePart(Фирми_Objest owner) : base(Config.Kernel, "tab_a40",
             new string[] { "col_g9", "col_h1" }) 
        {
            Owner = owner;
            Records = new List<Фирми_ПочтовийАдрес_TablePartRecord>();
        }
        
        public Фирми_Objest Owner { get; private set; }
        
        public List<Фирми_ПочтовийАдрес_TablePartRecord> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Фирми_ПочтовийАдрес_TablePartRecord record = new Фирми_ПочтовийАдрес_TablePartRecord();

                record.Дата = (fieldValue["col_g9"] != DBNull.Value) ? DateTime.Parse(fieldValue["col_g9"].ToString()) : DateTime.MinValue;
                record.ПочтовийАдрес = fieldValue["col_h1"].ToString();
                
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

                foreach (Фирми_ПочтовийАдрес_TablePartRecord record in Records)
                {
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                    fieldValue.Add("col_g9", record.Дата);
                    fieldValue.Add("col_h1", record.ПочтовийАдрес);
                    
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
    
    
    class Фирми_ПочтовийАдрес_TablePartRecord : DirectoryTablePartRecord
    {
        public Фирми_ПочтовийАдрес_TablePartRecord()
        {
            Дата = DateTime.MinValue;
            ПочтовийАдрес = "";
            
        }
        
        
        public Фирми_ПочтовийАдрес_TablePartRecord(
            DateTime?  _Дата = null, string _ПочтовийАдрес = "")
        {
            Дата = _Дата ?? DateTime.MinValue;
            ПочтовийАдрес = _ПочтовийАдрес;
            
        }
        public DateTime Дата { get; set; }
        public string ПочтовийАдрес { get; set; }
        
    }
      
    class Фирми_Руководитель_TablePart : DirectoryTablePart
    {
        public Фирми_Руководитель_TablePart(Фирми_Objest owner) : base(Config.Kernel, "tab_a41",
             new string[] { "col_h2", "col_h3" }) 
        {
            Owner = owner;
            Records = new List<Фирми_Руководитель_TablePartRecord>();
        }
        
        public Фирми_Objest Owner { get; private set; }
        
        public List<Фирми_Руководитель_TablePartRecord> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Фирми_Руководитель_TablePartRecord record = new Фирми_Руководитель_TablePartRecord();

                record.Дата = (fieldValue["col_h2"] != DBNull.Value) ? DateTime.Parse(fieldValue["col_h2"].ToString()) : DateTime.MinValue;
                record.Руководитель = new Довідники.Сотрудники_Pointer(fieldValue["col_h3"]);
                
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

                foreach (Фирми_Руководитель_TablePartRecord record in Records)
                {
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                    fieldValue.Add("col_h2", record.Дата);
                    fieldValue.Add("col_h3", record.Руководитель.UnigueID.UGuid);
                    
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
    
    
    class Фирми_Руководитель_TablePartRecord : DirectoryTablePartRecord
    {
        public Фирми_Руководитель_TablePartRecord()
        {
            Дата = DateTime.MinValue;
            Руководитель = new Довідники.Сотрудники_Pointer();
            
        }
        
        
        public Фирми_Руководитель_TablePartRecord(
            DateTime?  _Дата = null, Довідники.Сотрудники_Pointer _Руководитель = null)
        {
            Дата = _Дата ?? DateTime.MinValue;
            Руководитель = _Руководитель ?? new Довідники.Сотрудники_Pointer();
            
        }
        public DateTime Дата { get; set; }
        public Довідники.Сотрудники_Pointer Руководитель { get; set; }
        
    }
      
    class Фирми_ЮридическийАдрес_TablePart : DirectoryTablePart
    {
        public Фирми_ЮридическийАдрес_TablePart(Фирми_Objest owner) : base(Config.Kernel, "tab_a42",
             new string[] { "col_h4", "col_h5" }) 
        {
            Owner = owner;
            Records = new List<Фирми_ЮридическийАдрес_TablePartRecord>();
        }
        
        public Фирми_Objest Owner { get; private set; }
        
        public List<Фирми_ЮридическийАдрес_TablePartRecord> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Фирми_ЮридическийАдрес_TablePartRecord record = new Фирми_ЮридическийАдрес_TablePartRecord();

                record.Дата = (fieldValue["col_h4"] != DBNull.Value) ? DateTime.Parse(fieldValue["col_h4"].ToString()) : DateTime.MinValue;
                record.ЮридическийАдрес = fieldValue["col_h5"].ToString();
                
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

                foreach (Фирми_ЮридическийАдрес_TablePartRecord record in Records)
                {
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                    fieldValue.Add("col_h4", record.Дата);
                    fieldValue.Add("col_h5", record.ЮридическийАдрес);
                    
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
    
    
    class Фирми_ЮридическийАдрес_TablePartRecord : DirectoryTablePartRecord
    {
        public Фирми_ЮридическийАдрес_TablePartRecord()
        {
            Дата = DateTime.MinValue;
            ЮридическийАдрес = "";
            
        }
        
        
        public Фирми_ЮридическийАдрес_TablePartRecord(
            DateTime?  _Дата = null, string _ЮридическийАдрес = "")
        {
            Дата = _Дата ?? DateTime.MinValue;
            ЮридическийАдрес = _ЮридическийАдрес;
            
        }
        public DateTime Дата { get; set; }
        public string ЮридическийАдрес { get; set; }
        
    }
      
    class Фирми_Список_View : DirectoryView
    {
        public Фирми_Список_View() : base(Config.Kernel, "tab_a31", 
             new string[] { "col_e9", "col_e4", "col_e6", "col_e2" },
             new string[] { "Назва", "Руководитель", "Телефони", "ПочтовийАдрес" },
             new string[] { "string", "pointer", "string", "string" },
             "Фирми_Список")
        {
            
        }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "Цени"
    
    ///<summary>
    ///Цены товаров.
    ///</summary>
    class Цени_Objest : DirectoryObject
    {
        public Цени_Objest() : base(Config.Kernel, "tab_a43",
             new string[] { "col_a1", "col_a2", "col_a3", "col_a4", "col_a5", "col_a6", "col_a7" }) 
        {
            Валюта = new Довідники.Валюти_Pointer();
            Единица = new Довідники.Единици_Pointer();
            КатегорияЦени = new Довідники.КатегорииЦен_Pointer();
            Наценка = 0;
            Цена = 0;
            Назва = "";
            Код = "";
            
            //Табличні частини
            Валюта_TablePart = new Цени_Валюта_TablePart(this);
            Единица_TablePart = new Цени_Единица_TablePart(this);
            Цена_TablePart = new Цени_Цена_TablePart(this);
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Валюта = new Довідники.Валюти_Pointer(base.FieldValue["col_a1"]);
                Единица = new Довідники.Единици_Pointer(base.FieldValue["col_a2"]);
                КатегорияЦени = new Довідники.КатегорииЦен_Pointer(base.FieldValue["col_a3"]);
                Наценка = (base.FieldValue["col_a4"] != DBNull.Value) ? (decimal)base.FieldValue["col_a4"] : 0;
                Цена = (base.FieldValue["col_a5"] != DBNull.Value) ? (decimal)base.FieldValue["col_a5"] : 0;
                Назва = base.FieldValue["col_a6"].ToString();
                Код = base.FieldValue["col_a7"].ToString();
                
                BaseClear();
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["col_a1"] = Валюта.UnigueID.UGuid;
            base.FieldValue["col_a2"] = Единица.UnigueID.UGuid;
            base.FieldValue["col_a3"] = КатегорияЦени.UnigueID.UGuid;
            base.FieldValue["col_a4"] = Наценка;
            base.FieldValue["col_a5"] = Цена;
            base.FieldValue["col_a6"] = Назва;
            base.FieldValue["col_a7"] = Код;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public Цени_Pointer GetDirectoryPointer()
        {
            Цени_Pointer directoryPointer = new Цени_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public Довідники.Валюти_Pointer Валюта { get; set; }
        public Довідники.Единици_Pointer Единица { get; set; }
        public Довідники.КатегорииЦен_Pointer КатегорияЦени { get; set; }
        public decimal Наценка { get; set; }
        public decimal Цена { get; set; }
        public string Назва { get; set; }
        public string Код { get; set; }
        
        //Табличні частини
        public Цени_Валюта_TablePart Валюта_TablePart { get; set; }
        public Цени_Единица_TablePart Единица_TablePart { get; set; }
        public Цени_Цена_TablePart Цена_TablePart { get; set; }
        
    }
    
    ///<summary>
    ///Цены товаров.
    ///</summary>
    class Цени_Pointer : DirectoryPointer
    {
        public Цени_Pointer(object uid = null) : base(Config.Kernel, "tab_a43")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public Цени_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a43")
        {
            base.Init(uid, fields);
        } 
        
        public Цени_Objest GetDirectoryObject()
        {
            Цени_Objest ЦениObjestItem = new Цени_Objest();
            ЦениObjestItem.Read(base.UnigueID);
            return ЦениObjestItem;
        }
    }
    
    ///<summary>
    ///Цены товаров.
    ///</summary>
    class Цени_Select : DirectorySelect, IDisposable
    {
        public Цени_Select() : base(Config.Kernel, "tab_a43") { }
    
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
                Current = new Цени_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields);
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }

        public Цени_Pointer Current { get; private set; }
    }
    
      
    class Цени_Валюта_TablePart : DirectoryTablePart
    {
        public Цени_Валюта_TablePart(Цени_Objest owner) : base(Config.Kernel, "tab_a44",
             new string[] { "col_a6", "col_a7" }) 
        {
            Owner = owner;
            Records = new List<Цени_Валюта_TablePartRecord>();
        }
        
        public Цени_Objest Owner { get; private set; }
        
        public List<Цени_Валюта_TablePartRecord> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Цени_Валюта_TablePartRecord record = new Цени_Валюта_TablePartRecord();

                record.Дата = (fieldValue["col_a6"] != DBNull.Value) ? DateTime.Parse(fieldValue["col_a6"].ToString()) : DateTime.MinValue;
                record.Валюта = new Довідники.Валюти_Pointer(fieldValue["col_a7"]);
                
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

                foreach (Цени_Валюта_TablePartRecord record in Records)
                {
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                    fieldValue.Add("col_a6", record.Дата);
                    fieldValue.Add("col_a7", record.Валюта.UnigueID.UGuid);
                    
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
    
    
    class Цени_Валюта_TablePartRecord : DirectoryTablePartRecord
    {
        public Цени_Валюта_TablePartRecord()
        {
            Дата = DateTime.MinValue;
            Валюта = new Довідники.Валюти_Pointer();
            
        }
        
        
        public Цени_Валюта_TablePartRecord(
            DateTime?  _Дата = null, Довідники.Валюти_Pointer _Валюта = null)
        {
            Дата = _Дата ?? DateTime.MinValue;
            Валюта = _Валюта ?? new Довідники.Валюти_Pointer();
            
        }
        public DateTime Дата { get; set; }
        public Довідники.Валюти_Pointer Валюта { get; set; }
        
    }
      
    class Цени_Единица_TablePart : DirectoryTablePart
    {
        public Цени_Единица_TablePart(Цени_Objest owner) : base(Config.Kernel, "tab_a45",
             new string[] { "col_a8", "col_a9" }) 
        {
            Owner = owner;
            Records = new List<Цени_Единица_TablePartRecord>();
        }
        
        public Цени_Objest Owner { get; private set; }
        
        public List<Цени_Единица_TablePartRecord> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Цени_Единица_TablePartRecord record = new Цени_Единица_TablePartRecord();

                record.Дата = (fieldValue["col_a8"] != DBNull.Value) ? DateTime.Parse(fieldValue["col_a8"].ToString()) : DateTime.MinValue;
                record.Единица = new Довідники.Единици_Pointer(fieldValue["col_a9"]);
                
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

                foreach (Цени_Единица_TablePartRecord record in Records)
                {
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                    fieldValue.Add("col_a8", record.Дата);
                    fieldValue.Add("col_a9", record.Единица.UnigueID.UGuid);
                    
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
    
    
    class Цени_Единица_TablePartRecord : DirectoryTablePartRecord
    {
        public Цени_Единица_TablePartRecord()
        {
            Дата = DateTime.MinValue;
            Единица = new Довідники.Единици_Pointer();
            
        }
        
        
        public Цени_Единица_TablePartRecord(
            DateTime?  _Дата = null, Довідники.Единици_Pointer _Единица = null)
        {
            Дата = _Дата ?? DateTime.MinValue;
            Единица = _Единица ?? new Довідники.Единици_Pointer();
            
        }
        public DateTime Дата { get; set; }
        public Довідники.Единици_Pointer Единица { get; set; }
        
    }
      
    class Цени_Цена_TablePart : DirectoryTablePart
    {
        public Цени_Цена_TablePart(Цени_Objest owner) : base(Config.Kernel, "tab_a46",
             new string[] { "col_b1", "col_b2" }) 
        {
            Owner = owner;
            Records = new List<Цени_Цена_TablePartRecord>();
        }
        
        public Цени_Objest Owner { get; private set; }
        
        public List<Цени_Цена_TablePartRecord> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Цени_Цена_TablePartRecord record = new Цени_Цена_TablePartRecord();

                record.Дата = (fieldValue["col_b1"] != DBNull.Value) ? DateTime.Parse(fieldValue["col_b1"].ToString()) : DateTime.MinValue;
                record.Цена = (fieldValue["col_b2"] != DBNull.Value) ? (decimal)fieldValue["col_b2"] : 0;
                
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

                foreach (Цени_Цена_TablePartRecord record in Records)
                {
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                    fieldValue.Add("col_b1", record.Дата);
                    fieldValue.Add("col_b2", record.Цена);
                    
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
    
    
    class Цени_Цена_TablePartRecord : DirectoryTablePartRecord
    {
        public Цени_Цена_TablePartRecord()
        {
            Дата = DateTime.MinValue;
            Цена = 0;
            
        }
        
        
        public Цени_Цена_TablePartRecord(
            DateTime?  _Дата = null, decimal _Цена = 0)
        {
            Дата = _Дата ?? DateTime.MinValue;
            Цена = _Цена;
            
        }
        public DateTime Дата { get; set; }
        public decimal Цена { get; set; }
        
    }
      
    class Цени_Список_View : DirectoryView
    {
        public Цени_Список_View() : base(Config.Kernel, "tab_a43", 
             new string[] { "col_a6", "col_a1", "col_a3", "col_a4" },
             new string[] { "Назва", "Валюта", "КатегорияЦени", "Наценка" },
             new string[] { "string", "pointer", "pointer", "numeric" },
             "Цени_Список")
        {
            
        }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "Групи_Номенклатура"
    
    
    class Групи_Номенклатура_Objest : DirectoryObject
    {
        public Групи_Номенклатура_Objest() : base(Config.Kernel, "tab_a01",
             new string[] { "col_a1", "col_a2", "col_a3" }) 
        {
            Назва = "";
            Родитель = new Довідники.Групи_Номенклатура_Pointer();
            Код = "";
            
            //Табличні частини
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Назва = base.FieldValue["col_a1"].ToString();
                Родитель = new Довідники.Групи_Номенклатура_Pointer(base.FieldValue["col_a2"]);
                Код = base.FieldValue["col_a3"].ToString();
                
                BaseClear();
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["col_a1"] = Назва;
            base.FieldValue["col_a2"] = Родитель.UnigueID.UGuid;
            base.FieldValue["col_a3"] = Код;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public Групи_Номенклатура_Pointer GetDirectoryPointer()
        {
            Групи_Номенклатура_Pointer directoryPointer = new Групи_Номенклатура_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public string Назва { get; set; }
        public Довідники.Групи_Номенклатура_Pointer Родитель { get; set; }
        public string Код { get; set; }
        
        //Табличні частини
        
    }
    
    
    class Групи_Номенклатура_Pointer : DirectoryPointer
    {
        public Групи_Номенклатура_Pointer(object uid = null) : base(Config.Kernel, "tab_a01")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public Групи_Номенклатура_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a01")
        {
            base.Init(uid, fields);
        } 
        
        public Групи_Номенклатура_Objest GetDirectoryObject()
        {
            Групи_Номенклатура_Objest Групи_НоменклатураObjestItem = new Групи_Номенклатура_Objest();
            Групи_НоменклатураObjestItem.Read(base.UnigueID);
            return Групи_НоменклатураObjestItem;
        }
    }
    
    
    class Групи_Номенклатура_Select : DirectorySelect, IDisposable
    {
        public Групи_Номенклатура_Select() : base(Config.Kernel, "tab_a01") { }
    
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
                Current = new Групи_Номенклатура_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields);
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }

        public Групи_Номенклатура_Pointer Current { get; private set; }
    }
    
      
    class Групи_Номенклатура_Список_View : DirectoryView
    {
        public Групи_Номенклатура_Список_View() : base(Config.Kernel, "tab_a01", 
             new string[] { "col_a1", "col_a2" },
             new string[] { "Назва", "Родитель" },
             new string[] { "string", "pointer" },
             "Групи_Номенклатура_Список")
        {
            
        }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "Групи_МестаХранения"
    
    
    class Групи_МестаХранения_Objest : DirectoryObject
    {
        public Групи_МестаХранения_Objest() : base(Config.Kernel, "tab_a15",
             new string[] { "col_a1", "col_a2", "col_a3" }) 
        {
            Назва = "";
            Код = "";
            Родитель = new Довідники.МестаХранения_Pointer();
            
            //Табличні частини
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Назва = base.FieldValue["col_a1"].ToString();
                Код = base.FieldValue["col_a2"].ToString();
                Родитель = new Довідники.МестаХранения_Pointer(base.FieldValue["col_a3"]);
                
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
            base.FieldValue["col_a3"] = Родитель.UnigueID.UGuid;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public Групи_МестаХранения_Pointer GetDirectoryPointer()
        {
            Групи_МестаХранения_Pointer directoryPointer = new Групи_МестаХранения_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public string Назва { get; set; }
        public string Код { get; set; }
        public Довідники.МестаХранения_Pointer Родитель { get; set; }
        
        //Табличні частини
        
    }
    
    
    class Групи_МестаХранения_Pointer : DirectoryPointer
    {
        public Групи_МестаХранения_Pointer(object uid = null) : base(Config.Kernel, "tab_a15")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public Групи_МестаХранения_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a15")
        {
            base.Init(uid, fields);
        } 
        
        public Групи_МестаХранения_Objest GetDirectoryObject()
        {
            Групи_МестаХранения_Objest Групи_МестаХраненияObjestItem = new Групи_МестаХранения_Objest();
            Групи_МестаХраненияObjestItem.Read(base.UnigueID);
            return Групи_МестаХраненияObjestItem;
        }
    }
    
    
    class Групи_МестаХранения_Select : DirectorySelect, IDisposable
    {
        public Групи_МестаХранения_Select() : base(Config.Kernel, "tab_a15") { }
    
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
                Current = new Групи_МестаХранения_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields);
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }

        public Групи_МестаХранения_Pointer Current { get; private set; }
    }
    
      
    class Групи_МестаХранения_Список_View : DirectoryView
    {
        public Групи_МестаХранения_Список_View() : base(Config.Kernel, "tab_a15", 
             new string[] { "col_a1", "col_a2", "col_a3" },
             new string[] { "Назва", "Код", "Родитель" },
             new string[] { "string", "string", "pointer" },
             "Групи_МестаХранения_Список")
        {
            
        }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "test"
    
    
    class test_Objest : DirectoryObject
    {
        public test_Objest() : base(Config.Kernel, "tab_a16",
             new string[] { "col_a1", "col_a2", "col_a3", "col_a4", "col_a5", "col_a6" }) 
        {
            Назва = "";
            Код = "";
            ТипПоля = 0;
            Поле2 = 0;
            Поле3 = 0;
            Поле4 = new Довідники.Номенклатура_Pointer();
            
            //Табличні частини
            esddf_TablePart = new test_esddf_TablePart(this);
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Назва = base.FieldValue["col_a1"].ToString();
                Код = base.FieldValue["col_a2"].ToString();
                ТипПоля = (Перелічення.Перелічення2)base.FieldValue["col_a3"];
                Поле2 = (Перелічення.Перелічення)base.FieldValue["col_a4"];
                Поле3 = (Перелічення.Перелічення2)base.FieldValue["col_a5"];
                Поле4 = new Довідники.Номенклатура_Pointer(base.FieldValue["col_a6"]);
                
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
            base.FieldValue["col_a3"] = (int)ТипПоля;
            base.FieldValue["col_a4"] = (int)Поле2;
            base.FieldValue["col_a5"] = (int)Поле3;
            base.FieldValue["col_a6"] = Поле4.UnigueID.UGuid;
            
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
        
        public string Назва { get; set; }
        public string Код { get; set; }
        public Перелічення.Перелічення2 ТипПоля { get; set; }
        public Перелічення.Перелічення Поле2 { get; set; }
        public Перелічення.Перелічення2 Поле3 { get; set; }
        public Довідники.Номенклатура_Pointer Поле4 { get; set; }
        
        //Табличні частини
        public test_esddf_TablePart esddf_TablePart { get; set; }
        
    }
    
    
    class test_Pointer : DirectoryPointer
    {
        public test_Pointer(object uid = null) : base(Config.Kernel, "tab_a16")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public test_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a16")
        {
            base.Init(uid, fields);
        } 
        
        public test_Objest GetDirectoryObject()
        {
            test_Objest testObjestItem = new test_Objest();
            testObjestItem.Read(base.UnigueID);
            return testObjestItem;
        }
    }
    
    
    class test_Select : DirectorySelect, IDisposable
    {
        public test_Select() : base(Config.Kernel, "tab_a16") { }
    
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
                Current = new test_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields);
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
    
      
    class test_esddf_TablePart : DirectoryTablePart
    {
        public test_esddf_TablePart(test_Objest owner) : base(Config.Kernel, "tab_a47",
             new string[] { "col_a1" }) 
        {
            Owner = owner;
            Records = new List<test_esddf_TablePartRecord>();
        }
        
        public test_Objest Owner { get; private set; }
        
        public List<test_esddf_TablePartRecord> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                test_esddf_TablePartRecord record = new test_esddf_TablePartRecord();

                record.sdfasdf = (Перелічення.Перелічення2)fieldValue["col_a1"];
                
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

                foreach (test_esddf_TablePartRecord record in Records)
                {
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                    fieldValue.Add("col_a1", record.sdfasdf);
                    
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
    
    
    class test_esddf_TablePartRecord : DirectoryTablePartRecord
    {
        public test_esddf_TablePartRecord()
        {
            sdfasdf = 0;
            
        }
        
        
        public test_esddf_TablePartRecord(
            Перелічення.Перелічення2 _sdfasdf = 0)
        {
            sdfasdf = _sdfasdf;
            
        }
        public Перелічення.Перелічення2 sdfasdf { get; set; }
        
    }
      ///<summary>
    ///Список.
    ///</summary>
    class test_Список_View : DirectoryView
    {
        public test_Список_View() : base(Config.Kernel, "tab_a16", 
             new string[] { "col_a3", "col_a2", "col_a1" },
             new string[] { "ТипПоля", "Код", "Назва" },
             new string[] { "enum", "string", "string" },
             "test_Список")
        {
            
        }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "test2"
    
    
    class test2_Objest : DirectoryObject
    {
        public test2_Objest() : base(Config.Kernel, "tab_a48",
             new string[] { "col_a1", "col_a2" }) 
        {
            Назва = "";
            Код = "";
            
            //Табличні частини
            
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
        
        public test2_Pointer GetDirectoryPointer()
        {
            test2_Pointer directoryPointer = new test2_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public string Назва { get; set; }
        public string Код { get; set; }
        
        //Табличні частини
        
    }
    
    
    class test2_Pointer : DirectoryPointer
    {
        public test2_Pointer(object uid = null) : base(Config.Kernel, "tab_a48")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public test2_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a48")
        {
            base.Init(uid, fields);
        } 
        
        public test2_Objest GetDirectoryObject()
        {
            test2_Objest test2ObjestItem = new test2_Objest();
            test2ObjestItem.Read(base.UnigueID);
            return test2ObjestItem;
        }
    }
    
    
    class test2_Select : DirectorySelect, IDisposable
    {
        public test2_Select() : base(Config.Kernel, "tab_a48") { }
    
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
                Current = new test2_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields);
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
    
      ///<summary>
    ///Список.
    ///</summary>
    class test2_Список_View : DirectoryView
    {
        public test2_Список_View() : base(Config.Kernel, "tab_a48", 
             new string[] { "col_a1", "col_a2" },
             new string[] { "Назва", "Код" },
             new string[] { "string", "string" },
             "test2_Список")
        {
            
        }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "вапвапв"
    
    
    class вапвапв_Objest : DirectoryObject
    {
        public вапвапв_Objest() : base(Config.Kernel, "tab_a49",
             new string[] { "col_a1", "col_a2", "col_a3" }) 
        {
            Назва = "";
            Код = "";
            іваіваіваівjj = "";
            
            //Табличні частини
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Назва = base.FieldValue["col_a1"].ToString();
                Код = base.FieldValue["col_a2"].ToString();
                іваіваіваівjj = base.FieldValue["col_a3"].ToString();
                
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
            base.FieldValue["col_a3"] = іваіваіваівjj;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public вапвапв_Pointer GetDirectoryPointer()
        {
            вапвапв_Pointer directoryPointer = new вапвапв_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public string Назва { get; set; }
        public string Код { get; set; }
        public string іваіваіваівjj { get; set; }
        
        //Табличні частини
        
    }
    
    
    class вапвапв_Pointer : DirectoryPointer
    {
        public вапвапв_Pointer(object uid = null) : base(Config.Kernel, "tab_a49")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public вапвапв_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a49")
        {
            base.Init(uid, fields);
        } 
        
        public вапвапв_Objest GetDirectoryObject()
        {
            вапвапв_Objest вапвапвObjestItem = new вапвапв_Objest();
            вапвапвObjestItem.Read(base.UnigueID);
            return вапвапвObjestItem;
        }
    }
    
    
    class вапвапв_Select : DirectorySelect, IDisposable
    {
        public вапвапв_Select() : base(Config.Kernel, "tab_a49") { }
    
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
                Current = new вапвапв_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields);
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }

        public вапвапв_Pointer Current { get; private set; }
    }
    
      ///<summary>
    ///Список.
    ///</summary>
    class вапвапв_Список_View : DirectoryView
    {
        public вапвапв_Список_View() : base(Config.Kernel, "tab_a49", 
             new string[] { "col_a1", "col_a2" },
             new string[] { "Назва", "Код" },
             new string[] { "string", "string" },
             "вапвапв_Список")
        {
            
        }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "іваів"
    
    
    class іваів_Objest : DirectoryObject
    {
        public іваів_Objest() : base(Config.Kernel, "tab_a50",
             new string[] { "col_a1", "col_a2", "col_a3", "col_a4", "col_a5", "col_a6", "col_a7" }) 
        {
            Назва = "";
            Код = "";
            іваі = new Довідники.Категории_Pointer();
            sddfgsd = 0;
            Документ = new Документи.Test_Pointer();
            аавпва = 0;
            asdasd = "";
            
            //Табличні частини
            asdas_TablePart = new іваів_asdas_TablePart(this);
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Назва = base.FieldValue["col_a1"].ToString();
                Код = base.FieldValue["col_a2"].ToString();
                іваі = new Довідники.Категории_Pointer(base.FieldValue["col_a3"]);
                sddfgsd = (Перелічення.ВидиКонтрагентов)base.FieldValue["col_a4"];
                Документ = new Документи.Test_Pointer(base.FieldValue["col_a5"]);
                аавпва = (Перелічення.ВидиКонтрагентов)base.FieldValue["col_a6"];
                asdasd = base.FieldValue["col_a7"].ToString();
                
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
            base.FieldValue["col_a3"] = іваі.UnigueID.UGuid;
            base.FieldValue["col_a4"] = (int)sddfgsd;
            base.FieldValue["col_a5"] = Документ.UnigueID.UGuid;
            base.FieldValue["col_a6"] = (int)аавпва;
            base.FieldValue["col_a7"] = asdasd;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public іваів_Pointer GetDirectoryPointer()
        {
            іваів_Pointer directoryPointer = new іваів_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public string Назва { get; set; }
        public string Код { get; set; }
        public Довідники.Категории_Pointer іваі { get; set; }
        public Перелічення.ВидиКонтрагентов sddfgsd { get; set; }
        public Документи.Test_Pointer Документ { get; set; }
        public Перелічення.ВидиКонтрагентов аавпва { get; set; }
        public string asdasd { get; set; }
        
        //Табличні частини
        public іваів_asdas_TablePart asdas_TablePart { get; set; }
        
    }
    
    
    class іваів_Pointer : DirectoryPointer
    {
        public іваів_Pointer(object uid = null) : base(Config.Kernel, "tab_a50")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public іваів_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a50")
        {
            base.Init(uid, fields);
        } 
        
        public іваів_Objest GetDirectoryObject()
        {
            іваів_Objest іваівObjestItem = new іваів_Objest();
            іваівObjestItem.Read(base.UnigueID);
            return іваівObjestItem;
        }
    }
    
    
    class іваів_Select : DirectorySelect, IDisposable
    {
        public іваів_Select() : base(Config.Kernel, "tab_a50") { }
    
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
                Current = new іваів_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields);
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }

        public іваів_Pointer Current { get; private set; }
    }
    
      
    class іваів_asdas_TablePart : DirectoryTablePart
    {
        public іваів_asdas_TablePart(іваів_Objest owner) : base(Config.Kernel, "tab_a51",
             new string[] { "col_a1", "col_a2", "col_a3" }) 
        {
            Owner = owner;
            Records = new List<іваів_asdas_TablePartRecord>();
        }
        
        public іваів_Objest Owner { get; private set; }
        
        public List<іваів_asdas_TablePartRecord> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                іваів_asdas_TablePartRecord record = new іваів_asdas_TablePartRecord();

                record.asdasd = fieldValue["col_a1"].ToString();
                record.asdasda = fieldValue["col_a2"].ToString();
                record.asdas = fieldValue["col_a3"].ToString();
                
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

                foreach (іваів_asdas_TablePartRecord record in Records)
                {
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                    fieldValue.Add("col_a1", record.asdasd);
                    fieldValue.Add("col_a2", record.asdasda);
                    fieldValue.Add("col_a3", record.asdas);
                    
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
    
    
    class іваів_asdas_TablePartRecord : DirectoryTablePartRecord
    {
        public іваів_asdas_TablePartRecord()
        {
            asdasd = "";
            asdasda = "";
            asdas = "";
            
        }
        
        
        public іваів_asdas_TablePartRecord(
            string _asdasd = "", string _asdasda = "", string _asdas = "")
        {
            asdasd = _asdasd;
            asdasda = _asdasda;
            asdas = _asdas;
            
        }
        public string asdasd { get; set; }
        public string asdasda { get; set; }
        public string asdas { get; set; }
        
    }
      ///<summary>
    ///Список.
    ///</summary>
    class іваів_Список_View : DirectoryView
    {
        public іваів_Список_View() : base(Config.Kernel, "tab_a50", 
             new string[] { "col_a1", "col_a2" },
             new string[] { "Назва", "Код" },
             new string[] { "string", "string" },
             "іваів_Список")
        {
            
        }
        
    }
      
    
    #endregion
    
}

namespace ConfTrade_v1_1.Перелічення
{
    ///<summary>
    ///test.
    ///</summary>
    public enum Перелічення
    {
         Один = 1,
         Два = 2,
         Три = 3
    }
    
    ///<summary>
    ///test.
    ///</summary>
    public enum Перелічення2
    {
         Один = 1,
         Два = 2,
         Три = 3
    }
    
    ///<summary>
    ///ВидыКонтрагентов.
    ///</summary>
    public enum ВидиКонтрагентов
    {
         Организация = 1,
         ЧастноеЛицо = 2,
         Нерезидент = 3,
         Безналоговые = 4
    }
    
    ///<summary>
    ///fgcchf.
    ///</summary>
    public enum test2
    {
         sdfsdfsd = 3,
         sdfsdf = 2,
         sdfsd = 1
    }
    
    ///<summary>
    ///Види товарів.
    ///</summary>
    public enum ВидиТоварів
    {
         Товар = 1,
         Послуга = 2,
         Бартер = 3
    }
    
    ///<summary>
    ///sfas.
    ///</summary>
    public enum dsfasdfas
    {
         asdasd = 15,
         asdas = 16,
         sdfsdfsd = 11,
         ass = 13,
         asas = 14,
         sfas = 17
    }
    
    
}

namespace ConfTrade_v1_1.Документи
{
    
    #region DOCUMENT "Test"
    
    
    class Test_Objest : DocumentObject
    {
        public Test_Objest() : base(Config.Kernel, "test",
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
        
        public Test_Pointer GetDocumentPointer()
        {
            Test_Pointer directoryPointer = new Test_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        
        //Табличні частини
        
    }
    
    
    class Test_Pointer : DocumentPointer
    {
        public Test_Pointer(object uid = null) : base(Config.Kernel, "test")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public Test_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "test")
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
        public Test_Select() : base(Config.Kernel, "test") { }
    
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
                Current = new Test_Pointer(base.DocumentPointerPosition.UnigueID, base.DocumentPointerPosition.Fields);
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }

        public Test_Pointer Current { get; private set; }
    }
    
      
    
    #endregion
    
    #region DOCUMENT "РозхіднаНакладна"
    
    
    class РозхіднаНакладна_Objest : DocumentObject
    {
        public РозхіднаНакладна_Objest() : base(Config.Kernel, "tab_a51",
             new string[] { "col_a1", "col_a2", "col_a4", "col_a5" }) 
        {
            Назва = "";
            Номер = 0;
            Склад = new Довідники.МестаХранения_Pointer();
            Контрагент = new Довідники.Контрагенти_Pointer();
            
            //Табличні частини
            Товари_TablePart = new РозхіднаНакладна_Товари_TablePart(this);
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Назва = base.FieldValue["col_a1"].ToString();
                Номер = (base.FieldValue["col_a2"] != DBNull.Value) ? (int)base.FieldValue["col_a2"] : 0;
                Склад = new Довідники.МестаХранения_Pointer(base.FieldValue["col_a4"]);
                Контрагент = new Довідники.Контрагенти_Pointer(base.FieldValue["col_a5"]);
                
                BaseClear();
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["col_a1"] = Назва;
            base.FieldValue["col_a2"] = Номер;
            base.FieldValue["col_a4"] = Склад.UnigueID.UGuid;
            base.FieldValue["col_a5"] = Контрагент.UnigueID.UGuid;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public РозхіднаНакладна_Pointer GetDocumentPointer()
        {
            РозхіднаНакладна_Pointer directoryPointer = new РозхіднаНакладна_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public string Назва { get; set; }
        public int Номер { get; set; }
        public Довідники.МестаХранения_Pointer Склад { get; set; }
        public Довідники.Контрагенти_Pointer Контрагент { get; set; }
        
        //Табличні частини
        public РозхіднаНакладна_Товари_TablePart Товари_TablePart { get; set; }
        
    }
    
    
    class РозхіднаНакладна_Pointer : DocumentPointer
    {
        public РозхіднаНакладна_Pointer(object uid = null) : base(Config.Kernel, "tab_a51")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public РозхіднаНакладна_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a51")
        {
            base.Init(uid, fields);
        } 
        
        public РозхіднаНакладна_Objest GetDocumentObject()
        {
            РозхіднаНакладна_Objest РозхіднаНакладнаObjestItem = new РозхіднаНакладна_Objest();
            РозхіднаНакладнаObjestItem.Read(base.UnigueID);
            return РозхіднаНакладнаObjestItem;
        }
    }
    
    
    class РозхіднаНакладна_Select : DocumentSelect, IDisposable
    {
        public РозхіднаНакладна_Select() : base(Config.Kernel, "tab_a51") { }
    
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
                Current = new РозхіднаНакладна_Pointer(base.DocumentPointerPosition.UnigueID, base.DocumentPointerPosition.Fields);
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }

        public РозхіднаНакладна_Pointer Current { get; private set; }
    }
    
      
    class РозхіднаНакладна_Товари_TablePart : DocumentTablePart
    {
        public РозхіднаНакладна_Товари_TablePart(РозхіднаНакладна_Objest owner) : base(Config.Kernel, "tab_a52",
             new string[] { "col_a2", "col_a1", "col_a3", "col_a5", "col_a6" }) 
        {
            Owner = owner;
            Records = new List<РозхіднаНакладна_Товари_TablePartRecord>();
        }
        
        public РозхіднаНакладна_Objest Owner { get; private set; }
        
        public List<РозхіднаНакладна_Товари_TablePartRecord> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                РозхіднаНакладна_Товари_TablePartRecord record = new РозхіднаНакладна_Товари_TablePartRecord();

                record.Товар = new Довідники.Номенклатура_Pointer(fieldValue["col_a2"]);
                record.Кво = (fieldValue["col_a1"] != DBNull.Value) ? (int)fieldValue["col_a1"] : 0;
                record.Сума = (fieldValue["col_a3"] != DBNull.Value) ? (decimal)fieldValue["col_a3"] : 0;
                record.Ціна = (fieldValue["col_a5"] != DBNull.Value) ? (decimal)fieldValue["col_a5"] : 0;
                record.НомерСтроки = (fieldValue["col_a6"] != DBNull.Value) ? (int)fieldValue["col_a6"] : 0;
                
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

                foreach (РозхіднаНакладна_Товари_TablePartRecord record in Records)
                {
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                    fieldValue.Add("col_a2", record.Товар.UnigueID.UGuid);
                    fieldValue.Add("col_a1", record.Кво);
                    fieldValue.Add("col_a3", record.Сума);
                    fieldValue.Add("col_a5", record.Ціна);
                    fieldValue.Add("col_a6", record.НомерСтроки);
                    
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
    
    
    class РозхіднаНакладна_Товари_TablePartRecord : DocumentTablePartRecord
    {
        public РозхіднаНакладна_Товари_TablePartRecord()
        {
            Товар = new Довідники.Номенклатура_Pointer();
            Кво = 0;
            Сума = 0;
            Ціна = 0;
            НомерСтроки = 0;
            
        }
        
        
        public РозхіднаНакладна_Товари_TablePartRecord(
            Довідники.Номенклатура_Pointer _Товар = null, int _Кво = 0, decimal _Сума = 0, decimal _Ціна = 0, int _НомерСтроки = 0)
        {
            Товар = _Товар ?? new Довідники.Номенклатура_Pointer();
            Кво = _Кво;
            Сума = _Сума;
            Ціна = _Ціна;
            НомерСтроки = _НомерСтроки;
            
        }
        public Довідники.Номенклатура_Pointer Товар { get; set; }
        public int Кво { get; set; }
        public decimal Сума { get; set; }
        public decimal Ціна { get; set; }
        public int НомерСтроки { get; set; }
        
    }
      
    
    #endregion
    
    #region DOCUMENT "ПрихіднаНакладна"
    
    
    class ПрихіднаНакладна_Objest : DocumentObject
    {
        public ПрихіднаНакладна_Objest() : base(Config.Kernel, "tab_a51",
             new string[] { "col_a1", "col_a2", "col_a3" }) 
        {
            Номер = 0;
            Склад = new Довідники.МестаХранения_Pointer();
            Контрагент = new Довідники.Контрагенти_Pointer();
            
            //Табличні частини
            Товар_TablePart = new ПрихіднаНакладна_Товар_TablePart(this);
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Номер = (base.FieldValue["col_a1"] != DBNull.Value) ? (int)base.FieldValue["col_a1"] : 0;
                Склад = new Довідники.МестаХранения_Pointer(base.FieldValue["col_a2"]);
                Контрагент = new Довідники.Контрагенти_Pointer(base.FieldValue["col_a3"]);
                
                BaseClear();
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["col_a1"] = Номер;
            base.FieldValue["col_a2"] = Склад.UnigueID.UGuid;
            base.FieldValue["col_a3"] = Контрагент.UnigueID.UGuid;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public ПрихіднаНакладна_Pointer GetDocumentPointer()
        {
            ПрихіднаНакладна_Pointer directoryPointer = new ПрихіднаНакладна_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public int Номер { get; set; }
        public Довідники.МестаХранения_Pointer Склад { get; set; }
        public Довідники.Контрагенти_Pointer Контрагент { get; set; }
        
        //Табличні частини
        public ПрихіднаНакладна_Товар_TablePart Товар_TablePart { get; set; }
        
    }
    
    
    class ПрихіднаНакладна_Pointer : DocumentPointer
    {
        public ПрихіднаНакладна_Pointer(object uid = null) : base(Config.Kernel, "tab_a51")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public ПрихіднаНакладна_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a51")
        {
            base.Init(uid, fields);
        } 
        
        public ПрихіднаНакладна_Objest GetDocumentObject()
        {
            ПрихіднаНакладна_Objest ПрихіднаНакладнаObjestItem = new ПрихіднаНакладна_Objest();
            ПрихіднаНакладнаObjestItem.Read(base.UnigueID);
            return ПрихіднаНакладнаObjestItem;
        }
    }
    
    
    class ПрихіднаНакладна_Select : DocumentSelect, IDisposable
    {
        public ПрихіднаНакладна_Select() : base(Config.Kernel, "tab_a51") { }
    
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
                Current = new ПрихіднаНакладна_Pointer(base.DocumentPointerPosition.UnigueID, base.DocumentPointerPosition.Fields);
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }

        public ПрихіднаНакладна_Pointer Current { get; private set; }
    }
    
      
    class ПрихіднаНакладна_Товар_TablePart : DocumentTablePart
    {
        public ПрихіднаНакладна_Товар_TablePart(ПрихіднаНакладна_Objest owner) : base(Config.Kernel, "tab_a52",
             new string[] { "col_a2", "col_a3", "col_a4", "col_a5", "col_a6" }) 
        {
            Owner = owner;
            Records = new List<ПрихіднаНакладна_Товар_TablePartRecord>();
        }
        
        public ПрихіднаНакладна_Objest Owner { get; private set; }
        
        public List<ПрихіднаНакладна_Товар_TablePartRecord> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                ПрихіднаНакладна_Товар_TablePartRecord record = new ПрихіднаНакладна_Товар_TablePartRecord();

                record.Товар = new Довідники.Номенклатура_Pointer(fieldValue["col_a2"]);
                record.Кво = (fieldValue["col_a3"] != DBNull.Value) ? (int)fieldValue["col_a3"] : 0;
                record.Сума = (fieldValue["col_a4"] != DBNull.Value) ? (decimal)fieldValue["col_a4"] : 0;
                record.Ціна = (fieldValue["col_a5"] != DBNull.Value) ? (decimal)fieldValue["col_a5"] : 0;
                record.НомерСтроки = (fieldValue["col_a6"] != DBNull.Value) ? (int)fieldValue["col_a6"] : 0;
                
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

                foreach (ПрихіднаНакладна_Товар_TablePartRecord record in Records)
                {
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                    fieldValue.Add("col_a2", record.Товар.UnigueID.UGuid);
                    fieldValue.Add("col_a3", record.Кво);
                    fieldValue.Add("col_a4", record.Сума);
                    fieldValue.Add("col_a5", record.Ціна);
                    fieldValue.Add("col_a6", record.НомерСтроки);
                    
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
    
    
    class ПрихіднаНакладна_Товар_TablePartRecord : DocumentTablePartRecord
    {
        public ПрихіднаНакладна_Товар_TablePartRecord()
        {
            Товар = new Довідники.Номенклатура_Pointer();
            Кво = 0;
            Сума = 0;
            Ціна = 0;
            НомерСтроки = 0;
            
        }
        
        
        public ПрихіднаНакладна_Товар_TablePartRecord(
            Довідники.Номенклатура_Pointer _Товар = null, int _Кво = 0, decimal _Сума = 0, decimal _Ціна = 0, int _НомерСтроки = 0)
        {
            Товар = _Товар ?? new Довідники.Номенклатура_Pointer();
            Кво = _Кво;
            Сума = _Сума;
            Ціна = _Ціна;
            НомерСтроки = _НомерСтроки;
            
        }
        public Довідники.Номенклатура_Pointer Товар { get; set; }
        public int Кво { get; set; }
        public decimal Сума { get; set; }
        public decimal Ціна { get; set; }
        public int НомерСтроки { get; set; }
        
    }
      
    
    #endregion
    
    #region DOCUMENT "ПрихіднийКасовийОрдер"
    
    ///<summary>
    ///Прихідний касовий ордер.
    ///</summary>
    class ПрихіднийКасовийОрдер_Objest : DocumentObject
    {
        public ПрихіднийКасовийОрдер_Objest() : base(Config.Kernel, "tab_a52",
             new string[] { "col_a1", "col_a2", "col_a3", "col_a4", "col_a5" }) 
        {
            Контрагент = new Довідники.Контрагенти_Pointer();
            Каса = new Довідники.НашиДенежниеСчета_Pointer();
            Сума = 0;
            ДатаДок = DateTime.MinValue;
            НомерДок = 0;
            
            //Табличні частини
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Контрагент = new Довідники.Контрагенти_Pointer(base.FieldValue["col_a1"]);
                Каса = new Довідники.НашиДенежниеСчета_Pointer(base.FieldValue["col_a2"]);
                Сума = (base.FieldValue["col_a3"] != DBNull.Value) ? (decimal)base.FieldValue["col_a3"] : 0;
                ДатаДок = (base.FieldValue["col_a4"] != DBNull.Value) ? DateTime.Parse(base.FieldValue["col_a4"].ToString()) : DateTime.MinValue;
                НомерДок = (base.FieldValue["col_a5"] != DBNull.Value) ? (int)base.FieldValue["col_a5"] : 0;
                
                BaseClear();
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["col_a1"] = Контрагент.UnigueID.UGuid;
            base.FieldValue["col_a2"] = Каса.UnigueID.UGuid;
            base.FieldValue["col_a3"] = Сума;
            base.FieldValue["col_a4"] = ДатаДок;
            base.FieldValue["col_a5"] = НомерДок;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public ПрихіднийКасовийОрдер_Pointer GetDocumentPointer()
        {
            ПрихіднийКасовийОрдер_Pointer directoryPointer = new ПрихіднийКасовийОрдер_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public Довідники.Контрагенти_Pointer Контрагент { get; set; }
        public Довідники.НашиДенежниеСчета_Pointer Каса { get; set; }
        public decimal Сума { get; set; }
        public DateTime ДатаДок { get; set; }
        public int НомерДок { get; set; }
        
        //Табличні частини
        
    }
    
    ///<summary>
    ///Прихідний касовий ордер.
    ///</summary>
    class ПрихіднийКасовийОрдер_Pointer : DocumentPointer
    {
        public ПрихіднийКасовийОрдер_Pointer(object uid = null) : base(Config.Kernel, "tab_a52")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public ПрихіднийКасовийОрдер_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a52")
        {
            base.Init(uid, fields);
        } 
        
        public ПрихіднийКасовийОрдер_Objest GetDocumentObject()
        {
            ПрихіднийКасовийОрдер_Objest ПрихіднийКасовийОрдерObjestItem = new ПрихіднийКасовийОрдер_Objest();
            ПрихіднийКасовийОрдерObjestItem.Read(base.UnigueID);
            return ПрихіднийКасовийОрдерObjestItem;
        }
    }
    
    ///<summary>
    ///Прихідний касовий ордер.
    ///</summary>
    class ПрихіднийКасовийОрдер_Select : DocumentSelect, IDisposable
    {
        public ПрихіднийКасовийОрдер_Select() : base(Config.Kernel, "tab_a52") { }
    
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
                Current = new ПрихіднийКасовийОрдер_Pointer(base.DocumentPointerPosition.UnigueID, base.DocumentPointerPosition.Fields);
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }

        public ПрихіднийКасовийОрдер_Pointer Current { get; private set; }
    }
    
      
    
    #endregion
    
    #region DOCUMENT "РозхіднийКасовийОрдер"
    
    
    class РозхіднийКасовийОрдер_Objest : DocumentObject
    {
        public РозхіднийКасовийОрдер_Objest() : base(Config.Kernel, "tab_a53",
             new string[] { "col_a6", "col_a7", "col_a8", "col_a9", "col_b1" }) 
        {
            Контрагент = new Довідники.Контрагенти_Pointer();
            Каса = new Довідники.НашиДенежниеСчета_Pointer();
            Сума = 0;
            ДатаДок = DateTime.MinValue;
            НомерДок = 0;
            
            //Табличні частини
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Контрагент = new Довідники.Контрагенти_Pointer(base.FieldValue["col_a6"]);
                Каса = new Довідники.НашиДенежниеСчета_Pointer(base.FieldValue["col_a7"]);
                Сума = (base.FieldValue["col_a8"] != DBNull.Value) ? (decimal)base.FieldValue["col_a8"] : 0;
                ДатаДок = (base.FieldValue["col_a9"] != DBNull.Value) ? DateTime.Parse(base.FieldValue["col_a9"].ToString()) : DateTime.MinValue;
                НомерДок = (base.FieldValue["col_b1"] != DBNull.Value) ? (int)base.FieldValue["col_b1"] : 0;
                
                BaseClear();
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["col_a6"] = Контрагент.UnigueID.UGuid;
            base.FieldValue["col_a7"] = Каса.UnigueID.UGuid;
            base.FieldValue["col_a8"] = Сума;
            base.FieldValue["col_a9"] = ДатаДок;
            base.FieldValue["col_b1"] = НомерДок;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public РозхіднийКасовийОрдер_Pointer GetDocumentPointer()
        {
            РозхіднийКасовийОрдер_Pointer directoryPointer = new РозхіднийКасовийОрдер_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public Довідники.Контрагенти_Pointer Контрагент { get; set; }
        public Довідники.НашиДенежниеСчета_Pointer Каса { get; set; }
        public decimal Сума { get; set; }
        public DateTime ДатаДок { get; set; }
        public int НомерДок { get; set; }
        
        //Табличні частини
        
    }
    
    
    class РозхіднийКасовийОрдер_Pointer : DocumentPointer
    {
        public РозхіднийКасовийОрдер_Pointer(object uid = null) : base(Config.Kernel, "tab_a53")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public РозхіднийКасовийОрдер_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a53")
        {
            base.Init(uid, fields);
        } 
        
        public РозхіднийКасовийОрдер_Objest GetDocumentObject()
        {
            РозхіднийКасовийОрдер_Objest РозхіднийКасовийОрдерObjestItem = new РозхіднийКасовийОрдер_Objest();
            РозхіднийКасовийОрдерObjestItem.Read(base.UnigueID);
            return РозхіднийКасовийОрдерObjestItem;
        }
    }
    
    
    class РозхіднийКасовийОрдер_Select : DocumentSelect, IDisposable
    {
        public РозхіднийКасовийОрдер_Select() : base(Config.Kernel, "tab_a53") { }
    
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
                Current = new РозхіднийКасовийОрдер_Pointer(base.DocumentPointerPosition.UnigueID, base.DocumentPointerPosition.Fields);
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }

        public РозхіднийКасовийОрдер_Pointer Current { get; private set; }
    }
    
      
    
    #endregion
    
}

namespace ConfTrade_v1_1.Журнали
{

}

namespace ConfTrade_v1_1.Регістри
{

}
  
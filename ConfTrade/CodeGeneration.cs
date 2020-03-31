
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
  
 * Дата конфігурації: 31.03.2020 10:53:01
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

namespace ConfTrade_v1_1.Константи
{
    
    static class Основний
    {
        public static void ReadAll()
        {
            
            Dictionary<string, object> fieldValue = new Dictionary<string, object>();
            bool IsSelect = Config.Kernel.DataBase.SelectAllConstants("tab_constants",
                 new string[] { "col_a2", "col_a3", "col_a4", "col_a5", "col_a6", "col_a7", "col_a8", "col_a9", "col_b1", "col_b2" }, fieldValue);
            
            if (IsSelect)
            {
                m_a1_Const = fieldValue["col_a2"].ToString();
                m_a2_Const = (fieldValue["col_a3"] != DBNull.Value) ? (int)fieldValue["col_a3"] : 0;
                m_a3_Const = (fieldValue["col_a4"] != DBNull.Value) ? (decimal)fieldValue["col_a4"] : 0;
                m_a4_Const = (fieldValue["col_a5"] != DBNull.Value) ? bool.Parse(fieldValue["col_a5"].ToString()) : false;
                m_a5_Const = (fieldValue["col_a6"] != DBNull.Value) ? DateTime.Parse(fieldValue["col_a6"].ToString()) : DateTime.MinValue;
                m_a6_Const = (fieldValue["col_a7"] != DBNull.Value) ? DateTime.Parse(fieldValue["col_a7"].ToString()) : DateTime.MinValue;
                m_a7_Const = (fieldValue["col_a8"] != DBNull.Value) ? TimeSpan.Parse(fieldValue["col_a8"].ToString()) : DateTime.MinValue.TimeOfDay;
                m_a8_Const = (fieldValue["col_a9"] != DBNull.Value) ? (Перелічення.Test)fieldValue["col_a9"] : 0;
                m_a9_Const = new EmptyPointer();
                m_a10_Const = (fieldValue["col_b2"] != DBNull.Value) ? (string[])fieldValue["col_b2"] : new string[] { };
                
            }
        }
        
        
        static string m_a1_Const = "";
        public static string a1_Const
        {
            get
            {
                return m_a1_Const;
            }
            set
            {
                m_a1_Const = value;
                Config.Kernel.DataBase.SaveConstants("tab_constants", "col_a2", m_a1_Const);
            }
        }
        
        static int m_a2_Const = 0;
        public static int a2_Const
        {
            get
            {
                return m_a2_Const;
            }
            set
            {
                m_a2_Const = value;
                Config.Kernel.DataBase.SaveConstants("tab_constants", "col_a3", m_a2_Const);
            }
        }
        
        static decimal m_a3_Const = 0;
        public static decimal a3_Const
        {
            get
            {
                return m_a3_Const;
            }
            set
            {
                m_a3_Const = value;
                Config.Kernel.DataBase.SaveConstants("tab_constants", "col_a4", m_a3_Const);
            }
        }
        
        static bool m_a4_Const = false;
        public static bool a4_Const
        {
            get
            {
                return m_a4_Const;
            }
            set
            {
                m_a4_Const = value;
                Config.Kernel.DataBase.SaveConstants("tab_constants", "col_a5", m_a4_Const);
            }
        }
        
        static DateTime m_a5_Const = DateTime.MinValue;
        public static DateTime a5_Const
        {
            get
            {
                return m_a5_Const;
            }
            set
            {
                m_a5_Const = value;
                Config.Kernel.DataBase.SaveConstants("tab_constants", "col_a6", m_a5_Const);
            }
        }
        
        static DateTime m_a6_Const = DateTime.MinValue;
        public static DateTime a6_Const
        {
            get
            {
                return m_a6_Const;
            }
            set
            {
                m_a6_Const = value;
                Config.Kernel.DataBase.SaveConstants("tab_constants", "col_a7", m_a6_Const);
            }
        }
        
        static TimeSpan m_a7_Const = DateTime.MinValue.TimeOfDay;
        public static TimeSpan a7_Const
        {
            get
            {
                return m_a7_Const;
            }
            set
            {
                m_a7_Const = value;
                Config.Kernel.DataBase.SaveConstants("tab_constants", "col_a8", m_a7_Const);
            }
        }
        
        static Перелічення.Test m_a8_Const = 0;
        public static Перелічення.Test a8_Const
        {
            get
            {
                return m_a8_Const;
            }
            set
            {
                m_a8_Const = value;
                Config.Kernel.DataBase.SaveConstants("tab_constants", "col_a9", (int)m_a8_Const);
            }
        }
        
        static EmptyPointer m_a9_Const = new EmptyPointer();
        public static EmptyPointer a9_Const
        {
            get
            {
                return m_a9_Const;
            }
            set
            {
                m_a9_Const = value;
                Config.Kernel.DataBase.SaveConstants("tab_constants", "col_b1", m_a9_Const.UnigueID.UGuid);
            }
        }
        
        static string[] m_a10_Const = new string[] { };
        public static string[] a10_Const
        {
            get
            {
                return m_a10_Const;
            }
            set
            {
                m_a10_Const = value;
                Config.Kernel.DataBase.SaveConstants("tab_constants", "col_b2", m_a10_Const);
            }
        }
        
        public class a1_Історія_TablePart : ConstantsTablePart
        {
            public a1_Історія_TablePart() : base(Config.Kernel, "tab_a02",
                 new string[] { "col_a1", "col_a2" }) 
            {
                Records = new List<Record>();
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
                    
                    record.Дата = (fieldValue["col_a1"] != DBNull.Value) ? DateTime.Parse(fieldValue["col_a1"].ToString()) : DateTime.MinValue;
                    record.Значення = fieldValue["col_a2"].ToString();
                    
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

                    foreach (Record record in Records)
                    {
                        Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                        fieldValue.Add("col_a1", record.Дата);
                        fieldValue.Add("col_a2", record.Значення);
                        
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
            
            public class Record : ConstantsTablePartRecord
            {
                public Record()
                {
                    Дата = DateTime.MinValue;
                    Значення = "";
                    
                }
        
                
                public Record(
                    DateTime?  _Дата = null, string _Значення = "")
                {
                    Дата = _Дата ?? DateTime.MinValue;
                    Значення = _Значення;
                    
                }
                public DateTime Дата { get; set; }
                public string Значення { get; set; }
                
            }            
        }
               
    }
    
    static class Другий
    {
        public static void ReadAll()
        {
            
            Dictionary<string, object> fieldValue = new Dictionary<string, object>();
            bool IsSelect = Config.Kernel.DataBase.SelectAllConstants("tab_constants",
                 new string[] { "col_a1" }, fieldValue);
            
            if (IsSelect)
            {
                m_ОсновнаФірма_Const = new Довідники.Організації_Pointer(fieldValue["col_a1"]);
                
            }
        }
        
        
        static Довідники.Організації_Pointer m_ОсновнаФірма_Const = new Довідники.Організації_Pointer();
        public static Довідники.Організації_Pointer ОсновнаФірма_Const
        {
            get
            {
                return m_ОсновнаФірма_Const;
            }
            set
            {
                m_ОсновнаФірма_Const = value;
                Config.Kernel.DataBase.SaveConstants("tab_constants", "col_a1", m_ОсновнаФірма_Const.UnigueID.UGuid);
            }
        }
             
    }
    
    static class РегламентніЗавдання
    {
        public static void ReadAll()
        {
            
            Dictionary<string, object> fieldValue = new Dictionary<string, object>();
            bool IsSelect = Config.Kernel.DataBase.SelectAllConstants("tab_constants",
                 new string[] { "col_b3" }, fieldValue);
            
            if (IsSelect)
            {
                m_ФормуванняЗвітів_Const = fieldValue["col_b3"].ToString();
                
            }
        }
        
        
        static string m_ФормуванняЗвітів_Const = "";
        public static string ФормуванняЗвітів_Const
        {
            get
            {
                return m_ФормуванняЗвітів_Const;
            }
            set
            {
                m_ФормуванняЗвітів_Const = value;
                Config.Kernel.DataBase.SaveConstants("tab_constants", "col_b3", m_ФормуванняЗвітів_Const);
            }
        }
        
        public class ФормуванняЗвітів_ЗвітиКористувачів_TablePart : ConstantsTablePart
        {
            public ФормуванняЗвітів_ЗвітиКористувачів_TablePart() : base(Config.Kernel, "tab_a05",
                 new string[] { "col_a2", "col_a3", "col_a4", "col_a5", "col_a1" }) 
            {
                Records = new List<Record>();
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
                    
                    record.Дата = (fieldValue["col_a2"] != DBNull.Value) ? DateTime.Parse(fieldValue["col_a2"].ToString()) : DateTime.MinValue;
                    record.Звіт = fieldValue["col_a3"].ToString();
                    record.БлокДаних = fieldValue["col_a4"].ToString();
                    record.Користувач = new Довідники.Контрагенти_Pointer(fieldValue["col_a5"]);
                    record.Виконано = (fieldValue["col_a1"] != DBNull.Value) ? bool.Parse(fieldValue["col_a1"].ToString()) : false;
                    
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

                    foreach (Record record in Records)
                    {
                        Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                        fieldValue.Add("col_a2", record.Дата);
                        fieldValue.Add("col_a3", record.Звіт);
                        fieldValue.Add("col_a4", record.БлокДаних);
                        fieldValue.Add("col_a5", record.Користувач.UnigueID.UGuid);
                        fieldValue.Add("col_a1", record.Виконано);
                        
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
            
            public class Record : ConstantsTablePartRecord
            {
                public Record()
                {
                    Дата = DateTime.MinValue;
                    Звіт = "";
                    БлокДаних = "";
                    Користувач = new Довідники.Контрагенти_Pointer();
                    Виконано = false;
                    
                }
        
                
                public Record(
                    DateTime?  _Дата = null, string _Звіт = "", string _БлокДаних = "", Довідники.Контрагенти_Pointer _Користувач = null, bool _Виконано = false)
                {
                    Дата = _Дата ?? DateTime.MinValue;
                    Звіт = _Звіт;
                    БлокДаних = _БлокДаних;
                    Користувач = _Користувач ?? new Довідники.Контрагенти_Pointer();
                    Виконано = _Виконано;
                    
                }
                public DateTime Дата { get; set; }
                public string Звіт { get; set; }
                public string БлокДаних { get; set; }
                public Довідники.Контрагенти_Pointer Користувач { get; set; }
                public bool Виконано { get; set; }
                
            }            
        }
          
        public class ФормуванняЗвітів_ІсторіяЗапускуВебСервера_TablePart : ConstantsTablePart
        {
            public ФормуванняЗвітів_ІсторіяЗапускуВебСервера_TablePart() : base(Config.Kernel, "tab_a10",
                 new string[] { "col_a2" }) 
            {
                Records = new List<Record>();
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
                    
                    record.ДатаЗапуску = (fieldValue["col_a2"] != DBNull.Value) ? DateTime.Parse(fieldValue["col_a2"].ToString()) : DateTime.MinValue;
                    
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

                    foreach (Record record in Records)
                    {
                        Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                        fieldValue.Add("col_a2", record.ДатаЗапуску);
                        
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
            
            public class Record : ConstantsTablePartRecord
            {
                public Record()
                {
                    ДатаЗапуску = DateTime.MinValue;
                    
                }
        
                
                public Record(
                    DateTime?  _ДатаЗапуску = null)
                {
                    ДатаЗапуску = _ДатаЗапуску ?? DateTime.MinValue;
                    
                }
                public DateTime ДатаЗапуску { get; set; }
                
            }            
        }
               
    }
    
}

namespace ConfTrade_v1_1.Довідники
{
    
    #region DIRECTORY "Номенклатура"
    
    class Номенклатура_Objest : DirectoryObject
    {
        public Номенклатура_Objest() : base(Config.Kernel, "tab_a14",
             new string[] { "col_a1", "col_a2", "col_a3", "col_a4", "col_a5", "col_a6", "col_a7", "col_a8", "col_a9", "col_b2", "col_b3", "col_b4" }) 
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
            ДатаСтворення = DateTime.MinValue;
            Валюта = new Довідники.Валюти_Pointer();
            Група = new Довідники.Номенклатура_Групи_Pointer();
            
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
                ДатаСтворення = (base.FieldValue["col_b2"] != DBNull.Value) ? DateTime.Parse(base.FieldValue["col_b2"].ToString()) : DateTime.MinValue;
                Валюта = new Довідники.Валюти_Pointer(base.FieldValue["col_b3"]);
                Група = new Довідники.Номенклатура_Групи_Pointer(base.FieldValue["col_b4"]);
                
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
            base.FieldValue["col_b2"] = ДатаСтворення;
            base.FieldValue["col_b3"] = Валюта.UnigueID.UGuid;
            base.FieldValue["col_b4"] = Група.UnigueID.UGuid;
            
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
               "<ДатаСтворення>" + ДатаСтворення.ToString() + "</ДатаСтворення>"  +
               "<Валюта>" + Валюта.ToString() + "</Валюта>"  +
               "<Група>" + Група.ToString() + "</Група>"  +
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
        public DateTime ДатаСтворення { get; set; }
        public Довідники.Валюти_Pointer Валюта { get; set; }
        public Довідники.Номенклатура_Групи_Pointer Група { get; set; }
        
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
            return НоменклатураObjestItem.Read(base.UnigueID) ? НоменклатураObjestItem : null;
        }
    }
    
    
    class Номенклатура_Select : DirectorySelect, IDisposable
    {
        public Номенклатура_Select() : base(Config.Kernel, "tab_a14",
            new string[] { "col_a1", "col_a2", "col_a3", "col_a4", "col_a5", "col_a6", "col_a7", "col_a8", "col_a9", "col_b2", "col_b3", "col_b4" },
            new string[] { "Назва", "Код", "Ціна", "Кво", "Перелічення1", "Дата", "ДатаЧас", "Час", "Логічний", "ДатаСтворення", "Валюта", "Група" }) { }
    
        public bool Select() { return base.BaseSelect(); }
        
        public bool SelectSingle() { if (base.BaseSelectSingle()) { MoveNext(); return true; } else { Current = null; return false; } }
        
        public bool MoveNext() { if (MoveToPosition()) { Current = new Номенклатура_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields); return true; } else { Current = null; return false; } }

        public Номенклатура_Pointer Current { get; private set; }
        
        public Номенклатура_Pointer FindByField(string name, object value)
        {
            Номенклатура_Pointer itemPointer = new Номенклатура_Pointer();
            DirectoryPointer directoryPointer = base.BaseFindByField(name, value);
            if (!directoryPointer.IsEmpty()) itemPointer.Init(directoryPointer.UnigueID);
            return itemPointer;
        }
        
        public List<Номенклатура_Pointer> FindListByField(string name, object value, int limit = 0, int offset = 0)
        {
            List<Номенклатура_Pointer> directoryPointerList = new List<Номенклатура_Pointer>();
            foreach (DirectoryPointer directoryPointer in base.BaseFindListByField(name, value, limit, offset)) 
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
             new string[] { "col_a1", "col_a2", "col_a3", "col_a4", "col_b2", "col_b3" },
             new string[] { "Назва", "Код", "Ціна", "Кво", "ДатаСтворення", "Валюта" },
             new string[] { "string", "string", "numeric", "integer", "datetime", "pointer" },
             "Довідник_Номенклатура_Список")
        {
            
        }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "Валюти"
    
    class Валюти_Objest : DirectoryObject
    {
        public Валюти_Objest() : base(Config.Kernel, "tab_a03",
             new string[] { "col_a1", "col_a2" }) 
        {
            Назва = "";
            Код = "";
            
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

        public string Serialize()
        {
            return 
            "<Валюти>" +
               "<uid>" + base.UnigueID.ToString() + "</uid>" +
               "<Назва>" + "<![CDATA[" + Назва + "]]>" + "</Назва>"  +
               "<Код>" + "<![CDATA[" + Код + "]]>" + "</Код>"  +
               "</Валюти>";
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
        
        public string Назва { get; set; }
        public string Код { get; set; }
        
    }
    
    
    class Валюти_Pointer : DirectoryPointer
    {
        public Валюти_Pointer(object uid = null) : base(Config.Kernel, "tab_a03")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public Валюти_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a03")
        {
            base.Init(uid, fields);
        }
        
        public Валюти_Objest GetDirectoryObject()
        {
            Валюти_Objest ВалютиObjestItem = new Валюти_Objest();
            return ВалютиObjestItem.Read(base.UnigueID) ? ВалютиObjestItem : null;
        }
    }
    
    
    class Валюти_Select : DirectorySelect, IDisposable
    {
        public Валюти_Select() : base(Config.Kernel, "tab_a03",
            new string[] { "col_a1", "col_a2" },
            new string[] { "Назва", "Код" }) { }
    
        public bool Select() { return base.BaseSelect(); }
        
        public bool SelectSingle() { if (base.BaseSelectSingle()) { MoveNext(); return true; } else { Current = null; return false; } }
        
        public bool MoveNext() { if (MoveToPosition()) { Current = new Валюти_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields); return true; } else { Current = null; return false; } }

        public Валюти_Pointer Current { get; private set; }
        
        public Валюти_Pointer FindByField(string name, object value)
        {
            Валюти_Pointer itemPointer = new Валюти_Pointer();
            DirectoryPointer directoryPointer = base.BaseFindByField(name, value);
            if (!directoryPointer.IsEmpty()) itemPointer.Init(directoryPointer.UnigueID);
            return itemPointer;
        }
        
        public List<Валюти_Pointer> FindListByField(string name, object value, int limit = 0, int offset = 0)
        {
            List<Валюти_Pointer> directoryPointerList = new List<Валюти_Pointer>();
            foreach (DirectoryPointer directoryPointer in base.BaseFindListByField(name, value, limit, offset)) 
                directoryPointerList.Add(new Валюти_Pointer(directoryPointer.UnigueID));
            return directoryPointerList;
        }
    }
    
      ///<summary>
    ///Список.
    ///</summary>
    class Валюти_Список_View : DirectoryView
    {
        public Валюти_Список_View() : base(Config.Kernel, "tab_a03", 
             new string[] { "col_a1", "col_a2" },
             new string[] { "Назва", "Код" },
             new string[] { "string", "string" },
             "Довідник_Валюти_Список")
        {
            
        }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "ОдиниціВиміру"
    
    class ОдиниціВиміру_Objest : DirectoryObject
    {
        public ОдиниціВиміру_Objest() : base(Config.Kernel, "tab_a04",
             new string[] { "col_a1", "col_a2" }) 
        {
            Назва = "";
            Код = "";
            
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

        public string Serialize()
        {
            return 
            "<ОдиниціВиміру>" +
               "<uid>" + base.UnigueID.ToString() + "</uid>" +
               "<Назва>" + "<![CDATA[" + Назва + "]]>" + "</Назва>"  +
               "<Код>" + "<![CDATA[" + Код + "]]>" + "</Код>"  +
               "</ОдиниціВиміру>";
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
        
    }
    
    
    class ОдиниціВиміру_Pointer : DirectoryPointer
    {
        public ОдиниціВиміру_Pointer(object uid = null) : base(Config.Kernel, "tab_a04")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public ОдиниціВиміру_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a04")
        {
            base.Init(uid, fields);
        }
        
        public ОдиниціВиміру_Objest GetDirectoryObject()
        {
            ОдиниціВиміру_Objest ОдиниціВиміруObjestItem = new ОдиниціВиміру_Objest();
            return ОдиниціВиміруObjestItem.Read(base.UnigueID) ? ОдиниціВиміруObjestItem : null;
        }
    }
    
    
    class ОдиниціВиміру_Select : DirectorySelect, IDisposable
    {
        public ОдиниціВиміру_Select() : base(Config.Kernel, "tab_a04",
            new string[] { "col_a1", "col_a2" },
            new string[] { "Назва", "Код" }) { }
    
        public bool Select() { return base.BaseSelect(); }
        
        public bool SelectSingle() { if (base.BaseSelectSingle()) { MoveNext(); return true; } else { Current = null; return false; } }
        
        public bool MoveNext() { if (MoveToPosition()) { Current = new ОдиниціВиміру_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields); return true; } else { Current = null; return false; } }

        public ОдиниціВиміру_Pointer Current { get; private set; }
        
        public ОдиниціВиміру_Pointer FindByField(string name, object value)
        {
            ОдиниціВиміру_Pointer itemPointer = new ОдиниціВиміру_Pointer();
            DirectoryPointer directoryPointer = base.BaseFindByField(name, value);
            if (!directoryPointer.IsEmpty()) itemPointer.Init(directoryPointer.UnigueID);
            return itemPointer;
        }
        
        public List<ОдиниціВиміру_Pointer> FindListByField(string name, object value, int limit = 0, int offset = 0)
        {
            List<ОдиниціВиміру_Pointer> directoryPointerList = new List<ОдиниціВиміру_Pointer>();
            foreach (DirectoryPointer directoryPointer in base.BaseFindListByField(name, value, limit, offset)) 
                directoryPointerList.Add(new ОдиниціВиміру_Pointer(directoryPointer.UnigueID));
            return directoryPointerList;
        }
    }
    
      ///<summary>
    ///Список.
    ///</summary>
    class ОдиниціВиміру_Список_View : DirectoryView
    {
        public ОдиниціВиміру_Список_View() : base(Config.Kernel, "tab_a04", 
             new string[] { "col_a1", "col_a2" },
             new string[] { "Назва", "Код" },
             new string[] { "string", "string" },
             "Довідник_ОдиниціВиміру_Список")
        {
            
        }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "Організації"
    
    class Організації_Objest : DirectoryObject
    {
        public Організації_Objest() : base(Config.Kernel, "tab_a06",
             new string[] { "col_a3", "col_a4" }) 
        {
            Назва = "";
            Код = "";
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
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
            base.FieldValue["col_a3"] = Назва;
            base.FieldValue["col_a4"] = Код;
            
            BaseSave();
        }

        public string Serialize()
        {
            return 
            "<Організації>" +
               "<uid>" + base.UnigueID.ToString() + "</uid>" +
               "<Назва>" + "<![CDATA[" + Назва + "]]>" + "</Назва>"  +
               "<Код>" + "<![CDATA[" + Код + "]]>" + "</Код>"  +
               "</Організації>";
        }

        public void Delete()
        {
            base.BaseDelete();
        }
        
        public Організації_Pointer GetDirectoryPointer()
        {
            Організації_Pointer directoryPointer = new Організації_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public string Назва { get; set; }
        public string Код { get; set; }
        
    }
    
    
    class Організації_Pointer : DirectoryPointer
    {
        public Організації_Pointer(object uid = null) : base(Config.Kernel, "tab_a06")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public Організації_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a06")
        {
            base.Init(uid, fields);
        }
        
        public Організації_Objest GetDirectoryObject()
        {
            Організації_Objest ОрганізаціїObjestItem = new Організації_Objest();
            return ОрганізаціїObjestItem.Read(base.UnigueID) ? ОрганізаціїObjestItem : null;
        }
    }
    
    
    class Організації_Select : DirectorySelect, IDisposable
    {
        public Організації_Select() : base(Config.Kernel, "tab_a06",
            new string[] { "col_a3", "col_a4" },
            new string[] { "Назва", "Код" }) { }
    
        public bool Select() { return base.BaseSelect(); }
        
        public bool SelectSingle() { if (base.BaseSelectSingle()) { MoveNext(); return true; } else { Current = null; return false; } }
        
        public bool MoveNext() { if (MoveToPosition()) { Current = new Організації_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields); return true; } else { Current = null; return false; } }

        public Організації_Pointer Current { get; private set; }
        
        public Організації_Pointer FindByField(string name, object value)
        {
            Організації_Pointer itemPointer = new Організації_Pointer();
            DirectoryPointer directoryPointer = base.BaseFindByField(name, value);
            if (!directoryPointer.IsEmpty()) itemPointer.Init(directoryPointer.UnigueID);
            return itemPointer;
        }
        
        public List<Організації_Pointer> FindListByField(string name, object value, int limit = 0, int offset = 0)
        {
            List<Організації_Pointer> directoryPointerList = new List<Організації_Pointer>();
            foreach (DirectoryPointer directoryPointer in base.BaseFindListByField(name, value, limit, offset)) 
                directoryPointerList.Add(new Організації_Pointer(directoryPointer.UnigueID));
            return directoryPointerList;
        }
    }
    
      ///<summary>
    ///Список.
    ///</summary>
    class Організації_Список_View : DirectoryView
    {
        public Організації_Список_View() : base(Config.Kernel, "tab_a06", 
             new string[] { "col_a3", "col_a4" },
             new string[] { "Назва", "Код" },
             new string[] { "string", "string" },
             "Довідник_Організації_Список")
        {
            
        }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "Контрагенти"
    
    class Контрагенти_Objest : DirectoryObject
    {
        public Контрагенти_Objest() : base(Config.Kernel, "tab_a07",
             new string[] { "col_a5", "col_a6", "col_a1", "col_a2", "col_a3" }) 
        {
            Назва = "";
            Код = "";
            Постачальник = false;
            Покупець = false;
            Група = new Довідники.Контрагенти_Групи_Pointer();
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Назва = base.FieldValue["col_a5"].ToString();
                Код = base.FieldValue["col_a6"].ToString();
                Постачальник = (base.FieldValue["col_a1"] != DBNull.Value) ? bool.Parse(base.FieldValue["col_a1"].ToString()) : false;
                Покупець = (base.FieldValue["col_a2"] != DBNull.Value) ? bool.Parse(base.FieldValue["col_a2"].ToString()) : false;
                Група = new Довідники.Контрагенти_Групи_Pointer(base.FieldValue["col_a3"]);
                
                BaseClear();
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["col_a5"] = Назва;
            base.FieldValue["col_a6"] = Код;
            base.FieldValue["col_a1"] = Постачальник;
            base.FieldValue["col_a2"] = Покупець;
            base.FieldValue["col_a3"] = Група.UnigueID.UGuid;
            
            BaseSave();
        }

        public string Serialize()
        {
            return 
            "<Контрагенти>" +
               "<uid>" + base.UnigueID.ToString() + "</uid>" +
               "<Назва>" + "<![CDATA[" + Назва + "]]>" + "</Назва>"  +
               "<Код>" + "<![CDATA[" + Код + "]]>" + "</Код>"  +
               "<Постачальник>" + (Постачальник == true ? "1" : "0") + "</Постачальник>"  +
               "<Покупець>" + (Покупець == true ? "1" : "0") + "</Покупець>"  +
               "<Група>" + Група.ToString() + "</Група>"  +
               "</Контрагенти>";
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
        
        public string Назва { get; set; }
        public string Код { get; set; }
        public bool Постачальник { get; set; }
        public bool Покупець { get; set; }
        public Довідники.Контрагенти_Групи_Pointer Група { get; set; }
        
    }
    
    
    class Контрагенти_Pointer : DirectoryPointer
    {
        public Контрагенти_Pointer(object uid = null) : base(Config.Kernel, "tab_a07")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public Контрагенти_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a07")
        {
            base.Init(uid, fields);
        }
        
        public Контрагенти_Objest GetDirectoryObject()
        {
            Контрагенти_Objest КонтрагентиObjestItem = new Контрагенти_Objest();
            return КонтрагентиObjestItem.Read(base.UnigueID) ? КонтрагентиObjestItem : null;
        }
    }
    
    
    class Контрагенти_Select : DirectorySelect, IDisposable
    {
        public Контрагенти_Select() : base(Config.Kernel, "tab_a07",
            new string[] { "col_a5", "col_a6", "col_a1", "col_a2", "col_a3" },
            new string[] { "Назва", "Код", "Постачальник", "Покупець", "Група" }) { }
    
        public bool Select() { return base.BaseSelect(); }
        
        public bool SelectSingle() { if (base.BaseSelectSingle()) { MoveNext(); return true; } else { Current = null; return false; } }
        
        public bool MoveNext() { if (MoveToPosition()) { Current = new Контрагенти_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields); return true; } else { Current = null; return false; } }

        public Контрагенти_Pointer Current { get; private set; }
        
        public Контрагенти_Pointer FindByField(string name, object value)
        {
            Контрагенти_Pointer itemPointer = new Контрагенти_Pointer();
            DirectoryPointer directoryPointer = base.BaseFindByField(name, value);
            if (!directoryPointer.IsEmpty()) itemPointer.Init(directoryPointer.UnigueID);
            return itemPointer;
        }
        
        public List<Контрагенти_Pointer> FindListByField(string name, object value, int limit = 0, int offset = 0)
        {
            List<Контрагенти_Pointer> directoryPointerList = new List<Контрагенти_Pointer>();
            foreach (DirectoryPointer directoryPointer in base.BaseFindListByField(name, value, limit, offset)) 
                directoryPointerList.Add(new Контрагенти_Pointer(directoryPointer.UnigueID));
            return directoryPointerList;
        }
    }
    
      ///<summary>
    ///Список.
    ///</summary>
    class Контрагенти_Список_View : DirectoryView
    {
        public Контрагенти_Список_View() : base(Config.Kernel, "tab_a07", 
             new string[] { "col_a5", "col_a6", "col_a1", "col_a2", "col_a3" },
             new string[] { "Назва", "Код", "Постачальник", "Покупець", "Група" },
             new string[] { "string", "string", "boolean", "boolean", "pointer" },
             "Довідник_Контрагенти_Список")
        {
            
        }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "Контрагенти_Групи"
    
    class Контрагенти_Групи_Objest : DirectoryObject
    {
        public Контрагенти_Групи_Objest() : base(Config.Kernel, "tab_a08",
             new string[] { "col_a1", "col_a2", "col_a3" }) 
        {
            Назва = "";
            Код = "";
            Група = new Довідники.Контрагенти_Групи_Pointer();
            
            //Табличні частини
            Ієрархія_TablePart = new Контрагенти_Групи_Ієрархія_TablePart(this);
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Назва = base.FieldValue["col_a1"].ToString();
                Код = base.FieldValue["col_a2"].ToString();
                Група = new Довідники.Контрагенти_Групи_Pointer(base.FieldValue["col_a3"]);
                
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
            base.FieldValue["col_a3"] = Група.UnigueID.UGuid;
            
            BaseSave();
        }

        public string Serialize()
        {
            return 
            "<Контрагенти_Групи>" +
               "<uid>" + base.UnigueID.ToString() + "</uid>" +
               "<Назва>" + "<![CDATA[" + Назва + "]]>" + "</Назва>"  +
               "<Код>" + "<![CDATA[" + Код + "]]>" + "</Код>"  +
               "<Група>" + Група.ToString() + "</Група>"  +
               "<Група_Назва>" + Група.GetView() + "</Група_Назва>" +
               "</Контрагенти_Групи>";
        }

        public void Delete()
        {
            base.BaseDelete();
        }
        
        public Контрагенти_Групи_Pointer GetDirectoryPointer()
        {
            Контрагенти_Групи_Pointer directoryPointer = new Контрагенти_Групи_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public string Назва { get; set; }
        public string Код { get; set; }
        public Довідники.Контрагенти_Групи_Pointer Група { get; set; }
        
        //Табличні частини
        public Контрагенти_Групи_Ієрархія_TablePart Ієрархія_TablePart { get; set; }
        
    }
    
    
    class Контрагенти_Групи_Pointer : DirectoryPointer
    {
        public Контрагенти_Групи_Pointer(object uid = null) : base(Config.Kernel, "tab_a08")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public Контрагенти_Групи_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a08")
        {
            base.Init(uid, fields);
        }
        
        public Контрагенти_Групи_Objest GetDirectoryObject()
        {
            Контрагенти_Групи_Objest Контрагенти_ГрупиObjestItem = new Контрагенти_Групи_Objest();
            return Контрагенти_ГрупиObjestItem.Read(base.UnigueID) ? Контрагенти_ГрупиObjestItem : null;
        }

        public string GetView()
        {
            return base.GetView(base.UnigueID.UGuid, "col_a1");
        }
    }
    
    
    class Контрагенти_Групи_Select : DirectorySelect, IDisposable
    {
        public Контрагенти_Групи_Select() : base(Config.Kernel, "tab_a08",
            new string[] { "col_a1", "col_a2", "col_a3" },
            new string[] { "Назва", "Код", "Група" }) { }
    
        public bool Select() { return base.BaseSelect(); }
        
        public bool SelectSingle() { if (base.BaseSelectSingle()) { MoveNext(); return true; } else { Current = null; return false; } }
        
        public bool MoveNext() { if (MoveToPosition()) { Current = new Контрагенти_Групи_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields); return true; } else { Current = null; return false; } }

        public Контрагенти_Групи_Pointer Current { get; private set; }
        
        public Контрагенти_Групи_Pointer FindByField(string name, object value)
        {
            Контрагенти_Групи_Pointer itemPointer = new Контрагенти_Групи_Pointer();
            DirectoryPointer directoryPointer = base.BaseFindByField(name, value);
            if (!directoryPointer.IsEmpty()) itemPointer.Init(directoryPointer.UnigueID);
            return itemPointer;
        }
        
        public List<Контрагенти_Групи_Pointer> FindListByField(string name, object value, int limit = 0, int offset = 0)
        {
            List<Контрагенти_Групи_Pointer> directoryPointerList = new List<Контрагенти_Групи_Pointer>();
            foreach (DirectoryPointer directoryPointer in base.BaseFindListByField(name, value, limit, offset)) 
                directoryPointerList.Add(new Контрагенти_Групи_Pointer(directoryPointer.UnigueID));
            return directoryPointerList;
        }
    }
    
      ///<summary>
    ///Таблична частина для збереження ієрархії вкладеності групи..
    ///</summary>
    class Контрагенти_Групи_Ієрархія_TablePart : DirectoryTablePart
    {
        public Контрагенти_Групи_Ієрархія_TablePart(Контрагенти_Групи_Objest owner) : base(Config.Kernel, "tab_a01",
             new string[] { "col_a1", "col_a2" }) 
        {
            if (owner == null) throw new Exception("owner null");
            
            Owner = owner;
            Records = new List<Record>();
        }
        
        public Контрагенти_Групи_Objest Owner { get; private set; }
        
        public List<Record> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Record record = new Record();
                record.UID = (Guid)fieldValue["uid"];
                
                record.Рівень = (fieldValue["col_a1"] != DBNull.Value) ? (int)fieldValue["col_a1"] : 0;
                record.Група = new Довідники.Контрагенти_Групи_Pointer(fieldValue["col_a2"]);
                
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

                    fieldValue.Add("col_a1", record.Рівень);
                    fieldValue.Add("col_a2", record.Група.UnigueID.UGuid);
                    
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
        
        ///<summary>
    ///Таблична частина для збереження ієрархії вкладеності групи..
    ///</summary>
        public class Record : DirectoryTablePartRecord
        {
            public Record()
            {
                Рівень = 0;
                Група = new Довідники.Контрагенти_Групи_Pointer();
                
            }
        
            
            public Record(
                int _Рівень = 0, Довідники.Контрагенти_Групи_Pointer _Група = null)
            {
                Рівень = _Рівень;
                Група = _Група ?? new Довідники.Контрагенти_Групи_Pointer();
                
            }
            public int Рівень { get; set; }
            public Довідники.Контрагенти_Групи_Pointer Група { get; set; }
            
        }
    }
      ///<summary>
    ///Список.
    ///</summary>
    class Контрагенти_Групи_Список_View : DirectoryView
    {
        public Контрагенти_Групи_Список_View() : base(Config.Kernel, "tab_a08", 
             new string[] { "col_a1", "col_a2", "col_a3" },
             new string[] { "Назва", "Код", "Група" },
             new string[] { "string", "string", "pointer" },
             "Довідник_Контрагенти_Групи_Список")
        {
            
        }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "Номенклатура_Групи"
    
    class Номенклатура_Групи_Objest : DirectoryObject
    {
        public Номенклатура_Групи_Objest() : base(Config.Kernel, "tab_a09",
             new string[] { "col_a1", "col_a2", "col_a3" }) 
        {
            Назва = "";
            Код = "";
            Група = new Довідники.Номенклатура_Групи_Pointer();
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Назва = base.FieldValue["col_a1"].ToString();
                Код = base.FieldValue["col_a2"].ToString();
                Група = new Довідники.Номенклатура_Групи_Pointer(base.FieldValue["col_a3"]);
                
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
            base.FieldValue["col_a3"] = Група.UnigueID.UGuid;
            
            BaseSave();
        }

        public string Serialize()
        {
            return 
            "<Номенклатура_Групи>" +
               "<uid>" + base.UnigueID.ToString() + "</uid>" +
               "<Назва>" + "<![CDATA[" + Назва + "]]>" + "</Назва>"  +
               "<Код>" + "<![CDATA[" + Код + "]]>" + "</Код>"  +
               "<Група>" + Група.ToString() + "</Група>"  +
               "</Номенклатура_Групи>";
        }

        public void Delete()
        {
            base.BaseDelete();
        }
        
        public Номенклатура_Групи_Pointer GetDirectoryPointer()
        {
            Номенклатура_Групи_Pointer directoryPointer = new Номенклатура_Групи_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public string Назва { get; set; }
        public string Код { get; set; }
        public Довідники.Номенклатура_Групи_Pointer Група { get; set; }
        
    }
    
    
    class Номенклатура_Групи_Pointer : DirectoryPointer
    {
        public Номенклатура_Групи_Pointer(object uid = null) : base(Config.Kernel, "tab_a09")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public Номенклатура_Групи_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a09")
        {
            base.Init(uid, fields);
        }
        
        public Номенклатура_Групи_Objest GetDirectoryObject()
        {
            Номенклатура_Групи_Objest Номенклатура_ГрупиObjestItem = new Номенклатура_Групи_Objest();
            return Номенклатура_ГрупиObjestItem.Read(base.UnigueID) ? Номенклатура_ГрупиObjestItem : null;
        }
    }
    
    
    class Номенклатура_Групи_Select : DirectorySelect, IDisposable
    {
        public Номенклатура_Групи_Select() : base(Config.Kernel, "tab_a09",
            new string[] { "col_a1", "col_a2", "col_a3" },
            new string[] { "Назва", "Код", "Група" }) { }
    
        public bool Select() { return base.BaseSelect(); }
        
        public bool SelectSingle() { if (base.BaseSelectSingle()) { MoveNext(); return true; } else { Current = null; return false; } }
        
        public bool MoveNext() { if (MoveToPosition()) { Current = new Номенклатура_Групи_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields); return true; } else { Current = null; return false; } }

        public Номенклатура_Групи_Pointer Current { get; private set; }
        
        public Номенклатура_Групи_Pointer FindByField(string name, object value)
        {
            Номенклатура_Групи_Pointer itemPointer = new Номенклатура_Групи_Pointer();
            DirectoryPointer directoryPointer = base.BaseFindByField(name, value);
            if (!directoryPointer.IsEmpty()) itemPointer.Init(directoryPointer.UnigueID);
            return itemPointer;
        }
        
        public List<Номенклатура_Групи_Pointer> FindListByField(string name, object value, int limit = 0, int offset = 0)
        {
            List<Номенклатура_Групи_Pointer> directoryPointerList = new List<Номенклатура_Групи_Pointer>();
            foreach (DirectoryPointer directoryPointer in base.BaseFindListByField(name, value, limit, offset)) 
                directoryPointerList.Add(new Номенклатура_Групи_Pointer(directoryPointer.UnigueID));
            return directoryPointerList;
        }
    }
    
      ///<summary>
    ///Список.
    ///</summary>
    class Номенклатура_Групи_Список_View : DirectoryView
    {
        public Номенклатура_Групи_Список_View() : base(Config.Kernel, "tab_a08", 
             new string[] { "col_a1", "col_a2", "col_a3" },
             new string[] { "Назва", "Код", "Група" },
             new string[] { "string", "string", "pointer" },
             "Довідник_Номенклатура_Групи_Список")
        {
            
        }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "ІсторіяЗапускуВебСервера"
    
    class ІсторіяЗапускуВебСервера_Objest : DirectoryObject
    {
        public ІсторіяЗапускуВебСервера_Objest() : base(Config.Kernel, "tab_a11",
             new string[] { "col_a3" }) 
        {
            ДатаЗапуску = DateTime.MinValue;
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                ДатаЗапуску = (base.FieldValue["col_a3"] != DBNull.Value) ? DateTime.Parse(base.FieldValue["col_a3"].ToString()) : DateTime.MinValue;
                
                BaseClear();
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["col_a3"] = ДатаЗапуску;
            
            BaseSave();
        }

        public string Serialize()
        {
            return 
            "<ІсторіяЗапускуВебСервера>" +
               "<uid>" + base.UnigueID.ToString() + "</uid>" +
               "<ДатаЗапуску>" + ДатаЗапуску.ToString() + "</ДатаЗапуску>"  +
               "</ІсторіяЗапускуВебСервера>";
        }

        public void Delete()
        {
            base.BaseDelete();
        }
        
        public ІсторіяЗапускуВебСервера_Pointer GetDirectoryPointer()
        {
            ІсторіяЗапускуВебСервера_Pointer directoryPointer = new ІсторіяЗапускуВебСервера_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public DateTime ДатаЗапуску { get; set; }
        
    }
    
    
    class ІсторіяЗапускуВебСервера_Pointer : DirectoryPointer
    {
        public ІсторіяЗапускуВебСервера_Pointer(object uid = null) : base(Config.Kernel, "tab_a11")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public ІсторіяЗапускуВебСервера_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a11")
        {
            base.Init(uid, fields);
        }
        
        public ІсторіяЗапускуВебСервера_Objest GetDirectoryObject()
        {
            ІсторіяЗапускуВебСервера_Objest ІсторіяЗапускуВебСервераObjestItem = new ІсторіяЗапускуВебСервера_Objest();
            return ІсторіяЗапускуВебСервераObjestItem.Read(base.UnigueID) ? ІсторіяЗапускуВебСервераObjestItem : null;
        }
    }
    
    
    class ІсторіяЗапускуВебСервера_Select : DirectorySelect, IDisposable
    {
        public ІсторіяЗапускуВебСервера_Select() : base(Config.Kernel, "tab_a11",
            new string[] { "col_a3" },
            new string[] { "ДатаЗапуску" }) { }
    
        public bool Select() { return base.BaseSelect(); }
        
        public bool SelectSingle() { if (base.BaseSelectSingle()) { MoveNext(); return true; } else { Current = null; return false; } }
        
        public bool MoveNext() { if (MoveToPosition()) { Current = new ІсторіяЗапускуВебСервера_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields); return true; } else { Current = null; return false; } }

        public ІсторіяЗапускуВебСервера_Pointer Current { get; private set; }
        
        public ІсторіяЗапускуВебСервера_Pointer FindByField(string name, object value)
        {
            ІсторіяЗапускуВебСервера_Pointer itemPointer = new ІсторіяЗапускуВебСервера_Pointer();
            DirectoryPointer directoryPointer = base.BaseFindByField(name, value);
            if (!directoryPointer.IsEmpty()) itemPointer.Init(directoryPointer.UnigueID);
            return itemPointer;
        }
        
        public List<ІсторіяЗапускуВебСервера_Pointer> FindListByField(string name, object value, int limit = 0, int offset = 0)
        {
            List<ІсторіяЗапускуВебСервера_Pointer> directoryPointerList = new List<ІсторіяЗапускуВебСервера_Pointer>();
            foreach (DirectoryPointer directoryPointer in base.BaseFindListByField(name, value, limit, offset)) 
                directoryPointerList.Add(new ІсторіяЗапускуВебСервера_Pointer(directoryPointer.UnigueID));
            return directoryPointerList;
        }
    }
    
      ///<summary>
    ///Список.
    ///</summary>
    class ІсторіяЗапускуВебСервера_Список_View : DirectoryView
    {
        public ІсторіяЗапускуВебСервера_Список_View() : base(Config.Kernel, "tab_a11", 
             new string[] {  },
             new string[] {  },
             new string[] {  },
             "Довідник_ІсторіяЗапускуВебСервера_Список")
        {
            
        }
        
    }
      
    
    #endregion
    
}

namespace ConfTrade_v1_1.Перелічення
{
    
    public enum Test
    {
         er = 1
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
             new string[] { "col_a1", "col_a2", "col_a3" }) 
        {
            Records = new List<Record>();
            Filter = new SelectFilter();
        }
        
        public List<Record> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            
            
            if (Filter.a != null)
            {
                base.BaseFilter.Add(new Where("col_a1", Comparison.EQ, Filter.a, false));
                
            }
            

            base.BaseRead();
            
            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Record record = new Record();
                
                record.UID = (Guid)fieldValue["uid"];
                  
                record.a = fieldValue["col_a1"].ToString();
                record.a1 = fieldValue["col_a2"].ToString();
                record.a3 = fieldValue["col_a3"].ToString();
                
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

                    fieldValue.Add("col_a1", record.a);
                    fieldValue.Add("col_a2", record.a1);
                    fieldValue.Add("col_a3", record.a3);
                    
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
                a = "";
                a1 = "";
                a3 = "";
                
            }
        
            public string a { get; set; }
            public string a1 { get; set; }
            public string a3 { get; set; }
            
        }
    
        public class SelectFilter
        {
            public SelectFilter()
            {
                 a = null;
                 
            }
        
            public string a { get; set; }
            
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
  
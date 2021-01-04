
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
  
 * Дата конфігурації: 24.11.2020 19:12:56
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
                m_a10_Const = fieldValue["col_b2"].ToString();
                
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
        
        static string m_a10_Const = "";
        public static string a10_Const
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
          
        public class a2_Історія_TablePart : ConstantsTablePart
        {
            public a2_Історія_TablePart() : base(Config.Kernel, "tab_a30",
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
                    record.Значення = (fieldValue["col_a2"] != DBNull.Value) ? (int)fieldValue["col_a2"] : 0;
                    
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
                    Значення = 0;
                    
                }
        
                
                public Record(
                    DateTime?  _Дата = null, int _Значення = 0)
                {
                    Дата = _Дата ?? DateTime.MinValue;
                    Значення = _Значення;
                    
                }
                public DateTime Дата { get; set; }
                public int Значення { get; set; }
                
            }            
        }
          
        public class a3_Історія_TablePart : ConstantsTablePart
        {
            public a3_Історія_TablePart() : base(Config.Kernel, "tab_a31",
                 new string[] { "col_a3", "col_a4" }) 
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
                    
                    record.Дата = (fieldValue["col_a3"] != DBNull.Value) ? DateTime.Parse(fieldValue["col_a3"].ToString()) : DateTime.MinValue;
                    record.Значення = (fieldValue["col_a4"] != DBNull.Value) ? (decimal)fieldValue["col_a4"] : 0;
                    
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

                        fieldValue.Add("col_a3", record.Дата);
                        fieldValue.Add("col_a4", record.Значення);
                        
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
                    Значення = 0;
                    
                }
        
                
                public Record(
                    DateTime?  _Дата = null, decimal _Значення = 0)
                {
                    Дата = _Дата ?? DateTime.MinValue;
                    Значення = _Значення;
                    
                }
                public DateTime Дата { get; set; }
                public decimal Значення { get; set; }
                
            }            
        }
          
        public class a7_Історія_TablePart : ConstantsTablePart
        {
            public a7_Історія_TablePart() : base(Config.Kernel, "tab_a34",
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
            Перелічення = 0;
            Дата = DateTime.MinValue;
            ДатаЧас = DateTime.MinValue;
            Час = DateTime.MinValue.TimeOfDay;
            Логічний = false;
            ДатаСтворення = DateTime.MinValue;
            Валюта = new Довідники.Валюти_Pointer();
            Група = new Довідники.Номенклатура_Групи_Pointer();
            
            //Табличні частини
            Ціни_TablePart = new Номенклатура_Ціни_TablePart(this);
            Валюти_TablePart = new Номенклатура_Валюти_TablePart(this);
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Назва = base.FieldValue["col_a1"].ToString();
                Код = base.FieldValue["col_a2"].ToString();
                Ціна = (base.FieldValue["col_a3"] != DBNull.Value) ? (decimal)base.FieldValue["col_a3"] : 0;
                Кво = (base.FieldValue["col_a4"] != DBNull.Value) ? (int)base.FieldValue["col_a4"] : 0;
                Перелічення = (base.FieldValue["col_a5"] != DBNull.Value) ? (Перелічення.Test)base.FieldValue["col_a5"] : 0;
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
            base.FieldValue["col_a5"] = (int)Перелічення;
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
               "<Перелічення>" + ((int)Перелічення).ToString() + "</Перелічення>"  +
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
        public Перелічення.Test Перелічення { get; set; }
        public DateTime Дата { get; set; }
        public DateTime ДатаЧас { get; set; }
        public TimeSpan Час { get; set; }
        public bool Логічний { get; set; }
        public DateTime ДатаСтворення { get; set; }
        public Довідники.Валюти_Pointer Валюта { get; set; }
        public Довідники.Номенклатура_Групи_Pointer Група { get; set; }
        
        //Табличні частини
        public Номенклатура_Ціни_TablePart Ціни_TablePart { get; set; }
        public Номенклатура_Валюти_TablePart Валюти_TablePart { get; set; }
        
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
            new string[] { "Назва", "Код", "Ціна", "Кво", "Перелічення", "Дата", "ДатаЧас", "Час", "Логічний", "ДатаСтворення", "Валюта", "Група" }) { }
    
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
    
      
    class Номенклатура_Ціни_TablePart : DirectoryTablePart
    {
        public Номенклатура_Ціни_TablePart(Номенклатура_Objest owner) : base(Config.Kernel, "tab_a23",
             new string[] { "col_a1", "col_a2" }) 
        {
            if (owner == null) throw new Exception("owner null");
            
            Owner = owner;
            Records = new List<Record>();
        }
        
        public Номенклатура_Objest Owner { get; private set; }
        
        public List<Record> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Record record = new Record();
                record.UID = (Guid)fieldValue["uid"];
                
                record.Дата = (fieldValue["col_a1"] != DBNull.Value) ? DateTime.Parse(fieldValue["col_a1"].ToString()) : DateTime.MinValue;
                record.Ціна = (fieldValue["col_a2"] != DBNull.Value) ? (decimal)fieldValue["col_a2"] : 0;
                
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

                    fieldValue.Add("col_a1", record.Дата);
                    fieldValue.Add("col_a2", record.Ціна);
                    
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
                Дата = DateTime.MinValue;
                Ціна = 0;
                
            }
        
            
            public Record(
                DateTime?  _Дата = null, decimal _Ціна = 0)
            {
                Дата = _Дата ?? DateTime.MinValue;
                Ціна = _Ціна;
                
            }
            public DateTime Дата { get; set; }
            public decimal Ціна { get; set; }
            
        }
    }
      
    class Номенклатура_Валюти_TablePart : DirectoryTablePart
    {
        public Номенклатура_Валюти_TablePart(Номенклатура_Objest owner) : base(Config.Kernel, "tab_a20",
             new string[] { "col_a1", "col_a2" }) 
        {
            if (owner == null) throw new Exception("owner null");
            
            Owner = owner;
            Records = new List<Record>();
        }
        
        public Номенклатура_Objest Owner { get; private set; }
        
        public List<Record> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Record record = new Record();
                record.UID = (Guid)fieldValue["uid"];
                
                record.Дата = (fieldValue["col_a1"] != DBNull.Value) ? DateTime.Parse(fieldValue["col_a1"].ToString()) : DateTime.MinValue;
                record.Валюта = new Довідники.Валюти_Pointer(fieldValue["col_a2"]);
                
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

                    fieldValue.Add("col_a1", record.Дата);
                    fieldValue.Add("col_a2", record.Валюта.UnigueID.UGuid);
                    
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
                Дата = DateTime.MinValue;
                Валюта = new Довідники.Валюти_Pointer();
                
            }
        
            
            public Record(
                DateTime?  _Дата = null, Довідники.Валюти_Pointer _Валюта = null)
            {
                Дата = _Дата ?? DateTime.MinValue;
                Валюта = _Валюта ?? new Довідники.Валюти_Pointer();
                
            }
            public DateTime Дата { get; set; }
            public Довідники.Валюти_Pointer Валюта { get; set; }
            
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
             new string[] { "col_a1", "col_a2", "col_a3" }) 
        {
            Назва = "";
            Код = "";
            fgdfgdfg = "";
            
            //Табличні частини
            ппп_TablePart = new Валюти_ппп_TablePart(this);
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Назва = base.FieldValue["col_a1"].ToString();
                Код = base.FieldValue["col_a2"].ToString();
                fgdfgdfg = base.FieldValue["col_a3"].ToString();
                
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
            base.FieldValue["col_a3"] = fgdfgdfg;
            
            BaseSave();
        }

        public string Serialize()
        {
            return 
            "<Валюти>" +
               "<uid>" + base.UnigueID.ToString() + "</uid>" +
               "<Назва>" + "<![CDATA[" + Назва + "]]>" + "</Назва>"  +
               "<Код>" + "<![CDATA[" + Код + "]]>" + "</Код>"  +
               "<fgdfgdfg>" + "<![CDATA[" + fgdfgdfg + "]]>" + "</fgdfgdfg>"  +
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
        public string fgdfgdfg { get; set; }
        
        //Табличні частини
        public Валюти_ппп_TablePart ппп_TablePart { get; set; }
        
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
            new string[] { "col_a1", "col_a2", "col_a3" },
            new string[] { "Назва", "Код", "fgdfgdfg" }) { }
    
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
    
      
    class Валюти_ппп_TablePart : DirectoryTablePart
    {
        public Валюти_ппп_TablePart(Валюти_Objest owner) : base(Config.Kernel, "tab_a12",
             new string[] { "col_a1" }) 
        {
            if (owner == null) throw new Exception("owner null");
            
            Owner = owner;
            Records = new List<Record>();
        }
        
        public Валюти_Objest Owner { get; private set; }
        
        public List<Record> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Record record = new Record();
                record.UID = (Guid)fieldValue["uid"];
                
                record.нгшенгшен = fieldValue["col_a1"].ToString();
                
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

                    fieldValue.Add("col_a1", record.нгшенгшен);
                    
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
                нгшенгшен = "";
                
            }
        
            
            public Record(
                string _нгшенгшен = "")
            {
                нгшенгшен = _нгшенгшен;
                
            }
            public string нгшенгшен { get; set; }
            
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
            
            //Табличні частини
            ghjgfhj_TablePart = new Організації_ghjgfhj_TablePart(this);
            
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
        
        //Табличні частини
        public Організації_ghjgfhj_TablePart ghjgfhj_TablePart { get; set; }
        
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
    
      
    class Організації_ghjgfhj_TablePart : DirectoryTablePart
    {
        public Організації_ghjgfhj_TablePart(Організації_Objest owner) : base(Config.Kernel, "tab_a19",
             new string[] { "col_a1" }) 
        {
            if (owner == null) throw new Exception("owner null");
            
            Owner = owner;
            Records = new List<Record>();
        }
        
        public Організації_Objest Owner { get; private set; }
        
        public List<Record> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Record record = new Record();
                record.UID = (Guid)fieldValue["uid"];
                
                record.ghjgfhj = fieldValue["col_a1"].ToString();
                
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

                    fieldValue.Add("col_a1", record.ghjgfhj);
                    
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
                ghjgfhj = "";
                
            }
        
            
            public Record(
                string _ghjgfhj = "")
            {
                ghjgfhj = _ghjgfhj;
                
            }
            public string ghjgfhj { get; set; }
            
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
             new string[] { "col_a1", "col_a2", "col_a3", "col_a4", "col_a5" }) 
        {
            Назва = "";
            Код = "";
            Група = new Довідники.Номенклатура_Групи_Pointer();
            retert = "";
            rter = "";
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Назва = base.FieldValue["col_a1"].ToString();
                Код = base.FieldValue["col_a2"].ToString();
                Група = new Довідники.Номенклатура_Групи_Pointer(base.FieldValue["col_a3"]);
                retert = base.FieldValue["col_a4"].ToString();
                rter = base.FieldValue["col_a5"].ToString();
                
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
            base.FieldValue["col_a4"] = retert;
            base.FieldValue["col_a5"] = rter;
            
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
               "<retert>" + "<![CDATA[" + retert + "]]>" + "</retert>"  +
               "<rter>" + "<![CDATA[" + rter + "]]>" + "</rter>"  +
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
        public string retert { get; set; }
        public string rter { get; set; }
        
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
            new string[] { "col_a1", "col_a2", "col_a3", "col_a4", "col_a5" },
            new string[] { "Назва", "Код", "Група", "retert", "rter" }) { }
    
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
    
    #region DIRECTORY "Довідник"
    
    class Довідник_Objest : DirectoryObject
    {
        public Довідник_Objest() : base(Config.Kernel, "tab_a13",
             new string[] { "col_a1", "col_a2" }) 
        {
            Назва = "";
            Код = "";
            
            //Табличні частини
            рен_TablePart = new Довідник_рен_TablePart(this);
            
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
            "<Довідник>" +
               "<uid>" + base.UnigueID.ToString() + "</uid>" +
               "<Назва>" + "<![CDATA[" + Назва + "]]>" + "</Назва>"  +
               "<Код>" + "<![CDATA[" + Код + "]]>" + "</Код>"  +
               "</Довідник>";
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
        
        //Табличні частини
        public Довідник_рен_TablePart рен_TablePart { get; set; }
        
    }
    
    
    class Довідник_Pointer : DirectoryPointer
    {
        public Довідник_Pointer(object uid = null) : base(Config.Kernel, "tab_a13")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public Довідник_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a13")
        {
            base.Init(uid, fields);
        }
        
        public Довідник_Objest GetDirectoryObject()
        {
            Довідник_Objest ДовідникObjestItem = new Довідник_Objest();
            return ДовідникObjestItem.Read(base.UnigueID) ? ДовідникObjestItem : null;
        }
    }
    
    
    class Довідник_Select : DirectorySelect, IDisposable
    {
        public Довідник_Select() : base(Config.Kernel, "tab_a13",
            new string[] { "col_a1", "col_a2" },
            new string[] { "Назва", "Код" }) { }
    
        public bool Select() { return base.BaseSelect(); }
        
        public bool SelectSingle() { if (base.BaseSelectSingle()) { MoveNext(); return true; } else { Current = null; return false; } }
        
        public bool MoveNext() { if (MoveToPosition()) { Current = new Довідник_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields); return true; } else { Current = null; return false; } }

        public Довідник_Pointer Current { get; private set; }
        
        public Довідник_Pointer FindByField(string name, object value)
        {
            Довідник_Pointer itemPointer = new Довідник_Pointer();
            DirectoryPointer directoryPointer = base.BaseFindByField(name, value);
            if (!directoryPointer.IsEmpty()) itemPointer.Init(directoryPointer.UnigueID);
            return itemPointer;
        }
        
        public List<Довідник_Pointer> FindListByField(string name, object value, int limit = 0, int offset = 0)
        {
            List<Довідник_Pointer> directoryPointerList = new List<Довідник_Pointer>();
            foreach (DirectoryPointer directoryPointer in base.BaseFindListByField(name, value, limit, offset)) 
                directoryPointerList.Add(new Довідник_Pointer(directoryPointer.UnigueID));
            return directoryPointerList;
        }
    }
    
      
    class Довідник_рен_TablePart : DirectoryTablePart
    {
        public Довідник_рен_TablePart(Довідник_Objest owner) : base(Config.Kernel, "tab_a15",
             new string[] { "col_a1", "col_a2", "col_a3" }) 
        {
            if (owner == null) throw new Exception("owner null");
            
            Owner = owner;
            Records = new List<Record>();
        }
        
        public Довідник_Objest Owner { get; private set; }
        
        public List<Record> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Record record = new Record();
                record.UID = (Guid)fieldValue["uid"];
                
                record.цкцу = fieldValue["col_a1"].ToString();
                record.ййц = fieldValue["col_a2"].ToString();
                record.кукеук = fieldValue["col_a3"].ToString();
                
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

                    fieldValue.Add("col_a1", record.цкцу);
                    fieldValue.Add("col_a2", record.ййц);
                    fieldValue.Add("col_a3", record.кукеук);
                    
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
                цкцу = "";
                ййц = "";
                кукеук = "";
                
            }
        
            
            public Record(
                string _цкцу = "", string _ййц = "", string _кукеук = "")
            {
                цкцу = _цкцу;
                ййц = _ййц;
                кукеук = _кукеук;
                
            }
            public string цкцу { get; set; }
            public string ййц { get; set; }
            public string кукеук { get; set; }
            
        }
    }
      ///<summary>
    ///Список.
    ///</summary>
    class Довідник_Список_View : DirectoryView
    {
        public Довідник_Список_View() : base(Config.Kernel, "tab_a13", 
             new string[] { "col_a1", "col_a2" },
             new string[] { "Назва", "Код" },
             new string[] { "string", "string" },
             "Довідник_Довідник_Список")
        {
            
        }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "Товари"
    
    class Товари_Objest : DirectoryObject
    {
        public Товари_Objest() : base(Config.Kernel, "tab_a16",
             new string[] { "col_a1", "col_a2" }) 
        {
            Назва = "";
            Код = "";
            
            //Табличні частини
            sssdsds_TablePart = new Товари_sssdsds_TablePart(this);
            
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
            "<Товари>" +
               "<uid>" + base.UnigueID.ToString() + "</uid>" +
               "<Назва>" + "<![CDATA[" + Назва + "]]>" + "</Назва>"  +
               "<Код>" + "<![CDATA[" + Код + "]]>" + "</Код>"  +
               "</Товари>";
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
        
        //Табличні частини
        public Товари_sssdsds_TablePart sssdsds_TablePart { get; set; }
        
    }
    
    
    class Товари_Pointer : DirectoryPointer
    {
        public Товари_Pointer(object uid = null) : base(Config.Kernel, "tab_a16")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public Товари_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a16")
        {
            base.Init(uid, fields);
        }
        
        public Товари_Objest GetDirectoryObject()
        {
            Товари_Objest ТовариObjestItem = new Товари_Objest();
            return ТовариObjestItem.Read(base.UnigueID) ? ТовариObjestItem : null;
        }
    }
    
    
    class Товари_Select : DirectorySelect, IDisposable
    {
        public Товари_Select() : base(Config.Kernel, "tab_a16",
            new string[] { "col_a1", "col_a2" },
            new string[] { "Назва", "Код" }) { }
    
        public bool Select() { return base.BaseSelect(); }
        
        public bool SelectSingle() { if (base.BaseSelectSingle()) { MoveNext(); return true; } else { Current = null; return false; } }
        
        public bool MoveNext() { if (MoveToPosition()) { Current = new Товари_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields); return true; } else { Current = null; return false; } }

        public Товари_Pointer Current { get; private set; }
        
        public Товари_Pointer FindByField(string name, object value)
        {
            Товари_Pointer itemPointer = new Товари_Pointer();
            DirectoryPointer directoryPointer = base.BaseFindByField(name, value);
            if (!directoryPointer.IsEmpty()) itemPointer.Init(directoryPointer.UnigueID);
            return itemPointer;
        }
        
        public List<Товари_Pointer> FindListByField(string name, object value, int limit = 0, int offset = 0)
        {
            List<Товари_Pointer> directoryPointerList = new List<Товари_Pointer>();
            foreach (DirectoryPointer directoryPointer in base.BaseFindListByField(name, value, limit, offset)) 
                directoryPointerList.Add(new Товари_Pointer(directoryPointer.UnigueID));
            return directoryPointerList;
        }
    }
    
      
    class Товари_sssdsds_TablePart : DirectoryTablePart
    {
        public Товари_sssdsds_TablePart(Товари_Objest owner) : base(Config.Kernel, "tab_a35",
             new string[] { "col_a1", "col_a2" }) 
        {
            if (owner == null) throw new Exception("owner null");
            
            Owner = owner;
            Records = new List<Record>();
        }
        
        public Товари_Objest Owner { get; private set; }
        
        public List<Record> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Record record = new Record();
                record.UID = (Guid)fieldValue["uid"];
                
                record.sdsds = fieldValue["col_a1"].ToString();
                record.sdsdsd = fieldValue["col_a2"].ToString();
                
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

                    fieldValue.Add("col_a1", record.sdsds);
                    fieldValue.Add("col_a2", record.sdsdsd);
                    
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
                sdsds = "";
                sdsdsd = "";
                
            }
        
            
            public Record(
                string _sdsds = "", string _sdsdsd = "")
            {
                sdsds = _sdsds;
                sdsdsd = _sdsdsd;
                
            }
            public string sdsds { get; set; }
            public string sdsdsd { get; set; }
            
        }
    }
      ///<summary>
    ///Список.
    ///</summary>
    class Товари_Список_View : DirectoryView
    {
        public Товари_Список_View() : base(Config.Kernel, "tab_a16", 
             new string[] { "col_a1", "col_a2" },
             new string[] { "Назва", "Код" },
             new string[] { "string", "string" },
             "Довідник_Товари_Список")
        {
            
        }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "Новини"
    
    class Новини_Objest : DirectoryObject
    {
        public Новини_Objest() : base(Config.Kernel, "tab_a17",
             new string[] { "col_a1", "col_a2", "col_a3" }) 
        {
            Назва = "";
            Код = "";
            вв = "";
            
            //Табличні частини
            tyurty_TablePart = new Новини_tyurty_TablePart(this);
            tyurty_Копія_1_TablePart = new Новини_tyurty_Копія_1_TablePart(this);
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Назва = base.FieldValue["col_a1"].ToString();
                Код = base.FieldValue["col_a2"].ToString();
                вв = base.FieldValue["col_a3"].ToString();
                
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
            base.FieldValue["col_a3"] = вв;
            
            BaseSave();
        }

        public string Serialize()
        {
            return 
            "<Новини>" +
               "<uid>" + base.UnigueID.ToString() + "</uid>" +
               "<Назва>" + "<![CDATA[" + Назва + "]]>" + "</Назва>"  +
               "<Код>" + "<![CDATA[" + Код + "]]>" + "</Код>"  +
               "<вв>" + "<![CDATA[" + вв + "]]>" + "</вв>"  +
               "</Новини>";
        }

        public void Delete()
        {
            base.BaseDelete();
        }
        
        public Новини_Pointer GetDirectoryPointer()
        {
            Новини_Pointer directoryPointer = new Новини_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public string Назва { get; set; }
        public string Код { get; set; }
        public string вв { get; set; }
        
        //Табличні частини
        public Новини_tyurty_TablePart tyurty_TablePart { get; set; }
        public Новини_tyurty_Копія_1_TablePart tyurty_Копія_1_TablePart { get; set; }
        
    }
    
    
    class Новини_Pointer : DirectoryPointer
    {
        public Новини_Pointer(object uid = null) : base(Config.Kernel, "tab_a17")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public Новини_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a17")
        {
            base.Init(uid, fields);
        }
        
        public Новини_Objest GetDirectoryObject()
        {
            Новини_Objest НовиниObjestItem = new Новини_Objest();
            return НовиниObjestItem.Read(base.UnigueID) ? НовиниObjestItem : null;
        }
    }
    
    
    class Новини_Select : DirectorySelect, IDisposable
    {
        public Новини_Select() : base(Config.Kernel, "tab_a17",
            new string[] { "col_a1", "col_a2", "col_a3" },
            new string[] { "Назва", "Код", "вв" }) { }
    
        public bool Select() { return base.BaseSelect(); }
        
        public bool SelectSingle() { if (base.BaseSelectSingle()) { MoveNext(); return true; } else { Current = null; return false; } }
        
        public bool MoveNext() { if (MoveToPosition()) { Current = new Новини_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields); return true; } else { Current = null; return false; } }

        public Новини_Pointer Current { get; private set; }
        
        public Новини_Pointer FindByField(string name, object value)
        {
            Новини_Pointer itemPointer = new Новини_Pointer();
            DirectoryPointer directoryPointer = base.BaseFindByField(name, value);
            if (!directoryPointer.IsEmpty()) itemPointer.Init(directoryPointer.UnigueID);
            return itemPointer;
        }
        
        public List<Новини_Pointer> FindListByField(string name, object value, int limit = 0, int offset = 0)
        {
            List<Новини_Pointer> directoryPointerList = new List<Новини_Pointer>();
            foreach (DirectoryPointer directoryPointer in base.BaseFindListByField(name, value, limit, offset)) 
                directoryPointerList.Add(new Новини_Pointer(directoryPointer.UnigueID));
            return directoryPointerList;
        }
    }
    
      
    class Новини_tyurty_TablePart : DirectoryTablePart
    {
        public Новини_tyurty_TablePart(Новини_Objest owner) : base(Config.Kernel, "tab_a18",
             new string[] { "col_a1" }) 
        {
            if (owner == null) throw new Exception("owner null");
            
            Owner = owner;
            Records = new List<Record>();
        }
        
        public Новини_Objest Owner { get; private set; }
        
        public List<Record> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Record record = new Record();
                record.UID = (Guid)fieldValue["uid"];
                
                record.rtyer = fieldValue["col_a1"].ToString();
                
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

                    fieldValue.Add("col_a1", record.rtyer);
                    
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
                rtyer = "";
                
            }
        
            
            public Record(
                string _rtyer = "")
            {
                rtyer = _rtyer;
                
            }
            public string rtyer { get; set; }
            
        }
    }
      
    class Новини_tyurty_Копія_1_TablePart : DirectoryTablePart
    {
        public Новини_tyurty_Копія_1_TablePart(Новини_Objest owner) : base(Config.Kernel, "tab_a29",
             new string[] { "col_a1" }) 
        {
            if (owner == null) throw new Exception("owner null");
            
            Owner = owner;
            Records = new List<Record>();
        }
        
        public Новини_Objest Owner { get; private set; }
        
        public List<Record> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Record record = new Record();
                record.UID = (Guid)fieldValue["uid"];
                
                record.rtyer = fieldValue["col_a1"].ToString();
                
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

                    fieldValue.Add("col_a1", record.rtyer);
                    
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
                rtyer = "";
                
            }
        
            
            public Record(
                string _rtyer = "")
            {
                rtyer = _rtyer;
                
            }
            public string rtyer { get; set; }
            
        }
    }
      ///<summary>
    ///Список.
    ///</summary>
    class Новини_Список_View : DirectoryView
    {
        public Новини_Список_View() : base(Config.Kernel, "tab_a17", 
             new string[] { "col_a1", "col_a2" },
             new string[] { "Назва", "Код" },
             new string[] { "string", "string" },
             "Довідник_Новини_Список")
        {
            
        }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "Склад"
    
    class Склад_Objest : DirectoryObject
    {
        public Склад_Objest() : base(Config.Kernel, "tab_a24",
             new string[] { "col_a5", "col_a6" }) 
        {
            Назва = "";
            Код = "";
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Назва = base.FieldValue["col_a5"].ToString();
                Код = base.FieldValue["col_a6"].ToString();
                
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
            
            BaseSave();
        }

        public string Serialize()
        {
            return 
            "<Склад>" +
               "<uid>" + base.UnigueID.ToString() + "</uid>" +
               "<Назва>" + "<![CDATA[" + Назва + "]]>" + "</Назва>"  +
               "<Код>" + "<![CDATA[" + Код + "]]>" + "</Код>"  +
               "</Склад>";
        }

        public void Delete()
        {
            base.BaseDelete();
        }
        
        public Склад_Pointer GetDirectoryPointer()
        {
            Склад_Pointer directoryPointer = new Склад_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public string Назва { get; set; }
        public string Код { get; set; }
        
    }
    
    
    class Склад_Pointer : DirectoryPointer
    {
        public Склад_Pointer(object uid = null) : base(Config.Kernel, "tab_a24")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public Склад_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a24")
        {
            base.Init(uid, fields);
        }
        
        public Склад_Objest GetDirectoryObject()
        {
            Склад_Objest СкладObjestItem = new Склад_Objest();
            return СкладObjestItem.Read(base.UnigueID) ? СкладObjestItem : null;
        }
    }
    
    
    class Склад_Select : DirectorySelect, IDisposable
    {
        public Склад_Select() : base(Config.Kernel, "tab_a24",
            new string[] { "col_a5", "col_a6" },
            new string[] { "Назва", "Код" }) { }
    
        public bool Select() { return base.BaseSelect(); }
        
        public bool SelectSingle() { if (base.BaseSelectSingle()) { MoveNext(); return true; } else { Current = null; return false; } }
        
        public bool MoveNext() { if (MoveToPosition()) { Current = new Склад_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields); return true; } else { Current = null; return false; } }

        public Склад_Pointer Current { get; private set; }
        
        public Склад_Pointer FindByField(string name, object value)
        {
            Склад_Pointer itemPointer = new Склад_Pointer();
            DirectoryPointer directoryPointer = base.BaseFindByField(name, value);
            if (!directoryPointer.IsEmpty()) itemPointer.Init(directoryPointer.UnigueID);
            return itemPointer;
        }
        
        public List<Склад_Pointer> FindListByField(string name, object value, int limit = 0, int offset = 0)
        {
            List<Склад_Pointer> directoryPointerList = new List<Склад_Pointer>();
            foreach (DirectoryPointer directoryPointer in base.BaseFindListByField(name, value, limit, offset)) 
                directoryPointerList.Add(new Склад_Pointer(directoryPointer.UnigueID));
            return directoryPointerList;
        }
    }
    
      ///<summary>
    ///Список.
    ///</summary>
    class Склад_Список_View : DirectoryView
    {
        public Склад_Список_View() : base(Config.Kernel, "tab_a24", 
             new string[] { "col_a5", "col_a6" },
             new string[] { "Назва", "Код" },
             new string[] { "string", "string" },
             "Довідник_Склад_Список")
        {
            
        }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "ТипиЦін"
    
    class ТипиЦін_Objest : DirectoryObject
    {
        public ТипиЦін_Objest() : base(Config.Kernel, "tab_a25",
             new string[] { "col_a7", "col_a8" }) 
        {
            Назва = "";
            Код = "";
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
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
            base.FieldValue["col_a7"] = Назва;
            base.FieldValue["col_a8"] = Код;
            
            BaseSave();
        }

        public string Serialize()
        {
            return 
            "<ТипиЦін>" +
               "<uid>" + base.UnigueID.ToString() + "</uid>" +
               "<Назва>" + "<![CDATA[" + Назва + "]]>" + "</Назва>"  +
               "<Код>" + "<![CDATA[" + Код + "]]>" + "</Код>"  +
               "</ТипиЦін>";
        }

        public void Delete()
        {
            base.BaseDelete();
        }
        
        public ТипиЦін_Pointer GetDirectoryPointer()
        {
            ТипиЦін_Pointer directoryPointer = new ТипиЦін_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public string Назва { get; set; }
        public string Код { get; set; }
        
    }
    
    
    class ТипиЦін_Pointer : DirectoryPointer
    {
        public ТипиЦін_Pointer(object uid = null) : base(Config.Kernel, "tab_a25")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public ТипиЦін_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a25")
        {
            base.Init(uid, fields);
        }
        
        public ТипиЦін_Objest GetDirectoryObject()
        {
            ТипиЦін_Objest ТипиЦінObjestItem = new ТипиЦін_Objest();
            return ТипиЦінObjestItem.Read(base.UnigueID) ? ТипиЦінObjestItem : null;
        }
    }
    
    
    class ТипиЦін_Select : DirectorySelect, IDisposable
    {
        public ТипиЦін_Select() : base(Config.Kernel, "tab_a25",
            new string[] { "col_a7", "col_a8" },
            new string[] { "Назва", "Код" }) { }
    
        public bool Select() { return base.BaseSelect(); }
        
        public bool SelectSingle() { if (base.BaseSelectSingle()) { MoveNext(); return true; } else { Current = null; return false; } }
        
        public bool MoveNext() { if (MoveToPosition()) { Current = new ТипиЦін_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields); return true; } else { Current = null; return false; } }

        public ТипиЦін_Pointer Current { get; private set; }
        
        public ТипиЦін_Pointer FindByField(string name, object value)
        {
            ТипиЦін_Pointer itemPointer = new ТипиЦін_Pointer();
            DirectoryPointer directoryPointer = base.BaseFindByField(name, value);
            if (!directoryPointer.IsEmpty()) itemPointer.Init(directoryPointer.UnigueID);
            return itemPointer;
        }
        
        public List<ТипиЦін_Pointer> FindListByField(string name, object value, int limit = 0, int offset = 0)
        {
            List<ТипиЦін_Pointer> directoryPointerList = new List<ТипиЦін_Pointer>();
            foreach (DirectoryPointer directoryPointer in base.BaseFindListByField(name, value, limit, offset)) 
                directoryPointerList.Add(new ТипиЦін_Pointer(directoryPointer.UnigueID));
            return directoryPointerList;
        }
    }
    
      ///<summary>
    ///Список.
    ///</summary>
    class ТипиЦін_Список_View : DirectoryView
    {
        public ТипиЦін_Список_View() : base(Config.Kernel, "tab_a25", 
             new string[] { "col_a7", "col_a8" },
             new string[] { "Назва", "Код" },
             new string[] { "string", "string" },
             "Довідник_ТипиЦін_Список")
        {
            
        }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "Хронологія"
    
    class Хронологія_Objest : DirectoryObject
    {
        public Хронологія_Objest() : base(Config.Kernel, "tab_a28",
             new string[] { "col_a1", "col_a2", "col_a3", "col_a4" }) 
        {
            Назва = "";
            Код = "";
            Дата = DateTime.MinValue;
            Інформація = "";
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Назва = base.FieldValue["col_a1"].ToString();
                Код = base.FieldValue["col_a2"].ToString();
                Дата = (base.FieldValue["col_a3"] != DBNull.Value) ? DateTime.Parse(base.FieldValue["col_a3"].ToString()) : DateTime.MinValue;
                Інформація = base.FieldValue["col_a4"].ToString();
                
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
            base.FieldValue["col_a3"] = Дата;
            base.FieldValue["col_a4"] = Інформація;
            
            BaseSave();
        }

        public string Serialize()
        {
            return 
            "<Хронологія>" +
               "<uid>" + base.UnigueID.ToString() + "</uid>" +
               "<Назва>" + "<![CDATA[" + Назва + "]]>" + "</Назва>"  +
               "<Код>" + "<![CDATA[" + Код + "]]>" + "</Код>"  +
               "<Дата>" + Дата.ToString() + "</Дата>"  +
               "<Інформація>" + "<![CDATA[" + Інформація + "]]>" + "</Інформація>"  +
               "</Хронологія>";
        }

        public void Delete()
        {
            base.BaseDelete();
        }
        
        public Хронологія_Pointer GetDirectoryPointer()
        {
            Хронологія_Pointer directoryPointer = new Хронологія_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public string Назва { get; set; }
        public string Код { get; set; }
        public DateTime Дата { get; set; }
        public string Інформація { get; set; }
        
    }
    
    
    class Хронологія_Pointer : DirectoryPointer
    {
        public Хронологія_Pointer(object uid = null) : base(Config.Kernel, "tab_a28")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public Хронологія_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a28")
        {
            base.Init(uid, fields);
        }
        
        public Хронологія_Objest GetDirectoryObject()
        {
            Хронологія_Objest ХронологіяObjestItem = new Хронологія_Objest();
            return ХронологіяObjestItem.Read(base.UnigueID) ? ХронологіяObjestItem : null;
        }
    }
    
    
    class Хронологія_Select : DirectorySelect, IDisposable
    {
        public Хронологія_Select() : base(Config.Kernel, "tab_a28",
            new string[] { "col_a1", "col_a2", "col_a3", "col_a4" },
            new string[] { "Назва", "Код", "Дата", "Інформація" }) { }
    
        public bool Select() { return base.BaseSelect(); }
        
        public bool SelectSingle() { if (base.BaseSelectSingle()) { MoveNext(); return true; } else { Current = null; return false; } }
        
        public bool MoveNext() { if (MoveToPosition()) { Current = new Хронологія_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields); return true; } else { Current = null; return false; } }

        public Хронологія_Pointer Current { get; private set; }
        
        public Хронологія_Pointer FindByField(string name, object value)
        {
            Хронологія_Pointer itemPointer = new Хронологія_Pointer();
            DirectoryPointer directoryPointer = base.BaseFindByField(name, value);
            if (!directoryPointer.IsEmpty()) itemPointer.Init(directoryPointer.UnigueID);
            return itemPointer;
        }
        
        public List<Хронологія_Pointer> FindListByField(string name, object value, int limit = 0, int offset = 0)
        {
            List<Хронологія_Pointer> directoryPointerList = new List<Хронологія_Pointer>();
            foreach (DirectoryPointer directoryPointer in base.BaseFindListByField(name, value, limit, offset)) 
                directoryPointerList.Add(new Хронологія_Pointer(directoryPointer.UnigueID));
            return directoryPointerList;
        }
    }
    
      ///<summary>
    ///Список.
    ///</summary>
    class Хронологія_Список_View : DirectoryView
    {
        public Хронологія_Список_View() : base(Config.Kernel, "tab_a28", 
             new string[] { "col_a1", "col_a2" },
             new string[] { "Назва", "Код" },
             new string[] { "string", "string" },
             "Довідник_Хронологія_Список")
        {
            
        }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "Календар"
    
    class Календар_Objest : DirectoryObject
    {
        public Календар_Objest() : base(Config.Kernel, "tab_a32",
             new string[] { "col_a1", "col_a2", "col_a3", "col_a4", "col_a5", "col_a6", "col_a7", "col_a8", "col_a9", "col_b1", "col_b2", "col_b3", "col_b4", "col_b5" }) 
        {
            Назва = "";
            Код = "";
            День = 0;
            Місяць = 0;
            Рік = 0;
            Тиждень = 0;
            ВисокоснийРік = false;
            апап = "";
            апапрап = "";
            апрапр = "";
            куеуке = "";
            укеуке = "";
            укеук = "";
            укеуккк = "";
            
            //Табличні частини
            вапвапв_TablePart = new Календар_вапвапв_TablePart(this);
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Назва = base.FieldValue["col_a1"].ToString();
                Код = base.FieldValue["col_a2"].ToString();
                День = (base.FieldValue["col_a3"] != DBNull.Value) ? (int)base.FieldValue["col_a3"] : 0;
                Місяць = (base.FieldValue["col_a4"] != DBNull.Value) ? (int)base.FieldValue["col_a4"] : 0;
                Рік = (base.FieldValue["col_a5"] != DBNull.Value) ? (int)base.FieldValue["col_a5"] : 0;
                Тиждень = (base.FieldValue["col_a6"] != DBNull.Value) ? (int)base.FieldValue["col_a6"] : 0;
                ВисокоснийРік = (base.FieldValue["col_a7"] != DBNull.Value) ? bool.Parse(base.FieldValue["col_a7"].ToString()) : false;
                апап = base.FieldValue["col_a8"].ToString();
                апапрап = base.FieldValue["col_a9"].ToString();
                апрапр = base.FieldValue["col_b1"].ToString();
                куеуке = base.FieldValue["col_b2"].ToString();
                укеуке = base.FieldValue["col_b3"].ToString();
                укеук = base.FieldValue["col_b4"].ToString();
                укеуккк = base.FieldValue["col_b5"].ToString();
                
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
            base.FieldValue["col_a3"] = День;
            base.FieldValue["col_a4"] = Місяць;
            base.FieldValue["col_a5"] = Рік;
            base.FieldValue["col_a6"] = Тиждень;
            base.FieldValue["col_a7"] = ВисокоснийРік;
            base.FieldValue["col_a8"] = апап;
            base.FieldValue["col_a9"] = апапрап;
            base.FieldValue["col_b1"] = апрапр;
            base.FieldValue["col_b2"] = куеуке;
            base.FieldValue["col_b3"] = укеуке;
            base.FieldValue["col_b4"] = укеук;
            base.FieldValue["col_b5"] = укеуккк;
            
            BaseSave();
        }

        public string Serialize()
        {
            return 
            "<Календар>" +
               "<uid>" + base.UnigueID.ToString() + "</uid>" +
               "<Назва>" + "<![CDATA[" + Назва + "]]>" + "</Назва>"  +
               "<Код>" + "<![CDATA[" + Код + "]]>" + "</Код>"  +
               "<День>" + День.ToString() + "</День>"  +
               "<Місяць>" + Місяць.ToString() + "</Місяць>"  +
               "<Рік>" + Рік.ToString() + "</Рік>"  +
               "<Тиждень>" + Тиждень.ToString() + "</Тиждень>"  +
               "<ВисокоснийРік>" + (ВисокоснийРік == true ? "1" : "0") + "</ВисокоснийРік>"  +
               "<апап>" + "<![CDATA[" + апап + "]]>" + "</апап>"  +
               "<апапрап>" + "<![CDATA[" + апапрап + "]]>" + "</апапрап>"  +
               "<апрапр>" + "<![CDATA[" + апрапр + "]]>" + "</апрапр>"  +
               "<куеуке>" + "<![CDATA[" + куеуке + "]]>" + "</куеуке>"  +
               "<укеуке>" + "<![CDATA[" + укеуке + "]]>" + "</укеуке>"  +
               "<укеук>" + "<![CDATA[" + укеук + "]]>" + "</укеук>"  +
               "<укеуккк>" + "<![CDATA[" + укеуккк + "]]>" + "</укеуккк>"  +
               "</Календар>";
        }

        public void Delete()
        {
            base.BaseDelete();
        }
        
        public Календар_Pointer GetDirectoryPointer()
        {
            Календар_Pointer directoryPointer = new Календар_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public string Назва { get; set; }
        public string Код { get; set; }
        public int День { get; set; }
        public int Місяць { get; set; }
        public int Рік { get; set; }
        public int Тиждень { get; set; }
        public bool ВисокоснийРік { get; set; }
        public string апап { get; set; }
        public string апапрап { get; set; }
        public string апрапр { get; set; }
        public string куеуке { get; set; }
        public string укеуке { get; set; }
        public string укеук { get; set; }
        public string укеуккк { get; set; }
        
        //Табличні частини
        public Календар_вапвапв_TablePart вапвапв_TablePart { get; set; }
        
    }
    
    
    class Календар_Pointer : DirectoryPointer
    {
        public Календар_Pointer(object uid = null) : base(Config.Kernel, "tab_a32")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public Календар_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a32")
        {
            base.Init(uid, fields);
        }
        
        public Календар_Objest GetDirectoryObject()
        {
            Календар_Objest КалендарObjestItem = new Календар_Objest();
            return КалендарObjestItem.Read(base.UnigueID) ? КалендарObjestItem : null;
        }
    }
    
    
    class Календар_Select : DirectorySelect, IDisposable
    {
        public Календар_Select() : base(Config.Kernel, "tab_a32",
            new string[] { "col_a1", "col_a2", "col_a3", "col_a4", "col_a5", "col_a6", "col_a7", "col_a8", "col_a9", "col_b1", "col_b2", "col_b3", "col_b4", "col_b5" },
            new string[] { "Назва", "Код", "День", "Місяць", "Рік", "Тиждень", "ВисокоснийРік", "апап", "апапрап", "апрапр", "куеуке", "укеуке", "укеук", "укеуккк" }) { }
    
        public bool Select() { return base.BaseSelect(); }
        
        public bool SelectSingle() { if (base.BaseSelectSingle()) { MoveNext(); return true; } else { Current = null; return false; } }
        
        public bool MoveNext() { if (MoveToPosition()) { Current = new Календар_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields); return true; } else { Current = null; return false; } }

        public Календар_Pointer Current { get; private set; }
        
        public Календар_Pointer FindByField(string name, object value)
        {
            Календар_Pointer itemPointer = new Календар_Pointer();
            DirectoryPointer directoryPointer = base.BaseFindByField(name, value);
            if (!directoryPointer.IsEmpty()) itemPointer.Init(directoryPointer.UnigueID);
            return itemPointer;
        }
        
        public List<Календар_Pointer> FindListByField(string name, object value, int limit = 0, int offset = 0)
        {
            List<Календар_Pointer> directoryPointerList = new List<Календар_Pointer>();
            foreach (DirectoryPointer directoryPointer in base.BaseFindListByField(name, value, limit, offset)) 
                directoryPointerList.Add(new Календар_Pointer(directoryPointer.UnigueID));
            return directoryPointerList;
        }
    }
    
      
    class Календар_вапвапв_TablePart : DirectoryTablePart
    {
        public Календар_вапвапв_TablePart(Календар_Objest owner) : base(Config.Kernel, "tab_a22",
             new string[] { "col_a1", "col_a2", "col_a3" }) 
        {
            if (owner == null) throw new Exception("owner null");
            
            Owner = owner;
            Records = new List<Record>();
        }
        
        public Календар_Objest Owner { get; private set; }
        
        public List<Record> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Record record = new Record();
                record.UID = (Guid)fieldValue["uid"];
                
                record.вапвап = fieldValue["col_a1"].ToString();
                record.пвапв = fieldValue["col_a2"].ToString();
                record.gthyjgfhjfg = fieldValue["col_a3"].ToString();
                
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

                    fieldValue.Add("col_a1", record.вапвап);
                    fieldValue.Add("col_a2", record.пвапв);
                    fieldValue.Add("col_a3", record.gthyjgfhjfg);
                    
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
                вапвап = "";
                пвапв = "";
                gthyjgfhjfg = "";
                
            }
        
            
            public Record(
                string _вапвап = "", string _пвапв = "", string _gthyjgfhjfg = "")
            {
                вапвап = _вапвап;
                пвапв = _пвапв;
                gthyjgfhjfg = _gthyjgfhjfg;
                
            }
            public string вапвап { get; set; }
            public string пвапв { get; set; }
            public string gthyjgfhjfg { get; set; }
            
        }
    }
      ///<summary>
    ///Список.
    ///</summary>
    class Календар_Список_View : DirectoryView
    {
        public Календар_Список_View() : base(Config.Kernel, "tab_a32", 
             new string[] { "col_a1", "col_a2" },
             new string[] { "Назва", "Код" },
             new string[] { "string", "string" },
             "Довідник_Календар_Список")
        {
            
        }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "Завдання"
    
    class Завдання_Objest : DirectoryObject
    {
        public Завдання_Objest() : base(Config.Kernel, "tab_a33",
             new string[] { "col_a6", "col_a8" }) 
        {
            Назва = "";
            Опис = "";
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Назва = base.FieldValue["col_a6"].ToString();
                Опис = base.FieldValue["col_a8"].ToString();
                
                BaseClear();
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["col_a6"] = Назва;
            base.FieldValue["col_a8"] = Опис;
            
            BaseSave();
        }

        public string Serialize()
        {
            return 
            "<Завдання>" +
               "<uid>" + base.UnigueID.ToString() + "</uid>" +
               "<Назва>" + "<![CDATA[" + Назва + "]]>" + "</Назва>"  +
               "<Опис>" + "<![CDATA[" + Опис + "]]>" + "</Опис>"  +
               "</Завдання>";
        }

        public void Delete()
        {
            base.BaseDelete();
        }
        
        public Завдання_Pointer GetDirectoryPointer()
        {
            Завдання_Pointer directoryPointer = new Завдання_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public string Назва { get; set; }
        public string Опис { get; set; }
        
    }
    
    
    class Завдання_Pointer : DirectoryPointer
    {
        public Завдання_Pointer(object uid = null) : base(Config.Kernel, "tab_a33")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public Завдання_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a33")
        {
            base.Init(uid, fields);
        }
        
        public Завдання_Objest GetDirectoryObject()
        {
            Завдання_Objest ЗавданняObjestItem = new Завдання_Objest();
            return ЗавданняObjestItem.Read(base.UnigueID) ? ЗавданняObjestItem : null;
        }
    }
    
    
    class Завдання_Select : DirectorySelect, IDisposable
    {
        public Завдання_Select() : base(Config.Kernel, "tab_a33",
            new string[] { "col_a6", "col_a8" },
            new string[] { "Назва", "Опис" }) { }
    
        public bool Select() { return base.BaseSelect(); }
        
        public bool SelectSingle() { if (base.BaseSelectSingle()) { MoveNext(); return true; } else { Current = null; return false; } }
        
        public bool MoveNext() { if (MoveToPosition()) { Current = new Завдання_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields); return true; } else { Current = null; return false; } }

        public Завдання_Pointer Current { get; private set; }
        
        public Завдання_Pointer FindByField(string name, object value)
        {
            Завдання_Pointer itemPointer = new Завдання_Pointer();
            DirectoryPointer directoryPointer = base.BaseFindByField(name, value);
            if (!directoryPointer.IsEmpty()) itemPointer.Init(directoryPointer.UnigueID);
            return itemPointer;
        }
        
        public List<Завдання_Pointer> FindListByField(string name, object value, int limit = 0, int offset = 0)
        {
            List<Завдання_Pointer> directoryPointerList = new List<Завдання_Pointer>();
            foreach (DirectoryPointer directoryPointer in base.BaseFindListByField(name, value, limit, offset)) 
                directoryPointerList.Add(new Завдання_Pointer(directoryPointer.UnigueID));
            return directoryPointerList;
        }
    }
    
      ///<summary>
    ///Список.
    ///</summary>
    class Завдання_Список_View : DirectoryView
    {
        public Завдання_Список_View() : base(Config.Kernel, "tab_a33", 
             new string[] { "col_a6", "col_a7" },
             new string[] { "Назва", "Код" },
             new string[] { "string", "" },
             "Довідник_Завдання_Список")
        {
            
        }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "Проекти"
    ///<summary>
    ///Список проектів (наприклад: косарка, станок і т.д).
    ///</summary>
    class Проекти_Objest : DirectoryObject
    {
        public Проекти_Objest() : base(Config.Kernel, "tab_a36",
             new string[] { "col_a1" }) 
        {
            Назва = "";
            
            //Табличні частини
            Інформація_TablePart = new Проекти_Інформація_TablePart(this);
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Назва = base.FieldValue["col_a1"].ToString();
                
                BaseClear();
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["col_a1"] = Назва;
            
            BaseSave();
        }

        public string Serialize()
        {
            return 
            "<Проекти>" +
               "<uid>" + base.UnigueID.ToString() + "</uid>" +
               "<Назва>" + "<![CDATA[" + Назва + "]]>" + "</Назва>"  +
               "</Проекти>";
        }

        public void Delete()
        {
            base.BaseDelete();
        }
        
        public Проекти_Pointer GetDirectoryPointer()
        {
            Проекти_Pointer directoryPointer = new Проекти_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public string Назва { get; set; }
        
        //Табличні частини
        public Проекти_Інформація_TablePart Інформація_TablePart { get; set; }
        
    }
    
    ///<summary>
    ///Список проектів (наприклад: косарка, станок і т.д).
    ///</summary>
    class Проекти_Pointer : DirectoryPointer
    {
        public Проекти_Pointer(object uid = null) : base(Config.Kernel, "tab_a36")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public Проекти_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a36")
        {
            base.Init(uid, fields);
        }
        
        public Проекти_Objest GetDirectoryObject()
        {
            Проекти_Objest ПроектиObjestItem = new Проекти_Objest();
            return ПроектиObjestItem.Read(base.UnigueID) ? ПроектиObjestItem : null;
        }
    }
    
    ///<summary>
    ///Список проектів (наприклад: косарка, станок і т.д).
    ///</summary>
    class Проекти_Select : DirectorySelect, IDisposable
    {
        public Проекти_Select() : base(Config.Kernel, "tab_a36",
            new string[] { "col_a1" },
            new string[] { "Назва" }) { }
    
        public bool Select() { return base.BaseSelect(); }
        
        public bool SelectSingle() { if (base.BaseSelectSingle()) { MoveNext(); return true; } else { Current = null; return false; } }
        
        public bool MoveNext() { if (MoveToPosition()) { Current = new Проекти_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields); return true; } else { Current = null; return false; } }

        public Проекти_Pointer Current { get; private set; }
        
        public Проекти_Pointer FindByField(string name, object value)
        {
            Проекти_Pointer itemPointer = new Проекти_Pointer();
            DirectoryPointer directoryPointer = base.BaseFindByField(name, value);
            if (!directoryPointer.IsEmpty()) itemPointer.Init(directoryPointer.UnigueID);
            return itemPointer;
        }
        
        public List<Проекти_Pointer> FindListByField(string name, object value, int limit = 0, int offset = 0)
        {
            List<Проекти_Pointer> directoryPointerList = new List<Проекти_Pointer>();
            foreach (DirectoryPointer directoryPointer in base.BaseFindListByField(name, value, limit, offset)) 
                directoryPointerList.Add(new Проекти_Pointer(directoryPointer.UnigueID));
            return directoryPointerList;
        }
    }
    
      
    class Проекти_Інформація_TablePart : DirectoryTablePart
    {
        public Проекти_Інформація_TablePart(Проекти_Objest owner) : base(Config.Kernel, "tab_a37",
             new string[] { "col_a3", "col_a4", "col_a5" }) 
        {
            if (owner == null) throw new Exception("owner null");
            
            Owner = owner;
            Records = new List<Record>();
        }
        
        public Проекти_Objest Owner { get; private set; }
        
        public List<Record> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Record record = new Record();
                record.UID = (Guid)fieldValue["uid"];
                
                record.ДатаЗапису = (fieldValue["col_a3"] != DBNull.Value) ? DateTime.Parse(fieldValue["col_a3"].ToString()) : DateTime.MinValue;
                record.Запис = fieldValue["col_a4"].ToString();
                record.Сайт = fieldValue["col_a5"].ToString();
                
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

                    fieldValue.Add("col_a3", record.ДатаЗапису);
                    fieldValue.Add("col_a4", record.Запис);
                    fieldValue.Add("col_a5", record.Сайт);
                    
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
                ДатаЗапису = DateTime.MinValue;
                Запис = "";
                Сайт = "";
                
            }
        
            
            public Record(
                DateTime?  _ДатаЗапису = null, string _Запис = "", string _Сайт = "")
            {
                ДатаЗапису = _ДатаЗапису ?? DateTime.MinValue;
                Запис = _Запис;
                Сайт = _Сайт;
                
            }
            public DateTime ДатаЗапису { get; set; }
            public string Запис { get; set; }
            public string Сайт { get; set; }
            
        }
    }
      ///<summary>
    ///Список.
    ///</summary>
    class Проекти_Список_View : DirectoryView
    {
        public Проекти_Список_View() : base(Config.Kernel, "tab_a36", 
             new string[] { "col_a1", "col_a2" },
             new string[] { "Назва", "Код" },
             new string[] { "string", "" },
             "Довідник_Проекти_Список")
        {
            
        }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "tyrter"
    
    class tyrter_Objest : DirectoryObject
    {
        public tyrter_Objest() : base(Config.Kernel, "tab_a38",
             new string[] { "col_a1", "col_a2" }) 
        {
            Назва = "";
            Код = "";
            
            //Табличні частини
            rtrtye_TablePart = new tyrter_rtrtye_TablePart(this);
            
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
            "<tyrter>" +
               "<uid>" + base.UnigueID.ToString() + "</uid>" +
               "<Назва>" + "<![CDATA[" + Назва + "]]>" + "</Назва>"  +
               "<Код>" + "<![CDATA[" + Код + "]]>" + "</Код>"  +
               "</tyrter>";
        }

        public void Delete()
        {
            base.BaseDelete();
        }
        
        public tyrter_Pointer GetDirectoryPointer()
        {
            tyrter_Pointer directoryPointer = new tyrter_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public string Назва { get; set; }
        public string Код { get; set; }
        
        //Табличні частини
        public tyrter_rtrtye_TablePart rtrtye_TablePart { get; set; }
        
    }
    
    
    class tyrter_Pointer : DirectoryPointer
    {
        public tyrter_Pointer(object uid = null) : base(Config.Kernel, "tab_a38")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public tyrter_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a38")
        {
            base.Init(uid, fields);
        }
        
        public tyrter_Objest GetDirectoryObject()
        {
            tyrter_Objest tyrterObjestItem = new tyrter_Objest();
            return tyrterObjestItem.Read(base.UnigueID) ? tyrterObjestItem : null;
        }
    }
    
    
    class tyrter_Select : DirectorySelect, IDisposable
    {
        public tyrter_Select() : base(Config.Kernel, "tab_a38",
            new string[] { "col_a1", "col_a2" },
            new string[] { "Назва", "Код" }) { }
    
        public bool Select() { return base.BaseSelect(); }
        
        public bool SelectSingle() { if (base.BaseSelectSingle()) { MoveNext(); return true; } else { Current = null; return false; } }
        
        public bool MoveNext() { if (MoveToPosition()) { Current = new tyrter_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields); return true; } else { Current = null; return false; } }

        public tyrter_Pointer Current { get; private set; }
        
        public tyrter_Pointer FindByField(string name, object value)
        {
            tyrter_Pointer itemPointer = new tyrter_Pointer();
            DirectoryPointer directoryPointer = base.BaseFindByField(name, value);
            if (!directoryPointer.IsEmpty()) itemPointer.Init(directoryPointer.UnigueID);
            return itemPointer;
        }
        
        public List<tyrter_Pointer> FindListByField(string name, object value, int limit = 0, int offset = 0)
        {
            List<tyrter_Pointer> directoryPointerList = new List<tyrter_Pointer>();
            foreach (DirectoryPointer directoryPointer in base.BaseFindListByField(name, value, limit, offset)) 
                directoryPointerList.Add(new tyrter_Pointer(directoryPointer.UnigueID));
            return directoryPointerList;
        }
    }
    
      
    class tyrter_rtrtye_TablePart : DirectoryTablePart
    {
        public tyrter_rtrtye_TablePart(tyrter_Objest owner) : base(Config.Kernel, "tab_a39",
             new string[] { "col_a1", "col_a2", "col_a3", "col_a4", "col_a5", "col_a6", "col_a7" }) 
        {
            if (owner == null) throw new Exception("owner null");
            
            Owner = owner;
            Records = new List<Record>();
        }
        
        public tyrter_Objest Owner { get; private set; }
        
        public List<Record> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Record record = new Record();
                record.UID = (Guid)fieldValue["uid"];
                
                record.rtyrety = fieldValue["col_a1"].ToString();
                record.tytyu = fieldValue["col_a2"].ToString();
                record.rtyrty = fieldValue["col_a3"].ToString();
                record.rtyrt = fieldValue["col_a4"].ToString();
                record.saads = fieldValue["col_a5"].ToString();
                record.xzxczxc = fieldValue["col_a6"].ToString();
                record.zxczxczxc = fieldValue["col_a7"].ToString();
                
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

                    fieldValue.Add("col_a1", record.rtyrety);
                    fieldValue.Add("col_a2", record.tytyu);
                    fieldValue.Add("col_a3", record.rtyrty);
                    fieldValue.Add("col_a4", record.rtyrt);
                    fieldValue.Add("col_a5", record.saads);
                    fieldValue.Add("col_a6", record.xzxczxc);
                    fieldValue.Add("col_a7", record.zxczxczxc);
                    
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
                rtyrety = "";
                tytyu = "";
                rtyrty = "";
                rtyrt = "";
                saads = "";
                xzxczxc = "";
                zxczxczxc = "";
                
            }
        
            
            public Record(
                string _rtyrety = "", string _tytyu = "", string _rtyrty = "", string _rtyrt = "", string _saads = "", string _xzxczxc = "", string _zxczxczxc = "")
            {
                rtyrety = _rtyrety;
                tytyu = _tytyu;
                rtyrty = _rtyrty;
                rtyrt = _rtyrt;
                saads = _saads;
                xzxczxc = _xzxczxc;
                zxczxczxc = _zxczxczxc;
                
            }
            public string rtyrety { get; set; }
            public string tytyu { get; set; }
            public string rtyrty { get; set; }
            public string rtyrt { get; set; }
            public string saads { get; set; }
            public string xzxczxc { get; set; }
            public string zxczxczxc { get; set; }
            
        }
    }
      ///<summary>
    ///Список.
    ///</summary>
    class tyrter_Список_View : DirectoryView
    {
        public tyrter_Список_View() : base(Config.Kernel, "tab_a38", 
             new string[] { "col_a1", "col_a2" },
             new string[] { "Назва", "Код" },
             new string[] { "string", "string" },
             "Довідник_tyrter_Список")
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
    
    
    public enum ВклВикл
    {
         Вкл = 1,
         Викл = 2
    }
    
    
    public enum ТакНі
    {
         Так = 1,
         Ні = 2
    }
    
    
}

namespace ConfTrade_v1_1.Документи
{
    
    #region DOCUMENT "Test"
    
    
    class Test_Objest : DocumentObject
    {
        public Test_Objest() : base(Config.Kernel, "tab_a50",
             new string[] { "col_a3", "col_a4", "col_a1", "col_a2", "col_a5", "col_a6", "col_a7" }) 
        {
            ДатаДок = DateTime.MinValue;
            НомерДок = 0;
            Фірма = new Довідники.Організації_Pointer();
            Склад = new Довідники.Склад_Pointer();
            ВклВикл = 0;
            ПрихідВідПостачальника = 0;
            ЗагальнаСума = 0;
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                ДатаДок = (base.FieldValue["col_a3"] != DBNull.Value) ? DateTime.Parse(base.FieldValue["col_a3"].ToString()) : DateTime.MinValue;
                НомерДок = (base.FieldValue["col_a4"] != DBNull.Value) ? (int)base.FieldValue["col_a4"] : 0;
                Фірма = new Довідники.Організації_Pointer(base.FieldValue["col_a1"]);
                Склад = new Довідники.Склад_Pointer(base.FieldValue["col_a2"]);
                ВклВикл = (base.FieldValue["col_a5"] != DBNull.Value) ? (Перелічення.ВклВикл)base.FieldValue["col_a5"] : 0;
                ПрихідВідПостачальника = (base.FieldValue["col_a6"] != DBNull.Value) ? (Перелічення.ТакНі)base.FieldValue["col_a6"] : 0;
                ЗагальнаСума = (base.FieldValue["col_a7"] != DBNull.Value) ? (decimal)base.FieldValue["col_a7"] : 0;
                
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
            base.FieldValue["col_a1"] = Фірма.ToString();
            base.FieldValue["col_a2"] = Склад.ToString();
            base.FieldValue["col_a5"] = (int)ВклВикл;
            base.FieldValue["col_a6"] = (int)ПрихідВідПостачальника;
            base.FieldValue["col_a7"] = ЗагальнаСума;
            
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
        public Довідники.Організації_Pointer Фірма { get; set; }
        public Довідники.Склад_Pointer Склад { get; set; }
        public Перелічення.ВклВикл ВклВикл { get; set; }
        public Перелічення.ТакНі ПрихідВідПостачальника { get; set; }
        public decimal ЗагальнаСума { get; set; }
        
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
    
    #region DOCUMENT "ПрихіднаНакладна"
    
    
    class ПрихіднаНакладна_Objest : DocumentObject
    {
        public ПрихіднаНакладна_Objest() : base(Config.Kernel, "tab_a21",
             new string[] { "col_a1", "col_a2", "col_a3", "col_a4", "col_a5", "col_a6" }) 
        {
            ДатаДок = DateTime.MinValue;
            НомерДок = 0;
            Контрагент = new Довідники.Контрагенти_Pointer();
            Склад = new Довідники.Склад_Pointer();
            ДатаПриходу = DateTime.MinValue;
            ТипЦін = new Довідники.ТипиЦін_Pointer();
            
            //Табличні частини
            Товари_TablePart = new ПрихіднаНакладна_Товари_TablePart(this);
            Послуги_TablePart = new ПрихіднаНакладна_Послуги_TablePart(this);
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                ДатаДок = (base.FieldValue["col_a1"] != DBNull.Value) ? DateTime.Parse(base.FieldValue["col_a1"].ToString()) : DateTime.MinValue;
                НомерДок = (base.FieldValue["col_a2"] != DBNull.Value) ? (int)base.FieldValue["col_a2"] : 0;
                Контрагент = new Довідники.Контрагенти_Pointer(base.FieldValue["col_a3"]);
                Склад = new Довідники.Склад_Pointer(base.FieldValue["col_a4"]);
                ДатаПриходу = (base.FieldValue["col_a5"] != DBNull.Value) ? DateTime.Parse(base.FieldValue["col_a5"].ToString()) : DateTime.MinValue;
                ТипЦін = new Довідники.ТипиЦін_Pointer(base.FieldValue["col_a6"]);
                
                BaseClear();
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["col_a1"] = ДатаДок;
            base.FieldValue["col_a2"] = НомерДок;
            base.FieldValue["col_a3"] = Контрагент.ToString();
            base.FieldValue["col_a4"] = Склад.ToString();
            base.FieldValue["col_a5"] = ДатаПриходу;
            base.FieldValue["col_a6"] = ТипЦін.ToString();
            
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
        
        public DateTime ДатаДок { get; set; }
        public int НомерДок { get; set; }
        public Довідники.Контрагенти_Pointer Контрагент { get; set; }
        public Довідники.Склад_Pointer Склад { get; set; }
        public DateTime ДатаПриходу { get; set; }
        public Довідники.ТипиЦін_Pointer ТипЦін { get; set; }
        
        //Табличні частини
        public ПрихіднаНакладна_Товари_TablePart Товари_TablePart { get; set; }
        public ПрихіднаНакладна_Послуги_TablePart Послуги_TablePart { get; set; }
        
    }
    
    
    class ПрихіднаНакладна_Pointer : DocumentPointer
    {
        public ПрихіднаНакладна_Pointer(object uid = null) : base(Config.Kernel, "tab_a21")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public ПрихіднаНакладна_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a21")
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
        public ПрихіднаНакладна_Select() : base(Config.Kernel, "tab_a21") { }
        
        public bool Select() { return base.BaseSelect(); }
        
        public bool SelectSingle() { if (base.BaseSelectSingle()) { MoveNext(); return true; } else { Current = null; return false; } }
        
        public bool MoveNext() { if (MoveToPosition()) { Current = new ПрихіднаНакладна_Pointer(base.DocumentPointerPosition.UnigueID, base.DocumentPointerPosition.Fields); return true; } else { Current = null; return false; } }
        
        public ПрихіднаНакладна_Pointer Current { get; private set; }
    }
    
      
    class ПрихіднаНакладна_Товари_TablePart : DocumentTablePart
    {
        public ПрихіднаНакладна_Товари_TablePart(ПрихіднаНакладна_Objest owner) : base(Config.Kernel, "tab_a26",
             new string[] { "col_a9", "col_b1", "col_b2", "col_b3", "col_b4" }) 
        {
            if (owner == null) throw new Exception("owner null");
            
            Owner = owner;
            Records = new List<Record>();
        }
        
        public ПрихіднаНакладна_Objest Owner { get; private set; }
        
        public List<Record> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Record record = new Record();
                record.UID = (Guid)fieldValue["uid"];
                
                record.Номеклатура = new Довідники.Номенклатура_Pointer(fieldValue["col_a9"]);
                record.Кво = (fieldValue["col_b1"] != DBNull.Value) ? (int)fieldValue["col_b1"] : 0;
                record.Ціна = (fieldValue["col_b2"] != DBNull.Value) ? (decimal)fieldValue["col_b2"] : 0;
                record.Сума = (fieldValue["col_b3"] != DBNull.Value) ? (decimal)fieldValue["col_b3"] : 0;
                record.Скидка = (fieldValue["col_b4"] != DBNull.Value) ? (decimal)fieldValue["col_b4"] : 0;
                
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

                    fieldValue.Add("col_a9", record.Номеклатура.UnigueID.UGuid);
                    fieldValue.Add("col_b1", record.Кво);
                    fieldValue.Add("col_b2", record.Ціна);
                    fieldValue.Add("col_b3", record.Сума);
                    fieldValue.Add("col_b4", record.Скидка);
                    
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
        
        
        public class Record : DocumentTablePartRecord
        {
            public Record()
            {
                Номеклатура = new Довідники.Номенклатура_Pointer();
                Кво = 0;
                Ціна = 0;
                Сума = 0;
                Скидка = 0;
                
            }
        
            
            public Record(
                Довідники.Номенклатура_Pointer _Номеклатура = null, int _Кво = 0, decimal _Ціна = 0, decimal _Сума = 0, decimal _Скидка = 0)
            {
                Номеклатура = _Номеклатура ?? new Довідники.Номенклатура_Pointer();
                Кво = _Кво;
                Ціна = _Ціна;
                Сума = _Сума;
                Скидка = _Скидка;
                
            }
            public Довідники.Номенклатура_Pointer Номеклатура { get; set; }
            public int Кво { get; set; }
            public decimal Ціна { get; set; }
            public decimal Сума { get; set; }
            public decimal Скидка { get; set; }
            
        }
    }
      
    class ПрихіднаНакладна_Послуги_TablePart : DocumentTablePart
    {
        public ПрихіднаНакладна_Послуги_TablePart(ПрихіднаНакладна_Objest owner) : base(Config.Kernel, "tab_a27",
             new string[] { "col_a1", "col_a2", "col_a3", "col_a4" }) 
        {
            if (owner == null) throw new Exception("owner null");
            
            Owner = owner;
            Records = new List<Record>();
        }
        
        public ПрихіднаНакладна_Objest Owner { get; private set; }
        
        public List<Record> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Record record = new Record();
                record.UID = (Guid)fieldValue["uid"];
                
                record.Послуга = new Довідники.Номенклатура_Pointer(fieldValue["col_a1"]);
                record.Кво = (fieldValue["col_a2"] != DBNull.Value) ? (int)fieldValue["col_a2"] : 0;
                record.Ціна = (fieldValue["col_a3"] != DBNull.Value) ? (decimal)fieldValue["col_a3"] : 0;
                record.Сума = (fieldValue["col_a4"] != DBNull.Value) ? (decimal)fieldValue["col_a4"] : 0;
                
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

                    fieldValue.Add("col_a1", record.Послуга.UnigueID.UGuid);
                    fieldValue.Add("col_a2", record.Кво);
                    fieldValue.Add("col_a3", record.Ціна);
                    fieldValue.Add("col_a4", record.Сума);
                    
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
        
        
        public class Record : DocumentTablePartRecord
        {
            public Record()
            {
                Послуга = new Довідники.Номенклатура_Pointer();
                Кво = 0;
                Ціна = 0;
                Сума = 0;
                
            }
        
            
            public Record(
                Довідники.Номенклатура_Pointer _Послуга = null, int _Кво = 0, decimal _Ціна = 0, decimal _Сума = 0)
            {
                Послуга = _Послуга ?? new Довідники.Номенклатура_Pointer();
                Кво = _Кво;
                Ціна = _Ціна;
                Сума = _Сума;
                
            }
            public Довідники.Номенклатура_Pointer Послуга { get; set; }
            public int Кво { get; set; }
            public decimal Ціна { get; set; }
            public decimal Сума { get; set; }
            
        }
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
  
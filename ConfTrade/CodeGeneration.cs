

/*
 *
 * Конфігурації "ConfTrade 1.1"
 * Автор Yurik
 * Дата конфігурації: 04.02.2020 14:46:23
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
        public Tovary_Objest() : base(Config.Kernel, "tovary_v1_1",
             new string[] { "name", "code", "description", "field1", "field2", "field3", "field4", "field5", "od2", "count", "num", "isupdate", "isupdate2", "date_add", "date_add3", "time_add", "datetime_add", "text_arr", "int_arr", "numeric_arr" }) 
        {
            Name = "";
            Code = "";
            Description = "";
            Field1 = "";
            Field2 = "";
            Field3 = "";
            Field4 = "";
            Field5 = "";
            Od2 = new Od_Pointer();
            Count = 0;
            Num = 0;
            Isupdate = false;
            Isupdate2 = false;
            Date_add = DateTime.MinValue;
            Date_add3 = DateTime.MinValue;
            Time_add = DateTime.MinValue.TimeOfDay;
            Datetime_add = DateTime.MinValue;
            Text_arr = new string[] { };
            Int_arr = new int[] { };
            Numeric_arr = new decimal[] { };
            
            //Табличні частини
            Ceny_TablePart = new Tovary_Ceny_TablePart(this);
            Od_List_TablePart = new Tovary_Od_List_TablePart(this);
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Name = base.FieldValue["name"].ToString();
                Code = base.FieldValue["code"].ToString();
                Description = base.FieldValue["description"].ToString();
                Field1 = base.FieldValue["field1"].ToString();
                Field2 = base.FieldValue["field2"].ToString();
                Field3 = base.FieldValue["field3"].ToString();
                Field4 = base.FieldValue["field4"].ToString();
                Field5 = base.FieldValue["field5"].ToString();
                Od2 = new Od_Pointer(base.FieldValue["od2"]);
                Count = (int)base.FieldValue["count"];
                Num = (base.FieldValue["num"] != DBNull.Value) ? (decimal)base.FieldValue["num"] : 0;
                Isupdate = (bool)base.FieldValue["isupdate"];
                Isupdate2 = (bool)base.FieldValue["isupdate2"];
                Date_add = (base.FieldValue["date_add"] != DBNull.Value) ? DateTime.Parse(base.FieldValue["date_add"].ToString()) : DateTime.MinValue;
                Date_add3 = (base.FieldValue["date_add3"] != DBNull.Value) ? DateTime.Parse(base.FieldValue["date_add3"].ToString()) : DateTime.MinValue;
                Time_add = (base.FieldValue["time_add"] != DBNull.Value) ? TimeSpan.Parse(base.FieldValue["time_add"].ToString()) : DateTime.MinValue.TimeOfDay;
                Datetime_add = (base.FieldValue["datetime_add"] != DBNull.Value) ? DateTime.Parse(base.FieldValue["datetime_add"].ToString()) : DateTime.MinValue;
                Text_arr = (base.FieldValue["text_arr"] != DBNull.Value) ? (string[])base.FieldValue["text_arr"] : new string[] { };
                Int_arr = (base.FieldValue["int_arr"] != DBNull.Value) ? (int[])base.FieldValue["int_arr"] : new int[] { };
                Numeric_arr = (base.FieldValue["numeric_arr"] != DBNull.Value) ? (decimal[])base.FieldValue["numeric_arr"] : new decimal[] { };
                
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["name"] = Name;
            base.FieldValue["code"] = Code;
            base.FieldValue["description"] = Description;
            base.FieldValue["field1"] = Field1;
            base.FieldValue["field2"] = Field2;
            base.FieldValue["field3"] = Field3;
            base.FieldValue["field4"] = Field4;
            base.FieldValue["field5"] = Field5;
            base.FieldValue["od2"] = Od2.UnigueID.UGuid;
            base.FieldValue["count"] = Count;
            base.FieldValue["num"] = Num;
            base.FieldValue["isupdate"] = Isupdate;
            base.FieldValue["isupdate2"] = Isupdate2;
            base.FieldValue["date_add"] = Date_add;
            base.FieldValue["date_add3"] = Date_add3;
            base.FieldValue["time_add"] = Time_add;
            base.FieldValue["datetime_add"] = Datetime_add;
            base.FieldValue["text_arr"] = Text_arr;
            base.FieldValue["int_arr"] = Int_arr;
            base.FieldValue["numeric_arr"] = Numeric_arr;
            
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
        public string Description { get; set; }
        public string Field1 { get; set; }
        public string Field2 { get; set; }
        public string Field3 { get; set; }
        public string Field4 { get; set; }
        public string Field5 { get; set; }
        public Od_Pointer Od2 { get; set; }
        public int Count { get; set; }
        public decimal Num { get; set; }
        public bool Isupdate { get; set; }
        public bool Isupdate2 { get; set; }
        public DateTime Date_add { get; set; }
        public DateTime Date_add3 { get; set; }
        public TimeSpan Time_add { get; set; }
        public DateTime Datetime_add { get; set; }
        public string[] Text_arr { get; set; }
        public int[] Int_arr { get; set; }
        public decimal[] Numeric_arr { get; set; }
        
        //Табличні частини
        public Tovary_Ceny_TablePart Ceny_TablePart { get; set; }
        public Tovary_Od_List_TablePart Od_List_TablePart { get; set; }
        
    }
    
    /// <summary> 
    /// Довідник Товари
    /// </summary>
    class Tovary_Pointer : DirectoryPointer
    {
        public Tovary_Pointer(object uid = null) : base(Config.Kernel, "tovary_v1_1")
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
        public Tovary_Select() : base(Config.Kernel, "tovary_v1_1") { }
    
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
        public Tovary_Ceny_TablePart(Tovary_Objest owner) : base(Config.Kernel, "tovary_ceny_tablepart_v1_1",
             new string[] { "name", "cena" }) 
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
            
        }
        
        public Tovary_Ceny_TablePartRecord(
            string _Name = "", decimal _Cena = 0)
        {
            Name = _Name;
            Cena = _Cena;
            
        }
        
        public string Name { get; set; }
        public decimal Cena { get; set; }
        
    }
      
    /// <summary>
    /// Таблична частина одиниць вимірів
    /// </summary>
    class Tovary_Od_List_TablePart : DirectoryTablePart
    {
        public Tovary_Od_List_TablePart(Tovary_Objest owner) : base(Config.Kernel, "tovary_od_tablepart_v1_1",
             new string[] { "od_pointer", "name", "fullname" }) 
        {
            Owner = owner;
            Records = new List<Tovary_Od_List_TablePartRecord>();
        }
        
        public Tovary_Objest Owner { get; private set; }
        
        public List<Tovary_Od_List_TablePartRecord> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.FieldValueList.Clear();

            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Tovary_Od_List_TablePartRecord record = new Tovary_Od_List_TablePartRecord();

                record.Od_Pointer = new Od_Pointer(fieldValue["od_pointer"]);
                record.Name = fieldValue["name"].ToString();
                record.FullName = fieldValue["fullname"].ToString();
                
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

                foreach (Tovary_Od_List_TablePartRecord record in Records)
                {
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                    fieldValue.Add("od_pointer", record.Od_Pointer.UnigueID.UGuid);
                    fieldValue.Add("name", record.Name);
                    fieldValue.Add("fullname", record.FullName);
                    
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
    /// Таблична частина одиниць вимірів
    /// </summary>
    class Tovary_Od_List_TablePartRecord : DirectoryTablePartRecord
    {
        public Tovary_Od_List_TablePartRecord()
        {
            Od_Pointer = new Od_Pointer();
            Name = "";
            FullName = "";
            
        }
        
        public Tovary_Od_List_TablePartRecord(
            Od_Pointer _Od_Pointer = null, string _Name = "", string _FullName = "")
        {
            Od_Pointer = _Od_Pointer;
            Name = _Name;
            FullName = _FullName;
            
        }
        
        public Od_Pointer Od_Pointer { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        
    }
        
    /// <summary> 
    /// Візуалізація 1
    /// </summary>
    class Tovary_View1_View : DirectoryView
    {
        public Tovary_View1_View() : base(Config.Kernel, "", 
            new string[] { "name", "code" })
        {

        }
    }
      
    
    #endregion
    
    #region DIRECTORY "Od"
    
    /// <summary> 
    /// Одиниці вимірів
    /// </summary>
    class Od_Objest : DirectoryObject
    {
        public Od_Objest() : base(Config.Kernel, "od_v1_1",
             new string[] { "name", "fullname", "test" }) 
        {
            Name = "";
            FullName = "";
            Test = "";
            
            //Табличні частини
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Name = base.FieldValue["name"].ToString();
                FullName = base.FieldValue["fullname"].ToString();
                Test = base.FieldValue["test"].ToString();
                
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["name"] = Name;
            base.FieldValue["fullname"] = FullName;
            base.FieldValue["test"] = Test;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public Od_Pointer GetDirectoryPointer()
        {
            Od_Pointer directoryPointer = new Od_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public string Name { get; set; }
        public string FullName { get; set; }
        public string Test { get; set; }
        
        //Табличні частини
        
    }
    
    /// <summary> 
    /// Одиниці вимірів
    /// </summary>
    class Od_Pointer : DirectoryPointer
    {
        public Od_Pointer(object uid = null) : base(Config.Kernel, "od_v1_1")
        {
            if (uid != null && uid != DBNull.Value) base.Init(new UnigueID((Guid)uid), null);
        }

        public Od_Objest GetDirectoryObject()
        {
            Od_Objest OdObjestItem = new Od_Objest();
            OdObjestItem.Read(base.UnigueID);
            return OdObjestItem;
        }
    }
    
    /// <summary> 
    /// Одиниці вимірів
    /// </summary>
    class Od_Select : DirectorySelect
    {
        public Od_Select() : base(Config.Kernel, "od_v1_1") { }
    
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
                Current = new Od_Pointer();
                Current.Init(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields);
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }

        public Od_Pointer Current { get; private set; }
    }
    
      
    
    #endregion
    
    #region DIRECTORY "NewDir"
    
    /// <summary> 
    /// Новий довідник
    /// </summary>
    class NewDir_Objest : DirectoryObject
    {
        public NewDir_Objest() : base(Config.Kernel, "newdir_v1_1",
             new string[] { "name", "fullname", "test" }) 
        {
            Name = "";
            FullName = "";
            Test = "";
            
            //Табличні частини
            Ceny_TablePart = new NewDir_Ceny_TablePart(this);
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Name = base.FieldValue["name"].ToString();
                FullName = base.FieldValue["fullname"].ToString();
                Test = base.FieldValue["test"].ToString();
                
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["name"] = Name;
            base.FieldValue["fullname"] = FullName;
            base.FieldValue["test"] = Test;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public NewDir_Pointer GetDirectoryPointer()
        {
            NewDir_Pointer directoryPointer = new NewDir_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public string Name { get; set; }
        public string FullName { get; set; }
        public string Test { get; set; }
        
        //Табличні частини
        public NewDir_Ceny_TablePart Ceny_TablePart { get; set; }
        
    }
    
    /// <summary> 
    /// Новий довідник
    /// </summary>
    class NewDir_Pointer : DirectoryPointer
    {
        public NewDir_Pointer(object uid = null) : base(Config.Kernel, "newdir_v1_1")
        {
            if (uid != null && uid != DBNull.Value) base.Init(new UnigueID((Guid)uid), null);
        }

        public NewDir_Objest GetDirectoryObject()
        {
            NewDir_Objest NewDirObjestItem = new NewDir_Objest();
            NewDirObjestItem.Read(base.UnigueID);
            return NewDirObjestItem;
        }
    }
    
    /// <summary> 
    /// Новий довідник
    /// </summary>
    class NewDir_Select : DirectorySelect
    {
        public NewDir_Select() : base(Config.Kernel, "newdir_v1_1") { }
    
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
                Current = new NewDir_Pointer();
                Current.Init(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields);
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }

        public NewDir_Pointer Current { get; private set; }
    }
    
      
    /// <summary>
    /// Таблична частина Ціни
    /// </summary>
    class NewDir_Ceny_TablePart : DirectoryTablePart
    {
        public NewDir_Ceny_TablePart(NewDir_Objest owner) : base(Config.Kernel, "newdir_ceny_tablepart_v1_1",
             new string[] { "name", "cena" }) 
        {
            Owner = owner;
            Records = new List<NewDir_Ceny_TablePartRecord>();
        }
        
        public NewDir_Objest Owner { get; private set; }
        
        public List<NewDir_Ceny_TablePartRecord> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.FieldValueList.Clear();

            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                NewDir_Ceny_TablePartRecord record = new NewDir_Ceny_TablePartRecord();

                record.name = fieldValue["name"].ToString();
                record.cena = (fieldValue["cena"] != DBNull.Value) ? (decimal)fieldValue["cena"] : 0;
                
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

                foreach (NewDir_Ceny_TablePartRecord record in Records)
                {
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                    fieldValue.Add("name", record.name);
                    fieldValue.Add("cena", record.cena);
                    
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
    class NewDir_Ceny_TablePartRecord : DirectoryTablePartRecord
    {
        public NewDir_Ceny_TablePartRecord()
        {
            name = "";
            cena = 0;
            
        }
        
        public NewDir_Ceny_TablePartRecord(
            string _name = "", decimal _cena = 0)
        {
            name = _name;
            cena = _cena;
            
        }
        
        public string name { get; set; }
        public decimal cena { get; set; }
        
    }
      
    
    #endregion
    
}
  


/*
 *
 * Конфігурації "ConfTrade 1.1"
 * Автор Yurik
 * Дата конфігурації: 30.01.2020 06:59:12
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
    
    class Tovary_Objest : DirectoryObject
    {
        public Tovary_Objest() : base(Config.Kernel, "tovary_v1_1",
             new string[] { "name", "code", "description", "field1", "field2", "field3", "field4", "field5", "od2", "count", "num", "isupdate", "isupdate2", "date_add", "date_add3", "time_add", "datetime_add", "text_arr", "int_arr", "numeric_arr" }) 
        {
            name = "";
            code = "";
            description = "";
            field1 = "";
            field2 = "";
            field3 = "";
            field4 = "";
            field5 = "";
            od2 = new Od_Pointer();
            count = 0;
            num = 0;
            isupdate = false;
            isupdate2 = false;
            date_add = DateTime.MinValue;
            date_add3 = DateTime.MinValue;
            time_add = DateTime.MinValue.TimeOfDay;
            datetime_add = DateTime.MinValue;
            text_arr = new string[] { };
            int_arr = new int[] { };
            numeric_arr = new decimal[] { };
            
            //Табличні частини
            Ceny_TablePart = new Tovary_Ceny_TablePart(this);
            Od_List_TablePart = new Tovary_Od_List_TablePart(this);
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                name = base.FieldValue["name"].ToString();
                code = base.FieldValue["code"].ToString();
                description = base.FieldValue["description"].ToString();
                field1 = base.FieldValue["field1"].ToString();
                field2 = base.FieldValue["field2"].ToString();
                field3 = base.FieldValue["field3"].ToString();
                field4 = base.FieldValue["field4"].ToString();
                field5 = base.FieldValue["field5"].ToString();
                od2 = new Od_Pointer(base.FieldValue["od2"]);
                count = (int)base.FieldValue["count"];
                num = (base.FieldValue["num"] != DBNull.Value) ? (decimal)base.FieldValue["num"] : 0;
                isupdate = (bool)base.FieldValue["isupdate"];
                isupdate2 = (bool)base.FieldValue["isupdate2"];
                date_add = (base.FieldValue["date_add"] != DBNull.Value) ? DateTime.Parse(base.FieldValue["date_add"].ToString()) : DateTime.MinValue;
                date_add3 = (base.FieldValue["date_add3"] != DBNull.Value) ? DateTime.Parse(base.FieldValue["date_add3"].ToString()) : DateTime.MinValue;
                time_add = (base.FieldValue["time_add"] != DBNull.Value) ? TimeSpan.Parse(base.FieldValue["time_add"].ToString()) : DateTime.MinValue.TimeOfDay;
                datetime_add = (base.FieldValue["datetime_add"] != DBNull.Value) ? DateTime.Parse(base.FieldValue["datetime_add"].ToString()) : DateTime.MinValue;
                text_arr = (base.FieldValue["text_arr"] != DBNull.Value) ? (string[])base.FieldValue["text_arr"] : new string[] { };
                int_arr = (base.FieldValue["int_arr"] != DBNull.Value) ? (int[])base.FieldValue["int_arr"] : new int[] { };
                numeric_arr = (base.FieldValue["numeric_arr"] != DBNull.Value) ? (decimal[])base.FieldValue["numeric_arr"] : new decimal[] { };
                
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["name"] = name;
            base.FieldValue["code"] = code;
            base.FieldValue["description"] = description;
            base.FieldValue["field1"] = field1;
            base.FieldValue["field2"] = field2;
            base.FieldValue["field3"] = field3;
            base.FieldValue["field4"] = field4;
            base.FieldValue["field5"] = field5;
            base.FieldValue["od2"] = od2.UnigueID.UGuid;
            base.FieldValue["count"] = count;
            base.FieldValue["num"] = num;
            base.FieldValue["isupdate"] = isupdate;
            base.FieldValue["isupdate2"] = isupdate2;
            base.FieldValue["date_add"] = date_add;
            base.FieldValue["date_add3"] = date_add3;
            base.FieldValue["time_add"] = time_add;
            base.FieldValue["datetime_add"] = datetime_add;
            base.FieldValue["text_arr"] = text_arr;
            base.FieldValue["int_arr"] = int_arr;
            base.FieldValue["numeric_arr"] = numeric_arr;
            
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
        
        public string name { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public string field1 { get; set; }
        public string field2 { get; set; }
        public string field3 { get; set; }
        public string field4 { get; set; }
        public string field5 { get; set; }
        public Od_Pointer od2 { get; set; }
        public int count { get; set; }
        public decimal num { get; set; }
        public bool isupdate { get; set; }
        public bool isupdate2 { get; set; }
        public DateTime date_add { get; set; }
        public DateTime date_add3 { get; set; }
        public TimeSpan time_add { get; set; }
        public DateTime datetime_add { get; set; }
        public string[] text_arr { get; set; }
        public int[] int_arr { get; set; }
        public decimal[] numeric_arr { get; set; }
        
        //Табличні частини
        public Tovary_Ceny_TablePart Ceny_TablePart { get; set; }
        public Tovary_Od_List_TablePart Od_List_TablePart { get; set; }
        
    }
    
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
    /// [Ceny] 
    /// TablePart Test Info.
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

                foreach (Tovary_Ceny_TablePartRecord record in Records)
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
    
    class Tovary_Ceny_TablePartRecord : DirectoryTablePartRecord
    {
        public Tovary_Ceny_TablePartRecord()
        {
            name = "";
            cena = 0;
            
        }
        
        public Tovary_Ceny_TablePartRecord(
            string _name = "", decimal _cena = 0)
        {
            name = _name;
            cena = _cena;
            
        }
        
        public string name { get; set; }
        public decimal cena { get; set; }
        
    }
      
    /// <summary>
    /// [Od_List] 
    /// Таблична частина одиниць вимірів.
    /// </summary>
    class Tovary_Od_List_TablePart : DirectoryTablePart
    {
        public Tovary_Od_List_TablePart(Tovary_Objest owner) : base(Config.Kernel, "tovary_od_tablepart_v1_1",
             new string[] { "Od_Pointer", "Name", "FullName" }) 
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

                record.Od_Pointer = new Od_Pointer(fieldValue["Od_Pointer"]);
                record.Name = fieldValue["Name"].ToString();
                record.FullName = fieldValue["FullName"].ToString();
                
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

                    fieldValue.Add("Od_Pointer", record.Od_Pointer.UnigueID.UGuid);
                    fieldValue.Add("Name", record.Name);
                    fieldValue.Add("FullName", record.FullName);
                    
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
      
    #endregion
    
    #region DIRECTORY "Od"
    
    class Od_Objest : DirectoryObject
    {
        public Od_Objest() : base(Config.Kernel, "od_v1_1",
             new string[] { "Name", "FullName", "Test" }) 
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
                Name = base.FieldValue["Name"].ToString();
                FullName = base.FieldValue["FullName"].ToString();
                Test = base.FieldValue["Test"].ToString();
                
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["Name"] = Name;
            base.FieldValue["FullName"] = FullName;
            base.FieldValue["Test"] = Test;
            
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
    
    class NewDir_Objest : DirectoryObject
    {
        public NewDir_Objest() : base(Config.Kernel, "newdir_v1_1",
             new string[] { "Name", "FullName", "Test" }) 
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
                Name = base.FieldValue["Name"].ToString();
                FullName = base.FieldValue["FullName"].ToString();
                Test = base.FieldValue["Test"].ToString();
                
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["Name"] = Name;
            base.FieldValue["FullName"] = FullName;
            base.FieldValue["Test"] = Test;
            
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
    /// [Ceny] 
    /// TablePart Test Info.
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
  
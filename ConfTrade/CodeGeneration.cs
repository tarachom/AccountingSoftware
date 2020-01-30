

/*
 *
 * Конфігурації "ConfTrade 1.1"
 * Автор Yurik
 * Дата конфігурації: 30.01.2020 03:08:54
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
             new string[] { "Name", "FullName" }) 
        {
            Name = "";
            FullName = "";
            
            //Табличні частини
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Name = base.FieldValue["Name"].ToString();
                FullName = base.FieldValue["FullName"].ToString();
                
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["Name"] = Name;
            base.FieldValue["FullName"] = FullName;
            
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
    
    #region DIRECTORY "Od2"
    
    class Od2_Objest : DirectoryObject
    {
        public Od2_Objest() : base(Config.Kernel, "od_v1_1",
             new string[] { "Name", "FullName" }) 
        {
            Name = "";
            FullName = "";
            
            //Табличні частини
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Name = base.FieldValue["Name"].ToString();
                FullName = base.FieldValue["FullName"].ToString();
                
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["Name"] = Name;
            base.FieldValue["FullName"] = FullName;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public Od2_Pointer GetDirectoryPointer()
        {
            Od2_Pointer directoryPointer = new Od2_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public string Name { get; set; }
        public string FullName { get; set; }
        
        //Табличні частини
        
    }
    
    class Od2_Pointer : DirectoryPointer
    {
        public Od2_Pointer(object uid = null) : base(Config.Kernel, "od_v1_1")
        {
            if (uid != null && uid != DBNull.Value) base.Init(new UnigueID((Guid)uid), null);
        }

        public Od2_Objest GetDirectoryObject()
        {
            Od2_Objest Od2ObjestItem = new Od2_Objest();
            Od2ObjestItem.Read(base.UnigueID);
            return Od2ObjestItem;
        }
    }
    
    class Od2_Select : DirectorySelect
    {
        public Od2_Select() : base(Config.Kernel, "od_v1_1") { }
    
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
                Current = new Od2_Pointer();
                Current.Init(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields);
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }

        public Od2_Pointer Current { get; private set; }
    }
    
      
    #endregion
    
    #region DIRECTORY "Od3"
    
    class Od3_Objest : DirectoryObject
    {
        public Od3_Objest() : base(Config.Kernel, "od_v1_1",
             new string[] { "Name", "FullName" }) 
        {
            Name = "";
            FullName = "";
            
            //Табличні частини
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Name = base.FieldValue["Name"].ToString();
                FullName = base.FieldValue["FullName"].ToString();
                
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["Name"] = Name;
            base.FieldValue["FullName"] = FullName;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public Od3_Pointer GetDirectoryPointer()
        {
            Od3_Pointer directoryPointer = new Od3_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public string Name { get; set; }
        public string FullName { get; set; }
        
        //Табличні частини
        
    }
    
    class Od3_Pointer : DirectoryPointer
    {
        public Od3_Pointer(object uid = null) : base(Config.Kernel, "od_v1_1")
        {
            if (uid != null && uid != DBNull.Value) base.Init(new UnigueID((Guid)uid), null);
        }

        public Od3_Objest GetDirectoryObject()
        {
            Od3_Objest Od3ObjestItem = new Od3_Objest();
            Od3ObjestItem.Read(base.UnigueID);
            return Od3ObjestItem;
        }
    }
    
    class Od3_Select : DirectorySelect
    {
        public Od3_Select() : base(Config.Kernel, "od_v1_1") { }
    
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
                Current = new Od3_Pointer();
                Current.Init(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields);
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }

        public Od3_Pointer Current { get; private set; }
    }
    
      
    #endregion
    
    #region DIRECTORY "Od4"
    
    class Od4_Objest : DirectoryObject
    {
        public Od4_Objest() : base(Config.Kernel, "od_v1_1",
             new string[] { "Name", "FullName" }) 
        {
            Name = "";
            FullName = "";
            
            //Табличні частини
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Name = base.FieldValue["Name"].ToString();
                FullName = base.FieldValue["FullName"].ToString();
                
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["Name"] = Name;
            base.FieldValue["FullName"] = FullName;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public Od4_Pointer GetDirectoryPointer()
        {
            Od4_Pointer directoryPointer = new Od4_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public string Name { get; set; }
        public string FullName { get; set; }
        
        //Табличні частини
        
    }
    
    class Od4_Pointer : DirectoryPointer
    {
        public Od4_Pointer(object uid = null) : base(Config.Kernel, "od_v1_1")
        {
            if (uid != null && uid != DBNull.Value) base.Init(new UnigueID((Guid)uid), null);
        }

        public Od4_Objest GetDirectoryObject()
        {
            Od4_Objest Od4ObjestItem = new Od4_Objest();
            Od4ObjestItem.Read(base.UnigueID);
            return Od4ObjestItem;
        }
    }
    
    class Od4_Select : DirectorySelect
    {
        public Od4_Select() : base(Config.Kernel, "od_v1_1") { }
    
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
                Current = new Od4_Pointer();
                Current.Init(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields);
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }

        public Od4_Pointer Current { get; private set; }
    }
    
      
    #endregion
    
    #region DIRECTORY "Od5"
    
    class Od5_Objest : DirectoryObject
    {
        public Od5_Objest() : base(Config.Kernel, "od_v1_1",
             new string[] { "Name", "FullName" }) 
        {
            Name = "";
            FullName = "";
            
            //Табличні частини
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Name = base.FieldValue["Name"].ToString();
                FullName = base.FieldValue["FullName"].ToString();
                
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["Name"] = Name;
            base.FieldValue["FullName"] = FullName;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public Od5_Pointer GetDirectoryPointer()
        {
            Od5_Pointer directoryPointer = new Od5_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public string Name { get; set; }
        public string FullName { get; set; }
        
        //Табличні частини
        
    }
    
    class Od5_Pointer : DirectoryPointer
    {
        public Od5_Pointer(object uid = null) : base(Config.Kernel, "od_v1_1")
        {
            if (uid != null && uid != DBNull.Value) base.Init(new UnigueID((Guid)uid), null);
        }

        public Od5_Objest GetDirectoryObject()
        {
            Od5_Objest Od5ObjestItem = new Od5_Objest();
            Od5ObjestItem.Read(base.UnigueID);
            return Od5ObjestItem;
        }
    }
    
    class Od5_Select : DirectorySelect
    {
        public Od5_Select() : base(Config.Kernel, "od_v1_1") { }
    
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
                Current = new Od5_Pointer();
                Current.Init(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields);
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }

        public Od5_Pointer Current { get; private set; }
    }
    
      
    #endregion
    
    #region DIRECTORY "Od6"
    
    class Od6_Objest : DirectoryObject
    {
        public Od6_Objest() : base(Config.Kernel, "od_v1_1",
             new string[] { "Name", "FullName" }) 
        {
            Name = "";
            FullName = "";
            
            //Табличні частини
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Name = base.FieldValue["Name"].ToString();
                FullName = base.FieldValue["FullName"].ToString();
                
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["Name"] = Name;
            base.FieldValue["FullName"] = FullName;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public Od6_Pointer GetDirectoryPointer()
        {
            Od6_Pointer directoryPointer = new Od6_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public string Name { get; set; }
        public string FullName { get; set; }
        
        //Табличні частини
        
    }
    
    class Od6_Pointer : DirectoryPointer
    {
        public Od6_Pointer(object uid = null) : base(Config.Kernel, "od_v1_1")
        {
            if (uid != null && uid != DBNull.Value) base.Init(new UnigueID((Guid)uid), null);
        }

        public Od6_Objest GetDirectoryObject()
        {
            Od6_Objest Od6ObjestItem = new Od6_Objest();
            Od6ObjestItem.Read(base.UnigueID);
            return Od6ObjestItem;
        }
    }
    
    class Od6_Select : DirectorySelect
    {
        public Od6_Select() : base(Config.Kernel, "od_v1_1") { }
    
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
                Current = new Od6_Pointer();
                Current.Init(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields);
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }

        public Od6_Pointer Current { get; private set; }
    }
    
      
    #endregion
    
    #region DIRECTORY "Od7"
    
    class Od7_Objest : DirectoryObject
    {
        public Od7_Objest() : base(Config.Kernel, "od_v1_1",
             new string[] { "Name", "FullName" }) 
        {
            Name = "";
            FullName = "";
            
            //Табличні частини
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Name = base.FieldValue["Name"].ToString();
                FullName = base.FieldValue["FullName"].ToString();
                
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["Name"] = Name;
            base.FieldValue["FullName"] = FullName;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public Od7_Pointer GetDirectoryPointer()
        {
            Od7_Pointer directoryPointer = new Od7_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public string Name { get; set; }
        public string FullName { get; set; }
        
        //Табличні частини
        
    }
    
    class Od7_Pointer : DirectoryPointer
    {
        public Od7_Pointer(object uid = null) : base(Config.Kernel, "od_v1_1")
        {
            if (uid != null && uid != DBNull.Value) base.Init(new UnigueID((Guid)uid), null);
        }

        public Od7_Objest GetDirectoryObject()
        {
            Od7_Objest Od7ObjestItem = new Od7_Objest();
            Od7ObjestItem.Read(base.UnigueID);
            return Od7ObjestItem;
        }
    }
    
    class Od7_Select : DirectorySelect
    {
        public Od7_Select() : base(Config.Kernel, "od_v1_1") { }
    
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
                Current = new Od7_Pointer();
                Current.Init(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields);
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }

        public Od7_Pointer Current { get; private set; }
    }
    
      
    #endregion
    
    #region DIRECTORY "Od8"
    
    class Od8_Objest : DirectoryObject
    {
        public Od8_Objest() : base(Config.Kernel, "od_v1_1",
             new string[] { "Name", "FullName" }) 
        {
            Name = "";
            FullName = "";
            
            //Табличні частини
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Name = base.FieldValue["Name"].ToString();
                FullName = base.FieldValue["FullName"].ToString();
                
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["Name"] = Name;
            base.FieldValue["FullName"] = FullName;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public Od8_Pointer GetDirectoryPointer()
        {
            Od8_Pointer directoryPointer = new Od8_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public string Name { get; set; }
        public string FullName { get; set; }
        
        //Табличні частини
        
    }
    
    class Od8_Pointer : DirectoryPointer
    {
        public Od8_Pointer(object uid = null) : base(Config.Kernel, "od_v1_1")
        {
            if (uid != null && uid != DBNull.Value) base.Init(new UnigueID((Guid)uid), null);
        }

        public Od8_Objest GetDirectoryObject()
        {
            Od8_Objest Od8ObjestItem = new Od8_Objest();
            Od8ObjestItem.Read(base.UnigueID);
            return Od8ObjestItem;
        }
    }
    
    class Od8_Select : DirectorySelect
    {
        public Od8_Select() : base(Config.Kernel, "od_v1_1") { }
    
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
                Current = new Od8_Pointer();
                Current.Init(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields);
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }

        public Od8_Pointer Current { get; private set; }
    }
    
      
    #endregion
    
    #region DIRECTORY "Od9"
    
    class Od9_Objest : DirectoryObject
    {
        public Od9_Objest() : base(Config.Kernel, "od_v1_1",
             new string[] { "Name", "FullName" }) 
        {
            Name = "";
            FullName = "";
            
            //Табличні частини
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Name = base.FieldValue["Name"].ToString();
                FullName = base.FieldValue["FullName"].ToString();
                
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["Name"] = Name;
            base.FieldValue["FullName"] = FullName;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public Od9_Pointer GetDirectoryPointer()
        {
            Od9_Pointer directoryPointer = new Od9_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public string Name { get; set; }
        public string FullName { get; set; }
        
        //Табличні частини
        
    }
    
    class Od9_Pointer : DirectoryPointer
    {
        public Od9_Pointer(object uid = null) : base(Config.Kernel, "od_v1_1")
        {
            if (uid != null && uid != DBNull.Value) base.Init(new UnigueID((Guid)uid), null);
        }

        public Od9_Objest GetDirectoryObject()
        {
            Od9_Objest Od9ObjestItem = new Od9_Objest();
            Od9ObjestItem.Read(base.UnigueID);
            return Od9ObjestItem;
        }
    }
    
    class Od9_Select : DirectorySelect
    {
        public Od9_Select() : base(Config.Kernel, "od_v1_1") { }
    
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
                Current = new Od9_Pointer();
                Current.Init(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields);
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }

        public Od9_Pointer Current { get; private set; }
    }
    
      
    #endregion
    
    #region DIRECTORY "Od10"
    
    class Od10_Objest : DirectoryObject
    {
        public Od10_Objest() : base(Config.Kernel, "od_v1_1",
             new string[] { "Name", "FullName" }) 
        {
            Name = "";
            FullName = "";
            
            //Табличні частини
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Name = base.FieldValue["Name"].ToString();
                FullName = base.FieldValue["FullName"].ToString();
                
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["Name"] = Name;
            base.FieldValue["FullName"] = FullName;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public Od10_Pointer GetDirectoryPointer()
        {
            Od10_Pointer directoryPointer = new Od10_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public string Name { get; set; }
        public string FullName { get; set; }
        
        //Табличні частини
        
    }
    
    class Od10_Pointer : DirectoryPointer
    {
        public Od10_Pointer(object uid = null) : base(Config.Kernel, "od_v1_1")
        {
            if (uid != null && uid != DBNull.Value) base.Init(new UnigueID((Guid)uid), null);
        }

        public Od10_Objest GetDirectoryObject()
        {
            Od10_Objest Od10ObjestItem = new Od10_Objest();
            Od10ObjestItem.Read(base.UnigueID);
            return Od10ObjestItem;
        }
    }
    
    class Od10_Select : DirectorySelect
    {
        public Od10_Select() : base(Config.Kernel, "od_v1_1") { }
    
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
                Current = new Od10_Pointer();
                Current.Init(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields);
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }

        public Od10_Pointer Current { get; private set; }
    }
    
      
    #endregion
    
    #region DIRECTORY "Od11"
    
    class Od11_Objest : DirectoryObject
    {
        public Od11_Objest() : base(Config.Kernel, "od_v1_1",
             new string[] { "Name", "FullName" }) 
        {
            Name = "";
            FullName = "";
            
            //Табличні частини
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Name = base.FieldValue["Name"].ToString();
                FullName = base.FieldValue["FullName"].ToString();
                
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["Name"] = Name;
            base.FieldValue["FullName"] = FullName;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public Od11_Pointer GetDirectoryPointer()
        {
            Od11_Pointer directoryPointer = new Od11_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public string Name { get; set; }
        public string FullName { get; set; }
        
        //Табличні частини
        
    }
    
    class Od11_Pointer : DirectoryPointer
    {
        public Od11_Pointer(object uid = null) : base(Config.Kernel, "od_v1_1")
        {
            if (uid != null && uid != DBNull.Value) base.Init(new UnigueID((Guid)uid), null);
        }

        public Od11_Objest GetDirectoryObject()
        {
            Od11_Objest Od11ObjestItem = new Od11_Objest();
            Od11ObjestItem.Read(base.UnigueID);
            return Od11ObjestItem;
        }
    }
    
    class Od11_Select : DirectorySelect
    {
        public Od11_Select() : base(Config.Kernel, "od_v1_1") { }
    
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
                Current = new Od11_Pointer();
                Current.Init(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields);
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }

        public Od11_Pointer Current { get; private set; }
    }
    
      
    #endregion
    
    #region DIRECTORY "Od12"
    
    class Od12_Objest : DirectoryObject
    {
        public Od12_Objest() : base(Config.Kernel, "od_v1_1",
             new string[] { "Name", "FullName" }) 
        {
            Name = "";
            FullName = "";
            
            //Табличні частини
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Name = base.FieldValue["Name"].ToString();
                FullName = base.FieldValue["FullName"].ToString();
                
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["Name"] = Name;
            base.FieldValue["FullName"] = FullName;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public Od12_Pointer GetDirectoryPointer()
        {
            Od12_Pointer directoryPointer = new Od12_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public string Name { get; set; }
        public string FullName { get; set; }
        
        //Табличні частини
        
    }
    
    class Od12_Pointer : DirectoryPointer
    {
        public Od12_Pointer(object uid = null) : base(Config.Kernel, "od_v1_1")
        {
            if (uid != null && uid != DBNull.Value) base.Init(new UnigueID((Guid)uid), null);
        }

        public Od12_Objest GetDirectoryObject()
        {
            Od12_Objest Od12ObjestItem = new Od12_Objest();
            Od12ObjestItem.Read(base.UnigueID);
            return Od12ObjestItem;
        }
    }
    
    class Od12_Select : DirectorySelect
    {
        public Od12_Select() : base(Config.Kernel, "od_v1_1") { }
    
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
                Current = new Od12_Pointer();
                Current.Init(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields);
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }

        public Od12_Pointer Current { get; private set; }
    }
    
      
    #endregion
    
    #region DIRECTORY "Od13"
    
    class Od13_Objest : DirectoryObject
    {
        public Od13_Objest() : base(Config.Kernel, "od_v1_1",
             new string[] { "Name", "FullName" }) 
        {
            Name = "";
            FullName = "";
            
            //Табличні частини
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Name = base.FieldValue["Name"].ToString();
                FullName = base.FieldValue["FullName"].ToString();
                
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["Name"] = Name;
            base.FieldValue["FullName"] = FullName;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public Od13_Pointer GetDirectoryPointer()
        {
            Od13_Pointer directoryPointer = new Od13_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public string Name { get; set; }
        public string FullName { get; set; }
        
        //Табличні частини
        
    }
    
    class Od13_Pointer : DirectoryPointer
    {
        public Od13_Pointer(object uid = null) : base(Config.Kernel, "od_v1_1")
        {
            if (uid != null && uid != DBNull.Value) base.Init(new UnigueID((Guid)uid), null);
        }

        public Od13_Objest GetDirectoryObject()
        {
            Od13_Objest Od13ObjestItem = new Od13_Objest();
            Od13ObjestItem.Read(base.UnigueID);
            return Od13ObjestItem;
        }
    }
    
    class Od13_Select : DirectorySelect
    {
        public Od13_Select() : base(Config.Kernel, "od_v1_1") { }
    
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
                Current = new Od13_Pointer();
                Current.Init(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields);
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }

        public Od13_Pointer Current { get; private set; }
    }
    
      
    #endregion
    
    #region DIRECTORY "Od14"
    
    class Od14_Objest : DirectoryObject
    {
        public Od14_Objest() : base(Config.Kernel, "od_v1_1",
             new string[] { "Name", "FullName" }) 
        {
            Name = "";
            FullName = "";
            
            //Табличні частини
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Name = base.FieldValue["Name"].ToString();
                FullName = base.FieldValue["FullName"].ToString();
                
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["Name"] = Name;
            base.FieldValue["FullName"] = FullName;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public Od14_Pointer GetDirectoryPointer()
        {
            Od14_Pointer directoryPointer = new Od14_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public string Name { get; set; }
        public string FullName { get; set; }
        
        //Табличні частини
        
    }
    
    class Od14_Pointer : DirectoryPointer
    {
        public Od14_Pointer(object uid = null) : base(Config.Kernel, "od_v1_1")
        {
            if (uid != null && uid != DBNull.Value) base.Init(new UnigueID((Guid)uid), null);
        }

        public Od14_Objest GetDirectoryObject()
        {
            Od14_Objest Od14ObjestItem = new Od14_Objest();
            Od14ObjestItem.Read(base.UnigueID);
            return Od14ObjestItem;
        }
    }
    
    class Od14_Select : DirectorySelect
    {
        public Od14_Select() : base(Config.Kernel, "od_v1_1") { }
    
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
                Current = new Od14_Pointer();
                Current.Init(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields);
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }

        public Od14_Pointer Current { get; private set; }
    }
    
      
    #endregion
    
    #region DIRECTORY "Od15"
    
    class Od15_Objest : DirectoryObject
    {
        public Od15_Objest() : base(Config.Kernel, "od_v1_1",
             new string[] { "Name", "FullName" }) 
        {
            Name = "";
            FullName = "";
            
            //Табличні частини
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Name = base.FieldValue["Name"].ToString();
                FullName = base.FieldValue["FullName"].ToString();
                
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["Name"] = Name;
            base.FieldValue["FullName"] = FullName;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public Od15_Pointer GetDirectoryPointer()
        {
            Od15_Pointer directoryPointer = new Od15_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public string Name { get; set; }
        public string FullName { get; set; }
        
        //Табличні частини
        
    }
    
    class Od15_Pointer : DirectoryPointer
    {
        public Od15_Pointer(object uid = null) : base(Config.Kernel, "od_v1_1")
        {
            if (uid != null && uid != DBNull.Value) base.Init(new UnigueID((Guid)uid), null);
        }

        public Od15_Objest GetDirectoryObject()
        {
            Od15_Objest Od15ObjestItem = new Od15_Objest();
            Od15ObjestItem.Read(base.UnigueID);
            return Od15ObjestItem;
        }
    }
    
    class Od15_Select : DirectorySelect
    {
        public Od15_Select() : base(Config.Kernel, "od_v1_1") { }
    
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
                Current = new Od15_Pointer();
                Current.Init(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields);
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }

        public Od15_Pointer Current { get; private set; }
    }
    
      
    #endregion
    
    #region DIRECTORY "Od16"
    
    class Od16_Objest : DirectoryObject
    {
        public Od16_Objest() : base(Config.Kernel, "od_v1_1",
             new string[] { "Name", "FullName" }) 
        {
            Name = "";
            FullName = "";
            
            //Табличні частини
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Name = base.FieldValue["Name"].ToString();
                FullName = base.FieldValue["FullName"].ToString();
                
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["Name"] = Name;
            base.FieldValue["FullName"] = FullName;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public Od16_Pointer GetDirectoryPointer()
        {
            Od16_Pointer directoryPointer = new Od16_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public string Name { get; set; }
        public string FullName { get; set; }
        
        //Табличні частини
        
    }
    
    class Od16_Pointer : DirectoryPointer
    {
        public Od16_Pointer(object uid = null) : base(Config.Kernel, "od_v1_1")
        {
            if (uid != null && uid != DBNull.Value) base.Init(new UnigueID((Guid)uid), null);
        }

        public Od16_Objest GetDirectoryObject()
        {
            Od16_Objest Od16ObjestItem = new Od16_Objest();
            Od16ObjestItem.Read(base.UnigueID);
            return Od16ObjestItem;
        }
    }
    
    class Od16_Select : DirectorySelect
    {
        public Od16_Select() : base(Config.Kernel, "od_v1_1") { }
    
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
                Current = new Od16_Pointer();
                Current.Init(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields);
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }

        public Od16_Pointer Current { get; private set; }
    }
    
      
    #endregion
    
    #region DIRECTORY "Od17"
    
    class Od17_Objest : DirectoryObject
    {
        public Od17_Objest() : base(Config.Kernel, "od_v1_1",
             new string[] { "Name", "FullName" }) 
        {
            Name = "";
            FullName = "";
            
            //Табличні частини
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Name = base.FieldValue["Name"].ToString();
                FullName = base.FieldValue["FullName"].ToString();
                
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["Name"] = Name;
            base.FieldValue["FullName"] = FullName;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public Od17_Pointer GetDirectoryPointer()
        {
            Od17_Pointer directoryPointer = new Od17_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public string Name { get; set; }
        public string FullName { get; set; }
        
        //Табличні частини
        
    }
    
    class Od17_Pointer : DirectoryPointer
    {
        public Od17_Pointer(object uid = null) : base(Config.Kernel, "od_v1_1")
        {
            if (uid != null && uid != DBNull.Value) base.Init(new UnigueID((Guid)uid), null);
        }

        public Od17_Objest GetDirectoryObject()
        {
            Od17_Objest Od17ObjestItem = new Od17_Objest();
            Od17ObjestItem.Read(base.UnigueID);
            return Od17ObjestItem;
        }
    }
    
    class Od17_Select : DirectorySelect
    {
        public Od17_Select() : base(Config.Kernel, "od_v1_1") { }
    
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
                Current = new Od17_Pointer();
                Current.Init(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields);
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }

        public Od17_Pointer Current { get; private set; }
    }
    
      
    #endregion
    
    #region DIRECTORY "Od18"
    
    class Od18_Objest : DirectoryObject
    {
        public Od18_Objest() : base(Config.Kernel, "od_v1_1",
             new string[] { "Name", "FullName" }) 
        {
            Name = "";
            FullName = "";
            
            //Табличні частини
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Name = base.FieldValue["Name"].ToString();
                FullName = base.FieldValue["FullName"].ToString();
                
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["Name"] = Name;
            base.FieldValue["FullName"] = FullName;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public Od18_Pointer GetDirectoryPointer()
        {
            Od18_Pointer directoryPointer = new Od18_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public string Name { get; set; }
        public string FullName { get; set; }
        
        //Табличні частини
        
    }
    
    class Od18_Pointer : DirectoryPointer
    {
        public Od18_Pointer(object uid = null) : base(Config.Kernel, "od_v1_1")
        {
            if (uid != null && uid != DBNull.Value) base.Init(new UnigueID((Guid)uid), null);
        }

        public Od18_Objest GetDirectoryObject()
        {
            Od18_Objest Od18ObjestItem = new Od18_Objest();
            Od18ObjestItem.Read(base.UnigueID);
            return Od18ObjestItem;
        }
    }
    
    class Od18_Select : DirectorySelect
    {
        public Od18_Select() : base(Config.Kernel, "od_v1_1") { }
    
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
                Current = new Od18_Pointer();
                Current.Init(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields);
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }

        public Od18_Pointer Current { get; private set; }
    }
    
      
    #endregion
    
    #region DIRECTORY "Od19"
    
    class Od19_Objest : DirectoryObject
    {
        public Od19_Objest() : base(Config.Kernel, "od_v1_1",
             new string[] { "Name", "FullName" }) 
        {
            Name = "";
            FullName = "";
            
            //Табличні частини
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Name = base.FieldValue["Name"].ToString();
                FullName = base.FieldValue["FullName"].ToString();
                
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["Name"] = Name;
            base.FieldValue["FullName"] = FullName;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public Od19_Pointer GetDirectoryPointer()
        {
            Od19_Pointer directoryPointer = new Od19_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public string Name { get; set; }
        public string FullName { get; set; }
        
        //Табличні частини
        
    }
    
    class Od19_Pointer : DirectoryPointer
    {
        public Od19_Pointer(object uid = null) : base(Config.Kernel, "od_v1_1")
        {
            if (uid != null && uid != DBNull.Value) base.Init(new UnigueID((Guid)uid), null);
        }

        public Od19_Objest GetDirectoryObject()
        {
            Od19_Objest Od19ObjestItem = new Od19_Objest();
            Od19ObjestItem.Read(base.UnigueID);
            return Od19ObjestItem;
        }
    }
    
    class Od19_Select : DirectorySelect
    {
        public Od19_Select() : base(Config.Kernel, "od_v1_1") { }
    
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
                Current = new Od19_Pointer();
                Current.Init(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields);
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }

        public Od19_Pointer Current { get; private set; }
    }
    
      
    #endregion
    
    #region DIRECTORY "Od20"
    
    class Od20_Objest : DirectoryObject
    {
        public Od20_Objest() : base(Config.Kernel, "od_v1_1",
             new string[] { "Name", "FullName" }) 
        {
            Name = "";
            FullName = "";
            
            //Табличні частини
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Name = base.FieldValue["Name"].ToString();
                FullName = base.FieldValue["FullName"].ToString();
                
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["Name"] = Name;
            base.FieldValue["FullName"] = FullName;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public Od20_Pointer GetDirectoryPointer()
        {
            Od20_Pointer directoryPointer = new Od20_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public string Name { get; set; }
        public string FullName { get; set; }
        
        //Табличні частини
        
    }
    
    class Od20_Pointer : DirectoryPointer
    {
        public Od20_Pointer(object uid = null) : base(Config.Kernel, "od_v1_1")
        {
            if (uid != null && uid != DBNull.Value) base.Init(new UnigueID((Guid)uid), null);
        }

        public Od20_Objest GetDirectoryObject()
        {
            Od20_Objest Od20ObjestItem = new Od20_Objest();
            Od20ObjestItem.Read(base.UnigueID);
            return Od20ObjestItem;
        }
    }
    
    class Od20_Select : DirectorySelect
    {
        public Od20_Select() : base(Config.Kernel, "od_v1_1") { }
    
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
                Current = new Od20_Pointer();
                Current.Init(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields);
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }

        public Od20_Pointer Current { get; private set; }
    }
    
      
    #endregion
    
}
  
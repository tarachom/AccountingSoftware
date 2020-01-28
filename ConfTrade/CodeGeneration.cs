

/*
 *
 * Конфігурації "ConfTrade 1.1"
 * Автор Yurik
 *
 */

using System;
using AccountingSoftware;

namespace ConfTrade_v1_1
{
    static class Config
    {
        public static Kernel Kernel { get; set; }
    }
    
    /*******************************************************[ Tovary ]****************************************************/

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
            
        }
        
        public void Read(UnigueID uid)
        {
            BaseRead(uid);
            
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
            num = (decimal)base.FieldValue["num"];
            isupdate = (bool)base.FieldValue["isupdate"];
            isupdate2 = (bool)base.FieldValue["isupdate2"];
            date_add = (base.FieldValue["date_add"] != DBNull.Value) ? DateTime.Parse(base.FieldValue["date_add"].ToString()) : DateTime.MinValue;
            date_add3 = (base.FieldValue["date_add3"] != DBNull.Value) ? DateTime.Parse(base.FieldValue["date_add3"].ToString()) : DateTime.MinValue;
            time_add = (base.FieldValue["time_add"] != DBNull.Value) ? TimeSpan.Parse(base.FieldValue["time_add"].ToString()) : DateTime.MinValue.TimeOfDay;
            datetime_add = (base.FieldValue["datetime_add"] != DBNull.Value) ? DateTime.Parse(base.FieldValue["datetime_add"].ToString()) : DateTime.MinValue;
            text_arr = (base.FieldValue["text_arr"] != DBNull.Value) ? (string[])base.FieldValue["text_arr"] : new string[] { };
            int_arr = (base.FieldValue["int_arr"] != DBNull.Value) ? (int[])base.FieldValue["int_arr"] : new int[] { };
            numeric_arr = (base.FieldValue["numeric_arr"] != DBNull.Value) ? (decimal[])base.FieldValue["numeric_arr"] : new decimal[] { };
            
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
        
        
        public Tovary_Pointer GetDirectoryPointer()
        {
            Tovary_Pointer directoryPointer = new Tovary_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
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
    
        public void Select() 
        { 
            base.BaseSelect();
        }
        
        public bool MoveNext()
        {
            Current = null;

            if (MoveToPosition())
            {
                Current = new Tovary_Pointer();
                Current.Init(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields);

                return true;
            }
            else
                return false;
        }

        public Tovary_Pointer Current { get; private set; }
    }
    
    /*******************************************************[ Od ]****************************************************/

    class Od_Objest : DirectoryObject
    {
        public Od_Objest() : base(Config.Kernel, "od_v1_1",
             new string[] { "Name" }) 
        {
            Name = "";
            
        }
        
        public void Read(UnigueID uid)
        {
            BaseRead(uid);
            
            Name = base.FieldValue["Name"].ToString();
            
        }
        
        public void Save()
        {
            base.FieldValue["Name"] = Name;
            
            BaseSave();
        }
        
        public string Name { get; set; }
        
        
        public Od_Pointer GetDirectoryPointer()
        {
            Od_Pointer directoryPointer = new Od_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
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
    
        public void Select() 
        { 
            base.BaseSelect();
        }
        
        public bool MoveNext()
        {
            Current = null;

            if (MoveToPosition())
            {
                Current = new Od_Pointer();
                Current.Init(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields);

                return true;
            }
            else
                return false;
        }

        public Od_Pointer Current { get; private set; }
    }
    
}
  
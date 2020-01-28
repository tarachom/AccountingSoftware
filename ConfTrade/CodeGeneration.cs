

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
             new string[] { "name", "code", "description", "field1", "field2", "field3", "field4", "field5", "od2", "count", "num", "isupdate", "isupdate2" }) 
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
            
        }
        
        public void Init(UnigueID uid)
        {
            BaseInit(uid);
            
            name = base.Fields["name"].ToString();
            code = base.Fields["code"].ToString();
            description = base.Fields["description"].ToString();
            field1 = base.Fields["field1"].ToString();
            field2 = base.Fields["field2"].ToString();
            field3 = base.Fields["field3"].ToString();
            field4 = base.Fields["field4"].ToString();
            field5 = base.Fields["field5"].ToString();
            od2 = new Od_Pointer(base.Fields["od2"]);
            count = (int)base.Fields["count"];
            num = (decimal)base.Fields["num"];
            isupdate = (bool)base.Fields["isupdate"];
            isupdate2 = (bool)base.Fields["isupdate2"];
            
        }
        
        public void Save()
        {
            base.Fields["name"] = name;
            base.Fields["code"] = code;
            base.Fields["description"] = description;
            base.Fields["field1"] = field1;
            base.Fields["field2"] = field2;
            base.Fields["field3"] = field3;
            base.Fields["field4"] = field4;
            base.Fields["field5"] = field5;
            base.Fields["od2"] = od2.UnigueID.UGuid;
            base.Fields["count"] = count;
            base.Fields["num"] = num;
            base.Fields["isupdate"] = isupdate;
            base.Fields["isupdate2"] = isupdate2;
            
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
            TovaryObjestItem.Init(base.UnigueID);
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
        
        public void Init(UnigueID uid)
        {
            BaseInit(uid);
            
            Name = base.Fields["Name"].ToString();
            
        }
        
        public void Save()
        {
            base.Fields["Name"] = Name;
            
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
            OdObjestItem.Init(base.UnigueID);
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
  
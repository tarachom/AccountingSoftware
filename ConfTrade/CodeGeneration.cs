

/*
 *
 * Конфігурації "ConfTrade 1.1"
 * Автор Yurik
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
    
    /*******************************************************[ Tovary ]****************************************************/

    class Tovary_Objest : DirectoryObject
    {
        public Tovary_Objest() : base(Config.Kernel, "tovary_v1_1",
              new string[] { "name", "code", "description", "field1", "field2", "field3", "field4", "field5", "od" }) 
        {
            name = "";
            code = "";
            description = "";
            field1 = "";
            field2 = "";
            field3 = "";
            field4 = "";
            field5 = "";
            od = "";
            
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
            od = base.Fields["od"].ToString();
            
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
            base.Fields["od"] = od;
      
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
        
        public string od { get; set; }
        
    }

    class Tovary_Pointer : DirectoryPointer
    {
        public Tovary_Pointer() : base(Config.Kernel, "tovary_v1_1") { }

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
        
    }

    class Od_Pointer : DirectoryPointer
    {
        public Od_Pointer() : base(Config.Kernel, "od_v1_1") { }

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
  
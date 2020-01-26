

/*
 * Автоматично згенерований код.
 *
 * Конфігурації "ConfTrade 1.1"
 * Автор Yurik
 *
 */

using System;
using System.Collections.Generic;
using AccountingSoftware;

namespace AccountingSoftware.Conf
{
    static class Config
    {
        public static Kernel Kernel { get; set; }
    }
    
    /*******************************************************[ Tovary ]****************************************************/

    class Tovary_Objest : DirectoryObject
    {
        public Tovary_Objest() { }    
        
        public string name { get; set; }
        
        public string code { get; set; }
        
        public string description { get; set; }
        
        public string Field1 { get; set; }
        
        public string Field2 { get; set; }
        
        public string Field3 { get; set; }
        
        public string Field4 { get; set; }
        
        public string Field5 { get; set; }
        
    }

    class Tovary_Pointer : DirectoryPointer
    {
        public Tovary_Pointer() { }
    
        public Tovary_Objest GetDirectoryObject()
        {
            Tovary_Objest TovaryObjestItem = new Tovary_Objest();
            TovaryObjestItem.Init(base.UID);
            return TovaryObjestItem;
        }
    }

    class Tovary_Select : DirectorySelect
    {
        public Tovary_Select() : base(Config.Kernel, "Tovary") { }
    
        public void Select() 
        { 
            base.BaseSelect();
                        
            foreach (DirectoryPointer p in base.BaseSelectList)
            {
                
                Console.WriteLine(p.UID.ToString() + " name = " + p.Fields["name"].ToString());
            }
        }
    }
    
    class Tovary_Ceny_TablePart : DirectoryTablePart
    {
        public Tovary_Ceny_TablePart(Tovary_Objest owner) { Owner = owner; }
    
        public Tovary_Objest Owner { get; }
    
        public void Read() { }   
    
        public List<Tovary_Ceny_TablePartRecord> RecordCollection { get; }
    }

    class Tovary_Ceny_TablePartRecord : DirectoryTablePartRecord
    {
        
        public string Field1 { get; set; }
        public string Field2 { get; set; }
    }
      

    class Tovary_Od_TablePart : DirectoryTablePart
    {
        public Tovary_Od_TablePart(Tovary_Objest owner) { Owner = owner; }
    
        public Tovary_Objest Owner { get; }
    
        public void Read() { }   
    
        public List<Tovary_Od_TablePartRecord> RecordCollection { get; }
    }

    class Tovary_Od_TablePartRecord : DirectoryTablePartRecord
    {
        
        public string Name { get; set; }
    }
      
    /*******************************************************[ TovaryInfo ]****************************************************/

    class TovaryInfo_Objest : DirectoryObject
    {
        public TovaryInfo_Objest() { }    
        
        public string Field1 { get; set; }
        
        public string Field2 { get; set; }
        
        public string Field3 { get; set; }
        
        public string Field4 { get; set; }
        
        public string Field5 { get; set; }
        
    }

    class TovaryInfo_Pointer : DirectoryPointer
    {
        public TovaryInfo_Pointer() { }
    
        public TovaryInfo_Objest GetDirectoryObject()
        {
            TovaryInfo_Objest TovaryInfoObjestItem = new TovaryInfo_Objest();
            TovaryInfoObjestItem.Init(base.UID);
            return TovaryInfoObjestItem;
        }
    }

    class TovaryInfo_Select : DirectorySelect
    {
        public TovaryInfo_Select() : base(Config.Kernel, "TovaryInfo") { }
    
        public void Select() 
        { 
            base.BaseSelect();
                        
            foreach (DirectoryPointer p in base.BaseSelectList)
            {
                Console.WriteLine(p.UID.ToString());
            }
        }
    }
      

    class TovaryInfo_Ceny_TablePart : DirectoryTablePart
    {
        public TovaryInfo_Ceny_TablePart(TovaryInfo_Objest owner) { Owner = owner; }
    
        public TovaryInfo_Objest Owner { get; }
    
        public void Read() { }   
    
        public List<TovaryInfo_Ceny_TablePartRecord> RecordCollection { get; }
    }

    class TovaryInfo_Ceny_TablePartRecord : DirectoryTablePartRecord
    {
        
        public string Field1 { get; set; }
        public string Field2 { get; set; }
    }
      

    class TovaryInfo_Od_TablePart : DirectoryTablePart
    {
        public TovaryInfo_Od_TablePart(TovaryInfo_Objest owner) { Owner = owner; }
    
        public TovaryInfo_Objest Owner { get; }
    
        public void Read() { }   
    
        public List<TovaryInfo_Od_TablePartRecord> RecordCollection { get; }
    }

    class TovaryInfo_Od_TablePartRecord : DirectoryTablePartRecord
    {
        
        public string Field1 { get; set; }
        public string Field2 { get; set; }
        public string Field3 { get; set; }
    }
      
    /*******************************************************[ TMC ]****************************************************/

    class TMC_Objest : DirectoryObject
    {
        public TMC_Objest() { }    
        
        public string Code { get; set; }
        
    }

    class TMC_Pointer : DirectoryPointer
    {
        public TMC_Pointer() { }
    
        public TMC_Objest GetDirectoryObject()
        {
            TMC_Objest TMCObjestItem = new TMC_Objest();
            TMCObjestItem.Init(base.UID);
            return TMCObjestItem;
        }
    }

    class TMC_Select : DirectorySelect
    {
        public TMC_Select() : base(Config.Kernel, "TMC") { }
    
        public void Select() 
        { 
            base.BaseSelect();
                        
            foreach (DirectoryPointer p in base.BaseSelectList)
            {
                Console.WriteLine(p.UID.ToString());
            }
        }
    }
      
    /*******************************************************[ TMC2 ]****************************************************/

    class TMC2_Objest : DirectoryObject
    {
        public TMC2_Objest() { }    
        
        public string Code { get; set; }
        
        public string Name { get; set; }
        
    }

    class TMC2_Pointer : DirectoryPointer
    {
        public TMC2_Pointer() { }
    
        public TMC2_Objest GetDirectoryObject()
        {
            TMC2_Objest TMC2ObjestItem = new TMC2_Objest();
            TMC2ObjestItem.Init(base.UID);
            return TMC2ObjestItem;
        }
    }

    class TMC2_Select : DirectorySelect
    {
        public TMC2_Select() : base(Config.Kernel, "TMC2") { }
    
        public void Select() 
        { 
            base.BaseSelect();
                        
            foreach (DirectoryPointer p in base.BaseSelectList)
            {
                Console.WriteLine(p.UID.ToString());
            }
        }
    }
      
    /*******************************************************[ TMC5 ]****************************************************/

    class TMC5_Objest : DirectoryObject
    {
        public TMC5_Objest() { }    
        
        public string Name { get; set; }
        
    }

    class TMC5_Pointer : DirectoryPointer
    {
        public TMC5_Pointer() { }
    
        public TMC5_Objest GetDirectoryObject()
        {
            TMC5_Objest TMC5ObjestItem = new TMC5_Objest();
            TMC5ObjestItem.Init(base.UID);
            return TMC5ObjestItem;
        }
    }

    class TMC5_Select : DirectorySelect
    {
        public TMC5_Select() : base(Config.Kernel, "TMC5") { }
    
        public void Select() 
        { 
            base.BaseSelect();
                        
            foreach (DirectoryPointer p in base.BaseSelectList)
            {
                Console.WriteLine(p.UID.ToString());
            }
        }
    }
      

    class TMC5_Od_TablePart : DirectoryTablePart
    {
        public TMC5_Od_TablePart(TMC5_Objest owner) { Owner = owner; }
    
        public TMC5_Objest Owner { get; }
    
        public void Read() { }   
    
        public List<TMC5_Od_TablePartRecord> RecordCollection { get; }
    }

    class TMC5_Od_TablePartRecord : DirectoryTablePartRecord
    {
        
        public string Name { get; set; }
    }
      
    /*******************************************************[ TMC6 ]****************************************************/

    class TMC6_Objest : DirectoryObject
    {
        public TMC6_Objest() { }    
        
        public string Name { get; set; }
        
        public string Code { get; set; }
        
    }

    class TMC6_Pointer : DirectoryPointer
    {
        public TMC6_Pointer() { }
    
        public TMC6_Objest GetDirectoryObject()
        {
            TMC6_Objest TMC6ObjestItem = new TMC6_Objest();
            TMC6ObjestItem.Init(base.UID);
            return TMC6ObjestItem;
        }
    }

    class TMC6_Select : DirectorySelect
    {
        public TMC6_Select() : base(Config.Kernel, "TMC6") { }
    
        public void Select() 
        { 
            base.BaseSelect();
                        
            foreach (DirectoryPointer p in base.BaseSelectList)
            {
                Console.WriteLine(p.UID.ToString());
            }
        }
    }
      

    class TMC6_Od_TablePart : DirectoryTablePart
    {
        public TMC6_Od_TablePart(TMC6_Objest owner) { Owner = owner; }
    
        public TMC6_Objest Owner { get; }
    
        public void Read() { }   
    
        public List<TMC6_Od_TablePartRecord> RecordCollection { get; }
    }

    class TMC6_Od_TablePartRecord : DirectoryTablePartRecord
    {
        
        public string Name { get; set; }
    }
      
}
  
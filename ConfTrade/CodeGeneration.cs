

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

    
/*******************************************************[ Tovary ]****************************************************/
// --- Objest ---

/// <summary>
/// Directory Tovary Info
/// </summary>
class Tovary_Objest : DirectoryObject
{
    public Tovary_Objest() { }    
    
    public string Field1 { get; set; }
    
    public string Field2 { get; set; }
    
    public string Field3 { get; set; }
    
    public string Field4 { get; set; }
    
    public string Field5 { get; set; }
    
}
      
// --- Pointer ---

/// <summary>
/// Directory Tovary Info
/// </summary>
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

// --- Select ---

/// <summary>
/// Directory Tovary Info
/// </summary>
class Tovary_Select : DirectorySelect
{
    public Tovary_Select() { }
    
    public void Read() { }
    
    public List<Tovary_Pointer> DirectoryPointers { get; }
}

      
        
// --- TablePart ---    

/// <summary>
/// TablePart Test Info
/// </summary>
class Tovary_Ceny_TablePart : DirectoryTablePart
{
    public Tovary_Ceny_TablePart(Tovary_Objest owner)
    {
         Owner = owner;
    }
    
    public Tovary_Objest Owner { get; }
    
    public void Read() { }   
    
    public List<Tovary_Ceny_TablePartRecord> RecordCollection { get; }
}

// --- TablePartRecord ---

/// <summary>
/// TablePart Test Info
/// </summary>
class Tovary_Ceny_TablePartRecord : DirectoryTablePartRecord
{
    
    public string Field1 { get; set; }
    
    public string Field2 { get; set; }
    
}
      
        
// --- TablePart ---    

/// <summary>
/// 
          
/// </summary>
class Tovary_Od_TablePart : DirectoryTablePart
{
    public Tovary_Od_TablePart(Tovary_Objest owner)
    {
         Owner = owner;
    }
    
    public Tovary_Objest Owner { get; }
    
    public void Read() { }   
    
    public List<Tovary_Od_TablePartRecord> RecordCollection { get; }
}

// --- TablePartRecord ---

/// <summary>
/// 
          
/// </summary>
class Tovary_Od_TablePartRecord : DirectoryTablePartRecord
{
    
    public string Name { get; set; }
    
}
      
/*******************************************************[ TovaryInfo ]****************************************************/
// --- Objest ---

/// <summary>
/// Directory Tovary Info
/// </summary>
class TovaryInfo_Objest : DirectoryObject
{
    public TovaryInfo_Objest() { }    
    
    public string Field1 { get; set; }
    
    public string Field2 { get; set; }
    
    public string Field3 { get; set; }
    
    public string Field4 { get; set; }
    
    public string Field5 { get; set; }
    
}
      
// --- Pointer ---

/// <summary>
/// Directory Tovary Info
/// </summary>
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

// --- Select ---

/// <summary>
/// Directory Tovary Info
/// </summary>
class TovaryInfo_Select : DirectorySelect
{
    public TovaryInfo_Select() { }
    
    public void Read() { }
    
    public List<TovaryInfo_Pointer> DirectoryPointers { get; }
}

      
        
// --- TablePart ---    

/// <summary>
/// TablePart Test Info
/// </summary>
class TovaryInfo_Ceny_TablePart : DirectoryTablePart
{
    public TovaryInfo_Ceny_TablePart(TovaryInfo_Objest owner)
    {
         Owner = owner;
    }
    
    public TovaryInfo_Objest Owner { get; }
    
    public void Read() { }   
    
    public List<TovaryInfo_Ceny_TablePartRecord> RecordCollection { get; }
}

// --- TablePartRecord ---

/// <summary>
/// TablePart Test Info
/// </summary>
class TovaryInfo_Ceny_TablePartRecord : DirectoryTablePartRecord
{
    
    public string Field1 { get; set; }
    
    public string Field2 { get; set; }
    
}
      
        
// --- TablePart ---    

/// <summary>
/// TablePart Test Info
/// </summary>
class TovaryInfo_Od_TablePart : DirectoryTablePart
{
    public TovaryInfo_Od_TablePart(TovaryInfo_Objest owner)
    {
         Owner = owner;
    }
    
    public TovaryInfo_Objest Owner { get; }
    
    public void Read() { }   
    
    public List<TovaryInfo_Od_TablePartRecord> RecordCollection { get; }
}

// --- TablePartRecord ---

/// <summary>
/// TablePart Test Info
/// </summary>
class TovaryInfo_Od_TablePartRecord : DirectoryTablePartRecord
{
    
    public string Field1 { get; set; }
    
    public string Field2 { get; set; }
    
    public string Field3 { get; set; }
    
}
      
/*******************************************************[ TMC ]****************************************************/
// --- Objest ---

/// <summary>
/// TMC
/// </summary>
class TMC_Objest : DirectoryObject
{
    public TMC_Objest() { }    
    
    public string Code { get; set; }
    
}
      
// --- Pointer ---

/// <summary>
/// TMC
/// </summary>
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

// --- Select ---

/// <summary>
/// TMC
/// </summary>
class TMC_Select : DirectorySelect
{
    public TMC_Select() { }
    
    public void Read() { }
    
    public List<TMC_Pointer> DirectoryPointers { get; }
}

      
/*******************************************************[ TMC2 ]****************************************************/
// --- Objest ---

/// <summary>
/// TMC 2
/// </summary>
class TMC2_Objest : DirectoryObject
{
    public TMC2_Objest() { }    
    
    public string Code { get; set; }
    
    public string Name { get; set; }
    
}
      
// --- Pointer ---

/// <summary>
/// TMC 2
/// </summary>
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

// --- Select ---

/// <summary>
/// TMC 2
/// </summary>
class TMC2_Select : DirectorySelect
{
    public TMC2_Select() { }
    
    public void Read() { }
    
    public List<TMC2_Pointer> DirectoryPointers { get; }
}

      
/*******************************************************[ TMC5 ]****************************************************/
// --- Objest ---

/// <summary>
/// Desc
/// </summary>
class TMC5_Objest : DirectoryObject
{
    public TMC5_Objest() { }    
    
    public string Name { get; set; }
    
}
      
// --- Pointer ---

/// <summary>
/// Desc
/// </summary>
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

// --- Select ---

/// <summary>
/// Desc
/// </summary>
class TMC5_Select : DirectorySelect
{
    public TMC5_Select() { }
    
    public void Read() { }
    
    public List<TMC5_Pointer> DirectoryPointers { get; }
}

      
        
// --- TablePart ---    

/// <summary>
/// 
          
/// </summary>
class TMC5_Od_TablePart : DirectoryTablePart
{
    public TMC5_Od_TablePart(TMC5_Objest owner)
    {
         Owner = owner;
    }
    
    public TMC5_Objest Owner { get; }
    
    public void Read() { }   
    
    public List<TMC5_Od_TablePartRecord> RecordCollection { get; }
}

// --- TablePartRecord ---

/// <summary>
/// 
          
/// </summary>
class TMC5_Od_TablePartRecord : DirectoryTablePartRecord
{
    
    public string Name { get; set; }
    
}
      
/*******************************************************[ TMC6 ]****************************************************/
// --- Objest ---

/// <summary>
/// Desc
/// </summary>
class TMC6_Objest : DirectoryObject
{
    public TMC6_Objest() { }    
    
    public string Name { get; set; }
    
    public string Code { get; set; }
    
}
      
// --- Pointer ---

/// <summary>
/// Desc
/// </summary>
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

// --- Select ---

/// <summary>
/// Desc
/// </summary>
class TMC6_Select : DirectorySelect
{
    public TMC6_Select() { }
    
    public void Read() { }
    
    public List<TMC6_Pointer> DirectoryPointers { get; }
}

      
        
// --- TablePart ---    

/// <summary>
/// 
          
/// </summary>
class TMC6_Od_TablePart : DirectoryTablePart
{
    public TMC6_Od_TablePart(TMC6_Objest owner)
    {
         Owner = owner;
    }
    
    public TMC6_Objest Owner { get; }
    
    public void Read() { }   
    
    public List<TMC6_Od_TablePartRecord> RecordCollection { get; }
}

// --- TablePartRecord ---

/// <summary>
/// 
          
/// </summary>
class TMC6_Od_TablePartRecord : DirectoryTablePartRecord
{
    
    public string Name { get; set; }
    
}
      
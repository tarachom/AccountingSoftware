

/*
* 
* Автоматично згенерований код.
*
* Конфігурації "ConfTrade 1.1"
* Автор Yurik
* 
*/
    

// --- Objest ---

/// <summary>
/// Directory Tovary Info
/// </summary>
class TovaryObjest : DirectoryObject
{
    public TovaryObjest()
    {
         
    }
    
    
    /// <summary>
    /// Field1
    /// </summary>
    public string Field1 { get; set; }
    
    /// <summary>
    /// Field1
    /// </summary>
    public string Field2 { get; set; }
    
    /// <summary>
    /// Field1
    /// </summary>
    public string Field3 { get; set; }
    
    /// <summary>
    /// Field1
    /// </summary>
    public string Field4 { get; set; }
    
}
      
// --- Pointer ---

/// <summary>
/// Directory Tovary Info
/// </summary>
class TovaryPointer : DirectoryPointer, IDirectoryPointer
{
    public TovaryPointer()
    {
         
    }
    
    public TovaryObjest GetDirectoryObject()
    {
         
    }
}

// --- Select ---

/// <summary>
/// Directory Tovary Info
/// </summary>
class TovarySelect : DirectorySelect
{
    public TovarySelect()
    {
         
    }
    
    public void Read()
    {
         
    }
    
    public List<TovaryPointer> DirectoryPointers { get; }
}

      
        
// --- TablePart ---    

/// <summary>
/// TablePart Test Info
/// </summary>
class TovaryCenyTablePart : DirectoryTablePart
{
    public TovaryCenyTablePart(TovaryObjest owner)
    {
         Owner = owner;
    }
    
    public TovaryObjest Owner { get; }
    
    public void Read()
    {
         
    }
    
    public List<TovaryCenyTablePartRecord> RecordCollection { get; }
}

// --- TablePartRecord ---

/// <summary>
/// TablePart Test Info
/// </summary>
class TovaryCenyTablePartRecord : DirectoryTablePartRecord
{
    
    /// <summary>
    /// Field1
    /// </summary>
    public string Field1 { get; set; }
    
    /// <summary>
    /// Field2
    /// </summary>
    public string Field2 { get; set; }
    
}

      
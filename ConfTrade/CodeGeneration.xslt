<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="text" indent="yes" />

  <xsl:template match="/">

/*
 * Автоматично згенерований код.
 *
 * Конфігурації "<xsl:value-of select="Configuration/Name"/>"
 * Автор <xsl:value-of select="Configuration/Author"/>
 *
 */

using System;
using System.Collections.Generic;
using AccountingSoftware;

    <xsl:for-each select="Configuration/Directories/Directory">
      <xsl:variable name="DirectoryName" select="Name"/>
/*******************************************************[ <xsl:value-of select="$DirectoryName"/> ]****************************************************/
// --- Objest ---

/// &lt;summary&gt;
/// <xsl:value-of select="Desc"/>
/// &lt;/summary&gt;
class <xsl:value-of select="$DirectoryName"/>_Objest : DirectoryObject
{
    public <xsl:value-of select="$DirectoryName"/>_Objest() { }    
    <xsl:for-each select="Fields/Field">
    public <xsl:value-of select="Type"/><xsl:text> </xsl:text><xsl:value-of select="Name"/> { get; set; }
    </xsl:for-each>
}
      
// --- Pointer ---

/// &lt;summary&gt;
/// <xsl:value-of select="Desc"/>
/// &lt;/summary&gt;
class <xsl:value-of select="$DirectoryName"/>_Pointer : DirectoryPointer
{
    public <xsl:value-of select="$DirectoryName"/>_Pointer() { }
    
    public <xsl:value-of select="$DirectoryName"/>_Objest GetDirectoryObject()
    {
        <xsl:value-of select="$DirectoryName"/>_Objest <xsl:value-of select="$DirectoryName"/>ObjestItem = new <xsl:value-of select="$DirectoryName"/>_Objest();
        <xsl:value-of select="$DirectoryName"/>ObjestItem.Init(base.UID);
        return <xsl:value-of select="$DirectoryName"/>ObjestItem;
    }
}

// --- Select ---

/// &lt;summary&gt;
/// <xsl:value-of select="Desc"/>
/// &lt;/summary&gt;
class <xsl:value-of select="$DirectoryName"/>_Select : DirectorySelect
{
    public <xsl:value-of select="$DirectoryName"/>_Select() { }
    
    public void Read() { }
    
    public List&lt;<xsl:value-of select="$DirectoryName"/>_Pointer&gt; DirectoryPointers { get; }
}

      <xsl:for-each select="TabularParts/TablePart">
        <xsl:variable name="TablePartName" select="Name"/>
        <xsl:variable name="TablePartFullName" select="concat($DirectoryName, '_', $TablePartName)"/>
        
// --- TablePart ---    

/// &lt;summary&gt;
/// <xsl:value-of select="Desc"/>
/// &lt;/summary&gt;
class <xsl:value-of select="$TablePartFullName"/>_TablePart : DirectoryTablePart
{
    public <xsl:value-of select="$TablePartFullName"/>_TablePart(<xsl:value-of select="$DirectoryName"/>_Objest owner)
    {
         Owner = owner;
    }
    
    public <xsl:value-of select="$DirectoryName"/>_Objest Owner { get; }
    
    public void Read() { }   
    
    public List&lt;<xsl:value-of select="$TablePartFullName"/>_TablePartRecord&gt; RecordCollection { get; }
}

// --- TablePartRecord ---

/// &lt;summary&gt;
/// <xsl:value-of select="Desc"/>
/// &lt;/summary&gt;
class <xsl:value-of select="$TablePartFullName"/>_TablePartRecord : DirectoryTablePartRecord
{
    <xsl:for-each select="Fields/Field">
    public <xsl:value-of select="Type"/><xsl:text> </xsl:text><xsl:value-of select="Name"/> { get; set; }
    </xsl:for-each>
}
      </xsl:for-each>
    </xsl:for-each>

  </xsl:template>

</xsl:stylesheet>
<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="text" indent="yes" />

  <xsl:template match="/">

/*
 * Автоматично згенерований код.
 *
 * Конфігурації "<xsl:value-of select="Configuration/Name"/>"
 * Автор <xsl:value-of select="Configuration/Autor"/>
 *
 */

using System;
using System.Collections.Generic;

using WebServerTestErlang.AccountingSoftware;

    <xsl:for-each select="Configuration/Directories/Directory">
      <xsl:variable name="DirectoryName" select="Name"/>

// --- Objest ---

/// &lt;summary&gt;
/// <xsl:value-of select="Desc"/>
/// &lt;/summary&gt;
class <xsl:value-of select="$DirectoryName"/>Objest : DirectoryObject
{
    /// &lt;summary&gt;
    ///
    /// &lt;/summary&gt;
    public <xsl:value-of select="$DirectoryName"/>Objest()
    {
         
    }
    
    <xsl:for-each select="Fields/Field">
    /// &lt;summary&gt;
    /// <xsl:value-of select="Desc"/>
    /// &lt;/summary&gt;
    public <xsl:value-of select="Type"/><xsl:text> </xsl:text><xsl:value-of select="Name"/> { get; set; }
    </xsl:for-each>
}
      
// --- Pointer ---

/// &lt;summary&gt;
/// <xsl:value-of select="Desc"/>
/// &lt;/summary&gt;
class <xsl:value-of select="$DirectoryName"/>Pointer : DirectoryPointer, IDirectoryPointer
{
    /// &lt;summary&gt;
    ///
    /// &lt;/summary&gt;
    public <xsl:value-of select="$DirectoryName"/>Pointer()
    {
         
    }
    
    /// &lt;summary&gt;
    ///
    /// &lt;/summary&gt;
    public <xsl:value-of select="$DirectoryName"/>Objest GetDirectoryObject()
    {
        <xsl:value-of select="$DirectoryName"/>Objest <xsl:value-of select="$DirectoryName"/>ObjestItem = new <xsl:value-of select="$DirectoryName"/>Objest();
        <xsl:value-of select="$DirectoryName"/>ObjestItem.Init(base.UID);
        return <xsl:value-of select="$DirectoryName"/>ObjestItem;
    }
}

// --- Select ---

/// &lt;summary&gt;
/// <xsl:value-of select="Desc"/>
/// &lt;/summary&gt;
class <xsl:value-of select="$DirectoryName"/>Select : DirectorySelect
{
    /// &lt;summary&gt;
    ///
    /// &lt;/summary&gt;
    public <xsl:value-of select="$DirectoryName"/>Select()
    {
         
    }
    
    /// &lt;summary&gt;
    ///
    /// &lt;/summary&gt;
    public void Read()
    {
         
    }
    
    /// &lt;summary&gt;
    ///
    /// &lt;/summary&gt;
    public List&lt;<xsl:value-of select="$DirectoryName"/>Pointer&gt; DirectoryPointers { get; }
}

      <xsl:for-each select="TabularParts/TablePart">
        <xsl:variable name="TablePartName" select="Name"/>
        <xsl:variable name="TablePartFullName" select="concat($DirectoryName, $TablePartName)"/>
        
// --- TablePart ---    

/// &lt;summary&gt;
/// <xsl:value-of select="Desc"/>
/// &lt;/summary&gt;
class <xsl:value-of select="$TablePartFullName"/>TablePart : DirectoryTablePart
{
    /// &lt;summary&gt;
    ///
    /// &lt;/summary&gt;
    public <xsl:value-of select="$TablePartFullName"/>TablePart(<xsl:value-of select="$DirectoryName"/>Objest owner)
    {
         Owner = owner;
    }
    
    /// &lt;summary&gt;
    ///
    /// &lt;/summary&gt;
    public <xsl:value-of select="$DirectoryName"/>Objest Owner { get; }
    
    /// &lt;summary&gt;
    ///
    /// &lt;/summary&gt;
    public void Read()
    {
         
    }
    
    /// &lt;summary&gt;
    ///
    /// &lt;/summary&gt;
    public List&lt;<xsl:value-of select="$TablePartFullName"/>TablePartRecord&gt; RecordCollection { get; }
}

// --- TablePartRecord ---

/// &lt;summary&gt;
/// <xsl:value-of select="Desc"/>
/// &lt;/summary&gt;
class <xsl:value-of select="$TablePartFullName"/>TablePartRecord : DirectoryTablePartRecord
{
    <xsl:for-each select="Fields/Field">
    /// &lt;summary&gt;
    /// <xsl:value-of select="Desc"/>
    /// &lt;/summary&gt;
    public <xsl:value-of select="Type"/><xsl:text> </xsl:text><xsl:value-of select="Name"/> { get; set; }
    </xsl:for-each>
}

      </xsl:for-each>

    </xsl:for-each>

  </xsl:template>

</xsl:stylesheet>
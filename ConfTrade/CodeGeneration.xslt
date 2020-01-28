<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="text" indent="yes" />

  <xsl:template match="/">

/*
 *
 * Конфігурації "<xsl:value-of select="Configuration/Name"/>"
 * Автор <xsl:value-of select="Configuration/Author"/>
 *
 */

using System;
using AccountingSoftware;

namespace <xsl:value-of select="Configuration/NameSpace"/>
{
    static class Config
    {
        public static Kernel Kernel { get; set; }
    }
    <xsl:for-each select="Configuration/Directories/Directory">
      <xsl:variable name="DirectoryName" select="Name"/>
    /*******************************************************[ <xsl:value-of select="$DirectoryName"/> ]****************************************************/

    class <xsl:value-of select="$DirectoryName"/>_Objest : DirectoryObject
    {
        public <xsl:value-of select="$DirectoryName"/>_Objest() : base(Config.Kernel, "<xsl:value-of select="Table"/>",
             <xsl:text>new string[] { </xsl:text>
             <xsl:for-each select="Fields/Field">
               <xsl:if test="position() != 1">
                 <xsl:text>, </xsl:text>
               </xsl:if>
               <xsl:text>"</xsl:text><xsl:value-of select="Name"/><xsl:text>"</xsl:text>
             </xsl:for-each> }) 
        {
            <xsl:for-each select="Fields/Field">
              <xsl:value-of select="Name"/>
              <xsl:text> = </xsl:text> 
              <xsl:choose>
                <xsl:when test="Type = 'string'">
                  <xsl:text>""</xsl:text>
                </xsl:when>
                <xsl:when test="Type = 'integer' or Type = 'numeric'">
                  <xsl:text>0</xsl:text>
                </xsl:when>
                <xsl:when test="Type = 'boolean'">
                  <xsl:text>false</xsl:text>
                </xsl:when>
                <xsl:when test="Type = 'pointer'">
                  <xsl:text>new </xsl:text><xsl:value-of select="Pointer"/><xsl:text>_Pointer()</xsl:text>
                </xsl:when>
              </xsl:choose>;
            </xsl:for-each>
        }
        
        public void Init(UnigueID uid)
        {
            BaseInit(uid);
            
            <xsl:for-each select="Fields/Field">
              <xsl:value-of select="Name"/>
              <xsl:text> = </xsl:text>
              <xsl:choose>
                <xsl:when test="Type = 'string'">
                  <xsl:text>base.Fields["</xsl:text><xsl:value-of select="Name"/><xsl:text>"].ToString()</xsl:text>
                </xsl:when>
                <xsl:when test="Type = 'integer'">
                  <xsl:text>(int)base.Fields["</xsl:text><xsl:value-of select="Name"/><xsl:text>"]</xsl:text>
                </xsl:when>
                <xsl:when test="Type = 'numeric'">
                  <xsl:text>(decimal)base.Fields["</xsl:text><xsl:value-of select="Name"/><xsl:text>"]</xsl:text>
                </xsl:when>
                <xsl:when test="Type = 'boolean'">
                  <xsl:text>(bool)base.Fields["</xsl:text><xsl:value-of select="Name"/><xsl:text>"]</xsl:text>
                </xsl:when>
                <xsl:when test="Type = 'pointer'">
                  <xsl:text>new </xsl:text><xsl:value-of select="Pointer"/>
                  <xsl:text>_Pointer(base.Fields["</xsl:text><xsl:value-of select="Name"/><xsl:text>"])</xsl:text>
                </xsl:when>
              </xsl:choose>;
            </xsl:for-each>
        }
        
        public void Save()
        {
            <xsl:for-each select="Fields/Field">
              <xsl:text>base.Fields["</xsl:text><xsl:value-of select="Name"/><xsl:text>"] = </xsl:text>
              <xsl:value-of select="Name"/>
              <xsl:choose>
                <xsl:when test="Type = 'pointer'">
                  <xsl:text>.UnigueID.UGuid</xsl:text>
                </xsl:when>
              </xsl:choose>;
            </xsl:for-each>
            BaseSave();
        }
        
        <xsl:for-each select="Fields/Field">
          <xsl:text>public </xsl:text>
          <xsl:choose>
            <xsl:when test="Type = 'integer'">
              <xsl:text>int</xsl:text>
            </xsl:when>
            <xsl:when test="Type = 'numeric'">
              <xsl:text>decimal</xsl:text>
            </xsl:when>
            <xsl:when test="Type = 'boolean'">
              <xsl:text>bool</xsl:text>
            </xsl:when>
            <xsl:when test="Type = 'pointer'">
              <xsl:value-of select="Pointer"/>
              <xsl:text>_Pointer</xsl:text>
            </xsl:when>
            <xsl:otherwise>
              <xsl:value-of select="Type"/>
            </xsl:otherwise>
          </xsl:choose>
          <xsl:text> </xsl:text>
          <xsl:value-of select="Name"/>
          <xsl:text> { get; set; </xsl:text>}
        </xsl:for-each>
        
        public <xsl:value-of select="$DirectoryName"/>_Pointer GetDirectoryPointer()
        {
            <xsl:value-of select="$DirectoryName"/>_Pointer directoryPointer = new <xsl:value-of select="$DirectoryName"/>_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
    }

    class <xsl:value-of select="$DirectoryName"/>_Pointer : DirectoryPointer
    {
        public <xsl:value-of select="$DirectoryName"/>_Pointer(object uid = null) : base(Config.Kernel, "<xsl:value-of select="Table"/>")
        {
            if (uid != null &amp;&amp; uid != DBNull.Value) base.Init(new UnigueID((Guid)uid), null);
        }

        public <xsl:value-of select="$DirectoryName"/>_Objest GetDirectoryObject()
        {
            <xsl:value-of select="$DirectoryName"/>_Objest <xsl:value-of select="$DirectoryName"/>ObjestItem = new <xsl:value-of select="$DirectoryName"/>_Objest();
            <xsl:value-of select="$DirectoryName"/>ObjestItem.Init(base.UnigueID);
            return <xsl:value-of select="$DirectoryName"/>ObjestItem;
        }
    }

    class <xsl:value-of select="$DirectoryName"/>_Select : DirectorySelect
    {
        public <xsl:value-of select="$DirectoryName"/>_Select() : base(Config.Kernel, "<xsl:value-of select="Table"/>") { }
    
        public void Select() 
        { 
            base.BaseSelect();
        }
        
        public bool MoveNext()
        {
            Current = null;

            if (MoveToPosition())
            {
                Current = new <xsl:value-of select="$DirectoryName"/>_Pointer();
                Current.Init(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields);

                return true;
            }
            else
                return false;
        }

        public <xsl:value-of select="$DirectoryName"/>_Pointer Current { get; private set; }
    }
    <!--
      <xsl:for-each select="TabularParts/TablePart">
        <xsl:variable name="TablePartName" select="Name"/>
        <xsl:variable name="TablePartFullName" select="concat($DirectoryName, '_', $TablePartName)"/>

    class <xsl:value-of select="$TablePartFullName"/>_TablePart : DirectoryTablePart
    {
        public <xsl:value-of select="$TablePartFullName"/>_TablePart(<xsl:value-of select="$DirectoryName"/>_Objest owner) { Owner = owner; }
    
        public <xsl:value-of select="$DirectoryName"/>_Objest Owner { get; }
    
        public void Read() { }   
    
        public List&lt;<xsl:value-of select="$TablePartFullName"/>_TablePartRecord&gt; RecordCollection { get; }
    }

    class <xsl:value-of select="$TablePartFullName"/>_TablePartRecord : DirectoryTablePartRecord
    {
        <xsl:for-each select="Fields/Field">
        public <xsl:value-of select="Type"/><xsl:text> </xsl:text><xsl:value-of select="Name"/> { get; set; }</xsl:for-each>
    }
      </xsl:for-each>
    -->
    </xsl:for-each>
}
  </xsl:template>

</xsl:stylesheet>
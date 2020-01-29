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
using System.Collections.Generic;
using AccountingSoftware;

namespace <xsl:value-of select="Configuration/NameSpace"/>
{
    static class Config
    {
        public static Kernel Kernel { get; set; }
    }
    <xsl:for-each select="Configuration/Directories/Directory">
      <xsl:variable name="DirectoryName" select="Name"/>
    #region DIRECTORY "<xsl:value-of select="$DirectoryName"/>"
    
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
                <xsl:when test="Type = 'string[]'">
                  <xsl:text>new string[] { }</xsl:text>
                </xsl:when>
                <xsl:when test="Type = 'integer'">
                  <xsl:text>0</xsl:text>
                </xsl:when>
                <xsl:when test="Type = 'integer[]'">
                  <xsl:text>new int[] { }</xsl:text>
                </xsl:when>
                 <xsl:when test="Type = 'numeric'">
                  <xsl:text>0</xsl:text>
                </xsl:when>
                <xsl:when test="Type = 'numeric[]'">
                  <xsl:text>new decimal[] { }</xsl:text>
                </xsl:when>
                <xsl:when test="Type = 'boolean'">
                  <xsl:text>false</xsl:text>
                </xsl:when>
                <xsl:when test="Type = 'time'">
                  <xsl:text>DateTime.MinValue.TimeOfDay</xsl:text>
                </xsl:when>
                <xsl:when test="Type = 'date' or Type = 'datetime'">
                  <xsl:text>DateTime.MinValue</xsl:text>
                </xsl:when>
                <xsl:when test="Type = 'pointer'">
                  <xsl:text>new </xsl:text><xsl:value-of select="Pointer"/><xsl:text>_Pointer()</xsl:text>
                </xsl:when>
              </xsl:choose>;
            </xsl:for-each>
            //Табличні частини
            <xsl:for-each select="TabularParts/TablePart">
                <xsl:variable name="TablePartName" select="concat(Name, '_TablePart')"/>
                <xsl:value-of select="$TablePartName"/><xsl:text> = new </xsl:text>
                <xsl:value-of select="concat($DirectoryName, '_', $TablePartName)"/><xsl:text>(this)</xsl:text>;
            </xsl:for-each>
        }
        
        public void Read(UnigueID uid)
        {
            BaseRead(uid);
            
            <xsl:for-each select="Fields/Field">
              <xsl:value-of select="Name"/>
              <xsl:text> = </xsl:text>
              <xsl:choose>
                <xsl:when test="Type = 'string'">
                  <xsl:text>base.FieldValue["</xsl:text><xsl:value-of select="Name"/><xsl:text>"].ToString()</xsl:text>
                </xsl:when>
                <xsl:when test="Type = 'string[]'">
                  <xsl:text>(base.FieldValue["</xsl:text><xsl:value-of select="Name"/><xsl:text>"] != DBNull.Value) ? </xsl:text>
                  <xsl:text>(string[])base.FieldValue["</xsl:text><xsl:value-of select="Name"/><xsl:text>"]</xsl:text>
                  <xsl:text> : new string[] { }</xsl:text>
                </xsl:when>
                <xsl:when test="Type = 'integer'">
                  <xsl:text>(int)base.FieldValue["</xsl:text><xsl:value-of select="Name"/><xsl:text>"]</xsl:text>
                </xsl:when>
                <xsl:when test="Type = 'integer[]'">
                  <xsl:text>(base.FieldValue["</xsl:text><xsl:value-of select="Name"/><xsl:text>"] != DBNull.Value) ? </xsl:text>
                  <xsl:text>(int[])base.FieldValue["</xsl:text><xsl:value-of select="Name"/><xsl:text>"]</xsl:text>
                  <xsl:text> : new int[] { }</xsl:text>
                </xsl:when>
                <xsl:when test="Type = 'numeric'">
                  <xsl:text>(decimal)base.FieldValue["</xsl:text><xsl:value-of select="Name"/><xsl:text>"]</xsl:text>
                </xsl:when>
                <xsl:when test="Type = 'numeric[]'">
                  <xsl:text>(base.FieldValue["</xsl:text><xsl:value-of select="Name"/><xsl:text>"] != DBNull.Value) ? </xsl:text>
                  <xsl:text>(decimal[])base.FieldValue["</xsl:text><xsl:value-of select="Name"/><xsl:text>"]</xsl:text>
                  <xsl:text> : new decimal[] { }</xsl:text>
                </xsl:when>
                <xsl:when test="Type = 'boolean'">
                  <xsl:text>(bool)base.FieldValue["</xsl:text><xsl:value-of select="Name"/><xsl:text>"]</xsl:text>
                </xsl:when>
                <xsl:when test="Type = 'time'">
                  <xsl:text>(base.FieldValue["</xsl:text><xsl:value-of select="Name"/><xsl:text>"] != DBNull.Value) ? </xsl:text>
                  <xsl:text>TimeSpan.Parse(base.FieldValue["</xsl:text><xsl:value-of select="Name"/><xsl:text>"].ToString())</xsl:text>
                  <xsl:text> : DateTime.MinValue.TimeOfDay</xsl:text>
                </xsl:when>
                <xsl:when test="Type = 'date' or Type = 'datetime'">
                  <xsl:text>(base.FieldValue["</xsl:text><xsl:value-of select="Name"/><xsl:text>"] != DBNull.Value) ? </xsl:text>
                  <xsl:text>DateTime.Parse(base.FieldValue["</xsl:text><xsl:value-of select="Name"/><xsl:text>"].ToString())</xsl:text>
                  <xsl:text> : DateTime.MinValue</xsl:text>
                </xsl:when>
                <xsl:when test="Type = 'pointer'">
                  <xsl:text>new </xsl:text><xsl:value-of select="Pointer"/>
                  <xsl:text>_Pointer(base.FieldValue["</xsl:text><xsl:value-of select="Name"/><xsl:text>"])</xsl:text>
                </xsl:when>
              </xsl:choose>;
            </xsl:for-each>
        }
        
        public void Save()
        {
            <xsl:for-each select="Fields/Field">
              <xsl:text>base.FieldValue["</xsl:text><xsl:value-of select="Name"/><xsl:text>"] = </xsl:text>
              <xsl:value-of select="Name"/>
              <xsl:choose>
                <xsl:when test="Type = 'pointer'">
                  <xsl:text>.UnigueID.UGuid</xsl:text>
                </xsl:when>
              </xsl:choose>;
            </xsl:for-each>
            BaseSave();
        }
        
        public <xsl:value-of select="$DirectoryName"/>_Pointer GetDirectoryPointer()
        {
            <xsl:value-of select="$DirectoryName"/>_Pointer directoryPointer = new <xsl:value-of select="$DirectoryName"/>_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        <xsl:for-each select="Fields/Field">
          <xsl:text>public </xsl:text>
          <xsl:choose>
            <xsl:when test="Type = 'string'">
              <xsl:text>string</xsl:text>
            </xsl:when>
            <xsl:when test="Type = 'string[]'">
              <xsl:text>string[]</xsl:text>
            </xsl:when>
            <xsl:when test="Type = 'integer'">
              <xsl:text>int</xsl:text>
            </xsl:when>
            <xsl:when test="Type = 'integer[]'">
              <xsl:text>int[]</xsl:text>
            </xsl:when>
            <xsl:when test="Type = 'numeric'">
              <xsl:text>decimal</xsl:text>
            </xsl:when>
            <xsl:when test="Type = 'numeric[]'">
              <xsl:text>decimal[]</xsl:text>
            </xsl:when>
            <xsl:when test="Type = 'boolean'">
              <xsl:text>bool</xsl:text>
            </xsl:when>
            <xsl:when test="Type = 'time'">
              <xsl:text>TimeSpan</xsl:text>
            </xsl:when>
            <xsl:when test="Type = 'date' or Type = 'datetime'">
              <xsl:text>DateTime</xsl:text>
            </xsl:when>
            <xsl:when test="Type = 'pointer'">
              <xsl:value-of select="Pointer"/>
              <xsl:text>_Pointer</xsl:text>
            </xsl:when>
          </xsl:choose>
          <xsl:text> </xsl:text>
          <xsl:value-of select="Name"/>
          <xsl:text> { get; set; </xsl:text>}
        </xsl:for-each>
        //Табличні частини
        <xsl:for-each select="TabularParts/TablePart">
            <xsl:variable name="TablePartName" select="concat(Name, '_TablePart')"/>
            <xsl:text>public </xsl:text><xsl:value-of select="concat($DirectoryName, '_', $TablePartName)"/><xsl:text> </xsl:text>
            <xsl:value-of select="$TablePartName"/><xsl:text> { get; set; </xsl:text>}
        </xsl:for-each>
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
            <xsl:value-of select="$DirectoryName"/>ObjestItem.Read(base.UnigueID);
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
    
      <xsl:for-each select="TabularParts/TablePart"> <!-- TableParts -->
        <xsl:variable name="TablePartName" select="Name"/>
        <xsl:variable name="TablePartFullName" select="concat($DirectoryName, '_', $TablePartName)"/>
    class <xsl:value-of select="$TablePartFullName"/>_TablePart : DirectoryTablePart
    {
        public <xsl:value-of select="$TablePartFullName"/>_TablePart(<xsl:value-of select="$DirectoryName"/>_Objest owner) : base(Config.Kernel, "<xsl:value-of select="Table"/>",
             <xsl:text>new string[] { </xsl:text>
             <xsl:for-each select="Fields/Field">
               <xsl:if test="position() != 1">
                 <xsl:text>, </xsl:text>
               </xsl:if>
               <xsl:text>"</xsl:text><xsl:value-of select="Name"/><xsl:text>"</xsl:text>
             </xsl:for-each> }) 
        {
            Owner = owner;
        }
        
        public <xsl:value-of select="$DirectoryName"/>_Objest Owner { get; private set; }
        
        public List&lt;<xsl:value-of select="$TablePartFullName"/>_TablePartRecord&gt; Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.FieldValueList.Clear();

            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary&lt;string, object&gt; fieldValue in base.FieldValueList) 
            {
                <xsl:value-of select="$TablePartFullName"/>_TablePartRecord record = new <xsl:value-of select="$TablePartFullName"/>_TablePartRecord();

                <xsl:for-each select="Fields/Field">
                  <xsl:text>record.</xsl:text>
                  <xsl:value-of select="Name"/>
                  <xsl:text> = </xsl:text>
                  <xsl:choose>
                    <xsl:when test="Type = 'string'">
                      <xsl:text>fieldValue["</xsl:text><xsl:value-of select="Name"/><xsl:text>"].ToString()</xsl:text>
                    </xsl:when>
                    <xsl:when test="Type = 'string[]'">
                      <xsl:text>(fieldValue["</xsl:text><xsl:value-of select="Name"/><xsl:text>"] != DBNull.Value) ? </xsl:text>
                      <xsl:text>(string[])fieldValue["</xsl:text><xsl:value-of select="Name"/><xsl:text>"]</xsl:text>
                      <xsl:text> : new string[] { }</xsl:text>
                    </xsl:when>
                    <xsl:when test="Type = 'integer'">
                      <xsl:text>(int)fieldValue["</xsl:text><xsl:value-of select="Name"/><xsl:text>"]</xsl:text>
                    </xsl:when>
                    <xsl:when test="Type = 'integer[]'">
                      <xsl:text>(fieldValue["</xsl:text><xsl:value-of select="Name"/><xsl:text>"] != DBNull.Value) ? </xsl:text>
                      <xsl:text>(int[])fieldValue["</xsl:text><xsl:value-of select="Name"/><xsl:text>"]</xsl:text>
                      <xsl:text> : new int[] { }</xsl:text>
                    </xsl:when>
                    <xsl:when test="Type = 'numeric'">
                      <xsl:text>(decimal)fieldValue["</xsl:text><xsl:value-of select="Name"/><xsl:text>"]</xsl:text>
                    </xsl:when>
                    <xsl:when test="Type = 'numeric[]'">
                      <xsl:text>(fieldValue["</xsl:text><xsl:value-of select="Name"/><xsl:text>"] != DBNull.Value) ? </xsl:text>
                      <xsl:text>(decimal[])fieldValue["</xsl:text><xsl:value-of select="Name"/><xsl:text>"]</xsl:text>
                      <xsl:text> : new decimal[] { }</xsl:text>
                    </xsl:when>
                    <xsl:when test="Type = 'boolean'">
                      <xsl:text>(bool)fieldValue["</xsl:text><xsl:value-of select="Name"/><xsl:text>"]</xsl:text>
                    </xsl:when>
                    <xsl:when test="Type = 'time'">
                      <xsl:text>(fieldValue["</xsl:text><xsl:value-of select="Name"/><xsl:text>"] != DBNull.Value) ? </xsl:text>
                      <xsl:text>TimeSpan.Parse(fieldValue["</xsl:text><xsl:value-of select="Name"/><xsl:text>"].ToString())</xsl:text>
                      <xsl:text> : DateTime.MinValue.TimeOfDay</xsl:text>
                    </xsl:when>
                    <xsl:when test="Type = 'date' or Type = 'datetime'">
                      <xsl:text>(fieldValue["</xsl:text><xsl:value-of select="Name"/><xsl:text>"] != DBNull.Value) ? </xsl:text>
                      <xsl:text>DateTime.Parse(fieldValue["</xsl:text><xsl:value-of select="Name"/><xsl:text>"].ToString())</xsl:text>
                      <xsl:text> : DateTime.MinValue</xsl:text>
                    </xsl:when>
                    <xsl:when test="Type = 'pointer'">
                      <xsl:text>new </xsl:text><xsl:value-of select="Pointer"/>
                      <xsl:text>_Pointer(fieldValue["</xsl:text><xsl:value-of select="Name"/><xsl:text>"])</xsl:text>
                    </xsl:when>
                  </xsl:choose>;
                </xsl:for-each>
                Records.Add(record);
            }
        }
        
        public void Save() 
        {
            base.BaseDelete(Owner.UnigueID);

            foreach (<xsl:value-of select="$TablePartFullName"/>_TablePartRecord record in Records)
            {
                Dictionary&lt;string, object&gt; fieldValue = new Dictionary&lt;string, object&gt;();

                <xsl:for-each select="Fields/Field">
                  <xsl:text>fieldValue.Add("</xsl:text>
                  <xsl:value-of select="Name"/><xsl:text>", record.</xsl:text><xsl:value-of select="Name"/>
                  <xsl:choose>
                    <xsl:when test="Type = 'pointer'">
                      <xsl:text>.UnigueID.UGuid</xsl:text>
                    </xsl:when>
                  </xsl:choose>
                  <xsl:text>)</xsl:text>;
                </xsl:for-each>
                base.BaseSave(Owner.UnigueID, fieldValue);
            }
        }
    }
    
    class <xsl:value-of select="$TablePartFullName"/>_TablePartRecord : DirectoryTablePartRecord
    {
        public <xsl:value-of select="$TablePartFullName"/>_TablePartRecord()
        {
            <xsl:for-each select="Fields/Field">
              <xsl:value-of select="Name"/>
              <xsl:text> = </xsl:text> 
              <xsl:choose>
                <xsl:when test="Type = 'string'">
                  <xsl:text>""</xsl:text>
                </xsl:when>
                <xsl:when test="Type = 'string[]'">
                  <xsl:text>new string[] { }</xsl:text>
                </xsl:when>
                <xsl:when test="Type = 'integer'">
                  <xsl:text>0</xsl:text>
                </xsl:when>
                <xsl:when test="Type = 'integer[]'">
                  <xsl:text>new int[] { }</xsl:text>
                </xsl:when>
                 <xsl:when test="Type = 'numeric'">
                  <xsl:text>0</xsl:text>
                </xsl:when>
                <xsl:when test="Type = 'numeric[]'">
                  <xsl:text>new decimal[] { }</xsl:text>
                </xsl:when>
                <xsl:when test="Type = 'boolean'">
                  <xsl:text>false</xsl:text>
                </xsl:when>
                <xsl:when test="Type = 'time'">
                  <xsl:text>DateTime.MinValue.TimeOfDay</xsl:text>
                </xsl:when>
                <xsl:when test="Type = 'date' or Type = 'datetime'">
                  <xsl:text>DateTime.MinValue</xsl:text>
                </xsl:when>
                <xsl:when test="Type = 'pointer'">
                  <xsl:text>new </xsl:text><xsl:value-of select="Pointer"/><xsl:text>_Pointer()</xsl:text>
                </xsl:when>
              </xsl:choose>;
            </xsl:for-each>
        }
        
        <xsl:for-each select="Fields/Field">
          <xsl:text>public </xsl:text>
          <xsl:choose>
            <xsl:when test="Type = 'string'">
              <xsl:text>string</xsl:text>
            </xsl:when>
            <xsl:when test="Type = 'string[]'">
              <xsl:text>string[]</xsl:text>
            </xsl:when>
            <xsl:when test="Type = 'integer'">
              <xsl:text>int</xsl:text>
            </xsl:when>
            <xsl:when test="Type = 'integer[]'">
              <xsl:text>int[]</xsl:text>
            </xsl:when>
            <xsl:when test="Type = 'numeric'">
              <xsl:text>decimal</xsl:text>
            </xsl:when>
            <xsl:when test="Type = 'numeric[]'">
              <xsl:text>decimal[]</xsl:text>
            </xsl:when>
            <xsl:when test="Type = 'boolean'">
              <xsl:text>bool</xsl:text>
            </xsl:when>
            <xsl:when test="Type = 'time'">
              <xsl:text>TimeSpan</xsl:text>
            </xsl:when>
            <xsl:when test="Type = 'date' or Type = 'datetime'">
              <xsl:text>DateTime</xsl:text>
            </xsl:when>
            <xsl:when test="Type = 'pointer'">
              <xsl:value-of select="Pointer"/>
              <xsl:text>_Pointer</xsl:text>
            </xsl:when>
          </xsl:choose>
          <xsl:text> </xsl:text>
          <xsl:value-of select="Name"/>
          <xsl:text> { get; set; </xsl:text>}
        </xsl:for-each>
    }
      </xsl:for-each> <!-- TableParts -->
    #endregion
    </xsl:for-each>
}
  </xsl:template>

</xsl:stylesheet>
<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="text" indent="yes" />

  <xsl:template name="License">
/*
Copyright (C) 2019-2020 TARAKHOMYN YURIY IVANOVYCH
All rights reserved.

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

/*
Автор:    Тарахомин Юрій Іванович
Адреса:   Україна, м. Львів
Сайт:     find.org.ua
*/
  </xsl:template>
  
  <!-- Для задання типу поля -->
  <xsl:template name="FieldType">
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
      <xsl:when test="Type = 'empty_pointer'">
        <xsl:text>EmptyPointer</xsl:text>
      </xsl:when>
      <xsl:when test="Type = 'enum'">
        <xsl:value-of select="Pointer"/>
      </xsl:when>
    </xsl:choose>    
  </xsl:template>
  
  <!-- Для конструкторів. Значення поля по замовчуванню відповідно до типу -->
  <xsl:template name="DefaultFieldValue">
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
        <xsl:text>new </xsl:text>
        <xsl:value-of select="Pointer"/>
        <xsl:text>_Pointer()</xsl:text>
      </xsl:when>
      <xsl:when test="Type = 'empty_pointer'">
        <xsl:text>new EmptyPointer()</xsl:text>
      </xsl:when>
      <xsl:when test="Type = 'enum'">
        <xsl:text>0</xsl:text>
      </xsl:when>
    </xsl:choose>
  </xsl:template>

  <!-- Для параметрів функцій. Значення параметра по замовчуванню відповідно до типу -->
  <xsl:template name="DefaultParamValue">
    <xsl:choose>
      <xsl:when test="Type = 'string'">
        <xsl:text>""</xsl:text>
      </xsl:when>
      <xsl:when test="Type = 'string[]'">
        <xsl:text>null</xsl:text>
      </xsl:when>
      <xsl:when test="Type = 'integer'">
        <xsl:text>0</xsl:text>
      </xsl:when>
      <xsl:when test="Type = 'integer[]'">
        <xsl:text>null</xsl:text>
      </xsl:when>
      <xsl:when test="Type = 'numeric'">
        <xsl:text>0</xsl:text>
      </xsl:when>
      <xsl:when test="Type = 'numeric[]'">
        <xsl:text>null</xsl:text>
      </xsl:when>
      <xsl:when test="Type = 'boolean'">
        <xsl:text>false</xsl:text>
      </xsl:when>
      <xsl:when test="Type = 'time'">
        <xsl:text>null</xsl:text>
      </xsl:when>
      <xsl:when test="Type = 'date' or Type = 'datetime'">
        <xsl:text>null</xsl:text>
      </xsl:when>
      <xsl:when test="Type = 'pointer'">
        <xsl:text>null</xsl:text>
      </xsl:when>
      <xsl:when test="Type = 'empty_pointer'">
        <xsl:text>null</xsl:text>
      </xsl:when>
      <xsl:when test="Type = 'enum'">
        <xsl:text>0</xsl:text>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  
  <!-- Для присвоєння значення полям -->
  <xsl:template name="ReadFieldValue">
     <xsl:param name="BaseFieldContainer" />
     
     <xsl:choose>
        <xsl:when test="Type = 'string'">
          <xsl:value-of select="$BaseFieldContainer"/><xsl:text>["</xsl:text><xsl:value-of select="NameInTable"/><xsl:text>"].ToString()</xsl:text>
        </xsl:when>
        <xsl:when test="Type = 'string[]'">
          <xsl:text>(</xsl:text><xsl:value-of select="$BaseFieldContainer"/><xsl:text>["</xsl:text><xsl:value-of select="NameInTable"/><xsl:text>"] != DBNull.Value) ? </xsl:text>
          <xsl:text>(string[])</xsl:text><xsl:value-of select="$BaseFieldContainer"/><xsl:text>["</xsl:text><xsl:value-of select="NameInTable"/><xsl:text>"]</xsl:text>
          <xsl:text> : new string[] { }</xsl:text>
        </xsl:when>
       <xsl:when test="Type = 'integer'">
          <xsl:text>(</xsl:text><xsl:value-of select="$BaseFieldContainer"/><xsl:text>["</xsl:text><xsl:value-of select="NameInTable"/><xsl:text>"] != DBNull.Value) ? </xsl:text>
          <xsl:text>(int)</xsl:text><xsl:value-of select="$BaseFieldContainer"/><xsl:text>["</xsl:text><xsl:value-of select="NameInTable"/><xsl:text>"]</xsl:text>
          <xsl:text> : 0</xsl:text>
        </xsl:when>
        <xsl:when test="Type = 'integer[]'">
          <xsl:text>(</xsl:text><xsl:value-of select="$BaseFieldContainer"/><xsl:text>["</xsl:text><xsl:value-of select="NameInTable"/><xsl:text>"] != DBNull.Value) ? </xsl:text>
          <xsl:text>(int[])</xsl:text><xsl:value-of select="$BaseFieldContainer"/><xsl:text>["</xsl:text><xsl:value-of select="NameInTable"/><xsl:text>"]</xsl:text>
          <xsl:text> : new int[] { }</xsl:text>
        </xsl:when>
        <xsl:when test="Type = 'numeric'">
          <xsl:text>(</xsl:text><xsl:value-of select="$BaseFieldContainer"/><xsl:text>["</xsl:text><xsl:value-of select="NameInTable"/><xsl:text>"] != DBNull.Value) ? </xsl:text>
          <xsl:text>(decimal)</xsl:text><xsl:value-of select="$BaseFieldContainer"/><xsl:text>["</xsl:text><xsl:value-of select="NameInTable"/><xsl:text>"]</xsl:text>
          <xsl:text> : 0</xsl:text>
        </xsl:when>
        <xsl:when test="Type = 'numeric[]'">
          <xsl:text>(</xsl:text><xsl:value-of select="$BaseFieldContainer"/><xsl:text>["</xsl:text><xsl:value-of select="NameInTable"/><xsl:text>"] != DBNull.Value) ? </xsl:text>
          <xsl:text>(decimal[])</xsl:text><xsl:value-of select="$BaseFieldContainer"/><xsl:text>["</xsl:text><xsl:value-of select="NameInTable"/><xsl:text>"]</xsl:text>
          <xsl:text> : new decimal[] { }</xsl:text>
        </xsl:when>
        <xsl:when test="Type = 'boolean'">
          <xsl:text>(bool)</xsl:text><xsl:value-of select="$BaseFieldContainer"/><xsl:text>["</xsl:text><xsl:value-of select="NameInTable"/><xsl:text>"]</xsl:text>
        </xsl:when>
        <xsl:when test="Type = 'time'">
          <xsl:text>(</xsl:text><xsl:value-of select="$BaseFieldContainer"/><xsl:text>["</xsl:text><xsl:value-of select="NameInTable"/><xsl:text>"] != DBNull.Value) ? </xsl:text>
          <xsl:text>TimeSpan.Parse(</xsl:text><xsl:value-of select="$BaseFieldContainer"/><xsl:text>["</xsl:text><xsl:value-of select="NameInTable"/><xsl:text>"].ToString())</xsl:text>
          <xsl:text> : DateTime.MinValue.TimeOfDay</xsl:text>
        </xsl:when>
        <xsl:when test="Type = 'date' or Type = 'datetime'">
          <xsl:text>(</xsl:text><xsl:value-of select="$BaseFieldContainer"/><xsl:text>["</xsl:text><xsl:value-of select="NameInTable"/><xsl:text>"] != DBNull.Value) ? </xsl:text>
          <xsl:text>DateTime.Parse(</xsl:text><xsl:value-of select="$BaseFieldContainer"/><xsl:text>["</xsl:text><xsl:value-of select="NameInTable"/><xsl:text>"].ToString())</xsl:text>
          <xsl:text> : DateTime.MinValue</xsl:text>
        </xsl:when>
        <xsl:when test="Type = 'pointer'">
          <xsl:text>new </xsl:text><xsl:value-of select="Pointer"/>
          <xsl:text>_Pointer(</xsl:text><xsl:value-of select="$BaseFieldContainer"/><xsl:text>["</xsl:text><xsl:value-of select="NameInTable"/><xsl:text>"])</xsl:text>
        </xsl:when>
        <xsl:when test="Type = 'empty_pointer'">
          <xsl:text>new EmptyPointer()</xsl:text>
        </xsl:when>
        <xsl:when test="Type = 'enum'">
          <xsl:text>(</xsl:text><xsl:value-of select="Pointer"/><xsl:text>)</xsl:text>
          <xsl:value-of select="$BaseFieldContainer"/><xsl:text>["</xsl:text><xsl:value-of select="NameInTable"/><xsl:text>"]</xsl:text>
        </xsl:when>
     </xsl:choose>
  </xsl:template>

  <!-- Документування коду -->
  <xsl:template name="CommentSummary">
    <xsl:variable name="normalize_space_Desc" select="normalize-space(Desc)" />
    <xsl:if test="$normalize_space_Desc != ''">
    <xsl:text>///&lt;summary</xsl:text>&gt;
    <xsl:text>///</xsl:text>
    <xsl:value-of select="$normalize_space_Desc"/>.
    <xsl:text>///&lt;/summary&gt;</xsl:text>
    </xsl:if>
  </xsl:template>
  
  <xsl:template match="/">
    <xsl:call-template name="License" />
/*
 *
 * Конфігурації "<xsl:value-of select="Configuration/Name"/>"
 * Автор <xsl:value-of select="Configuration/Author"/>
 * Дата конфігурації: <xsl:value-of select="Configuration/DateTimeSave"/>
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
}

namespace <xsl:value-of select="Configuration/NameSpace"/>.Константи
{
    <xsl:for-each select="Configuration/ConstantsBlocks/ConstantsBlock">
    static class <xsl:value-of select="Name"/>_Block
    {
        <xsl:for-each select="Constants/Constant">
        <xsl:text>public static </xsl:text>
        <xsl:call-template name="FieldType" />
        <xsl:text> </xsl:text>
        <xsl:value-of select="Name"/>
        <xsl:text> { get; set; </xsl:text>}
        </xsl:for-each>
    }
    </xsl:for-each>
}

namespace <xsl:value-of select="Configuration/NameSpace"/>.Довідники
{
    <xsl:for-each select="Configuration/Directories/Directory">
      <xsl:variable name="DirectoryName" select="Name"/>
    #region DIRECTORY "<xsl:value-of select="$DirectoryName"/>"
    <!--
    class <xsl:value-of select="$DirectoryName"/>_Manager : DirectoryManager
    {
        public <xsl:value-of select="$DirectoryName"/>_Manager() : base(Config.Kernel, "<xsl:value-of select="Table"/>",
            <xsl:text>new string[] { </xsl:text>
            <xsl:for-each select="Fields/Field">
              <xsl:if test="position() != 1">
                <xsl:text>, </xsl:text>
              </xsl:if>
              <xsl:text>"</xsl:text><xsl:value-of select="NameInTable"/><xsl:text>"</xsl:text>
            </xsl:for-each> },
            <xsl:text>new string[] { </xsl:text>
            <xsl:for-each select="Fields/Field">
              <xsl:if test="position() != 1">
                <xsl:text>, </xsl:text>
              </xsl:if>
              <xsl:text>"</xsl:text><xsl:value-of select="Name"/><xsl:text>"</xsl:text>
            </xsl:for-each> }) { }

        public <xsl:value-of select="$DirectoryName"/>_Pointer FindByField(string name, object value)
        {
            <xsl:value-of select="$DirectoryName"/>_Pointer itemPointer = new <xsl:value-of select="$DirectoryName"/>_Pointer();
            DirectoryPointer directoryPointer = base.BaseFindByField(base.Alias[name], value);
            if (!directoryPointer.IsEmpty()) itemPointer.Init(directoryPointer.UnigueID);
            return itemPointer;
        }
    }
    -->
    <xsl:call-template name="CommentSummary" />
    class <xsl:value-of select="$DirectoryName"/>_Objest : DirectoryObject
    {
        public <xsl:value-of select="$DirectoryName"/>_Objest() : base(Config.Kernel, "<xsl:value-of select="Table"/>",
             <xsl:text>new string[] { </xsl:text>
             <xsl:for-each select="Fields/Field">
               <xsl:if test="position() != 1">
                 <xsl:text>, </xsl:text>
               </xsl:if>
               <xsl:text>"</xsl:text><xsl:value-of select="NameInTable"/><xsl:text>"</xsl:text>
             </xsl:for-each> }) 
        {
            <xsl:for-each select="Fields/Field">
              <xsl:value-of select="Name"/>
              <xsl:text> = </xsl:text>
              <xsl:call-template name="DefaultFieldValue" />;
            </xsl:for-each>
            <xsl:if test="count(TabularParts/TablePart) &gt; 0">
            //Табличні частини
            </xsl:if>
            <xsl:for-each select="TabularParts/TablePart">
                <xsl:variable name="TablePartName" select="concat(Name, '_TablePart')"/>
                <xsl:value-of select="$TablePartName"/><xsl:text> = new </xsl:text>
                <xsl:value-of select="concat($DirectoryName, '_', $TablePartName)"/><xsl:text>(this)</xsl:text>;
            </xsl:for-each>
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                <xsl:for-each select="Fields/Field">
                  <xsl:value-of select="Name"/>
                  <xsl:text> = </xsl:text>
                  <xsl:call-template name="ReadFieldValue">
                    <xsl:with-param name="BaseFieldContainer">base.FieldValue</xsl:with-param>
                  </xsl:call-template>;
                </xsl:for-each>
                BaseClear();
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            <xsl:for-each select="Fields/Field">
              <xsl:text>base.FieldValue["</xsl:text><xsl:value-of select="NameInTable"/><xsl:text>"] = </xsl:text>
              <xsl:if test="Type = 'enum'">
                  <xsl:text>(int)</xsl:text>      
              </xsl:if>
              <xsl:value-of select="Name"/>
              <xsl:choose>
                <xsl:when test="Type = 'pointer' or Type = 'empty_pointer'">
                  <xsl:text>.ToString()</xsl:text>
                </xsl:when>
              </xsl:choose>;
            </xsl:for-each>
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public <xsl:value-of select="$DirectoryName"/>_Pointer GetDirectoryPointer()
        {
            <xsl:value-of select="$DirectoryName"/>_Pointer directoryPointer = new <xsl:value-of select="$DirectoryName"/>_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        <xsl:for-each select="Fields/Field">
          <xsl:text>public </xsl:text>
          <xsl:call-template name="FieldType" />
          <xsl:text> </xsl:text>
          <xsl:value-of select="Name"/>
          <xsl:text> { get; set; </xsl:text>}
        </xsl:for-each>
        <xsl:if test="count(TabularParts/TablePart) &gt; 0">
        //Табличні частини
        </xsl:if>
        <xsl:for-each select="TabularParts/TablePart">
            <xsl:variable name="TablePartName" select="concat(Name, '_TablePart')"/>
            <xsl:text>public </xsl:text><xsl:value-of select="concat($DirectoryName, '_', $TablePartName)"/><xsl:text> </xsl:text>
            <xsl:value-of select="$TablePartName"/><xsl:text> { get; set; </xsl:text>}
        </xsl:for-each>
    }
    
    <xsl:call-template name="CommentSummary" />
    class <xsl:value-of select="$DirectoryName"/>_Pointer : DirectoryPointer
    {
        public <xsl:value-of select="$DirectoryName"/>_Pointer(object uid = null) : base(Config.Kernel, "<xsl:value-of select="Table"/>")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public <xsl:value-of select="$DirectoryName"/>_Pointer(UnigueID uid, Dictionary&lt;string, object&gt; fields = null) : base(Config.Kernel, "<xsl:value-of select="Table"/>")
        {
            base.Init(uid, fields);
        } 
        
        public <xsl:value-of select="$DirectoryName"/>_Objest GetDirectoryObject()
        {
            <xsl:value-of select="$DirectoryName"/>_Objest <xsl:value-of select="$DirectoryName"/>ObjestItem = new <xsl:value-of select="$DirectoryName"/>_Objest();
            <xsl:value-of select="$DirectoryName"/>ObjestItem.Read(base.UnigueID);
            return <xsl:value-of select="$DirectoryName"/>ObjestItem;
        }
    }
    
    <xsl:call-template name="CommentSummary" />
    class <xsl:value-of select="$DirectoryName"/>_Select : DirectorySelect, IDisposable
    {
        public <xsl:value-of select="$DirectoryName"/>_Select() : base(Config.Kernel, "<xsl:value-of select="Table"/>",
            <xsl:text>new string[] { </xsl:text>
            <xsl:for-each select="Fields/Field">
              <xsl:if test="position() != 1">
                <xsl:text>, </xsl:text>
              </xsl:if>
              <xsl:text>"</xsl:text><xsl:value-of select="NameInTable"/><xsl:text>"</xsl:text>
            </xsl:for-each> },
            <xsl:text>new string[] { </xsl:text>
            <xsl:for-each select="Fields/Field">
              <xsl:if test="position() != 1">
                <xsl:text>, </xsl:text>
              </xsl:if>
              <xsl:text>"</xsl:text><xsl:value-of select="Name"/><xsl:text>"</xsl:text>
            </xsl:for-each> }) { }
    
        public bool Select() 
        { 
            return base.BaseSelect();
        }
        
        public bool SelectSingle()
        {
            if (base.BaseSelectSingle())
            {
                MoveNext();
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }
        
        public bool MoveNext()
        {
            if (MoveToPosition())
            {
                Current = new <xsl:value-of select="$DirectoryName"/>_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields);
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }

        public <xsl:value-of select="$DirectoryName"/>_Pointer Current { get; private set; }
        
        public <xsl:value-of select="$DirectoryName"/>_Pointer FindByField(string name, object value)
        {
            <xsl:value-of select="$DirectoryName"/>_Pointer itemPointer = new <xsl:value-of select="$DirectoryName"/>_Pointer();
            DirectoryPointer directoryPointer = base.BaseFindByField(base.Alias[name], value);
            if (!directoryPointer.IsEmpty()) itemPointer.Init(directoryPointer.UnigueID);
            return itemPointer;
        }
    }
    
      <xsl:for-each select="TabularParts/TablePart"> <!-- TableParts -->
        <xsl:variable name="TablePartName" select="Name"/>
        <xsl:variable name="TablePartFullName" select="concat($DirectoryName, '_', $TablePartName)"/>
    
    <xsl:call-template name="CommentSummary" />
    class <xsl:value-of select="$TablePartFullName"/>_TablePart : DirectoryTablePart
    {
        public <xsl:value-of select="$TablePartFullName"/>_TablePart(<xsl:value-of select="$DirectoryName"/>_Objest owner) : base(Config.Kernel, "<xsl:value-of select="Table"/>",
             <xsl:text>new string[] { </xsl:text>
             <xsl:for-each select="Fields/Field">
               <xsl:if test="position() != 1">
                 <xsl:text>, </xsl:text>
               </xsl:if>
               <xsl:text>"</xsl:text><xsl:value-of select="NameInTable"/><xsl:text>"</xsl:text>
             </xsl:for-each> }) 
        {
            if (owner == null) throw new Exception("owner null");
            
            Owner = owner;
            Records = new List&lt;<xsl:value-of select="$TablePartFullName"/>_TablePartRecord&gt;();
        }
        
        public <xsl:value-of select="$DirectoryName"/>_Objest Owner { get; private set; }
        
        public List&lt;<xsl:value-of select="$TablePartFullName"/>_TablePartRecord&gt; Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary&lt;string, object&gt; fieldValue in base.FieldValueList) 
            {
                <xsl:value-of select="$TablePartFullName"/>_TablePartRecord record = new <xsl:value-of select="$TablePartFullName"/>_TablePartRecord();

                <xsl:for-each select="Fields/Field">
                  <xsl:text>record.</xsl:text>
                  <xsl:value-of select="Name"/>
                  <xsl:text> = </xsl:text>
                  <xsl:call-template name="ReadFieldValue">
                    <xsl:with-param name="BaseFieldContainer">fieldValue</xsl:with-param>
                  </xsl:call-template>;
                </xsl:for-each>
                Records.Add(record);
            }
            
            base.BaseClear();
        }
        
        /// &lt;summary&gt;
        /// Зберегти колекцію Records в базу.
        /// &lt;/summary&gt;
        /// &lt;param name="clear_all_before_save"&gt;
        /// Щоб не очищати всю колекцію в базі перед записом треба поставити clear_all_before_save = false.
        /// &lt;/param&gt;
        public void Save(bool clear_all_before_save /*= true*/) 
        {
            if (Records.Count > 0)
            {
                base.BaseBeginTransaction();
                
                if (clear_all_before_save)
                    base.BaseDelete(Owner.UnigueID);

                foreach (<xsl:value-of select="$TablePartFullName"/>_TablePartRecord record in Records)
                {
                    Dictionary&lt;string, object&gt; fieldValue = new Dictionary&lt;string, object&gt;();

                    <xsl:for-each select="Fields/Field">
                      <xsl:text>fieldValue.Add("</xsl:text>
                      <xsl:value-of select="NameInTable"/><xsl:text>", record.</xsl:text><xsl:value-of select="Name"/>
                      <xsl:choose>
                        <xsl:when test="Type = 'pointer' or Type = 'empty_pointer'">
                          <xsl:text>.ToString()</xsl:text>
                        </xsl:when>
                      </xsl:choose>
                      <xsl:text>)</xsl:text>;
                    </xsl:for-each>
                    base.BaseSave(Owner.UnigueID, fieldValue);
                }
                
                base.BaseCommitTransaction();
            }
        }
        
        public void Delete()
        {
            base.BaseBeginTransaction();
            base.BaseDelete(Owner.UnigueID);
            base.BaseCommitTransaction();
        }
    }
    
    <xsl:call-template name="CommentSummary" />
    class <xsl:value-of select="$TablePartFullName"/>_TablePartRecord : DirectoryTablePartRecord
    {
        public <xsl:value-of select="$TablePartFullName"/>_TablePartRecord()
        {
            <xsl:for-each select="Fields/Field">
              <xsl:value-of select="Name"/>
              <xsl:text> = </xsl:text>
              <xsl:call-template name="DefaultFieldValue" />;
            </xsl:for-each>
        }
        
        <xsl:if test="count(Fields/Field) > 0">
        public <xsl:value-of select="$TablePartFullName"/>_TablePartRecord(
            <xsl:for-each select="Fields/Field">
              <xsl:if test="position() != 1"><xsl:text>, </xsl:text></xsl:if>
              <xsl:call-template name="FieldType" />
              <xsl:if test="Type = 'date' or Type = 'datetime' or Type = 'time'">
                   <xsl:text>? </xsl:text>    
              </xsl:if>
              <xsl:text> _</xsl:text>
              <xsl:value-of select="Name"/>
              <xsl:text> = </xsl:text>
              <xsl:call-template name="DefaultParamValue" />
            </xsl:for-each>)
        {
            <xsl:for-each select="Fields/Field">
              <xsl:value-of select="Name"/>
              <xsl:text> = _</xsl:text>
              <xsl:value-of select="Name"/>
              <xsl:if test="Type = 'date' or Type = 'datetime' or Type = 'time' or Type = 'pointer' or Type = 'empty_pointer'">
                   <xsl:text> ?? </xsl:text>
                   <xsl:call-template name="DefaultFieldValue" />
              </xsl:if>;
            </xsl:for-each>
        }
        </xsl:if>
        
        <xsl:for-each select="Fields/Field">
          <xsl:text>public </xsl:text>
          <xsl:call-template name="FieldType" />
          <xsl:text> </xsl:text>
          <xsl:value-of select="Name"/>
          <xsl:text> { get; set; </xsl:text>}
        </xsl:for-each>
    }
      </xsl:for-each> <!-- TableParts -->
      
      <xsl:for-each select="Views/View"> <!-- Views -->
        <xsl:variable name="ViewsName" select="Name"/>
        <xsl:variable name="ViewsFullName" select="concat($DirectoryName, '_', $ViewsName)"/>
    
    <xsl:call-template name="CommentSummary" />
    class <xsl:value-of select="$ViewsFullName"/>_View : DirectoryView
    {
        public <xsl:value-of select="$ViewsFullName"/>_View() : base(Config.Kernel, "<xsl:value-of select="Table"/>", 
             <xsl:text>new string[] { </xsl:text>
             <xsl:for-each select="Fields/Field">
               <xsl:if test="position() != 1">
                 <xsl:text>, </xsl:text>
               </xsl:if>
               <xsl:text>"</xsl:text><xsl:value-of select="NameInTable"/><xsl:text>"</xsl:text>
             </xsl:for-each> },
             <xsl:text>new string[] { </xsl:text>
             <xsl:for-each select="Fields/Field">
               <xsl:if test="position() != 1">
                 <xsl:text>, </xsl:text>
               </xsl:if>
               <xsl:text>"</xsl:text><xsl:value-of select="Name"/><xsl:text>"</xsl:text>
             </xsl:for-each> },
             <xsl:text>new string[] { </xsl:text>
             <xsl:for-each select="Fields/Field">
               <xsl:if test="position() != 1">
                 <xsl:text>, </xsl:text>
               </xsl:if>
               <xsl:text>"</xsl:text><xsl:value-of select="Type"/><xsl:text>"</xsl:text>
             </xsl:for-each> },
             "Довідники.<xsl:value-of select="$ViewsFullName"/>")
        {
            <!--
            base.QuerySelect.PrimaryField = "<xsl:value-of select="PrimaryField"/>";
            -->
            <!--
            <xsl:for-each select="Where/Field">
              <xsl:text>Where_</xsl:text><xsl:value-of select="NameInTable"/><xsl:text> = new Where("</xsl:text><xsl:value-of select="NameInTable"/><xsl:text>", Comparison.EQ, null)</xsl:text>;
              <xsl:text>base.QuerySelect.Where.Add(Where_</xsl:text><xsl:value-of select="NameInTable"/><xsl:text>)</xsl:text>;
            </xsl:for-each>
            -->
        }
        <!--
        <xsl:for-each select="Where/Field">
          <xsl:text>public Where Where_</xsl:text><xsl:value-of select="NameInTable"/><xsl:text> { get; set; }</xsl:text>
        </xsl:for-each>
        -->
    }
      </xsl:for-each> <!-- Views -->
    
    #endregion
    </xsl:for-each>
}

namespace <xsl:value-of select="Configuration/NameSpace"/>.Перелічення
{
    <xsl:for-each select="Configuration/Enums/Enum">
    <xsl:call-template name="CommentSummary" />
    public enum <xsl:value-of select="Name"/>
    {
         <xsl:variable name="CountEnumField" select="count(Fields/Field)" />
         <xsl:for-each select="Fields/Field">
             <xsl:value-of select="Name"/>
             <xsl:text> = </xsl:text>
             <xsl:value-of select="Value"/>
             <xsl:if test="position() &lt; $CountEnumField">
         <xsl:text>,
         </xsl:text>
             </xsl:if>
         </xsl:for-each>
    }
    
    </xsl:for-each>
}

namespace <xsl:value-of select="Configuration/NameSpace"/>.Документи
{
    <xsl:for-each select="Configuration/Documents/Document">
      <xsl:variable name="DocumentName" select="Name"/>
    #region DOCUMENT "<xsl:value-of select="$DocumentName"/>"
    
    <xsl:call-template name="CommentSummary" />
    class <xsl:value-of select="$DocumentName"/>_Objest : DocumentObject
    {
        public <xsl:value-of select="$DocumentName"/>_Objest() : base(Config.Kernel, "<xsl:value-of select="Table"/>",
             <xsl:text>new string[] { </xsl:text>
             <xsl:for-each select="Fields/Field">
               <xsl:if test="position() != 1">
                 <xsl:text>, </xsl:text>
               </xsl:if>
               <xsl:text>"</xsl:text><xsl:value-of select="NameInTable"/><xsl:text>"</xsl:text>
             </xsl:for-each> }) 
        {
            <xsl:for-each select="Fields/Field">
              <xsl:value-of select="Name"/>
              <xsl:text> = </xsl:text>
              <xsl:call-template name="DefaultFieldValue" />;
            </xsl:for-each>
            <xsl:if test="count(TabularParts/TablePart) &gt; 0">
            //Табличні частини
            </xsl:if>
            <xsl:for-each select="TabularParts/TablePart">
                <xsl:variable name="TablePartName" select="concat(Name, '_TablePart')"/>
                <xsl:value-of select="$TablePartName"/><xsl:text> = new </xsl:text>
                <xsl:value-of select="concat($DocumentName, '_', $TablePartName)"/><xsl:text>(this)</xsl:text>;
            </xsl:for-each>
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                <xsl:for-each select="Fields/Field">
                  <xsl:value-of select="Name"/>
                  <xsl:text> = </xsl:text>
                  <xsl:call-template name="ReadFieldValue">
                    <xsl:with-param name="BaseFieldContainer">base.FieldValue</xsl:with-param>
                  </xsl:call-template>;
                </xsl:for-each>
                BaseClear();
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            <xsl:for-each select="Fields/Field">
              <xsl:text>base.FieldValue["</xsl:text><xsl:value-of select="NameInTable"/><xsl:text>"] = </xsl:text>
              <xsl:if test="Type = 'enum'">
                  <xsl:text>(int)</xsl:text>      
              </xsl:if>
              <xsl:value-of select="Name"/>
              <xsl:choose>
                <xsl:when test="Type = 'pointer' or Type = 'empty_pointer'">
                  <xsl:text>.ToString()</xsl:text>
                </xsl:when>
              </xsl:choose>;
            </xsl:for-each>
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public <xsl:value-of select="$DocumentName"/>_Pointer GetDocumentPointer()
        {
            <xsl:value-of select="$DocumentName"/>_Pointer directoryPointer = new <xsl:value-of select="$DocumentName"/>_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        <xsl:for-each select="Fields/Field">
          <xsl:text>public </xsl:text>
          <xsl:call-template name="FieldType" />
          <xsl:text> </xsl:text>
          <xsl:value-of select="Name"/>
          <xsl:text> { get; set; </xsl:text>}
        </xsl:for-each>
        <xsl:if test="count(TabularParts/TablePart) &gt; 0">
        //Табличні частини
        </xsl:if>
        <xsl:for-each select="TabularParts/TablePart">
            <xsl:variable name="TablePartName" select="concat(Name, '_TablePart')"/>
            <xsl:text>public </xsl:text><xsl:value-of select="concat($DocumentName, '_', $TablePartName)"/><xsl:text> </xsl:text>
            <xsl:value-of select="$TablePartName"/><xsl:text> { get; set; </xsl:text>}
        </xsl:for-each>
    }
    
    <xsl:call-template name="CommentSummary" />
    class <xsl:value-of select="$DocumentName"/>_Pointer : DocumentPointer
    {
        public <xsl:value-of select="$DocumentName"/>_Pointer(object uid = null) : base(Config.Kernel, "<xsl:value-of select="Table"/>")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public <xsl:value-of select="$DocumentName"/>_Pointer(UnigueID uid, Dictionary&lt;string, object&gt; fields = null) : base(Config.Kernel, "<xsl:value-of select="Table"/>")
        {
            base.Init(uid, fields);
        } 
        
        public <xsl:value-of select="$DocumentName"/>_Objest GetDocumentObject()
        {
            <xsl:value-of select="$DocumentName"/>_Objest <xsl:value-of select="$DocumentName"/>ObjestItem = new <xsl:value-of select="$DocumentName"/>_Objest();
            <xsl:value-of select="$DocumentName"/>ObjestItem.Read(base.UnigueID);
            return <xsl:value-of select="$DocumentName"/>ObjestItem;
        }
    }
    
    <xsl:call-template name="CommentSummary" />
    class <xsl:value-of select="$DocumentName"/>_Select : DocumentSelect, IDisposable
    {
        public <xsl:value-of select="$DocumentName"/>_Select() : base(Config.Kernel, "<xsl:value-of select="Table"/>") { }
    
        public bool Select() 
        { 
            return base.BaseSelect();
        }
        
        public bool SelectSingle()
        {
            if (base.BaseSelectSingle())
            {
                MoveNext();
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }
        
        public bool MoveNext()
        {
            if (MoveToPosition())
            {
                Current = new <xsl:value-of select="$DocumentName"/>_Pointer(base.DocumentPointerPosition.UnigueID, base.DocumentPointerPosition.Fields);
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }

        public <xsl:value-of select="$DocumentName"/>_Pointer Current { get; private set; }
    }
    
      <xsl:for-each select="TabularParts/TablePart"> <!-- TableParts -->
        <xsl:variable name="TablePartName" select="Name"/>
        <xsl:variable name="TablePartFullName" select="concat($DocumentName, '_', $TablePartName)"/>
    
    <xsl:call-template name="CommentSummary" />
    class <xsl:value-of select="$TablePartFullName"/>_TablePart : DocumentTablePart
    {
        public <xsl:value-of select="$TablePartFullName"/>_TablePart(<xsl:value-of select="$DocumentName"/>_Objest owner) : base(Config.Kernel, "<xsl:value-of select="Table"/>",
             <xsl:text>new string[] { </xsl:text>
             <xsl:for-each select="Fields/Field">
               <xsl:if test="position() != 1">
                 <xsl:text>, </xsl:text>
               </xsl:if>
               <xsl:text>"</xsl:text><xsl:value-of select="NameInTable"/><xsl:text>"</xsl:text>
             </xsl:for-each> }) 
        {
            if (owner == null) throw new Exception("owner null");
            
            Owner = owner;
            Records = new List&lt;<xsl:value-of select="$TablePartFullName"/>_TablePartRecord&gt;();
        }
        
        public <xsl:value-of select="$DocumentName"/>_Objest Owner { get; private set; }
        
        public List&lt;<xsl:value-of select="$TablePartFullName"/>_TablePartRecord&gt; Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary&lt;string, object&gt; fieldValue in base.FieldValueList) 
            {
                <xsl:value-of select="$TablePartFullName"/>_TablePartRecord record = new <xsl:value-of select="$TablePartFullName"/>_TablePartRecord();

                <xsl:for-each select="Fields/Field">
                  <xsl:text>record.</xsl:text>
                  <xsl:value-of select="Name"/>
                  <xsl:text> = </xsl:text>
                  <xsl:call-template name="ReadFieldValue">
                    <xsl:with-param name="BaseFieldContainer">fieldValue</xsl:with-param>
                  </xsl:call-template>;
                </xsl:for-each>
                Records.Add(record);
            }
            
            base.BaseClear();
        }
        
        /// &lt;summary&gt;
        /// Зберегти колекцію Records в базу.
        /// &lt;/summary&gt;
        /// &lt;param name="clear_all_before_save"&gt;
        /// Щоб не очищати всю колекцію в базі перед записом треба поставити clear_all_before_save = false.
        /// &lt;/param&gt;
        public void Save(bool clear_all_before_save /*= true*/) 
        {
            if (Records.Count > 0)
            {
                base.BaseBeginTransaction();
                
                if (clear_all_before_save)
                    base.BaseDelete(Owner.UnigueID);

                foreach (<xsl:value-of select="$TablePartFullName"/>_TablePartRecord record in Records)
                {
                    Dictionary&lt;string, object&gt; fieldValue = new Dictionary&lt;string, object&gt;();

                    <xsl:for-each select="Fields/Field">
                      <xsl:text>fieldValue.Add("</xsl:text>
                      <xsl:value-of select="NameInTable"/><xsl:text>", record.</xsl:text><xsl:value-of select="Name"/>
                      <xsl:choose>
                        <xsl:when test="Type = 'pointer'">
                          <xsl:text>.UnigueID.UGuid</xsl:text>
                        </xsl:when>
                        <xsl:when test="Type = 'empty_pointer'">
                          <xsl:text>.UnigueID.UGuid</xsl:text>
                        </xsl:when>
                      </xsl:choose>
                      <xsl:text>)</xsl:text>;
                    </xsl:for-each>
                    base.BaseSave(Owner.UnigueID, fieldValue);
                }
                
                base.BaseCommitTransaction();
            }
        }
        
        public void Delete()
        {
            base.BaseBeginTransaction();
            base.BaseDelete(Owner.UnigueID);
            base.BaseCommitTransaction();
        }
    }
    
    <xsl:call-template name="CommentSummary" />
    class <xsl:value-of select="$TablePartFullName"/>_TablePartRecord : DocumentTablePartRecord
    {
        public <xsl:value-of select="$TablePartFullName"/>_TablePartRecord()
        {
            <xsl:for-each select="Fields/Field">
              <xsl:value-of select="Name"/>
              <xsl:text> = </xsl:text>
              <xsl:call-template name="DefaultFieldValue" />;
            </xsl:for-each>
        }
        
        <xsl:if test="count(Fields/Field) > 0">
        public <xsl:value-of select="$TablePartFullName"/>_TablePartRecord(
            <xsl:for-each select="Fields/Field">
              <xsl:if test="position() != 1"><xsl:text>, </xsl:text></xsl:if>
              <xsl:call-template name="FieldType" />
              <xsl:if test="Type = 'date' or Type = 'datetime' or Type = 'time'">
                   <xsl:text>? </xsl:text>    
              </xsl:if>
              <xsl:text> _</xsl:text>
              <xsl:value-of select="Name"/>
              <xsl:text> = </xsl:text>
              <xsl:call-template name="DefaultParamValue" />
            </xsl:for-each>)
        {
            <xsl:for-each select="Fields/Field">
              <xsl:value-of select="Name"/>
              <xsl:text> = _</xsl:text>
              <xsl:value-of select="Name"/>
              <xsl:if test="Type = 'date' or Type = 'datetime' or Type = 'time' or Type = 'pointer' or Type = 'empty_pointer'">
                   <xsl:text> ?? </xsl:text>
                   <xsl:call-template name="DefaultFieldValue" />
              </xsl:if>;
            </xsl:for-each>
        }
        </xsl:if>
        
        <xsl:for-each select="Fields/Field">
          <xsl:text>public </xsl:text>
          <xsl:call-template name="FieldType" />
          <xsl:text> </xsl:text>
          <xsl:value-of select="Name"/>
          <xsl:text> { get; set; </xsl:text>}
        </xsl:for-each>
    }
      </xsl:for-each> <!-- TableParts -->
    
    #endregion
    </xsl:for-each>
}

namespace <xsl:value-of select="Configuration/NameSpace"/>.Журнали
{

}

namespace <xsl:value-of select="Configuration/NameSpace"/>.РегістриВідомостей
{
    <xsl:for-each select="Configuration/RegistersInformation/RegisterInformation">
       <xsl:variable name="RegisterName" select="Name"/>
    #region REGISTER "<xsl:value-of select="$RegisterName"/>"
    
    <xsl:call-template name="CommentSummary" />
    class <xsl:value-of select="$RegisterName"/>_RecordsSet : RegisterRecordsSet
    {
        public <xsl:value-of select="$RegisterName"/>_RecordsSet() : base(Config.Kernel, "<xsl:value-of select="Table"/>",
             <xsl:text>new string[] { </xsl:text>
             <xsl:for-each select="(DimensionFields|ResourcesFields|PropertyFields)/Fields/Field">
               <xsl:if test="position() != 1">
                 <xsl:text>, </xsl:text>
               </xsl:if>
               <!--<xsl:value-of select="name(../..)"/>-->
               <xsl:text>"</xsl:text><xsl:value-of select="NameInTable"/><xsl:text>"</xsl:text>
             </xsl:for-each>}) 
        {
            Records = new List&lt;<xsl:value-of select="$RegisterName"/>_Record&gt;();
            Filter = new <xsl:value-of select="$RegisterName"/>_Filter();
        }
                
        public List&lt;<xsl:value-of select="$RegisterName"/>_Record&gt; Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            
            <xsl:variable name="DimensionFieldsCount" select="count(DimensionFields/Fields/Field)" />
            <xsl:if test="$DimensionFieldsCount &gt; 1">bool isExistPreceding = false;</xsl:if> //Чи є попередні фільтри
            
            <xsl:for-each select="DimensionFields/Fields/Field">
              <xsl:text>if (Filter.</xsl:text>
              <xsl:value-of select="Name"/>
              <xsl:text> != </xsl:text>
              <xsl:call-template name="DefaultFieldValue" />) {
              <xsl:choose>
                <xsl:when test="position() = 1">
                  <xsl:text>base.BaseFilter.Add(new Where("</xsl:text>
                  <xsl:value-of select="NameInTable"/>
                  <xsl:text>", Comparison.EQ, Filter.</xsl:text>
                  <xsl:value-of select="Name"/>
                  <xsl:text>, false))</xsl:text>;
                  <xsl:if test="$DimensionFieldsCount &gt; 1">
                  <xsl:text>isExistPreceding = true</xsl:text>;
                  </xsl:if>
                </xsl:when>
                <xsl:otherwise>
                  <xsl:text>if (isExistPreceding</xsl:text>)
                  <xsl:text>base.BaseFilter.Add(new Where(Comparison.AND, "</xsl:text>
                  <xsl:value-of select="NameInTable"/>
                  <xsl:text>", Comparison.EQ, Filter.</xsl:text>
                  <xsl:value-of select="Name"/>
                  <xsl:text>, false))</xsl:text>;
                  <xsl:text>else</xsl:text> {
                  <xsl:text>base.BaseFilter.Add(new Where("</xsl:text>
                  <xsl:value-of select="NameInTable"/>
                  <xsl:text>", Comparison.EQ, Filter.</xsl:text>
                  <xsl:value-of select="Name"/>
                  <xsl:text>, false))</xsl:text>;
                  <xsl:text>isExistPreceding = true</xsl:text>; }
                </xsl:otherwise>
              </xsl:choose>}
            </xsl:for-each>

            base.BaseRead();
            
            foreach (Dictionary&lt;string, object&gt; fieldValue in base.FieldValueList) 
            {
                <xsl:value-of select="$RegisterName"/>_Record record = new <xsl:value-of select="$RegisterName"/>_Record();

                <xsl:for-each select="(DimensionFields|ResourcesFields|PropertyFields)/Fields/Field">
                  <xsl:text>record.</xsl:text>
                  <xsl:value-of select="Name"/>
                  <xsl:text> = </xsl:text>
                  <xsl:call-template name="ReadFieldValue">
                    <xsl:with-param name="BaseFieldContainer">fieldValue</xsl:with-param>
                  </xsl:call-template>;
                </xsl:for-each>
                Records.Add(record);
            }
            
            base.BaseClear();
        }
        
        public void Save(bool clear_all_before_save = true) 
        {
            if (Records.Count > 0)
            {
                base.BaseBeginTransaction();
                
                if (clear_all_before_save)
                    base.BaseDelete();

                foreach (<xsl:value-of select="$RegisterName"/>_Record record in Records)
                {
                    Dictionary&lt;string, object&gt; fieldValue = new Dictionary&lt;string, object&gt;();

                    <xsl:for-each select="(DimensionFields|ResourcesFields|PropertyFields)/Fields/Field">
                      <xsl:text>fieldValue.Add("</xsl:text>
                      <xsl:value-of select="NameInTable"/><xsl:text>", record.</xsl:text><xsl:value-of select="Name"/>
                      <xsl:choose>
                        <xsl:when test="Type = 'pointer' or Type = 'empty_pointer'">
                          <xsl:text>.ToString()</xsl:text>
                        </xsl:when>
                      </xsl:choose>
                      <xsl:text>)</xsl:text>;
                    </xsl:for-each>
                    base.BaseSave(fieldValue);
                }
                
                base.BaseCommitTransaction();
            }
        }
        
        public void Delete()
        {
            base.BaseBeginTransaction();
            base.BaseDelete();
            base.BaseCommitTransaction();
        }
        
        public <xsl:value-of select="$RegisterName"/>_Filter Filter { get; set; }
    }
    
    <xsl:call-template name="CommentSummary" />
    class <xsl:value-of select="$RegisterName"/>_Record
    {
        public <xsl:value-of select="$RegisterName"/>_Record()
        {
            <xsl:for-each select="(DimensionFields|ResourcesFields|PropertyFields)/Fields/Field">
              <xsl:value-of select="Name"/>
              <xsl:text> = </xsl:text>
              <xsl:call-template name="DefaultFieldValue" />;
            </xsl:for-each>
        }
        
        <xsl:for-each select="(DimensionFields|ResourcesFields|PropertyFields)/Fields/Field">
          <xsl:text>public </xsl:text>
          <xsl:call-template name="FieldType" />
          <xsl:text> </xsl:text>
          <xsl:value-of select="Name"/>
          <xsl:text> { get; set; </xsl:text>}
        </xsl:for-each>
    }
    
    class <xsl:value-of select="$RegisterName"/>_Filter
    {        
        <xsl:for-each select="DimensionFields/Fields/Field">
          <xsl:text>public </xsl:text>
          <xsl:call-template name="FieldType" />
          <xsl:text> </xsl:text>
          <xsl:value-of select="Name"/>
          <xsl:text> { get; set; </xsl:text>}
        </xsl:for-each>
    }
    #endregion
  </xsl:for-each>
}

namespace <xsl:value-of select="Configuration/NameSpace"/>.РегістриНакопичення
{
  <xsl:for-each select="Configuration/RegistersAccumulation/RegisterAccumulation">
    <xsl:variable name="RegisterName" select="Name"/>
    #region REGISTER "<xsl:value-of select="$RegisterName"/>"
    <xsl:call-template name="CommentSummary" />
    class <xsl:value-of select="$RegisterName"/>_Record
    {
        public <xsl:value-of select="$RegisterName"/>_Record()
        {
            <xsl:for-each select="(DimensionFields|ResourcesFields|PropertyFields)/Fields/Field">
              <xsl:value-of select="Name"/>
              <xsl:text> = </xsl:text>
              <xsl:call-template name="DefaultFieldValue" />;
            </xsl:for-each>
        }
        
        <xsl:for-each select="(DimensionFields|ResourcesFields|PropertyFields)/Fields/Field">
          <xsl:text>public </xsl:text>
          <xsl:call-template name="FieldType" />
          <xsl:text> </xsl:text>
          <xsl:value-of select="Name"/>
          <xsl:text> { get; set; </xsl:text>}
        </xsl:for-each>
    }
    #endregion
  </xsl:for-each>
}
  </xsl:template>
</xsl:stylesheet>
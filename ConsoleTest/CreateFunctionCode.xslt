<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="text" indent="yes"/>

  <xsl:param name="XmlHeap" />
  <xsl:param name="DirectoryName" />

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
Сайт:     accounting.org.ua
*/
  </xsl:template>
  
  <xsl:template match="View">
    <xsl:variable name="FullViewName" select="concat($DirectoryName, '_', Name, '_View')" />
    <xsl:variable name="CountPointerField" select="count(Fields/Field[Type = 'pointer'])" />
    <xsl:call-template name="License" />
    
using System.Text;
using System.Collections.Generic;

using AccountingSoftware;
using Conf = ConfTrade_v1_1;
using Довідники = ConfTrade_v1_1.Довідники;    

namespace ConfTrade
{
    public partial class ConfTrade
    {
        public static string Run()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<xsl:text disable-output-escaping="yes">&lt;root&gt;</xsl:text>");
            
            <xsl:value-of select="$FullViewName" /> m_<xsl:value-of select="position()" /> = new <xsl:value-of select="$FullViewName" />();
            
            <xsl:if test="$CountPointerField > 0">
              
            <xsl:text>m_</xsl:text>
            <xsl:value-of select="position()" />.QuerySelect.CreateTempTable = true;
            <xsl:text disable-output-escaping="yes">Dictionary&lt;string, string&gt; Alias = m_</xsl:text>
            <xsl:value-of select="position()" />.Alias;
              
            </xsl:if>

            <xsl:text>sb.Append(m_</xsl:text>
            <xsl:value-of select="position()" />.Read());
            
            <xsl:if test="$CountPointerField > 0">
              
            string TempTable = m_<xsl:value-of select="position()" />.QuerySelect.TempTable;
              
            </xsl:if>
            
            <xsl:for-each select="Fields/Field">

              <xsl:choose>
                <xsl:when test="Type = 'pointer'">
                  
            <xsl:variable name="FullFieldViewName" select="concat(Pointer, '_Список_View')" />
            <xsl:value-of select="$FullFieldViewName" /> m_<xsl:value-of select="position()" /> = new <xsl:value-of select="$FullFieldViewName" />();
            m_<xsl:value-of select="position()" />.QuerySelect.Where.Add(new Where("uid", Comparison.IN, "SELECT DISTINCT " + Alias["<xsl:value-of select="Name" />"] + " FROM " + TempTable, true));
            sb.Append(m_<xsl:value-of select="position()" />.Read());
            
                </xsl:when>
              </xsl:choose>
    
            </xsl:for-each>

            <xsl:if test="normalize-space($XmlHeap) != ''">
            sb.Append(@"<xsl:value-of select="$XmlHeap"/>");
            </xsl:if>
            sb.Append("<xsl:text disable-output-escaping="yes">&lt;/root&gt;</xsl:text>");
            return sb.ToString();
        }
    }
}
  </xsl:template>

</xsl:stylesheet>

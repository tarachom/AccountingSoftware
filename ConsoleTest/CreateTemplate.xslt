<?xml version="1.0" encoding="utf-8"?>
<!--
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
-->
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="html" indent="yes"/>
  
  <xsl:param name="DirectoryName" />

  <xsl:template match="View">
    <xsl:text disable-output-escaping="yes">
&lt;xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" &gt;

   &lt;xsl:output method="html" indent="yes" /&gt;
   &lt;xsl:include  href="../Function.xslt" /&gt;                 
   
   &lt;xsl:template match="/"&gt;
    </xsl:text>

    <html>
      <title>HTML</title>
      <body>

        <table border="1">

      <xsl:text disable-output-escaping="yes">
      &lt;xsl:for-each select="</xsl:text>
          <xsl:value-of select="concat('root/', $DirectoryName, '_', Name, '/row')"/>
          <xsl:text disable-output-escaping="yes">"&gt;</xsl:text>
          <xsl:text>
      </xsl:text>

          <tr>
            <xsl:for-each select="Fields/Field">
              <td>
                <xsl:choose>
                  
                  <xsl:when test="Type = 'pointer'">
                    <xsl:text disable-output-escaping="yes">&lt;xsl:call-template name="GetNameOd"&gt;</xsl:text>
                    <xsl:text disable-output-escaping="yes">&lt;xsl:with-param name="list" select="/root/</xsl:text>
                    <xsl:value-of select="Pointer"/>
                    <xsl:text>_Список</xsl:text>
                    <xsl:text disable-output-escaping="yes">" /&gt;</xsl:text>
                    <xsl:text disable-output-escaping="yes">&lt;xsl:with-param name="uid" select="</xsl:text>
                    <xsl:value-of select="Name"/>
                    <xsl:text disable-output-escaping="yes">" /&gt;</xsl:text>
                    <xsl:text disable-output-escaping="yes">&lt;/xsl:call-template&gt;</xsl:text>
                  </xsl:when>
                  
                  <xsl:when test="Type = 'empty_pointer'">
                    <xsl:text> </xsl:text>
                  </xsl:when>
                  
                  <xsl:when test="Type = 'enum'">
                    <xsl:text disable-output-escaping="yes">&lt;xsl:call-template name="GetNameSelect"&gt;</xsl:text>
                    <xsl:text disable-output-escaping="yes">&lt;xsl:with-param name="pointer"&gt;</xsl:text>
                    <xsl:value-of select="Pointer"/>
                   <xsl:text disable-output-escaping="yes">&lt;/xsl:with-param&gt;</xsl:text>
                    <xsl:text disable-output-escaping="yes">&lt;xsl:with-param name="value" select="</xsl:text>
                    <xsl:value-of select="Name"/>
                    <xsl:text disable-output-escaping="yes">" /&gt;</xsl:text>
                    <xsl:text disable-output-escaping="yes">&lt;/xsl:call-template&gt;</xsl:text>
                  </xsl:when>
                  
                  <xsl:otherwise>
                    <xsl:text disable-output-escaping="yes">&lt;xsl:value-of select="</xsl:text>
                    <xsl:value-of select="Name"/>
                    <xsl:text disable-output-escaping="yes">"/&gt;</xsl:text>
                  </xsl:otherwise>
                
                </xsl:choose>
              </td>
            </xsl:for-each>
          </tr>

      <xsl:text disable-output-escaping="yes">
      &lt;/xsl:for-each&gt;
      </xsl:text>

        </table>
      </body>
    </html>
    
    <xsl:text disable-output-escaping="yes">
   &lt;/xsl:template&gt;
&lt;/xsl:stylesheet&gt;
    </xsl:text>

  </xsl:template>
</xsl:stylesheet>
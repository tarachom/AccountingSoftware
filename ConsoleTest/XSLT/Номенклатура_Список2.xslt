﻿
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" >

   <xsl:output method="html" indent="yes" />
   

   
   <xsl:template name="GetNameOd">
    <xsl:param name="list" />
    <xsl:param name="uid" />
    <xsl:for-each select="$list/row[uid = $uid]">
      <xsl:value-of select="Назва"/>
    </xsl:for-each>    
   </xsl:template>

   
   <xsl:template name="GetNameSelect">
     <xsl:param name="pointer" />
     <xsl:param name="value" />
     <select>
     <xsl:for-each select="/root/Enums/Enum[Name = $pointer]/Fields/Field">
        <option>
          <xsl:attribute name="value">
            <xsl:value-of select="Value"/>
          </xsl:attribute>
          <xsl:if test="$value = Value">
             <xsl:attribute name="selected">selected</xsl:attribute>          
          </xsl:if>
          <xsl:value-of select="Name"/>
       </option>
    </xsl:for-each>
    </select>
   </xsl:template>
                      
  
   
   <xsl:template match="/">
    <html>
  <title>HTML</title>
  <body>
    <table border="1">
      <xsl:for-each select="root/Номенклатура_Список2/row">
      <tr>
        <td> </td>
        <td><xsl:value-of select="Артикул"/></td>
        <td><xsl:call-template name="GetNameOd"><xsl:with-param name="list" select="/root/КлассификаторЕдИзм_Список" /><xsl:with-param name="uid" select="БазоваяЕдиница" /></xsl:call-template></td>
        <td><xsl:value-of select="Вес"/></td>
        <td><xsl:call-template name="GetNameOd"><xsl:with-param name="list" select="/root/Валюти_Список" /><xsl:with-param name="uid" select="ВалютаУчета" /></xsl:call-template></td>
        <td><xsl:value-of select="Назва"/></td>
        <td><xsl:value-of select="Код"/></td>
        <td><xsl:call-template name="GetNameOd"><xsl:with-param name="list" select="/root/Групи_Номенклатура_Список" /><xsl:with-param name="uid" select="Група" /></xsl:call-template></td>
      </tr>
      </xsl:for-each>
      </table>
  </body>
</html>
   </xsl:template>
</xsl:stylesheet>
    

<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" >

   <xsl:output method="html" indent="yes" />
   <xsl:include href="Include.xslt" />

   <xsl:template match="/">

    <html>
  <title>HTML</title>
  <body>
    <table border="1">
      <xsl:for-each select="root/Фирми_Список/row">
      <tr>
        <td><xsl:value-of select="Назва"/></td>
        <td><xsl:call-template name="GetNameOd"><xsl:with-param name="list" select="/root/Сотрудники_Список" /><xsl:with-param name="uid" select="Руководитель" /></xsl:call-template></td>
        <td><xsl:value-of select="Телефони"/></td>
        <td><xsl:value-of select="ПочтовийАдрес"/></td>
      </tr>
      </xsl:for-each>
      </table>
  </body>
</html>

   </xsl:template>
</xsl:stylesheet>
    
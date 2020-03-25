
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" >

   <xsl:output method="html" indent="yes" />
   <xsl:include  href="../Function.xslt" />                 
   
   <xsl:template match="/">
    <html>
  <title>HTML</title>
  <body>
    <table border="1">
      <xsl:for-each select="root/Довідники.Номенклатура_Список/row">
      <tr>
        <td><xsl:value-of select="Назва"/></td>
        <td><xsl:value-of select="Код"/></td>
        <td><xsl:value-of select="Ціна"/></td>
        <td><xsl:value-of select="Кво"/></td>
        <td><xsl:value-of select="ДатаСтворення"/></td>
        <td><xsl:call-template name="GetNameSelect"><xsl:with-param name="pointer">Перелічення.Test</xsl:with-param><xsl:with-param name="value" select="Перелічення1" /></xsl:call-template></td>
        <td><xsl:call-template name="GetNameOd"><xsl:with-param name="list" select="/root/Довідники.Test_Список" /><xsl:with-param name="uid" select="Вказівник" /></xsl:call-template></td>
      </tr>
      </xsl:for-each>
      </table>
  </body>
</html>
   </xsl:template>
</xsl:stylesheet>
    
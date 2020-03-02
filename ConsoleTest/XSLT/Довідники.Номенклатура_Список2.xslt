
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" >

   <xsl:output method="html" indent="yes" />
   <xsl:include  href="../Function.xslt" />                 
   
   <xsl:template match="/">
    <html>
  <title>HTML</title>
  <body>
    <table border="1">
      <xsl:for-each select="root/Довідники.Номенклатура_Список2/row">
      <tr>
        <td><xsl:call-template name="GetNameSelect"><xsl:with-param name="pointer">Перелічення.ВидиТоварів</xsl:with-param><xsl:with-param name="value" select="ВидТовара" /></xsl:call-template></td>
        <td><xsl:value-of select="Артикул"/></td>
        <td><xsl:call-template name="GetNameOd"><xsl:with-param name="list" select="/root/Довідники.КлассификаторЕдИзм_Список" /><xsl:with-param name="uid" select="БазоваяЕдиница" /></xsl:call-template></td>
        <td><xsl:value-of select="Вес"/></td>
        <td><xsl:call-template name="GetNameOd"><xsl:with-param name="list" select="/root/Довідники.Валюти_Список" /><xsl:with-param name="uid" select="ВалютаУчета" /></xsl:call-template></td>
        <td><xsl:value-of select="Назва"/></td>
        <td><xsl:value-of select="Код"/></td>
        <td><xsl:call-template name="GetNameOd"><xsl:with-param name="list" select="/root/Довідники.Групи_Номенклатура_Список" /><xsl:with-param name="uid" select="Група" /></xsl:call-template></td>
      </tr>
      </xsl:for-each>
      </table>
  </body>
</html>
   </xsl:template>
</xsl:stylesheet>
    
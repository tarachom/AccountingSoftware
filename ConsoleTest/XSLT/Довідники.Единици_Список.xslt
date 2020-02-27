
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" >

   <xsl:output method="html" indent="yes" />
   <xsl:include  href="../Function.xslt" />                 
   
   <xsl:template match="/">
    <html>
  <title>HTML</title>
  <body>
    <table border="1">
      <xsl:for-each select="root/Довідники.Единици_Список/row">
      <tr>
        <td><xsl:call-template name="GetNameOd"><xsl:with-param name="list" select="/root/Довідники.КлассификаторЕдИзм_Список" /><xsl:with-param name="uid" select="Единица" /></xsl:call-template></td>
        <td><xsl:value-of select="Назва"/></td>
      </tr>
      </xsl:for-each>
      </table>
  </body>
</html>
   </xsl:template>
</xsl:stylesheet>
    
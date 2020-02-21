
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" >

   <xsl:output method="html" indent="yes" />
   
   <xsl:template name="GetNameOd">
    <xsl:param name="list" />
    <xsl:param name="uid" />

    <xsl:for-each select="$list/row[uid = $uid]">
      <xsl:value-of select="Назва"/>
    </xsl:for-each>
    
   </xsl:template>

   <xsl:template match="/">

    <html>
  <title>HTML</title>
  <body>
    <table border="1">
      <xsl:for-each select="root/МестаХранения_Список/row">
      <tr>
        <td><xsl:value-of select="Назва"/></td>
      </tr>
      </xsl:for-each>
      </table>
  </body>
</html>
   </xsl:template>
</xsl:stylesheet>
    

<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" >

   <xsl:output method="html" indent="yes" />
   <xsl:include href="Include.xslt" />

   <xsl:template match="/">

    <table>
      <xsl:for-each select="Групи_МестаХранения_Список/Field">
      <tr>
    <td><xsl:value-of select="Назва"/></td>
    <td><xsl:value-of select="Код"/></td>
    <td><xsl:call-template name="GetNameOd"><xsl:with-param name="list">МестаХранения_Список</xsl:with-param><xsl:with-param name="uid" select="Родитель" /></xsl:call-template></td>
  </tr>
      </xsl:for-each>
      </table>

   </xsl:template>
</xsl:stylesheet>
    
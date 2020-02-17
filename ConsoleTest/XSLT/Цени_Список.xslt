
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" >

   <xsl:output method="html" indent="yes" />
   <xsl:include href="Include.xslt" />

   <xsl:template match="/">

    <table>
      <xsl:for-each select="Цени_Список/Field">
      <tr>
    <td><xsl:value-of select="Назва"/></td>
    <td><xsl:call-template name="GetNameOd"><xsl:with-param name="list">Валюти_Список</xsl:with-param><xsl:with-param name="uid" select="Валюта" /></xsl:call-template></td>
    <td><xsl:call-template name="GetNameOd"><xsl:with-param name="list">КатегорииЦен_Список</xsl:with-param><xsl:with-param name="uid" select="КатегорияЦени" /></xsl:call-template></td>
    <td><xsl:value-of select="Наценка"/></td>
  </tr>
      </xsl:for-each>
      </table>

   </xsl:template>
</xsl:stylesheet>
    
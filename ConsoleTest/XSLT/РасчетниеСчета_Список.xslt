﻿
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" >

   <xsl:output method="html" indent="yes" />
   <xsl:include href="Include.xslt" />

   <xsl:template match="/">

    <html>
  <title>HTML</title>
  <body>
    <table border="1">
      <xsl:for-each select="root/РасчетниеСчета_Список/row">
      <tr>
        <td><xsl:value-of select="Назва"/></td>
        <td><xsl:value-of select="БанкНазвание"/></td>
      </tr>
      </xsl:for-each>
      </table>
  </body>
</html>

   </xsl:template>
</xsl:stylesheet>
    
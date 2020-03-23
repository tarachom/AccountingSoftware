<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="html" indent="yes"/>

  <xsl:param name="ConfObject" />
  <xsl:param name="Cmd" />

  <xsl:param name="Offset" />
  <xsl:param name="Limit" />
  
  <xsl:template match="/">

    <html>
      <title>HTML</title>
      <body>

        <h1>Довідник Номенклатура</h1>

        <table border="1">
          <xsl:for-each select="root/Довідники.Номенклатура_Список/row">
            <tr>
              <td>
                <xsl:value-of select="Назва"/>
              </td>
              <td>
                <xsl:value-of select="Код"/>
              </td>
            </tr>
          </xsl:for-each>
        </table>
      </body>
    </html>
    
  </xsl:template>

</xsl:stylesheet>

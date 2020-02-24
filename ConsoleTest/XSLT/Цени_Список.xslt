<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" >

  <xsl:output method="html" indent="yes" />
  <xsl:include  href="../Function.xslt" />


  <xsl:template match="/">
    <html>
      <title>HTML</title>
      <body>
        <table border="1">
          <xsl:for-each select="root/Цени_Список/row">
            <tr>
              <td>
                <xsl:value-of select="Назва"/>
              </td>
              <td>
                <xsl:call-template name="GetNameOd">
                  <xsl:with-param name="list" select="/root/Валюти_Список" />
                  <xsl:with-param name="uid" select="Валюта" />
                </xsl:call-template>
              </td>
              <td>
                <xsl:call-template name="GetNameOd">
                  <xsl:with-param name="list" select="/root/КатегорииЦен_Список" />
                  <xsl:with-param name="uid" select="КатегорияЦени" />
                </xsl:call-template>
              </td>
              <td>
                <xsl:value-of select="Наценка"/>
              </td>
            </tr>
          </xsl:for-each>
        </table>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>

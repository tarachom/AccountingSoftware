<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" >

  <xsl:output method="html" indent="yes" />
  <xsl:include  href="../Function.xslt" />


  <xsl:template match="/">
    <html>
      <title>HTML</title>
      <body>
        <table border="1">
          <xsl:for-each select="root/test_Список/row">
            <tr>
              <td>
                <xsl:value-of select="Назва"/>
              </td>
              <td>
                <xsl:value-of select="Код"/>
              </td>
              <td>
                <xsl:call-template name="GetNameSelect">
                  <xsl:with-param name="pointer">Перелічення2</xsl:with-param>
                  <xsl:with-param name="value" select="ТипПоля" />
                </xsl:call-template>
              </td>
              <td>
                <xsl:call-template name="GetNameSelect">
                  <xsl:with-param name="pointer">Перелічення</xsl:with-param>
                  <xsl:with-param name="value" select="Поле2" />
                </xsl:call-template>
              </td>
              <td>
                <xsl:call-template name="GetNameSelect">
                  <xsl:with-param name="pointer">Перелічення2</xsl:with-param>
                  <xsl:with-param name="value" select="Поле3" />
                </xsl:call-template>
              </td>
              <td>
                <xsl:call-template name="GetNameOd">
                  <xsl:with-param name="list" select="/root/Номенклатура_Список" />
                  <xsl:with-param name="uid" select="Поле4" />
                </xsl:call-template>
              </td>
            </tr>
          </xsl:for-each>
        </table>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>

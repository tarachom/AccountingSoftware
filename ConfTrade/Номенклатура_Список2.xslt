<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" 
                xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl">

  <xsl:output method="html" indent="yes" />
  <xsl:include href="Include.xslt" />

  <xsl:template match="/">
    <html>
      <title>HTML</title>
      <body>

        <table border="1">

          <xsl:for-each select="root/Номенклатура_Список2/row">
            <tr>
              <td>
                <xsl:value-of select="Назва"/>
              </td>
              <td>
                <xsl:value-of select="Код"/>
              </td>
              <td>
                <xsl:call-template name="GetNameOd">
                  <xsl:with-param name="list" select="/root/КлассификаторЕдИзм_Список" />
                  <xsl:with-param name="uid" select="БазоваяЕдиница" />
                </xsl:call-template>
              </td>
              <td>
                <xsl:value-of select="Вес"/>
              </td>
              <td>
                <xsl:call-template name="GetNameOd">
                  <xsl:with-param name="list" select="/root/Единици_Список" />
                  <xsl:with-param name="uid" select="ЕдиницаПоУмолчанию" />
                </xsl:call-template>
              </td>
              <td>
                <xsl:call-template name="GetNameOd">
                  <xsl:with-param name="list" select="/root/Валюти_Список" />
                  <xsl:with-param name="uid" select="ВалютаУчета" />
                </xsl:call-template>
              </td>
              <td>
                <xsl:call-template name="GetNameOd">
                  <xsl:with-param name="list" select="/root/КодиУКТВЕД_Список" />
                  <xsl:with-param name="uid" select="КодУКТВЕД" />
                </xsl:call-template>
              </td>
              
              <td>
                <xsl:call-template name="GetNameOd">
                  <xsl:with-param name="list" select="/root/Групи_Номенклатура_Список" />
                  <xsl:with-param name="uid" select="Група" />
                </xsl:call-template>
              </td>
            </tr>
          </xsl:for-each>
        </table>

      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>

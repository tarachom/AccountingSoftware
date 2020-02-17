<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl">

  <xsl:output method="html" indent="yes"/>

  <xsl:template name="GetNameOd">
    <xsl:param name="list" />
    <xsl:param name="uid" />

    <xsl:choose>
      <xsl:when test="$list/row[uid = $uid]">
        <xsl:value-of select="Назва"/>
      </xsl:when>
      <xsl:otherwise>
        <xsl:text> </xsl:text>
      </xsl:otherwise>
    </xsl:choose>

  </xsl:template>

  <xsl:template match="/">

    <html>
      <title>HTML Tutorial</title>
      <body>

        <table border="1">

          <xsl:for-each select="Записи_Вибірка/row">

            <tr>
              <td>
                <xsl:value-of select="uid"/>
              </td>
              <td>
                <xsl:value-of select="Запис"/>
              </td>
              <td>
                <xsl:value-of select="Товар"/>
              </td>
              <td>
                <xsl:value-of select="Документ"/>
                
                <xsl:call-template name="GetNameOd">
                  <xsl:with-param name="list"></xsl:with-param>
                  <xsl:with-param name="uid" select="" />
                </xsl:call-template>
                
              </td>
            </tr>

          </xsl:for-each>

        </table>

        <table border="1">

          <xsl:for-each select="Товари_Візуалізація3/row">

            <tr>
              <td>
                <xsl:value-of select="uid"/>
              </td>
              <td>
                <xsl:value-of select="Назва"/>
              </td>
              <td>
                <xsl:value-of select="Код"/>
              </td>
              <td>
                <xsl:value-of select="Кількість"/>
              </td>
            </tr>

          </xsl:for-each>

        </table>

      </body>
    </html>

  </xsl:template>

</xsl:stylesheet>

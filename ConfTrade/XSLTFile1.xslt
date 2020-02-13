﻿<?xml version="1.0" encoding="utf-8"?>
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

    <table>

      <xsl:for-each select="root/Товари_ВибіркаТовари/row">

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
            <xsl:call-template name="GetNameOd">
              <xsl:with-param name="list" select="/root/ОдиниціВиміру_Вибірка" />
              <xsl:with-param name="uid" select="Одиниця" />
            </xsl:call-template>
          </td>
        </tr>

      </xsl:for-each>

    </table>

  </xsl:template>

</xsl:stylesheet>
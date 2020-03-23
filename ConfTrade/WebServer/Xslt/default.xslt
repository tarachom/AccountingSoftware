<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="html" indent="yes"/>

  <xsl:template match="/">

    <h1>Список</h1>

    <ul>

      <xsl:for-each select="root/item">

        <li>
          <a>
            <xsl:attribute name="href">
              <xsl:text>?confobj=</xsl:text>
              <xsl:value-of select="text()"/>
            </xsl:attribute>
            <xsl:value-of select="text()"/>
          </a>
        </li>

      </xsl:for-each>

    </ul>

  </xsl:template>

</xsl:stylesheet>

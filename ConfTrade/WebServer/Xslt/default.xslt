<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="html" indent="yes"/>

  <xsl:template match="/">

    <h1>Список</h1>

    <xsl:for-each select="root/group">
      
      <div class="list-group">

        <p>
          <xsl:value-of select="name"/>
        </p>
        
        <xsl:for-each select="item">

          <a class="list-group-item list-group-item-action">
            <xsl:attribute name="href">
              <xsl:text>javascript:Load('container', '?confobj=</xsl:text>
              <xsl:value-of select="@src"/>
              <xsl:text>')</xsl:text>
            </xsl:attribute>
            <img src="Images/list.ico" />
            <xsl:text> </xsl:text>
            <xsl:value-of select="text()"/>
          </a>

        </xsl:for-each>
      </div>

    </xsl:for-each>

  </xsl:template>

</xsl:stylesheet>

<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl">

  <xsl:template name="GetNameOd">
    <xsl:param name="list" />
    <xsl:param name="uid" />

    <xsl:for-each select="$list/row[uid = $uid]">
      <xsl:value-of select="Назва"/>
    </xsl:for-each>
    
  </xsl:template>

</xsl:stylesheet>

<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="html" indent="yes"/>

  <xsl:include href="ModalFormBody.xslt"/>
  
  <xsl:template match="/">

    <xsl:value-of select="root/info"/>

    <xsl:apply-templates select="root/Валюти" />
  </xsl:template>

</xsl:stylesheet>

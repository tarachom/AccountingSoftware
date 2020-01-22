<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="text" indent="yes" />

    <xsl:template match="/">
public class Test
{
      <xsl:for-each select="root/fields/field">
    public <xsl:value-of select="type"/><xsl:text> </xsl:text><xsl:value-of select="name"/> { get; set; }
      </xsl:for-each>
}
    </xsl:template>
</xsl:stylesheet>

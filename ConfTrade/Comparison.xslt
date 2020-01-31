<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="xml" indent="yes" />

  <xsl:template match="/">

    <root>

      <xsl:for-each select="Comparison/CreateTable">
        <sql>
          CREATE TABLE <xsl:value-of select="Name"/> (
          uid uuid NOT NULL,
          <xsl:for-each select="Field">
            <xsl:value-of select="Name"/>
            <xsl:text> </xsl:text>
            <xsl:value-of select="DataType"/>
            <xsl:text>,</xsl:text>
          </xsl:for-each>
          PRIMARY KEY(uid)
          );
        </sql>
      </xsl:for-each>
      
      <xsl:for-each select="Comparison/RenameColumn">
        <sql>
          ALTER TABLE <xsl:value-of select="TableName"/> RENAME <xsl:value-of select="Field/Name"/> TO <xsl:value-of select="Field/NewName"/>;
        </sql>
      </xsl:for-each>
      
      <xsl:for-each select="Comparison/AddColumn">
        <sql>
          ALTER TABLE <xsl:value-of select="TableName"/> ADD COLUMN <xsl:value-of select="Field/Name"/><xsl:text> </xsl:text><xsl:value-of select="Field/DataType"/>;
        </sql>
      </xsl:for-each>
      
    </root>

  </xsl:template>

</xsl:stylesheet>

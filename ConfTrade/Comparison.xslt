<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="xml" indent="yes" />

  <xsl:template match="/">

    <root>

      <xsl:variable name="TableList" select="Comparison/InformationSchema/Table" />
      
      <xsl:for-each select="document('Configuration.xml')/Configuration/Directories/Directory">
        <xsl:variable name="DirectoryName" select="Name" />
        <xsl:variable name="DirectoryTable" select="Table" />
        
        <text>
          <name>
            <xsl:value-of select="$DirectoryName"/>
          </name>
          <table>
            <xsl:value-of select="$DirectoryTable"/>
          </table>

          <xsl:choose>
            <xsl:when test="$TableList[Name = $DirectoryTable]">
              <xsl:variable name="ColumnList" select="$TableList[Name = $DirectoryTable]/Column" />
              <ok>ok</ok>

              <xsl:for-each select="Fields/Field">
                <xsl:variable name="FieldName" select="Name" />
                
                <field_name>
                  <xsl:value-of select="$FieldName"/>
                </field_name>

                <xsl:choose>
                  <xsl:when test="$ColumnList[Name = $FieldName]">
                    <ok>ok</ok>
                  </xsl:when>
                  <xsl:otherwise>
                    <no>ok</no>
                  </xsl:otherwise>
                </xsl:choose>
                
              </xsl:for-each>
              
            </xsl:when>
            <xsl:otherwise>
              <no>ok</no>
              
              
            </xsl:otherwise>
          </xsl:choose>
          
        </text>
        
      </xsl:for-each>

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

<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
  xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:utils="urn:myExtension" exclude-result-prefixes="msxsl">
  
  <xsl:output method="xml" indent="yes" />

  <msxsl:script implements-prefix="utils" language="C#">
    <![CDATA[
      public string ToLower(string stringValue)
      {
        string result = String.Empty;

        if(!String.IsNullOrEmpty(stringValue))
        {
          result = stringValue.ToLower(); 
        }

        return result;
      }
    ]]>
  </msxsl:script>
  
  <xsl:template name="FieldsControl">
    <xsl:param name="InformationSchemaFieldList" />
    <xsl:param name="ConfigurationFieldList" />

    <xsl:for-each select="$ConfigurationFieldList">
      <xsl:variable name="ConfFieldName" select="utils:ToLower(Name)" />

      <Control_Field>
        <Name>
          <xsl:value-of select="$ConfFieldName"/>
        </Name>

        <xsl:choose>
          <xsl:when test="$InformationSchemaFieldList[Name = $ConfFieldName]">
            <IsExist>yes</IsExist>



          </xsl:when>
          <xsl:otherwise>
            <IsExist>no</IsExist>
          </xsl:otherwise>
        </xsl:choose>

      </Control_Field>

    </xsl:for-each>

  </xsl:template>

  <xsl:template name="TabularPartsControl">
    <xsl:param name="InformationSchemaTableList" />
    <xsl:param name="ConfigurationTablePartList" />

    <xsl:for-each select="$ConfigurationTablePartList">
      <xsl:variable name="ConTablePart" select="Name" />
      <xsl:variable name="ConfTablePartTable" select="Table" />

      <Control_TabularParts>
        <Name>
          <xsl:value-of select="$ConTablePart"/>
        </Name>
        <Table>
          <xsl:value-of select="$ConfTablePartTable"/>
        </Table>

        <xsl:choose>
          <xsl:when test="$InformationSchemaTableList[Name = $ConfTablePartTable]">
            <IsExist>yes</IsExist>



          </xsl:when>
          <xsl:otherwise>
            <IsExist>no</IsExist>
          </xsl:otherwise>
        </xsl:choose>

      </Control_TabularParts>

    </xsl:for-each>

  </xsl:template>

  <xsl:template match="/">

    <root>

      <xsl:variable name="InfoSchemaTableList" select="Comparison/InformationSchema/Table" />

      <xsl:for-each select="document('Configuration.xml')/Configuration/Directories/Directory">
        <xsl:variable name="ConfDirectoryName" select="Name" />
        <xsl:variable name="ConfDirectoryTable" select="Table" />

        <Control_Directory>
          <Name>
            <xsl:value-of select="$ConfDirectoryName"/>
          </Name>
          <Table>
            <xsl:value-of select="$ConfDirectoryTable"/>
          </Table>

          <xsl:choose>
            <xsl:when test="$InfoSchemaTableList[Name = $ConfDirectoryTable]">
              <IsExist>yes</IsExist>

              <xsl:call-template name="FieldsControl">
                <xsl:with-param name="ConfigurationFieldList" select="Fields/Field" />
                <xsl:with-param name="InformationSchemaFieldList" select="$InfoSchemaTableList[Name = $ConfDirectoryTable]/Column" />
              </xsl:call-template>

              <xsl:call-template name="TabularPartsControl">
                <xsl:with-param name="ConfigurationTablePartList" select="TabularParts/TablePart" />
                <xsl:with-param name="InformationSchemaTableList" select="$InfoSchemaTableList" />
              </xsl:call-template>

            </xsl:when>
            <xsl:otherwise>
              <IsExist>no</IsExist>


            </xsl:otherwise>
          </xsl:choose>



        </Control_Directory>

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

<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
  xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:utils="urn:myExtension" exclude-result-prefixes="msxsl">

  <xsl:output method="xml" indent="yes" />

  <msxsl:script implements-prefix="utils" language="C#">
    <![CDATA[
      public string ToLower(string stringValue)
      {
          return String.IsNullOrEmpty(stringValue) ? String.Empty : stringValue.ToLower();
      }
    ]]>
  </msxsl:script>

  <xsl:template name="FieldsControl">
    <xsl:param name="InfoSchemaFieldList" />
    <xsl:param name="ConfigurationFieldList" />

    <xsl:for-each select="$ConfigurationFieldList">
      <xsl:variable name="ConfFieldName" select="utils:ToLower(Name)" />

      <Control_Field>
        <Name>
          <xsl:value-of select="$ConfFieldName"/>
        </Name>

        <xsl:choose>
          <xsl:when test="$InfoSchemaFieldList[Name = $ConfFieldName]">
            <IsExist>yes</IsExist>
            <Type>
              <xsl:variable name="ConfFieldType" select="Type" />
              <xsl:variable name="InfoSchemaFieldDataType" select="$InfoSchemaFieldList[Name = $ConfFieldName]/DataType" />
              <xsl:variable name="InfoSchemaFieldUdtName" select="$InfoSchemaFieldList[Name = $ConfFieldName]/UdtName" />

              <Coincide>
                <xsl:choose>
                  <xsl:when test="$ConfFieldType = 'string' and ($InfoSchemaFieldDataType = 'text' and $InfoSchemaFieldUdtName = 'text')">
                    <xsl:text>yes</xsl:text>
                  </xsl:when>
                  <xsl:when test="$ConfFieldType = 'string[]' and ($InfoSchemaFieldDataType = 'ARRAY' and $InfoSchemaFieldUdtName = '_text')">
                    <xsl:text>yes</xsl:text>
                  </xsl:when>
                  <xsl:when test="$ConfFieldType = 'integer' and ($InfoSchemaFieldDataType = 'integer' and $InfoSchemaFieldUdtName = 'int4')">
                    <xsl:text>yes</xsl:text>
                  </xsl:when>
                  <xsl:when test="$ConfFieldType = 'integer[]' and ($InfoSchemaFieldDataType = 'ARRAY' and $InfoSchemaFieldUdtName = '_int4')">
                    <xsl:text>yes</xsl:text>
                  </xsl:when>
                  <xsl:when test="$ConfFieldType = 'numeric' and ($InfoSchemaFieldDataType = 'numeric' and $InfoSchemaFieldUdtName = 'numeric')">
                    <xsl:text>yes</xsl:text>
                  </xsl:when>
                  <xsl:when test="$ConfFieldType = 'numeric[]' and ($InfoSchemaFieldDataType = 'ARRAY' and $InfoSchemaFieldUdtName = '_numeric')">
                    <xsl:text>yes</xsl:text>
                  </xsl:when>
                  <xsl:when test="$ConfFieldType = 'boolean' and ($InfoSchemaFieldDataType = 'boolean' and $InfoSchemaFieldUdtName = 'bool')">
                    <xsl:text>yes</xsl:text>
                  </xsl:when>
                  <xsl:when test="$ConfFieldType = 'date' and ($InfoSchemaFieldDataType = 'date' and $InfoSchemaFieldUdtName = 'date')">
                    <xsl:text>yes</xsl:text>
                  </xsl:when>
                  <xsl:when test="$ConfFieldType = 'time' and ($InfoSchemaFieldDataType = 'time without time zone' and $InfoSchemaFieldUdtName = 'time')">
                    <xsl:text>yes</xsl:text>
                  </xsl:when>
                  <xsl:when test="$ConfFieldType = 'datetime' and ($InfoSchemaFieldDataType = 'timestamp without time zone' and $InfoSchemaFieldUdtName = 'timestamp')">
                    <xsl:text>yes</xsl:text>
                  </xsl:when>
                  <xsl:when test="$ConfFieldType = 'pointer' and ($InfoSchemaFieldDataType = 'uuid' and $InfoSchemaFieldUdtName = 'uuid')">
                    <xsl:text>yes</xsl:text>
                  </xsl:when>
                  <xsl:otherwise>
                    <xsl:text>no</xsl:text>
                  </xsl:otherwise>
                </xsl:choose>
              </Coincide>

              <ConfType>
                <xsl:value-of select="$ConfFieldType"/>
              </ConfType>

              <DataType>
                <xsl:value-of select="$InfoSchemaFieldDataType"/>
              </DataType>

              <UdtName>
                <xsl:value-of select="$InfoSchemaFieldUdtName"/>
              </UdtName>

            </Type>
          </xsl:when>
          <xsl:otherwise>
            <IsExist>no</IsExist>

            <xsl:call-template name="FieldCreate">
              <xsl:with-param name="ConfFieldName" select="$ConfFieldName" />
              <xsl:with-param name="ConfFieldType" select="Type" />
            </xsl:call-template>

          </xsl:otherwise>
        </xsl:choose>
      </Control_Field>

    </xsl:for-each>
  </xsl:template>

  <xsl:template name="FieldCreate">
    <xsl:param name="ConfFieldName" />
    <xsl:param name="ConfFieldType" />

    <FieldCreate>

      <Name>
        <xsl:value-of select="$ConfFieldName"/>
      </Name>

      <ConfType>
        <xsl:value-of select="$ConfFieldType"/>
      </ConfType>

      <DataType>
        <xsl:choose>
          <xsl:when test="Type = 'string'">
            <xsl:text>text</xsl:text>
          </xsl:when>
          <xsl:when test="Type = 'string[]'">
            <xsl:text>text[]</xsl:text>
          </xsl:when>
          <xsl:when test="Type = 'integer'">
            <xsl:text>integer</xsl:text>
          </xsl:when>
          <xsl:when test="Type = 'integer[]'">
            <xsl:text>integer[]</xsl:text>
          </xsl:when>
          <xsl:when test="Type = 'numeric'">
            <xsl:text>numeric</xsl:text>
          </xsl:when>
          <xsl:when test="Type = 'numeric[]'">
            <xsl:text>numeric[]</xsl:text>
          </xsl:when>
          <xsl:when test="Type = 'boolean'">
            <xsl:text>boolean</xsl:text>
          </xsl:when>
          <xsl:when test="Type = 'date'">
            <xsl:text>date</xsl:text>
          </xsl:when>
          <xsl:when test="Type = 'time'">
            <xsl:text>time without time zone</xsl:text>
          </xsl:when>
          <xsl:when test="Type = 'datetime'">
            <xsl:text>timestamp without time zone</xsl:text>
          </xsl:when>
          <xsl:when test="Type = 'pointer'">
            <xsl:text>uuid</xsl:text>
          </xsl:when>
        </xsl:choose>
      </DataType>

    </FieldCreate>

  </xsl:template>

  <xsl:template name="TabularPartsControl">
    <xsl:param name="InfoSchemaTableList" />
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
          <xsl:when test="$InfoSchemaTableList[Name = $ConfTablePartTable]">
            <IsExist>yes</IsExist>

            <xsl:call-template name="FieldsControl">
              <xsl:with-param name="ConfigurationFieldList" select="$ConfigurationTablePartList/Fields/Field" />
              <xsl:with-param name="InfoSchemaFieldList" select="$InfoSchemaTableList[Name = $ConfTablePartTable]/Column" />
            </xsl:call-template>

          </xsl:when>
          <xsl:otherwise>
            <IsExist>no</IsExist>

            <TableCreate>

              <xsl:for-each select="$ConfigurationTablePartList/Fields/Field">
                <xsl:call-template name="FieldCreate">
                  <xsl:with-param name="ConfFieldName" select="utils:ToLower(Name)" />
                  <xsl:with-param name="ConfFieldType" select="Type" />
                </xsl:call-template>
              </xsl:for-each>

            </TableCreate>

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
                <xsl:with-param name="InfoSchemaFieldList" select="$InfoSchemaTableList[Name = $ConfDirectoryTable]/Column" />
              </xsl:call-template>

            </xsl:when>
            <xsl:otherwise>
              <IsExist>no</IsExist>

              <TableCreate>

                <xsl:for-each select="Fields/Field">
                  <xsl:call-template name="FieldCreate">
                    <xsl:with-param name="ConfFieldName" select="utils:ToLower(Name)" />
                    <xsl:with-param name="ConfFieldType" select="Type" />
                  </xsl:call-template>
                </xsl:for-each>

              </TableCreate>

            </xsl:otherwise>
          </xsl:choose>

          <xsl:call-template name="TabularPartsControl">
            <xsl:with-param name="ConfigurationTablePartList" select="TabularParts/TablePart" />
            <xsl:with-param name="InfoSchemaTableList" select="$InfoSchemaTableList" />
          </xsl:call-template>

        </Control_Directory>

      </xsl:for-each>

    </root>

  </xsl:template>

</xsl:stylesheet>
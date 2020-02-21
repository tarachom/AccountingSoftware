<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="xml" indent="yes" />

  <!-- xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:utils="urn:myExtension" exclude-result-prefixes="msxsl"
  <msxsl:script implements-prefix="utils" language="C#">
    <![CDATA[
      public string ToLower(string stringValue)
      {
          return String.IsNullOrEmpty(stringValue) ? String.Empty : stringValue.ToLower();
      }
    ]]>
  </msxsl:script>
  -->

  <xsl:template name="FieldsControl">
    <xsl:param name="InfoSchemaFieldList" />
    <xsl:param name="ConfigurationFieldList" />

    <xsl:for-each select="$ConfigurationFieldList">
      <xsl:variable name="ConfFieldName" select="NameInTable" />

      <Control_Field>
        <Name>
          <xsl:value-of select="Name"/>
        </Name>
        <NameInTable>
          <xsl:value-of select="$ConfFieldName"/>
        </NameInTable>

        <xsl:choose>
          <xsl:when test="$InfoSchemaFieldList[Name = $ConfFieldName]">
            <IsExist>yes</IsExist>
            <Type>
              <xsl:variable name="ConfFieldType" select="Type" />
              <xsl:variable name="InfoSchemaFieldDataType" select="$InfoSchemaFieldList[Name = $ConfFieldName]/DataType" />
              <xsl:variable name="InfoSchemaFieldUdtName" select="$InfoSchemaFieldList[Name = $ConfFieldName]/UdtName" />

              <ConfType>
                <xsl:value-of select="$ConfFieldType"/>
              </ConfType>

              <DataType>
                <xsl:value-of select="$InfoSchemaFieldDataType"/>
              </DataType>

              <UdtName>
                <xsl:value-of select="$InfoSchemaFieldUdtName"/>
              </UdtName>
              
              <xsl:if test="$ConfFieldType = 'string'">
                <xsl:choose>
                  <xsl:when test="$InfoSchemaFieldDataType = 'text' and $InfoSchemaFieldUdtName = 'text'">
                    <Coincide>yes</Coincide>
                  </xsl:when>
                  <xsl:otherwise>
                    <Coincide>no</Coincide>
                    <DataTypeCreate>text</DataTypeCreate>
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:if>

              <xsl:if test="$ConfFieldType = 'string[]'">
                <xsl:choose>
                  <xsl:when test="$InfoSchemaFieldDataType = 'ARRAY' and $InfoSchemaFieldUdtName = '_text'">
                    <Coincide>yes</Coincide>
                  </xsl:when>
                  <xsl:otherwise>
                    <Coincide>no</Coincide>
                    <DataTypeCreate>text[]</DataTypeCreate>
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:if>

              <xsl:if test="$ConfFieldType = 'integer'">
                <xsl:choose>
                  <xsl:when test="$InfoSchemaFieldDataType = 'integer' and $InfoSchemaFieldUdtName = 'int4'">
                    <Coincide>yes</Coincide>
                  </xsl:when>
                  <xsl:otherwise>
                    <Coincide>no</Coincide>
                    <DataTypeCreate>integer</DataTypeCreate>
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:if>

              <xsl:if test="$ConfFieldType = 'integer[]'">
                <xsl:choose>
                  <xsl:when test="$InfoSchemaFieldDataType = 'ARRAY' and $InfoSchemaFieldUdtName = '_int4'">
                    <Coincide>yes</Coincide>
                  </xsl:when>
                  <xsl:otherwise>
                    <Coincide>no</Coincide>
                    <DataTypeCreate>integer[]</DataTypeCreate>
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:if>

              <xsl:if test="$ConfFieldType = 'numeric'">
                <xsl:choose>
                  <xsl:when test="$InfoSchemaFieldDataType = 'numeric' and $InfoSchemaFieldUdtName = 'numeric'">
                    <Coincide>yes</Coincide>
                  </xsl:when>
                  <xsl:otherwise>
                    <Coincide>no</Coincide>
                    <DataTypeCreate>numeric</DataTypeCreate>
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:if>

              <xsl:if test="$ConfFieldType = 'numeric[]'">
                <xsl:choose>
                  <xsl:when test="$InfoSchemaFieldDataType = 'ARRAY' and $InfoSchemaFieldUdtName = '_numeric'">
                    <Coincide>yes</Coincide>
                  </xsl:when>
                  <xsl:otherwise>
                    <Coincide>no</Coincide>
                    <DataTypeCreate>numeric[]</DataTypeCreate>
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:if>

              <xsl:if test="$ConfFieldType = 'boolean'">
                <xsl:choose>
                  <xsl:when test="$InfoSchemaFieldDataType = 'boolean' and $InfoSchemaFieldUdtName = 'bool'">
                    <Coincide>yes</Coincide>
                  </xsl:when>
                  <xsl:otherwise>
                    <Coincide>no</Coincide>
                    <DataTypeCreate>boolean</DataTypeCreate>
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:if>

              <xsl:if test="$ConfFieldType = 'date'">
                <xsl:choose>
                  <xsl:when test="$InfoSchemaFieldDataType = 'date' and $InfoSchemaFieldUdtName = 'date'">
                    <Coincide>yes</Coincide>
                  </xsl:when>
                  <xsl:otherwise>
                    <Coincide>no</Coincide>
                    <DataTypeCreate>date</DataTypeCreate>
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:if>

              <xsl:if test="$ConfFieldType = 'time'">
                <xsl:choose>
                  <xsl:when test="$InfoSchemaFieldDataType = 'time without time zone' and $InfoSchemaFieldUdtName = 'time'">
                    <Coincide>yes</Coincide>
                  </xsl:when>
                  <xsl:otherwise>
                    <Coincide>no</Coincide>
                    <DataTypeCreate>time without time zone</DataTypeCreate>
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:if>

              <xsl:if test="$ConfFieldType = 'datetime'">
                <xsl:choose>
                  <xsl:when test="$InfoSchemaFieldDataType = 'timestamp without time zone' and $InfoSchemaFieldUdtName = 'timestamp'">
                    <Coincide>yes</Coincide>
                  </xsl:when>
                  <xsl:otherwise>
                    <Coincide>no</Coincide>
                    <DataTypeCreate>timestamp without time zone</DataTypeCreate>
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:if>
              
              <xsl:if test="$ConfFieldType = 'pointer'">
                <xsl:choose>
                  <xsl:when test="$InfoSchemaFieldDataType = 'uuid' and $InfoSchemaFieldUdtName = 'uuid'">
                    <Coincide>yes</Coincide>
                  </xsl:when>
                  <xsl:otherwise>
                    <Coincide>no</Coincide>
                    <DataTypeCreate>uuid</DataTypeCreate>
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:if>

              <xsl:if test="$ConfFieldType = 'empty_pointer'">
                <xsl:choose>
                  <xsl:when test="$InfoSchemaFieldDataType = 'uuid' and $InfoSchemaFieldUdtName = 'uuid'">
                    <Coincide>yes</Coincide>
                  </xsl:when>
                  <xsl:otherwise>
                    <Coincide>no</Coincide>
                    <DataTypeCreate>uuid</DataTypeCreate>
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:if>

              <xsl:if test="$ConfFieldType = 'enum'">
                <xsl:choose>
                  <xsl:when test="$InfoSchemaFieldDataType = 'integer' and $InfoSchemaFieldUdtName = 'int4'">
                    <Coincide>yes</Coincide>
                  </xsl:when>
                  <xsl:otherwise>
                    <Coincide>no</Coincide>
                    <DataTypeCreate>integer</DataTypeCreate>
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:if>

            </Type>
          </xsl:when>
          <xsl:otherwise>
            <IsExist>no</IsExist>
            
            <xsl:call-template name="FieldCreate">
              <xsl:with-param name="ConfFieldName" select="Name" />
              <xsl:with-param name="ConfFieldNameInTable" select="$ConfFieldName" />
              <xsl:with-param name="ConfFieldType" select="Type" />
            </xsl:call-template>

          </xsl:otherwise>
        </xsl:choose>
      </Control_Field>

    </xsl:for-each>
  </xsl:template>

  <xsl:template name="FieldCreate">
    <xsl:param name="ConfFieldName" />
    <xsl:param name="ConfFieldNameInTable" />
    <xsl:param name="ConfFieldType" />

    <FieldCreate>
      <Name>
        <xsl:value-of select="$ConfFieldName"/>
      </Name>

      <NameInTable>
        <xsl:value-of select="$ConfFieldNameInTable"/>
      </NameInTable>

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
          <xsl:when test="Type = 'empty_pointer'">
            <xsl:text>uuid</xsl:text>
          </xsl:when>
          <xsl:when test="Type = 'enum'">
            <xsl:text>integer</xsl:text>
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
              <xsl:with-param name="ConfigurationFieldList" select="Fields/Field" />
              <xsl:with-param name="InfoSchemaFieldList" select="$InfoSchemaTableList[Name = $ConfTablePartTable]/Column" />
            </xsl:call-template>

          </xsl:when>
          <xsl:otherwise>
            <IsExist>no</IsExist>

            <TableCreate>

              <xsl:for-each select="Fields/Field">
                <xsl:call-template name="FieldCreate">
                  <xsl:with-param name="ConfFieldName" select="Name" />
                  <xsl:with-param name="ConfFieldNameInTable" select="NameInTable" />
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

      <xsl:variable name="InfoSchemaTableList" select="InformationSchema/Table" />

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
                    <xsl:with-param name="ConfFieldName" select="Name" />
                    <xsl:with-param name="ConfFieldNameInTable" select="NameInTable" />
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
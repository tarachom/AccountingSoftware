<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="xml" indent="yes" />

  <xsl:template name="Template_RenameColumn">
    <xsl:param name="TableName" />
    <xsl:param name="FieldNameInTable" />
    <xsl:param name="DataTypeCreate" />

    <sql>BEGIN;</sql>

    <sql>
      <xsl:text>ALTER TABLE </xsl:text>
      <xsl:value-of select="$TableName"/>
      <xsl:text> RENAME COLUMN </xsl:text>
      <xsl:value-of select="$FieldNameInTable"/>
      <xsl:text> TO </xsl:text>
      <xsl:value-of select="$FieldNameInTable"/>
      <xsl:text>_OLD;</xsl:text>
    </sql>

    <sql>
      <xsl:text>ALTER TABLE </xsl:text>
      <xsl:value-of select="$TableName"/>
      <xsl:text> ADD COLUMN </xsl:text>
      <xsl:value-of select="$FieldNameInTable"/>
      <xsl:text> </xsl:text>
      <xsl:value-of select="$DataTypeCreate"/>
      <xsl:text>;</xsl:text>
    </sql>

    <sql>COMMIT;</sql>

  </xsl:template>
  
  <xsl:template name="Template_Control_Field">
    <xsl:param name="Control_Field" />
    <xsl:param name="TableName" />

    <xsl:for-each select="$Control_Field">

      <xsl:choose>
        <xsl:when test="IsExist = 'yes'">

          <xsl:if test="Type/Coincide = 'no'">

            <info>
              <xsl:value-of select="Type/DataType"/>
              <xsl:value-of select="Type/UdtName"/>
            </info>
            
            <xsl:choose>
              <xsl:when test="Type/DataType = 'text'">
                <xsl:choose>
                  <xsl:when test="Type/ConfType = 'string[]'">
                    <sql>BEGIN;</sql>

                    <sql>
                      <xsl:text>ALTER TABLE </xsl:text>
                      <xsl:value-of select="$TableName"/>
                      <xsl:text> RENAME COLUMN </xsl:text>
                      <xsl:value-of select="NameInTable"/>
                      <xsl:text> TO </xsl:text>
                      <xsl:value-of select="NameInTable"/>
                      <xsl:text>_OLD;</xsl:text>
                    </sql>

                    <sql>
                      <xsl:text>ALTER TABLE </xsl:text>
                      <xsl:value-of select="$TableName"/>
                      <xsl:text> ADD COLUMN </xsl:text>
                      <xsl:value-of select="NameInTable"/>
                      <xsl:text> </xsl:text>
                      <xsl:value-of select="Type/DataTypeCreate"/>
                      <xsl:text>;</xsl:text>
                    </sql>

                    <!--
                    UPDATE public.test
                          SET text_mas = (SELECT array_agg(test2.text) FROM public.test AS test2 
                    WHERE test2.uid = test.uid) 
                    -->

                    <sql>
                      <xsl:text>UPDATE </xsl:text>
                      <xsl:value-of select="$TableName"/>
                      <xsl:text> SET </xsl:text>
                      <xsl:value-of select="NameInTable"/>
                      <xsl:value-of select="concat(' = (SELECT array_agg(', 't.', NameInTable, '_OLD) FROM ')"/>
                      <xsl:value-of select="$TableName"/>
                      <xsl:text> AS t WHERE t.uid = </xsl:text>
                      <xsl:value-of select="$TableName"/>
                      <xsl:text>.uid AND t.</xsl:text>
                      <xsl:value-of select="NameInTable"/>
                      <xsl:text>_OLD != NULL);</xsl:text>
                    </sql>

                    <!-- ALTER TABLE table_name DROP COLUMN column_name;  -->

                    <sql>
                      <xsl:text>ALTER TABLE </xsl:text>
                      <xsl:value-of select="$TableName"/>
                      <xsl:text> DROP COLUMN </xsl:text>
                      <xsl:value-of select="NameInTable"/>
                      <xsl:text>_OLD;</xsl:text>
                    </sql>

                    <sql>COMMIT;</sql>
                  </xsl:when>
                  <xsl:otherwise>

                    <xsl:call-template name="Template_RenameColumn">
                      <xsl:with-param name="TableName" select="$TableName" />
                      <xsl:with-param name="FieldNameInTable" select="NameInTable" />
                      <xsl:with-param name="DataTypeCreate" select="Type/DataTypeCreate" />
                    </xsl:call-template>

                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>

              <xsl:when test="Type/DataType = 'integer' or Type/DataType = 'numeric'">
                <xsl:choose>
                  <xsl:when test="Type/ConfType = 'integer[]' or Type/ConfType = 'numeric[]'">
                    <sql>BEGIN;</sql>

                    <sql>
                      <xsl:text>ALTER TABLE </xsl:text>
                      <xsl:value-of select="$TableName"/>
                      <xsl:text> RENAME COLUMN </xsl:text>
                      <xsl:value-of select="NameInTable"/>
                      <xsl:text> TO </xsl:text>
                      <xsl:value-of select="NameInTable"/>
                      <xsl:text>_OLD;</xsl:text>
                    </sql>

                    <sql>
                      <xsl:text>ALTER TABLE </xsl:text>
                      <xsl:value-of select="$TableName"/>
                      <xsl:text> ADD COLUMN </xsl:text>
                      <xsl:value-of select="NameInTable"/>
                      <xsl:text> </xsl:text>
                      <xsl:value-of select="Type/DataTypeCreate"/>
                      <xsl:text>;</xsl:text>
                    </sql>

                    <!--
                    UPDATE public.test
                          SET text_mas = (SELECT array_agg(test2.text) FROM public.test AS test2 
                    WHERE test2.uid = test.uid) 
                    -->

                    <sql>
                      <xsl:text>UPDATE </xsl:text>
                      <xsl:value-of select="$TableName"/>
                      <xsl:text> SET </xsl:text>
                      <xsl:value-of select="NameInTable"/>
                      <xsl:value-of select="concat(' = (SELECT array_agg(', 't.', NameInTable, '_OLD) FROM ')"/>
                      <xsl:value-of select="$TableName"/>
                      <xsl:text> AS t WHERE t.uid = </xsl:text>
                      <xsl:value-of select="$TableName"/>
                      <xsl:text>.uid AND t.</xsl:text>
                      <xsl:value-of select="NameInTable"/>
                      <xsl:text>_OLD != NULL);</xsl:text>
                    </sql>

                    <!-- ALTER TABLE table_name DROP COLUMN column_name;  -->

                    <sql>
                      <xsl:text>ALTER TABLE </xsl:text>
                      <xsl:value-of select="$TableName"/>
                      <xsl:text> DROP COLUMN </xsl:text>
                      <xsl:value-of select="NameInTable"/>
                      <xsl:text>_OLD;</xsl:text>
                    </sql>

                    <sql>COMMIT;</sql>
                  </xsl:when>
                  <xsl:otherwise>

                    <xsl:call-template name="Template_RenameColumn">
                      <xsl:with-param name="TableName" select="$TableName" />
                      <xsl:with-param name="FieldNameInTable" select="NameInTable" />
                      <xsl:with-param name="DataTypeCreate" select="Type/DataTypeCreate" />
                    </xsl:call-template>

                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>
              
              <xsl:when test="Type/DataType = 'ARRAY' and Type/UdtName = '_text'">
                <xsl:choose>
                  <xsl:when test="Type/ConfType = 'string'">

                    <sql>BEGIN;</sql>

                    <sql>
                      <xsl:text>ALTER TABLE </xsl:text>
                      <xsl:value-of select="$TableName"/>
                      <xsl:text> RENAME COLUMN </xsl:text>
                      <xsl:value-of select="NameInTable"/>
                      <xsl:text> TO </xsl:text>
                      <xsl:value-of select="NameInTable"/>
                      <xsl:text>_OLD;</xsl:text>
                    </sql>

                    <sql>
                      <xsl:text>ALTER TABLE </xsl:text>
                      <xsl:value-of select="$TableName"/>
                      <xsl:text> ADD COLUMN </xsl:text>
                      <xsl:value-of select="NameInTable"/>
                      <xsl:text> </xsl:text>
                      <xsl:value-of select="Type/DataTypeCreate"/>
                      <xsl:text>;</xsl:text>
                    </sql>

                    <!--
                    UPDATE test SET text = (SELECT array_to_string(text_mas, ', ') FROM test AS t
					                                  Where t.uid = test.uid);   
                    -->

                    <sql>
                      <xsl:text>UPDATE </xsl:text>
                      <xsl:value-of select="$TableName"/>
                      <xsl:text> SET </xsl:text>
                      <xsl:value-of select="NameInTable"/>
                      <xsl:text> = (SELECT array_to_string(t.</xsl:text>
                      <xsl:value-of select="NameInTable"/>
                      <xsl:text>_OLD, ', ') FROM </xsl:text>
                      <xsl:value-of select="$TableName"/>
                      <xsl:text> AS t WHERE t.uid = </xsl:text>
                      <xsl:value-of select="$TableName"/>
                      <xsl:text>.uid AND t.</xsl:text>
                      <xsl:value-of select="NameInTable"/>
                      <xsl:text>_OLD != NULL);</xsl:text>
                    </sql>

                    <sql>
                      <xsl:text>ALTER TABLE </xsl:text>
                      <xsl:value-of select="$TableName"/>
                      <xsl:text> DROP COLUMN </xsl:text>
                      <xsl:value-of select="NameInTable"/>
                      <xsl:text>_OLD;</xsl:text>
                    </sql>

                    <sql>COMMIT;</sql>
                  </xsl:when>
                  <xsl:otherwise>

                    <xsl:call-template name="Template_RenameColumn">
                      <xsl:with-param name="TableName" select="$TableName" />
                      <xsl:with-param name="FieldNameInTable" select="NameInTable" />
                      <xsl:with-param name="DataTypeCreate" select="Type/DataTypeCreate" />
                    </xsl:call-template>

                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>

              <xsl:when test="Type/DataType = 'ARRAY' and Type/UdtName = '_int4'">
                
                <xsl:call-template name="Template_RenameColumn">
                  <xsl:with-param name="TableName" select="$TableName" />
                  <xsl:with-param name="FieldNameInTable" select="NameInTable" />
                  <xsl:with-param name="DataTypeCreate" select="Type/DataTypeCreate" />
                </xsl:call-template>
              </xsl:when>

              <xsl:when test="DataType = 'boolean' or 
                              DataType = 'date' or  DataType = 'time without time zone' or DataType = 'timestamp without time zone' or 
                              DataType = 'uuid'">
                <xsl:choose>
                  <xsl:when test="Type/ConfType = 'string'">

                    <sql>BEGIN;</sql>

                    <sql>
                      <xsl:text>ALTER TABLE </xsl:text>
                      <xsl:value-of select="$TableName"/>
                      <xsl:text> RENAME COLUMN </xsl:text>
                      <xsl:value-of select="NameInTable"/>
                      <xsl:text> TO </xsl:text>
                      <xsl:value-of select="NameInTable"/>
                      <xsl:text>_OLD;</xsl:text>
                    </sql>

                    <sql>
                      <xsl:text>ALTER TABLE </xsl:text>
                      <xsl:value-of select="$TableName"/>
                      <xsl:text> ADD COLUMN </xsl:text>
                      <xsl:value-of select="NameInTable"/>
                      <xsl:text> </xsl:text>
                      <xsl:value-of select="Type/DataTypeCreate"/>
                      <xsl:text>;</xsl:text>
                    </sql>

                    <sql>
                      <xsl:text>UPDATE </xsl:text>
                      <xsl:value-of select="$TableName"/>
                      <xsl:text> SET </xsl:text>
                      <xsl:value-of select="NameInTable"/>
                      <xsl:text> = (SELECT t.</xsl:text>
                      <xsl:value-of select="NameInTable"/>
                      <xsl:text>_OLD FROM </xsl:text>
                      <xsl:value-of select="$TableName"/>
                      <xsl:text> AS t WHERE t.uid = </xsl:text>
                      <xsl:value-of select="$TableName"/>
                      <xsl:text>.uid);</xsl:text>
                    </sql>

                    <sql>
                      <xsl:text>ALTER TABLE </xsl:text>
                      <xsl:value-of select="$TableName"/>
                      <xsl:text> DROP COLUMN </xsl:text>
                      <xsl:value-of select="NameInTable"/>
                      <xsl:text>_OLD;</xsl:text>
                    </sql>

                    <sql>COMMIT;</sql>
                  </xsl:when>
                  <xsl:otherwise>

                    <xsl:call-template name="Template_RenameColumn">
                      <xsl:with-param name="TableName" select="$TableName" />
                      <xsl:with-param name="FieldNameInTable" select="NameInTable" />
                      <xsl:with-param name="DataTypeCreate" select="Type/DataTypeCreate" />
                    </xsl:call-template>

                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>

            </xsl:choose>
          </xsl:if>

        </xsl:when>
        <xsl:when test="IsExist = 'no'">

          <xsl:for-each select="FieldCreate">

            <sql>
              <xsl:text>ALTER TABLE </xsl:text>
              <xsl:value-of select="$TableName"/>
              <xsl:text> ADD COLUMN </xsl:text>
              <xsl:value-of select="NameInTable"/>
              <xsl:text> </xsl:text>
              <xsl:value-of select="DataType"/>
              <xsl:text>;</xsl:text>
            </sql>
            
          </xsl:for-each>

        </xsl:when>
      </xsl:choose>

    </xsl:for-each>

  </xsl:template>

  <xsl:template match="/">

    <root>

      <xsl:for-each select="root/Control_Directory">

        <xsl:variable name="TableName" select="Table" />

        <xsl:choose>
          <xsl:when test="IsExist = 'yes'">

            <xsl:call-template name="Template_Control_Field">
              <xsl:with-param name="Control_Field" select="Control_Field" />
              <xsl:with-param name="TableName" select="$TableName" />
            </xsl:call-template>

            <xsl:for-each select="Control_TabularParts">
              <xsl:variable name="TabularParts_TableName" select="Table" />

              <xsl:call-template name="Template_Control_Field">
                <xsl:with-param name="Control_Field" select="Control_Field" />
                <xsl:with-param name="TableName" select="$TabularParts_TableName" />
              </xsl:call-template>

            </xsl:for-each>

          </xsl:when>
          <xsl:when test="IsExist = 'no'">

            <xsl:for-each select="TableCreate">
              <sql>
                <xsl:text>CREATE TABLE </xsl:text>
                <xsl:value-of select="$TableName"/>
                <xsl:text> (</xsl:text>
                <xsl:text>uid uuid NOT NULL, </xsl:text>
                <xsl:for-each select="FieldCreate">
                  <xsl:value-of select="NameInTable"/>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="DataType"/>
                  <xsl:text>, </xsl:text>
                </xsl:for-each>
                <xsl:text>PRIMARY KEY(uid));</xsl:text>
              </sql>
            </xsl:for-each>

            <xsl:for-each select="Control_TabularParts">
              <xsl:variable name="TabularParts_TableName" select="Table" />

              <xsl:choose>
                <xsl:when test="IsExist = 'yes'">
                  <ERROR>TabularParts IsExist = 'yes'</ERROR>
                </xsl:when>
                <xsl:when test="IsExist = 'no'">

                  <xsl:for-each select="TableCreate">
                    <sql>
                      <xsl:text>CREATE TABLE </xsl:text>
                      <xsl:value-of select="$TabularParts_TableName"/>
                      <xsl:text> (</xsl:text>
                      <xsl:text>owner uuid NOT NULL, </xsl:text>
                      <xsl:for-each select="FieldCreate">
                        <xsl:value-of select="NameInTable"/>
                        <xsl:text> </xsl:text>
                        <xsl:value-of select="DataType"/>
                        <xsl:if test="position() &lt; count(../FieldCreate)">
                          <xsl:text>, </xsl:text>
                        </xsl:if>
                      </xsl:for-each>
                      <xsl:text>);</xsl:text>
                    </sql>
                    <sql>
                      <xsl:text>CREATE INDEX ON </xsl:text>
                      <xsl:value-of select="$TabularParts_TableName"/>
                      <xsl:text> (owner);</xsl:text>
                    </sql>
                  </xsl:for-each>

                </xsl:when>
              </xsl:choose>

            </xsl:for-each>

          </xsl:when>
        </xsl:choose>

      </xsl:for-each>

    </root>

  </xsl:template>

</xsl:stylesheet>
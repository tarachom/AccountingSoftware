<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="xml" indent="yes" />

  <xsl:template name="Template_Control_Field">
    <xsl:param name="Control_Field" />
    <xsl:param name="TableName" />

    <xsl:for-each select="$Control_Field">

      <xsl:choose>
        <xsl:when test="IsExist = 'yes'">

          <xsl:if test="Type/Coincide = 'no'">

            <xsl:choose>
              <xsl:when test="Type/DataType = 'text'">

                <xsl:choose>
                  <xsl:when test="Type/ConfType = 'string[]'">
                    <info>start</info>

                    <sql>BEGIN;</sql>

                    <sql>
                      <xsl:text>ALTER TABLE </xsl:text>
                      <xsl:value-of select="$TableName"/>
                      <xsl:text> RENAME </xsl:text>
                      <xsl:value-of select="NameInTable"/>
                      <xsl:text> </xsl:text>
                      <xsl:value-of select="NameInTable"/>
                      <xsl:text>_OLD; </xsl:text>
                    </sql>

                    <sql>
                      <xsl:text>ALTER TABLE </xsl:text>
                      <xsl:value-of select="$TableName"/>
                      <xsl:text> ADD COLUMN </xsl:text>
                      <xsl:value-of select="NameInTable"/>
                      <xsl:text> </xsl:text>
                      <xsl:value-of select="Type/DataType"/>
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
                      <xsl:text>.uid);</xsl:text>
                    </sql>

                    <sql>COMMIT;</sql>

                    <info>stop</info>
                  </xsl:when>
                </xsl:choose>

              </xsl:when>
              <xsl:when test="DataType = ''">

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
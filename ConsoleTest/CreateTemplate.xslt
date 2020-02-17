<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="html" indent="yes"/>

  <xsl:param name="DirectoryName" />

  <xsl:template match="View">
    <xsl:text disable-output-escaping="yes">
&lt;xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" &gt;

   &lt;xsl:output method="html" indent="yes" /&gt;
   &lt;xsl:include href="Include.xslt" /&gt;

   &lt;xsl:template match="/"&gt;

    </xsl:text>

    <html>
      <title>HTML</title>
      <body>

        <table border="1">

          <xsl:text disable-output-escaping="yes">
      &lt;xsl:for-each select="root/</xsl:text>
          <xsl:value-of select="concat($DirectoryName, '_', Name)"/>
          <xsl:text disable-output-escaping="yes">/row"&gt;</xsl:text>
          <xsl:text>
      </xsl:text>

          <tr>
            <xsl:for-each select="Fields/Field">
              <td>
                <xsl:choose>
                  <xsl:when test="Type = 'empty_pointer'">
                    <xsl:text> </xsl:text>
                  </xsl:when>
                  <xsl:when test="Type = 'pointer'">
                    <xsl:text disable-output-escaping="yes">&lt;xsl:call-template name="GetNameOd"&gt;</xsl:text>
                    <xsl:text disable-output-escaping="yes">&lt;xsl:with-param name="list" select="/root/</xsl:text>
                    <xsl:value-of select="Pointer"/>
                    <xsl:text>_Список</xsl:text>
                    <xsl:text disable-output-escaping="yes">" /&gt;</xsl:text>
                    <xsl:text disable-output-escaping="yes">&lt;xsl:with-param name="uid" select="</xsl:text>
                    <xsl:value-of select="Name"/>
                    <xsl:text disable-output-escaping="yes">" /&gt;</xsl:text>
                    <xsl:text disable-output-escaping="yes">&lt;/xsl:call-template&gt;</xsl:text>

                  </xsl:when>
                  <xsl:otherwise>

                    <xsl:text disable-output-escaping="yes">&lt;xsl:value-of select="</xsl:text>
                    <xsl:value-of select="Name"/>
                    <xsl:text disable-output-escaping="yes">"/&gt;</xsl:text>

                  </xsl:otherwise>
                </xsl:choose>
              </td>
            </xsl:for-each>
          </tr>

          <xsl:text disable-output-escaping="yes">
      &lt;/xsl:for-each&gt;
      </xsl:text>

        </table>
      </body>
    </html>
    <xsl:text disable-output-escaping="yes">

   &lt;/xsl:template&gt;
&lt;/xsl:stylesheet&gt;
    </xsl:text>

  </xsl:template>
</xsl:stylesheet>
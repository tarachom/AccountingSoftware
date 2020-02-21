<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="html" indent="yes"/>

  <xsl:param name="DirectoryName" />

  <xsl:template match="View">
    <xsl:text disable-output-escaping="yes">
&lt;xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" &gt;

   &lt;xsl:output method="html" indent="yes" /&gt;
   

   <!---->
   &lt;xsl:template name="GetNameOd"&gt;
    &lt;xsl:param name="list" /&gt;
    &lt;xsl:param name="uid" /&gt;
    &lt;xsl:for-each select="$list/row[uid = $uid]"&gt;
      &lt;xsl:value-of select="Назва"/&gt;
    &lt;/xsl:for-each&gt;    
   &lt;/xsl:template&gt;

   <!---->
   &lt;xsl:template name="GetNameSelect"&gt;
     &lt;xsl:param name="pointer" /&gt;
     &lt;xsl:param name="value" /&gt;
     &lt;select&gt;
     &lt;xsl:for-each select="/root/Enums/Enum[Name = $pointer]/Fields/Field"&gt;
        &lt;option&gt;
          &lt;xsl:attribute name="value"&gt;
            &lt;xsl:value-of select="Value"/&gt;
          &lt;/xsl:attribute&gt;
          &lt;xsl:if test="$value = Value"&gt;
             &lt;xsl:attribute name="selected"&gt;selected&lt;/xsl:attribute&gt;          
          &lt;/xsl:if&gt;
          &lt;xsl:value-of select="Name"/&gt;
       &lt;/option&gt;
    &lt;/xsl:for-each&gt;
    &lt;/select&gt;
   &lt;/xsl:template&gt;
                      
  
   <!---->
   &lt;xsl:template match="/"&gt;
    </xsl:text>

    <html>
      <title>HTML</title>
      <body>

        <table border="1">

      <xsl:text disable-output-escaping="yes">
      &lt;xsl:for-each select="</xsl:text>
          <xsl:value-of select="concat('root/', $DirectoryName, '_', Name, '/row')"/>
          <xsl:text disable-output-escaping="yes">"&gt;</xsl:text>
          <xsl:text>
      </xsl:text>

          <tr>
            <xsl:for-each select="Fields/Field">
              <td>
                <xsl:choose>
                  
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
                  
                  <xsl:when test="Type = 'empty_pointer'">
                    <xsl:text> </xsl:text>
                  </xsl:when>
                  
                  <xsl:when test="Type = 'enum'">
                    <xsl:text disable-output-escaping="yes">&lt;xsl:call-template name="GetNameSelect"&gt;</xsl:text>
                    <xsl:text disable-output-escaping="yes">&lt;xsl:with-param name="pointer"&gt;</xsl:text>
                    <xsl:value-of select="Pointer"/>
                   <xsl:text disable-output-escaping="yes">&lt;/xsl:with-param&gt;</xsl:text>
                    <xsl:text disable-output-escaping="yes">&lt;xsl:with-param name="value" select="</xsl:text>
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
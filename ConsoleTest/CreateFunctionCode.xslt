<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="text" indent="yes"/>

  <xsl:param name="DirectoryName" />

  <xsl:template match="View">
    <xsl:variable name="FullViewName" select="concat($DirectoryName, '_', Name, '_View')" />
    
using System.Text;

using AccountingSoftware;
using Conf = ConfTrade_v1_1;
using Довідники = ConfTrade_v1_1.Directory;    

namespace ConfTrade
{
    public partial class ConfTrade
    {
        public static string Run()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<xsl:text disable-output-escaping="yes">&lt;root&gt;</xsl:text>");
            
            Довідники.<xsl:value-of select="$FullViewName" /> m_<xsl:value-of select="$FullViewName" /> = new Довідники.<xsl:value-of select="$FullViewName" />();
            m_<xsl:value-of select="$FullViewName" />.QuerySelect.CreateTempTable = true;
            sb.Append(m_<xsl:value-of select="$FullViewName" />.Read());
            
            <xsl:for-each select="Fields/Field">

              <xsl:choose>
                <xsl:when test="Type = 'pointer'">
                  <xsl:variable name="FullFieldViewName" select="concat(Pointer, '_Список_View')" />
            Довідники.<xsl:value-of select="$FullFieldViewName" /> m_<xsl:value-of select="$FullFieldViewName" /> = new Довідники.<xsl:value-of select="$FullFieldViewName" />();
            m_<xsl:value-of select="$FullFieldViewName" />.QuerySelect.Where.Add(
                new Where("uid", Comparison.IN, /* <xsl:value-of select="NameInTable" /> */ 
                "SELECT DISTINCT " + m_<xsl:value-of select="$FullViewName" />.Alias["<xsl:value-of select="Name" />"] + 
                " FROM " + m_<xsl:value-of select="$FullViewName" />.QuerySelect.TempTable, true));
                
            sb.Append(m_<xsl:value-of select="$FullFieldViewName" />.Read());
                </xsl:when>
              </xsl:choose>
    
            </xsl:for-each>
    
            sb.Append("<xsl:text disable-output-escaping="yes">&lt;/root&gt;</xsl:text>");
            return sb.ToString();
        }
    }
}
  </xsl:template>

</xsl:stylesheet>

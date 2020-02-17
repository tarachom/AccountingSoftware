<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="text" indent="yes"/>

  <xsl:param name="DirectoryName" />

  <xsl:template match="View">
using System.Text;

using AccountingSoftware;
using Conf = ConfTrade_v1_1;
using Довідники = ConfTrade_v1_1.Directory;    

namespace ConsoleTest
{
    public partial class Program
    {
        public static string Run()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<xsl:text disable-output-escaping="yes">&lt;root&gt;</xsl:text>");
        
            Довідники.<xsl:value-of select="$DirectoryName" />_Список_View m_<xsl:value-of select="$DirectoryName" />_Список_View = new Довідники.<xsl:value-of select="$DirectoryName" />_Список_View();
            m_<xsl:value-of select="$DirectoryName" />_Список_View.QuerySelect.CreateTempTable = true;
            sb.Append(m_<xsl:value-of select="$DirectoryName" />_Список_View.Read());

            <xsl:for-each select="Fields/Field">

              <xsl:choose>
                <xsl:when test="Type = 'pointer'">
            Довідники.<xsl:value-of select="Pointer" />_Список_View m_<xsl:value-of select="Pointer" />_Список_View = new Довідники.<xsl:value-of select="Pointer" />_Список_View();
            m_<xsl:value-of select="Pointer" />_Список_View.QuerySelect.Where.Add(
                new Where("uid", Comparison.EQ, "(SELECT DISTINCT <xsl:value-of select="NameInTable" /> FROM " + m_<xsl:value-of select="$DirectoryName" />_Список_View.QuerySelect.TempTable + ")", true));
            sb.Append(m_<xsl:value-of select="Pointer" />_Список_View.Read());
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

<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="text" indent="yes"/>

  <xsl:template match="/">

    <xsl:for-each select="Configuration/Directories/Directory">

/// &lt;summary&gt;
/// <xsl:value-of select="Desc"/>
/// &lt;/summary&gt;
class <xsl:value-of select="Name"/>Objest : DirectoryObject
{
    public <xsl:value-of select="Name"/>Objest()
    {
         
    }
    
    <xsl:for-each select="Fields/Field">
    /// &lt;summary&gt;
    /// <xsl:value-of select="Desc"/>
    /// &lt;/summary&gt;
    public <xsl:value-of select="Type"/><xsl:text> </xsl:text><xsl:value-of select="Name"/> { get; set; }
    </xsl:for-each>
}
<!--
class TestObjest : DirectoryObject
{
		public TestObjest()
		{
			ATablePart = new TestATablePart(this);
		}

		public TestPointer GetDirectoryPointer()
		{
			TestPointer TestPointerItem = new TestPointer();
			TestPointerItem.Init(base.UID);

			return TestPointerItem;
		}

		public void Save()
		{

		}

		public TestPointer Field1 { get; set; }

		public TestPointer Field2 { get; set; }

		public TestPointer Field3 { get; set; }

		public string Field4 { get; set; }

		public TestATablePart ATablePart { get; }
}
-->
    
    </xsl:for-each>

  </xsl:template>

</xsl:stylesheet>

<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="html" indent="yes"/>

  <xsl:template match="Контрагенти_Групи">

    <form id="ModalFormSendForm">
      <div class="form-group">
        <label for="code">
          Uid: <xsl:value-of select="uid"/>
        </label>
      </div>
      <div class="form-group">
        <label for="code">Код:</label>
        <input type="text" class="form-control" placeholder="Код" name="Code" value="{Код}" />
      </div>
      <div class="form-group">
        <label for="name">Назва:</label>
        <input type="text" class="form-control" placeholder="Назва" name="Name" value="{Назва}" />
      </div>
    </form>

  </xsl:template>
  
  <xsl:template match="Контрагенти">

    <form id="ModalFormSendForm">
      <div class="form-group">
        <label for="code">
          Uid: <xsl:value-of select="uid"/>
        </label>
      </div>
      <div class="form-group">
        <label for="code">Код:</label>
        <input type="text" class="form-control" placeholder="Код" name="Code" value="{Код}" />
      </div>
      <div class="form-group">
        <label for="name">Назва:</label>
        <input type="text" class="form-control" placeholder="Назва" name="Name" value="{Назва}" />
      </div>
      <div class="form-group form-check">
        <label class="form-check-label">
          <input type="checkbox" class="form-check-input" name="Postachalnyk">
            <xsl:choose>
              <xsl:when test="Постачальник = '1'">
                <xsl:attribute name="checked">
                  <xsl:text>checked</xsl:text>
                </xsl:attribute>
              </xsl:when>
            </xsl:choose>
            <xsl:text>Постачальник</xsl:text>
          </input>
        </label>
      </div>
      <div class="form-group form-check">
        <label class="form-check-label">
          <input type="checkbox" class="form-check-input" name="Pokupec">
            <xsl:choose>
              <xsl:when test="Покупець = '1'">
                <xsl:attribute name="checked">
                  <xsl:text>checked</xsl:text>
                </xsl:attribute>
              </xsl:when>
            </xsl:choose>
            <xsl:text>Покупець</xsl:text>
          </input>
        </label>
      </div>
    </form>

  </xsl:template>

</xsl:stylesheet>

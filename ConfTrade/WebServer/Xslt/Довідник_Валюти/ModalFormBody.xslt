<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="html" indent="yes"/>

  <xsl:template match="Валюти">

    <form id="ModalFormSendForm">
      <div class="form-group">
        <label for="code">Uid: <xsl:value-of select="uid"/></label>
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

</xsl:stylesheet>

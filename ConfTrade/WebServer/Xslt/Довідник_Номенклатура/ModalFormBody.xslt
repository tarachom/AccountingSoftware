<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="html" indent="yes"/>

  <xsl:template match="Номенклатура">

    <form id="ModalFormSendForm">
      <div class="form-group">
        <label for="code">Uid: <xsl:value-of select="uid"/></label>
      </div>
      <div class="form-group">
        <label for="code">Дата створення: <xsl:value-of select="ДатаСтворення"/></label>
      </div>
      <div class="form-group">
        <label for="code">Код:</label>
        <input type="text" class="form-control" placeholder="Код" name="Code" value="{Код}" />
      </div>
      <div class="form-group">
        <label for="name">Назва:</label>
        <input type="text" class="form-control" placeholder="Назва" name="Name" value="{Назва}" />
      </div>
      <div class="form-group">
        <label for="name">Ціна:</label>
        <input type="text" class="form-control" placeholder="Ціна" name="Cena" value="{Ціна}" />
      </div>
      <div class="form-group">
        <label for="name">Кво:</label>
        <input type="text" class="form-control" placeholder="Кво" name="Kvo" value="{Кво}" />
      </div>
    </form>

  </xsl:template>

</xsl:stylesheet>

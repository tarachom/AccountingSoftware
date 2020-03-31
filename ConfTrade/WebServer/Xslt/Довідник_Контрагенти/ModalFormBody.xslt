<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="html" indent="yes"/>

  <xsl:param name="confobj" />
  <xsl:param name="cmd" />
  
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
      <div class="form-group">
        <label for="name">Група:</label>
        <input type="text" class="form-control" placeholder="Група" name="Group" id="Group" value="{Група}"/>
        <input type="text" class="form-control" placeholder="Група Назва" name="GroupName" id="GroupName" value="{Група_Назва}"/>
        <input type="button" value="..." onclick="Load_ListGroup()" />
        <div id="ListGroup"></div>
      </div>
    </form>

    <script type="text/javascript">

      function Load_ListGroup() {
         Load("ListGroup", "?confobj=<xsl:value-of select="$confobj"/>" +
            "&amp;cmd=ListGroup&amp;Parent=<xsl:value-of select="Група"/>" +
            "&amp;CurrentGroup=<xsl:value-of select="uid"/>");
      }

    </script>
    
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

<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="html" indent="yes"/>

  <xsl:param name="confobj" />
  <xsl:param name="cmd" />

  <xsl:param name="Offset" />
  <xsl:param name="Limit" />

  <xsl:template match="/">
    
    <h1>Довідник Номенклатура</h1>

    <div class="btn-group">
      <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#AddNewModal">Новий</button>
    </div>

    <div class="modal fade" id="AddNewModal">
      <div class="modal-dialog modal-lg modal-dialog-scrollable modal-dialog-centered">
        <div class="modal-content">

          <div class="modal-header">
            <h4 class="modal-title">Новий елемент</h4>
            <button type="button" class="close" data-dismiss="modal">x</button>
          </div>

          <div class="modal-body">
            <p style="color:red;" id="StatusInfo"></p>
            <form id="SendForm">
              <div class="form-group">
                <label for="code">Код:</label>
                <input type="text" class="form-control" placeholder="Код" name="Code"></input>
              </div>
              <div class="form-group">
                <label for="name">Назва:</label>
                <input type="text" class="form-control" placeholder="Назва" name="Name"></input>
              </div>
            </form>

            <script type="text/javascript">
              function SendForm() {
                $.post("http://localhost/5555/?confobj=<xsl:value-of select="$confobj"/>&amp;cmd=Add",
                  $("#SendForm").serialize(),
                  function(data, status) { $("#StatusInfo").html(data); }
                );
              }
            </script>
            
          </div>

          <div class="modal-footer">
            <button type="button" class="btn btn-primary" onclick="SendForm();">Submit</button>
            <button type="button" class="btn btn-danger" data-dismiss="modal">Закрити</button>
          </div>
        </div>
      </div>
    </div>

    <div class="modal fade" id="DeleteModal">
      <div class="modal-dialog modal-lg modal-dialog-centered">
        <div class="modal-content">

          <div class="modal-header">
            <h4 class="modal-title">Видалити елемент</h4>
            <button type="button" class="close" data-dismiss="modal">x</button>
          </div>

          <div class="modal-body">
            <p style="color:red;" id="StatusInfo"></p>
                        
          </div>

          <div class="modal-footer">
            <button type="button" class="btn btn-primary" onclick="SendForm();">Submit</button>
            <button type="button" class="btn btn-danger" data-dismiss="modal">Закрити</button>
          </div>
        </div>
      </div>
    </div>
    
    <div class="table-responsive">

      <table class="table table-bordered table-sm">
        <xsl:for-each select="root/Довідники.Номенклатура_Список/row">
          <tr>

            <td>
              <xsl:value-of select="Назва"/>
            </td>
            <td>
              <xsl:value-of select="Код"/>
            </td>
            <td>
              <a>
                <xsl:attribute name="href">
                  <xsl:text>?confobj=</xsl:text>
                  <xsl:value-of select="$confobj"/>
                  <xsl:text>&amp;cmd=Edit&amp;Uid=</xsl:text>
                  <xsl:value-of select="uid"/>
                </xsl:attribute>
                <xsl:text>Редагувати</xsl:text>
              </a>
            </td>
            <td>
              <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#DeleteModal">Видалити</button>
            </td>

          </tr>
        </xsl:for-each>
      </table>

    </div>

  </xsl:template>

</xsl:stylesheet>

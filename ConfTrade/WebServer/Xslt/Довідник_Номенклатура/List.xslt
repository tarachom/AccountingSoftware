<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="html" indent="yes"/>

  <xsl:param name="confobj" />
  <xsl:param name="cmd" />

  <xsl:param name="Offset" />
  <xsl:param name="Limit" />

  <xsl:template match="/">

    <h1>Довідник Номенклатура</h1>

    <div class="btn-group" style="margin-bottom:10px;">
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
              <div class="form-group">
                <label for="name">Ціна:</label>
                <input type="text" class="form-control" placeholder="Ціна" name="Cena"></input>
              </div>
              <div class="form-group">
                <label for="name">Кво:</label>
                <input type="text" class="form-control" placeholder="Кво" name="Kvo"></input>
              </div>
            </form>

            <script type="text/javascript">
              function SendForm() {
              $.post("http://localhost/5555/?confobj=<xsl:value-of select="$confobj"/>&amp;cmd=Add",
              $("#SendForm").serialize(), function(data, status) { $("#StatusInfo").html(data); } ); }
            </script>

          </div>

          <div class="modal-footer">
            <button type="button" class="btn btn-primary" onclick="SendForm();">Записати</button>
            <button type="button" class="btn btn-danger" data-dismiss="modal">Закрити</button>
          </div>
        </div>
      </div>
    </div>

    <div class="modal fade" id="EditModal">
      <div class="modal-dialog modal-lg modal-dialog-centered">
        <div class="modal-content">

          <div class="modal-header">
            <h4 class="modal-title">Редагувати елемент</h4>
            <button type="button" class="close" data-dismiss="modal">x</button>
          </div>

          <div class="modal-body">
            <p style="color:red;" id="SaveForm_StatusInfo"></p>
            <div id="EditContent"></div>
            <script type="text/javascript">
              var SaveForm_Uid = "";
              function OpenSaveForm(uid) {
                 $("#SaveForm_StatusInfo").html('');
                 Load("EditContent", "?confobj=<xsl:value-of select="$confobj"/>&amp;cmd=Edit&amp;Uid=" + uid);
                 SaveForm_Uid = uid;}

              function SaveForm() {
                 $.post("http://localhost/5555/?confobj=<xsl:value-of select="$confobj"/>&amp;cmd=Save&amp;Uid=" + SaveForm_Uid,
                 $("#SaveForm").serialize(), function(data, status) { $("#SaveForm_StatusInfo").html(data); } ); }
            </script>
          </div>

          <div class="modal-footer">
            <button type="button" class="btn btn-primary" onclick="SaveForm();">Записати</button>
            <button type="button" class="btn btn-danger" data-dismiss="modal">Закрити</button>
          </div>
        </div>
      </div>
    </div>

    <div class="table-responsive">

      <table class="table table-bordered table-sm table-hover">
        <col width="10%" />
        <col width="60%" />
        <col width="10%" />
        <col width="10%" />
        <col width="10%" />
        <thead class="thead-light">
          <tr>
            <th>Код</th>
            <th>Назва</th>
            <th>Ціна</th>
            <th>Кво</th>
            <th>Сума</th>
          </tr>
        </thead>
        <tbody>
          <xsl:for-each select="root/Довідники.Номенклатура_Список/row">
            <tr>
              <td>
                <xsl:value-of select="Код"/>
              </td>
              <td>
                <a href="#" onclick="OpenSaveForm('{uid}')" data-toggle="modal" data-target="#EditModal">
                  <xsl:value-of select="Назва"/>
                </a>
              </td>
              <td>
                <xsl:value-of select="Ціна"/>
              </td>
              <td>
                <xsl:value-of select="Кво"/>
              </td>
              <td>
                <xsl:value-of select="Ціна*Кво"/>
              </td>
            </tr>
          </xsl:for-each>
        </tbody>
      </table>

    </div>

  </xsl:template>

</xsl:stylesheet>

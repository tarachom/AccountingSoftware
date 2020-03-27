<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="html" indent="yes"/>

  <xsl:template name="ModalForm">
    <xsl:param name="confobj" />
    
    <div class="modal fade" id="ModalForm">
      <div class="modal-dialog modal-xl modal-dialog-centered">
        <div class="modal-content">

          <div class="modal-header">
            <h5 class="modal-title" id="ModalFormTitle"></h5>
            <button type="button" class="close" data-dismiss="modal">x</button>
          </div>

          <div class="modal-body">
            <div id="ModalFormBody"></div>
            <script type="text/javascript">
              var modalFormUid = "";
              function OpenModalForm(cmd, uid) {
              $("#ModalFormTitle").html( (cmd == "Edit" ? "Редагувати" : "Новий") + " елемент");
              $("#ModalFormStatusInfo").html("");
              Load("ModalFormBody", "?confobj=<xsl:value-of select="$confobj"/>&amp;cmd=" + cmd + "&amp;Uid=" + uid);
              modalFormUid = uid; }

              function SendModalForm() {
              Send("?confobj=<xsl:value-of select="$confobj"/>&amp;cmd=Save&amp;Uid=" + modalFormUid,
              $("#ModalFormSendForm").serialize(),
              function(data, status) { $("#ModalFormStatusInfo").html(data); }); }
            </script>
          </div>

          <div class="modal-footer">
            <p align="left" id="ModalFormStatusInfo"></p>
            <button type="button" class="btn btn-primary" onclick="SendModalForm();">Записати</button>
            <button type="button" class="btn btn-danger" data-dismiss="modal">Закрити</button>
          </div>
        </div>
      </div>
    </div>

  </xsl:template>
  
</xsl:stylesheet>

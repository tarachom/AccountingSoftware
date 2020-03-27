<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="html" indent="yes"/>

  <xsl:param name="confobj" />
  <xsl:param name="cmd" />

  <xsl:param name="Offset" />
  <xsl:param name="Limit" />

  <xsl:include href="../ModalForm.xslt" />
  <xsl:include href="../Function.xslt" />

  <xsl:template match="/">

    <h1>Довідник Валюти</h1>

    <div class="btn-group" style="margin-bottom:10px;">
      <button onclick="OpenModalForm('Add', 'Save', '')" type="button" class="btn btn-primary" data-toggle="modal" data-target="#ModalForm">Новий</button>
    </div>

    <xsl:call-template name="ModalForm">
      <xsl:with-param name="confobj" select="$confobj" />
    </xsl:call-template>

    <div class="table-responsive">

      <table class="table table-bordered table-sm table-hover">
        <col width="10%" />
        <col width="90%" />
        <thead class="thead-light">
          <tr>
            <th>Код</th>
            <th>Назва</th>
          </tr>
        </thead>
        <tbody>
          <xsl:for-each select="root/Довідник_Валюти_Список/row">
            <tr>
              <td>
                <xsl:value-of select="Код"/>
              </td>
              <td>
                <a href="#" onclick="OpenModalForm('Edit', 'Save', '{uid}')" data-toggle="modal" data-target="#ModalForm">
                  <xsl:choose>
                    <xsl:when test="normalize-space(Назва) != ''">
                      <xsl:value-of select="Назва"/>
                    </xsl:when>
                    <xsl:otherwise>
                      <xsl:text>&lt;...&gt;</xsl:text>
                    </xsl:otherwise>
                  </xsl:choose>
                </a>
              </td>
            </tr>
          </xsl:for-each>
        </tbody>
      </table>

    </div>

  </xsl:template>

</xsl:stylesheet>

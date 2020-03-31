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

    <h1>Довідник Номенклатура</h1>

    <div class="btn-group" style="margin-bottom:10px;">
      <button onclick="OpenModalForm('Add', 'Save', '')" type="button" class="btn btn-primary" data-toggle="modal" data-target="#ModalForm">Новий</button>
    </div>

    <xsl:call-template name="ModalForm">
      <xsl:with-param name="confobj" select="$confobj" />
    </xsl:call-template>

    <div class="table-responsive">

      <table class="table table-bordered table-sm table-hover">
        <col width="10%" />
        <col width="50%" />
        <col width="10%" />
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
            <th>Створений</th>
            <th>Валюта</th>
          </tr>
        </thead>
        <tbody>
          <xsl:for-each select="root/Довідник_Номенклатура_Список/row">
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
              <td>
                <xsl:value-of select="Ціна"/>
              </td>
              <td>
                <xsl:value-of select="Кво"/>
              </td>
              <td>
                <xsl:value-of select="Ціна*Кво"/>
              </td>
              <td>
                <xsl:value-of select="ДатаСтворення"/>
              </td>
              <td>
                <xsl:if test="normalize-space(Валюта) != ''">
                  <xsl:call-template name="GetNameOd">
                    <xsl:with-param name="list" select="/root/Довідник_Валюти_Список" />
                    <xsl:with-param name="uid" select="Валюта" />
                  </xsl:call-template>
                </xsl:if>
              </td>
            </tr>
          </xsl:for-each>
        </tbody>
      </table>

    </div>

  </xsl:template>

</xsl:stylesheet>

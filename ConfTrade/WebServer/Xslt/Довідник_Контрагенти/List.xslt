<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="html" indent="yes"/>

  <xsl:param name="confobj" />
  <xsl:param name="cmd" />

  <xsl:param name="Parent" />
  
  <xsl:param name="Offset" />
  <xsl:param name="Limit" />

  <xsl:include href="../ModalForm.xslt" />
  <xsl:include href="../Function.xslt" />

  <xsl:template match="/">

    <h1>Довідник Контрагенти</h1>

    <div class="btn-group" style="margin-bottom:10px;">
      <button onclick="OpenModalForm('Add', 'Save', '')" type="button" class="btn btn-primary" data-toggle="modal" data-target="#ModalForm">Новий</button>
      <button onclick="OpenModalForm('AddGroup', 'SaveGroup', '')" type="button" class="btn btn-primary" data-toggle="modal" data-target="#ModalForm">Нова група</button>
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
            <th>Група</th>
          </tr>
        </thead>
        <tbody>
          <xsl:for-each select="root/Довідник_Контрагенти_Групи_Список/row">
            <tr>
              <td>
                <xsl:value-of select="Код"/>
              </td>
              <td>
                <a href="#" onclick="Load('container', '?confobj={$confobj}&amp;cmd=List&amp;Parent={uid}');">
                  <xsl:choose>
                    <xsl:when test="normalize-space(Назва) != ''">
                      <xsl:value-of select="Назва"/>
                    </xsl:when>
                    <xsl:otherwise>
                      <xsl:text>&lt;...&gt;</xsl:text>
                    </xsl:otherwise>
                  </xsl:choose>
                </a>
                <xsl:text> | </xsl:text>
                <a href="#" onclick="OpenModalForm('EditGroup', 'SaveGroup', '{uid}')" data-toggle="modal" data-target="#ModalForm">
                  <xsl:text>Редагувати</xsl:text>
                </a>
              </td>
            </tr>
          </xsl:for-each>
        </tbody>
      </table>

    </div>
    
    <div class="table-responsive">

      <table class="table table-bordered table-sm table-hover">
        <col width="10%" />
        <col width="70%" />
        <col width="10%" />
        <col width="10%" />
        <thead class="thead-light">
          <tr>
            <th>Код</th>
            <th>Назва</th>
            <th>Постачальник</th>
            <th>Покупець</th>
          </tr>
        </thead>
        <tbody>
          <xsl:for-each select="root/Довідник_Контрагенти_Список/row">
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
                <xsl:value-of select="Постачальник"/>
              </td>
              <td>
                <xsl:value-of select="Покупець"/>
              </td>
            </tr>
          </xsl:for-each>
        </tbody>
      </table>

    </div>

  </xsl:template>

</xsl:stylesheet>

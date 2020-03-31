<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="html" indent="yes"/>

  <xsl:param name="confobj" />
  <xsl:param name="cmd" />
  <xsl:param name="Parent" />
  <xsl:param name="CurrentGroup" />
  
  <xsl:template match="/">

    <div class="table-responsive">

      <table class="table table-bordered table-sm table-hover">
        <col width="1%" />
        <col width="10%" />
        <col width="89%" />
        <thead class="thead-light">
          <tr>
            <th></th>
            <th>Код</th>
            <th>Група</th>
          </tr>

          <xsl:for-each select="root/parents/row">
            <xsl:sort data-type="number" select="level" order="descending"/>
            <tr>
              <th>
                <img src="/Images/open_folder.png" />
              </th>
              <th>
                <xsl:value-of select="Код"/>
              </th>
              <th>
                <a href="#" onclick="Load('ListGroup', '?confobj={$confobj}&amp;cmd=ListGroup&amp;Parent={puid}&amp;CurrentGroup={$CurrentGroup}');">
                  <xsl:choose>
                    <xsl:when test="normalize-space(Назва) != ''">
                      <xsl:value-of select="Назва"/>
                    </xsl:when>
                    <xsl:otherwise>
                      <xsl:text>&lt;...&gt;</xsl:text>
                    </xsl:otherwise>
                  </xsl:choose>
                </a>
              </th>
            </tr>
          </xsl:for-each>

        </thead>
        <tbody>
          <xsl:for-each select="root/Довідник_Контрагенти_Групи_Список/row">
            <tr>
              <td>
                <img src="/Images/folder.png" />
              </td>
              <td>
                <xsl:value-of select="Код"/>
              </td>
              <td>
                <a href="#" onclick="Load('ListGroup', '?confobj={$confobj}&amp;cmd=ListGroup&amp;Parent={uid}&amp;CurrentGroup={$CurrentGroup}');">
                  <xsl:choose>
                    <xsl:when test="normalize-space(Назва) != ''">
                      <xsl:value-of select="Назва"/>
                    </xsl:when>
                    <xsl:otherwise>
                      <xsl:text>&lt;...&gt;</xsl:text>
                    </xsl:otherwise>
                  </xsl:choose>
                </a> | 
                <a href="#" onclick="getElementById('Group').value = '{uid}';">
                  <xsl:text>Вибрати</xsl:text>
                </a>
              </td>
            </tr>
          </xsl:for-each>

        </tbody>
      </table>

    </div>

  </xsl:template>

</xsl:stylesheet>

<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" >

  <xsl:output method="html" indent="yes" />
  <xsl:include href="Include.xslt" />

  <xsl:template match="/">

    <table>
      <xsl:for-each select="Номенклатура_Список2/Field">
        <tr>
          <td>
            <xsl:value-of select="ПолнНаименование"/>
          </td>
          <td>
            <xsl:value-of select="ВидТовара"/>
          </td>
          <td>
            <xsl:value-of select="Артикул"/>
          </td>
          <td>
            <xsl:call-template name="GetNameOd">
              <xsl:with-param name="list">КлассификаторЕдИзм_Список</xsl:with-param>
              <xsl:with-param name="uid" select="БазоваяЕдиница" />
            </xsl:call-template>
          </td>
          <td>
            <xsl:value-of select="Вес"/>
          </td>
          <td>
            <xsl:call-template name="GetNameOd">
              <xsl:with-param name="list">Единици_Список</xsl:with-param>
              <xsl:with-param name="uid" select="ЕдиницаПоУмолчанию" />
            </xsl:call-template>
          </td>
          <td>
            <xsl:call-template name="GetNameOd">
              <xsl:with-param name="list">Валюти_Список</xsl:with-param>
              <xsl:with-param name="uid" select="ВалютаУчета" />
            </xsl:call-template>
          </td>
          <td>
            <xsl:value-of select="УчетнаяЦена"/>
          </td>
          <td>
            <xsl:value-of select="МинимальнийОстаток"/>
          </td>
          <td>
            <xsl:value-of select="СтавкаНДС"/>
          </td>
          <td>
            <xsl:value-of select="СтатьяИздержекУслуги"/>
          </td>
          <td>
            <xsl:value-of select="ТипТовара"/>
          </td>
          <td>
            <xsl:value-of select="ТорговаяНаценка"/>
          </td>
          <td>
            <xsl:value-of select="ШтрихКод"/>
          </td>
          <td>
            <xsl:value-of select="Комментарий"/>
          </td>
          <td>
            <xsl:value-of select="Транспорт"/>
          </td>
          <td>
            <xsl:value-of select="УслугиНаСебестоимость"/>
          </td>
          <td>
            <xsl:value-of select="ЛьготаНДС"/>
          </td>
          <td>
            <xsl:value-of select="КодЛьготи"/>
          </td>
          <td>
            <xsl:value-of select="КвоДляНН"/>
          </td>
          <td>
            <xsl:call-template name="GetNameOd">
              <xsl:with-param name="list">КодиУКТВЕД_Список</xsl:with-param>
              <xsl:with-param name="uid" select="КодУКТВЕД" />
            </xsl:call-template>
          </td>
          <td>
            <xsl:value-of select="Назва"/>
          </td>
          <td>
            <xsl:value-of select="Код"/>
          </td>
          <td>
            <xsl:call-template name="GetNameOd">
              <xsl:with-param name="list">Групи_Номенклатура_Список</xsl:with-param>
              <xsl:with-param name="uid" select="Група" />
            </xsl:call-template>
          </td>
        </tr>
      </xsl:for-each>
    </table>

  </xsl:template>
</xsl:stylesheet>

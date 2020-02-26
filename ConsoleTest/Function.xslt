<?xml version="1.0" encoding="utf-8"?>
<!--
/*
Copyright (C) 2019-2020 Tarakhomin Yuri Ivanovich
All rights reserved.

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

/*
Автор:    Тарахомин Юрій Іванович
Адреса:   Україна, м. Львів
Сайт:     find.org.ua
*/
-->
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="html" indent="yes"/>

  <xsl:template name="GetNameOd">
    <xsl:param name="list" />
    <xsl:param name="uid" />
    <xsl:for-each select="$list/row[uid = $uid]">
      <xsl:value-of select="Назва"/>
    </xsl:for-each>
  </xsl:template>

  <xsl:template name="GetNameSelect">
    <xsl:param name="pointer" />
    <xsl:param name="value" />
    <select>
      <xsl:for-each select="/root/Enums/Enum[Name = $pointer]/Fields/Field">
        <option>
          <xsl:attribute name="value">
            <xsl:value-of select="Value"/>
          </xsl:attribute>
          <xsl:if test="$value = Value">
            <xsl:attribute name="selected">selected</xsl:attribute>
          </xsl:if>
          <xsl:value-of select="Name"/>
        </option>
      </xsl:for-each>
    </select>
  </xsl:template>

</xsl:stylesheet>

<?xml version="1.0" encoding="utf-8"?>
<!--
/*
Copyright (C) 2019-2020 TARAKHOMYN YURIY IVANOVYCH
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
Сайт:     accounting.org.ua
*/
-->
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl">

  <xsl:output method="xml" indent="yes"/>

  <xsl:template match="/">

    <root>

      <xsl:variable name="НоменклатураНазва">Номенклатура</xsl:variable>
      <xsl:variable name="Номенклатура" select="Configuration/Directories/Directory[Name = $НоменклатураНазва]" />
      <xsl:variable name="Номенклатура.Таблиця" select="$Номенклатура/Table" />
      <xsl:variable name="Номенклатура.Поля" select="$Номенклатура/Fields/Field" />

      <xsl:variable name="Номенклатура-Вибірка">
        <Поле>Код</Поле>
        <Поле>Назва</Поле>
        <Поле>ПолнНаименование</Поле>
      </xsl:variable>

      <xsl:variable name="ВалютиНазва">Валюти</xsl:variable>
      <xsl:variable name="Валюти" select="Configuration/Directories/Directory[Name = $ВалютиНазва]" />
      <xsl:variable name="Валюти.Таблиця" select="$Валюти/Table" />
      <xsl:variable name="Валюти.Поля" select="$Валюти/Fields/Field" />

      <xsl:variable name="Валюти-Вибірка">
        <Поле>Код</Поле>
        <Поле>Назва</Поле>
        <Поле>Курс</Поле>
      </xsl:variable>

      <sql>

        SELECT

        <xsl:for-each select="msxsl:node-set($Номенклатура-Вибірка)/Поле">
          <xsl:variable name="НазваПоля" select="text()" />
          <xsl:variable name="Current" select="$Номенклатура.Поля[Name = $НазваПоля]" />
          <xsl:value-of select="concat($Номенклатура.Таблиця, '.', $Current/NameInTable, ' AS ', $НоменклатураНазва, '_', $Current/Name)" />,
        </xsl:for-each>

        <xsl:for-each select="msxsl:node-set($Валюти-Вибірка)/Поле">
          <xsl:variable name="НазваПоля" select="text()" />
          <xsl:variable name="Current" select="$Валюти.Поля[Name = $НазваПоля]" />
          <xsl:if test="position() > 1">
            <xsl:text>, 
          </xsl:text>
          </xsl:if>
          <xsl:value-of select="concat($Валюти.Таблиця, '.', $Current/NameInTable, ' AS ', $ВалютиНазва, '_', $Current/Name)" />
        </xsl:for-each>

        FROM TABLE

        <xsl:value-of select="$Номенклатура.Таблиця" />,
        <xsl:value-of select="$Валюти.Таблиця" />

      </sql>

      <sql>
        SELECT
        FROM
        WHERE
      </sql>
    </root>
  </xsl:template>

</xsl:stylesheet>

<!--
  <xsl:for-each select="$Номенклатура.Поля">
      <xsl:if test="position() > 1">
        ,
      </xsl:if>
      <xsl:value-of select="$Номенклатура.Таблиця" />.<xsl:value-of select="NameInTable" /> AS <xsl:value-of select="Name" />
    </xsl:for-each>
  -->

<!--
  
  <xsl:variable name="Номенклатура.Код"
       select="concat($Номенклатура.Таблиця, '.', $Номенклатура.Поля[Name = $Код]/NameInTable, ' AS ', $Довідник, $Код)" />

    <xsl:variable name="Номенклатура.Назва"
       select="concat($Номенклатура.Таблиця, '.', $Номенклатура.Поля[Name = 'Назва']/NameInTable)" />

    <xsl:variable name="Номенклатура.ПолнНаименование"
       select="concat($Номенклатура.Таблиця, '.', $Номенклатура.Поля[Name = 'ПолнНаименование']/NameInTable)" />
  -->
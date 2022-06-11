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
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="xml" indent="yes" />

  <!-- Заміщати існуючу колонку новою колонкою у випадку неможливості провести реструктуризацію даних (yes/no) -->
  <xsl:param name="ReplacementColumn" /> 

  <!-- Унікальний ключ для створення копій колонок -->
  <xsl:param name="KeyUID" />

  <xsl:template name="Template_AddColumn">
    <xsl:param name="TableName" />
    <xsl:param name="FieldNameInTable" />
    <xsl:param name="DataTypeCreate" />

    <info>
      <xsl:text disable-output-escaping="yes"> -&gt; </xsl:text> Додати колонку <xsl:value-of select="$FieldNameInTable"/> в таблицю <xsl:value-of select="$TableName"/>
    </info>

    <sql>
      <xsl:text>ALTER TABLE </xsl:text>
      <xsl:value-of select="$TableName"/>
      <xsl:text> ADD COLUMN "</xsl:text>
      <xsl:value-of select="$FieldNameInTable"/>
      <xsl:text>" </xsl:text>
      <xsl:value-of select="$DataTypeCreate"/>
      <xsl:text>;</xsl:text>
    </sql>
    
  </xsl:template>
  
  <xsl:template name="Template_CopyColumn">
    <xsl:param name="TableName" />
    <xsl:param name="FieldNameInTable" />
    <xsl:param name="DataTypeCreate" />

    <info> <xsl:text disable-output-escaping="yes"> -&gt; </xsl:text> Перейменувати колонку <xsl:value-of select="$FieldNameInTable"/> в таблиці <xsl:value-of select="$TableName"/><xsl:text> на </xsl:text>
      <xsl:value-of select="$FieldNameInTable"/><xsl:text>_old_</xsl:text><xsl:value-of select="$KeyUID"/>
    </info>

    <sql>
      <xsl:text>ALTER TABLE </xsl:text>
      <xsl:value-of select="$TableName"/>
      <xsl:text> RENAME COLUMN "</xsl:text>
      <xsl:value-of select="$FieldNameInTable"/>
      <xsl:text>" TO "</xsl:text>
      <xsl:value-of select="$FieldNameInTable"/>
      <xsl:text>_old_</xsl:text>
      <xsl:value-of select="$KeyUID"/>
      <xsl:text>";</xsl:text>
    </sql>

    <xsl:call-template name="Template_AddColumn">
      <xsl:with-param name="TableName" select="$TableName" />
      <xsl:with-param name="FieldNameInTable" select="NameInTable" />
      <xsl:with-param name="DataTypeCreate" select="Type/DataTypeCreate" />
    </xsl:call-template>

  </xsl:template>

  <xsl:template name="Template_DropColumn">
    <xsl:param name="TableName" />
    <xsl:param name="FieldNameInTable" />

    <info>
      <xsl:text disable-output-escaping="yes"> -&gt; </xsl:text> Видалити колонку <xsl:value-of select="$FieldNameInTable"/> в таблиці <xsl:value-of select="$TableName"/>
    </info>

    <sql>
      <xsl:text>ALTER TABLE </xsl:text>
      <xsl:value-of select="$TableName"/>
      <xsl:text> DROP COLUMN "</xsl:text>
      <xsl:value-of select="$FieldNameInTable"/>
      <xsl:text>";</xsl:text>
    </sql>
  </xsl:template>
  
  <xsl:template name="Template_DropOldColumn">
    <xsl:param name="TableName" />
    <xsl:param name="FieldNameInTable" />

    <info> <xsl:text disable-output-escaping="yes"> -&gt; </xsl:text> Видалити колонку <xsl:value-of select="$FieldNameInTable"/><xsl:text>_old_</xsl:text><xsl:value-of select="$KeyUID"/> в таблиці <xsl:value-of select="$TableName"/></info>

    <sql>
      <xsl:text>ALTER TABLE </xsl:text>
      <xsl:value-of select="$TableName"/>
      <xsl:text> DROP COLUMN "</xsl:text>
      <xsl:value-of select="$FieldNameInTable"/>
      <xsl:text>_old_</xsl:text>
      <xsl:value-of select="$KeyUID"/>
      <xsl:text>";</xsl:text>
    </sql>
  </xsl:template>

  <xsl:template name="Template_Control_Field">
    <xsl:param name="Control_Field" />
    <xsl:param name="Name" />
    <xsl:param name="TableName" />

    <xsl:for-each select="$Control_Field">

      <xsl:choose>
       <xsl:when test="IsExist = 'yes'">

        <xsl:choose>
          <xsl:when test="Type/Coincide = 'no'">

            <info>Таблиця <xsl:value-of select="$Name"/> (<xsl:value-of select="$TableName"/>), поле <xsl:value-of select="Name"/> (<xsl:value-of select="NameInTable"/><xsl:text>). </xsl:text>
              <xsl:text>Перетворити тип даних </xsl:text>(<xsl:value-of select="Type/DataType"/>, <xsl:value-of select="Type/UdtName"/>)<xsl:text disable-output-escaping="yes"> -&gt; </xsl:text> <xsl:value-of select="Type/ConfType"/></info>

            <xsl:choose>
              <xsl:when test="Type/DataType = 'text'">
                <xsl:choose>
                  <!-- Текст в масив -->
                  <xsl:when test="Type/ConfType = 'string[]'">
                    <info>Резструктуризація можлива: Текст в масив.</info>
                    <sql>BEGIN;</sql>

                    <xsl:call-template name="Template_CopyColumn">
                      <xsl:with-param name="TableName" select="$TableName" />
                      <xsl:with-param name="FieldNameInTable" select="NameInTable" />
                      <xsl:with-param name="DataTypeCreate" select="Type/DataTypeCreate" />
                    </xsl:call-template>

                    <!--
                    UPDATE public.test
                          SET text_mas = (SELECT array_agg(test2.text) FROM public.test AS test2 
                    WHERE test2.uid = test.uid) 
                    -->

                    <sql>
                      <xsl:text>UPDATE </xsl:text>
                      <xsl:value-of select="$TableName"/>
                      <xsl:text> SET </xsl:text>
                      <xsl:value-of select="NameInTable"/>
                      <xsl:value-of select="concat(' = (SELECT array_agg(', 't.', NameInTable, '_old_', $KeyUID, ') FROM ')"/>
                      <xsl:value-of select="$TableName"/>
                      <xsl:text> AS t WHERE t.uid = </xsl:text>
                      <xsl:value-of select="$TableName"/>
                      <xsl:text>.uid AND t.</xsl:text>
                      <xsl:value-of select="NameInTable"/>
                      <xsl:text>_old_</xsl:text>
                      <xsl:value-of select="$KeyUID"/>
                      <xsl:text> != NULL);</xsl:text>
                    </sql>

                    <xsl:call-template name="Template_DropOldColumn">
                      <xsl:with-param name="TableName" select="$TableName" />
                      <xsl:with-param name="FieldNameInTable" select="NameInTable" />
                    </xsl:call-template>

                    <sql>COMMIT;</sql>
                  </xsl:when>
                  <xsl:otherwise>

                    <xsl:choose>
                      <xsl:when test="$ReplacementColumn = 'yes'">

                        <info>Заміна колонки! Втрата даних!</info>
                        <sql>BEGIN;</sql>
                        <xsl:call-template name="Template_DropColumn">
                          <xsl:with-param name="TableName" select="$TableName" />
                          <xsl:with-param name="FieldNameInTable" select="NameInTable" />
                        </xsl:call-template>
                        <xsl:call-template name="Template_AddColumn">
                          <xsl:with-param name="TableName" select="$TableName" />
                          <xsl:with-param name="FieldNameInTable" select="NameInTable" />
                          <xsl:with-param name="DataTypeCreate" select="Type/DataTypeCreate" />
                        </xsl:call-template>
                        <sql>COMMIT;</sql>

                      </xsl:when>
                      <xsl:otherwise>

                        <info>Реструкторизація неможлива, створення копії колонки!</info>
                        <sql>BEGIN;</sql>
                        <xsl:call-template name="Template_CopyColumn">
                          <xsl:with-param name="TableName" select="$TableName" />
                          <xsl:with-param name="FieldNameInTable" select="NameInTable" />
                          <xsl:with-param name="DataTypeCreate" select="Type/DataTypeCreate" />
                        </xsl:call-template>
                        <sql>COMMIT;</sql>

                      </xsl:otherwise>
                    </xsl:choose>

                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>

              <xsl:when test="Type/DataType = 'integer' or 
                              Type/DataType = 'numeric'">
                <xsl:choose>
                  <!-- Число в масив -->
                  <xsl:when test="Type/ConfType = 'integer[]' or Type/ConfType = 'numeric[]'">
                    <info>Резструктуризація можлива: Число в масив.</info>
                    <sql>BEGIN;</sql>

                    <xsl:call-template name="Template_CopyColumn">
                      <xsl:with-param name="TableName" select="$TableName" />
                      <xsl:with-param name="FieldNameInTable" select="NameInTable" />
                      <xsl:with-param name="DataTypeCreate" select="Type/DataTypeCreate" />
                    </xsl:call-template>

                    <!-- UPDATE public.test SET text_mas = (SELECT array_agg(test2.text) FROM public.test AS test2 WHERE test2.uid = test.uid) -->

                    <sql>
                      <xsl:text>UPDATE </xsl:text>
                      <xsl:value-of select="$TableName"/>
                      <xsl:text> SET </xsl:text>
                      <xsl:value-of select="NameInTable"/>
                      <xsl:value-of select="concat(' = (SELECT array_agg(', 't.', NameInTable, '_old_', $KeyUID, ') FROM ')"/>
                      <xsl:value-of select="$TableName"/>
                      <xsl:text> AS t WHERE t.uid = </xsl:text>
                      <xsl:value-of select="$TableName"/>
                      <xsl:text>.uid AND t.</xsl:text>
                      <xsl:value-of select="NameInTable"/>
                      <xsl:text>_old_</xsl:text>
                      <xsl:value-of select="$KeyUID"/>
                      <xsl:text> != NULL);</xsl:text>
                    </sql>

                    <xsl:call-template name="Template_DropOldColumn">
                      <xsl:with-param name="TableName" select="$TableName" />
                      <xsl:with-param name="FieldNameInTable" select="NameInTable" />
                    </xsl:call-template>

                    <sql>COMMIT;</sql>
                  </xsl:when>
                  <!-- Ціле число в число з комою -->
                  <xsl:when test="Type/ConfType = 'numeric' and Type/DataType = 'integer'">
                    <info>Резструктуризація можлива: Ціле число в число з комою.</info>
                    <sql>BEGIN;</sql>

                    <xsl:call-template name="Template_CopyColumn">
                      <xsl:with-param name="TableName" select="$TableName" />
                      <xsl:with-param name="FieldNameInTable" select="NameInTable" />
                      <xsl:with-param name="DataTypeCreate" select="Type/DataTypeCreate" />
                    </xsl:call-template>

                    <sql>
                      <xsl:text>UPDATE </xsl:text>
                      <xsl:value-of select="$TableName"/>
                      <xsl:text> SET </xsl:text>
                      <xsl:value-of select="NameInTable"/>
                      <xsl:text> = (SELECT t.</xsl:text>
                      <xsl:value-of select="NameInTable"/>
                      <xsl:text>_old_</xsl:text>
                      <xsl:value-of select="$KeyUID"/>
                      <xsl:text> FROM </xsl:text>
                      <xsl:value-of select="$TableName"/>
                      <xsl:text> AS t WHERE t.uid = </xsl:text>
                      <xsl:value-of select="$TableName"/>
                      <xsl:text>.uid);</xsl:text>
                    </sql>

                    <xsl:call-template name="Template_DropOldColumn">
                      <xsl:with-param name="TableName" select="$TableName" />
                      <xsl:with-param name="FieldNameInTable" select="NameInTable" />
                    </xsl:call-template>

                    <sql>COMMIT;</sql>
                  </xsl:when>
                  <!-- Число з комою в ціле число -->
                  <xsl:when test="Type/ConfType = 'integer' and Type/DataType = 'numeric'">
                    <info>Резструктуризація можлива (Часткова втрата даних: значення після коми будуть втрачення): Число з комою в ціле число.</info>
                    <sql>BEGIN;</sql>

                    <xsl:call-template name="Template_CopyColumn">
                      <xsl:with-param name="TableName" select="$TableName" />
                      <xsl:with-param name="FieldNameInTable" select="NameInTable" />
                      <xsl:with-param name="DataTypeCreate" select="Type/DataTypeCreate" />
                    </xsl:call-template>

                    <sql>
                      <xsl:text>UPDATE </xsl:text>
                      <xsl:value-of select="$TableName"/>
                      <xsl:text> SET </xsl:text>
                      <xsl:value-of select="NameInTable"/>
                      <xsl:text> = (SELECT trunc(t.</xsl:text>
                      <xsl:value-of select="NameInTable"/>
                      <xsl:text>_old_</xsl:text>
                      <xsl:value-of select="$KeyUID"/>
                      <xsl:text>) FROM </xsl:text>
                      <xsl:value-of select="$TableName"/>
                      <xsl:text> AS t WHERE t.uid = </xsl:text>
                      <xsl:value-of select="$TableName"/>
                      <xsl:text>.uid);</xsl:text>
                    </sql>

                    <xsl:call-template name="Template_DropOldColumn">
                      <xsl:with-param name="TableName" select="$TableName" />
                      <xsl:with-param name="FieldNameInTable" select="NameInTable" />
                    </xsl:call-template>

                    <sql>COMMIT;</sql>
                  </xsl:when>
                  <xsl:otherwise>

                    <xsl:choose>
                      <xsl:when test="$ReplacementColumn = 'yes'">

                        <info>Заміна колонки! Втрата даних!</info>
                        <sql>BEGIN;</sql>
                        <xsl:call-template name="Template_DropColumn">
                          <xsl:with-param name="TableName" select="$TableName" />
                          <xsl:with-param name="FieldNameInTable" select="NameInTable" />
                        </xsl:call-template>
                        <xsl:call-template name="Template_AddColumn">
                          <xsl:with-param name="TableName" select="$TableName" />
                          <xsl:with-param name="FieldNameInTable" select="NameInTable" />
                          <xsl:with-param name="DataTypeCreate" select="Type/DataTypeCreate" />
                        </xsl:call-template>
                        <sql>COMMIT;</sql>

                      </xsl:when>
                      <xsl:otherwise>

                        <info>Реструкторизація неможлива, створення копії колонки!</info>
                        <sql>BEGIN;</sql>
                        <xsl:call-template name="Template_CopyColumn">
                          <xsl:with-param name="TableName" select="$TableName" />
                          <xsl:with-param name="FieldNameInTable" select="NameInTable" />
                          <xsl:with-param name="DataTypeCreate" select="Type/DataTypeCreate" />
                        </xsl:call-template>
                        <sql>COMMIT;</sql>

                      </xsl:otherwise>
                    </xsl:choose>

                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>

              <xsl:when test="(Type/DataType = 'ARRAY' and Type/UdtName = '_text') or
                              (Type/DataType = 'ARRAY' and Type/UdtName = '_int4') or 
                              (Type/DataType = 'ARRAY' and Type/UdtName = '_numeric')">
                <xsl:choose>
                  <!-- Масив в текст -->
                  <xsl:when test="Type/ConfType = 'string'">
                    <info>Резструктуризація можлива: Масив в текст.</info>
                    <sql>BEGIN;</sql>

                    <xsl:call-template name="Template_CopyColumn">
                      <xsl:with-param name="TableName" select="$TableName" />
                      <xsl:with-param name="FieldNameInTable" select="NameInTable" />
                      <xsl:with-param name="DataTypeCreate" select="Type/DataTypeCreate" />
                    </xsl:call-template>

                    <!-- UPDATE test SET text = (SELECT array_to_string(text_mas, ', ') FROM test AS t Where t.uid = test.uid); -->

                    <sql>
                      <xsl:text>UPDATE </xsl:text>
                      <xsl:value-of select="$TableName"/>
                      <xsl:text> SET </xsl:text>
                      <xsl:value-of select="NameInTable"/>
                      <xsl:text> = (SELECT array_to_string(t.</xsl:text>
                      <xsl:value-of select="NameInTable"/>
                      <xsl:text>_old_</xsl:text>
                      <xsl:value-of select="$KeyUID"/>
                      <xsl:text>, ', ') FROM </xsl:text>
                      <xsl:value-of select="$TableName"/>
                      <xsl:text> AS t WHERE t.uid = </xsl:text>
                      <xsl:value-of select="$TableName"/>
                      <xsl:text>.uid AND t.</xsl:text>
                      <xsl:value-of select="NameInTable"/>
                      <xsl:text>_old_</xsl:text>
                      <xsl:value-of select="$KeyUID"/>
                      <xsl:text> != NULL);</xsl:text>
                    </sql>

                    <xsl:call-template name="Template_DropOldColumn">
                      <xsl:with-param name="TableName" select="$TableName" />
                      <xsl:with-param name="FieldNameInTable" select="NameInTable" />
                    </xsl:call-template>

                    <sql>COMMIT;</sql>
                  </xsl:when>
                  <xsl:otherwise>
                    
                    <xsl:choose>
                      <xsl:when test="$ReplacementColumn = 'yes'">

                        <info>Заміна колонки! Втрата даних!</info>
                        <sql>BEGIN;</sql>
                        <xsl:call-template name="Template_DropColumn">
                          <xsl:with-param name="TableName" select="$TableName" />
                          <xsl:with-param name="FieldNameInTable" select="NameInTable" />
                        </xsl:call-template>
                        <xsl:call-template name="Template_AddColumn">
                          <xsl:with-param name="TableName" select="$TableName" />
                          <xsl:with-param name="FieldNameInTable" select="NameInTable" />
                          <xsl:with-param name="DataTypeCreate" select="Type/DataTypeCreate" />
                        </xsl:call-template>
                        <sql>COMMIT;</sql>

                      </xsl:when>
                      <xsl:otherwise>

                        <info>Реструкторизація неможлива, створення копії колонки!</info>
                        <sql>BEGIN;</sql>
                        <xsl:call-template name="Template_CopyColumn">
                          <xsl:with-param name="TableName" select="$TableName" />
                          <xsl:with-param name="FieldNameInTable" select="NameInTable" />
                          <xsl:with-param name="DataTypeCreate" select="Type/DataTypeCreate" />
                        </xsl:call-template>
                        <sql>COMMIT;</sql>

                      </xsl:otherwise>
                    </xsl:choose>
                    
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>
              
              <xsl:when test="Type/DataType = 'boolean' or 
                              Type/DataType = 'date' or  
                              Type/DataType = 'time without time zone' or 
                              Type/DataType = 'timestamp without time zone' or 
                              Type/DataType = 'uuid' ">
                <xsl:choose>
                  <!-- Значення в Текст -->
                  <xsl:when test="Type/ConfType = 'string'">
                    <info>Резструктуризація можлива: Дані в текст.</info>
                    <sql>BEGIN;</sql>

                    <xsl:call-template name="Template_CopyColumn">
                      <xsl:with-param name="TableName" select="$TableName" />
                      <xsl:with-param name="FieldNameInTable" select="NameInTable" />
                      <xsl:with-param name="DataTypeCreate" select="Type/DataTypeCreate" />
                    </xsl:call-template>

                    <sql>
                      <xsl:text>UPDATE </xsl:text>
                      <xsl:value-of select="$TableName"/>
                      <xsl:text> SET </xsl:text>
                      <xsl:value-of select="NameInTable"/>
                      <xsl:text> = (SELECT t.</xsl:text>
                      <xsl:value-of select="NameInTable"/>
                      <xsl:text>_old_</xsl:text>
                      <xsl:value-of select="$KeyUID"/>
                      <xsl:text> FROM </xsl:text>
                      <xsl:value-of select="$TableName"/>
                      <xsl:text> AS t WHERE t.uid = </xsl:text>
                      <xsl:value-of select="$TableName"/>
                      <xsl:text>.uid);</xsl:text>
                    </sql>

                    <xsl:call-template name="Template_DropOldColumn">
                      <xsl:with-param name="TableName" select="$TableName" />
                      <xsl:with-param name="FieldNameInTable" select="NameInTable" />
                    </xsl:call-template>

                    <sql>COMMIT;</sql>
                  </xsl:when>
                  <!-- Дата -> Дата та час -->
                  <xsl:when test="Type/ConfType = 'datetime' and Type/DataType = 'date'">
                    <info>Резструктуризація можлива: Дата в Дата та час.</info>
                    <sql>BEGIN;</sql>

                    <xsl:call-template name="Template_CopyColumn">
                      <xsl:with-param name="TableName" select="$TableName" />
                      <xsl:with-param name="FieldNameInTable" select="NameInTable" />
                      <xsl:with-param name="DataTypeCreate" select="Type/DataTypeCreate" />
                    </xsl:call-template>

                    <sql>
                      <xsl:text>UPDATE </xsl:text>
                      <xsl:value-of select="$TableName"/>
                      <xsl:text> SET </xsl:text>
                      <xsl:value-of select="NameInTable"/>
                      <xsl:text> = (SELECT t.</xsl:text>
                      <xsl:value-of select="NameInTable"/>
                      <xsl:text>_old_</xsl:text>
                      <xsl:value-of select="$KeyUID"/>
                      <xsl:text> FROM </xsl:text>
                      <xsl:value-of select="$TableName"/>
                      <xsl:text> AS t WHERE t.uid = </xsl:text>
                      <xsl:value-of select="$TableName"/>
                      <xsl:text>.uid);</xsl:text>
                    </sql>

                    <xsl:call-template name="Template_DropOldColumn">
                      <xsl:with-param name="TableName" select="$TableName" />
                      <xsl:with-param name="FieldNameInTable" select="NameInTable" />
                    </xsl:call-template>

                    <sql>COMMIT;</sql>
                  </xsl:when>
                  <!-- Дата та час -> Дата -->
                  <xsl:when test="Type/ConfType = 'date' and Type/DataType = 'timestamp without time zone'">
                    <info>Резструктуризація можлива (часткова втрата даних): Дата та час в Дата.</info>
                    <sql>BEGIN;</sql>

                    <xsl:call-template name="Template_CopyColumn">
                      <xsl:with-param name="TableName" select="$TableName" />
                      <xsl:with-param name="FieldNameInTable" select="NameInTable" />
                      <xsl:with-param name="DataTypeCreate" select="Type/DataTypeCreate" />
                    </xsl:call-template>

                    <sql>
                      <xsl:text>UPDATE </xsl:text>
                      <xsl:value-of select="$TableName"/>
                      <xsl:text> SET </xsl:text>
                      <xsl:value-of select="NameInTable"/>
                      <xsl:text> = (SELECT t.</xsl:text>
                      <xsl:value-of select="NameInTable"/>
                      <xsl:text>_old_</xsl:text>
                      <xsl:value-of select="$KeyUID"/>
                      <xsl:text> FROM </xsl:text>
                      <xsl:value-of select="$TableName"/>
                      <xsl:text> AS t WHERE t.uid = </xsl:text>
                      <xsl:value-of select="$TableName"/>
                      <xsl:text>.uid);</xsl:text>
                    </sql>

                    <xsl:call-template name="Template_DropOldColumn">
                      <xsl:with-param name="TableName" select="$TableName" />
                      <xsl:with-param name="FieldNameInTable" select="NameInTable" />
                    </xsl:call-template>

                    <sql>COMMIT;</sql>
                  </xsl:when>
                  <!-- Дата та час -> Час -->
                  <xsl:when test="Type/ConfType = 'time' and Type/DataType = 'timestamp without time zone'">
                    <info>Резструктуризація можлива (часткова втрата даних): Дата та час в Час.</info>
                    <sql>BEGIN;</sql>

                    <xsl:call-template name="Template_CopyColumn">
                      <xsl:with-param name="TableName" select="$TableName" />
                      <xsl:with-param name="FieldNameInTable" select="NameInTable" />
                      <xsl:with-param name="DataTypeCreate" select="Type/DataTypeCreate" />
                    </xsl:call-template>

                    <sql>
                      <xsl:text>UPDATE </xsl:text>
                      <xsl:value-of select="$TableName"/>
                      <xsl:text> SET </xsl:text>
                      <xsl:value-of select="NameInTable"/>
                      <xsl:text> = (SELECT t.</xsl:text>
                      <xsl:value-of select="NameInTable"/>
                      <xsl:text>_old_</xsl:text>
                      <xsl:value-of select="$KeyUID"/>
                      <xsl:text> FROM </xsl:text>
                      <xsl:value-of select="$TableName"/>
                      <xsl:text> AS t WHERE t.uid = </xsl:text>
                      <xsl:value-of select="$TableName"/>
                      <xsl:text>.uid);</xsl:text>
                    </sql>

                    <xsl:call-template name="Template_DropOldColumn">
                      <xsl:with-param name="TableName" select="$TableName" />
                      <xsl:with-param name="FieldNameInTable" select="NameInTable" />
                    </xsl:call-template>

                    <sql>COMMIT;</sql>
                  </xsl:when>
                  <xsl:otherwise>

                    <xsl:choose>
                      <xsl:when test="$ReplacementColumn = 'yes'">

                        <info>Заміна колонки! Втрата даних!</info>
                        <sql>BEGIN;</sql>
                        <xsl:call-template name="Template_DropColumn">
                          <xsl:with-param name="TableName" select="$TableName" />
                          <xsl:with-param name="FieldNameInTable" select="NameInTable" />
                        </xsl:call-template>
                        <xsl:call-template name="Template_AddColumn">
                          <xsl:with-param name="TableName" select="$TableName" />
                          <xsl:with-param name="FieldNameInTable" select="NameInTable" />
                          <xsl:with-param name="DataTypeCreate" select="Type/DataTypeCreate" />
                        </xsl:call-template>
                        <sql>COMMIT;</sql>
                        
                      </xsl:when>
                      <xsl:otherwise>

                        <info>Реструкторизація неможлива, створення копії колонки!</info>
                        <sql>BEGIN;</sql>
                        <xsl:call-template name="Template_CopyColumn">
                          <xsl:with-param name="TableName" select="$TableName" />
                          <xsl:with-param name="FieldNameInTable" select="NameInTable" />
                          <xsl:with-param name="DataTypeCreate" select="Type/DataTypeCreate" />
                        </xsl:call-template>
                        <sql>COMMIT;</sql>
                        
                      </xsl:otherwise>
                    </xsl:choose>
                    
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>

              <xsl:otherwise>
                <info>ПОМИЛКА! Не вдалось знайти спосіб реструктуризації для даного типу.</info>
              </xsl:otherwise>
            </xsl:choose>

          </xsl:when>
          <xsl:when test="Type/Coincide = 'clear'">
            
            <info>Таблиця <xsl:value-of select="$Name"/> (<xsl:value-of select="$TableName"/>), поле <xsl:value-of select="Name"/> (<xsl:value-of select="NameInTable"/><xsl:text>). </xsl:text></info>
            
            <xsl:choose>
              <xsl:when test="$ReplacementColumn = 'yes'">
                <info>Очищення колонки! Втрата даних!</info>
                <sql>BEGIN;</sql>
                <sql>
                  <xsl:text>UPDATE </xsl:text>
                  <xsl:value-of select="$TableName"/>
                  <xsl:text> SET </xsl:text>
                  <xsl:value-of select="NameInTable"/>
                  <xsl:text> = NULL;</xsl:text>
                </sql>
                <sql>COMMIT;</sql>
              </xsl:when>
              <xsl:otherwise>
                <info>Реструкторизація неможлива, створення копії колонки!</info>
                <sql>BEGIN;</sql>
                <xsl:call-template name="Template_CopyColumn">
                  <xsl:with-param name="TableName" select="$TableName" />
                  <xsl:with-param name="FieldNameInTable" select="NameInTable" />
                  <xsl:with-param name="DataTypeCreate" select="Type/DataTypeCreate" />
                </xsl:call-template>
                <sql>COMMIT;</sql>
              </xsl:otherwise>
            </xsl:choose>
            
          </xsl:when>
        </xsl:choose>
         
       </xsl:when>
       <xsl:when test="IsExist = 'no'">

          <xsl:for-each select="FieldCreate">
            <info>Додати колонку <xsl:value-of select="Name"/> (<xsl:value-of select="NameInTable"/>, тип <xsl:value-of select="DataType"/>)  в таблицю <xsl:value-of select="$Name"/> (<xsl:value-of select="$TableName"/>)</info>
            <sql>
              <xsl:text>ALTER TABLE </xsl:text>
              <xsl:value-of select="$TableName"/>
              <xsl:text> ADD COLUMN "</xsl:text>
              <xsl:value-of select="NameInTable"/>
              <xsl:text>" </xsl:text>
              <xsl:value-of select="DataType"/>
              <xsl:text>;</xsl:text>
            </sql>

          </xsl:for-each>

        </xsl:when>
       <xsl:when test="IsExist = 'delete'">

          <info>Видалити колонку <xsl:value-of select="Name"/> (<xsl:value-of select="NameInTable"/>)  в таблиці <xsl:value-of select="$Name"/> (<xsl:value-of select="$TableName"/>)</info>
          <sql>
            <xsl:text>ALTER TABLE </xsl:text>
            <xsl:value-of select="$TableName"/>
            <xsl:text> DROP COLUMN "</xsl:text>
            <xsl:value-of select="NameInTable"/>
            <xsl:text>" </xsl:text>
            <xsl:text>;</xsl:text>
          </sql>

        </xsl:when>
      </xsl:choose>

    </xsl:for-each>

  </xsl:template>

  <xsl:template match="/">

    <root>

      <xsl:for-each select="root/Control_Table">

        <xsl:variable name="DirectoryName" select="Name" />
        <xsl:variable name="TableName" select="Table" />
	    <xsl:variable name="TableType" select="Type" />
		  
        <xsl:choose>
          <xsl:when test="IsExist = 'yes'">

            <xsl:call-template name="Template_Control_Field">
              <xsl:with-param name="Control_Field" select="Control_Field" />
              <xsl:with-param name="Name" select="$DirectoryName" />
              <xsl:with-param name="TableName" select="$TableName" />
            </xsl:call-template>

          </xsl:when>
          <xsl:when test="IsExist = 'no'">

            <xsl:for-each select="TableCreate">
              <info>Створити таблицю <xsl:value-of select="$DirectoryName"/> (<xsl:value-of select="$TableName"/>)</info>
              <sql>
                <xsl:text>CREATE TABLE IF NOT EXISTS </xsl:text>
                <xsl:value-of select="$TableName"/>
                <xsl:text> (</xsl:text>
                <xsl:text>uid uuid NOT NULL, </xsl:text>
				<xsl:if test="$TableType = 'RegisterAccumulation'">
					<!-- Прибуток true, Витрата false -->
					<xsl:text>period timestamp without time zone NOT NULL, </xsl:text>
					<xsl:text>income bool NOT NULL, </xsl:text>
					<xsl:text>owner uuid NOT NULL, </xsl:text>
				</xsl:if>
                <xsl:for-each select="FieldCreate">
                  <xsl:text> "</xsl:text>
                  <xsl:value-of select="NameInTable"/>
                  <xsl:text>" </xsl:text>
                  <xsl:value-of select="DataType"/>
                  <xsl:text>, </xsl:text>
                </xsl:for-each>
                <xsl:text>PRIMARY KEY(uid));</xsl:text>
              </sql>
			  <xsl:if test="$TableType = 'RegisterAccumulation'">
				  <sql>
					  <xsl:text>CREATE INDEX IF NOT EXISTS </xsl:text>
					  <xsl:value-of select="$TableName"/>
					  <xsl:text>_period_idx ON </xsl:text>
					  <xsl:value-of select="$TableName"/>
					  <xsl:text> (period);</xsl:text>
				  </sql>
				 <sql>
					<xsl:text>CREATE INDEX IF NOT EXISTS </xsl:text>
					<xsl:value-of select="$TableName"/>
					<xsl:text>_income_idx ON </xsl:text>
					<xsl:value-of select="$TableName"/>
					<xsl:text> (income);</xsl:text>
				 </sql>
				 <sql>
					<xsl:text>CREATE INDEX IF NOT EXISTS </xsl:text>
					<xsl:value-of select="$TableName"/>
					<xsl:text>_owner_idx ON </xsl:text>
					<xsl:value-of select="$TableName"/>
					<xsl:text> (owner);</xsl:text>
				 </sql>
			  </xsl:if>
            </xsl:for-each>

          </xsl:when>
          <xsl:when test="IsExist = 'delete'">

            <info>Видалити таблицю <xsl:value-of select="$DirectoryName"/> (<xsl:value-of select="$TableName"/>)</info>
            <sql>
              <xsl:text>DROP TABLE </xsl:text>
              <xsl:value-of select="$TableName"/>
              <xsl:text>;</xsl:text>
            </sql>
            
          </xsl:when>
        </xsl:choose>

        <xsl:for-each select="Control_TabularParts">
          
          <xsl:variable name="TablePartName" select="Name" />
          <xsl:variable name="TabularParts_TableName" select="Table" />
          <xsl:variable name="IsCreateOwnerField" select="IsCreateOwner" />
          
          <xsl:choose>
            <xsl:when test="IsExist = 'yes'">

              <xsl:call-template name="Template_Control_Field">
                <xsl:with-param name="Control_Field" select="Control_Field" />
                <xsl:with-param name="Name" select="$TablePartName" />
                <xsl:with-param name="TableName" select="$TabularParts_TableName" />
              </xsl:call-template>

            </xsl:when>
            <xsl:when test="IsExist = 'no'">

              <xsl:for-each select="TableCreate">
                <info>Створити таблицю <xsl:value-of select="$TablePartName"/> (<xsl:value-of select="$TabularParts_TableName"/>)</info>
                <sql>
                  <xsl:text>CREATE TABLE IF NOT EXISTS </xsl:text>
                  <xsl:value-of select="$TabularParts_TableName"/>
                  <xsl:text> (</xsl:text>
                  <xsl:text>uid uuid NOT NULL, </xsl:text>
                  <xsl:if test="$IsCreateOwnerField = 'yes'">
                    <xsl:text>owner uuid NOT NULL, </xsl:text>
                  </xsl:if>
                  <xsl:for-each select="FieldCreate">
                    <xsl:text>"</xsl:text>
                    <xsl:value-of select="NameInTable"/>
                    <xsl:text>" </xsl:text>
                    <xsl:value-of select="DataType"/>
                    <xsl:text>, </xsl:text>
                  </xsl:for-each>
                  <xsl:text>PRIMARY KEY(uid));</xsl:text>
                </sql>
                <xsl:if test="$IsCreateOwnerField = 'yes'">
                  <sql>
                    <xsl:text>CREATE INDEX IF NOT EXISTS </xsl:text>
					<xsl:value-of select="$TabularParts_TableName"/>
					<xsl:text>_owner_idx ON </xsl:text>
                    <xsl:value-of select="$TabularParts_TableName"/>
                    <xsl:text> (owner);</xsl:text>
                  </sql>
                </xsl:if>
              </xsl:for-each>

            </xsl:when>
            <xsl:when test="IsExist = 'delete'">

              <info>Видалити таблицю <xsl:value-of select="$DirectoryName"/> (<xsl:value-of select="$TableName"/>)</info>
              <sql>
                <xsl:text>DROP TABLE </xsl:text>
                <xsl:value-of select="$TabularParts_TableName"/>
                <xsl:text>;</xsl:text>
              </sql>

            </xsl:when>
            
          </xsl:choose>

        </xsl:for-each>

      </xsl:for-each>

    </root>

  </xsl:template>

</xsl:stylesheet>
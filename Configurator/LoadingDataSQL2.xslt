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
    <xsl:output method="xml" indent="yes"/>

    <xsl:template match="/">
		<xsl:variable name="escape">'</xsl:variable>
		<xsl:variable name="replace">&apos;</xsl:variable>

		<root>
		
		<xsl:for-each select="root/sql">

			<row>
			
				<sql>INSERT INTO <xsl:value-of select="@tab"/>
				<xsl:text> (</xsl:text>
			
				<xsl:for-each select="row">
					<xsl:if test="position() != 1">
						<xsl:text>, </xsl:text>
					</xsl:if>
					<xsl:value-of select="@name"/>   
				</xsl:for-each>

				<xsl:text>) VALUES (</xsl:text>

				<xsl:for-each select="row">
					<xsl:if test="position() != 1">
						<xsl:text>, </xsl:text>
					</xsl:if>
					<xsl:value-of select="concat('@', @name)"/>
				</xsl:for-each>
			
				<xsl:text>) ON CONFLICT (uid) DO UPDATE SET </xsl:text>
			
				<xsl:for-each select="row">
					<xsl:if test="position() != 1">
						<xsl:text>, </xsl:text>
					</xsl:if>
					<xsl:value-of select="@name"/>
					<xsl:text> = </xsl:text>
					<xsl:value-of select="concat('@', @name)"/>
				</xsl:for-each>
			
				</sql>

				<xsl:for-each select="row">
					<p name="{@name}" type="{@type}">
						<xsl:choose>
							<xsl:when test="@type = 'String[]' or @type = 'Int32[]' or @type = 'Decimal[]'">
								<xsl:copy-of select="node()"/>
							</xsl:when>
							<xsl:otherwise>
								<xsl:value-of select="text()"/>
							</xsl:otherwise>
						</xsl:choose>
					</p>
				</xsl:for-each>
				
			</row>
				
		</xsl:for-each>
			
		</root>
	
    </xsl:template>
	
</xsl:stylesheet>

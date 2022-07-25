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

		<root>

		<xsl:for-each select="root/Constants">
			<sql tab="tab_constants">
				<xsl:for-each select="Constant">
					<xsl:for-each select="row">
						<xsl:apply-templates select="node()" />
					</xsl:for-each>
				</xsl:for-each>
			</sql>
		</xsl:for-each>
		
		<xsl:for-each select="root/Constants/Constant">
		    <xsl:for-each select="TablePart">			    
			   <xsl:apply-templates select="row" />
		    </xsl:for-each>
		</xsl:for-each>

		<xsl:for-each select="root/Directories/Directory">
			<xsl:apply-templates select="row" />

		    <xsl:for-each select="TablePart">			    
			   <xsl:apply-templates select="row" />
		    </xsl:for-each>
		</xsl:for-each>
		
		<xsl:for-each select="root/Documents/Document">
			<xsl:apply-templates select="row" />

		    <xsl:for-each select="TablePart">			    
			   <xsl:apply-templates select="row" />
		    </xsl:for-each>
		</xsl:for-each>
			
		<xsl:for-each select="root/RegistersInformation/Register">
			<xsl:apply-templates select="row" />
		</xsl:for-each>
		
		<xsl:for-each select="root/RegistersAccumulation/Register">
			<xsl:apply-templates select="row" />
		</xsl:for-each>
		
		</root>
		
    </xsl:template>

	<xsl:template match="row">
		<sql tab="{../@tab}">
			<xsl:apply-templates select="node()" />
		</sql>
	</xsl:template>
	
	<xsl:template match="node()">		
		<xsl:if test="self::*">
			<name><xsl:value-of select="name(.)"/></name>
		    <value><xsl:value-of select="."/></value>
		</xsl:if>
	</xsl:template>
	
</xsl:stylesheet>

﻿/*
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

using System;
using System.Collections.Generic;
using NpgsqlTypes;

namespace AccountingSoftware
{
	/// <summary>
	/// Композитний тип даних
	/// </summary>
	public class UuidAndText
	{
		public UuidAndText() { }

		public UuidAndText(Guid uuid)
		{
			Uuid = uuid;
		}

		public UuidAndText(Guid uuid, string text)
		{
			Uuid = uuid;
			Text = text;
		}

		/// <summary>
		/// Вказівник
		/// </summary>
		[PgName("uuid")]
		public Guid Uuid { get; set; }
				
		/// <summary>
		/// Додаткова інформація
		/// </summary>
		[PgName("text")]
		public string Text { get; set; }

		/// <summary>
		/// Дані у XML форматі
		/// </summary>
		/// <returns></returns>
		public string ToXml()
        {
			return $"<uuid>{Uuid}</uuid><text>{Text}</text>";
		}

		public override string ToString()
		{
			return $"('{Uuid}', '{Text}')";
		}
	}
}
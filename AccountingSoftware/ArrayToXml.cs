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

namespace AccountingSoftware
{
	/// <summary>
	/// Перетворює масив даних в ХМЛ стрічку виду "<e>значення 1</e><e>значення 2</e>"
	/// </summary>
	/// <typeparam name="T">Тип даних масиву</typeparam>
	public static class ArrayToXml<T>
	{
		/// <summary>
		/// Функції перетворення масиву даних в ХМЛ стрічку
		/// </summary>
		/// <param name="value">Масив</param>
		/// <returns>ХМЛ стрічку</returns>
		public static string Convert(T[] value)
		{
			string XmlData = "";
			string LeftCData = "";
			string RightCData = "";

			if (value.GetType().Name == "String[]")
			{
				LeftCData = "<![CDATA[";
				RightCData = "]]>";
			}

			foreach (T item in value)
				XmlData += "<e>" + LeftCData + item.ToString() + RightCData + "</e>";

			return XmlData;
		}
	}
}
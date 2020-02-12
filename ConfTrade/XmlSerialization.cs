using System;
using System.Collections.Generic;

using System.IO;

using System.Xml;
using System.Xml.Serialization;

namespace ConfTrade
{
	public static class XmlSerialization
	{
		public static string Serialize<T>(this T value)
		{
			var xmlSerializer = new XmlSerializer(typeof(T));
			using (var textWriter = new StringWriter())
			{
				xmlSerializer.Serialize(textWriter, value);
				return textWriter.ToString();
			}
		}

		public static T Deserialize<T>(this string xml)
		{
			if (string.IsNullOrEmpty(xml))
			{
				return default(T);
			}
			using (var sr = new StringReader(xml))
			{
				var xmlSerializer = new XmlSerializer(typeof(T));
				using (var reader = XmlReader.Create(sr))
				{
					return (T)xmlSerializer.Deserialize(reader);
				}
			}
		}
	}
}

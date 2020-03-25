
namespace AccountingSoftware
{
	public static class ArrayToXml<T>
	{
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
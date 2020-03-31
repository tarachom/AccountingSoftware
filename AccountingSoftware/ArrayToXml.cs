
namespace AccountingSoftware
{
	/// <summary>
	/// Перетворює масив даних в ХМЛ стрічку виду "<e> значення 1 </e><e> значення 2 </e>"
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
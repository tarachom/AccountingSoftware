using System;
using System.Collections.Generic;
using System.Text;

using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

using System.IO;
using System.Threading;
using System.Net;
using System.Web;

using AccountingSoftware;
using Conf = ConfTrade_v1_1;
using Довідники = ConfTrade_v1_1.Directory;

//Конфігурація Торгівля
namespace ConfTrade
{
	public class ConfTrade
	{
		public static readonly object readonly_lock = new object();
		
		AutoResetEvent autoResetEvent = new AutoResetEvent(false);

		public static List<HttpListenerContext> listHttpContext { get; set; }

		static void Main(string[] args)
		{
			Conf.Config.Kernel = new Kernel();
			Conf.Config.Kernel.Open();

			listHttpContext = new List<HttpListenerContext>();

			Thread threadWebServer = new Thread(new ThreadStart(WebServer));
			threadWebServer.Start();

			Thread thread = new Thread(new ThreadStart(Test));
			thread.Start();

			Console.ReadLine();
		}

		static void WebServer()
		{
			HttpListener listener = new HttpListener();

			listener.Prefixes.Add("http://localhost:5555/");

			listener.Start();
			Console.WriteLine("Listening...");

			while (true)
			{
				HttpListenerContext context = listener.GetContext();
				Console.WriteLine("Add list context");

				lock (readonly_lock)
				{
					listHttpContext.Add(context);
				}
			}

			//listener.Stop();
		}

		static void Test()
		{
			while (true)
			{
				HttpListenerContext context = null;

				lock (readonly_lock)
				{
					if (listHttpContext.Count > 0)
					{
						context = listHttpContext[0];
						listHttpContext.RemoveAt(0);
					}
				}

				if (context != null)
				{
					HttpListenerRequest request = context.Request;
					Console.WriteLine(" ->");

					bool isExist = false;

					foreach (string key in request.QueryString.AllKeys)
					{
						Console.WriteLine(key + " = " + request.QueryString[key].ToString());

						if (key == "cmd") {
							isExist = true;
						}
					}

					if (!isExist) {
						context.Response.Close();
						continue;
					}

					if (request.HttpMethod == "POST")
					{
						string documentContents;
						using (Stream receiveStream = request.InputStream)
						{
							using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
							{
								documentContents = readStream.ReadToEnd();
							}
						}
						Console.WriteLine($"Recived request for {request.Url}");
						Console.WriteLine(documentContents);

						Dictionary<string, string> postParams = new Dictionary<string, string>();
						string[] rawParams = documentContents.Split('&');
						foreach (string param in rawParams)
						{
							string[] kvPair = param.Split('=');
							string key = kvPair[0];
							string value = HttpUtility.UrlDecode(kvPair[1]);
							postParams.Add(key, value);
							Console.WriteLine(key + " = " + value);
						}
					}

					HttpListenerResponse response = context.Response;
					response.ContentType = "text/html";

					Stream output = response.OutputStream;

					string res = Run2();

					StringReader sr = new StringReader(res);
					XmlReader xr = XmlReader.Create(sr);

					//Console.WriteLine(res);

					XslCompiledTransform xslCompiledTransform = new XslCompiledTransform();
					xslCompiledTransform.Load(@"../../XSLTFile1.xslt");

					XsltArgumentList xsltArgumentList = new XsltArgumentList();

					try
					{
						xslCompiledTransform.Transform(xr, xsltArgumentList, output);
					}
					catch (Exception e)
					{
						Console.WriteLine(e.Message);
					}
					
					try
					{
						output.Close();
					}
					catch (Exception e)
					{
						Console.WriteLine(e.Message);
					}
				}

				//Console.WriteLine(DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"));
				Thread.Sleep(10);
			}
		}

		static string Run()
		{
			Довідники.Записи_Вибірка_View записи_View = new Довідники.Записи_Вибірка_View();
			return записи_View.Read();
		}

		static string Run2()
		{
			Довідники.Товари_Візуалізація3_View товари_Візуалізація3_View = new Довідники.Товари_Візуалізація3_View();
			return товари_Візуалізація3_View.Read();
		}

	}
}


//Довідники.Записи_Objest записи_Objest = new Довідники.Записи_Objest();
//записи_Objest.New();
//записи_Objest.Дата = DateTime.Now;
//записи_Objest.Час = DateTime.Now.TimeOfDay;
//записи_Objest.Запис = "sfkgkfgj fgsdjfgksdj dfgjsdkf";
//записи_Objest.Save();

//Довідники.Товари_Візуалізація3_View товари_Візуалізація3_View = new Довідники.Товари_Візуалізація3_View();
//товари_Візуалізація3_View.QuerySelect.CreateTempTable = true;
//Console.WriteLine( товари_Візуалізація3_View.Read());

//Довідники.Записи_Вибірка_View записи_View = new Довідники.Записи_Вибірка_View();
//Console.WriteLine(записи_View.Read());

//Console.ReadLine();

/*
using (Conf.Товари_Select s = new Conf.Товари_Select())
{
	s.Select();

	while (s.MoveNext())
	{
		Conf.Товари_Objest o = s.Current.GetDirectoryObject();
		o.Назва = "Товар " + new Random().Next(1000).ToString();
		o.Код = "Код " + new Random().Next(1000).ToString();
		o.Save();
	}
}

Conf.ОдиниціВиміру_Objest одиниціВиміру_Objest = new Conf.ОдиниціВиміру_Objest();
одиниціВиміру_Objest.New();
одиниціВиміру_Objest.Назва = "Назва 2";
одиниціВиміру_Objest.Код = "12";
одиниціВиміру_Objest.Save();
*/

//Conf.Config.Kernel.DataBase.BeginTransaction();

//System.IO.StringWriter stringWriter = new System.IO.StringWriter();
//stringWriter.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
//stringWriter.WriteLine("<root>");


//Довідники.Товари_ВибіркаТовари_View v = new Довідники.Товари_ВибіркаТовари_View();
//v.QuerySelect.Where.Add(new Where(v.Alias["Одиниця"], Comparison.NOTNULL, "", true));
////v.QuerySelect.Limit = 1;
//v.QuerySelect.CreateTempTable = true;
//stringWriter.Write(v.Read());

/**/

//Conf.Товари_Select sel = new Conf.Товари_Select();
//sel.QuerySelect.Table = v.QuerySelect.TempTable;
//if (sel.SelectSingle())
//{
//	Conf.Товари_Objest товари_Objest = sel.Current.GetDirectoryObject();

//	товари_Objest.Ціни_TablePart.Records.Add(new Conf.Товари_Ціни_TablePartRecord("Name", 0.35m, 0, null, DateTime.Now));
//	товари_Objest.Ціни_TablePart.Save(false);
//}


//Довідники.ОдиниціВиміру_Вибірка_View od = new Довідники.ОдиниціВиміру_Вибірка_View();
//od.QuerySelect.CreateTempTable = true;
//od.QuerySelect.Where.Add(new Where("uid", Comparison.IN, "SELECT DISTINCT " + v.Alias["Одиниця"] + " FROM " + v.QuerySelect.TempTable, true));
//stringWriter.Write(od.Read());

//Довідники.Товари_ВибіркаЦіни_View ceny = new Довідники.Товари_ВибіркаЦіни_View();
//ceny.QuerySelect.Where.Add(new Where("owner", Comparison.EQ, "(SELECT uid FROM " + v.QuerySelect.TempTable + ")", true));
//stringWriter.Write(ceny.Read());
/**/

//stringWriter.WriteLine("</root>");

//System.IO.File.WriteAllText(@"D:\xml.txt", stringWriter.ToString());

//XslCompiledTransform xslCompiledTransform = new XslCompiledTransform();
//xslCompiledTransform.Load(@"D:\VS\Project\AccountingSoftware\ConfTrade\XSLTFile1.xslt");

//xslCompiledTransform.Transform(@"D:\xml.txt", @"D:\html.txt");

//Console.ReadLine();
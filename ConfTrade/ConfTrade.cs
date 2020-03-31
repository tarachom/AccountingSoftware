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
Сайт:     find.org.ua
*/

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
using Константи = ConfTrade_v1_1.Константи;
using Довідники = ConfTrade_v1_1.Довідники;

//Конфігурація Торгівля
namespace ConfTrade
{
	public partial class ConfTrade
	{
		public static HttpServerConfig httpServerConfig { get; set; }

		public static readonly object readonly_lock = new object();
		public static AutoResetEvent autoResetEvent = new AutoResetEvent(false);
		public static Stack<HttpListenerContext> stackHttpContext = new Stack<HttpListenerContext>();

		public static Encoding Win1251 = Encoding.GetEncoding("windows-1251");

		static string ConvertUrl(string url)
		{
			return Encoding.UTF8.GetString(Win1251.GetBytes(url));
		}

		static void Main(string[] args)
		{
			stackHttpContext = new Stack<HttpListenerContext>();

			httpServerConfig = new HttpServerConfig();
			httpServerConfig.ReadXml(@"D:\VS\Project\AccountingSoftware\ConfTrade\WebServer\HttpServerConfig.xml");

			Conf.Config.Kernel = new Kernel();
			Conf.Config.Kernel.Open();

			Константи.РегламентніЗавдання.ФормуванняЗвітів_ІсторіяЗапускуВебСервера_TablePart ІсторіяЗапускуВебСервера =
				new Константи.РегламентніЗавдання.ФормуванняЗвітів_ІсторіяЗапускуВебСервера_TablePart();

			ІсторіяЗапускуВебСервера.Records.Add(
				new Константи.РегламентніЗавдання.ФормуванняЗвітів_ІсторіяЗапускуВебСервера_TablePart.Record(
					DateTime.Now));

			ІсторіяЗапускуВебСервера.Save(false);

			Довідники.ІсторіяЗапускуВебСервера_Objest ІсторіяЗапускуВебСервераДов = new Довідники.ІсторіяЗапускуВебСервера_Objest();
			ІсторіяЗапускуВебСервераДов.New();
			ІсторіяЗапускуВебСервераДов.ДатаЗапуску = DateTime.Now;
			ІсторіяЗапускуВебСервераДов.Save();

			//Константи.РегламентніЗавдання.ФормуванняЗвітів_ЗвітиКористувачів_TablePart ЗвітиКористувачів =
			//	new Константи.РегламентніЗавдання.ФормуванняЗвітів_ЗвітиКористувачів_TablePart();

			//ЗвітиКористувачів.Records.Add(
			//	new Константи.РегламентніЗавдання.ФормуванняЗвітів_ЗвітиКористувачів_TablePart.Record(
			//		DateTime.Now, "Report", "", null, false));

			//ЗвітиКористувачів.Save(false);

			//ЗвітиКористувачів.Read();
			//foreach (Константи.РегламентніЗавдання.ФормуванняЗвітів_ЗвітиКористувачів_TablePart.Record record in ЗвітиКористувачів.Records)
			//{
			//	record.Виконано = true;
			//	Console.WriteLine(record.UID + "|" + record.Дата + "|" + record.Звіт);
			//}
			//ЗвітиКористувачів.Save(true);

			//Conf.Константи.Основний.a1_Історія_TablePart a1_Історія = new Константи.Основний.a1_Історія_TablePart();
			//a1_Історія.Records.Add(new Константи.Основний.a1_Історія_TablePart.Історія_Record(DateTime.Now, "text"));
			//a1_Історія.Save(false);

			//Console.ReadLine();

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
					stackHttpContext.Push(context);
				}

				autoResetEvent.Set();
			}

			//listener.Stop();
		}

		static void Test()
		{
			while (true)
			{
				HttpListenerContext context;

				lock (readonly_lock)
				{
					context = (stackHttpContext.Count > 0) ? stackHttpContext.Pop() : null;
				}

				if (context == null)
				{
					autoResetEvent.WaitOne();
				}
				else
				{
					HttpListenerRequest request = context.Request;
					HttpListenerResponse response = context.Response;

					Console.WriteLine(context.Request.Url.LocalPath);
					Console.WriteLine(context.Request.Url.Query);

					//foreach (string key in context.Request.Headers.AllKeys)
					//{
					//	Console.WriteLine(" - " + key + " = " + context.Request.Headers[key]);
					//}

					response.ContentType = "text/html";
					response.ContentEncoding = Encoding.UTF8;

					if (context.Request.Url.LocalPath != "/" && context.Request.Url.LocalPath != "/5555/")
					{
						Console.WriteLine("Redirect /" + context.Request.Url.Query);

						context.Response.Redirect("/" + context.Request.Url.Query);
						context.Response.Close();
						continue;
					}

					string confObjectName = "";
					string cmdName = "";

					foreach (string key in request.QueryString.AllKeys)
					{
						switch (key)
						{
							case "confobj":
								{
									confObjectName = (request.ContentEncoding.WebName == Win1251.WebName) ?
										ConvertUrl(request.QueryString["confobj"]) : request.QueryString["confobj"];
									break;
								}
							case "cmd":
								{
									cmdName = (request.ContentEncoding.WebName == Win1251.WebName) ?
										ConvertUrl(request.QueryString["cmd"]) : request.QueryString["cmd"];
									break;
								}
							default:
								break;
						}
					}

					Console.WriteLine(confObjectName);
					Console.WriteLine(cmdName);

					if (String.IsNullOrEmpty(confObjectName) ||
						(confObjectName != "default" && !httpServerConfig.ConfObjects.ContainsKey(confObjectName)))
					{
						Console.WriteLine("Redirect " + context.Request.Url.LocalPath + "?confobj=default");

						context.Response.Redirect(context.Request.Url.LocalPath + "?confobj=default");
						context.Response.Close();
						continue;
					}

					HttpServerConfig.ConfObject ConfObject = httpServerConfig.ConfObjects[confObjectName];

					if (String.IsNullOrEmpty(cmdName) ||
						(cmdName != "default" && !ConfObject.Commands.ContainsKey(cmdName)))
					{
						Console.WriteLine(context.Request.Url.LocalPath + "?confobj=" + Uri.EscapeUriString(confObjectName) + "&cmd=default");

						context.Response.Redirect(context.Request.Url.LocalPath + "?confobj=" + Uri.EscapeUriString(confObjectName) + "&cmd=default");
						context.Response.Close();
						continue;
					}

					CommandParamsValue commandParamsValue = null;

					if (cmdName != "default")
						commandParamsValue = ConfObject.Commands[cmdName].GetCommandParamsValue();
					else
					{
						if (ConfObject.FirstCommand != null)
							commandParamsValue = ConfObject.FirstCommand.GetCommandParamsValue();
					}

					if (commandParamsValue != null)
					{
						Console.WriteLine(commandParamsValue.Name);

						if (commandParamsValue.Get_Params.Count > 0)
						{
							foreach (string key in request.QueryString.AllKeys)
							{
								if (commandParamsValue.Get_Params.ContainsKey(key))
								{
									commandParamsValue.Get_Params[key] = (request.ContentEncoding.WebName == Win1251.WebName) ?
										ConvertUrl(request.QueryString[key]) : request.QueryString[key];

									Console.WriteLine(key + " = " + commandParamsValue.Get_Params[key]);
								}
							}
						}

						if (request.HttpMethod == "POST")
						{
							string documentContents;
							using (Stream receiveStream = request.InputStream)
							{
								using (StreamReader readStream = new StreamReader(receiveStream))
								{
									documentContents = readStream.ReadToEnd();
								}
							}
							Console.WriteLine($"Recived request for {request.Url}");
							Console.WriteLine(documentContents);
							Console.WriteLine("ContentEncoding: " + request.ContentEncoding.WebName);

							string[] rawParams = documentContents.Split('&');
							foreach (string param in rawParams)
							{
								string paramModifice = param.Replace("+", " ");
								string[] kvPair = paramModifice.Split('=');
								string key = Uri.UnescapeDataString((request.ContentEncoding.WebName == Win1251.WebName) ? ConvertUrl(kvPair[0]) : kvPair[0]);
								string value = Uri.UnescapeDataString((request.ContentEncoding.WebName == Win1251.WebName) ? ConvertUrl(kvPair[1]) : kvPair[1]);
								commandParamsValue.Post_Params.Add(key, value);
								Console.WriteLine(key + " = " + value);
							}
						}
					}

					Function function = new Function();
					function.GetType().GetMethod(confObjectName).Invoke(function, new object[] { response.OutputStream, commandParamsValue });

					context.Response.StatusCode = 200;
					context.Response.StatusDescription = "ok";
					context.Response.KeepAlive = false;

					context.Response.Close();
				}
			}
		}

	}
}
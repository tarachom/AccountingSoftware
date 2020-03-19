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

//Конфігурація Торгівля
namespace ConfTrade
{
	public partial class ConfTrade
	{
		public static readonly object readonly_lock = new object();

		AutoResetEvent autoResetEvent = new AutoResetEvent(false);

		public static List<HttpListenerContext> listHttpContext { get; set; }

		static void Main(string[] args)
		{
			Conf.Config.Kernel = new Kernel();
			Conf.Config.Kernel.Open();
			Conf.Config.InitAllConstants();

			Console.ReadLine();

			listHttpContext = new List<HttpListenerContext>();

			Thread threadWebServer = new Thread(new ThreadStart(WebServer));
			threadWebServer.Start();

			Thread thread = new Thread(new ThreadStart(Test));
			thread.Start();

			Run4();

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

					Console.WriteLine(context.Request.Headers.Count);

					Console.WriteLine(context.Request.ProtocolVersion);
					Console.WriteLine(context.Request.Url.LocalPath);

					//foreach (string key in context.Request.Headers.AllKeys)
					//{
					//	Console.WriteLine(key + " = " + context.Request.Headers[key]);
					//}

					bool isExist = false;

					foreach (string key in request.QueryString.AllKeys)
					{
						Console.WriteLine(key + " = " + request.QueryString[key].ToString());

						if (key == "cmd")
						{
							isExist = true;
						}
					}

					if (!isExist)
					{
						context.Response.Close();
						continue;
					}

					if (!(context.Request.Url.LocalPath == "/"))
					{
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

					string res = ""; // Run();

					//Console.WriteLine(res);

					StringReader sr = new StringReader(res);
					XmlReader xr = XmlReader.Create(sr);

					XslCompiledTransform xslCompiledTransform = new XslCompiledTransform();
					xslCompiledTransform.Load(@"../../test_Список.xslt");

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
	}
}
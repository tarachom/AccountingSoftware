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
		public static HttpServerConfig httpServerConfig { get; set; }

		public static readonly object readonly_lock = new object();
		public static AutoResetEvent autoResetEvent = new AutoResetEvent(false);
		public static Stack<HttpListenerContext> stackHttpContext = new Stack<HttpListenerContext>();

		static void Main(string[] args)
		{
			Conf.Config.Kernel = new Kernel();
			Conf.Config.Kernel.Open();
			Conf.Config.InitAllConstants();

			httpServerConfig = new HttpServerConfig();
			httpServerConfig.ReadXml(@"D:\VS\Project\AccountingSoftware\ConfTrade\HttpServerConfig.xml");

			stackHttpContext = new Stack<HttpListenerContext>();

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

					Console.WriteLine(HttpUtility.UrlDecode(context.Request.Url.Query));

					response.ContentType = "text/html";
					response.ContentEncoding = Encoding.UTF8;

					if (context.Request.Url.LocalPath != "/")
					{
						context.Response.Redirect("/" + context.Request.Url.Query);
						context.Response.Close();
						continue;
					}

					string[] allKeys = request.QueryString.AllKeys;

					string confObjectName = "";
					string cmdName = "";

					foreach (string key in allKeys)
					{
						switch (key)
						{
							case "confobj":
								{
									confObjectName = HttpUtility.UrlDecode(request.QueryString["confobj"], Encoding.UTF8);
									break;
								}
							case "cmd":
								{
									cmdName = HttpUtility.UrlDecode(request.QueryString["cmd"]);
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
						context.Response.Redirect("/?confobj=default");
						context.Response.Close();
						continue;
					}

					HttpServerConfig.ConfObject ConfObject = httpServerConfig.ConfObjects[confObjectName];

					if (String.IsNullOrEmpty(cmdName) ||
						(cmdName != "default" && !ConfObject.Commands.ContainsKey(cmdName)))
					{
						context.Response.Redirect("/?confobj=" + confObjectName + "&cmd=default");
						context.Response.Close();
						continue;
					}

					HttpServerConfig.ConfObject.Command command = null;

					if (cmdName != "default")
						command = ConfObject.Commands[cmdName];
					else
					{
						foreach (string firstKey in ConfObject.Commands.Keys)
						{
							command = ConfObject.Commands[firstKey];
							break;
						}
					}

					Console.WriteLine(command.Name);

					if (command.Params.Count > 0)
					{
						foreach (string key in allKeys)
						{
							if (command.Params.ContainsKey(key))
							{
								command.Params[key] = request.QueryString[key];
								Console.WriteLine(key + " = " + command.Params[key]);
							}
						}
					}

					Function function = new Function();
					function.GetType().GetMethod(confObjectName).Invoke(function, new object[] { response.OutputStream, command });

					context.Response.Close();
				}
			}
		}

	}
}
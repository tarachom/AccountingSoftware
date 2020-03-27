using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

using AccountingSoftware;
using Conf = ConfTrade_v1_1;
using Константи = ConfTrade_v1_1.Константи;
using Довідники = ConfTrade_v1_1.Довідники;
using Документи = ConfTrade_v1_1.Документи;
using Перелічення = ConfTrade_v1_1.Перелічення;
using РегістриВідомостей = ConfTrade_v1_1.РегістриВідомостей;
using РегістриНакопичення = ConfTrade_v1_1.РегістриНакопичення;

namespace ConfTrade
{
	public partial class Function
	{
		public void Довідник_Контрагенти(Stream output, CommandParamsValue commandParamsValue)
		{
			XslCompiledTransform xslCompiledTransform = new XslCompiledTransform();
			xslCompiledTransform.Load(@"../../WebServer/Xslt/Довідник_Контрагенти/" + commandParamsValue.Name + ".xslt");

			XsltArgumentList xsltArgumentList = new XsltArgumentList();
			xsltArgumentList.AddParam("confobj", "", "Довідник_Контрагенти");
			xsltArgumentList.AddParam("cmd", "", commandParamsValue.Name);

			foreach (string key in commandParamsValue.Get_Params.Keys)
				xsltArgumentList.AddParam(key, "", commandParamsValue.Get_Params[key]);

			string XmlData = "<root>";

			switch (commandParamsValue.Name)
			{
				case "List":
					{
						string ParentUid = commandParamsValue.Get_Params["Parent"];
						if (ParentUid == "") ParentUid = Guid.Empty.ToString();

						Довідники.Контрагенти_Групи_Pointer контрагенти_Групи_Pointer = new
							Довідники.Контрагенти_Групи_Pointer(new UnigueID(ParentUid));

						if (!контрагенти_Групи_Pointer.IsEmpty())
						{
							XmlData += "<parents>\n";

							Довідники.Контрагенти_Групи_Pointer Група = контрагенти_Групи_Pointer;
							int level = 0;

							while (!Група.IsEmpty())
							{
								Довідники.Контрагенти_Групи_Objest Група_Objest = Група.GetDirectoryObject();
								if (Група_Objest != null)
								{
									XmlData +=
										"<parent>\n" +
										"	<level>" + level.ToString() + "</level>\n" +
										"	<puid>" + Група_Objest.Група.UnigueID.ToString() + "</puid>\n" +
										"	<Код>" + Група_Objest.Код + "</Код>\n" +
										"	<Назва>" + Група_Objest.Назва + "</Назва>\n" +
										"</parent>\n";

									Група = Група_Objest.Група;
									level++;
								}

								if (level > 10)
								{
									break;
								}
							}

							XmlData += "</parents>\n";

							Console.WriteLine(XmlData);
						}

						Довідники.Контрагенти_Групи_Список_View m_parent = new Довідники.Контрагенти_Групи_Список_View();
						m_parent.QuerySelect.Where.Add(new Where(m_parent.Alias["Група"], Comparison.EQ, контрагенти_Групи_Pointer.GetPointer(), false));
						XmlData += m_parent.Read();

						Довідники.Контрагенти_Список_View m_1 = new Довідники.Контрагенти_Список_View();
						m_1.QuerySelect.Where.Add(new Where(m_1.Alias["Група"], Comparison.EQ, контрагенти_Групи_Pointer.GetPointer(), false));
						m_1.QuerySelect.Order.Add(m_1.Alias["Код"], SelectOrder.ASC);
						XmlData += m_1.Read();

						break;
					}

				case "Add":
					{
						Довідники.Контрагенти_Objest контрагенти_Objest = new Довідники.Контрагенти_Objest();
						XmlData += контрагенти_Objest.Serialize();
						break;
					}

				case "AddGroup":
					{
						Довідники.Контрагенти_Групи_Objest контрагенти_Групи_Objest = new Довідники.Контрагенти_Групи_Objest();
						XmlData += контрагенти_Групи_Objest.Serialize();
						break;
					}

				case "Edit":
					{
						string Uid = commandParamsValue.Get_Params["Uid"];

						if (String.IsNullOrEmpty(Uid))
						{
							XmlData += "<info>Error Uid</info>";
							break;
						}

						Довідники.Контрагенти_Objest контрагенти_Objest = new Довідники.Контрагенти_Objest();
						if (контрагенти_Objest.Read(new UnigueID(Uid)))
							XmlData += контрагенти_Objest.Serialize();
						else
							XmlData += "<info>Error read Uid</info>";

						break;
					}

				case "EditGroup":
					{
						string Uid = commandParamsValue.Get_Params["Uid"];

						if (String.IsNullOrEmpty(Uid))
						{
							XmlData += "<info>Error Uid</info>";
							break;
						}

						Довідники.Контрагенти_Групи_Objest контрагенти_Групи_Objest = new Довідники.Контрагенти_Групи_Objest();
						if (контрагенти_Групи_Objest.Read(new UnigueID(Uid)))
							XmlData += контрагенти_Групи_Objest.Serialize();
						else
							XmlData += "<info>Error read Uid</info>";

						break;
					}

				case "Save":
					{
						string Uid = commandParamsValue.Get_Params["Uid"];

						Довідники.Контрагенти_Objest контрагенти_Objest = new Довідники.Контрагенти_Objest();

						if (String.IsNullOrEmpty(Uid))
						{
							контрагенти_Objest.New();
						}
						else
						{
							if (!контрагенти_Objest.Read(new UnigueID(Uid)))
							{
								XmlData += "<info>Error read Uid</info>";
								break;
							}
						}

						контрагенти_Objest.Код = commandParamsValue.Post_Params["Code"];
						контрагенти_Objest.Назва = commandParamsValue.Post_Params["Name"];

						контрагенти_Objest.Постачальник = (commandParamsValue.Post_Params.ContainsKey("Postachalnyk") &&
							commandParamsValue.Post_Params["Postachalnyk"] == "on") ? true : false;

						контрагенти_Objest.Покупець = (commandParamsValue.Post_Params.ContainsKey("Pokupec") &&
							commandParamsValue.Post_Params["Pokupec"] == "on") ? true : false;

						контрагенти_Objest.Група = new Довідники.Контрагенти_Групи_Select().FindByField("Код", "2");

						контрагенти_Objest.Save();

						XmlData += "<info>" + "Записано. Ід " + контрагенти_Objest.UnigueID.ToString() + "</info>";
						break;
					}

				case "SaveGroup":
					{
						string Uid = commandParamsValue.Get_Params["Uid"];

						Довідники.Контрагенти_Групи_Objest контрагенти_Групи_Objest = new Довідники.Контрагенти_Групи_Objest();

						if (String.IsNullOrEmpty(Uid))
						{
							контрагенти_Групи_Objest.New();
						}
						else
						{
							if (!контрагенти_Групи_Objest.Read(new UnigueID(Uid)))
							{
								XmlData += "<info>Error read Uid</info>";
								break;
							}
						}

						контрагенти_Групи_Objest.Код = commandParamsValue.Post_Params["Code"];
						контрагенти_Групи_Objest.Назва = commandParamsValue.Post_Params["Name"];
						контрагенти_Групи_Objest.Група = new Довідники.Контрагенти_Групи_Select().FindByField("Код", "2");
						контрагенти_Групи_Objest.Save();

						//Довідники.Контрагенти_Групи_Pointer Група = контрагенти_Групи_Objest.Група;
						//int level = 0;

						//while (!Група.IsEmpty())
						//{
						//	Довідники.Контрагенти_Групи_Objest Група_Objest = Група.GetDirectoryObject();
						//	if (Група_Objest != null)
						//	{
						//		контрагенти_Групи_Objest.Ієрархія_TablePart.Records.Add(
						//			new Довідники.Контрагенти_Групи_Ієрархія_TablePart.Record(level, Група));

						//		Група = Група_Objest.Група;
						//		level++;
						//	}

						//	if (level > 10)
						//		throw new Exception("Level > 10!");
						//}

						//контрагенти_Групи_Objest.Ієрархія_TablePart.Save(true);

						XmlData += "<info>" + "Записано. Ід " + контрагенти_Групи_Objest.UnigueID.ToString() + "</info>";
						break;
					}

				case "Delete":
					{
						string Uid = commandParamsValue.Get_Params["Uid"];

						if (String.IsNullOrEmpty(Uid))
						{
							XmlData += "<info>Error Uid</info>";
							break;
						}

						Довідники.Контрагенти_Objest контрагенти_Objest = new Довідники.Контрагенти_Objest();
						if (контрагенти_Objest.Read(new UnigueID(Uid)))
						{
							контрагенти_Objest.Delete();
							XmlData += "<info>Ok delete</info>";
						}
						else
							XmlData += "<info>Error read Uid</info>";

						break;
					}
			}

			XmlData += "</root>";

			//Console.WriteLine(XmlData);

			StringReader stringReader = new StringReader(XmlData);
			XmlReader xmlReader = XmlReader.Create(stringReader);

			try
			{
				xslCompiledTransform.Transform(xmlReader, xsltArgumentList, output);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}
	}
}

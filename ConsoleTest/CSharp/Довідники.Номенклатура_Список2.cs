
/*
Copyright (C) 2019-2020 Tarakhomin Yuri Ivanovich
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
  
    
using System.Text;
using System.Collections.Generic;

using AccountingSoftware;
using Conf = ConfTrade_v1_1;
using Довідники = ConfTrade_v1_1.Довідники;    

namespace ConfTrade
{
    public partial class ConfTrade
    {
        public static string Run()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<root>");
            
            Довідники.Номенклатура_Список2_View m_1 = new Довідники.Номенклатура_Список2_View();
            
            m_1.QuerySelect.CreateTempTable = true;
            Dictionary<string, string> Alias = m_1.Alias;
              
            sb.Append(m_1.Read());
            
            
              
            string TempTable = m_1.QuerySelect.TempTable;
              
            Довідники.КлассификаторЕдИзм_Список_View m_3 = new Довідники.КлассификаторЕдИзм_Список_View();
            m_3.QuerySelect.Where.Add(new Where("uid", Comparison.IN, "SELECT DISTINCT " + Alias["БазоваяЕдиница"] + " FROM " + TempTable, true));
            sb.Append(m_3.Read());
            
                Довідники.Валюти_Список_View m_5 = new Довідники.Валюти_Список_View();
            m_5.QuerySelect.Where.Add(new Where("uid", Comparison.IN, "SELECT DISTINCT " + Alias["ВалютаУчета"] + " FROM " + TempTable, true));
            sb.Append(m_5.Read());
            
                Довідники.Групи_Номенклатура_Список_View m_8 = new Довідники.Групи_Номенклатура_Список_View();
            m_8.QuerySelect.Where.Add(new Where("uid", Comparison.IN, "SELECT DISTINCT " + Alias["Група"] + " FROM " + TempTable, true));
            sb.Append(m_8.Read());
            
                
            sb.Append(@"<Enums>
<Enum>
  <Name>ВидиТоварів</Name>
  <Desc>Види товарів</Desc>
  <SerialNumber>4</SerialNumber>
  <Fields>
    <Field>
      <Name>Товар</Name>
      <Value>1</Value>
      <Desc>Просто товар</Desc>
    </Field>
    <Field>
      <Name>Послуга</Name>
      <Value>2</Value>
      <Desc>Послуга</Desc>
    </Field>
    <Field>
      <Name>Бартер</Name>
      <Value>3</Value>
      <Desc>Бартер</Desc>
    </Field>
  </Fields>
</Enum>
</Enums>
");
            
            sb.Append("</root>");
            return sb.ToString();
        }
    }
}
  
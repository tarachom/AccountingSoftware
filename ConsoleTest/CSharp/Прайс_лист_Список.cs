
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
using Довідники = ConfTrade_v1_1.Directory;    

namespace ConfTrade
{
    public partial class ConfTrade
    {
        public static string Run()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<root>");
            
            Довідники.Прайс_лист_Список_View m_Прайс_лист_Список_View = new Довідники.Прайс_лист_Список_View();
            
            m_Прайс_лист_Список_View.QuerySelect.CreateTempTable = true;
              Dictionary<string, string> Alias = m_Прайс_лист_Список_View.Alias;
              
            sb.Append(m_Прайс_лист_Список_View.Read());
            
            
            Довідники.Довідники.Номенклатура_Список_View m_Довідники.Номенклатура_Список_View = new Довідники.Довідники.Номенклатура_Список_View();
            m_Довідники.Номенклатура_Список_View.QuerySelect.Where.Add(new Where("uid", Comparison.IN, 
                "SELECT DISTINCT " + Alias["Товар"] + " FROM " + m_Прайс_лист_Список_View.QuerySelect.TempTable, true)); /* col_a1 */
            sb.Append(m_Довідники.Номенклатура_Список_View.Read());
                
            sb.Append("</root>");
            return sb.ToString();
        }
    }
}
  
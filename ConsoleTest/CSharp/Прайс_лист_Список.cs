
    
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
            
            
            Довідники.Номенклатура_Список_View m_Номенклатура_Список_View = new Довідники.Номенклатура_Список_View();
            m_Номенклатура_Список_View.QuerySelect.Where.Add(new Where("uid", Comparison.IN, 
                "SELECT DISTINCT " + Alias["Товар"] + " FROM " + m_Прайс_лист_Список_View.QuerySelect.TempTable, true)); /* col_a1 */
            sb.Append(m_Номенклатура_Список_View.Read());
                
            sb.Append("</root>");
            return sb.ToString();
        }
    }
}
  
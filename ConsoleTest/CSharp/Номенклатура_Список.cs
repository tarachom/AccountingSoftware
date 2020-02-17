
    
using System.Text;

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
            
            Довідники.Номенклатура_Список_View m_Номенклатура_Список_View = new Довідники.Номенклатура_Список_View();
            m_Номенклатура_Список_View.QuerySelect.CreateTempTable = true;
            sb.Append(m_Номенклатура_Список_View.Read());
            
            
            Довідники.Валюти_Список_View m_Валюти_Список_View = new Довідники.Валюти_Список_View();
            m_Валюти_Список_View.QuerySelect.Where.Add(
                new Where("uid", Comparison.IN, /* col_d3 */ 
                "SELECT DISTINCT " + m_Номенклатура_Список_View.Alias["ВалютаУчета"] + 
                " FROM " + m_Номенклатура_Список_View.QuerySelect.TempTable, true));
                
            sb.Append(m_Валюти_Список_View.Read());
                
    
            sb.Append("</root>");
            return sb.ToString();
        }
    }
}
  
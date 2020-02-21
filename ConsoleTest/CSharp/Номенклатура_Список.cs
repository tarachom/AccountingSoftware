
    
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
              string TempTable = m_Номенклатура_Список_View.QuerySelect.TempTable;
              string[] Alias = m_Номенклатура_Список_View.Alias;
              
            sb.Append(m_Номенклатура_Список_View.Read());
            
            
            Довідники.Валюти_Список_View m_Валюти_Список_View = new Довідники.Валюти_Список_View();
            m_Валюти_Список_View.QuerySelect.Where.Add(new Where("uid", Comparison.IN, 
                "SELECT DISTINCT " + Alias["ВалютаУчета"] + " FROM " + TempTable, true)); /* col_d3 */
            sb.Append(m_Валюти_Список_View.Read());
                
            
            sb.Append(@"<Enums>
</Enums>
");    
            sb.Append("</root>");
            return sb.ToString();
        }
    }
}
  
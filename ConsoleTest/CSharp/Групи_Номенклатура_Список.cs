
    
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
            
            Довідники.Групи_Номенклатура_Список_View m_Групи_Номенклатура_Список_View = new Довідники.Групи_Номенклатура_Список_View();
            
            m_Групи_Номенклатура_Список_View.QuerySelect.CreateTempTable = true;
              Dictionary<string, string> Alias = m_Групи_Номенклатура_Список_View.Alias;
              
            sb.Append(m_Групи_Номенклатура_Список_View.Read());
            
            
            Довідники.Групи_Номенклатура_Список_View m_Групи_Номенклатура_Список_View = new Довідники.Групи_Номенклатура_Список_View();
            m_Групи_Номенклатура_Список_View.QuerySelect.Where.Add(new Where("uid", Comparison.IN, 
                "SELECT DISTINCT " + Alias["Родитель"] + " FROM " + m_Групи_Номенклатура_Список_View.QuerySelect.TempTable, true)); /* col_a2 */
            sb.Append(m_Групи_Номенклатура_Список_View.Read());
                
            
            sb.Append(@"<Enums>
</Enums>
");    
            sb.Append("</root>");
            return sb.ToString();
        }
    }
}
  
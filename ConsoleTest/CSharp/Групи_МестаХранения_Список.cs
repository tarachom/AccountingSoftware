
    
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
            
            Довідники.Групи_МестаХранения_Список_View m_Групи_МестаХранения_Список_View = new Довідники.Групи_МестаХранения_Список_View();
            
            m_Групи_МестаХранения_Список_View.QuerySelect.CreateTempTable = true;
              Dictionary<string, string> Alias = m_Групи_МестаХранения_Список_View.Alias;
              
            sb.Append(m_Групи_МестаХранения_Список_View.Read());
            
            
            Довідники.МестаХранения_Список_View m_МестаХранения_Список_View = new Довідники.МестаХранения_Список_View();
            m_МестаХранения_Список_View.QuerySelect.Where.Add(new Where("uid", Comparison.IN, 
                "SELECT DISTINCT " + Alias["Родитель"] + " FROM " + m_Групи_МестаХранения_Список_View.QuerySelect.TempTable, true)); /* col_a3 */
            sb.Append(m_МестаХранения_Список_View.Read());
                
            sb.Append("</root>");
            return sb.ToString();
        }
    }
}
  
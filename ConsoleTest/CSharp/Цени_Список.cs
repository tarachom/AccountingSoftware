
    
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
            
            Довідники.Цени_Список_View m_Цени_Список_View = new Довідники.Цени_Список_View();
            
            m_Цени_Список_View.QuerySelect.CreateTempTable = true;
              Dictionary<string, string> Alias = m_Цени_Список_View.Alias;
              
            sb.Append(m_Цени_Список_View.Read());
            
            
            Довідники.Валюти_Список_View m_Валюти_Список_View = new Довідники.Валюти_Список_View();
            m_Валюти_Список_View.QuerySelect.Where.Add(new Where("uid", Comparison.IN, 
                "SELECT DISTINCT " + Alias["Валюта"] + " FROM " + m_Цени_Список_View.QuerySelect.TempTable, true)); /* col_a1 */
            sb.Append(m_Валюти_Список_View.Read());
                
            Довідники.КатегорииЦен_Список_View m_КатегорииЦен_Список_View = new Довідники.КатегорииЦен_Список_View();
            m_КатегорииЦен_Список_View.QuerySelect.Where.Add(new Where("uid", Comparison.IN, 
                "SELECT DISTINCT " + Alias["КатегорияЦени"] + " FROM " + m_Цени_Список_View.QuerySelect.TempTable, true)); /* col_a3 */
            sb.Append(m_КатегорииЦен_Список_View.Read());
                
            
            sb.Append(@"<Enums>
</Enums>
");    
            sb.Append("</root>");
            return sb.ToString();
        }
    }
}
  
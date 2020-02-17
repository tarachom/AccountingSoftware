
    
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
            
            Довідники.Групи_Номенклатура_Список_View m_Групи_Номенклатура_Список_View = new Довідники.Групи_Номенклатура_Список_View();
            m_Групи_Номенклатура_Список_View.QuerySelect.CreateTempTable = true;
            sb.Append(m_Групи_Номенклатура_Список_View.Read());
            
            
            Довідники.Групи_Номенклатура_Список_View m_Групи_Номенклатура_Список_View = new Довідники.Групи_Номенклатура_Список_View();
            m_Групи_Номенклатура_Список_View.QuerySelect.Where.Add(
                new Where("uid", Comparison.IN, /* col_a2 */ 
                "SELECT DISTINCT " + m_Групи_Номенклатура_Список_View.Alias["Родитель"] + 
                " FROM " + m_Групи_Номенклатура_Список_View.QuerySelect.TempTable, true));
                
            sb.Append(m_Групи_Номенклатура_Список_View.Read());
                
    
            sb.Append("</root>");
            return sb.ToString();
        }
    }
}
  
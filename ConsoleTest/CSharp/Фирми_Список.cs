
    
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
            
            Довідники.Фирми_Список_View m_Фирми_Список_View = new Довідники.Фирми_Список_View();
            m_Фирми_Список_View.QuerySelect.CreateTempTable = true;
            sb.Append(m_Фирми_Список_View.Read());
            
            
            Довідники.Сотрудники_Список_View m_Сотрудники_Список_View = new Довідники.Сотрудники_Список_View();
            m_Сотрудники_Список_View.QuerySelect.Where.Add(
                new Where("uid", Comparison.EQ, /* col_e4 */ 
                "(SELECT DISTINCT " + m_Фирми_Список_View.Alias["Руководитель"] + 
                " FROM " + m_Фирми_Список_View.QuerySelect.TempTable + ")", true));
                
            sb.Append(m_Сотрудники_Список_View.Read());
                
    
            sb.Append("</root>");
            return sb.ToString();
        }
    }
}
  
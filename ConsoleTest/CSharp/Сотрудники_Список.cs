
    
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
            
            Довідники.Сотрудники_Список_View m_Сотрудники_Список_View = new Довідники.Сотрудники_Список_View();
            
            sb.Append(m_Сотрудники_Список_View.Read());
            
            
    
            sb.Append("</root>");
            return sb.ToString();
        }
    }
}
  
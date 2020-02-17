
using System.Text;

using AccountingSoftware;
using Conf = ConfTrade_v1_1;
using Довідники = ConfTrade_v1_1.Directory;    

namespace ConsoleTest
{
    public partial class Program
    {
        public static string Run()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<root>");
        
            Довідники.Словарь_Список_View m_Словарь_Список_View = new Довідники.Словарь_Список_View();
            m_Словарь_Список_View.QuerySelect.CreateTempTable = true;
            sb.Append(m_Словарь_Список_View.Read());

            
    
            sb.Append("</root>");
            return sb.ToString();
        }
    }
}
  

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
        
            Довідники.Групи_МестаХранения_Список_View m_Групи_МестаХранения_Список_View = new Довідники.Групи_МестаХранения_Список_View();
            m_Групи_МестаХранения_Список_View.QuerySelect.CreateTempTable = true;
            sb.Append(m_Групи_МестаХранения_Список_View.Read());

            
            Довідники.МестаХранения_Список_View m_МестаХранения_Список_View = new Довідники.МестаХранения_Список_View();
            m_МестаХранения_Список_View.QuerySelect.Where.Add(
                new Where("uid", Comparison.EQ, "(SELECT DISTINCT col_a3 FROM " + m_Групи_МестаХранения_Список_View.QuerySelect.TempTable + ")", true));
            sb.Append(m_МестаХранения_Список_View.Read());
                
    
            sb.Append("</root>");
            return sb.ToString();
        }
    }
}
  
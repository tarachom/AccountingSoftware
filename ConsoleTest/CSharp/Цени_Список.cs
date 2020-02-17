
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
        
            Довідники.Цени_Список_View m_Цени_Список_View = new Довідники.Цени_Список_View();
            m_Цени_Список_View.QuerySelect.CreateTempTable = true;
            sb.Append(m_Цени_Список_View.Read());

            
            Довідники.Валюти_Список_View m_Валюти_Список_View = new Довідники.Валюти_Список_View();
            m_Валюти_Список_View.QuerySelect.Where.Add(
                new Where("uid", Comparison.EQ, "(SELECT DISTINCT col_a1 FROM " + m_Цени_Список_View.QuerySelect.TempTable + ")", true));
            sb.Append(m_Валюти_Список_View.Read());
                
            Довідники.КатегорииЦен_Список_View m_КатегорииЦен_Список_View = new Довідники.КатегорииЦен_Список_View();
            m_КатегорииЦен_Список_View.QuerySelect.Where.Add(
                new Where("uid", Comparison.EQ, "(SELECT DISTINCT col_a3 FROM " + m_Цени_Список_View.QuerySelect.TempTable + ")", true));
            sb.Append(m_КатегорииЦен_Список_View.Read());
                
    
            sb.Append("</root>");
            return sb.ToString();
        }
    }
}
  
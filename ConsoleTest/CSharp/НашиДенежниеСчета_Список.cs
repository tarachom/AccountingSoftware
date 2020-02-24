
    
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
            
            Довідники.НашиДенежниеСчета_Список_View m_НашиДенежниеСчета_Список_View = new Довідники.НашиДенежниеСчета_Список_View();
            
            sb.Append(m_НашиДенежниеСчета_Список_View.Read());
            
            
            sb.Append("</root>");
            return sb.ToString();
        }
    }
}
  
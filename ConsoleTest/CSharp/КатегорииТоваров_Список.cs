﻿
    
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
            
            Довідники.КатегорииТоваров_Список_View m_КатегорииТоваров_Список_View = new Довідники.КатегорииТоваров_Список_View();
            
            sb.Append(m_КатегорииТоваров_Список_View.Read());
            
            
            
            sb.Append(@"<Enums>
</Enums>
");    
            sb.Append("</root>");
            return sb.ToString();
        }
    }
}
  
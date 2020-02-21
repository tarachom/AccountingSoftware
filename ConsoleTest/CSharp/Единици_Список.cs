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
            
            Довідники.Единици_Список_View m_Единици_Список_View = new Довідники.Единици_Список_View();
            
            m_Единици_Список_View.QuerySelect.CreateTempTable = true;
              string TempTable = m_Единици_Список_View.QuerySelect.TempTable;
              string[] Alias = m_Единици_Список_View.Alias;
              
            sb.Append(m_Единици_Список_View.Read());
            
            
            Довідники.КлассификаторЕдИзм_Список_View m_КлассификаторЕдИзм_Список_View = new Довідники.КлассификаторЕдИзм_Список_View();
            m_КлассификаторЕдИзм_Список_View.QuerySelect.Where.Add(new Where("uid", Comparison.IN, 
                "SELECT DISTINCT " + Alias["Единица"] + " FROM " + TempTable, true)); /* col_a9 */
            sb.Append(m_КлассификаторЕдИзм_Список_View.Read());
                
            
            sb.Append(@"<Enums>
</Enums>
");    
            sb.Append("</root>");
            return sb.ToString();
        }
    }
}
  
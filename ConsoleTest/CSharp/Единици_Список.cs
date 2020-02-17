﻿
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
        
            Довідники.Единици_Список_View m_Единици_Список_View = new Довідники.Единици_Список_View();
            m_Единици_Список_View.QuerySelect.CreateTempTable = true;
            sb.Append(m_Единици_Список_View.Read());

            
            Довідники.КлассификаторЕдИзм_Список_View m_КлассификаторЕдИзм_Список_View = new Довідники.КлассификаторЕдИзм_Список_View();
            m_КлассификаторЕдИзм_Список_View.QuerySelect.Where.Add(
                new Where("uid", Comparison.EQ, "(SELECT DISTINCT col_a9 FROM " + m_Единици_Список_View.QuerySelect.TempTable + ")", true));
            sb.Append(m_КлассификаторЕдИзм_Список_View.Read());
                
    
            sb.Append("</root>");
            return sb.ToString();
        }
    }
}
  
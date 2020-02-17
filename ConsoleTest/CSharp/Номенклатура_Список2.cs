
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
        
            Довідники.Номенклатура_Список_View m_Номенклатура_Список_View = new Довідники.Номенклатура_Список_View();
            m_Номенклатура_Список_View.QuerySelect.CreateTempTable = true;
            sb.Append(m_Номенклатура_Список_View.Read());

            
            Довідники.КлассификаторЕдИзм_Список_View m_КлассификаторЕдИзм_Список_View = new Довідники.КлассификаторЕдИзм_Список_View();
            m_КлассификаторЕдИзм_Список_View.QuerySelect.Where.Add(
                new Where("uid", Comparison.EQ, "(SELECT DISTINCT col_c9 FROM " + m_Номенклатура_Список_View.QuerySelect.TempTable + ")", true));
            sb.Append(m_КлассификаторЕдИзм_Список_View.Read());
                
            Довідники.Единици_Список_View m_Единици_Список_View = new Довідники.Единици_Список_View();
            m_Единици_Список_View.QuerySelect.Where.Add(
                new Where("uid", Comparison.EQ, "(SELECT DISTINCT col_d2 FROM " + m_Номенклатура_Список_View.QuerySelect.TempTable + ")", true));
            sb.Append(m_Единици_Список_View.Read());
                
            Довідники.Валюти_Список_View m_Валюти_Список_View = new Довідники.Валюти_Список_View();
            m_Валюти_Список_View.QuerySelect.Where.Add(
                new Where("uid", Comparison.EQ, "(SELECT DISTINCT col_d3 FROM " + m_Номенклатура_Список_View.QuerySelect.TempTable + ")", true));
            sb.Append(m_Валюти_Список_View.Read());
                
            Довідники.КодиУКТВЕД_Список_View m_КодиУКТВЕД_Список_View = new Довідники.КодиУКТВЕД_Список_View();
            m_КодиУКТВЕД_Список_View.QuerySelect.Where.Add(
                new Where("uid", Comparison.EQ, "(SELECT DISTINCT col_e8 FROM " + m_Номенклатура_Список_View.QuerySelect.TempTable + ")", true));
            sb.Append(m_КодиУКТВЕД_Список_View.Read());
                
            Довідники.Групи_Номенклатура_Список_View m_Групи_Номенклатура_Список_View = new Довідники.Групи_Номенклатура_Список_View();
            m_Групи_Номенклатура_Список_View.QuerySelect.Where.Add(
                new Where("uid", Comparison.EQ, "(SELECT DISTINCT col_a3 FROM " + m_Номенклатура_Список_View.QuerySelect.TempTable + ")", true));
            sb.Append(m_Групи_Номенклатура_Список_View.Read());
                
    
            sb.Append("</root>");
            return sb.ToString();
        }
    }
}
  
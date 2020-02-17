

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

            Довідники.Номенклатура_Список2_View m_Номенклатура_Список2_View = new Довідники.Номенклатура_Список2_View();
            m_Номенклатура_Список2_View.QuerySelect.CreateTempTable = true;
            sb.Append(m_Номенклатура_Список2_View.Read());


            Довідники.КлассификаторЕдИзм_Список_View m_КлассификаторЕдИзм_Список_View = new Довідники.КлассификаторЕдИзм_Список_View();
            m_КлассификаторЕдИзм_Список_View.QuerySelect.Where.Add(
                new Where("uid", Comparison.IN, /* col_c9 */
                "SELECT DISTINCT " + m_Номенклатура_Список2_View.Alias["БазоваяЕдиница"] +
                " FROM " + m_Номенклатура_Список2_View.QuerySelect.TempTable, true));

            sb.Append(m_КлассификаторЕдИзм_Список_View.Read());

            Довідники.Валюти_Список_View m_Валюти_Список_View = new Довідники.Валюти_Список_View();
            m_Валюти_Список_View.QuerySelect.Where.Add(
                new Where("uid", Comparison.IN, /* col_d3 */
                "SELECT DISTINCT " + m_Номенклатура_Список2_View.Alias["ВалютаУчета"] +
                " FROM " + m_Номенклатура_Список2_View.QuerySelect.TempTable, true));

            sb.Append(m_Валюти_Список_View.Read());

            Довідники.Групи_Номенклатура_Список_View m_Групи_Номенклатура_Список_View = new Довідники.Групи_Номенклатура_Список_View();
            m_Групи_Номенклатура_Список_View.QuerySelect.Where.Add(
                new Where("uid", Comparison.IN, /* col_a3 */
                "SELECT DISTINCT " + m_Номенклатура_Список2_View.Alias["Група"] +
                " FROM " + m_Номенклатура_Список2_View.QuerySelect.TempTable, true));

            sb.Append(m_Групи_Номенклатура_Список_View.Read());


            sb.Append("</root>");
            return sb.ToString();
        }
    }
}

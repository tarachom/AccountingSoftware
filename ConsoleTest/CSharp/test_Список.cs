
    
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
            
            Довідники.test_Список_View m_test_Список_View = new Довідники.test_Список_View();
            
            m_test_Список_View.QuerySelect.CreateTempTable = true;
              Dictionary<string, string> Alias = m_test_Список_View.Alias;
              
            sb.Append(m_test_Список_View.Read());
            
            
            Довідники.Номенклатура_Список_View m_Номенклатура_Список_View = new Довідники.Номенклатура_Список_View();
            m_Номенклатура_Список_View.QuerySelect.Where.Add(new Where("uid", Comparison.IN, 
                "SELECT DISTINCT " + Alias["Поле4"] + " FROM " + m_test_Список_View.QuerySelect.TempTable, true)); /* col_a6 */
            sb.Append(m_Номенклатура_Список_View.Read());
                
            
            sb.Append(@"<Enums>
<Enum>
  <Name>Перелічення2</Name>
  <Desc>test
      </Desc>
  <Fields>
    <Field>
      <Name>Один</Name>
      <Value>1</Value>
    </Field>
    <Field>
      <Name>Два</Name>
      <Value>2</Value>
    </Field>
    <Field>
      <Name>Три</Name>
      <Value>3</Value>
    </Field>
  </Fields>
</Enum>
<Enum>
  <Name>Перелічення</Name>
  <Desc>test
      </Desc>
  <Fields>
    <Field>
      <Name>Один</Name>
      <Value>1</Value>
    </Field>
    <Field>
      <Name>Два</Name>
      <Value>2</Value>
    </Field>
    <Field>
      <Name>Три</Name>
      <Value>3</Value>
    </Field>
  </Fields>
</Enum>
</Enums>
");    
            sb.Append("</root>");
            return sb.ToString();
        }
    }
}
  
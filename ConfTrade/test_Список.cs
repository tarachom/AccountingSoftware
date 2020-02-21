
    
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
            
            Довідники.test_Список_View m_test_Список_View = new Довідники.test_Список_View();
            
            sb.Append(m_test_Список_View.Read());
            
            
            
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
  
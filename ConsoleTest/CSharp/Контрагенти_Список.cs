
    
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
            
            Довідники.Контрагенти_Список_View m_Контрагенти_Список_View = new Довідники.Контрагенти_Список_View();
            
            sb.Append(m_Контрагенти_Список_View.Read());
            
            
            sb.Append(@"<Enums>
<Enum>
  <Name>ВидиКонтрагентов</Name>
  <Desc>ВидыКонтрагентов</Desc>
  <Fields>
    <Field>
      <Name>Организация</Name>
      <Value>1</Value>
    </Field>
    <Field>
      <Name>ЧастноеЛицо</Name>
      <Value>2</Value>
    </Field>
    <Field>
      <Name>Нерезидент</Name>
      <Value>3</Value>
    </Field>
    <Field>
      <Name>Безналоговые</Name>
      <Value>4</Value>
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
  
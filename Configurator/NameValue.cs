
namespace Configurator
{
	/// <summary>
	/// Використовується для елементів ComboBox, коли потрібно задати відображення для обєкта
	/// </summary>
	/// <typeparam name="T">Тип обєкта для якого задається відображення</typeparam>
	/// <example>
	/// comboBoxRegisterType.Items.Add(new NameValue<TypeRegistersAccumulation>("Залишки", TypeRegistersAccumulation.Residues));
	/// comboBoxRegisterType.Items.Add(new NameValue<TypeRegistersAccumulation>("Обороти", TypeRegistersAccumulation.Turnover));
	///	comboBoxRegisterType.SelectedItem = comboBoxRegisterType.Items[0];
	///	
	/// ...
	/// 
	/// var a = ((NameValue<TypeRegistersAccumulation>)comboBoxRegisterType.SelectedItem).Value
	/// </example>
	public class NameValue<T>
	{
		public NameValue() { }
		
		public NameValue(string name, T value)
		{
			Name = name; 
			Value = value;
		}

		public string Name { get; set; }

		public T Value { get; set; }

		public override string ToString()
		{
			return Name;
		}
	}
}
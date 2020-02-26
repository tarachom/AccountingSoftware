using System;
using System.Windows.Forms;
using AccountingSoftware;

namespace Trade
{
	public static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Form1());
		}

		public static Kernel Kernel { get; set; }
	}
}

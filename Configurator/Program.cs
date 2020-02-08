using System;
using System.Collections.Generic;
using System.Windows.Forms;
using AccountingSoftware;

namespace Configurator
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
			Application.Run(new FormConfiguration());
		}

		public static Kernel Kernel { get; set; }
	}
}

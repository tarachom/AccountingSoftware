

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using AccountingSoftware;
using Конфа = StorageAndTrade_1_0;
using Константи = StorageAndTrade_1_0.Константи;
using Довідники = StorageAndTrade_1_0.Довідники;
using Перелічення = StorageAndTrade_1_0.Перелічення;

namespace StorageAndTrade
{
    public partial class Form_ОрганізаціїЕлемент : Form
    {
        public Form_ОрганізаціїЕлемент()
        {
            InitializeComponent();
        }

        public Form_Організації OwnerForm { get; set; }
        
        public Nullable<bool> IsNew { get; set; }

        public string Uid { get; set; }

        private Довідники.Організації_Objest Організації_Objest { get; set; }

		private void Form_ОрганізаціїЕлемент_Load(object sender, EventArgs e)
        {
			if (IsNew.HasValue)
			{
				Організації_Objest = new Довідники.Організації_Objest();

				if (IsNew.Value)
				{
					this.Text += " - Новий";
					//textBox_Код.Text = Організації_Objest.Код = (++Константи.НумераціяДовідників.Організації_Const).ToString("D6");
				}
				else
				{
					if (Організації_Objest.Read(new UnigueID(Uid)))
					{
						this.Text += " - Редагування";
						
			            //textBox_Назва.Text = Організації_Objest.Назва;
			            
			            //textBox_Код.Text = Організації_Objest.Код;
			            
			            //textBox_НазваПовна.Text = Організації_Objest.НазваПовна;
			            
			            //textBox_НазваСкорочена.Text = Організації_Objest.НазваСкорочена;
			            
					}
					else
						MessageBox.Show("Error read");
				}
			}
		}

        private void buttonSave_Click(object sender, EventArgs e)
        {
			if (IsNew.HasValue)
			{
				if (IsNew.Value)
					Організації_Objest.New();

				try
				{
					
			        //Організації_Objest.Назва = textBox_Назва.Text;
			        
			        //Організації_Objest.Код = textBox_Код.Text;
			        
			        //Організації_Objest.НазваПовна = textBox_НазваПовна.Text;
			        
			        //Організації_Objest.НазваСкорочена = textBox_НазваСкорочена.Text;
			        Організації_Objest.Save();
				}
				catch (Exception exp)
				{
					MessageBox.Show(exp.Message);
					return;
				}

				if (OwnerForm != null)
					OwnerForm.LoadRecords();

				this.Close();
			}
		}

		private void buttonClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
	
	
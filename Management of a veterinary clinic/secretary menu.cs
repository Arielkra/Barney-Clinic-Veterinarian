using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Management_of_a_veterinary_clinic
{
    public partial class SecretaryMenu : Form
    {
        public SecretaryMenu()
        {
            InitializeComponent();
        }

        private void close_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("?האם אתה בטוח שאתה רוצה לצאת", "!יציאה ", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (dr == DialogResult.OK)
            {
                this.Close();

                login aaa = new login();
                aaa.Show();

            }
        }
    }
}

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
    public partial class startM : Form
    {
        public startM()
        {
            InitializeComponent();
        }

        //כפתור יציאה בתפריט ראשי
        private void יציאהToolStripMenuItem_Click(object sender, EventArgs e)
        {
            login d = new login();
            this.Hide();
            d.ShowDialog();
            this.Show();
        }

        //כפתור כניסה למערכת מעבר לחלון הזדהות
        private void enterbt_Click(object sender, EventArgs e)
        {
            login d = new login();
            this.Hide();
            d.ShowDialog();
            this.Show();
        }

        //כפתור יציאה מהמערכת 
        private void closebt_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("?האם אתה בטוח שאתה רוצה לצאת", "! יציאה מהמערכת ", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (dr == DialogResult.OK)
            {
                Application.ExitThread();
            }
           
        }
        //כפתור יציאה בתפריט ראשי
        private void יציאהToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("?האם אתה בטוח שאתה רוצה לצאת", "! יציאה מהמערכת ", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (dr == DialogResult.OK)
            {
                Application.ExitThread();
            }
        }
        //כפתור כניסה למנהל מערכת
        private void Manegerbt_Click(object sender, EventArgs e)
        {
            Lgoin_ManagerSystem d = new Lgoin_ManagerSystem();
            d.ShowDialog();
        }
        //כפתור כניסה למנהל מערכת דרך תפריט
        private void מנהלמערכתToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Lgoin_ManagerSystem d = new Lgoin_ManagerSystem();
            d.ShowDialog();
        }
    }
}

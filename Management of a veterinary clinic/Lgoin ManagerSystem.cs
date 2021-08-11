using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Security.Cryptography;

namespace Management_of_a_veterinary_clinic
{
    public partial class Lgoin_ManagerSystem : Form
    {
        public Lgoin_ManagerSystem()
        {
            InitializeComponent();
        }
        //פונקצית סיסמא בסגנון SHA1 מאובטחת
        public string maksha1(string str)        {            SHA1CryptoServiceProvider sh = new SHA1CryptoServiceProvider();            sh.ComputeHash(ASCIIEncoding.ASCII.GetBytes(str));            byte[] re = sh.Hash;            StringBuilder sb = new StringBuilder();            foreach (byte b in re)            {                sb.Append(b.ToString("X2"));            }            return sb.ToString();        }
        //טעינה של התפקיד וכניסה למערכת
        private void Lgoin_ManagerSystem_Load(object sender, EventArgs e)
        {

            List<string> columes = new List<string>() { "job" };

            string query = SQL_Queries.Select("LoginManagerSystem", columes);
            List<Row> table = Access.getObjects(query);
            if (table != null)
            {
                foreach (Row row in table)
                {
                    bool flag = false;//בדיקה אם יש כפילויות
                    foreach (object item in comboBox1.Items)
                    {
                        if (row.GetColValue("job").ToString() == item.ToString())
                            flag = true;
                    }
                    if (flag == false)
                        comboBox1.Items.Add(row.GetColValue("job").ToString());
                }
                comboBox1.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show(Access.ExplaindError());

            }

           
        }
        //כפתור כניסה למנהל מערכת
        private void Loginbt_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;
            string jobs = comboBox1.Text;




            if (textBox1.Text == "")
            {
                MessageBox.Show("הכנס שם משתמש");
            }
            else
            {
                if (textBox2.Text == "")
                {
                    MessageBox.Show("הכנס סיסמא");
                }
                else
                {

                    if (Access_Actions.loginManage(username, password, jobs))
                    {

                        this.Hide();
                        ManagerSystem d = new ManagerSystem();
                        d.ShowDialog();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("!אין לך הרשאה");
                    }
                    

                }
            }
        }
        //כפתור סגירה
        private void closebt_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

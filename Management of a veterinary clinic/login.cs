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
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();

            

        }
        //פונקציה לסיסמא מאובטחת בסגנון SHA1
        public string maksha1(string str)        {            SHA1CryptoServiceProvider sh = new SHA1CryptoServiceProvider();            sh.ComputeHash(ASCIIEncoding.ASCII.GetBytes(str));            byte[] re = sh.Hash;            StringBuilder sb = new StringBuilder();            foreach (byte b in re)            {                sb.Append(b.ToString("X2"));            }            return sb.ToString();        }
        //כפתור כניסה לאחר הזנת שם משתמש סיסמא ובחירת תפקיד
        private void Loginbt_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password1 = textBox2.Text;
            string jobs = comboBox1.Text;




            if (textBox1.Text == "")
            {
                MessageBox.Show("הכנס שם משתמש", "שגיאה ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (textBox2.Text == "")
                {
                    MessageBox.Show("הכנס סיסמא", "שגיאה ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DoctorMenu doctor = new DoctorMenu();
                    SecretaryMenu secrtry = new SecretaryMenu();

                    if (Access_Actions.login(username, password1, jobs))
                    {

                        this.Hide();
                        if (comboBox1.SelectedIndex == 0)
                            doctor.ShowDialog();
                        else
                            secrtry.ShowDialog();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("!אין לך הרשאה", "שגיאה ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }



                    
                }
            }

        }
        //טוען את התפקיד
        private void login_Load(object sender, EventArgs e)
        {
            textBox1.Text = "gila";
            textBox2.Text = "1234";
            List<string> columes = new List<string>() { "job" };

            string query = SQL_Queries.Select("login1", columes);
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
        //כפתור סגירה
        private void backbt_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("?האם אתה בטוח שאתה רוצה לצאת", "!יציאה ", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (dr == DialogResult.OK)
            {
                this.Close();

            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //לא בשימוש
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Data.OleDb;

namespace Management_of_a_veterinary_clinic
{
    
    public partial class ManagerSystem : Form
    {
        List<Row> clientc2;
        public ManagerSystem()
        {
            InitializeComponent();
        }
       //פונקציה בוליאנית שבודקת אם חסר נתונים
        private bool validation()
        {
            if (usernametx.Text == "" || passwordtx.Text == "")
                return false;
            else return true;
        }
        //פוקציה שטוענת ומציגה לפי השם הנחבר את התפקיד שלו 
        private void ManagerSystem_Load(object sender, EventArgs e)
        {
           
                List<string> columes = new List<string>() { "job","username" };
            combojobs2.Items.Clear();
            comboBox1.Items.Clear();
                string query = SQL_Queries.Select("login1", columes);
            clientc2 = Access.getObjects(query);
                if (clientc2 != null)
                {
                    foreach (Row row in clientc2)
                    {
                        bool flag = false;//בדיקה אם יש כפילויות
                    combojobs2.Items.Add(row.GetColValue("username"));
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

        //יצירת משתמש חדש
        private void savebt_Click(object sender, EventArgs e)
        {

            List<object> values = new List<object>();
            values.Add(usernametx.Text);
            values.Add(Access_Actions.maksha1(passwordtx.Text));
            values.Add(comboBox1.Text);
            values.Add(usernamehebtx.Text);
            string qurey = SQL_Queries.Insert("login1", values);
            if (validation())
            {
                if (Access.Execute(qurey))
                {
                    MessageBox.Show("!משתמש חדש נוסף בהצלחה למערכת", "הוספת משתמש ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    usernametx.Text = "";passwordtx.Text = "";comboBox1.Text = ""; usernamehebtx.Text = "";
                }
                else
                {
                    MessageBox.Show(Access.ExplaindError());
                }
            }
            else
                MessageBox.Show("please enter username and password");
        }

        private void usernametx_TextChanged(object sender, EventArgs e)
        {

        }
        //כפתור סגירה חלון
        private void closebt_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("?האם אתה בטוח שאתה רוצה לצאת", "!יציאה ", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (dr == DialogResult.OK)
            {
                this.Close();

              

            }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {
            //אין שימוש
        }

        //הסרת משתמש מהמערכת
        private void removebt_Click(object sender, EventArgs e)
        {
          
            
            Condition con = new Condition("username",combojobs2.Text);
            string qurey = SQL_Queries.Delete("login1", con);
            
                if (Access.Execute(qurey))
                {
                MessageBox.Show("!משתמש הוסר בהצלחה", "הסרת משתמש ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ManagerSystem_Load(null, null);
                    combojobs2.Text = "";
                    joblabel.Text = "";
                }
                else
                {
                    MessageBox.Show(Access.ExplaindError());
                }
           
        }
        //כפתור עדכון סיסמא למשתמש
        private void button1_Click(object sender, EventArgs e)
        {
            List<Col> list = new List<Col>();
            list.Add(new Col("password1", Access_Actions.maksha1(updatepasstx.Text)));
            Condition con = new Condition("username", combojobs2.Text);
            string qurey = SQL_Queries.Update("login1",list,con);
            if (Access.Execute(qurey))
            {
                MessageBox.Show("!סיסמא עודכנה בהצלחה", "עדכון סיסמא ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ManagerSystem_Load(null, null);
                updatepasstx.Text = "";
            }
            else
            {
                MessageBox.Show(Access.ExplaindError());
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            //לא בשימוש
        }

        //כפתור רענון פרטים
        private void button2_Click(object sender, EventArgs e)
        {

            ManagerSystem_Load(null, null);

        }

        //מתיג את התפקיד של המשתמש רופא את מזכירה
        private void combojobs2_SelectedIndexChanged(object sender, EventArgs e)
        {
            joblabel.Text = clientc2[combojobs2.SelectedIndex].GetColValue("job").ToString();
        }
    }
    
}

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

namespace Management_of_a_veterinary_clinic
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {



            string username = textBox1.Text;
            string password = textBox2.Text;
            string jobs = comboBox1.Text;


            
            
                if (textBox1.Text == "")
                {
                    MessageBox.Show("Insert Username!");
                }
                else
                {
                    if (textBox2.Text == "")
                    {
                        MessageBox.Show("Insert Password!");
                    }
                    else
                    {


                        try
                        {
                            string strDb = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=mvc project.accdb;" + "Persist Security Info=False";
                            OleDbConnection conn = new OleDbConnection(strDb);
                            conn.Open();
                            OleDbDataReader dr;
                            OleDbCommand cmd = new OleDbCommand("Select * from login where username='" + textBox1.Text + "' and password='" + textBox2.Text + "' and job='" + comboBox1.Text + "' ;", conn);

                            dr = cmd.ExecuteReader();

                            string str1 = "";
                            string str2 = "";
                            string str3 = "";



                            while (dr.Read())
                            {

                                str1 += dr["username"] + "\n";
                                str2 += dr["password"] + "\n";
                                str3 += dr["job"] + "\n";

                            }

                            dr.Close();
                            conn.Close();
                            if (str1 == "" || str2 == "" || str3 == "")
                            {
                                MessageBox.Show("משהו השתבש!");
                            }
                            else
                            {
                            if (comboBox1.SelectedIndex==0)
                            {
                                Form2 d = new Form2();
                                d.Show();
                                this.Hide();
                            }
                            else
                            {
                                Form3 d = new Form3();
                                d.Show();
                                this.Hide();
                            }
                            }



                        }
                        catch (Exception err)
                        {

                            MessageBox.Show(err.Message);
                        }

                    }
                }
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            




            try
            {
                string username = textBox1.Text;



                string strDb;
                strDb = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=mvc project.accdb;" + "Persist Security Info=False";
                OleDbConnection conn = new OleDbConnection(strDb);
                conn.Open();
                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandText = "select * from login";
                OleDbDataReader n = cmd.ExecuteReader();
                while (n.Read())
                {
                    comboBox1.Items.Add(n["job"].ToString());

                }

                comboBox1.SelectedIndex = 0;


                conn.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }    
    }
}

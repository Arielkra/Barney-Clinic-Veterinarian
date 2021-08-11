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
using System.IO;

namespace Management_of_a_veterinary_clinic
{
    public partial class DoctorMenu : Form
    {
        int lastindexA;
        List<Col> row = new List<Col>();
        List<Row> clienta,workers,drugs, vaccines;
        private List<string> fields = new List<string>() { "namep", "namef", "phone", "address", "animalname", "email" };
        List<Col> rowa = new List<Col>();
        List<Row> queues, animals_list;
        List<Col> rowq = new List<Col>();
        private List<string> fields2 = new List<string>() { "date1", "time1", "doctor" };
        List<Col> rowd = new List<Col>();
        private List<string> fields3 = new List<string>() { "idd", "namedr", "price" };

        public DoctorMenu()
        {
            InitializeComponent();
        }
        //פונקציות בוליאניות שבודקות אם חסר נתונים
        private bool validationtreatment()
        {
            if (DoctorBoxA.Text==""||heatboxA.Text==""||pulseBoxA.Text==""||problamBoxA.Text==""||reaportBox.Text=="")

                return false;

            else return true;
        }
        
        private void closebt_Click(object sender, EventArgs e)
        {
            //לא בשימוש
        }

        private void DoctorMenu_Load(object sender, EventArgs e)
        {
            //טוען רשימת של החיות
            string queryCA = SQL_Queries.Select("animals");
            animals_list = Access.getObjects(queryCA);
            if (animals_list != null)
            {

                foreach (Row row in animals_list)
                {
                    lastindexA = int.Parse(row.GetColValue("trcode").ToString());

                }
            }
            else
            {
                MessageBox.Show(Access.ExplaindError());
            }



            //טוען רשימה של טיפולים
            string queryTA = SQL_Queries.Select("treatment");
            queues = Access.getObjects(queryTA);
            if (queues != null)
            {

                foreach (Row row in queues)
                {
                    lastindexA = int.Parse(row.GetColValue("tcode").ToString());

                }
            }
            else
            {
                MessageBox.Show(Access.ExplaindError());
            }


            
            
            
            //מציג רשימה של רופאים ואת התפקיד
            string job = DoctorBoxA.Text;
            DoctorBoxA.Text = "";
            DoctorBoxA.Items.Clear();
            Condition conditions3 = new Condition("job", @"רופא\ה");
            string query2 = SQL_Queries.Select("login1", conditions3);
            workers = Access.getObjects(query2);
            if (workers != null)
            {

                foreach (Row row in workers)
                {
                    string name = row.GetColValue("usernameheb").ToString() + " " + row.GetColValue("job").ToString();
                    DoctorBoxA.Items.Add(name);
                }
            }
            else
            {
                MessageBox.Show(Access.ExplaindError());
            }

        }

        private void infoaclient_SelectedIndexChanged(object sender, EventArgs e)
        {

              //לא בשימוש
        }

        //מציג רשימת לקוחות לפי חיפוש של ת.ז
        private int getclientindex(string id)
        {
            for(int i = 0; i < clienta.Count; i++)
            {
                if (clienta[i].GetColValue("idc").ToString() == id)
                    return i;

            }
            MessageBox.Show("!לקוח לא נמצא במערכת", "!שגיאה ", MessageBoxButtons.OK, MessageBoxIcon.Error);
           
            return -1;
        }
        

       //מציג את הכל הפרטים הממדים של החיה שנבחרה
      private void InfoshowAnimals(Row row)
        {
           
            Agelb.Text = row.GetColValue("age").ToString();
            typelb.Text = row.GetColValue("taypa").ToString();
            sexlb.Text = row.GetColValue("sex").ToString();
            Colorlb.Text = row.GetColValue("colora").ToString();
            castratedlb.Text = row.GetColValue("castrated").ToString();
            Omlb.Text = row.GetColValue("om").ToString();
            weightlb.Text = row.GetColValue("weight").ToString();
            Datebornlb.Text = row.GetColValue("dateborn").ToString();
            Descriptx.Text = row.GetColValue("description1").ToString();

        }

        //פונקציה שטוענת את כל הרשימה של החיות ללקוח לפי תעודת זהות
        private void Load_Animals(string owner)
        {
            string queryAN = SQL_Queries.Select("animals", new Condition("idac", owner));
            
            animals_list = Access.getObjects(queryAN);
            if (animals_list != null)
            {
                AnimalDoctor.Items.Clear();
                foreach (Row row in animals_list)
                    AnimalDoctor.Items.Add(row.GetColValue("namea").ToString());
            }
            else
                animals_list = new List<Row>();

        }
        //פונקציה שטוענת את כל הרשימה של החיות ללקוח לפי תעודת זהות כדי להציג את ההיסטורית טיפולים של החיה
        private void Load_HistoryA(string owner)
        {
            string queryHA = SQL_Queries.Select("animals", new Condition("idac", owner));

            animals_list = Access.getObjects(queryHA);
            if (animals_list != null)
            {
                AnimalDoctor.Items.Clear();
                foreach (Row row in animals_list)
                    AnimalDoctor.Items.Add(row.GetColValue("namea").ToString());
            }
            else
                animals_list = new List<Row>();

        }

        //פונקציה שטוענת את כל הרשימה של החיות ללקוח לפי תעודת זהות כדי להציג את הסטטיסטיקה של כל חיה שנבחרה
        private void Load_Pie(string owner)
        {
            string queryPie1 = SQL_Queries.Select("animals", new Condition("idac", owner));

            animals_list = Access.getObjects(queryPie1);
            if (animals_list != null)
            {
                AnimalPaieCom.Items.Clear();
                foreach (Row row in animals_list)
                    AnimalPaieCom.Items.Add(row.GetColValue("namea").ToString());
            }
            else
                animals_list = new List<Row>();

        }

        //בחירת חיה ואז מציג מתי התאריך האחרון שבקרה במרפאה
        private void AnimalDoctor_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (AnimalDoctor.SelectedIndex == -1) return;

            Row row = animals_list[AnimalDoctor.SelectedIndex];
            InfoshowAnimals(row);

            List<Condition> conditions = new List<Condition>() {
                new Condition("idat",ClientBoxs.Text),new Condition("animal_name",AnimalDoctor.Text)

                };
            string qurey2 = SQL_Queries.Select("treatment", conditions, "and");

            List<Row> ques = Access.getObjects(qurey2);

            if (ques != null)
            {
                try
                {
                    if (ques.Count != 0)
                    {
                        int i = 0, big = int.Parse(ques[0].GetColValue("tcode").ToString()), temp;
                        for (int j = 0; j < ques.Count; j++)
                        {
                            temp = int.Parse(ques[j].GetColValue("tcode").ToString());
                            if (temp > big)
                            {
                                big = temp;
                                i = j;
                            }

                        }
                        datelastlb.Text = ques[i].GetColValue("datet").ToString();

                    }
                    else
                    {
                        datelastlb.Text = "לא קיים תאריך";

                    }
                }
                catch { }
            }
            else
            {
                MessageBox.Show(Access.ExplaindError());
            }


            string idat = ClientBoxs.Text;
            string animal_name = AnimalDoctor.Text;
            string datet = CalendarHistory.SelectionRange.Start.ToShortDateString();

            Condition conditionpay3 = new Condition("idat", idat);
            Condition conditionpay1 = new Condition("animal_name", animal_name);
            Condition conditionpay2 = new Condition("datet", datet);

            List<Condition> conditionsS = new List<Condition>() { conditionpay3, conditionpay2, conditionpay1 };
            string qureyHis = SQL_Queries.Select("treatment", conditionsS, "and");
            List<Row> teablh = Access.getObjects(qureyHis);
            if (teablh != null)
            {
                if (teablh.Count == 0)
                {
                    ProblamHtx.Text = "לא הוזנה תלונה באותו יום";
                    TreatmentHtx.Text = "לא הוזן טיפול באותו יום";
                    NameDocHlb.Text = "";
                    HealtHlb.Text = "";
                    PulseHlb.Text = "";
                    weightHlb.Text = "";

                }
                else
                {
                    Row rowS = teablh[0];
                    NameDocHlb.Text = rowS.GetColValue("doctort").ToString();
                    HealtHlb.Text = rowS.GetColValue("heat").ToString();
                    PulseHlb.Text = rowS.GetColValue("pulse").ToString();
                    weightHlb.Text = rowS.GetColValue("weightt").ToString();
                    ProblamHtx.Text = rowS.GetColValue("problam").ToString();
                    TreatmentHtx.Text = rowS.GetColValue("freetext").ToString();


                }

            }
        }
        //כפתור ניקוי למדדים של החיות
        private void CleanAnimal_Click(object sender, EventArgs e)
        {
            AnimalDoctor.Text = "";
            Agelb.Text = "";
            typelb.Text = "";
            sexlb.Text = "";
            Colorlb.Text = "";
            castratedlb.Text = "";
            Omlb.Text = "";
            Datebornlb.Text = "";
            weightlb.Text = "";
            Descriptx.Text = "";
            datelastlb.Text = "";
        }

        //כפתור פותח את החלון של חיסונים ותרופות
        private void OpenDV_Click(object sender, EventArgs e)
        {
            DrugsVaccines d = new DrugsVaccines(this);
            d.ShowDialog();

        }
        //כפתור סגירה של החלון 
        private void closebtDoctor_Click(object sender, EventArgs e)
        {
            
                DialogResult dr = MessageBox.Show("?האם אתה בטוח שאתה רוצה לצאת", "!יציאה ", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (dr == DialogResult.OK)
            {
                this.Close();
                
            }
           

        }
        //חיפוש של לקוחות והחיות שלהם בחלון של ההיסטוריה טיפולים
        private void SearchClientH_Click(object sender, EventArgs e)
        {
            


            

        }
        //חיפוש טיפולים לפי תאריך 
        private void CalendarHistory_DateChanged(object sender, DateRangeEventArgs e)
        {
            CalendarHistory.MaxDate = DateTime.Today.AddDays(0);
            dateTimeHisP.Value = CalendarHistory.SelectionStart;
            string idat = ClientBoxs.Text;

            Condition condition = new Condition("idat", idat);
            string querypaylist = SQL_Queries.Select("treatment", condition);
            List<Row> table = Access.getObjects(querypaylist);
            if (table != null)
            {

                foreach (Row r in table)
                {

                    if (CalendarHistory.SelectionRange.Start.ToLongDateString() == r.GetColValue("datet").ToString())
                    {
                        Row row = table[0];
                        

                    }


                }

            }
            else
            {
                MessageBox.Show(Access.ExplaindError());
            }

        }

        //מציג את התרופות שנתנו באותו תאריך נבחר בהיסטוריה טיפולים
        private void Load_DrugsHistory(string owner, string datedr, string named)
        {


            Condition cond1 = new Condition("idclientsdrugs", owner);
            Condition cond2 = new Condition("dated", datedr);
            Condition cond3 = new Condition("animalnamed", named);
            List<Condition> conditions = new List<Condition>() { cond1, cond2, cond3 };
            string queryAN = SQL_Queries.Select("treatmentdrugs", conditions, "and");

            drugs = Access.getObjects(queryAN);
            if (drugs != null)
            {
                DrugsHistory.Items.Clear();
               
                foreach (Row row in drugs)
                {
                    DrugsHistory.Items.Add(row.GetColValue("drugsname").ToString());

                }
            }
            else
                drugs = new List<Row>();

        }

        //מציג את החיסונים שנתנו באותו תאריך נבחר בהיסטוריה טיפולים
        private void Load_VaccinesHistory(string owner, string datevr, string namev)
        {


            Condition conv1 = new Condition("idclientsvaccines", owner);
            Condition conv2 = new Condition("datev", datevr);
            Condition conv3 = new Condition("animalnamev", namev);
            List<Condition> conditions = new List<Condition>() { conv1, conv2, conv3 };
            string queryhisv = SQL_Queries.Select("treatmentvaccines", conditions, "and");

            vaccines = Access.getObjects(queryhisv);

            if (vaccines != null)
            {
                vaccinesHistory.Items.Clear();
                
                foreach (Row row in vaccines)
                {
                    vaccinesHistory.Items.Add(row.GetColValue("vaccinesname").ToString());

                }
            }
            else
                vaccines = new List<Row>();

        }

        //כפתור הצגת הנתונים בהיסטוריה טיפולים של החיה לפי בחירה של הלקוח ותאריך
        private void ShowHistory_Click(object sender, EventArgs e)
        {
       
        }
        //בחירת תאריך בלוח שנה לפי תאריך שנבחר בחלונית תאריך
        private void dateTimeHisP_ValueChanged(object sender, EventArgs e)
        {
            dateTimeHisP.MaxDate = DateTime.Today.AddDays(0);
            CalendarHistory.SetDate(dateTimeHisP.Value);
        }
        //כפתור יציאה וחזרה למערכת
        private void returnH_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("?האם אתה בטוח שאתה רוצה לצאת", "!יציאה ", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (dr == DialogResult.OK)
            {
                this.Close();



            }
        }
        //כפתור חיפוש לקוח בסטטיסטיקה
        private void SearchPaibt_Click(object sender, EventArgs e)
        {
            string idc = PaiTexId.Text;
            string idac = PaiTexId.Text;
            string namea = AnimalPaieCom.Text;


            if (PaiTexId.Text == "")
            {
                MessageBox.Show("הכנס אחד הפרטים", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {



                Condition condition = new Condition("idac", idac);
                string qurePie = SQL_Queries.Select("animals", condition);
                List<Condition> conditions = new List<Condition>() {
                new Condition("idat",idac),new Condition("animal_name",namea)

                };
                string qureyp2 = SQL_Queries.Select("treatment", conditions, "and");
                List<Row> teabl = Access.getObjects(qurePie);
                if (teabl != null)
                {
                    if (teabl.Count == 0)
                    {
                        MessageBox.Show("לא קיימות חיות לתצוגה ללקוח", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        Row row = teabl[0];

                        Load_Pie(row.GetColValue("idac").ToString());
                        AnimalPaieCom.SelectedIndex = 0;

                    }
                }
                else
                {
                    MessageBox.Show(Access.ExplaindError());
                }


            }
        }

        //הצגת כל נתוני ה סטטיסטיקה של כל חיה שנבחרה , אם ויש לה נתונים יציג אותם בעוגה יוצר עוגה תוך כדי ואם אין נתונים לא יוצג כלום
        private void AnimalPaieCom_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strDb = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=mvc project.accdb;" + "Persist Security Info=False";
            System.Data.OleDb.OleDbConnection conn = new System.Data.OleDb.OleDbConnection(strDb);
            conn.Open();
            System.Data.OleDb.OleDbDataReader dr;
            OleDbCommand cmd = new OleDbCommand("SELECT COUNT(idclientsdrugs) AS [counter] FROM treatmentdrugs Where idclientsdrugs = '" + PaiTexId.Text + "' and animalnamed = '" + AnimalPaieCom.SelectedItem + "' ;", conn);//command sql
            dr = cmd.ExecuteReader();
            int cwerwrfwq = 0;

            while (dr.Read())
            {
                cwerwrfwq = int.Parse(dr["counter"].ToString());
            }
            dr.Close();
            conn.Close();
            foreach (var s in chart1.Series)
            {
                s.Points.Clear();
            }
            chart1.Series["Series1"].IsValueShownAsLabel = true;
            chart1.Series["Series1"].Points.AddXY("תרופות", cwerwrfwq);

            string strDb1 = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=mvc project.accdb;" + "Persist Security Info=False";
            System.Data.OleDb.OleDbConnection conn1 = new System.Data.OleDb.OleDbConnection(strDb1);
            conn1.Open();
            System.Data.OleDb.OleDbDataReader dr1;
            OleDbCommand cmd1 = new OleDbCommand("SELECT COUNT(idclientsvaccines) AS [counter] FROM treatmentvaccines Where idclientsvaccines = '" + PaiTexId.Text + "' and animalnamev = '" + AnimalPaieCom.SelectedItem + "' ;", conn1);//command sql
            dr1 = cmd1.ExecuteReader();
            cwerwrfwq = 0;

            while (dr1.Read())
            {
                cwerwrfwq = int.Parse(dr1["counter"].ToString());
            }
            dr1.Close();
            conn1.Close();
            chart1.Series["Series1"].Points.AddXY("חיסונים", cwerwrfwq);

            string strDb2 = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=mvc project.accdb;" + "Persist Security Info=False";
            System.Data.OleDb.OleDbConnection conn2 = new System.Data.OleDb.OleDbConnection(strDb2);
            conn2.Open();
            System.Data.OleDb.OleDbDataReader dr2;
            OleDbCommand cmd2 = new OleDbCommand("SELECT COUNT(idclientpay) AS [counter] FROM paylist Where idclientpay = '" + PaiTexId.Text + "' and animalpay = '" + AnimalPaieCom.SelectedItem + "' ;", conn2);//command sql
            dr2 = cmd2.ExecuteReader();
            cwerwrfwq = 0;

            while (dr2.Read())
            {
                cwerwrfwq = int.Parse(dr2["counter"].ToString());
            }
            dr2.Close();
            conn2.Close();
            chart1.Series["Series1"].Points.AddXY("תשלומים", cwerwrfwq);
        }
        //כפתור סיגרה לסטטיסטיקה
        private void CloseStat_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("?האם אתה בטוח שאתה רוצה לצאת", "!יציאה ", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (dr == DialogResult.OK)
            {
                this.Close();



            }
        }

        //בדיקת כמות ספרות של ת.ז
        private void ClientBoxs_TextChanged(object sender, EventArgs e)
        {
            if (ClientBoxs.Text.Length > 9)
            {

                MessageBox.Show("בבקשה נא להזין לא יותר מ-9 ספרות", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ClientBoxs.Text = "";
            }
        }

        //בדיקת כמות ספרות של ת.ז
        private void IdclientH_TextChanged(object sender, EventArgs e)
        {
            if (ClientBoxs.Text.Length > 9)
            {

                MessageBox.Show("בבקשה נא להזין לא יותר מ-9 ספרות", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ClientBoxs.Text = "";
            }
        }

        //בדיקת כמות ספרות של ת.ז
        private void PaiTexId_TextChanged(object sender, EventArgs e)
        {
            if (PaiTexId.Text.Length > 9)
            {

                MessageBox.Show("בבקשה נא להזין לא יותר מ-9 ספרות", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error);
                PaiTexId.Text = "";
            }

        }

        private void DrugsHistory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string idat = ClientBoxs.Text;
            string animal_name = AnimalDoctor.Text;
            string datet = CalendarHistory.SelectionRange.Start.ToShortDateString();

            Condition conditionpay3 = new Condition("idat", idat);
            Condition conditionpay1 = new Condition("animal_name", animal_name);
            Condition conditionpay2 = new Condition("datet", datet);

            List<Condition> conditions = new List<Condition>() { conditionpay3, conditionpay2, conditionpay1 };
            string qureyHis = SQL_Queries.Select("treatment", conditions, "and");
            List<Row> teablh = Access.getObjects(qureyHis);
            if (teablh != null)
            {
                if (teablh.Count == 0)
                {
                    ProblamHtx.Text = "לא הוזנה תלונה באותו יום";
                    TreatmentHtx.Text = "לא הוזן טיפול באותו יום";
                    NameDocHlb.Text = "";
                    HealtHlb.Text = "";
                    PulseHlb.Text = "";
                    weightHlb.Text = "";

                }
                else
                {
                    Row row = teablh[0];
                    NameDocHlb.Text = row.GetColValue("doctort").ToString();
                    HealtHlb.Text = row.GetColValue("heat").ToString();
                    PulseHlb.Text = row.GetColValue("pulse").ToString();
                    weightHlb.Text = row.GetColValue("weightt").ToString();
                    ProblamHtx.Text = row.GetColValue("problam").ToString();
                    TreatmentHtx.Text = row.GetColValue("freetext").ToString();


                }
            }



            //הצגת התרופות שניתנו באותו תאריך נבחר
            Condition conPay3 = new Condition("idclientsdrugs", idat);
            Condition conPay4 = new Condition("dated", datet);
            Condition conPay5 = new Condition("animalnamed", animal_name);
            List<Condition> conditionshv = new List<Condition>() { conPay3, conPay4, conPay5 };
            string qurey_Hist = SQL_Queries.Select("treatmentdrugs", conditionshv, "and");
            List<Row> teabl = Access.getObjects(qurey_Hist);
            DrugsHistory.Items.Clear();
            if (teabl != null)
            {
                if (teabl.Count == 0)
                {

                    DrugsHistory.Items.Clear();
                    DrugsHistory.Items.Add("לא ניתנו תרופות באותו יום");

                }
                else
                {

                    Row row = teabl[0];
                    Load_DrugsHistory(row.GetColValue("idclientsdrugs").ToString(), CalendarHistory.SelectionRange.Start.ToShortDateString(), AnimalDoctor.Text);

                }

            }
            else
            {
                MessageBox.Show(Access.ExplaindError());
            }






            //הצגת החיסונים שניתנו באותו תאריך נבחר
            Condition conPay6 = new Condition("idclientsvaccines", idat);
            Condition conPay7 = new Condition("datev", datet);
            Condition conPay8 = new Condition("animalnamev", animal_name);
            List<Condition> conditions2 = new List<Condition>() { conPay6, conPay7, conPay8 };
            string qurey_His2 = SQL_Queries.Select("treatmentvaccines", conditions2, "and");
            List<Row> teablhis = Access.getObjects(qurey_His2);

            if (teablhis != null)
            {
                if (teablhis.Count == 0)
                {

                    vaccinesHistory.Items.Clear();
                    vaccinesHistory.Items.Add("לא ניתנו חיסונים באותו יום");

                }
                else
                {
                    Row row9 = teablhis[0];

                    Load_VaccinesHistory(row9.GetColValue("idclientsvaccines").ToString(), CalendarHistory.SelectionRange.Start.ToShortDateString(), AnimalDoctor.Text);
                }
            }
            else
            {
                MessageBox.Show(Access.ExplaindError());
            }
        }

        //חיפוש לקוח והצגת החיות שלו בחלונית הטיפולים
        private void SerchClientA_Click(object sender, EventArgs e)
        {

            //string idc = ClientBoxs.Text;
            string idat = ClientBoxs.Text;
            string namea = AnimalDoctor.Text;
            string datet = CalendarHistory.SelectionRange.Start.ToShortDateString();



            if (ClientBoxs.Text == "")
            {
                MessageBox.Show("הכנס אחד הפרטים", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {



                Condition condition = new Condition("idac", idat);
                string qurey = SQL_Queries.Select("animals", condition);
                List<Condition> conditions = new List<Condition>() {
                new Condition("idat",idat),new Condition("animal_name",namea)

                };
                string qurey2 = SQL_Queries.Select("treatment", conditions, "and");
                List<Row> teabl = Access.getObjects(qurey);
                if (teabl != null)
                {
                    if (teabl.Count == 0)
                    {
                        MessageBox.Show("לא קיימות חיות לתצוגה ללקוח", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        Row row = teabl[0];

                        Load_Animals(row.GetColValue("idac").ToString());
                        AnimalDoctor.SelectedIndex = 0;

                    }
                }
                else
                {
                    MessageBox.Show(Access.ExplaindError());
                }


                Condition conditionA = new Condition("idac", idat);
                string qureyh = SQL_Queries.Select("animals", conditionA);
                List<Condition> conditionsA = new List<Condition>() {
                new Condition("idat",idat),new Condition("animal_name",namea)

                };
                string qureyh2 = SQL_Queries.Select("treatment", conditionsA, "and");
                List<Row> teablR = Access.getObjects(qureyh);
                if (teablR != null)
                {
                    if (teablR.Count == 0)
                    {
                        MessageBox.Show("לא קיימות חיות לתצוגה ללקוח", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        Row row = teablR[0];

                        Load_HistoryA(row.GetColValue("idac").ToString());
                        AnimalDoctor.SelectedIndex = 0;

                    }
                }
                else
                {
                    MessageBox.Show(Access.ExplaindError());
                }






                Condition conPay3 = new Condition("idclientsdrugs", idat);
                Condition conPay4 = new Condition("dated", datet);
                Condition conPay5 = new Condition("animalnamed", namea);
                List<Condition> conditionshvD = new List<Condition>() { conPay3, conPay4, conPay5 };
                string qurey_Hist = SQL_Queries.Select("treatmentdrugs", conditionshvD, "or");
                List<Row> teablTA = Access.getObjects(qurey_Hist);
                DrugsHistory.Items.Clear();
                if (teablTA != null)
                {
                    if (teablTA.Count == 0)
                    {

                        DrugsHistory.Items.Clear();
                        DrugsHistory.Items.Add("לא ניתנו תרופות באותו יום");

                    }
                    else
                    {

                        Row row = teablTA[0];
                        Load_DrugsHistory(row.GetColValue("idclientsdrugs").ToString(), CalendarHistory.SelectionRange.Start.ToShortDateString(), AnimalDoctor.Text);

                    }

                }
                else
                {
                    MessageBox.Show(Access.ExplaindError());
                }






                //הצגת החיסונים שניתנו באותו תאריך נבחר
                Condition conPay6 = new Condition("idclientsvaccines", idat);
                Condition conPay7 = new Condition("datev", datet);
                Condition conPay8 = new Condition("animalnamev", namea);
                List<Condition> conditionsVCR = new List<Condition>() { conPay6, conPay7, conPay8 };
                string qurey_His2 = SQL_Queries.Select("treatmentvaccines", conditionsVCR, "or");
                List<Row> teablhisD = Access.getObjects(qurey_His2);

                if (teablhisD != null)
                {
                    if (teablhisD.Count == 0)
                    {

                        vaccinesHistory.Items.Clear();
                        vaccinesHistory.Items.Add("לא ניתנו חיסונים באותו יום");

                    }
                    else
                    {
                        Row row = teablhisD[0];

                        Load_VaccinesHistory(row.GetColValue("idclientsvaccines").ToString(), CalendarHistory.SelectionRange.Start.ToShortDateString(), AnimalDoctor.Text);
                    }
                }
                else
                {
                    MessageBox.Show(Access.ExplaindError());
                }

            }
        }

        //פונקציה כפתור לאיחסון כל נתוני הטיפול שהחיה עברה באותו יום
        private void InserTreatment_Click(object sender, EventArgs e)
        {
            List<object> values = new List<object>();


            values.Add(ClientBoxs.Text);
            values.Add(AnimalDoctor.Text);
            values.Add(DateTodayAnimals.Text);
            values.Add(DoctorBoxA.Text);
            values.Add(heatboxA.Text);
            values.Add(pulseBoxA.Text);
            values.Add(weightlb.Text);
            values.Add(problamBoxA.Text);
            values.Add(reaportBox.Text);
            lastindexA += 1;
            values.Add(lastindexA);



            string qurey = SQL_Queries.Insert("treatment", values);



            if (validationtreatment())
            {
                if (double.Parse(heatboxA.Text) <= 0|| double.Parse(pulseBoxA.Text) <= 0)
                {
                    MessageBox.Show("!נתון חום או דופק לא תקין", "!שגיאה ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (Access.Execute(qurey))
                    {


                        MessageBox.Show("!נתוני הביקור הוזנו בהצלחה במערכת", "ביקור", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        AnimalDoctor.Items.Clear();
                        AnimalDoctor.Text = "";
                        ClientBoxs.Text = "";
                        DateTodayAnimals.Text = "";
                        DoctorBoxA.Text = "";
                        heatboxA.Text = "";
                        pulseBoxA.Text = "";
                        problamBoxA.Text = "";
                        reaportBox.Text = "";

                        Agelb.Text = "";
                        typelb.Text = "";
                        sexlb.Text = "";
                        Colorlb.Text = "";
                        castratedlb.Text = "";
                        Omlb.Text = "";
                        Datebornlb.Text = "";
                        weightlb.Text = "";
                        Descriptx.Text = "";
                        datelastlb.Text = "";

                    }
                    else
                    {
                        MessageBox.Show("שגיאת נתונים", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("נא להזין נתונים חסרים", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        
        private void DateTodayAnimals_ValueChanged(object sender, EventArgs e)
        {
            DateTodayAnimals.Value = DateTime.Now;
        }

        //כפתור עדכון משקל לחיה
        private void UpdateW_Click(object sender, EventArgs e)
        {

            try
            {
                List<Col> list = new List<Col>();
                list.Add(new Col("weight", UpdateboxW.Text + " " + UpdateComboW.Text));
                Condition con1 = new Condition("trcode", animals_list[AnimalDoctor.SelectedIndex].GetColValue(0).ToString());
                List<Condition> conditions = new List<Condition>() { con1 };
                string qurey = SQL_Queries.Update("animals", list, conditions, "or");

                if (double.Parse(UpdateboxW.Text) <= 0)
                {
                    MessageBox.Show("!נתון משקל לא תקין", "!שגיאה ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (Access.Execute(qurey))
                    {
                        MessageBox.Show("!משקל עודכן בהצלחה", "עדכון משקל ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        weightlb.Text = UpdateboxW.Text + " " + UpdateComboW.Text;
                        UpdateboxW.Text = "";
                        UpdateComboW.Text = "";
                        SerchClientA_Click(null, null);


                    }
                    else
                    {
                        MessageBox.Show(Access.ExplaindError());
                    }
                }
            }
            catch {

                MessageBox.Show("!נא להזין נתונים", "עדכון משקל ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
    
}

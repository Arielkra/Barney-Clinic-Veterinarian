using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net.Security;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using Font = iTextSharp.text.Font;


namespace Management_of_a_veterinary_clinic
{
    public partial class SecretaryMenu : Form

    {
        List<Row> drugs_Box, Vanices_BOX;
        List<List<object>> animalsLis = new List<List<object>>();
        List<Attachment> attachments = new List<Attachment>();
        List<Row> clientsc;
        List<int> Prices = new List<int>(){150};
        int lastindex;
        int lastindexA;
        int lastindexpay;
        int sum_drugs=0,sum_vacciness=0,TotalGV=0;
        List<Row> clients, animals_list,Drugs;
        List<Row> Pay;
        List<Row> DrugsCombo, vaccinesCombo;
        List<Row> queues, animals_listA;
        List<Row> animalsListA;
        List<Row> opintments = new List<Row>();
        List<Row> treatments;
        List<Col> row = new List<Col>();
        List<string> hours = new List<string>() { "9:00", "9:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:30", "14:00", "14:30", "15:00", "15:30", "16:00", "16:30" };
        private List<string> fields = new List<string>() { "namep", "namef", "phone", "address", "animalname", "email" };
        List<Col> row2 = new List<Col>();
        private List<string> fields2 = new List<string>() {"animalclient","date1", "time1", "doctor" };

        List<Col> rowUpdateAnimala = new List<Col>();
        private List<string> fields3 = new List<string>() { "namea", "taypa", "age", "dateborn", "weight", "om", "description1", "colora", "sex", "castrated" };


        public SecretaryMenu()
        {
            InitializeComponent();
            this.dateBornP.Value = DateTime.Now;
        }
        //בתוך כל פונקציה בוליאנית של  בדיקה , בודקת האם הטקסט ריק ואם אין בחירה ואם לא אז לא ממשיכה לפעולות - Validaion
        private bool validation()
        {
            if (idbox.Text == "" || firstbox.Text == "")
                return false;
            else if (lastbox.Text == "" || addressbox.Text == "" || phonebox.Text == "" || mailboxbx.Text == "")
                return false;


            else return true;
        }
        private bool validation2()
        {
            if (infobox.Text == "" || dateTimePicker1.Text == "" || timebox.Text == "" || doctorbox.Text == "" || label27.Text == ""||AnimalCombo.Text=="")
                return false;



            else return true;
        }
        private bool validation3()
        {
            if (dateTimePicker1.Text == "" || timebox.Text == "" || doctorbox.Text == "")
                return false;



            else return true;
        }
        private bool validationanimal()
        {
            if (colortx.Text == "" || weightBoxd.Text == "" || animalnamebx.Text == "" || typeanimalbx.Text == "" || ageanimalbx.Text == ""
                || dateBornP.Text == "" || weightbx.Text == "" || (originalb.Checked == false && mixb.Checked == false || malebox.Checked == false && famlebox.Checked == false
                || castratedFBox.Checked == false && castratedMBox.Checked == false && NotcastrateFBox.Checked == false))

                return false;



            else return true;
        }
        private bool validation_insart_animal()
        {
            if (color_animal.Text == "" || wight.Text == "" || animal_name2.Text == "" || type_animal.Text == "" || age_animal.Text == ""
                ||date_animal.Text == "" || Wbox.Text == "" || (OrigiCheck.Checked == false && MixCheack.Checked == false || MaleChekB.Checked == false && FamleChekB.Checked == false
                || CFcheck.Checked == false && Ccheck.Checked == false && NoCcheck.Checked == false))

                return false;



            else return true;
        }

        private bool validationPay()
        {
            if (Searchidptxt.Text==""|| AnimalPaylist.Text=="")

                return false;



            else return true;
        }


        //כפתור סגירה וחזרה לתפריט ראשי
        private void closebt_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("?האם אתה בטוח שאתה רוצה לצאת", "!יציאה ", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (dr == DialogResult.OK)
            {
                this.Close();



            }
        }
        //כפתור איסחון פרטי לקוח חדש
        private void Insertclient_Click(object sender, EventArgs e)
        {

            List<object> values = new List<object>();

            values.Add(idbox.Text);
            values.Add(firstbox.Text);
            values.Add(lastbox.Text);
            values.Add(phonebox.Text);
            values.Add(addressbox.Text);
            values.Add(mailboxbx.Text);
            string qurey = SQL_Queries.Insert("client", values);

            //תנאים בודקים תקינות של פרטי לקוח
            if (Access_Actions.CheckIDNo(idbox.Text) == false)
            {

                MessageBox.Show("ת.ז לא חוקית", "!שגיאה ", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else if (Access_Actions.CheckTel(phonebox.Text) == false)
            {
                MessageBox.Show("מספר טלפון לא תקין", "!שגיאה ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (Access_Actions.CheckMail(mailboxbx.Text) == false)
            {
                MessageBox.Show("כתובת דוא'ל לא תקין", "!שגיאה ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (validation())
                {
                    if (Access.Execute(qurey))//בדיקת תקינות שאילתה
                    {
                        MessageBox.Show("!לקוח נוסף בהצלחה למערכת", "הוספת לקוח ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        idbox.Text = "";
                        firstbox.Text = "";
                        lastbox.Text = "";
                        addressbox.Text = "";
                        phonebox.Text = "";
                        mailboxbx.Text = "";
                        SecretaryMenu_Load(null, null);
                    }
                    else
                    {
                        MessageBox.Show("!ת.ז או מספר טלפון קיימים במערכת", "!שגיאה ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                    MessageBox.Show("!נא להזין בבקשה נתוני לקוח", "!שגיאה ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }




        }
        //כפתור חיפוש פרטי לקוח
        private void selectclient_Click(object sender, EventArgs e)
        {
            string idc = idbox2.Text;
            string phone = phonebox2.Text;

            if (idbox2.Text == "" && phonebox2.Text == "")
            {
                MessageBox.Show("הכנס אחד הפרטים", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {

                Condition condition = new Condition("idc", idc);
                Condition condition2 = new Condition("phone", phone);
                List<Condition> conditions = new List<Condition>() { condition, condition2 };
                string qurey = SQL_Queries.Select("client", conditions, "or");
                List<Row> teabl = Access.getObjects(qurey);

                if (teabl != null)
                {
                    if (teabl.Count == 0)
                    {
                        MessageBox.Show("לקוח לא נמצא במערכת", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        Row row = teabl[0];
                        desplayid.Text = row.GetColValue("idc").ToString();
                        desplayname.Text = row.GetColValue("namep").ToString();
                        desplayfn.Text = row.GetColValue("namef").ToString();
                        desplayphone.Text = row.GetColValue("phone").ToString();
                        desplayaddress.Text = row.GetColValue("address").ToString();
                        displayMail.Text = row.GetColValue("email").ToString();
                        Load_Animals(row.GetColValue("idc").ToString());
                    }
                }
                else
                {
                    MessageBox.Show(Access.ExplaindError());
                }

            }

        }

        //פונקציה שמקבלת מחרוזת של ת.ז מחפש לפי ת.ז של הלקוח את רשימת החיות ובסוף מציגה את כל רשימת החיות של הלקוח
        private void Load_Animals(string owner)
        {
            string queryAN = SQL_Queries.Select("animals", new Condition("idac", owner));
            animals_list = Access.getObjects(queryAN);
            if (animals_list != null)
            {
                animal_list2.Items.Clear();
                foreach (Row row in animals_list)
                    animal_list2.Items.Add(row.GetColValue("namea").ToString());
            }
            else
                animals_list = new List<Row>();

        }
        private void tabPage2_Click(object sender, EventArgs e)
        {
            //לא בשימוש 
        }
        //כפתור ניקוי פרטים
        private void cleanbt_Click(object sender, EventArgs e)
        {
            animal_list2.Items.Clear();
            textBox1.Text = "";
            idbox2.Text = "";
            desplayaddress.Text = "";
            desplayfn.Text = "";
            desplayname.Text = "";
            desplayid.Text = "";
            desplayphone.Text = "";
            displayMail.Text = "";
        }
        //כפתור יציאה 
        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("?האם אתה בטוח שאתה רוצה לצאת", "!יציאה ", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (dr == DialogResult.OK)
            {
                this.Close();



            }
        }

        private void label6_Click(object sender, EventArgs e)
        {
            //לא בשימוש
        }

        //פונקציה שמקבלת מחרוזת של ת.ז ומחזירה ת.ז של הלקוח אחרי שמשווה את הפרטים של הת.ז
        private Row selectClient(string id)
        {
            foreach (Row row in clients)
            {
                if (row.GetColValue("idc").ToString() == id)
                    return row;

            }
            return null;
        }
        private FileInfo[] files;
        private DirectoryInfo directory;

        private void SecretaryMenu_Load(object sender, EventArgs e)
        {
            //מציג בתוך הרשימה באופן אוטומטי את דמי הביקור שנגבים באופן אוטומטי
            TodayPayLB.Text = DateTime.Now.ToString("dd/MM/yyyy");//מציג תאריך של היום
            invoicepayList.Items.Clear();
            invoicepayList.Items.Add("דמי ביקור : 150"+" "+@"ש""ח");
            TotalPaylb.Text = "150";
            Random random = new Random();
            int randomNumber = random.Next(0, 10000);
            OrderNum.Text = randomNumber.ToString();//מספרי קבלות באופן אקראי
             




            //פונקציה שמציגה את רשימת התרופות ומחזירה את השמות שלהם
            string queryPayDrugs = SQL_Queries.Select("drug");
            DrugsCombo = Access.getObjects(queryPayDrugs);
            DrugsPayBox.Items.Clear();
            if (DrugsCombo != null)
            {

                foreach (Row rowd in DrugsCombo)
                {
                    string name = rowd.GetColValue("namedr").ToString();
                    DrugsPayBox.Items.Add(name);


                }

            }
            else
            {
                MessageBox.Show(Access.ExplaindError());
            }

            //פונקציה שמציגה את רשימת החיסונים ומחזירה את השמות שלהם
            string queryPayvaccines = SQL_Queries.Select("vaccines");
            vaccinesCombo = Access.getObjects(queryPayvaccines);
            VaccinesPayBox.Items.Clear();
            if (vaccinesCombo != null)
            {

                foreach (Row rowd in vaccinesCombo)
                {
                    string name = rowd.GetColValue("namev").ToString();
                    VaccinesPayBox.Items.Add(name);


                }

            }
            else
            {
                MessageBox.Show(Access.ExplaindError());
            }




            //מילוי הטיפול התרופתי לפי החיה שמקבלת הטיפול באותו יום
            string queryCAPay = SQL_Queries.Select("treatmentdrugs");
            Drugs = Access.getObjects(queryCAPay);
            if (Drugs != null)
            {

                foreach (Row row in Drugs)
                {
                    row.GetColValue("iddruds").ToString();

                }
            }
            else
            {
                MessageBox.Show(Access.ExplaindError());
            }


            //מילוי התשלומים לפי הפרטי לקוח והחיה שנבחרו 
            string querypi = SQL_Queries.Select("paylist");
            Pay = Access.getObjects(querypi);
            if (Pay != null)
            {

                foreach (Row row in Pay)
                {
                    lastindexpay = int.Parse(row.GetColValue("iddpay").ToString());

                }
            }
            else
            {
                MessageBox.Show(Access.ExplaindError());
            }



            //מילוי שמות החיות שנוספות ללקוחות
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



            //הצגת תורים ללקוח לפי החיות תאריכים וטבלאות שקשורות
            Todaylabal.Text = monthCalendar1.SelectionRange.Start.ToLongDateString();
            string queryc = SQL_Queries.Select("client");
            clients = Access.getObjects(queryc);
            string query5 = SQL_Queries.Select("queues");
            List<Row> table = Access.getObjects(query5);
            datetimelist.Items.Clear();
            clientlist.Items.Clear();
            if (table != null)
            {
                opintments.Clear();
                foreach (Row r in table)
                {
                    Row client = selectClient(r.GetColValue("idclient").ToString());
                    if (monthCalendar1.SelectionRange.Start.ToLongDateString() == r.GetColValue("date1").ToString())
                    {
                        opintments.Add(r);
                        clientlist.Items.Add("שם מלא : " + client.GetColValue("namep") + " " + client.GetColValue("namef") + " " + @" | מטפל\ת : " + r.GetColValue("doctor"));
                        datetimelist.Items.Add("שם החיה : " + r.GetColValue("animalclient") + " " + "|" +" "+"בתאריך : " + r.GetColValue("date1") + " " + " | בשעה : " + r.GetColValue("time1"));
                        clientlist.Items.Add("\n");
                        datetimelist.Items.Add("\n");
                    }


                }

            }
            else
            {
                MessageBox.Show(Access.ExplaindError());
            }




            //מציג פרטי לקוח בזימון תורים
            info2box.Text = "";
            info2box.Items.Clear();
            string queryTreament = SQL_Queries.Select("queues");
            treatments = Access.getObjects(queryTreament);
            string querymail = SQL_Queries.Select("client");
            clients = Access.getObjects(querymail);

            if (clients != null)
            {

                foreach (Row row in clients)
                {
                    string name = row.GetColValue("namep").ToString() + " " + row.GetColValue("namef").ToString();
                    info2box.Items.Add(name);
                }
            }
            else
            {
                MessageBox.Show(Access.ExplaindError());
            }

         
            //הצגת תורים של הלקוחות בתמונה כללית
            dateTimePicker1.Text = "";
            lastindex = 0;
            infobox.Text = "";
            infobox.Items.Clear();
            string query = SQL_Queries.Select("client");
            clients = Access.getObjects(query);
            if (clients != null)
            {

                foreach (Row row in clients)
                {
                    string name = row.GetColValue("namep").ToString() + " " + row.GetColValue("namef").ToString() + " -טל : " + "   " + row.GetColValue("phone").ToString();
                    infobox.Items.Add(name);


                }

            }
            else
            {
                MessageBox.Show(Access.ExplaindError());
            }

            //Queues מילוי התורים של הלוחות
            query = SQL_Queries.Select("queues");
            queues = Access.getObjects(query);
            if (queues != null)
            {

                foreach (Row row in queues)
                {
                    lastindex = int.Parse(row.GetColValue("idcode").ToString());

                }
            }
            else
            {
                MessageBox.Show(Access.ExplaindError());
            }
            label28.Text = lastindex.ToString();

            //בזימון תורים שיציג לנו את רשימת הרופאים
            string job = doctorbox.Text;
            doctorbox.Text = "";
            doctorbox.Items.Clear();
            Condition conditions3 = new Condition("job", @"רופא\ה");
            string query2 = SQL_Queries.Select("login1", conditions3);
            clientsc = Access.getObjects(query2);
            if (clientsc != null)
            {

                foreach (Row row in clientsc)
                {
                    string name = row.GetColValue("usernameheb").ToString() + " " + row.GetColValue("job").ToString();
                    doctorbox.Items.Add(name);
                }
            }
            else
            {
                MessageBox.Show(Access.ExplaindError());
            }


        }

        //פונקציה שמטפלת בעדכון פרטי לקוח
        private void addOrUpdate(Col data)
        {
            bool flag = true;
            if (data.GetValue().ToString() == "")
            {
                foreach (Col col in row)
                {
                    if (data.GetField() == col.GetField())
                    {
                        row.Remove(col);
                        break;
                    }
                }
                return;
            }
            foreach (Col col in row)
            {
                if (data.GetField() == col.GetField())
                {
                    col.Set(data);
                    flag = false;
                }
            }
            if (flag)
                row.Add(data);
        }
        private void ComboDeatiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            //לא פעיל
        }

        //עדכון פרטי לקוח
        private void button2_Click(object sender, EventArgs e)
        {
            string query = SQL_Queries.Update("client", row, new Condition("idc", desplayid.Text));
            if (ComboDeatiles.Text == "מס' פלאפון")
            {
                if (Access_Actions.CheckTel(textBox1.Text) == false)
                {
                    MessageBox.Show("מספר טלפון לא תקין", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (Access.Execute(query))
                    {

                        MessageBox.Show("פרטי לקוח עודכנו בהצלחה", "הודעה", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        textBox1.Text = "";
                        SecretaryMenu_Load(null, null);
                        selectclient_Click(null, null);
                    }
                    else
                        MessageBox.Show("העדכון כבר בוצע", "הודעה", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (ComboDeatiles.Text == "דוא'ל")
            {
                if (Access_Actions.CheckMail(textBox1.Text) == false)
                {
                    MessageBox.Show("כתובת דוא'ל לא תקינה", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (Access.Execute(query))
                    {

                        MessageBox.Show("פרטי לקוח עודכנו בהצלחה", "הודעה", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        textBox1.Text = "";
                        SecretaryMenu_Load(null, null);
                        selectclient_Click(null, null);
                    }
                    else
                        MessageBox.Show("העדכון כבר בוצע", "הודעה", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                if (Access.Execute(query))
                {

                    MessageBox.Show("פרטי לקוח עודכנו בהצלחה", "הודעה", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SecretaryMenu_Load(null, null);
                    selectclient_Click(null, null);
                }
                else
                    MessageBox.Show("העדכון כבר בוצע", "הודעה", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            row.Clear();
        }

        //עדכון פרטי לקוח
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                addOrUpdate(new Col(fields[ComboDeatiles.SelectedIndex], textBox1.Text));
            }
            catch
            {
                MessageBox.Show("אנא בחר שדה לשינוי", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //כפתור מחיקה של לקוח
        private void button3_Click(object sender, EventArgs e)
        {
            Condition con = new Condition("idc", idbox2.Text);
            string qurey = SQL_Queries.Delete("client", con);
            DialogResult dr = MessageBox.Show("                        !!!זהירות\n\n הסרת לקוח תמחק אותו סופית מהמערת\n?האם בטוח כי ברצונך למחוק אותו", "!הסרת לקוח ", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            if (dr == DialogResult.OK)
            {
                if (Access.Execute(qurey))
                {
                    MessageBox.Show("לקוח הוסר בהצלחה", "הסרה", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    idbox2.Text = "";
                    desplayaddress.Text = "";
                    desplayfn.Text = "";
                    desplayname.Text = "";
                    desplayid.Text = "";
                    desplayphone.Text = "";
                    displayMail.Text = "";
                    SecretaryMenu_Load(null, null);
                }
                else
                {
                    MessageBox.Show(Access.ExplaindError());
                }
            }
        }

        //תיאום בין תאריכים של הלוח שנה לחלונית של התאריך
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            monthCalendar1.SetDate(dateTimePicker1.Value);


        }

        //פונקציה שמסננת את הזמנים בזימון תורים 
        private void filterTime(List<string> times)
        {
            timebox.Items.Clear();
            foreach (string hour in hours)
            {
                bool exisit = false;
                foreach (string opint in times)
                    if (opint == hour)
                    {
                        exisit = true;
                        break;
                    }
                if (!exisit)
                    timebox.Items.Add(hour);
            }
        }
        //פונקציה של הלוח שנה כשבוחרים תאריך זה מראה תמונה כללית של התורים  בתאריך הנבחר במקרה זה טוען תאריך של היום 
        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            
            dateTimePicker1.Value = monthCalendar1.SelectionStart;
            List<string> opitment = new List<string>();
            foreach (Row row in treatments)
            {
                if (dateTimePicker1.Value.ToLongDateString() == row.GetColValue("date1").ToString())
                {
                    opitment.Add(row.GetColValue("time1").ToString());
                }
            }
            filterTime(opitment);

            Todaylabal.Text = monthCalendar1.SelectionRange.Start.ToLongDateString();
            string querym = SQL_Queries.Select("queues");
            List<Row> table = Access.getObjects(querym);
            datetimelist.Items.Clear();
            clientlist.Items.Clear();
            if (table != null)
            {
                opintments.Clear();
                foreach (Row r in table)
                {
                    Row client = selectClient(r.GetColValue("idclient").ToString());
                    if (monthCalendar1.SelectionRange.Start.ToLongDateString() == r.GetColValue("date1").ToString())
                    {
                        opintments.Add(r);
                        clientlist.Items.Add("שם מלא : " + client.GetColValue("namep") + " " + client.GetColValue("namef") + " " + @" | מטפל\ת : " + r.GetColValue("doctor"));
                        datetimelist.Items.Add("שם החיה : " + r.GetColValue("animalclient") +" "+ "|" + " " + "בתאריך : " + r.GetColValue("date1") + " " + " | בשעה : " + r.GetColValue("time1"));
                        clientlist.Items.Add("\n");
                        datetimelist.Items.Add("\n");
                    }


                }

            }
            else
            {
                MessageBox.Show(Access.ExplaindError());
            }
        }
        //קביעת תור ללקוחות ושליחת מייל עם התור שנקבע
        private void queuesbt_Click(object sender, EventArgs e)
        {
            try
            {
                List<object> values = new List<object>();

                lastindex += 1;
                values.Add(lastindex);
                label28.Text = lastindex.ToString();
                values.Add(clients[infobox.SelectedIndex].GetColValue("idc"));
                values.Add(AnimalCombo.Text);
                values.Add(dateTimePicker1.Text.Trim());
                values.Add(timebox.Text.Trim());
                values.Add(doctorbox.Text.Trim());



                string qurey = SQL_Queries.Insert("queues", values);
                if (dateTimePicker1.Value >= DateTime.Today)
                {
                    if (validation2())
                    {

                        if (Access.Execute(qurey))
                        {
                            if (mailSender.SendMail2(new List<string>() { clients[infobox.SelectedIndex].GetColValue("email").ToString() }, "הודעת תור ללקוח-" + infobox.Text, "זוהי הודעה ל   : " + AnimalCombo.Text + " " + "| הינך מוזמן למרפאה בתאריך," + " " + dateTimePicker1.Text + " " + "| בשעה : " + timebox.Text + " " + "|" + "**" + "נא להודיע על שינוי או ביטול תור" + "**"))
                            {

                                MessageBox.Show("!נקבע תור בהצלחה ללקוח,ונשלחה הודעה לדוא'ל", "קבעת תור", MessageBoxButtons.OK, MessageBoxIcon.Information);


                                infobox.Text = "";
                                AnimalCombo.Text = "";
                                dateTimePicker1.Text = "";
                                timebox.Text = "";
                                doctorbox.Text = "";

                                SecretaryMenu_Load(null, null);

                                listBox1.Items.Clear();
                            }

                            else
                            {
                                MessageBox.Show("שגיאת נתונים", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("שגיאת נתונים", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("תאריך שגוי", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch { }
        }
        //עובר לחלונית של שליחת הודעות ללקוח
        private void button4_Click(object sender, EventArgs e)
        {
            tabControl2.SelectTab("tabPage5");
        }
        //מחיקת תור ללקוח
        private void removeqbt_Click(object sender, EventArgs e)
        {
            try
            {
                string date1 = dateTimePicker1.Text;

                Condition con = new Condition("idcode", queues[listBox1.SelectedIndex].GetColValue(0).ToString());
                
                string qurey = SQL_Queries.Delete("queues", con);


                if (Access.Execute(qurey))
                {
  
                    MessageBox.Show("התור הוסר בהצלחה", "הסרה", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    queues.RemoveAt(listBox1.SelectedIndex);
                    listBox1.Items.RemoveAt(listBox1.SelectedIndex);
                    dateTimePicker1.Text = "";
                    timebox.Text = "";
                    doctorbox.Text = "";

                }
                else
                {
                    MessageBox.Show(Access.ExplaindError());
                }


            }
            catch
            {
                MessageBox.Show("בחירה לא נכונה", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        //מציג תורים ללקוח לפי בחירת לקוח 
        private void infobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            Condition con = new Condition("idclient", clients[infobox.SelectedIndex].GetColValue("idc"));
            string query = SQL_Queries.Select("queues", con);
            List<Row> table = Access.getObjects(query);
            listBox1.Items.Clear();
            if (table != null)
            {
                opintments.Clear();
                foreach (Row r in table)
                {
                    opintments.Add(r);
                    listBox1.Items.Add(r.GetColValue("animalclient")+" "+"|"+" "+r.GetColValue("date1") + " " + "-" + r.GetColValue("time1") + " - " + r.GetColValue("doctor"));
                    
                }

            }
            else
            {
                MessageBox.Show(Access.ExplaindError());
            }

            //מציג את שם החיה לפי בחירת לקוח
            Condition con2 = new Condition("idac", clients[infobox.SelectedIndex].GetColValue("idc"));
            string queryQA = SQL_Queries.Select("animals", con2);
            List<Row> tableQA = Access.getObjects(queryQA);
            AnimalCombo.Items.Clear();
            if (tableQA != null)
            {
                opintments.Clear();
                foreach (Row r2 in tableQA)
                {
                    opintments.Add(r2);
                    AnimalCombo.Items.Add(r2.GetColValue("namea"));

                }


            }
            else
            {
                MessageBox.Show(Access.ExplaindError());
            }

        }
        //מציג את התורים של לקוחות לפי בחירה
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Row pick = opintments[listBox1.SelectedIndex];
                dateTimePicker1.Text = pick.GetColValue("date1").ToString();
                label27.Text = pick.GetColValue("idcode").ToString();


            }
            catch { }

        }
        //מעדכן תור ללקוחות תאריך שעה ורופא
        private void updateqbt_Click(object sender, EventArgs e)
        {

            try
            {
                row2.Add(new Col("date1", dateTimePicker1.Text));
                row2.Add(new Col("time1", timebox.Text));
                row2.Add(new Col("doctor", doctorbox.Text));
                Condition con = new Condition("idcode", queues[listBox1.SelectedIndex].GetColValue(0).ToString());
                List<Condition> conditions = new List<Condition>() { con };
                string queryUpdateA = SQL_Queries.Update("queues", row2, conditions, "or");

                if (validation3())
                {



                    if (Access.Execute(queryUpdateA))
                    {
                        if (mailSender.SendMail2(new List<string>() { clients[infobox.SelectedIndex].GetColValue("email").ToString() }, "הודעת עדכון תור ללקוח-" + infobox.Text, "זוהי הודעה ל   : " + AnimalCombo.Text + " " + " " + "| הינך מוזמן למרפאה בתאריך," + " " + dateTimePicker1.Text + " " + "| בשעה : " + timebox.Text + " " + "|" + "**" + "נא להודיע על שינוי או ביטול תור" + "**"))
                        {


                            MessageBox.Show("פרטי לקוח עודכנו בהצלחה ונשלחה הודעה", "הודעה", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            timebox.Text = "";
                            doctorbox.Text = "";
                            AnimalCombo.Text = "";
                            dateTimePicker1.Text = "";
                            listBox1.Items.Clear();
                            SecretaryMenu_Load(null, null);

                        }

                    }
                    else
                        MessageBox.Show(Access.ExplaindError());


                }
                else
                {
                    MessageBox.Show("בחר בבקשה את כל הנתונים", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                row2.Clear();
            }
            catch { }

        }

        private void timebox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //לא בשימוש
        }

        private void doctorbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //לא בשימוש
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {
            //לא בשימוש
        }

        //כפתור שליחת הודעות ללקוחות 
        private void sendbt_Click(object sender, EventArgs e)
        {
            if (mailSender.SendMail(new List<string>() { sendtotxt.Text }, subjecttxt.Text, bodytxt.Text, attachments))
                if (attachments.Count != 0)
                    MessageBox.Show("ההודעה נשלחה בהצלחה עם קובץ", "הודעה", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("ההודעה נשלחה בהצלחה", "הודעה", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("בדוק את הנתונים", "שגיאה", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            attachments.Clear();
            Clear_Screen();
            SecretaryMenu_Load(null, null);
        }
        private void Clear_Screen()
        {
            listFileBox.Items.Clear();
            sendtotxt.Text = ""; subjecttxt.Text = ""; bodytxt.Text = ""; info2box.Text = "";

        }
        //בחירת לקוח והצגת המייל שלו
        private void info2box_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Row row = clients[info2box.SelectedIndex];
                sendtotxt.Text = row.GetColValue("email").ToString();
            }
            catch
            {


            }
        }
        //פונקציה לבחירה קובץ במערכת שליחת מיילים במערכת זימון תורים
        private void addfilebt_Click(object sender, EventArgs e)
        {
            openFileDialog1.Multiselect = true;
            openFileDialog1.Filter = "All files (*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                foreach (string file in openFileDialog1.FileNames)
                    attachments.Add(new Attachment(file));
                foreach (string name in openFileDialog1.SafeFileNames)
                    listFileBox.Items.Add(name);
                openFileDialog1.Reset();
            }
            else
            {
                MessageBox.Show("!לא נבחרו קבצים", "הסופת קבצים");
            }
        }
        //כפתור סגירה וחזרה לתפריט הראשי
        private void closeQ_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("?האם אתה בטוח שאתה רוצה לצאת", "!יציאה ", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (dr == DialogResult.OK)
            {
                this.Close();



            }
        }

        
    //חוזר למערכת זימון תורים
       private void button10_Click(object sender, EventArgs e)
        {
            tabControl2.SelectTab("tabPage3");
        }
        //כםתור למעבר חלון שליחת אימייל
        private void SendMailbt_Click(object sender, EventArgs e)
        {
            tabControl2.SelectTab("tabPage6");
        }

        private void label7_Click(object sender, EventArgs e)
        {
            //לא בשימוש
        }

        //בודק שת.ז לא עוברת את ה9 ספרות
        private void idbox2_TextChanged(object sender, EventArgs e)
        {
            if (idbox2.Text.Length > 9)
            {

                MessageBox.Show("בבקשה נא להזין לא יותר מ-9 ספרות", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error);
                idbox2.Text = "";
            }
        }

        //פונקציה ששולחת כתובת מייל לפי בחירת ת.ז של לקוח
        private int getclientindexMail(string id)
        {
            for (int i = 0; i < clients.Count; i++)
            {
                if (clients[i].GetColValue("idc").ToString() == id)
                    return i;

            }
            MessageBox.Show("!לקוח לא נמצא במערכת", "!שגיאה ", MessageBoxButtons.OK, MessageBoxIcon.Error);

            return -1;
        }

        //פונקציה שמחפשת את כתבות מייל לפי בחירת לקוח ומציגה את הכתובת
        private void ShearchClienMail_Click(object sender, EventArgs e)
        {
                info2box.SelectedIndex = getclientindexMail(idboxMail.Text);
            
        }
        //כפתור הוספת לקוחות
        private void Insertclient_Click_1(object sender, EventArgs e)
        {
            List<object> values = new List<object>();

            values.Add(idbox.Text);
            values.Add(firstbox.Text);
            values.Add(lastbox.Text);
            values.Add(phonebox.Text);
            values.Add(addressbox.Text);
            values.Add(mailboxbx.Text);
            string qurey = SQL_Queries.Insert("client", values);
            if (Access_Actions.CheckIDNo(idbox.Text) == false)
            {

                MessageBox.Show("ת.ז לא חוקית", "!שגיאה ", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else if (Access_Actions.CheckTel(phonebox.Text) == false)
            {
                MessageBox.Show("מספר טלפון לא תקין", "!שגיאה ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (Access_Actions.CheckMail(mailboxbx.Text) == false)
            {
                MessageBox.Show("כתובת דוא'ל לא תקין", "!שגיאה ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (validation())
                {
                    if (Access.Execute(qurey))
                    {
                        MessageBox.Show("!לקוח נוסף בהצלחה למערכת", "הוספת לקוח ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        firstbox.Text = "";
                        lastbox.Text = "";
                        addressbox.Text = "";
                        phonebox.Text = "";
                        mailboxbx.Text = "";
                        SecretaryMenu_Load(null, null);
                    }
                    else
                    {
                        MessageBox.Show("!ת.ז או מספר טלפון קיימים במערכת", "!שגיאה ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                    MessageBox.Show("!נא להזין בבקשה נתוני לקוח", "!שגיאה ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }

        //פונקציה שטוענת את רשימת החיות ללקוחות
        private void Load_animals()
        {
            animal_List.Items.Clear();
            foreach (List<object> values in animalsLis)
            {
                animal_List.Items.Add(values[2].ToString());
            }
        }
        //פונקציה שטוענת את רשימת החיות ללקוחות
        private void Load_animals2()
        {
            animal_list2.Items.Clear();
            foreach (List<object> values in animalsLis)
            {
                animal_list2.Items.Add(values[2].ToString());
            }
        }

        //הוספת חיות ללקוח קיים במידה ויש לו חיות חדשות
        private void InsertAnimal_Click(object sender, EventArgs e)
        {

            List<object> values = new List<object>();

            try
            {
                lastindexA += 1;
                values.Add(lastindexA);
                values.Add(idbox.Text);
                values.Add(animalnamebx.Text);
                values.Add(typeanimalbx.Text);
                values.Add(ageanimalbx.Text);
                values.Add(dateBornP.Text);

                values.Add(weightbx.Text + " " + weightBoxd.Text);

                if (mixb.Checked)
                {
                    values.Add(mixb.Text);

                }
                else
                {
                    values.Add(originalb.Text);
                }
                values.Add(descriptionbx.Text);
                values.Add(colortx.Text);
                if (malebox.Checked)
                {

                    values.Add(malebox.Text);


                }

                else
                {
                    values.Add(famlebox.Text);

                }
                if (castratedFBox.Checked == true)
                {
                    values.Add(castratedFBox.Text);
                }
                else if (castratedMBox.Checked == true)
                {
                    values.Add(castratedMBox.Text);
                }
                else
                {
                    values.Add(NotcastrateFBox.Text);
                }
                

                string qurey = SQL_Queries.Insert("animals", values);

                if (validationanimal())
                {
                    if (double.Parse(weightbx.Text) <= 0)
                    {
                        MessageBox.Show("!נתון משקל לא תקין", "!שגיאה ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        if (Access.Execute(qurey))
                        {
                            MessageBox.Show("!חיה נוספה בהצלחה ללקוח", "הוספת חיות ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            animalsLis.Add(values);
                            animal_List.Items.Add(animalnamebx.Text);
                           
                        }
                        else
                        {
                            MessageBox.Show("!שגיאה", "!שגיאה ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        
                        weightbx.Text = "";
                        weightBoxd.Text = "";
                        castratedFBox.Checked = false;
                        castratedMBox.Checked = false;
                        NotcastrateFBox.Checked = false;
                        malebox.Checked = false;
                        famlebox.Checked = false;
                        originalb.Checked = false;
                        mixb.Checked = false;
                        animalnamebx.Text = "";
                        typeanimalbx.Text = "";
                        ageanimalbx.Text = "";
                        dateBornP.Text = "";
                        descriptionbx.Text = "";
                        colortx.Text = "";
                        SecretaryMenu_Load(null, null);

                    }
                }
                else
                    MessageBox.Show("!נא להזין בבקשה נתוני לקוח", "!שגיאה ", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            catch { }
           
        }
        //פונקציות לבחירות כמו מין ,גזע,מסורסר לא מסורס וכו... 
        private void malebox_CheckedChanged(object sender, EventArgs e)
        {
            if (malebox.Checked == true)
                famlebox.Enabled = false;
            else if (malebox.Checked == false)
                famlebox.Enabled = true;
        }

        private void famlebox_CheckedChanged(object sender, EventArgs e)
        {
            if (famlebox.Checked == true)
                malebox.Enabled = false;
            else if (famlebox.Checked == false)
                malebox.Enabled = true;
        }

        private void castratedMBox_CheckedChanged(object sender, EventArgs e)
        {
            if (castratedMBox.Checked == true)
            {
                castratedFBox.Enabled = false;
                NotcastrateFBox.Enabled = false;
            }
            else if (castratedMBox.Checked == false)
            {
                castratedFBox.Enabled = true;
                NotcastrateFBox.Enabled = true;
            }
        }

        private void castratedFBox_CheckedChanged(object sender, EventArgs e)
        {
            if (castratedFBox.Checked == true)
            {
                castratedMBox.Enabled = false;
                NotcastrateFBox.Enabled = false;
            }
            else if (castratedFBox.Checked == false)
            {
                castratedMBox.Enabled = true;
                NotcastrateFBox.Enabled = true;
            }
        }

        private void NotcastrateFBox_CheckedChanged(object sender, EventArgs e)
        {
            if (NotcastrateFBox.Checked == true)
            {
                castratedFBox.Enabled = false;
                castratedMBox.Enabled = false;
            }
            else if (NotcastrateFBox.Checked == false)
            {
                castratedFBox.Enabled = true;
                castratedMBox.Enabled = true;
            }

        }
        //הצגת כל הנתונים של חיה שבחרת בחיפוש של הקוח
        private void animal_list2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (animal_list2.SelectedIndex == -1) return;
           
            Row row = animals_list[animal_list2.SelectedIndex];
            animal_name2.Text = row.GetColValue("namea").ToString();
            type_animal.Text = row.GetColValue("taypa").ToString();
            age_animal.Text = row.GetColValue("age").ToString();
            color_animal.Text = row.GetColValue("colora").ToString();
            date_animal.Text = row.GetColValue("dateborn").ToString();
            witeLB.Text = row.GetColValue("weight").ToString();
            decription_animal.Text = row.GetColValue("description1").ToString();
            switch (row.GetColValue("sex").ToString())
            {
                case "זכר":
                    {
                        MaleChekB.Checked = true;
                        break;
                    }
                default:
                    {
                        FamleChekB.Checked = true;
                        break;
                    }
            }
            switch (row.GetColValue("om").ToString())
            {
                case "גזעי":
                    {
                        OrigiCheck.Checked = true;
                        break;
                    }
                default:
                    {
                        MixCheack.Checked = true;
                        break;
                    }
            }
            switch (row.GetColValue("castrated").ToString())
            {
                case "מעוקרת":
                    {
                        CFcheck.Checked = true;
                        break;
                    }
                case "מסורס":
                    {
                        Ccheck.Checked = true;
                        break;
                    }
                default:
                    {
                        NoCcheck.Checked = true;
                        break;
                    }
            }
        }
        //פונקציות לבחירות כמו מין ,גזע,מסורסר לא מסורס וכו... 
        private void MaleChekB_CheckedChanged(object sender, EventArgs e)
        {

            if (MaleChekB.Checked == true)
            {
                MaleChekB.Enabled = true;
                FamleChekB.Checked = false;
                FamleChekB.Enabled = false;
            }
            else { 
                FamleChekB.Enabled = true;
            }
        }

        
        private void FamleChekB_CheckedChanged(object sender, EventArgs e)
        {
            if (FamleChekB.Checked == true)
            {
                FamleChekB.Enabled = true;
                MaleChekB.Checked = false;
                MaleChekB.Enabled = false;
            }
            else
            {
                MaleChekB.Enabled = true;
            }

        }
       
        private void Ccheck_CheckedChanged(object sender, EventArgs e)
        {
            if (Ccheck.Checked == true)
            {
                Ccheck.Enabled = true;

                CFcheck.Checked = false;
                CFcheck.Enabled = false;

                NoCcheck.Checked = false;
                NoCcheck.Enabled = false;
            }
            else
            {
                CFcheck.Enabled = true;
                NoCcheck.Enabled = true;
            }
        }

        private void CFcheck_CheckedChanged(object sender, EventArgs e)
        {
            if (CFcheck.Checked == true)
            {
                CFcheck.Enabled = true;

                Ccheck.Checked = false;
                Ccheck.Enabled = false;

                NoCcheck.Checked = false;
                NoCcheck.Enabled = false;
            }
            else
            {
                Ccheck.Enabled = true;
                NoCcheck.Enabled = true;
            }
        }

        private void NoCcheck_CheckedChanged(object sender, EventArgs e)
        {
            if (NoCcheck.Checked == true)
            {
                NoCcheck.Enabled = true;

                Ccheck.Checked = false;
                Ccheck.Enabled = false;

                CFcheck.Checked = false;
                CFcheck.Enabled = false;
            }
            else
            {
                Ccheck.Enabled = true;
                CFcheck.Enabled = true;
            }

        }
        //פונקציה כםתור למחיקת חיה שנבחרה ללקוח
        private void delete_animal2_Click(object sender, EventArgs e)
        {

            try
            {
                Condition con = new Condition("trcode", animals_list[animal_list2.SelectedIndex].GetColValue(0).ToString());
                string qureyANA = SQL_Queries.Delete("animals", con);
                DialogResult dr = MessageBox.Show("                        !!!זהירות\n\n הסרת חיה תמחק אותו סופית מהמערת\n?האם בטוח כי ברצונך למחוק אותו", "!הסרת חיה ", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                if (dr == DialogResult.OK)
                {
                    if (Access.Execute(qureyANA))
                    {
                        MessageBox.Show("חיה הוסרה", "הסרה", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        animals_list.RemoveAt(animal_list2.SelectedIndex);
                        animal_list2.Items.RemoveAt(animal_list2.SelectedIndex);
                        Cleanbt5_Click(null, null);
                    }
                    else
                    {
                        MessageBox.Show(Access.ExplaindError());
                    }
                }
            }
            catch { }
        }
        //פונקציה לאחיסון חיות חדשות ללקוח לפי פרמטרים
        private void AddAnimals_Click(object sender, EventArgs e)
        {
            List<object> values = new List<object>();

            try
            {
                lastindexA += 1;
                values.Add(lastindexA);
                values.Add(idbox2.Text);
                values.Add(animal_name2.Text);
                values.Add(type_animal.Text);
                values.Add(age_animal.Text);
                values.Add(date_animal.Text);

                values.Add(wight.Text + " " + Wbox.Text);

                if (MixCheack.Checked)
                {
                    values.Add(MixCheack.Text);

                }
                else
                {
                    values.Add(OrigiCheck.Text);
                }
                values.Add(decription_animal.Text);
                values.Add(color_animal.Text);
                if (MaleChekB.Checked)
                {

                    values.Add(MaleChekB.Text);


                }

                else
                {
                    values.Add(FamleChekB.Text);

                }
                if (CFcheck.Checked == true)
                {
                    values.Add(CFcheck.Text);
                }
                else if (Ccheck.Checked == true)
                {
                    values.Add(Ccheck.Text);
                }
                else
                {
                    values.Add(NoCcheck.Text);
                }

                string qureyInsertAnimal = SQL_Queries.Insert("animals", values);

                if ( validation_insart_animal())
                {
                    if (double.Parse(wight.Text) <= 0|| double.Parse(age_animal.Text) <= 0)
                    {
                        MessageBox.Show("!נתון משקל או גיל לא תקין", "!שגיאה ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        if (Access.Execute(qureyInsertAnimal))
                        {
                            MessageBox.Show("!חיה נוספה בהצלחה ללקוח", "הוספת חיות ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            selectclient_Click(null, null);
                           
                        }
                        else
                        {
                            MessageBox.Show("!שגיאה", "!שגיאה ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        wight.Text = "";
                        Wbox.Text = "";
                        CFcheck.Checked = false;
                        Ccheck.Checked = false;
                        NoCcheck.Checked = false;
                        MaleChekB.Checked = false;
                        FamleChekB.Checked = false;
                        OrigiCheck.Checked = false;
                        MixCheack.Checked = false;
                        animal_name2.Text = "";
                        type_animal.Text = "";
                        age_animal.Text = "";
                        date_animal.Text = "";
                        decription_animal.Text = "";
                        color_animal.Text = "";
                        

                    }
                }
                else
                    MessageBox.Show("!נא להזין בבקשה נתוני לקוח", "!שגיאה ", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            catch { }
            }
        //פונקציה לעדכון נתונים של החיות 
        private void update_animal2_Click(object sender, EventArgs e)
        {
            try
            {
                bool flag = false;
                rowUpdateAnimala.Add(new Col("namea", animal_name2.Text));
                rowUpdateAnimala.Add(new Col("taypa", type_animal.Text));
                if (int.Parse(age_animal.Text) <= 0)
                {
                    flag = true;

                }
                else
                {
                    rowUpdateAnimala.Add(new Col("age", age_animal.Text));
                }
                rowUpdateAnimala.Add(new Col("dateborn", date_animal.Text));
                if (wight.Text == "" && Wbox.Text == "")
                {

                    rowUpdateAnimala.Add(new Col("weight", witeLB.Text));

                }
                else
                {

                    if (int.Parse(wight.Text) <= 0)
                    {
                        flag = true;

                    }
                    else
                    {
                        rowUpdateAnimala.Add(new Col("weight", wight.Text + " " + Wbox.Text));
                    }


                }
                if (MixCheack.Checked)
                {
                    rowUpdateAnimala.Add(new Col("om", MixCheack.Text));

                }
                else
                {
                    rowUpdateAnimala.Add(new Col("om", OrigiCheck.Text));
                }
                rowUpdateAnimala.Add(new Col("description1", decription_animal.Text));
                rowUpdateAnimala.Add(new Col("colora", color_animal.Text));

                if (MaleChekB.Checked)
                {

                    rowUpdateAnimala.Add(new Col("sex", MaleChekB.Text));


                }

                else
                {
                    rowUpdateAnimala.Add(new Col("sex", FamleChekB.Text));

                }
                if (CFcheck.Checked == true)
                {
                    rowUpdateAnimala.Add(new Col("castrated", CFcheck.Text));
                }
                else if (Ccheck.Checked == true)
                {
                    rowUpdateAnimala.Add(new Col("castrated", Ccheck.Text));
                }
                else
                {
                    rowUpdateAnimala.Add(new Col("castrated", NoCcheck.Text));
                }

                Condition con = new Condition("trcode", animals_list[animal_list2.SelectedIndex].GetColValue(0).ToString());
                List<Condition> conditions = new List<Condition>() { con };
                string queryUpdate = SQL_Queries.Update("animals", rowUpdateAnimala, conditions, "or");


                if (flag)
                {
                    MessageBox.Show("!נא להזין גיל או משקל תקין", "!שגיאה ", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                else
                {
                    if (Access.Execute(queryUpdate))
                    {
                        MessageBox.Show("פרטי חיה עודכנו בהצלחה", "הודעה", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        selectclient_Click(null, null);

                    }
                    else
                    {
                        MessageBox.Show(Access.ExplaindError());
                    }
                }
                rowUpdateAnimala.Clear();
            }
            catch { }

        }
        //פונקציה שטוענת את רשימת החיות לפי בחירה של הלקוח
        private void Load_PayAnimals(string owner)
        {
            string queryANP = SQL_Queries.Select("animals", new Condition("idac", owner));
            animals_list = Access.getObjects(queryANP);
            if (animals_list != null)
            {
                AnimalPaylist.Items.Clear();
                foreach (Row row in animals_list)
                    AnimalPaylist.Items.Add(row.GetColValue("namea").ToString());
            }
            else
                animals_list = new List<Row>();

        }
        //פונקציה שטוענת את קובץ התשלום של הלקוח לפי ת.ז תאריך ושם החיה
        private void Load_Paypdf(string owner, string datepay,string namepay )
        {
            Condition condp1 = new Condition("idclientpay", owner);
            Condition condp2 = new Condition("datepay", datepay);
            Condition condp3 = new Condition("animalpay", namepay);
            List<Condition> conditions = new List<Condition>() { condp1, condp2, condp3 };
            string querypN = SQL_Queries.Select("paylist", conditions, "and");
          
            Pay = Access.getObjects(querypN);

            if (Pay != null)
            {

                listPaypdf.Items.Clear();
                foreach (Row row in Pay)
                {
                   
                    listPaypdf.Items.Add(row.GetColValue("pdfname").ToString());
                    
                }
            }
            else
                Pay = new List<Row>();
            

        }
        //מציג את רשימת החיסונים של הלקוח שקיבל באותו יום
        private void VaccinesPayBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Condition con = new Condition("namev", vaccinesCombo[VaccinesPayBox.SelectedIndex].GetColValue("namev"));
            int.Parse(vaccinesCombo[VaccinesPayBox.SelectedIndex].GetColValue("pricev").ToString());
            Prices.Add(int.Parse(vaccinesCombo[VaccinesPayBox.SelectedIndex].GetColValue("pricev").ToString()));
            Total();
            string queryPatlist2 = SQL_Queries.Select("vaccines", con);
            List<Row> table = Access.getObjects(queryPatlist2);
            if (table != null)
            {
                opintments.Clear();
                foreach (Row r in table)
                {
                    opintments.Add(r);
                    invoicepayList.Items.Add(r.GetColValue("namev") + " " + "|" + " " + r.GetColValue("pricev") + " " + @"ש""ח");
                   
                    
                }


            }
            else
            {
                MessageBox.Show(Access.ExplaindError());
            }
        }
        //חיפוש לקוח בדף התשלומים
        private void Searchpaytxt_Click(object sender, EventArgs e)
        {
            string idc = Searchidptxt.Text;

                Condition condition = new Condition("idc", idc);
                List<Condition> conditions = new List<Condition>() { condition };
                string qureyPay = SQL_Queries.Select("client", condition);
                List<Row> teabl = Access.getObjects(qureyPay);

                if (teabl != null)
                {
                    if (teabl.Count == 0)
                    {
                        MessageBox.Show("לקוח לא נמצא במערכת", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        Row row = teabl[0];
                    NameClienLB.Text = row.GetColValue("namep").ToString() + " " + row.GetColValue("namef").ToString();
                    Load_PayAnimals(row.GetColValue("idc").ToString());
                    }
                }
                else
                {
                    MessageBox.Show(Access.ExplaindError());
                }

        }

        private void invoicepayList_SelectedIndexChanged(object sender, EventArgs e)
        {
           //לא בשימוש
        }

        //יצירת קובץ תשלומים ללקוח בקובץ PDF
        private void EndPayBt_Click(object sender, EventArgs e)
        {
            if (validationPay())
            {
                Document Doc = new Document(PageSize.A4, 25, 25, 30, 30);

                using (System.IO.FileStream fs = new FileStream(@"pdf\" + OrderNum.Text + "-"+DateTime.Now.ToString("dd.MM.yyyy") + "-"+Searchidptxt.Text + "-" + NameClienLB.Text + "-" + AnimalPaylist.Text + ".pdf", FileMode.Create, FileAccess.Write, FileShare.Read))
                {
                    PdfWriter writer = PdfWriter.GetInstance(Doc, fs);
                    Doc.Open();
                    Doc.NewPage();
                    string ARIALUNI_TFF = Path.Combine(@"", "ARIAL.TTF");
                    BaseFont bf = BaseFont.CreateFont(ARIALUNI_TFF, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                    Font f1 = new Font(bf, 12);
                    PdfPTable Table = new PdfPTable(2);
                    PdfPTable Table1 = new PdfPTable(1);


                    Table.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                    Table1.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                    Table1.DefaultCell.BorderWidth = 2;
                    //-------------------------------------------------------------------------
                    iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance("pic/Animals-Icon.png");
                    image.ScalePercent(24f);
                    Doc.Add(image);

                    PdfPCell employee = new PdfPCell(new Phrase("\nמרפאת בארני                     " + " " + "תאריך הדפסה:" + " " + DateTime.Now.ToString("dd/MM/yyyy HH:mm ") + "      מספר קבלה : " + " " + OrderNum.Text, f1))
                    {
                        ExtraParagraphSpace = 5,
                        HorizontalAlignment = 2,
                    };
                    employee.Border = 2;
                    Table1.AddCell(employee);
                    Doc.Add(Table1);
                    Table.AddCell(new Phrase("שם הלקוח", f1));
                    Table.AddCell(new Phrase("שם המוצר ומחיר", f1));
                    Table.AddCell(new Phrase(NameClienLB.Text+" | "+" "+"שם החיה : "+ AnimalPaylist.Text, f1));
                    Table.AddCell(new Phrase("", f1));
                    for (int i = 0; i < invoicepayList.Items.Count; i++)
                    {
                        Table.AddCell(new Phrase("", f1));
                        Table.AddCell(new Phrase(invoicepayList.Items[i].ToString(), f1));
                    }
                    Doc.Add(Table);

                    Table1.DeleteLastRow();
                    PdfPCell cellAmount = new PdfPCell(new Phrase("סה\"כ סכום חשבונית:" + " " + TotalPaylb.Text+" "+@"ש""ח", f1))
                    {
                        HorizontalAlignment = 2,
                        ExtraParagraphSpace = 10
                    };
                    cellAmount.Border = 2;
                    Table1.AddCell(cellAmount);
                    Doc.Add(Table1);

                    //---------------------------------------------------------
                    Doc.Close();
                    NamePdf.Text = OrderNum.Text + "-" + DateTime.Now.ToString("dd.MM.yyyy") + "-" + Searchidptxt.Text + "-" + NameClienLB.Text + "-" + AnimalPaylist.Text + ".pdf";
                    try
                    {
                        List<object> valuesPay = new List<object>();
                        lastindexpay += 1;
                        valuesPay.Add(lastindexpay);
                        valuesPay.Add(OrderNum.Text);
                        valuesPay.Add(Searchidptxt.Text);
                        valuesPay.Add(TodayPayLB.Text);
                        valuesPay.Add(AnimalPaylist.Text);
                        valuesPay.Add(NamePdf.Text);
                        string qureyPayInsert2 = SQL_Queries.Insert("paylist", valuesPay);
                        if (Access.Execute(qureyPayInsert2))
                        {
                            MessageBox.Show("!חשבונית נוצרה בהצלחה", "חשבונית ", MessageBoxButtons.OK, MessageBoxIcon.Information);


                        }
                        else
                        {
                            MessageBox.Show("!שגיאה", "!שגיאה ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch { }
                    DrugsPayBox.Text = "";
                    VaccinesPayBox.Text = "";
                    Searchidptxt.Text = "";
                    NameClienLB.Text = "";
                    InfoVDList.Items.Clear();
                    AnimalPaylist.Items.Clear();
                    invoicepayList.Items.Clear();
                    Prices.Clear();
                    Prices.Add(150);
                    Random random = new Random();
                    int randomNumber = random.Next(0, 1000000000);
                    OrderNum.Text = randomNumber.ToString();
                    SecretaryMenu_Load(null, null);
                }
                
            }
            else
            {
                MessageBox.Show("שגיאת נתונים", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            




        }
        //כפתור סגירה וחזרה לפתריט הראשי
        private void closebtPay_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("?האם אתה בטוח שאתה רוצה לצאת", "!יציאה ", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (dr == DialogResult.OK)
            {
                this.Close();



            }
        }
        //פונקציה שמוחקת מרשימת התשלומים פריט שבתטעות נבחר
        private void RemovePay_Click(object sender, EventArgs e)
        {
            try
            {
                if (invoicepayList.SelectedIndex == 0)
                {
                    return;
                }
                Prices.RemoveAt(invoicepayList.SelectedIndex);
                int Sum = 0;
                foreach (int item in Prices)
                    Sum += item;
                TotalPaylb.Text = Sum.ToString();
                invoicepayList.Items.RemoveAt(invoicepayList.SelectedIndex);
            }
            catch { }

        }
        //מציג קבצי תשלומים לפני בחירת תאריך ספציפי
        private void CalendarPay_DateChanged(object sender, DateRangeEventArgs e)
        {
            CalendarPay.MaxDate = DateTime.Today.AddDays(0);
            datePayPicker.Value = CalendarPay.SelectionStart;
            string idclientpay = Searchidptxt.Text;

            Condition condition = new Condition("idclientpay", idclientpay);
            string querypaylist = SQL_Queries.Select("paylist", condition);
            List<Row> table = Access.getObjects(querypaylist);
            if (table != null)
            {
             
                foreach (Row r in table)
                {
                    
                    if (CalendarPay.SelectionRange.Start.ToShortDateString() == r.GetColValue("datepay").ToString())
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
        //חיפוש קבלה לפי לקוח תאריך ושם החיה ספציפי
        private void searchClientPay2_Click(object sender, EventArgs e)
        {
           
            string idclientpay = Searchidptxt.Text;
            string animalpay = AnimalPaylist.Text;
            string datepay =CalendarPay.SelectionRange.Start.ToShortDateString();

             Condition conditionpay3 = new Condition("idclientpay", idclientpay);
            Condition conditionpay1 = new Condition("animalpay", animalpay);
            Condition conditionpay2 = new Condition("datepay", datepay);
            
            List<Condition> conditions = new List<Condition>() { conditionpay3, conditionpay2, conditionpay1};
            string qureyPaysc = SQL_Queries.Select("paylist", conditions,"and");
            List<Row> teablpay = Access.getObjects(qureyPaysc);
           
           
            if (teablpay != null)
            {
                if (teablpay.Count == 0)
                {
                    MessageBox.Show("אין נתוני קבלות להצגה לתאריך זה", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    
                }
                else
                {

                    try { 
                        
                    
                        Row row = teablpay[0];
                        Load_Paypdf(row.GetColValue("idclientpay").ToString(), CalendarPay.SelectionRange.Start.ToShortDateString(), AnimalPaylist.Text);
                        

                    }
                    catch {

                        MessageBox.Show("קובץ לא נמצא", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            }
            else
            {
                MessageBox.Show(Access.ExplaindError());
            }
            
        }
        //מציד תאריך בלוח שנה לפי בחירה בחלונית תאריך 
        private void datePayPicker_ValueChanged(object sender, EventArgs e)
        {
            datePayPicker.MaxDate = DateTime.Today.AddDays(0);
            CalendarPay.SetDate(datePayPicker.Value);
        }

        private void Namepdybox_TextChanged(object sender, EventArgs e)
        {
            //לא בשימוש

        }
        //כפתור ניקוי רשימת חיות אחרי שנוספו ללקוח חדש
        private void CleanListA_Click(object sender, EventArgs e)
        {
            idbox.Text = "";
            animal_List.DataSource = null;
            animal_List.Items.Clear();
        }
        //מציג רשימת קבצים ללקוח של תשלומים
        private void listPaypdf_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Row pick = opintments[listPaypdf.SelectedIndex];
                datePayPicker.Text = pick.GetColValue("datepay").ToString();
                 pick.GetColValue("iddpay").ToString();


            }
            catch { }
        }
        //כפתור בחירת קובץ אחרי שנמצא ללקוח ופתיחת קובץ תשלומים ללקוח
        private void OpenPfd_Click(object sender, EventArgs e)
        {
            if (listPaypdf.Items.Count!=0&&listPaypdf.SelectedItem!=null)
            {
                string filename = @"pdf\" + listPaypdf.Text;
                System.Diagnostics.Process.Start(filename);

            }
            else
            {
                MessageBox.Show("לא נבחר קובץ", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //כפתור ניקוי רשימת קבצים ללקוח
        private void CleanListPdf_Click(object sender, EventArgs e)
        {
            listPaypdf.Items.Clear();
        }

        //בודק שת.ז לא עוברת את ה9 ספרות
        private void idbox_TextChanged(object sender, EventArgs e)
        {
            if (idbox.Text.Length > 9)
            {

                MessageBox.Show("בבקשה נא להזין לא יותר מ-9 ספרות", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error);
                idbox.Text = "";
            }
        }

        //בודק שת.ז לא עוברת את ה9 ספרות
        private void Searchidptxt_TextChanged(object sender, EventArgs e)
        {
            if (Searchidptxt.Text.Length > 9)
            {

                MessageBox.Show("בבקשה נא להזין לא יותר מ-9 ספרות", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Searchidptxt.Text = "";
            }
        }

        //בודק שת.ז לא עוברת את ה9 ספרות
        private void idboxMail_TextChanged(object sender, EventArgs e)
        {
            if (idboxMail.Text.Length > 9)
            {

                MessageBox.Show("בבקשה נא להזין לא יותר מ-9 ספרות", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error);
                idboxMail.Text = "";
            }

        }

        //בדיקת כמות ספרות של מס טל
        private void phonebox2_TextChanged(object sender, EventArgs e)
        {
            if (phonebox2.Text.Length > 10)
            {

                MessageBox.Show("בבקשה נא להזין לא יותר מ-10 ספרות", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error);
                phonebox2.Text = "";
            }
        }

        //בדיקת כמות של ספרות של מס טל
        private void phonebox_TextChanged(object sender, EventArgs e)
        {
            if (phonebox.Text.Length > 10)
            {

                MessageBox.Show("בבקשה נא להזין לא יותר מ-10 ספרות", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error);
                phonebox.Text = "";
            }
        }

        //פונקציה טוענת רשימת תרופות ללקוח לפי חיה ותאריך
        private void Load_DrugsPay2(string owner, string datedr, string named)
        {


            Condition cond1 = new Condition("idclientsdrugs", owner);
            Condition cond2 = new Condition("dated", datedr);
            Condition cond3 = new Condition("animalnamed", named);
            List<Condition> conditions = new List<Condition>() { cond1, cond2, cond3 };
            string queryAN = SQL_Queries.Select("treatmentdrugs", conditions, "and");
         
            drugs_Box = Access.getObjects(queryAN);
            if (drugs_Box != null)
            {
               InfoVDList.Items.Clear();
               InfoVDList.Items.Add("רשימת תרופות :");
                
                foreach (Row row in drugs_Box)
                {
                    InfoVDList.Items.Add(row.GetColValue("drugsname").ToString());
                  
                }
            }
            else
                drugs_Box = new List<Row>();

        }
        //רשימת תרופות ומציגה את המחירים שלהן לאחר בחירתן לתוך הרשימה
        private void DrugsPayBox_SelectedIndexChanged(object sender, EventArgs e)
        {


            Condition con = new Condition("namedr", DrugsCombo[DrugsPayBox.SelectedIndex].GetColValue("namedr"));
            int.Parse(DrugsCombo[DrugsPayBox.SelectedIndex].GetColValue("price").ToString());
            Prices.Add(int.Parse(DrugsCombo[DrugsPayBox.SelectedIndex].GetColValue("price").ToString()));
            Total();
            string queryPatlist = SQL_Queries.Select("drug", con);
            List<Row> table = Access.getObjects(queryPatlist);
            if (table != null)
            {
               opintments.Clear();
                foreach (Row r in table)
                {
                    opintments.Add(r);
                    invoicepayList.Items.Add(r.GetColValue("namedr") + " " + "|" + " " + r.GetColValue("price")+" "+@"ש""ח");
                    
                }


            }
            else
            {
                MessageBox.Show(Access.ExplaindError());
            }
        }
        //פונקציית סיכום מחיר ללקוח
         int Total()
        {
            int Sum = 0;
            foreach (int item in Prices)
                Sum += item;
            TotalPaylb.Text = Sum.ToString();
            return Sum;
        }


        //פונקציה טוענת רשימת חיסונים ללקוח לפי חיה ותאריך
        private void Load_VaccinesPay2(string owner, string datevr, string namev)
        {


            Condition conv1 = new Condition("idclientsvaccines", owner);
            Condition conv2 = new Condition("datev", datevr);
            Condition conv3 = new Condition("animalnamev", namev);
            List<Condition> conditions = new List<Condition>() { conv1, conv2, conv3 };
            string queryAN = SQL_Queries.Select("treatmentvaccines", conditions, "and");

            Vanices_BOX = Access.getObjects(queryAN);
            
            if (Vanices_BOX != null)
            {

                InfoVDList.Items.Add("\n");
                InfoVDList.Items.Add("__________________");
                InfoVDList.Items.Add("\n");
                InfoVDList.Items.Add("רשימת חיסונים :\n");
                foreach (Row row in Vanices_BOX)
                {
                    InfoVDList.Items.Add(row.GetColValue("vaccinesname").ToString());
                    
                }
            }
            else
                Vanices_BOX = new List<Row>();

        }
        //טוען רשימת תרופות כדי לדעת מה לחייב את הלקוח 
        private void AnimalPaylist_SelectedIndexChanged(object sender, EventArgs e)
        {
            string idclientsdrugs = Searchidptxt.Text;
            string dated = TodayPayLB.Text;
            string animalnamed = AnimalPaylist.Text;

            Condition conPay3 = new Condition("idclientsdrugs", idclientsdrugs);
            Condition conPay4 = new Condition("dated", dated);
            Condition conPay5 = new Condition("animalnamed", animalnamed);
            List<Condition> conditions = new List<Condition>() { conPay3, conPay4, conPay5 };
            string qurey_Pay = SQL_Queries.Select("treatmentdrugs", conditions, "and");
            List<Row> teablPay = Access.getObjects(qurey_Pay);
            InfoVDList.Items.Clear();
            if (teablPay != null)
            {
                if (teablPay.Count == 0)
                {

                    InfoVDList.Items.Clear();
                    InfoVDList.Items.Add("לא ניתנו היום תרופות");

                }
                else
                {
                    
                    Row row = teablPay[0];
                    TodayPayLB.Text = row.GetColValue("dated").ToString();
                    AnimalPaylist.Text = row.GetColValue("animalnamed").ToString();
                    Load_DrugsPay2(row.GetColValue("idclientsdrugs").ToString(), TodayPayLB.Text, AnimalPaylist.Text);
                  
                }
                
            }
            else
            {
                MessageBox.Show(Access.ExplaindError());
            }
            
      


            //טוען את רשימת החיסונים של הלקוח כדי לדעת מה לחייב אותו באותו יום
            string idclientsvaccines = Searchidptxt.Text;
            string datev = TodayPayLB.Text;
            string animalnamev = AnimalPaylist.Text;

            Condition conPay6 = new Condition("idclientsvaccines", idclientsvaccines);
            Condition conPay7 = new Condition("datev", datev);
            Condition conPay8 = new Condition("animalnamev", animalnamev);
            List<Condition> conditions2 = new List<Condition>() { conPay6, conPay7, conPay8 };
            string qurey_Pay2 = SQL_Queries.Select("treatmentvaccines", conditions2, "and");
            List<Row> teablPay2 = Access.getObjects(qurey_Pay2);

            if (teablPay2 != null)
            {
                if (teablPay2.Count == 0)
                {

                    InfoVDList.Items.Add("\n");
                    InfoVDList.Items.Add("__________________");
                    InfoVDList.Items.Add("\n");
                    InfoVDList.Items.Add("לא ניתנו היום חיסונים");

                }
                else
                {
                    Row row9 = teablPay2[0];
                    TodayPayLB.Text = row9.GetColValue("datev").ToString();
                    AnimalPaylist.Text = row9.GetColValue("animalnamev").ToString();
                    Load_VaccinesPay2(row9.GetColValue("idclientsvaccines").ToString(), TodayPayLB.Text, AnimalPaylist.Text);
                }
            }
            else
            {
                MessageBox.Show(Access.ExplaindError());
            }




        }
        //כפתור ניקוי לכל המדדים של החיות
        private void Cleanbt5_Click(object sender, EventArgs e)
        {
            witeLB.Text="";
            wight.Text = "";
            Wbox.Text = "";
            CFcheck.Checked = false;
            Ccheck.Checked = false;
            NoCcheck.Checked = false;
            MaleChekB.Checked = false;
            FamleChekB.Checked = false;
            OrigiCheck.Checked = false;
            MixCheack.Checked = false;
            animal_name2.Text = "";
            type_animal.Text = "";
            age_animal.Text = "";
            date_animal.Text = "";
            decription_animal.Text = "";
            color_animal.Text = "";

        }
        // תאריך לידה של החיה ללא אפשרות לבחור תאריכים קדימה
        private void dateBornP_ValueChanged(object sender, EventArgs e)
        {
            
            dateBornP.MaxDate = DateTime.Today.AddDays(0);
            
        }
    }
}   
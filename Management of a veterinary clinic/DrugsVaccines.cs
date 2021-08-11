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
    public partial class DrugsVaccines : Form
    {
        DoctorMenu f1;
        List<Col> row = new List<Col>();
        List<Row> clienta, workers, drugs,drugs2, vaccines;
        List<Row> drugs_Box, Vanices_BOX;
        int lastindexD, lastindexV;
        List<List<object>> DrugsList = new List<List<object>>();
        List<List<object>> VanicesList = new List<List<object>>();
        List<Row> opintments = new List<Row>();


        //פונקציות בדיקה בוליאניות בודקות האם חסר החזנת נתונים
        private bool validationdrug()
        {
            if (DrugsCombo.Text == "" )

                return false;

            else return true;
        }

        private bool validationv()
        {
            if (VanicesCombo.Text == "")

                return false;

            else return true;
        }
        private void Load_Drugs()
        {
            drugslist.Items.Clear();
            foreach (List<object> values in DrugsList)
            {
                drugslist.Items.Add(values[4].ToString());
            }
        }
        //פונקציה טוענת רשימת חיסונים
        private void Load_Vanices()
        {
            VaccinesListbx.Items.Clear();
            foreach (List<object> values in VanicesList)
            {
                VaccinesListbx.Items.Add(values[4].ToString());
            }
        }
        //טוענת רשימת תרופות והצגתן
        private void drugslist_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                Row pick = opintments[drugslist.SelectedIndex];
                Datelb.Text= pick.GetColValue("dated").ToString();
                pick.GetColValue("iddruds").ToString();
            }
            catch { }
        }

        //פונקציה מציגה רשימת תרופות ללקוח לפי ת.ז לקוח תאריך של היום ושם החיה
        private void Load_Drugs2(string owner,string datedr, string named)
        {


            Condition cond1 = new Condition("idclientsdrugs", owner);
            Condition cond2 = new Condition("dated", datedr);
            Condition cond3 = new Condition("animalnamed", named);
            List<Condition> conditions = new List<Condition>() { cond1, cond2,cond3 };
            string queryAN = SQL_Queries.Select("treatmentdrugs", conditions,"and");

            drugs_Box = Access.getObjects(queryAN);
            if (drugs_Box != null)
            {
                drugslist.Items.Clear();
                foreach (Row row in drugs_Box)
                    drugslist.Items.Add(row.GetColValue("drugsname").ToString());
            }
            else
                drugs_Box = new List<Row>();

        }
        //הסרת חיסון לחיה אם נוספה
        private void Removev_Click(object sender, EventArgs e)
        {
            try
            {
                Condition conv = new Condition("idvaccines", Vanices_BOX[VaccinesListbx.SelectedIndex].GetColValue(0).ToString());
                string qureyrv = SQL_Queries.Delete("treatmentvaccines", conv);
                if (Access.Execute(qureyrv))
                {

                    MessageBox.Show("החיסון הוסר בהצלחה", "הסרה", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Vanices_BOX.RemoveAt(VaccinesListbx.SelectedIndex);
                    VaccinesListbx.Items.RemoveAt(VaccinesListbx.SelectedIndex);

                }
                else
                {
                    MessageBox.Show(Access.ExplaindError());
                }
            }
            catch { }
        }
        //הצגת תרופות ללקוח שניתנו באותו היום אם בטעות נסגר החלון 
        private void ShowD_Click(object sender, EventArgs e)
        {
            string idclientsdrugs = IdvLabel.Text;
            string dated = Datelb.Text;
            string animalnamed = NameAnilb.Text;

            Condition conv3 = new Condition("idclientsdrugs", idclientsdrugs);
            Condition conv4 = new Condition("dated", dated);
            Condition conv5 = new Condition("animalnamed", animalnamed);
            List<Condition> conditions = new List<Condition>() { conv3, conv4, conv5 };
            string qurey_va = SQL_Queries.Select("treatmentdrugs", conditions, "and");
            List<Row> teablv = Access.getObjects(qurey_va);
                if (teablv != null)
                {
                    if (teablv.Count == 0)
                    {
                        MessageBox.Show("לא ניתנו היום תרופות", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {

                        Row row = teablv[0];
                        Datelb.Text = row.GetColValue("dated").ToString();
                        NameAnilb.Text = row.GetColValue("animalnamed").ToString();
                        Load_Drugs2(row.GetColValue("idclientsdrugs").ToString(), Datelb.Text, NameAnilb.Text);
                    }
                }
                else
                {
                    MessageBox.Show(Access.ExplaindError());
                }
            }
        //הצגת חיסונים ללקוח שניתנו באותו היום אם בטעות נסגר החלון 
        private void ShowV_Click(object sender, EventArgs e)
        {
            string idclientsvaccines = IdvLabel.Text;
            string datev = Datelb.Text;
            string animalnamev = NameAnilb.Text;

            Condition con_v3 = new Condition("idclientsvaccines", idclientsvaccines);
            Condition con_v4 = new Condition("datev", datev);
            Condition con_v5 = new Condition("animalnamev", animalnamev);
            List<Condition> conditions_v = new List<Condition>() { con_v3, con_v4, con_v5 };
            string qurey_vea = SQL_Queries.Select("treatmentvaccines", conditions_v, "and");
            List<Row> teabl = Access.getObjects(qurey_vea);
                if (teabl != null)
                {
                    if (teabl.Count == 0)
                    {
                        MessageBox.Show("לא ניתנו חיסונים היום", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {

                        Row row = teabl[0];
                        Datelb.Text = row.GetColValue("datev").ToString();
                        NameAnilb.Text = row.GetColValue("animalnamev").ToString();
                        Load_vaccines2(row.GetColValue("idclientsvaccines").ToString(), Datelb.Text, NameAnilb.Text);
                    }
                }
                else
                {
                    MessageBox.Show(Access.ExplaindError());
                }
            }

        //כפתור סגירה וחזרה לחלון הטיפול של הרופא
        private void CloseDV_Click(object sender, EventArgs e)
        {
            Close();
        }
        //טעינה של רשימת חיסונים שניתנו ללקוח לחיה באותו זמן
        private void Load_vaccines2(string owner, string datedr, string named)
        {


            Condition cond1 = new Condition("idclientsvaccines", owner);
            Condition cond2 = new Condition("datev", datedr);
            Condition cond3 = new Condition("animalnamev", named);
            List<Condition> conditions = new List<Condition>() { cond1, cond2, cond3 };
            string queryAV = SQL_Queries.Select("treatmentvaccines", conditions, "and");

            Vanices_BOX = Access.getObjects(queryAV);
            if (Vanices_BOX != null)
            {
                VaccinesListbx.Items.Clear();
                foreach (Row row in Vanices_BOX)
                    VaccinesListbx.Items.Add(row.GetColValue("vaccinesname").ToString());
            }
            else
                Vanices_BOX = new List<Row>();

        }
        //מחיקה של תרופות מהרשימה ללקוח באותו יום
        private void Removed_Click(object sender, EventArgs e)
        {
            try
            {
                Condition con = new Condition("iddruds", drugs_Box[drugslist.SelectedIndex].GetColValue(0).ToString());
                string qureyrd = SQL_Queries.Delete("treatmentdrugs", con);
                if (Access.Execute(qureyrd))
                {

                    MessageBox.Show("התרופה הוסרה בהצלחה", "הסרה", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    drugs_Box.RemoveAt(drugslist.SelectedIndex);
                    drugslist.Items.RemoveAt(drugslist.SelectedIndex);

                }
                else
                {
                    MessageBox.Show(Access.ExplaindError());
                }
            }
            catch { }
        }
        //איחסון החיסונים ללקוח באותו יום
        private void InsertV_Click(object sender, EventArgs e)
        {
            List<object> values = new List<object>();

            try
            {
                lastindexV += 1;
                values.Add(lastindexV);
                values.Add(IdvLabel.Text);
                values.Add(Datelb.Text);
                values.Add(NameAnilb.Text);
                values.Add(VanicesCombo.Text);
                string qureydrugs = SQL_Queries.Insert("treatmentvaccines", values);

                if (validationv())
                {
                    if (Access.Execute(qureydrugs))
                    {
                        MessageBox.Show("!חיסון נוסף בהצלחה ללקוח", "הוספת חיסון ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        VanicesList.Add(values);
                        Load_Vanices();
                    }
                    else
                    {
                        MessageBox.Show(Access.ExplaindError());
                    }
                }
                else
                {
                    MessageBox.Show("לא נבחר חיסון מהרשימה", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }




            }
            catch { }


            //מציג מה ניתן בנתיים ללקוח בחיסונים
            string idclientsvaccines = IdvLabel.Text;
            string datev = Datelb.Text;
            string animalnamev = NameAnilb.Text;

            Condition con_v3 = new Condition("idclientsvaccines", idclientsvaccines);
            Condition con_v4 = new Condition("datev", datev);
            Condition con_v5 = new Condition("animalnamev", animalnamev);
            List<Condition> conditions_v = new List<Condition>() { con_v3, con_v4, con_v5 };
            string qurey_ve = SQL_Queries.Select("treatmentvaccines", conditions_v, "and");
            List<Row> teabl = Access.getObjects(qurey_ve);
            if (validationv())
            {
                if (teabl != null)
                {
                    if (teabl.Count == 0)
                    {
                        MessageBox.Show("לקוח לא נמצא במערכת", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {

                        Row row = teabl[0];
                        Datelb.Text = row.GetColValue("datev").ToString();
                        NameAnilb.Text = row.GetColValue("animalnamev").ToString();
                        Load_vaccines2(row.GetColValue("idclientsvaccines").ToString(), Datelb.Text, NameAnilb.Text);
                    }
                }
                else
                {
                    MessageBox.Show(Access.ExplaindError());
                }
            }
        }
        //איחסון של התרופות באותו יום
        private void InsertDrugs_Click(object sender, EventArgs e)
        {
            List<object> values = new List<object>();
           
            try
            {
                lastindexD += 1;
                values.Add(lastindexD);
                values.Add(IdvLabel.Text);
                values.Add(Datelb.Text);
                values.Add(NameAnilb.Text);
                values.Add(DrugsCombo.Text);
                string qureydrugs = SQL_Queries.Insert("treatmentdrugs", values);

                if (validationdrug())
                {
                    if (Access.Execute(qureydrugs))
                    {
                        MessageBox.Show("!תרופה נוספה בהצלחה ללקוח", "הוספת תרופה ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        DrugsList.Add(values);
                        Load_Drugs();
                    }
                    else
                    {
                        MessageBox.Show(Access.ExplaindError());
                    }

                }
                else
                {
                    MessageBox.Show("לא נבחרה תרופה מהרשימה", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch { }

            //מציג מה ניתן בנתיים ללקוח בתרופות
            string idclientsdrugs = IdvLabel.Text;
            string dated = Datelb.Text;
            string animalnamed = NameAnilb.Text;

            Condition con3 = new Condition("idclientsdrugs", idclientsdrugs);
            Condition con4 = new Condition("dated", dated);
            Condition con5 = new Condition("animalnamed", animalnamed);
            List<Condition> conditions = new List<Condition>() { con3, con4,con5 };
            string qurey_vact = SQL_Queries.Select("treatmentdrugs", conditions, "and");
            List<Row> teabl = Access.getObjects(qurey_vact);
            if (validationdrug())
            {
                if (teabl != null)
                {
                    if (teabl.Count == 0)
                    {
                        MessageBox.Show("לקוח לא נמצא במערכת", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {

                        Row row = teabl[0];
                        Datelb.Text = row.GetColValue("dated").ToString();
                        NameAnilb.Text = row.GetColValue("animalnamed").ToString();
                        Load_Drugs2(row.GetColValue("idclientsdrugs").ToString(), Datelb.Text, NameAnilb.Text);
                    }
                }
                else
                {
                    MessageBox.Show(Access.ExplaindError());
                }
            }


        }
        
        public DrugsVaccines(DoctorMenu frm1)
        {
            InitializeComponent();
            this.f1 = frm1;
        }
        
        private void DrugsVaccines_Load(object sender, EventArgs e)
        {
            IdvLabel.Text = f1.ClientBoxs.Text;
            NameAnilb.Text = f1.AnimalDoctor.Text;

            //טוען רשימת של התרופות ללקוח
            string queryCA = SQL_Queries.Select("treatmentdrugs");
            drugs2 = Access.getObjects(queryCA);
            if (drugs2 != null)
            {

                foreach (Row row in drugs2)
                {
                    lastindexD = int.Parse(row.GetColValue("iddruds").ToString());
                    
                }
            }
            else
            {
                MessageBox.Show(Access.ExplaindError());
            }
             lastindexD.ToString();


            //טוען רשימת של החיסונים ללקוח
            string queryV = SQL_Queries.Select("treatmentvaccines");
            vaccines = Access.getObjects(queryV);
            if (vaccines != null)
            {

                foreach (Row row in vaccines)
                {
                    lastindexV = int.Parse(row.GetColValue("idvaccines").ToString());

                }
            }
            else
            {
                MessageBox.Show(Access.ExplaindError());
            }

            //טוען רשימה של תרופות
            Datelb.Text = DateTime.Now.ToString("dd/MM/yyyy");
            string queryd = SQL_Queries.Select("drug");
            drugs = Access.getObjects(queryd);
            DrugsCombo.Items.Clear();
            if (drugs != null)
            {

                foreach (Row rowd in drugs)
                {
                    string name = rowd.GetColValue("namedr").ToString();
                    DrugsCombo.Items.Add(name);


                }

            }
            else
            {
                MessageBox.Show(Access.ExplaindError());
            }


            //טוען רשימה של תיסונים
            VanicesCombo.Items.Clear();
            string queryv = SQL_Queries.Select("vaccines");
            vaccines = Access.getObjects(queryv);
            if (vaccines != null)
            {

                foreach (Row rowd in vaccines)
                {
                    string name = rowd.GetColValue("namev").ToString();
                    VanicesCombo.Items.Add(name);


                }

            }
            else
            {
                MessageBox.Show(Access.ExplaindError());
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Management_of_a_veterinary_clinic
{
    public class Access_Actions
    {
       
        public static bool CheckIDNo(string strID)
        {

            //פונקציה בודקת תקינות תעודת זהות

            int[] id_12_digits = { 1, 2, 1, 2, 1, 2, 1, 2, 1 };
            int count = 0;
            try
            {
                if (strID == null)
                    return false;

                strID = strID.PadLeft(9, '0');

                for (int i = 0; i < 9; i++)

                {
                    int num = int.Parse(strID.Substring(i, 1)) * id_12_digits[i];

                    if (num > 9)
                        num = (num / 10) + (num % 10);

                    count += num;
                }
            }
            catch { }
            return (count % 10 == 0);

        }
        public static bool CheckTel(string str)
        {
            //פונקציה בודקת תקינות טלפון

            int i = 0;
            for (i = 0; i < str.Length; i++)
                if (str[i] < '0' || str[i] > '9')
                    return false;
            if (str.Length != 10)
                return false;

            for (i = 0; i < str.Length; i++)
                if (str[i] == '0' || str[i - 1] != '0')

                    return true;

            return true;
        }
        public static bool CheckMail(string str)
        {
            //פונקציה בודקת תקינות אמייל

            int i = 0;

            
            
            if (str.IndexOf("@") != str.LastIndexOf("@") || str.IndexOf("@") == -1 || str.IndexOf("@") == 0)
            {

                return false;
            }
            if (str.LastIndexOf(".") == str.Length - 1 || str.IndexOf(".") == 0)
            {

                return false;
            }
            if (str.IndexOf('@' + ".") == str.IndexOf('@'))

                return false;


            if (str.IndexOf("@") > str.LastIndexOf("."))
            {

                return false;
            }
            //if (str1.IndexOf(".") < str1.LastIndexOf("."))
            //{

            //  return false;
            //}
            for (i = 0; i < str.Length; i++)
                if ((str[i] < 'a' && str[i] > 'Z'))


                    return false;


            return true;

        }
        public static string maksha1(string str)        {            //פונקציה יוצרת סיסמא באבטחת מידע של SHA1            SHA1CryptoServiceProvider sh = new SHA1CryptoServiceProvider();            sh.ComputeHash(ASCIIEncoding.ASCII.GetBytes(str));            byte[] re = sh.Hash;            StringBuilder sb = new StringBuilder();            foreach (byte b in re)            {                sb.Append(b.ToString("X2"));            }            return sb.ToString();        }
        public static bool login(string username,string password,string job)

        {
            //פונקציה בודקת שם משתמש וסיסמא בכניסה ראשית
            Condition condition = new Condition("username", username);
            string query = SQL_Queries.Select("login1", condition);
            List<Row> result = Access.getObjects(query);
            if (result != null)
            {
                Row row = result[0];
                if (row.GetColValue("password1").ToString() == maksha1(password))
                {
                    if (row.GetColValue("job").ToString() == job)
                    {
                        return true;
                    }
                    
                }
                return false;
            }
            else
            {
                return false;
            }
        }
        public static bool loginManage(string username, string password, string job)
        {
            //פונקציה בודקת שם משתמש וסיסמא בכניסה למנהל מערכת
            Condition condition = new Condition("username", username);
            string query = SQL_Queries.Select("LoginManagerSystem", condition);
            List<Row> result = Access.getObjects(query);
            if (result != null)
            {
                Row row = result[0];
                if (row.GetColValue("password").ToString() == maksha1(password))
                {
                        return true;
                }
                return false;
            }
            else
            {
                return false;
            }
        }
    }
}

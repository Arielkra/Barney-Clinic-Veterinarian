using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.OleDb;
using System.Text;
using System.Threading.Tasks;
using System.Data;

public class Access
    {
    //מחלקה שקוראת לבסיס נתונים ובודקת תקינות שאילתות
        private static string ConnectionStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=mvc project.accdb;Persist Security Info=False;";
        private static string Error ;
        private static OleDbCommand OleCommand;
        private static OleDbConnection Connection=new OleDbConnection(ConnectionStr);
        private static OleDbDataReader Reader;

            private static void testc() { }
        private static void ConnectionState(bool state)
        {

            try
            {
                if (state)
                    Connection.Open();
                else
                    Connection.Close();
            }
            catch(Exception err)
            {

            }
        }



        public static bool Execute(string command)
        {
            try
            {
                ConnectionState(true);
                OleCommand = new OleDbCommand(command, Connection);
                OleCommand.ExecuteNonQuery();
                
            }
            catch(Exception err)
            {
                Error = err.Message;
                ConnectionState(false);
                return false;
            }
            ConnectionState(false);
            return true;

        }
        public static string ExplaindError()
        {
            return Error;
        }
    public static List<Row> getObjects(string command)
    {
        List<Row> Rows = new List<Row>();
        try
        {
            ConnectionState(true);
            OleCommand = new OleDbCommand(command, Connection);
            Reader = OleCommand.ExecuteReader();
            while (Reader.Read())
            {
                Row row = new Row();
                var table = Reader.GetSchemaTable();
                var nameCol = table.Columns["ColumnName"];
                var fields = new List<string>();
                foreach(DataRow r in table.Rows)
                {
                    fields.Add(r[nameCol].ToString());
                }
                for (int i = 0; i < Reader.FieldCount; i++) 
                    row.AddColume(new Col(fields[i],Reader[i].ToString()));
                Rows.Add(row);
            }
            Reader.Close();
            ConnectionState(false);
            return Rows;

        }
        catch (Exception err)
        {
            Error = err.Message;
            ConnectionState(false);
            return null;
        }

    }

}


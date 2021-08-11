using System.Collections.Generic;

public class SQL_Queries
{ //מחלקה של השאילתות הוספה, עדכון,מחיקה
   
    private static string SQL_Syntax(object item)
    {
        switch (item.GetType().ToString())
        {

            case "System.String":
                {
                    return $"{'"'}{item.ToString()}{'"'}";

                }
            default:
                {
                    return $"{item.ToString()}";
                }
        }
    }
    private static string SQL_Syntax(Col value)
    {
        return $"{value.GetField()} = {value.Value_SQL_Syntax()}";
    } 


    public static string Insert(string table, List<object> list)
    {
        string values = "";
        
        foreach (object item in list)
        {
                values += $"{SQL_Syntax(item)},";
        }
        
        return $"insert into { table} values({ values.Trim(',')});";
    }
    public static string Update(string table, List<Col> Updated_Values,List<Condition> Conditions,string ConditionType)
    {
        string values =Col.Values_SQL_Syntax(Updated_Values,","),selector= Condition.SQL_Syntax(Conditions,ConditionType);
        return $"update {table} set {values} where {selector};";
    }
    public static string Update(string table, List<Col> Updated_Values, Condition Condition)
    {
        string values = Col.Values_SQL_Syntax(Updated_Values,",");
        return $"update {table} set {values.Trim(',')} where {Condition.SQL_Syntax()};";
    }
    public static string Delete(string table, List<Condition> Condition,string Contition_Type)
    {
        return $"delete from {table} where {(global::Condition.SQL_Syntax(Condition, Contition_Type))}";
    }
    public static string Delete(string table,Condition Condition)
    {
        return $"delete from {table} where {Condition.SQL_Syntax()};";
    }
    public static string Select(string table,List<string>Columns,List<Condition> Conditions,string ConditionType)
    {
        return $"select {Col.Values_SQL_Syntax(Columns, ",")} from {table} where {Condition.SQL_Syntax(Conditions, ConditionType)};";
    }
    public static string Select(string table, List<Condition> Conditions, string ConditionType)
    {
        return $"select * from {table} where {Condition.SQL_Syntax(Conditions, ConditionType)};";
    }
    public static string Select(string table, Condition Condition)
    {
        return $"select * from {table} where {Condition.SQL_Syntax()};";
    }
    public static string Select(string table)
    {
        return $"select * from {table};";
    }
    public static string Select(string table,List<string> Columes)
    {
        return $"select {Col.Values_SQL_Syntax(Columes,",")} from {table};";
    }
}



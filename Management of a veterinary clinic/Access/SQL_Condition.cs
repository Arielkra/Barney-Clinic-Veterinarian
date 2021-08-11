using System.Collections.Generic;

public class Condition
{  //מחלקה שיוצרת הרכבה של תנאים
    private Col Value { get; set; }
    private string Condition_Type { get; set; } = "=";
    public Condition(Col value, string condition)
    {
        Value = value;
        this.Condition_Type = condition;
    }
    public Condition(Col value)
    {
        Value = value;
    }


    public Condition(string field, object value)
    {
        Value = new Col(field, value);
    }


    public Condition(string field, object value, string condition)
    {
        Value = new Col(field, value);
        this.Condition_Type = condition;
    }


    public Col GetValue() {
        return Value;
    }



    public string GetCondition() {
        return Condition_Type;
    }

    public string SQL_Syntax() {
        return $"{Value.GetField()} {Condition_Type} {Value.Value_SQL_Syntax()}";
    }
    public static string SQL_Syntax(List<Condition> Conditions, string seperat)
    {
        string Sql_Conditions = $"{seperat} ";
        int i = 0;
        foreach (Condition condition in Conditions)
        {
            Sql_Conditions += $"{condition.SQL_Syntax()} ";
            if (i != Conditions.Count - 1)
                Sql_Conditions += $"{seperat} ";
            i++;
        }
        return Sql_Conditions.Trim(seperat.ToCharArray()).Trim();
    }
    public static string SQL_Syntax(List<List<Condition>> Conditions, string innerSeperate, string outtersparat)
    {
        string Sql_Conditions = $"{outtersparat} ";
        int i = 0;
        foreach (List<Condition> set in Conditions)
        {
            Sql_Conditions += $"{SQL_Syntax(set, innerSeperate)} ";
            if (i != Conditions.Count - 1)
                Sql_Conditions += $"{outtersparat} ";
            i++;
        }
        return Sql_Conditions.Trim(outtersparat.ToCharArray());
    }
}
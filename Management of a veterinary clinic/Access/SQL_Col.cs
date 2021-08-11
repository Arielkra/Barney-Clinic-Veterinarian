using System.Collections.Generic;

public class Col
{
    //מחלקה עבודה על עמודות
    //TODO: rename vars
    private string _field {get;set;}
    private object _value {get;set;}

    /// <summary>
    /// Create a Column
    /// </summary>
    /// <param name="field">column name</param>
    /// <param name="value">value in the column</param>
    public Col(string field, object value)
    {
        Set(field, value);
    }
    /// <summary>
    /// Set column values
    /// </summary>
    /// <param name="field">column name</param>
    /// <param name="value">value in the column</param>
    public void Set(string field, object value)
    {
        _field = field;
        _value = value;
    }
    /// <summary>
    /// Copy column data from another column
    /// </summary>
    /// <param name="col">column to cope from</param>
    public void Set(Col col)
    {
        _field = col.GetField();
        _value = col.GetValue();
    }

    /// <summary>
    /// Get column value
    /// </summary>
    public object GetValue() {
        return _value;
    }
    /// <summary>
    /// Get column field
    /// </summary>
    public string GetField() {
        return _field;
    }

    /// <summary>
    /// return the string of column value object
    /// </summary>
    /// <returns></returns>
    public string Value_SQL_Syntax()
    {
        return _value is string ? $"{'"'}{_value.ToString()}{'"'}" : $"{_value.ToString()}";
    }

    /// <summary>
    /// Get list of columns from list of fields and values
    /// </summary>
    /// <param name="fields">list of column names</param>
    /// <param name="values">list of data</param>
    /// <returns></returns>
    public static List<Col> Get_SQL_Values(List<string> fields, List<object> values)
    {
        List<Col> list = new List<Col>();
        for (int i = 0; i < fields.Count && i < values.Count; i++)
            list.Add(new Col(fields[i], values[i]));
        return list;
    }

    /// <summary>
    /// Returns a string from a list of columns seperated by a seperator
    /// </summary>
    /// <param name="Values">List of colomns</param>
    /// <param name="seperator">the seperator</param>
    /// <returns></returns>
    public static string Values_SQL_Syntax(List<Col> Values, string seperator)
    {
        string syntax = "";
        int i = 0;
        foreach (Col update in Values)
        {
            syntax += $"{update._field}={update.Value_SQL_Syntax()}";
            if (i++ != Values.Count - 1)
                syntax += $"{seperator}";
        }
        return syntax.Trim(seperator.ToCharArray()).Trim();
    }
    /// <summary>
    /// Returns a string from a list of strings seperated by a seperator
    /// </summary>
    /// <param name="Values">List of strings</param>
    /// <param name="seperator">the seperator</param>
    /// <returns></returns>
    public static string Values_SQL_Syntax(List<string> Values, string seperator)
    {
        string syntax = "";
        int i = 0;
        foreach (string update in Values)
        {
            syntax += $"{update}";
            if (i++ != Values.Count - 1)
                syntax += $"{seperator}";
        }
        return syntax.Trim(seperator.ToCharArray()).Trim();
    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Row{
    //מחלקה שמטפלת על פי שורות בטבלה
    private List<Col> Columes { get; set; } = new List<Col>();

    public Row(List<Col> cols)
    {
        Columes = cols;
    }
    public Row()
    {

    }
    public List<Col> GetColumes() { return Columes; }

///<summary>
    ///adding a new Colume to the end of the row
    ///</summary>
    ///<param name="col">adding a new colume to the end of the row</params>
    ///<returns>returns true of false if the colume exsist</returns>
    private bool ExistColume(Col col)
    {
        foreach (Col colume in Columes)
            if (colume.GetField() == col.GetField())
                return true;
        return false;
    }
    ///<summary>
    ///adding a new Colume to the end of the row
    ///</summary>
    ///<param name="string">the field name</params>
    ///<returns>returns true of false if the colume exsist by </returns>
    private bool ExistColume(string key)
    {
        foreach (Col colume in Columes)
            if (colume.GetField() == key)
                return true;
        return false;
    }
    ///<summary>
    ///adding a new Colume to the end of the row
    ///</summary>
    ///<param name="col">adding a new colume to the end of the row</params>
    public void AddColume(Col col)
    {
        if(ExistColume(col)==false)
            Columes.Add(col);
    }
    ///<summary>
    ///adding a new Colume to the end of the row
    ///</summary>
    ///<param name="string">field</params>
    ///</params name="object">value</params>
    
    public void AddColume(string field,object value)
    {
        if(ExistColume(field)==false)
            Columes.Add(new Col(field, value));
    }
     /// <summary>
    /// adding a new Colume to the end of the row
    /// </summary>
    /// <param name="Col">Colume </param>
    /// <param name="int">index</param>

    public void AddColume(Col col,int index)
    {
        if (ExistColume(col) == false)
            return;
        List<Col> cols = new List<Col>();
        if (index < Columes.Count)
        {
            for (int i = 0; i < Columes.Count;)
            {
                if (index == i)
                    cols.Add(col);
                else
                {
                    cols.Add(Columes[i]);
                    i++;
                }
            }
            Columes = cols;
        }
        else
            Columes.Add(col);
    }
     /// <summary>
    /// add a new colume in a specific place 
    /// </summary>
    /// <param name="string">field key</param>
    /// <param name="object">value colume</param>
    /// <param name="int">index where you want to put the colume</param>
    public void AddColume(string field, object value,int index)
    {

        if (ExistColume(field) == false)
            return;
        List<Col> cols = new List<Col>();
        if (index < Columes.Count)
        {
            for (int i = 0; i < Columes.Count;)
            {
                if (index == i)
                    cols.Add(new Col(field, value));
                else
                {
                    cols.Add(Columes[i]);
                    i++;
                }
            }
            Columes = cols;
        }
        else
            Columes.Add(new Col(field, value));
        
    }
     /// <summary>
    /// returns the object value based on index 
    /// </summary>
    /// <param name="index">zero index based</param>
    public void DeleteColume(int index)
    {
        Columes.RemoveAt(index);
    }
     /// <summary>
    /// deletes the colume from the row based on the colume index 
    /// </summary>
    /// <param name="key">field key</param>
    public void DeleteColume(string key)
    {
        foreach(Col col in Columes)
        {
            if (col.GetField() == key)
            {
                Columes.Remove(col);
                return;
            }
        }
    }
     /// <summary>
    /// returns the object value based on field key 
    /// </summary>
    /// <param name="key">field key</param>
    public object GetColValue(string key)
    {
        foreach (Col col in Columes)
            if (col.GetField() == key)
                return col.GetValue();
        return null;

    }
      /// <summary>
    /// returns the object value based on index 
    /// </summary>
    /// <param name="index">zero based index</param>
  
    public object GetColValue(int index)
    {
        try
        {
            return Columes[index].GetValue();
        }
        catch
        {
            return null;

        }
    }
         /// <summary>
    /// update a Colume in row 
    /// </summary>
    /// <param name="Col">column to update colume with new value (fields must be idintical)</param>
    public void UpdateColume(Col New_data) {
        foreach(Col col in Columes){
            if(col.GetField()== New_data.GetField())
                {
                    col.Set(New_data);
                    break;
                }
        }

     }
      /// <summary>
    /// Copy a set of columns in the row
    /// </summary>
    /// <param name="List<col>">a row of columes with the new values (fields must be idintical)</param>
    public void UpdateColume(List<Col> New_data) {
        foreach(Col col in New_data)
            UpdateColume(col);
     }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank;

public interface IQueryTool
{

    public int InsertQuery(string fullName, List<string> Сlassif, int price, int col);
    public void DeleteQuery(int userId);
    public void ListCompletion();
    public List<string> GlobalSelectQuery();
    public List<string> SingleSelectQuery(string name);

    //public void Insert(string name, int price, int col);
    public void UpdateQuery(string it_name, int userAnswer);
    public int Delete(string it_name);

    public Form1 myform { set; }


}

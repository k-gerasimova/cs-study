using Npgsql;
using System;
using System.Diagnostics.Metrics;
namespace Bank;

public class QueryTool : IQueryTool
{
    private readonly NpgsqlConnection con;
    private List<string> dbQueryResidence = new List<string>();
    private Form1 mf;




    public QueryTool()
    {
        con = new NpgsqlConnection(@"Server=localhost;Port=5432;User Id=postgres;Password=happysadness;Database=aboba;");
    }
    private void SqlQuery(string insertQuery)
    {
        using (NpgsqlCommand a = new NpgsqlCommand(insertQuery, con))
        {
            con.Open();
            //NpgsqlCommand insert = new(insertQuery, con);
            //a.Parameters.AddWithValue(p1, p1);
            //a.Parameters.AddWithValue(p2, p2);
            a.ExecuteNonQuery();
            con.Close();
        }
    }



    public void ListCompletion()
    {
        NpgsqlCommand command = new("select id_item, name, price, count from item", con);
        con.Open();
        NpgsqlDataReader reader = command.ExecuteReader();
        if (reader.HasRows)
        {
            while (reader.Read())
            {
                string[] b = { reader.GetInt64(0).ToString(), reader.GetString(1), reader.GetInt64(2).ToString(), reader.GetInt64(3).ToString() };
                List<string> a = new List<string>(b);
                dbQueryResidence.AddRange(a);
            }
        }
        reader.Close();
        con.Close();
    }





    private int ReturnId(string query)
    {
        NpgsqlCommand test = new(query, con);
        con.Open();
        int result = Convert.ToInt32(test.ExecuteScalar());
        con.Close();
        return result;
    }
    public int InsertQuery(string fullName, List<string> Сlassif, int price, int col)
    {
        int userId = 0;


        int id = int.Parse(dbQueryResidence[dbQueryResidence.Count - 4]) + 1;
        dbQueryResidence.AddRange(new[] { id.ToString(), fullName, price.ToString(), col.ToString() });
        SqlQuery($"insert into item values (default, '{fullName}', {price}, {col})");
        //Insert(fullName, price, col);


        if (Сlassif != null)
        {
            
            string k;
            for (int i =0; i < Сlassif.Count; i++)
            {
                 k = Сlassif[i].ToLower();
                //k = Сlassif[i] ;
                //в комманд вставляем plsql c возвращением значения 0 - класс уже добавлен, 1 - такого класса не существует, 2 - все супер добавилось
                //SqlQuery($"insert into it_class values ((select id_item from item where name = '{fullName}'), (select id_class from classification where name_class = '{k}'))");
                SqlQuery($"select ins_class('{fullName}', '{k}')");
            }
        }
        return userId;
    }
    public void DeleteQuery(int Id)
    {
        SqlQuery($"delete from item_order where id_order = {Id};delete from transorder where id_ord = {Id}");
    }
    public List<string> GlobalSelectQuery()
    {
        List<string> ret = new List<string>();
        NpgsqlCommand command = new("select company_name, item.name, item_order.amount, (item_order.amount*item.price), face, adress from transorder inner join item_order on transorder.id_ord = item_order.id_order inner join item on item_order.id_item = item.id_item order by company_name", con);
        con.Open();
        NpgsqlDataReader reader = command.ExecuteReader();
        if (reader.HasRows)
        {
            while (reader.Read())
            {
                //Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}", reader[0], reader[1], reader[2], reader[3], reader[4], reader[5]);
                ret.AddRange(new[] { reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString() });
            }
        } else
        {
            con.Close();
            return null;
        }
        reader.Close();
        con.Close();
        return ret;
    }
    public List<string> SingleSelectQuery(string name)
    {
        NpgsqlCommand command = new($"select id_item,name, price, count from item where name = '{name}'", con);
        con.Open();
        List<string> ret = new List<string>();
        NpgsqlDataReader reader = command.ExecuteReader();
        if (!reader.HasRows)
        {
            con.Close();
           return null;
        }
        else
        {
            while (reader.Read())
            {
                //Console.WriteLine("{0}\t{1}\t{2}\t{3}", reader[0], reader[1], reader[2], reader[3]);
                ret.AddRange(new[] { reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString() });
            }
        }
        con.Close();
        return ret;
    }  


    Label a = new Label();
    public int Delete(string it_name)
    {
        int i = 0;
        NpgsqlCommand command = new($"select name_class from classification inner join it_class on classification.id_class = it_class.id_class inner join item on it_class.id_item = item.id_item where item.name = '{it_name}'", con);
        con.Open();
        NpgsqlDataReader reader = command.ExecuteReader();
        if (reader.HasRows)
        {
            
            a.Location = new Point(200, 235);

            a.Size = new Size(250, 23);
            a.Font = new Font(a.Font.Name, 11, a.Font.Style);
            a.Text = "Классификации товара:";
            a.Name = "a3" + "a";
            mf.Controls.Add(a);
            Label r;

            while (reader.Read())
            {
                r = new Label(); r.Location = new Point(250, 270 + 30*i); r.Text = reader[0].ToString(); r.Size = new Size(250, 23);
                r.Name = "r" + i;
                i++;
                mf.Controls.Add(r);
            }
            reader.Close();



        } else
        {
            MessageBox.Show("У товара нет классификаций или такого товара нет.");
        }
        con.Close();
        return i;
    }


    public void UpdateQuery(string it_name, int userAnswer)
    {

        bool error = false;
        int newData;
        if (userAnswer == 1)
        {
            try
            {
                 newData = Convert.ToInt32(mf.Controls["a3t"].Text.ToLower());
                 SqlQuery($"update item set price = '{newData}' where name = '{it_name}'");


            }
            catch
            {
                error = true;
                MessageBox.Show("Цена - числовое значение!");
                return;
            }
            MessageBox.Show("Цена товара обновлена.");
            //SqlQuery($"update item set price = '{newData}' where name = '{it_name}'");

        }
        else if (userAnswer == 2)
        {
                try
                {
                newData = Convert.ToInt32(mf.Controls["a3t"].Text);
                SqlQuery($"update item set count = '{newData}' where name = '{it_name}'");
                return;

            }
                catch
                {
                MessageBox.Show("Количество - числовое значение!");
            }
            MessageBox.Show("Количество товара обновлено.");
            //SqlQuery($"update item set count = '{newData}' where name = '{it_name}'");
        }
        else if (userAnswer == 3)
        {
            if (mf.add_c == true)
            {

                string Data = mf.Controls["a3tadd"].Text.ToLower();
                //в комманд вставляем plsql c возвращением значения 0 - класс уже добавлен, 1 - такого класса не существует, 2 - все супер добавилось

                //NpgsqlCommand command = new($"insert into it_class values ((select id_item from item where name = '{it_name}'),(select id_class from classification where name_class = '{Data}'))", con);
                NpgsqlCommand command = new( $"select ins_class('{it_name}', '{Data}')", con);

                int n;
                con.Open();

                NpgsqlDataReader read = command.ExecuteReader();
                read.Read();
                n = int.Parse(read[0].ToString());
                //n = int.Parse(command.ExecuteReader()[0]);
                con.Close();
                read.Close();


                if (n == 0)
                {
                    MessageBox.Show("Такой класс уже добавлен.");
                }
                else if (n == 1)
                {
                    MessageBox.Show("Такой класса не существует.");
                }
                else if (n == 2)
                {
                    MessageBox.Show("Классификация добавлена.");
                }
            }
            else
            {
                con.Open();
                string Data = mf.Controls["a3td"].Text.ToLower();
                NpgsqlCommand command1 = new($"delete from it_class where id_class = (select id_class from classification where name_class = '{Data}') and id_item = (select id_item from item where name = '{it_name}')", con);
                command1.ExecuteNonQuery();
                MessageBox.Show("Классификация с товара удалена.");
                con.Close();
            }

        }

    }


    public Form1 myform
    {
        set
        {
            mf = value;
        }
    }

}

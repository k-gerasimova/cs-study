using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace Bank
{
    public partial class Form1 : Form
    {
        string a1 = "1. Добавить новый товар.";
        string a2 = "2. Удалить заказ";
        string a3 = "3. Обновить товар";
        string a4 = "4. Показать все заказы";
        string a5 = "5. Показать информацию об одном товаре";
        int n = 0;
        int for_upd = 0;
        private readonly IQueryTool user;
        public DataGridView tab = new DataGridView();
        public Form1()
        {
            Communication test = new(); 
            user = new QueryTool();
            tab = new DataGridView();
            user.myform = this;
            user.ListCompletion();

            InitializeComponent();
            this.AutoScroll = true;
            comboBox1.Items.Add(a1);
            comboBox1.Items.Add(a2);
            comboBox1.Items.Add(a3);
            comboBox1.Items.Add(a4);
            comboBox1.Items.Add(a5);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Restart()
        {
            this.Controls.Clear();
            this.InitializeComponent();
            this.AutoScroll = true;
            comboBox1.Items.Add(a1);
            comboBox1.Items.Add(a2);
            comboBox1.Items.Add(a3);
            comboBox1.Items.Add(a4);
            comboBox1.Items.Add(a5);
            for_upd = 0;
            add_c = false;
            i = 0;

        }

        private void Add_to_base(object sender, EventArgs e)
        {
            List<string> Class = new List<string>();
            /*while ((a = Console.ReadLine()) != "OK")
            {
                Class.Add(a);
            }
            Console.WriteLine("Введите цену товара");
            int price = int.Parse(Console.ReadLine());
            Console.WriteLine();
            Console.WriteLine("Введите количество товара в сети магазина");
            int col = int.Parse(Console.ReadLine());
            Console.WriteLine();


            int userId = user.InsertQuery(fullName, Class, price, col);

            Console.WriteLine("Товар успешно добавлен!");*/

            string a = (this.Controls[(sender as Button).Name + "tclass"].Text.ToLower());

            //string a = (this.Controls[(sender as Button).Name + "tclass"].Text);


            Class = a.Split(",").ToList<string>();
            string fullName = (this.Controls[(sender as Button).Name + "tname"].Text);
            int price = int.Parse((this.Controls[(sender as Button).Name + "tprice"].Text));
            int col = int.Parse((this.Controls[(sender as Button).Name + "tcol"].Text));

            user.InsertQuery(fullName.ToLower(), Class, price, col);
            //user.InsertQuery(fullName, Class, price, col);
            // int userId = user.InsertQuery(fullName, Class, price, col);

            MessageBox.Show("Товар успешно добавлен.");
            // string[] cClass = (this.Controls[(sender as Button).Name + "tclass"]).Text).Split(",");
            Restart();


        }

        private void newit()
        {
            TextBox txtname = new TextBox();
            TextBox txtprice = new TextBox();
            TextBox txtcol = new TextBox();
            TextBox txtclass = new TextBox();

            Label lname = new Label();
            Label lprice = new Label();
            Label lcol = new Label();
            Label lclass = new Label();

            txtname.Location = new System.Drawing.Point(200, 130);
            txtprice.Location = new System.Drawing.Point(200, 190);
            txtcol.Location = new System.Drawing.Point(200, 250);
            txtclass.Location = new System.Drawing.Point(200, 310);
            txtname.Name = "a1" + "tname";
            txtprice.Name = "a1" + "tprice";
            txtcol.Name = "a1" + "tcol";
            txtclass.Name = "a1" + "tclass";


            lname.Location = new Point(200, 100);
            lprice.Location = new Point(200, 160);
            lcol.Location = new Point(200, 220);
            lclass.Location = new Point(200, 280);

            lname.Size = new Size(160, 23);
            lname.Text = "Введите название товара";
            lprice.Size = new Size(160, 23);
            lprice.Text = "Введите цену товара";
            lcol.Size = new Size(160, 23);
            lcol.Text = "Введите количество товара";
            lclass.Size = new Size(230, 23);
            lclass.Text = "Введите классификации через запятую";


            txtname.Size = new System.Drawing.Size(200, 23);
            txtprice.Size = new System.Drawing.Size(200, 23);
            txtcol.Size = new System.Drawing.Size(200, 23);
            txtclass.Size = new System.Drawing.Size(260, 23);
            Controls.Add(txtname);
            Controls.Add(lname);
            Controls.Add(txtprice);
            Controls.Add(lprice);
            Controls.Add(txtcol);
            Controls.Add(lcol);
            Controls.Add(txtclass);
            Controls.Add(lclass);

            Button bt = new Button();
            bt.Location = new Point(450, 130);
            bt.Size = new Size(90, 23);
            bt.Text = "Подтвердить.";
            bt.Name = "a1";
            bt.Click += Add_to_base;
            Controls.Add(bt);
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            int Id = Convert.ToInt32(this.Controls[(sender as Button).Name + "tid"].Text);
            user.DeleteQuery(Id);
            MessageBox.Show("Заказ успешно  удален.");
            Restart();
        }

        private void itdelete()
        {

            TextBox Tid = new TextBox();
            Label lid = new Label();
            Tid.Location = new System.Drawing.Point(200, 130);
            Tid.Name = "a2" + "tid";
            lid.Location = new Point(200, 100);

            lid.Size = new Size(160, 23);
            lid.Text = "Введите ID заказа, который хотите удалить.";

            Tid.Size = new System.Drawing.Size(200, 23);
            Controls.Add(Tid);
            Controls.Add(lid);

            Button bt = new Button();
            bt.Location = new Point(450, 130);
            bt.Size = new Size(90, 23);
            bt.Text = "Подтвердить.";
            bt.Name = "a2";
            bt.Click +=Delete_Click;
            Controls.Add(bt);


        }
         




        private void Print()
        {

             List<string> obj = user.GlobalSelectQuery();
             if (obj == null) { return; }


            tab.Size = new Size(650, 300);
            tab.Location =  new Point(70, 80);
            DataGridViewTextBoxColumn col0 = new DataGridViewTextBoxColumn();
            col0.Name = "Заказчик";
            col0.HeaderText = "Заказчик";

            DataGridViewTextBoxColumn col1 = new DataGridViewTextBoxColumn();
            col1.Name = "Товар";
            col1.HeaderText = "Товар";

            DataGridViewTextBoxColumn col2 = new DataGridViewTextBoxColumn();
            col2.Name = "Количество";
            col2.HeaderText = "Количество";

            DataGridViewTextBoxColumn col3 = new DataGridViewTextBoxColumn();
            col3.Name = "Сумма заказа";
            col3.HeaderText = "Сумма заказа";

            DataGridViewTextBoxColumn col4 = new DataGridViewTextBoxColumn();
            col4.Name = "Тип лица";
            col4.HeaderText = "Типа лица";

            DataGridViewTextBoxColumn col5 = new DataGridViewTextBoxColumn();
            col5.Name = "Адрес";
            col5.HeaderText = "Адрес";


            tab.Columns.AddRange(col0, col1, col2, col3, col4, col5);

            tab.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            tab.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            tab.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            tab.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            tab.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            tab.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            DataGridViewCell cel;
            DataGridViewRow row ;

            for (int i = 0; i < obj.Count; i += 6)
            {
                row = new DataGridViewRow();
                cel = new DataGridViewTextBoxCell();
                cel.Value = obj[i];
                row.Cells.Add(cel);

                cel = new DataGridViewTextBoxCell();
                cel.Value = obj[i + 1];
                row.Cells.Add(cel);

                cel = new DataGridViewTextBoxCell();
                cel.Value = obj[i + 2];
                row.Cells.Add(cel);

                cel = new DataGridViewTextBoxCell();
                cel.Value = obj[i + 3];
                row.Cells.Add(cel);

                cel = new DataGridViewTextBoxCell();
                cel.Value = obj[i + 4];
                row.Cells.Add(cel);

                cel = new DataGridViewTextBoxCell();
                cel.Value = obj[i + 5];
                row.Cells.Add(cel);

                tab.Rows.Add(row);
            }

            Controls.Add(tab);

        }

        private void Select(object sender, EventArgs e)
        {
            

            List<string> obj = user.SingleSelectQuery((this.Controls[(sender as Button).Name + "tus"].Text.ToLower()));
            if (obj == null)
            {
                MessageBox.Show("Такого товара нет.");
                return;
            }
            if (n == 1)
            {


                for (int i = 0; i < obj.Count(); i += 4)
                {

                     this.Controls["lid"].Text = $"ID: {obj[i]}";
                     this.Controls["ln"].Text = $"Название товара: {obj[i + 1]}";
                     this.Controls["lprice"].Text = $"Цена: {obj[i + 2]}";
                     this.Controls["lcol"].Text = $"Количество товара: {obj[i + 3]}";


                }
                //this.Conrols["lid"].Text
                return;
            }

            for (int i = 0; i < obj.Count(); i+=4)
            {
                Label lid = new Label(); Label ln = new Label(); Label lprice = new Label(); Label lcol = new Label();
                lid.Size = new Size(160, 23); lid.Text = $"ID: {obj[i]}";
                ln.Size = new Size(230, 23); ln.Text = $"Название товара: {obj[i+1]}";
                lprice.Size = new Size(160, 23); lprice.Text = $"Цена: {obj[i+2]}";
                lcol.Size = new Size(160, 23); lcol.Text = $"Количество товара: {obj[i+3]}";
                lid.Name = "lid"; ln.Name = "ln"; lprice.Name = "lprice"; lcol.Name = "lcol";

                lid.Location = new Point(200, 180 + i*30);
                ln.Location = new Point(200, 180 +i*30 + 30);
                lprice.Location = new Point(200, 180 +i*30 + 60);
                lcol.Location = new Point(200, 180+i*30 + 90);

                Controls.Add(lid); Controls.Add(ln); Controls.Add(lprice); Controls.Add(lcol);
                n = 1;


            }
           // n++;
        }

        private void SelectUser()
        {
            //Console.WriteLine("Введите название товара");
            //string name = Console.ReadLine();
            
            
            TextBox Tus = new TextBox();
            Label lus = new Label();
            Tus.Location = new System.Drawing.Point(200, 130);
            Tus.Name =   "a5" + "tus";
            lus.Location = new Point(200, 100);

            lus.Size = new Size(160, 23);
            lus.Text = "Введите название товара.";

            Tus.Size = new System.Drawing.Size(200, 23);
            Controls.Add(Tus);
            Controls.Add(lus);

            Button bt = new Button();
            bt.Location = new Point(450, 130);
            bt.Size = new Size(90, 23);
            bt.Text = "Подтвердить.";
            bt.Name = "a5";
            bt.Click += Select;
            Controls.Add(bt);


            

        }



        //объекты для классификации//
        Button delete = new Button();
        Button add = new Button();
        Label classif = new Label(); // чтобы вывести все класификации при удалении
        TextBox for_del = new TextBox(); 
        TextBox for_add = new TextBox();

        int i = 0;

        private void delete_c(object sender, EventArgs e)
        {
            Remov();
            /*if (i != 0)
            {
                this.Controls.Remove(this.Controls["a3" + "td"]);
                this.Controls.Remove(this.Controls["a3" + "ld"]);
                for (int k=0; k <i; k++)
                {
                    this.Controls.Remove(this.Controls["r" + k]);
                }
            }*/
            i = user.Delete(this.Controls["a3tup"].Text.ToLower());

            if (i != 0)
            {
                TextBox td = new TextBox();
                Label ld = new Label();

                td.Location = new System.Drawing.Point(380, 270 + 30 *i);
                td.Size = new System.Drawing.Size(200, 23);
                td.Name = "a3" + "td";

                ld.Name = "a3" + "ld";
                ld.Location = new Point(200, 270 + 30 * i);
                ld.Size = new Size(200, 23);
                ld.Text = "Классификация на удаление:";
                
                Controls.Add(td);
                Controls.Add(ld);

            }



        }

        private void Remov()
        {
            Control a;
            if (i != 0)
            {
                
                a = this.Controls["a3" + "td"];
                this.Controls.Remove(a);
                a.Dispose();
                a = this.Controls["a3" + "ld"];
                this.Controls.Remove(a);
                a.Dispose();


                for (int k = 0; k < i; k++)
                {
                    this.Controls.Remove(a = this.Controls["r" + k]);
                    a.Dispose();
                }
                this.Controls.Remove(a = this.Controls["a3" + "a"]);
            }
            i = 0;
            if (add_c == true)
            {
                a = this.Controls["a3" + "tadd"];
                this.Controls.Remove(a);
                a.Dispose();
                a = this.Controls["a3" + "ladd"];
                this.Controls.Remove(a);
                a.Dispose();
                add_c = false;
            }

        }

        public bool add_c = false;

       private void add_click(object sender, EventArgs e)
        {
            Remov();
            TextBox ta = new TextBox();
            Label la = new Label();

            ta.Location = new System.Drawing.Point(400, 275);
            ta.Size = new System.Drawing.Size(200, 23);
            ta.Name = "a3" + "tadd";

            la.Name = "a3" + "ladd";
            la.Location = new Point(200, 275);
            la.Size = new Size(200, 23);
            la.Text = "Классификация на добавление:";
            add_c = true;
            Controls.Add(la); Controls.Add(ta);

        }

        private void change(object sender, EventArgs e)
        {
            if (for_upd == 3)
            {
                 Controls.Remove(delete);
                 Controls.Remove(add);
                Remov();

            } else if (for_upd == 1 || for_upd == 2)
            {
                Controls.Remove(T);
                Remov();

            }

            if ((sender as ComboBox).SelectedItem.ToString() == "Цена")
            {

                for_upd = 1;


            } else if ((sender as ComboBox).SelectedItem.ToString() == "Количество")
            {
                for_upd = 2;

            } else if ((sender as ComboBox).SelectedItem.ToString() == "Классификация")
            {
                for_upd = 3;

                delete.Location = new System.Drawing.Point(400, 215);
                delete.Name = "a3" + "d";
                delete.Text = "Удалить";
                delete.Click += delete_c;

                add.Location = new System.Drawing.Point(400, 190);
                add.Name = "a3" + "ad";
                add.Text = "Добавить";
                add.Click += add_click;

                Controls.Add(add);
                Controls.Add(delete);
            }
            if (for_upd == 1 || for_upd == 2)
            {
                T.Location = new System.Drawing.Point(400, 205);
                T.Name = "a3" + "t";
                Controls.Add(T);
            }





        }

        
        Label l = new Label();
        TextBox T = new TextBox(); // текст бокс для цены и количества

        private void Upd(object sender, EventArgs e) //событие при нажатити кнопки
        {
            //it_name = this.Controls[]

            user.UpdateQuery(this.Controls["a3tup"].Text.ToLower(), for_upd);


         //   user.UpdateQuery(this.Controls["a3tup"].Text, for_upd);

            //for_upd = 0;
        }

        private void Update()
        {
            TextBox Tup = new TextBox();
            Label lus = new Label();
            Tup.Location = new System.Drawing.Point(200, 130);
            Tup.Name = "a3" + "tup";
            lus.Location = new Point(200, 100);

            lus.Size = new Size(160, 23);
            lus.Text = "Введите название товара.";

            Tup.Size = new System.Drawing.Size(200, 23);
            Controls.Add(Tup);
            Controls.Add(lus);



            l.Location = new Point(200, 165);
            l.Size = new Size(400, 23);
            l.Text = "Какие данные вы хотите обновить?";


            ComboBox ord = new ComboBox();
            ord.Location = new Point(200, 205);
            ord.Size = new Size(160, 23);
            ord.Items.Add("Цена");
            ord.Items.Add("Количество");
            ord.Items.Add("Классификация");


            ord.SelectedIndexChanged += change;

            Controls.Add(l);
            Controls.Add(ord);


            Button bt = new Button();
            bt.Location = new Point(450, 130);
            bt.Size = new Size(90, 23);
            bt.Text = "Подтвердить.";
            bt.BackColor = Color.CadetBlue;
            bt.Name = "a3";
            bt.Click += Upd;
            Controls.Add(bt);




        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = comboBox1.SelectedItem.ToString();
            n = 0;
            tab.Rows.Clear();
            tab.Columns.Clear();

            if (selected == a1) //новый заказ
            {
                Restart();
                newit();


            }
            else if (selected == a2) //удалить заказ
            {
               
                Restart();
                itdelete();
                

            }
            else if (selected == a3) //обновить товар
            {
                Restart();
                Update();

            }
            else if (selected == a4) //показать все заказы
            {
                
                Restart();
                Print();

            }
            else if (selected == a5) //показать инф об одном заказе
            {
                
                Restart();
                SelectUser();

            }




        }
    }
}

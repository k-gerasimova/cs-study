using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        Controller controller = new Controller();
        public Form1()
        {
            InitializeComponent();
            this.AutoScroll = true;
        }

        int num=0;


        public static List<Button> buttons = new List<Button>()  ;
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "выбрать")
            {
                if (Appear.vkl == true) Restart();
                if (Choose.vkl == true) { MessageBox.Show("Вы уже вывели всех доступных животных"); return; }
                num = 1;
            }
            else if (textBox1.Text == "показать") {
                if (Choose.vkl == true) Restart();
                num = 2;
            }
            else if (textBox1.Text == "справка") { num = 3; }
            functions Chose = new Choose();
            functions Apear = new Appear();
            functions help = new Help();
            Chose.successor = Apear;
            Apear.successor = help;
            Chose.handle(num);
            num = 0;
            textBox1.Clear();
            if (Choose.vkl == true) Create_button();
            if (Appear.vkl == true) Passports();
        }


        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void Passports()
        {
            num = 2;
            TextBox txt = new TextBox();
            Label l = new Label();
            txt.Location = new System.Drawing.Point(200, 130);
            l.Location = new Point(200, 100);
            txt.Name = "txt";
            l.Size = new Size(160, 23);
            l.Text = "Введите имя животного";
            txt.Size = new System.Drawing.Size(200, 23);
            Controls.Add(txt);
            Controls.Add(l);
            Button bt = new Button();
            bt.Location = new Point(450, 130);
            bt.Size = new Size(90, 23);
            bt.Text = "Подтвердить.";
            bt.Name = "bt";
            bt.Click += Appear_button;
            Controls.Add(bt);

        }

        private void Appear_button(object sender, EventArgs e)
        {
            Passport s;
            Form2 newForm = new Form2();
            s = Search(this.Controls["txt"].Text);
            if (s != null)
            {
                newForm.pictureBox1.Image = s.IMG;
                newForm.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                newForm.label4.Text = s.name;
                newForm.label5.Text = s.age.ToString();
                newForm.label6.Text = s.information;
                newForm.Show();
            }
       //     Restart();
        }

        private Passport Search(string name)
        {
            foreach (var i in Base.Animals)
            {
                if (i.name == name) return i;
            }
            MessageBox.Show("Такого животного нет в базе.");
            return null;
        }

        class Controller
        {
            public string Question(string msg)//msg - то что ищем  
            { 
                return DataBase.GetAnser(msg);

            }
        }


        public static class DataBase
        {
            //question - то что ищем  
            public static List<string> Base = new List<string>();
            public static string GetAnser(string question)
            {
                

                string path = "D:\\base.txt";// путь к базе даных ):  

                // создаем обьект (читатель)  
                using (StreamReader sr =
                    new StreamReader(path, Encoding.Default))
                {
                    string str = null;
                    while (true)
                    {
                        //если пустая то это конец  
                        str = sr.ReadLine();
                        if (str != null)
                        {
                            //если считаная строчка   
                            //равна строке для поиска  
                            if (Choose.vkl == true) { Base.Add(str); }

                            if (str == question)
                            {
                                sr.Close();
                                return question;
                            }
                            
                        } else
                        {
                            sr.Close();
                            return "1101";
                        }
                    }
                }
            }
        }

        private void ButtonOnClick(object sender, EventArgs eventArgs)
        {
            Label lbn = new Label();
            Label lba = new Label();
            Label lbi = new Label();
            TextBox txtn = new TextBox();
            TextBox txta = new TextBox();
            TextBox txti = new TextBox();
            txtn.Location = new System.Drawing.Point((sender as Button).Left + 200, (sender as Button).Top+25);
            txta.Location = new System.Drawing.Point((sender as Button).Left + 210, (sender as Button).Top + 75);
            txti.Location = new System.Drawing.Point((sender as Button).Left + 200, (sender as Button).Top + 125);
            txta.Name = (sender as Button).Name+" ba";
            txtn.Name = (sender as Button).Name+" bn";
            txti.Name = (sender as Button).Name+" bi";
            txta.Size = new System.Drawing.Size(30, 23);
            txtn.Size = new System.Drawing.Size(80, 23);
            txti.Size = new System.Drawing.Size(200, 23);
            Controls.Add(txta);
            Controls.Add(txtn);
            Controls.Add(txti);

            lbn.Location = new System.Drawing.Point((sender as Button).Left + 200, (sender as Button).Top);
            lba.Location = new System.Drawing.Point((sender as Button).Left + 200, (sender as Button).Top+50);
            lbi.Location = new System.Drawing.Point((sender as Button).Left + 200, (sender as Button).Top+100);
            lba.Size = new System.Drawing.Size(60, 21);
            lbn.Size = new System.Drawing.Size(40, 21);
            lbi.Size = new System.Drawing.Size(90, 21);
            lba.Text = "Возраст";
            lbn.Text = "Имя";
            lbi.Text = "Информация";
            Controls.Add(lba);
            Controls.Add(lbn);
            Controls.Add(lbi);
            Button make = new Button();
            make.Location = new Point((sender as Button).Left + 400, (sender as Button).Top + 40);
            make.Name = (sender as Button).Name + " b";
            make.Size = new Size(80, 30);
            make.Text = "Обработать";
            make.Click += Make_click;
            Controls.Add(make);

            
        }


        private void Make_click(object sender, EventArgs eventArgs)
        {
            string local;
            Passport ps = new Passport();
            string a = (this.Controls[(sender as Button).Name + "a"].Text);
            if (a == String.Empty || a == null) ps.set_age(0);
            else ps.set_age(Convert.ToInt32(a));
            ps.set_name ((this.Controls[(sender as Button).Name+"n"]).Text);
            //    MessageBox.Show((this.Controls[(sender as Button).Name + " n"]).Text);

          //  ps.set_age(Convert.ToInt32((this.Controls[(sender as Button).Name + "a"]).Text ?? "0"));
        //    MessageBox.Show((this.Controls[(sender as Button).Name + " a"]).Text);
            ps.set_info((this.Controls[(sender as Button).Name + "i"]).Text);
            //    MessageBox.Show((this.Controls[(sender as Button).Name + " i"]).Text);
            local = (sender as Button).Name;
            local = local.Remove(local.Length - 2);
    //        MessageBox.Show(local);
            ps.set_Image(System.Drawing.Image.FromFile($"D:\\animals\\{local}.jpg"));
            ps.bn = local;
            Base.Add_t_B(ps);

        }
        
        public void Create_button()
        {
            int i1 = 0;
            int queue = 0;
            foreach (var i in DataBase.Base)
            {
                Button button = new Button();
                button.Size = new Size(180, 160);
                button.Location = new Point(200, 100 + i1);

                button.Name = i;
                i1 += 180;
                queue += 1;
                button.Click += ButtonOnClick;
                button.BackgroundImage = System.Drawing.Image.FromFile($"D:\\animals\\{i}.jpg");
                button.BackgroundImageLayout = ImageLayout.Stretch;
                Controls.Add(button);
                buttons.Add(button);


            }


        }
        private void Restart()
        {
            this.Controls.Clear();
            this.InitializeComponent();
            Choose.vkl = false;
            Appear.vkl = false;
            DataBase.Base.Clear();

        }


        abstract class functions // цепочка ответственности
        {
            public functions successor { get; set; }
            public abstract void handle(int a);
        }

        class Choose : functions
        {
            public static bool vkl;
            string nnum;
            Controller contr = new Controller();
            public override void handle(int a)
            {
                if (a == 1)
                {
                    vkl = true;
                    nnum = contr.Question("1101");
                }
                else if (successor != null) successor.handle(a);
                else MessageBox.Show("Такой команды нет.");
            }
        }

        class Appear : functions
        {
            public static bool vkl = false;
            public override void handle(int a)
            {
                if (a == 2)
                {

                    vkl = true;

                }
                else if (successor != null) successor.handle(a);
                else MessageBox.Show("Такой команды нет.");
            }
        }

        class Help : functions
        {
            public override void handle(int a)
            {
                if (a == 3)
                {
                    MessageBox.Show("Команды:\nВыбрать - внести в базу данных животных.\nПоказать-вывести конкретного животного.");
                }
                else if (successor != null) successor.handle(a);
                else MessageBox.Show("Такой команды нет.");
            }
        }


        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Controls.Clear();
            this.InitializeComponent();
        }

        public static class Base
        {
            public static List<Passport> Animals = new List<Passport>();
            public static void Add_t_B(Passport pas)
            {
                Animals.Add(pas);
                var re = File.ReadAllLines("D:\\base.txt", Encoding.Default).Where(s => !s.Contains($"{pas.bn}"));
                File.WriteAllLines("D:\\base.txt", re, Encoding.Default);

            }
        
        }

        public class Passport
        {
            public string name;
            private string button_name;
            public int age = 0;
            public string information;
            public Image IMG;

            public void set_name(string name) { if (this.name == null) this.name = name; }
            public void set_age(int age) { if (this.age == 0) this.age = age; }
            public void set_info(string information) { if (this.information == null) this.information = information; }
            public void set_Image(Image img) { if (IMG == null) IMG = img;}



            public string bn
            {
                get { return button_name; }
                set { button_name = value; }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}

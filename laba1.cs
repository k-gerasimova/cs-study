using System;
using System.Collections.Generic;
namespace laba4
{
    abstract class Human
    {
        public double Bill { get; set; }
        public abstract void Order(IFood food);
        public int table_num;
        public double Time { get; set; }
        public abstract void Info();
        public Human(int table_num)
        {
            this.table_num = table_num;
        }
    }

    class Client : Human
    {

            Institution z = null;
            public Client (int table_num, Institution a) : base(table_num) {
                z = a;
            }


        public override void Order(IFood food)
            {
                Bill += food.Cost;
                Time += food.Time;
                z.Make_order(food);
                
                
            }
           public override void Info()
            {
            Console.WriteLine($"Table {table_num} have an order of {Bill}$ and have to wait for {Time} minutes.");
            }
    }

    interface IFood
        {
            string Name { get; set; }
            int Cost { get; set; }
            int Time { get; set; }
        }

    class Food : IFood
        {
        private string name;
        private int price;
        private int time;
         public string Name
        {
            get{return name;}
            set {name = value;}
        }

        public int Cost
        {
            get{return price;}
            set{price = value;}
        }

        public int Time
        {
            get {return time;}
            set{time = value;}
        }

    }

    class Institution  //класс А - заведение
    {
        public string name = "";
        public string type = "";
        protected double money = 0;
        public List<IFood> Orders = new List<IFood> ();
        public Institution(string name, string type) {
            this.name = name;
            this.type = type;

        }
        public void Make_order(IFood food)
        {
            Orders.Add(food);
        }
        public virtual void Info()
        {
            foreach (var item in Orders)
                Console.WriteLine(item.Name);
        }
    }

    class Cafe : Institution
    {
        public Cafe(string name, string a = "cafe") : base(name, "cafe") { }
        protected List<IFood> menu = new List<IFood>();
        public int ttime = 0;
        public void AddM(IFood food)
        {
            menu.Add(food);
        }

        public override void Info() {
            Console.WriteLine("Menu of the cafe: \n");
            foreach (var item in menu)
                Console.WriteLine(item.Name);
            Console.WriteLine("\nOrders:\n");
            base.Info();
        }
        public virtual int Time()
        {
            foreach(var item in Orders)
            {
                ttime += item.Time;
            }
            return ttime;
        }

    } //расширение

    class Kitchen : Cafe
    {
        private string variety = ""; //к примеру, азиатская европейская и др
        public int workers = 0; //1 повар - 1 заказ
        private int time = 0, min =9999, count = 0;
        private IFood Fmin = null;
        private Cafe a = null;
        public Kitchen(string name, string variety, int workers, Cafe a) : base(name, "cafe") {
            this.variety = variety;
            this.workers = workers;
            this.a = a;
        }


        public override int Time() //количество времени, которое понадобится для выполнения всех заказов
        {
            if (a.Orders.Count > workers)
            {
                while (count < workers)
                {

                    if (time < a.Orders[count].Time) time = a.Orders[count].Time;
                    count++;
                }
                count = 0;
                while (a.Orders.Count > workers)
                {
                    while (count < workers)
                    {
                        if (a.Orders[count].Time < min) min = a.Orders[count].Time; Fmin = a.Orders[count];
                        count++;
                    }
                    count = 0;
                    a.Orders.Remove(Fmin);
                    if (min + a.Orders[workers - 1].Time > time) time = min + a.Orders[workers - 1].Time; a.Orders[workers - 1].Time += min;
                    min = 9999;
                }
            }
            foreach (var item in a.Orders)
                {
                    if (time < item.Time) time = item.Time;
                }

            return time;
        }

    } //конструирование

    class Coffee_house : Cafe //специализация
    {
        private IFood drink = null;
        public Coffee_house(string name, string a = "cafe") : base(name, "cafe") { }
        public override void Info()
        {
            Console.WriteLine($"You need to wait for you coffee for {Time()} minutes");
        }
       
        public void cof_info(string name)
        {
            foreach(var item in menu)
            {
                if (item.Name == name)
                {
                    drink = item;
                }
            }
            if (drink != null) Console.WriteLine($"Requested coffee costs {drink.Cost}$ and will be made in {drink.Time} minutes");
            else Console.WriteLine("Sorry, but Cofee house has not got such coffee.");
            drink = null;
        }

    }

    class Jap_strfood : Cafe //специализация
    {
        private Dictionary<int, string> Collab = new Dictionary<int, string>(5)
        {
            {1, "Cup with Keya" },
            {2, "Poster with Eola" },
            {3, "Figure with Hu Tao" },
            {4, "Card with Tartaglia" },
            {5, "Poster with Diluc" }

        };
        public Jap_strfood(string name, string a = "cafe") : base(name, "cafe") { }

        public double Combo(IFood food1, IFood food2, IFood drink) //акция комбо набора со скидкой
        {
            Orders.Add(food1);
            Orders.Add(food2);
            Orders.Add(drink);
            Console.WriteLine($"*You bought special combo so you get an unique card due to collaboration*");
            return (food1.Cost + food2.Cost + drink.Cost)*0.7;
        }

        public string Lotery_merch(int number)
        {
            Console.WriteLine($"*Congratulations! You've won {Collab[number]} in lottery.*");
            return Collab[number];
      }
    }
    class Program
    {
        static void Main(string[] args)
        {

                IFood dango = new Food(); dango.Time = 10; dango.Cost = 3; dango.Name = "dango";
                IFood Milk_tea = new Food(); Milk_tea.Time = 5; Milk_tea.Cost = 2; Milk_tea.Name = "Milk tea";
                IFood tayaki = new Food(); tayaki.Time = 15; tayaki.Cost = 4; tayaki.Name = "tayaki";
                IFood Ramen = new Food(); Ramen.Time = 25; Ramen.Cost = 15; Ramen.Name = "Ramen";
            //еда в японский стритфуд 
            
                IFood Latte = new Food(); Latte.Cost = 2; Latte.Name ="Latte" ; Latte.Time = 3;
                IFood Espresso = new Food(); Espresso.Cost = 1; Espresso.Name = "Espresso"; Espresso.Time = 2;
                IFood Mocha = new Food(); Mocha.Cost = 2; Mocha.Name = "Mocha"; Mocha.Time = 4;
                IFood Donut = new Food(); Donut.Cost = 2 ; Donut.Name = "Donut"; Donut.Time = 0;
            //еда в кофейню


            IFood salad = new Food(); salad.Cost = 4 ; salad.Name = "Caesar"; salad.Time = 15 ;
            IFood soup = new Food(); soup.Cost = 7; soup.Name = "gazpacho"; soup.Time = 30 ;
            IFood tea = new Food(); tea.Cost = 3 ; tea.Name = "tea" ; tea.Time = 5;
            IFood pie = new Food(); pie.Cost = 10 ; pie.Name = "pumpkin pie" ; pie.Time = 35;
            IFood meat = new Food(); meat.Cost = 10 ; meat.Name = "barbecue"; meat.Time = 25;
            //еда в кафе

            Food croissant = new Food(); croissant.Cost = 3; croissant.Time = 1; croissant.Name = "Chocolate croissant";
            Food cheesecake = new Food(); cheesecake.Cost = 2; cheesecake.Time = 1; cheesecake.Name = "cheesecake";
            Food muffin = new Food(); muffin.Cost = 3; muffin.Time = 2; muffin.Name = "muffin";
            //еда в пекарню

            Institution a = new Institution("Bakery", "bakery"); //продуктовый

            Human h1 = new Client(3, a);  
            h1.Order(muffin);
            Human h2 = new Client(4, a);
            h2.Order(croissant);

            Console.WriteLine("Function of A:");

            a.Info();
            Cafe c = new Cafe("white fox");
            Console.WriteLine("--------------------");
            Console.WriteLine("\nFunctions of B. Expansion A->B \n"); //расширение

            Human h3 = new Client(1, c);
            h3.Order(salad);
            Human h4 = new Client(2, c);
            h4.Order(soup);
            Human h5 = new Client(3, c);
            h5.Order(tea);
            Human h6 = new Client(4, c);
            h6.Order(pie);
            c.AddM(salad); c.AddM(soup); c.AddM(tea); c.AddM(pie); c.AddM(meat);
            c.Info();
            Console.WriteLine($"\nIt is needed {c.Time()} minutes to finish all of orders in {c.name}");

            Console.WriteLine("--------------------");

            Console.WriteLine("\nConstruction B->D. Function of D:\n"); //констуирование

            Kitchen b = new Kitchen("kitchen of" + c.name, "european", 3, c);
            Console.WriteLine($"It is needed {b.Time()} minutes to finish all of orders in {c.name} with {b.workers} workers in kitchen ");
            Console.WriteLine("--------------------");
            Console.WriteLine("\nSpecialization B->E. Functions of E:\n");

             Coffee_house e = new Coffee_house("Starbucks");
            e.AddM(Latte); e.AddM(Espresso); e.AddM(Mocha); e.AddM(Donut);
            e.Info();
            e.Make_order(Espresso);
            e.Info();
            Console.WriteLine("\n Information about Latte:");
            e.cof_info("Latte");
            Console.WriteLine("\n Information about Macchiato:");
            e.cof_info("Macchiato");

            Console.WriteLine("--------------------");
            Console.WriteLine("\nSpecialization B->F. Functions of F:\n");
            Jap_strfood Shokuyoku = new Jap_strfood("Shokuyoku");
            Shokuyoku.AddM(dango); Shokuyoku.AddM(Milk_tea); Shokuyoku.AddM(tayaki); Shokuyoku.AddM(Ramen);
            Console.WriteLine("Japaniese street food cafe has special event. If costumer orders a drink and 2 dishes he has 30% sale.\n");
            Console.WriteLine($"\nFor ordering {dango.Name}, {Ramen.Name} and {Milk_tea.Name} we have to pay {Shokuyoku.Combo(dango, Ramen, Milk_tea)}$ instead of {dango.Cost + Milk_tea.Cost + Ramen.Cost}$\n");
            Console.WriteLine("Moreover, cafe has special lottery. If customer buy a card with random number he can grant a prize:");
            Shokuyoku.Lotery_merch(3);
            Console.WriteLine("For buying card with 3.");
        }
    }
}

using System;
using System.Collections.Generic;
namespace laba8
{

    interface ISend
    {
        void Status_I();

        string Make_ID();

        bool Status { get; set; }
        string Type { get; set; }
        string ID { get; set; }
    }
    interface ISender
    {
        string Name { get; set; }
        public void Send<T>(T t) where T:ISend;
    }
    class Person:ISender
    {
        private string name = "";
       // Post<Person, ISend> post = new Post<Person,ISend>();
        public string Name { get { return name; } set { name = value; } }
        public void Send<send>(send b) where send:ISend { Post<Person, ISend>.Send(b, this); }

    }
    class Entity :ISender
    {
     //   Post<Entity,ISend> post = new Post<Entity,ISend>();
        string name = "";
        public Entity () {}
        public void Add_Factory<T>(Factory<T> M) where T:Present
        {
            Fact = M;
        }
        public object Fact = null;
        public List<Present> Presents = new List<Present>();
        public void To_Post<T>(Factory<T> a) where T : Present { Post<Entity,ISend>.transfer<Factory<T>, T>(a); }
        public void Send<T>(T t) where T:ISend
        {
            Post<Entity,T>.Send(t, this);
        }
        public void To_Factory<T>(T a, Factory<T> b) where T:Present { b.Lenta(a); }
        public string Name { get { return name; } set { name = value; } }

    }

    class Present : ISend //подарок
    {
        public Present(string name) { this.name = name; }
        public string name = "";
        private string id = "-";
        public void Status_I() 
        {
            if (status == true) Console.WriteLine($"the package of {name} has been sent");
            else Console.WriteLine($"the package of {name} has not been sent");
        }
        private string packing = "";

        private bool status = false;
        private string type = "";
        public virtual string Make_ID() { return id; }
        public string Packing
        {
            get { return packing; }
            set { packing = value; }
        }
        public virtual string Type
        {
            get { return type; }
            set { }
        }
        public bool Status
        {
            get { return status; }
            set { status = value; }
        }
        public virtual string ID
        {
            get { return id; }
            set { }
        }
    }

    class Puzzle : Present
    {
        public Puzzle(string name):base(name) { }
        private string type = "parsel";
        private string id = "";
        public override string Make_ID()
        {
            Random R = new Random();
            int RR = R.Next();
            ID = RR.ToString();
            return RR.ToString();
        }
        public override string Type
        {
            get { return type; }
            set {  } //бандероль
        }
        public override string ID
        {
            get { return id; }
            set { id = value; }
        }
    }

    class Robot :Present
    {
        public string id = "";
        public Robot(string name) : base(name) { }
        private string type = "small package";
        public override string Make_ID()
        {
            Guid g = Guid.NewGuid();
            string ID = Convert.ToBase64String(g.ToByteArray());
            ID = ID.Replace("=", "");
            ID = ID.Replace("+", "");
            ID = ID.Substring(0, 10);
            this.ID = ID;
            return ID;
        }
        public override string Type
        {
            get { return type; }
            set {}
        }
        public override string ID
        {
            get { return id; }
            set { id = value; }
        }
    }

    static class Post<sender, send>
        where sender: ISender
        where send: ISend
    {
        public static List<ISend>  shipment = new List<ISend>();
        public static void transfer<A, T>(A a) where A:Factory<T> where T:Present
        {
            Console.WriteLine($"Starting the process of transfering from {a.owner.Name}...");
            foreach (Present item in a.owner.Presents) { item.Status = true; shipment.Add(item); Console.WriteLine($"{item.Type} \"{item.name}\" is transfering. ID - {maketrack<Present>(item)}"); }
            a.owner.Presents.Clear();
            Console.WriteLine($"All presents were sended.\n");
        }
        public static string maketrack<M>(M Send) where M:ISend
        {
                return Send.Make_ID();
        }
        public static void Send(send M, sender K)
        {
            M.Status = true;
            shipment.Add(M);
            maketrack(M);
            Console.WriteLine($"{K.Name} sended a {M.Type}.");
        }
    }

    class Factory<good> where good: Present
    {
        public Factory()
        {
        }
        public Entity owner = null;
        public void SetOwn(Entity B)
        {
            owner = B;
        }
        public void Lenta(good Pr)
        {
            if (Pr is Robot) { Pr.Packing = "Blue"; owner.Presents.Add(Pr); }
            else if (Pr is Puzzle) { Pr.Packing = "Red"; owner.Presents.Add(Pr); }
        }
    }

    class Letter<Reciever> : ISend
        where Reciever:ISender
    {
        public Letter(Reciever a)
        {
            R = a;
        }
        public void Status_I() 
        {
            if (status == true) Console.WriteLine($"the letter to {R} has been sent");
            else Console.WriteLine($"the letter to {R} has not been sent");
        }
        private bool status = false;
        public Reciever R;
        private string type = "letter";
        static private string id = "-";
        public string Make_ID() { return id; }
        public string Type
        {
            get { return type; }
            set { }
        }
        public bool Status
        {
            get { return status; }
            set { status = value; }
        }
        public string ID
        {
            get { return id; }
            set { }
        }
    }
    class Program
    {

        static void Main(string[] args)
        {
            Entity A = new Entity { Name = "OAO Gift" };
            Entity B = new Entity { Name = "OAO Presents" };
            Factory<Robot> M = new Factory<Robot>();
            Person Jack = new Person { Name = "Jack" };
            Factory<Present> Z = new Factory<Present>();
            Z.SetOwn(B);
            M.SetOwn(A);
            Person Leo = new Person { Name = "Leo" };


            Robot Transformer = new Robot("Transformer");
            Robot Dog = new Robot("Dog");
            Puzzle Lion = new Puzzle("Lion");
            Puzzle Cat = new Puzzle("Cat");
            Robot Car = new Robot("Car");
            Robot Lego = new Robot("Lego");
            Letter<Person> a1 = new Letter<Person>(Leo);
            Letter<Entity> a2 = new Letter<Entity>(A);

            A.To_Factory(Transformer,M) ; A.To_Factory(Car, M);  B.To_Factory(Dog,Z); B.To_Factory(Lion, Z); B.To_Factory(Cat, Z);
            Console.WriteLine($"Presents packed in different colours:\n{Transformer.name} is {Transformer.Packing}.\n{Lion.name} is {Lion.Packing}\n");

            A.To_Post(M); B.To_Post(Z);

            Console.WriteLine($"Working with personal parcels:\nCurrent {Lego.name} status:");
            Lego.Status_I();
            Jack.Send<Robot>(Lego);
            Lego.Status_I();
            Console.WriteLine();
            Jack.Send<Letter<Person>>(a1);
            Leo.Send<Robot>(Car);
            Jack.Send<Puzzle>(Cat);
            Console.WriteLine($"\nWe can track the Jack's and Leo's parcels by codes:\n{Cat.ID} - {Cat.Type} {Cat.name}\n{Car.ID} - {Car.Type} {Car.name}\nLetter   {a1.ID}");



        }
    }
}
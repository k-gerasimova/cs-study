namespace Bank
{

    public class Communication
    {
        private readonly IQueryTool user;
        public Communication()
        {
            user = new QueryTool();
            user.ListCompletion();
        }
        private void StartMenu()
        {
            Console.WriteLine("------------------------------------");
            Console.WriteLine("Выберите операцию:");
            Console.WriteLine("1. Добавить новый товар"); 
            Console.WriteLine("2. Удалить заказ"); 
            Console.WriteLine("3. Обновить товар"); 
            Console.WriteLine("4. Показать все заказы"); 
            Console.WriteLine("5. Показать информацию об одном товаре"); 
            Console.WriteLine("Для выхода из программы нажмите ESC");
        }

        private void InsertMenu()
        {


           // int userId = user.InsertQuery(fullName, Class, price, col);

            Console.WriteLine("Товар успешно добавлен!");
           // Console.WriteLine($"Id добавленного пользователя: {userId}");

        }

        private void DeleteMenu()
        {
            Console.WriteLine("Введите ID заказа, который хотите удалить.");
            int Id = Convert.ToInt32(Console.ReadLine());
            user.DeleteQuery(Id);
            Console.WriteLine();
            Console.WriteLine("Заказ успешно удален!");
        }
        private void UpdateMenu()
        {
            Console.WriteLine("Введите название товара:");
            int it_name = int.Parse(Console.ReadLine());
            Console.WriteLine();
            Console.WriteLine("------------------------------------");
            Console.WriteLine("Какие данные вы ходите обновить?");
            Console.WriteLine("1. Цену");
            Console.WriteLine("2. количество");
            Console.WriteLine("3. Классификацию");
            int userAnswer = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine();

            if (userAnswer == 1)
            {
                Console.WriteLine("Введите новую цену");
                string newCity = Console.ReadLine();
                Console.WriteLine();
               // user.UpdateQuery(it_name, newCity, newCountry);
            }
            else
            {
                Console.WriteLine("Введите новые данные");
                string newUserData = Console.ReadLine();
              //  user.UpdateQuery(userId, userAnswer, newUserData);
            }
            Console.WriteLine("Данные успешно обновлены!");

        }
        private void SelectUserMenu()
        {
            Console.WriteLine("Введите название товара");
            string name = Console.ReadLine();
            user.SingleSelectQuery(name);
            Console.WriteLine();
        }
        public void Dialog()
        {
            ConsoleKeyInfo escape = Console.ReadKey();
            Console.ReadLine();
            while (escape.Key != ConsoleKey.Escape)
            {
                StartMenu();
                Console.WriteLine();
                escape = Console.ReadKey();
                Console.WriteLine();

                if (escape.Key == ConsoleKey.D1)
                {
                    Console.WriteLine();
                    InsertMenu();
                }
                else if (escape.Key == ConsoleKey.D2)
                {
                    Console.WriteLine();
                    DeleteMenu();
                }
                else if (escape.Key == ConsoleKey.D3)
                {
                    Console.WriteLine();
                    UpdateMenu();
                }
                else if (escape.Key == ConsoleKey.D4)
                {
                    Console.WriteLine();
                    user.GlobalSelectQuery();
                }
                else if (escape.Key == ConsoleKey.D5)
                {
                    Console.WriteLine();
                    SelectUserMenu();
                }
            }
        }
    }
}

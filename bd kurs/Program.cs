namespace Bank;

internal class Program
{


    private static void Main(string[] args)
    {
         //Communication test = new();
        // test.Dialog();
        Application.SetHighDpiMode(HighDpiMode.SystemAware);
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Form1 myForm = new Form1();

        Application.Run(myForm);
    }       

}
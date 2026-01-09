namespace ConsoleExpenseTracker
{
    class ConsoleExpenseTracker
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;

            TransactionsView view = new TransactionsView();
            view.Run();
        }
    }
}

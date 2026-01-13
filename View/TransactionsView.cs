namespace ConsoleExpenseTracker
{
    public class TransactionsView
    {
        private TransactionService _service = new TransactionService();
        
        public void Run()
        {
            _service.LoadFromFile();
            ShowMenu();

            while (true)
            {
                Console.Write("Введіть номер функції: ");
                string? userInput = Console.ReadLine();

                if (int.TryParse(userInput, out int input))
                {
                    switch (input)
                    {
                        case 1:
                            AddTransactionView();
                            break;
                        case 2:
                            ShowTransactionsView();
                            break;
                        case 3:
                            ShowRemoveTransactionView();
                            break;
                        case 4:
                            ShowBalanceView();
                            break;
                        case 5:
                            _service.SaveToFile();
                            Console.WriteLine("До побачення.");
                            return;
                        default:
                            Console.WriteLine("Такої функції немає!");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Введіть коректно номер функції!");
                }
            }
        }

        private void ShowMenu()
        {
            Console.WriteLine("1. Додати транзакцію.");
            Console.WriteLine("2. Показати список.");
            Console.WriteLine("3. Видалити транзакцію.");
            Console.WriteLine("4. Баланс.");
            Console.WriteLine("5. Вихід.");
            Console.WriteLine();
        }

        private void AddTransactionView()
        {
            Console.Write("Введіть назву транзакції: ");
            string? title = Console.ReadLine();

            Console.Write("Введіть cуму транзакції: ");
            decimal amount;
            while (!decimal.TryParse(Console.ReadLine(), out amount))
            {
                Console.Write("Некоректна сума. Спробуйте ще раз: ");
            }

            Console.Write("Введіть тип транзакції (1 - Income / 2 - Expense): ");
            TransactionType type = TransactionType.Income;
            bool typeBoolean = true;
            while (typeBoolean)
            {
                string? typeOfTransaction = Console.ReadLine();
                if (typeOfTransaction == "1")
                {
                    type = TransactionType.Income;
                    typeBoolean = false;
                }
                else if (typeOfTransaction == "2")
                {
                    type = TransactionType.Expense;
                    typeBoolean = false;
                }
                else
                {
                    Console.Write("Данні введено не коректно, спробуйте ще раз: ");
                }
            }

            _service.AddTransaction(title, amount, type);
            Console.WriteLine("Транзакцію успішно додано!\n");
        }

        private void ShowTransactionsView()
        {
            var transactions = _service.GetAllTransactions();

            if (transactions.Count == 0)
            {
                Console.WriteLine("Поки що немає транзакцій!\n");
                return;
            }

            foreach (var t in transactions)
            {
                Console.WriteLine($"{t.Id}. {t.Title} | {t.Amount} | {t.Type} | {t.Date}");
            }
            Console.WriteLine();
        }

        private void ShowBalanceView()
        {
            decimal balance = _service.CountBalance();
            Console.WriteLine($"Поточний баланс: {balance}\n");
        }

        private void ShowRemoveTransactionView() 
        {
            Console.Write("Введіть ID транзакції для видалення: ");
            string? userInput = Console.ReadLine();

            if (int.TryParse(userInput, out int id) && _service.GetAllTransactions().Any(t => t.Id == id))
            {
                _service.RemoveTransactionByID(id);
                Console.WriteLine("Транзакцію успішно видалено!\n");
            }
            else
            {
                Console.WriteLine("Некоректний ID. Спробуйте ще раз.\n");
            }
        }
    }
}
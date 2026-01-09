namespace ConsoleExpenseTracker
{
    public class TransactionsView
    {
        private TransactionService _service = new TransactionService();
        
        public void Run()
        {
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
                            ShowBalanceView();
                            break;
                        case 4:
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
            Console.WriteLine("3. Баланс.");
            Console.WriteLine("4. Вихід.");
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

            Console.Write("Введіть тип транзакції (Income/Expense): ");
            string type = string.Empty;
            bool typeBoolean = true;
            while (typeBoolean)
            {
                string? typeOfTransaction = Console.ReadLine();
                if (typeOfTransaction!.ToLower() == "income" || typeOfTransaction.ToLower() == "expense")
                {
                    type = typeOfTransaction.ToUpper();
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

            int id = 1;
            foreach (var t in transactions)
            {
                Console.WriteLine($"{id}. {t.Title} | {t.Amount} | {t.Type} | {t.Date}");
                id++;
            }
            Console.WriteLine();
        }

        private void ShowBalanceView()
        {
            decimal balance = _service.CountBalance();
            Console.WriteLine($"Поточний баланс: {balance}\n");
        }
    }
}
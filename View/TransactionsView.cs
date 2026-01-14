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
                            ShowFoundTransactionView();
                            break;
                        case 5:
                            ShowBalanceView();
                            break;
                        case 6:
                            ShowAnalyzeView();
                            break;
                        case 7:
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
            Console.WriteLine("4. Знайти транзакцію за назвою.");
            Console.WriteLine("5. Баланс.");
            Console.WriteLine("6. Аналітика витрат.");
            Console.WriteLine("7. Вихід.");
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

            Console.Write("Введіть тип транзакції (1 - Прибуток / 2 - Витрата): ");
            TransactionType type = TransactionType.Income;
            TransactionCategory category = TransactionCategory.Other;
            bool typeBoolean = true;
            while (typeBoolean)
            {
                string? typeOfTransaction = Console.ReadLine();
                if (typeOfTransaction == "1")
                {
                    type = TransactionType.Income;
                    Console.Write("Який саме прибуток (1 - Зарплата / 2 - Подарунок): ");
                    while(true)
                    {
                        string? categoryInput = Console.ReadLine();
                        if (categoryInput == "1" )
                        {
                            category = TransactionCategory.Salary;
                            typeBoolean = false;
                            break;
                        }
                        else if(categoryInput == "2")
                        {
                            category = TransactionCategory.Present;
                            typeBoolean = false;
                            break;
                        }
                        else
                        {
                            Console.Write("Данні введено не коректно, спробуйте ще раз: ");
                        }
                    }
                }
                else if (typeOfTransaction == "2")
                {
                    type = TransactionType.Expense;
                    Console.Write("На що витрачаються кошти (1 - Їжа / 2 - Транспорт / Будь-яка інша клавіша - Інше): ");
                    string? categoryInput = Console.ReadLine();
                    
                    if (categoryInput == "1")
                    {
                        category = TransactionCategory.Food;
                    }
                    else if (categoryInput == "2")
                    {
                        category = TransactionCategory.Transport;
                    }
                    else
                    {
                        category = TransactionCategory.Other;
                    }
                    typeBoolean = false;
                }
                else
                {
                    Console.Write("Данні введено не коректно, спробуйте ще раз: ");
                }
            }

            _service.AddTransaction(title, amount, type, category);
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
                Console.WriteLine($"{t.Id}. {t.Title} | {t.Amount} | {t.Type} | {t.Category} | {t.Date}");
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

        private void ShowFoundTransactionView() 
        {
            Console.Write("Введіть назву транзакції для її пошуку: ");
            string? userInput = Console.ReadLine();

            var foundTransactions = _service.FindByName(userInput!);

            if (foundTransactions.Count > 0)
            {
                foreach (var t in foundTransactions)
                {
                    Console.WriteLine($"{t.Id}. {t.Title} | {t.Amount} | {t.Type} | {t.Category} | {t.Date}");
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Транзакцій з такою назвою не знайдено!\n");
            }
        }

        private void ShowAnalyzeView()
        {
            Console.WriteLine("Аналітика витрат:");

            var analyzeByExpense = _service.AnalyzeByExpenseCategory();

            if(analyzeByExpense.Count > 0)
            {
                foreach(var t in analyzeByExpense)
                    Console.WriteLine($"{t.Key}: {t.Value}");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Витрат не знайдено!");
            }
        }
    }
}
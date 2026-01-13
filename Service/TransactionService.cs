using System.Text.Json;

namespace ConsoleExpenseTracker
{
    public class TransactionService
    {
        private List<Transaction> _transactions = new List<Transaction>();

        public void AddTransaction(string? title, decimal amount, TransactionType type)
        {
            _transactions.Add(new Transaction(title, amount, type));
        }

        public List<Transaction> GetAllTransactions()
        {
            return _transactions;
        }

        public decimal CountBalance()
        {
            decimal income = _transactions.Where(t => t.Type == TransactionType.Income).Sum(t => t.Amount);
            decimal expense = _transactions.Where(t => t.Type == TransactionType.Expense).Sum(t => t.Amount);

            return income - expense;
        }

        public void SaveToFile()
        {
            string jsonString = JsonSerializer.Serialize(_transactions);
            File.WriteAllText("transactions.json", jsonString);
        }

        public void LoadFromFile()
        {
            if (!File.Exists("transactions.json"))
            {
                _transactions = new List<Transaction>();
                return;
            }

            try
            {
                string jsonString = File.ReadAllText("transactions.json");
                _transactions = JsonSerializer.Deserialize<List<Transaction>>(jsonString) ?? new List<Transaction>();
            }
            catch (JsonException)
            {
                _transactions = new List<Transaction>();
            }
        }
    }
}

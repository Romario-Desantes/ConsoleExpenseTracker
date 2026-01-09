namespace ConsoleExpenseTracker
{
    public class TransactionService
    {
        private List<Transaction> _transactions = new List<Transaction>();

        public void AddTransaction(string? title, decimal amount, string? type)
        {
            _transactions.Add(new Transaction(title, amount, type));
        }

        public List<Transaction> GetAllTransactions()
        {
            return _transactions;
        }

        public decimal CountBalance()
        {
            decimal income = _transactions.Where(t => t.Type == "INCOME").Sum(t => t.Amount);
            decimal expense = _transactions.Where(t => t.Type == "EXPENSE").Sum(t => t.Amount);

            return income - expense;
        }
    }
}

namespace ConsoleExpenseTracker
{
    public enum TransactionType
    {
        Income,
        Expense
    }

    public class Transaction
    {
        public string? Title { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public TransactionType Type { get; set; }

        public Transaction() { }

        public Transaction(string? title, decimal amount, TransactionType type)
        {
            Title = title;
            Amount = amount;
            Date = DateTime.Now;
            Type = type;
        }
    }
}

namespace ConsoleExpenseTracker
{
    public enum TransactionCategory
    {
        Food,
        Entertainment,
        Transport,
        Salary,
        Present,
        Other
    }

    public enum TransactionType
    {
        Income,
        Expense
    }

    public class Transaction
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public TransactionType Type { get; set; }
        public TransactionCategory Category { get; set; } 

        public Transaction() { }

        public Transaction(int id, string? title, decimal amount, TransactionType type, TransactionCategory category)
        {
            Id = id;
            Title = title;
            Amount = amount;
            Date = DateTime.Now;
            Category = category;
            Type = type;
        }
    }
}

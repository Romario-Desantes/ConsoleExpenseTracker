namespace ConsoleExpenseTracker
{
    public class Transaction
    {
        public string? Title { get; set; }
        public decimal Amount {  get; set; }
        public DateTime Date { get; set; }
        public string? Type;

        public Transaction() { }

        public Transaction(string? title, decimal amount, string? type)
        {
            Title = title;
            Amount = amount;
            Date = DateTime.Now;
            Type = type;
        }
    }
}

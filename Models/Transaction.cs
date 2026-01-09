namespace ConsoleExpenseTracker
{
    public class Transaction
    {
        public string? Title { get; set; }
        public decimal Amount {  get; set; }
        public DateTime Date { get; set; }
        public string? Type;

        public Transaction() { }
        public Transaction(string? tittle, decimal amount, string? type)
        {
            Title = tittle;
            Amount = amount;
            Date = DateTime.Now;
            Type = type;
        }
    }
}

namespace NHibernateDemo.Domain
{
    public class Money
    {
        public Money(decimal amount, string type)
        {
            Value = amount;
            CurrencyType = type;
        }

        public decimal Value { get; set; }
        public string CurrencyType { get; set; }
    }
}
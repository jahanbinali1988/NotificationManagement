namespace Common.Domain.Utilities
{
    public static class MoneyFactory
    {
        public static Money CreateIranCurrency(long amount)
        {
            return new Money(amount, CurrencyCode.Rial);
        }
    }
}

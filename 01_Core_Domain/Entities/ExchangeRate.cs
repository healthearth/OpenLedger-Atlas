namespace OpenLedgerAtlas.Domain.Entities
{
    public class ExchangeRate
    {
        public int Id { get; set; }
        public string FromCurrency { get; set; } = string.Empty;
        public string ToCurrency { get; set; } = string.Empty;
        public decimal Rate { get; set; }
        public DateTime Timestamp { get; set; }
    }
}

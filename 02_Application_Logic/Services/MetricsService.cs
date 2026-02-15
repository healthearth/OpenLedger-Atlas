namespace OpenLedgerAtlas.Application.Services
{
    public class MetricsService
    {
        public object GetMetrics()
        {
            return new
            {
                System = "OpenLedger Atlas",
                Status = "Healthy",
                Timestamp = DateTime.UtcNow
            };
        }
    }
}

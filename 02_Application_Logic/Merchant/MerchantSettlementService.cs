//
public class MerchantSettlementService
{
    private readonly IBankingOperations _ops;

    public async Task<bool> ProcessPayment(
        Guid customer,
        Guid merchant,
        decimal amount)
    {
        return await _ops.TransactAsync(customer, merchant, amount);
    }
}

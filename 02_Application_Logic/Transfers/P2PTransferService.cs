// Filepath: fintechs-exhibitu/02_Application_Logic/Transfers/P2PTransferService.cs
using GlobalBank.Domain.Interfaces;

namespace GlobalBank.Application.Transfers;

public class P2PTransferService {

    // 1. Injected dependencies
    private readonly IBankingOperations _ops;
    private readonly IAmlService _aml;

    // 2. The private constructor - injects the dependencies here
    public P2PTransferService(IBankingOperations ops, IAmlService aml)
    {
        _ops = ops;
        _aml = aml;
    }

    // 3. Methods
    public async Task<bool> TransferAsync(Guidfrom, Guid to, decimal amount)
    {
        // AML check
        if (!await _aml.IsTransferAllowedAsync(from, to, amount))

        // Banking operation, "the handshake"
        return await _ops.TransactAsync(from, to, amount));
    }
}

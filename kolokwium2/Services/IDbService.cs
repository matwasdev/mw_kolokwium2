using kolokwium2.DTOs;

namespace kolokwium2.Services;

public interface IDbService
{
    Task<CustomerPurchasesDto> GetCustomerPurchasesAsync(int customerId);
    Task CreateWashingMachineAsync(CreateWashingMachineRequestDto requestDto);
}
using kolokwium2.Data;
using kolokwium2.DTOs;
using kolokwium2.Exceptions;
using kolokwium2.Models;
using Microsoft.EntityFrameworkCore;

namespace kolokwium2.Services;

public class DbService : IDbService
{
    private readonly DatabaseContext _context;

    public DbService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<CustomerPurchasesDto> GetCustomerPurchasesAsync(int customerId)
    {
        var customer = await _context.Customers.FirstOrDefaultAsync(e => e.CustomerId==customerId);
        if(customer == null)
            throw new NotFoundException($"Customer with id {customerId} not found");
        
        var purchases = await _context.PurchaseHistories
            .Include(e => e.AvailableProgram)
            .ThenInclude(ap => ap.ProgramEntity)
            .Include(e => e.AvailableProgram)
            .ThenInclude(ap => ap.WashingMachine)
            .Where(e => e.CustomerId == customerId)
            .ToListAsync();

        var customerPurchase = new CustomerPurchasesDto()
        {
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            PhoneNumber = customer.PhoneNumber,
            Purchases = purchases.Select(e => new PurchaseDto()
            {
                Date = e.PurchaseDate,
                Rating = e.Rating,
                Price = e.AvailableProgram.Price,
                WashingMachine = new WashingMachineDto()
                {
                    Serial = e.AvailableProgram.WashingMachine.SerialNumber,
                    MaxWeight = e.AvailableProgram.WashingMachine.MaxWeight
                },
                Program = new ProgramEntityDto()
                {
                    Name = e.AvailableProgram.ProgramEntity.Name,
                    Duration = e.AvailableProgram.ProgramEntity.DurationMinutes
                }
            }).ToList()
        };
        
        return customerPurchase;
    }


    public async Task CreateWashingMachineAsync(CreateWashingMachineRequestDto requestDto)
    {
        var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var doesWMExist =
                await _context.WashingMachines.AnyAsync(w => w.SerialNumber == requestDto.WashingMachine.SerialNumber);
            if (doesWMExist)
                throw new ConflictException("Washing machine with serial number already exists");

            if (requestDto.WashingMachine.MaxWeight <8)
                throw new BadRequestException("Weight must be at least 8");
            
            foreach (var avprogram in requestDto.AvailablePrograms)
            {
                var doesAvProgramExist = await _context.ProgramsEntities.AnyAsync(e => e.Name == avprogram.ProgramName);
                if(!doesAvProgramExist)
                    throw new NotFoundException($"Program with name {avprogram.ProgramName} not found");
                
                if (avprogram.Price > 25)
                    throw new BadRequestException("Program price cannot be greater than 25");
            }


            var washingMachine = new WashingMachine()
            {
                MaxWeight = requestDto.WashingMachine.MaxWeight,
                SerialNumber = requestDto.WashingMachine.SerialNumber,
            };
            
            _context.WashingMachines.Add(washingMachine);
            await _context.SaveChangesAsync();
            
            foreach (var prog in requestDto.AvailablePrograms)
            {
                var program = await _context.ProgramsEntities.FirstOrDefaultAsync(e => e.Name == prog.ProgramName);
                if(program == null)
                    throw new NotFoundException($"Program with name {prog.ProgramName} not found");

                var avProgram = new AvailableProgram()
                {
                    WashingMachineId = washingMachine.WashingMachineId,
                    ProgramId = program.ProgramId,
                    Price = prog.Price,
                };
                
                _context.AvailablePrograms.Add(avProgram);
                await _context.SaveChangesAsync();
            }
            
            
            await _context.SaveChangesAsync();
            
            await transaction.CommitAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            await transaction.RollbackAsync();
            throw;
        }
    }
}
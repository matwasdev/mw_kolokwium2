using kolokwium2.Models;
using Microsoft.EntityFrameworkCore;

namespace kolokwium2.Data;

public class DatabaseContext : DbContext
{
    public DbSet<WashingMachine> WashingMachines { get; set; }
    public DbSet<AvailableProgram> AvailablePrograms { get; set; }
    public DbSet<ProgramEntity> ProgramsEntities { get; set; }
    public DbSet<PurchaseHistory> PurchaseHistories { get; set; }
    public DbSet<Customer> Customers { get; set; }
    
    protected DatabaseContext(){}
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options){}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<WashingMachine>().HasData(new List<WashingMachine>()
        {
            new WashingMachine(){WashingMachineId = 1, MaxWeight = 10.5, SerialNumber = "111"},
            new WashingMachine(){WashingMachineId = 2, MaxWeight = 20.5, SerialNumber = "222"},
            new WashingMachine(){WashingMachineId = 3, MaxWeight = 30.5, SerialNumber = "333"},
        });

        modelBuilder.Entity<ProgramEntity>().HasData(new List<ProgramEntity>()
        {
            new ProgramEntity(){ProgramId = 1, Name = "Program1", DurationMinutes = 10, TemperatureCelsius = 100},
            new ProgramEntity(){ProgramId = 2, Name = "Program2", DurationMinutes = 20, TemperatureCelsius = 200},
            new ProgramEntity(){ProgramId = 3, Name = "Program3", DurationMinutes = 30, TemperatureCelsius = 300},
        });

        modelBuilder.Entity<Customer>().HasData(new List<Customer>()
        {
            new Customer() { CustomerId = 1, FirstName = "John", LastName = "Doe", PhoneNumber = "1111" },
            new Customer() { CustomerId = 2, FirstName = "John2", LastName = "Doe2", PhoneNumber = "2222" },
            new Customer() {CustomerId = 3,FirstName = "John3", LastName = "Doe3", PhoneNumber = "3333"},
        });

        modelBuilder.Entity<AvailableProgram>().HasData(new List<AvailableProgram>()
        {
            new AvailableProgram(){AvailableProgramId = 1, WashingMachineId = 1, ProgramId = 1, Price = 100.5},
            new AvailableProgram(){AvailableProgramId = 2, WashingMachineId = 1, ProgramId = 2, Price = 100.25},
            new AvailableProgram(){AvailableProgramId = 3, WashingMachineId = 2, ProgramId = 2, Price = 300.88},
            new AvailableProgram(){AvailableProgramId = 4, WashingMachineId = 3, ProgramId = 3, Price = 400.20},
        });

        modelBuilder.Entity<PurchaseHistory>().HasData(new List<PurchaseHistory>()
        {
            new PurchaseHistory(){AvailableProgramId = 1, CustomerId = 1, PurchaseDate = DateTime.Parse("2021-01-01"),Rating = 5},
            new PurchaseHistory(){AvailableProgramId = 2, CustomerId = 2, PurchaseDate = DateTime.Parse("2022-01-01"),Rating = 4},
            new PurchaseHistory(){AvailableProgramId = 3, CustomerId = 3, PurchaseDate = DateTime.Parse("2023-01-01"),Rating = 3},
        });
    }
    
}
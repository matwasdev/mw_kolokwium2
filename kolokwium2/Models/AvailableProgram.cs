using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kolokwium2.Models;

[Table("Available_Program")]
public class AvailableProgram
{
    [Key]
    public int AvailableProgramId { get; set; }
    
    [ForeignKey(nameof(WashingMachine))]
    public int WashingMachineId { get; set; }
    
    [ForeignKey(nameof(ProgramEntity))]
    public int ProgramId { get; set; }
    
    [Column(TypeName = "decimal(10,2)")]
    public double Price { get; set; }
    
    public WashingMachine WashingMachine { get; set; }
    
    public ProgramEntity ProgramEntity { get; set; }
    
    public ICollection<PurchaseHistory> PurchaseHistories { get; set; }
}
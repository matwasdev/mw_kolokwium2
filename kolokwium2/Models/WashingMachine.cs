using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kolokwium2.Models;

[Table("Washing_Machine")]
public class WashingMachine
{
    [Key]
    public int WashingMachineId { get; set; }
    
    [Column(TypeName = "decimal(10, 2)")]
    public double MaxWeight { get; set; }
    
    [MaxLength(100)]
    public string SerialNumber { get; set; }
    
    public ICollection<AvailableProgram> AvailablePrograms { get; set; }
}
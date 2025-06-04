using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace kolokwium2.Models;


[PrimaryKey(nameof(AvailableProgramId), nameof(CustomerId))]
[Table("Purchase_History")]
public class PurchaseHistory
{
    [ForeignKey(nameof(AvailableProgram))]
    public int AvailableProgramId { get; set; }
    
    [ForeignKey(nameof(Customer))]
    public int CustomerId { get; set; }
    
    public DateTime PurchaseDate { get; set; }
    
    public int? Rating { get; set; }
    
    public AvailableProgram AvailableProgram { get; set; }
    public Customer Customer { get; set; }
}
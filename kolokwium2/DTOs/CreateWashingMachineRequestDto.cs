using System.ComponentModel.DataAnnotations;

namespace kolokwium2.DTOs;

public class CreateWashingMachineRequestDto
{
    [Required]
    public WashingMachineCreateDto WashingMachine { get; set; }
    
    [Required]
    public List<AvailableProgramDto> AvailablePrograms { get; set; }
    
}
public class WashingMachineCreateDto{
    
    [Required]
    public double MaxWeight { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string SerialNumber { get; set; }
}

public class AvailableProgramDto
{
    [Required]
    [MaxLength(50)]
    public string ProgramName { get; set; }
    
    [Required]
    public double Price { get; set; }
}
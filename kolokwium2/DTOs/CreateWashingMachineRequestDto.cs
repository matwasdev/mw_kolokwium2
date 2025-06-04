namespace kolokwium2.DTOs;

public class CreateWashingMachineRequestDto
{
    public WashingMachineCreateDto WashingMachine { get; set; }
    public List<AvailableProgramDto> AvailablePrograms { get; set; }
    
}
public class WashingMachineCreateDto{
    public double MaxWeight { get; set; }
    public string SerialNumber { get; set; }
}

public class AvailableProgramDto
{
    public string ProgramName { get; set; }
    public double Price { get; set; }
}
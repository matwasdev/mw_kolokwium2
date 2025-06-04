﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kolokwium2.Models;

[Table("Program")]
public class ProgramEntity
{
    [Key]
    public int ProgramId { get; set; }
    
    [MaxLength(50)]
    public string Name { get; set; }
    
    public int DurationMinutes { get; set; }
    
    public int TemperatureCelsius { get; set; }
    
    public ICollection<AvailableProgram> AvailablePrograms { get; set; }
}
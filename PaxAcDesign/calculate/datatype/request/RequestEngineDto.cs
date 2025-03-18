using System.ComponentModel.DataAnnotations;
namespace PaxAcDesign.calculate.datatype.request;

public record RequestEngineDto()
{

    [Display(Name = "Тип двигателя")]
    public EngineType EngineType { get; set; } = EngineType.Turbojet;

    [Display(Name = "Количество двигателей")]
    public int NumberOfEngines { get; set; }

    [Display(Name = "Степень двухконтурности")]
    public double BypassRatio { get; set; }

}
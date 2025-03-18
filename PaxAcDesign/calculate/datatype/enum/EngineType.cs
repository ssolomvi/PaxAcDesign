using System.ComponentModel.DataAnnotations;

namespace PaxAcDesign.calculate.datatype;

public enum EngineType
{
    
    [Display(Name = "Турбореактивный двигатель")]
    Turbojet,

    [Display(Name = "Турбовинтовой двигатель")]
    Turboprop
    
}
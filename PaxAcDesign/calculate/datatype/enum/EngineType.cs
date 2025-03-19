using System.ComponentModel.DataAnnotations;

namespace PaxAcDesign.calculate.datatype;

public enum EngineType
{
    [Display(Description = "Turbojet", ResourceType = typeof(Resources.App))]
    Turbojet,

    [Display(Description = "Turboprop", ResourceType = typeof(Resources.App))]
    Turboprop
}
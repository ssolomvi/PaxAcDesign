using System.ComponentModel.DataAnnotations;

namespace PaxAcDesign.calculate.datatype;

public enum DesignRangeClassification
{
    [Display(Description = "ShortRange", ResourceType = typeof(Resources.App))]
    ShortRange,

    [Display(Description = "MediumRange", ResourceType = typeof(Resources.App))]
    MediumRange,

    [Display(Description = "LongRange", ResourceType = typeof(Resources.App))]
    LongRange,

    [Display(Description = "UltraLongRange", ResourceType = typeof(Resources.App))]
    UltraLongRange
}
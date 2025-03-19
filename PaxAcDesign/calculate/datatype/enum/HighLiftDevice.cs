using System.ComponentModel.DataAnnotations;

namespace PaxAcDesign.calculate.datatype;

public enum HighLiftDevice
{
    [Display(Description = "CleanAirfoil", ResourceType = typeof(Resources.App))]
    CleanAirfoil,

    [Display(Description = "PlainFlap", ResourceType = typeof(Resources.App))]
    PlainFlap,

    [Display(Description = "SingleSlottedFlap", ResourceType = typeof(Resources.App))]
    SingleSlottedFlap,

    [Display(Description = "DoubleSlottedFlap", ResourceType = typeof(Resources.App))]
    DoubleSlottedFlap,

    [Display(Description = "SplitFlap", ResourceType = typeof(Resources.App))]
    SplitFlap,

    [Display(Description = "DoubleWingJunkers", ResourceType = typeof(Resources.App))]
    DoubleWingJunkers,

    [Display(Description = "FowlerFlap", ResourceType = typeof(Resources.App))]
    FowlerFlap,

    [Display(Description = "Slat", ResourceType = typeof(Resources.App))]
    Slat,

    [Display(Description = "PlainFlapAndSlat", ResourceType = typeof(Resources.App))]
    PlainFlapAndSlat,

    [Display(Description = "SingleSlottedFlapAndSlat", ResourceType = typeof(Resources.App))]
    SingleSlottedFlapAndSlat,

    [Display(Description = "DoubleSlottedFlapAndSlat", ResourceType = typeof(Resources.App))]
    DoubleSlottedFlapAndSlat,

    [Display(Description = "FowlerFlapAndSlat", ResourceType = typeof(Resources.App))]
    FowlerFlapAndSlat
}
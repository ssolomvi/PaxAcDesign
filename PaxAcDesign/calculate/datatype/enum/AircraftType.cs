using System.ComponentModel.DataAnnotations;

namespace PaxAcDesign.calculate.datatype;

public enum AircraftType
{
    [Display(Description = "BusinessJet", ResourceType = typeof(Resources.App))]
    BusinessJet,

    [Display(Description = "ShortRangeJetTransport", ResourceType = typeof(Resources.App))]
    ShortRangeJetTransport,

    [Display(Description = "MediumRangeJetTransport", ResourceType = typeof(Resources.App))]
    MediumRangeJetTransport,

    [Display(Description = "LongRangeJetTransport", ResourceType = typeof(Resources.App))]
    LongRangeJetTransport,

    [Display(Description = "UltraLongRangeJetTransport", ResourceType = typeof(Resources.App))]
    UltraLongRangeJetTransport,

    [Display(Description = "Fighter", ResourceType = typeof(Resources.App))]
    Fighter,

    [Display(Description = "SupersonicCruise", ResourceType = typeof(Resources.App))]
    SupersonicCruise
}
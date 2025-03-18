using System.ComponentModel.DataAnnotations;
namespace PaxAcDesign.calculate.datatype;

public enum AircraftType
{
    
    [Display(Name = "Самолет бизнес-класса")]
    BusinessJet,
    [Display(Name = "Реактивный самолет малой дальности полета")]
    ShortRangeJetTransport,
    [Display(Name = "Реактивный самолет средней дальности полета")]
    MediumRangeJetTransport,
    [Display(Name = "Реактивный самолет большой дальности полета")]
    LongRangeJetTransport,
    [Display(Name = "Реактивный самолет сверхбольшой дальности полета")]
    UltraLongRangeJetTransport,
    [Display(Name = "Истребитель")]
    Fighter,
    [Display(Name = "Сверхзвуковой крейсерский самолет")]
    SupersonicCruise
    
}
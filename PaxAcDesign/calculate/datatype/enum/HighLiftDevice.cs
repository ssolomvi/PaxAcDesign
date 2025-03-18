using System.ComponentModel.DataAnnotations;
namespace PaxAcDesign.calculate.datatype;

public enum HighLiftDevice
{
    
    [Display(Name = "Чистый аэродинамический профиль")]
    CleanAirfoil,
    [Display(Name = "Простой закрылок")]
    PlainFlap,
    [Display(Name = "Закрылок с одной прорезью")]
    SingleSlottedFlap,
    [Display(Name = "Закрылок с двумя прорезями")]
    DoubleSlottedFlap,
    [Display(Name = "Разделенный закрылок")]
    SplitFlap,
    [Display(Name = "Двухкрылые юнкерсы")]
    DoubleWingJunkers,
    [Display(Name = "Закрылок Фаулера")]
    FowlerFlap,
    [Display(Name = "Предкрылок")]
    Slat,
    [Display(Name = "Простой закрылок и предкрылок")]
    PlainFlapAndSlat,
    [Display(Name = "Закрылок с одной прорезью и предкрылок")]
    SingleSlottedFlapAndSlat,
    [Display(Name = "Закрылок с двумя прорезями и предкрылок")]
    DoubleSlottedFlapAndSlat,
    [Display(Name = "Закрылок Фаулера и предкрылок")]
    FowlerFlapAndSlat
    
}
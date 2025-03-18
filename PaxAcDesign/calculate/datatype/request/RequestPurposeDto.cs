using System.ComponentModel.DataAnnotations;
namespace PaxAcDesign.calculate.datatype.request;

public record RequestPurposeDto()
{
    [Display(Name = "Тип самолета")]
    public AircraftType AircraftType { get; set; }
    [Display(Name = "Тип механизации крыла (закрылок и предкрылок)")]
    public HighLiftDevice HighLiftDevice { get; set; }
    [Display(Name = "Высота над уровнем моря")]
    public double HeightAboveSeaLevel { get; set; }
    [Display(Name = "Посадочный путь")]
    public double LandingDistance { get; set; }
    [Display(Name = "Путь по земле при взлете")]
    public double TakeOffGroundRoll { get; set; }
    [Display(Name = "Соотношение сторон крыла")]
    public double WingAspectRatio { get; set; }
    [Display(Name = "Отношение площади смачивания самолета к площади крыла")]
    public double WingWettedArea { get; set; }
    
}
using System.ComponentModel.DataAnnotations;

namespace PaxAcDesign.calculate.datatype;

public class RequestPurpose : IRequest
{
    #region Поля, берущиеся из конструктора

    // тип самолета                                                                         BLOCK 1
    [Display(Name = "Тип самолета")] public AircraftType AircraftType { get; set; }

    // тип механизации крыла (закрылок и предкрылок)                                        BLOCK 1
    [Display(Name = "Тип механизации крыла (закрылок и предкрылок)")]
    public HighLiftDevice HighLiftDevice { get; set; }

    // todo: высота над уровнем моря, sigma ?                                               BLOCK 1
    [Display(Name = "Высота над уровнем моря")]
    public double HeightAboveSeaLevel { get; set; }

    // посадочный путь, s_L                                                                 BLOCK 1
    [Display(Name = "Посадочный путь")] public double LandingDistance { get; set; }

    // путь по земле при взлете, s_TOG                                                      BLOCK 2
    [Display(Name = "Путь по земле при взлете")]
    public double TakeOffGroundRoll { get; set; }

    // соотношение сторон крыла, A                                                          BLOCK 4
    [Display(Name = "Соотношение сторон крыла")]
    public double WingAspectRatio { get; set; }

    // отношение площади смачивания самолета к площади крыла, S_wet / S_W                   BLOCK 6
    [Display(Name = "Отношение площади смачивания самолета к площади крыла")]
    public double WingWettedArea { get; set; }

    #endregion

    #region Расчетные значения

    // максимальный коэффициент подъемной силы, C_L,max                                     BLOCK 1
    public double MaxLiftCoefficient { get; set; }

    // отношение максимальной посадочной массы к максимальной взлетной массе, m_ML/m_MTO    BLOCK 1
    public double MaxLandingMassToMaxTakeOffMassRatio { get; set; }

    // максимальная нагрузка на крыло m/S (или m_MTO/S_W)                                   BLOCK 1
    public double MaxWingLoading { get; set; }

    // максимальный коэффициент подъемной силы при взлете, C_L,max,TO                       BLOCK 2
    public double MaxLiftCoefficientTakeOff { get; set; }

    // коэффициент зависимости тяговооруженности от нагрузки на крыло,
    // (T_TO / (m_MTO * g)) / (m_MTO / S_W)                                                 BLOCK 2
    public double ThrustToWeightRatioAndWingLoadingCoefficient { get; set; }

    // длина взлетного пути (поля), s_TOFL                                                  BLOCK 2
    public double TakeOffFieldLength { get; set; }

    // угол набора высоты, gamma                                                            BLOCK 3
    public double ClimbAngle { get; set; }

    // минимальная тяговооруженность во время 2nd Segment, min(T_TO / (m_MTO * g))          BLOCK 3/4
    public double MinThrustToWeightRatio2Segment { get; set; }

    // отношение подъемной силы к лобовому сопротивлению с шасси и закрылками, E            BLOCK 4
    public double LiftToDragRatio { get; set; }

    // минимальная тяговооруженность во время Missed Approach, min(T_TO / (m_MTO * g))      BLOCK 5
    public double MinThrustToWeightRatioMissedApproach { get; set; }

    // коэффициент подъемной силы во время круизного полета, C_L                            BLOCK 6
    public double LiftCoefficientCruise { get; set; }

    // отношение подъемной силы к лобовому сопротивлению во время Cruise, E                 BLOCK 6
    public double LiftToDragRatioCruise { get; set; }

    // делегат-Func для сохранения зависимости тяговооруженности от высоты                  BLOCK 7
    public Func<double, double> ThrustToWeightFunctionCruise { get; set; }

    // делегат-Func для сохранения зависимости нагрузки на крыло от высоты                  BLOCK 7
    public Func<double, double> WingLoadingFunctionCruise { get; set; }

    // высота крейсерского полета, h_CR                                                     BLOCK 8
    public double AltitudeCruise { get; set; }

    // удельная нагрузка на крыло, m/S (или m_MTO/S_W)                                      BLOCK 8
    public double WingLoading { get; set; }

    // тяговооруженность, T_TO / (m_MTO * g)                                                BLOCK 8
    public double ThrustToWeightRatio { get; set; }

    #endregion

    public RequestPurpose(
        AircraftType aircraftType,
        HighLiftDevice highLiftDevice,
        double heightAboveSeaLevel,
        double landingDistance,
        double takeOffGroundRoll,
        double wingAspectRatio,
        double wingWettedArea)
    {
        AircraftType = aircraftType;
        HighLiftDevice = highLiftDevice;
        HeightAboveSeaLevel = heightAboveSeaLevel;
        LandingDistance = landingDistance;
        TakeOffGroundRoll = takeOffGroundRoll;
        WingAspectRatio = wingAspectRatio;
        WingWettedArea = wingWettedArea;
    }
}
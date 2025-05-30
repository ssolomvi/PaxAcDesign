﻿using System.ComponentModel.DataAnnotations;

namespace PaxAcDesign.calculate.datatype;

public class RequestEngine : IRequest
{
    // тип двигателя                                                    BLOCK 1
    // [Required(ErrorMessage = "Engine type is required.")]
    [Display(Name = "EngineType", ResourceType = typeof(Resources.App))]
    public EngineType EngineType { get; set; } = EngineType.Turbojet;

    // количество двигателей, n_E                                       BLOCK 3
    // [Required(ErrorMessage = "Number of engines is required.")]
    [Display(Name = "Количество двигателей")]
    public int NumberOfEngines { get; set; }

    // степень двухконтурности, BPR (bypass ratio), мю                  BLOCK 7
    // [Required(ErrorMessage = "Bypass ratio required.")]
    [Display(Name = "Степень двухконтурности")]
    public double BypassRatio { get; set; }

    public RequestEngine(
        EngineType engineType,
        int numberOfEngines,
        double bypassRatio)
    {
        EngineType = engineType;
        NumberOfEngines = numberOfEngines;
        BypassRatio = bypassRatio;
    }
}
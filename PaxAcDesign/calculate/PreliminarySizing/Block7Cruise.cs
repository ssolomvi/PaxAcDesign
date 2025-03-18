using Pax_AC_Design.ModuleCalculate;
using Pax_AC_Design.ModuleCalculate.Handlers;
using Pax_AC_Design.ModuleCalculate.Request;

namespace pax_ac_design.ModuleCalculate.PreliminarySizing;

// этот блок в методике на самом деле нумерован как 5.6, однако он использует величину lift-to-drag ratio E,
// рассчитываемую в блоке 5.7, поэтому очередность их нумерации была изменена
public class Block7Cruise : AbstractHandler
{
    protected override bool CanHandle(Request request)
    {
        // check requirements
        return true;
    }

    public override Request Handle(Request request)
    {
        if (!CanHandle(request)) return PassToNextHandler(request);
        
        // метод для подсчета тяговооруженности, T_TO / (m_MTO * g), формулы 5.27 - 5.28
        double ThrustToWeightRatioFunction(double altitude) =>
            1 / (request.RequestPurpose.LiftToDragRatioCruise *
                ((0.0013 * request.RequestEngine.BypassRatio - 0.0397) * (altitude / 1000)
                - 0.0248 * request.RequestEngine.BypassRatio + 0.7125));
        
        // сохраняем зависимость тяговооруженности от высоты
        request.RequestPurpose.ThrustToWeightFunctionCruise = ThrustToWeightRatioFunction;

        // число Маха; считается равным 0.8 для дозвуковых самолетов
        const double machNumber = 0.8;
        
        // отношение удельных теплоемкостей, gamma, для воздуха принимается как 1.4
        const double ratioOfSpecificHeats = 1.4;
        
        // метод для подсчета нагрузки на крыло, m_MTO / S_W, формула 5.34
        double WingLoadingFunction(double altitude) =>
            request.RequestPurpose.LiftCoefficientCruise
            * double.Pow(machNumber, 2)
            * ratioOfSpecificHeats
            * Globals.PressureFunction(altitude)
            / (2 * Globals.g);

        // сохраняем зависимость нагрузки на крыло от высоты
        request.RequestPurpose.WingLoadingFunctionCruise = WingLoadingFunction;

        return PassToNextHandler(request);
    }
}
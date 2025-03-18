using Pax_AC_Design.ModuleCalculate;
using Pax_AC_Design.ModuleCalculate.Handlers;
using Pax_AC_Design.ModuleCalculate.Request;
using PaxAcDesign.calculate.datatype;

namespace pax_ac_design.ModuleCalculate.PreliminarySizing;

public class Block9MaximumTakeOffMass : AbstractHandler
{
    protected override bool CanHandle(Request request)
    {
        // check requirements
        return true;
    }

    public override Request Handle(Request request)
    {
        if (!CanHandle(request)) return PassToNextHandler(request);
        
        // todo: заглушка! непонятно, что значит скорость V
        double V = 155; 
        
        #region Относительная масса топлива (5.9.2)

        double relativeFuelMass;
        
        double missionFuelFraction;

        double breguetRangeFactor;

        // коэффициент Бреге для турбореактивных самолетов, B_s
        if (request.RequestEngine.EngineType == EngineType.Turbojet)
        {
            // удельный расход топлива на тягу, SFC_T
            double thrustSpecificFuelConsumption = 0;

            breguetRangeFactor = request.RequestPurpose.LiftToDragRatioCruise * V
                                 / (thrustSpecificFuelConsumption * Globals.g);
        }
        // коэффициент Бреге для турбовинтовых самолетов, B_s
        else
        {
            // удельный расход топлива на производительность, SFC_P
            double performanceSpecificFuelConsumption = 0;

            // КПД воздушного винта
            double propellerEfficiency = 0;
            
            breguetRangeFactor = request.RequestPurpose.LiftToDragRatioCruise * V
                                 / (performanceSpecificFuelConsumption * Globals.g);
        }
        
        
        
        #endregion
        
        
        return PassToNextHandler(request);
    }
}
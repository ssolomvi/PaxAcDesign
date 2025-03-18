using Pax_AC_Design.ModuleCalculate.Handlers;
using Pax_AC_Design.ModuleCalculate.Request;
// using ZedGraph;

namespace pax_ac_design.ModuleCalculate.PreliminarySizing;

public class Block8MatchingChart : AbstractHandler
{
    protected override bool CanHandle(Request request)
    {
        // check requirements
        return true;
    }

    private static (double, double, double) FindDesignPoint(Request request)
    {
        double altitudeUpper = 10000.0, altitudeLower = 0.0, altitudeCurr;
        double wingLoading, thrustToWeightRatio, coefficient;
        double coefficientRequired = request.RequestPurpose.ThrustToWeightRatioAndWingLoadingCoefficient;
        do
        {
            altitudeCurr = (altitudeUpper + altitudeLower) / 2;
            wingLoading = request.RequestPurpose.WingLoadingFunctionCruise(altitudeCurr);
            thrustToWeightRatio = request.RequestPurpose.ThrustToWeightFunctionCruise(altitudeCurr);

            coefficient = thrustToWeightRatio / wingLoading;

            if (coefficient < coefficientRequired)
            {
                altitudeLower = altitudeCurr;
            }
            else
            {
                altitudeUpper = altitudeCurr;
            }
        } while (double.Abs(coefficient - request.RequestPurpose.ThrustToWeightRatioAndWingLoadingCoefficient) > 10E-09);
        
        return (altitudeCurr, wingLoading, thrustToWeightRatio);
    }

    public void MatchingChartForm(Request request)
    {
        return;
    }
    
    public override Request Handle(Request request)
    {
        if (!CanHandle(request)) return PassToNextHandler(request);
        
        (request.RequestPurpose.AltitudeCruise, 
                request.RequestPurpose.WingLoading,
                request.RequestPurpose.ThrustToWeightRatio) = 
            FindDesignPoint(request);

        if (request.RequestPurpose.WingLoading > request.RequestPurpose.MaxWingLoading ||
            request.RequestPurpose.ThrustToWeightRatio < request.RequestPurpose.MinThrustToWeightRatio2Segment ||
            request.RequestPurpose.ThrustToWeightRatio < request.RequestPurpose.MinThrustToWeightRatioMissedApproach)
        {
            // todo: что делать? кидать исключение? или завести какой-то флаг для этого в request
            return PassToNextHandler(request);
        }
        
        return PassToNextHandler(request);
    }
}
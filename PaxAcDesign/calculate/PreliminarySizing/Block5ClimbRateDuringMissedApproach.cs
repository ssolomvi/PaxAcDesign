using PaxAcDesign.calculate.datatype;
using PaxAcDesign.calculate.Handlers;

namespace PaxAcDesign.calculate.PreliminarySizing;

public class Block5ClimbRateDuringMissedApproach : AbstractHandler
{
    protected override bool CanHandle(Request request)
    {
        // check requirements
        return true;
    }

    public override Request Handle(Request request)
    {
        if (!CanHandle(request)) return PassToNextHandler(request);

        // минимальная тяговооруженность во время Missed Approach, формула 5.24
        request.RequestPurpose.MinThrustToWeightRatioMissedApproach =
            request.RequestPurpose.MinThrustToWeightRatio2Segment
            * request.RequestPurpose.MaxLandingMassToMaxTakeOffMassRatio;


        return PassToNextHandler(request);
    }
}
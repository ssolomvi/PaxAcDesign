using Pax_AC_Design.ModuleCalculate.Handlers;
using Pax_AC_Design.ModuleCalculate.Request;

namespace pax_ac_design.ModuleCalculate.PreliminarySizing;

public class Block4LiftToDragRatioWithExtendedLandingGearAndExtendedFlaps : AbstractHandler
{
    protected override bool CanHandle(Request request)
    {
        // check requirements
        return true;
    }

    public override Request Handle(Request request)
    {
        if (!CanHandle(request)) return PassToNextHandler(request);

        // todo: заглушка! непонятно, откуда брать эти скорости и что они значат
        double Vs = 150, V = 155;
        
        // профильное сопротивление, C_D,P - складывается из сопротивления нулевой подъемной силы и сопротивления, вызванного механизацией и шасси
        double dragProfile;
        
        // сопротивление нулевой подъемной силы, C_D,0
        const double dragZeroLift = 0.02;

        // сопротивление, вызванное предкрылками, C_D,slat (является незначительным, negligible)
        const double dragSlat = 0.0;
        
        // сопротивление, вызванное шасси, C_D,gear
        const double dragGear = 0.015;
        
        // коэффициент подъемной силы, C_L - формула 5.21a
        double liftCoefficient = request.RequestPurpose.MaxLiftCoefficient * double.Pow(Vs / V, 2);

        // сопротивление, вызванное закрылками, C_D,flap, формула 5.21b
        double dragFlap = liftCoefficient >= 1.1 ? 0.05 * liftCoefficient - 0.055 : 0.0;
        
        // профильное сопротивление, формула 5.21
        dragProfile = dragZeroLift + dragFlap + dragSlat + dragGear;
        
        // коэффициент полезного действия Освальда, e
        const double oswaldEfficiencyFactor = 0.7;

        // todo: откуда берется соотношение сторон крыла, A (wing aspect ratio)?
        // отношение подъемной силы к лобовому сопротивлению, E, формула 5.20
        request.RequestPurpose.LiftToDragRatio =
            liftCoefficient / (dragProfile
                               + (double.Pow(liftCoefficient, 2) / (double.Pi
                                                                    * oswaldEfficiencyFactor
                                                                    * request.RequestPurpose.WingAspectRatio)));
        
        // todo: нормально ли рассчитывать тяговооруженность здесь, заместо Block3?
        // минимальная тяговооруженность во время 2nd Segment, формула 5.14
        request.RequestPurpose.MinThrustToWeightRatio2Segment =
            (request.RequestEngine.NumberOfEngines / (request.RequestEngine.NumberOfEngines - 1.0))
            * (1 / request.RequestPurpose.LiftToDragRatio + double.Sin(request.RequestPurpose.ClimbAngle));
        
        return PassToNextHandler(request);
    }
}
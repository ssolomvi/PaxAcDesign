using Pax_AC_Design.ModuleCalculate.Handlers;
using Pax_AC_Design.ModuleCalculate.Request;

namespace pax_ac_design.ModuleCalculate.PreliminarySizing;

public class Block3ClimbRateDuringSecondSegment : AbstractHandler
{
    protected override bool CanHandle(Request request)
    {
        // check requirements
        return true;
    }

    public override Request Handle(Request request)
    {
        if (!CanHandle(request)) return PassToNextHandler(request);
        
        // уклон набора высоты
        double climbGradient = 0.0;
        switch (request.RequestEngine.NumberOfEngines)
        {
            case 2:
                climbGradient = 2.4;
                break;
            case 3:
                climbGradient = 2.7;
                break;
            case 4:
                climbGradient = 3.0;
                break;
        }

        request.RequestPurpose.ClimbAngle = double.Atan(climbGradient / 100);
        
        // todo: как рассчитывать минимальное отношение тяги к весу T_TO / (m_MTO * g), если значение E считается в блоке 5.4?

        return PassToNextHandler(request);
    }
}
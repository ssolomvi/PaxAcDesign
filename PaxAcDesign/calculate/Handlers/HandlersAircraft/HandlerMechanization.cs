namespace Pax_AC_Design.ModuleCalculate.Handlers.handlersAircraft;
using Request;

// механизация
public class HandlerMechanization : AbstractHandlerAircraft
{
    protected override bool CanHandle(Request request)
    {
        // check requirements

        return true;
    }

    public override Request Handle(Request request)
    {
        if (!CanHandle(request)) return PassToNextHandler(request);
        
        // do smth

        return PassToNextHandler(request);
    }
}
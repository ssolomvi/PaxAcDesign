namespace Pax_AC_Design.ModuleCalculate.Handlers.handlersPrognosis;
using Request;

// экспертный 2
public class HandlerExpertTwo : AbstractHandlerPrognosis
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
namespace Pax_AC_Design.ModuleCalculate.Handlers.handlersPrognosis;
using Request;
// автоматический
public class HandlerAutomatic : AbstractHandlerPrognosis
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
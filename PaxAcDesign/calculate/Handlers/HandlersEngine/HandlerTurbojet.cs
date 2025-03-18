namespace Pax_AC_Design.ModuleCalculate.Handlers.handlersEngine;
using Request;

// турбореактивный
public class HandlerTurbojet : AbstractHandlerEngine
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
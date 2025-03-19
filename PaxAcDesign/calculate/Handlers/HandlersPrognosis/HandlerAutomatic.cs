using PaxAcDesign.calculate.datatype;

namespace PaxAcDesign.calculate.Handlers.HandlersPrognosis;

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
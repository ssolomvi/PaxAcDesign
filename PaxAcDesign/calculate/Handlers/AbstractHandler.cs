using PaxAcDesign.calculate.datatype;

namespace PaxAcDesign.calculate.Handlers;

public abstract class AbstractHandler : IHandler
{
    private IHandler _nextHandler;

    public IHandler SetNext(IHandler handler)
    {
        this._nextHandler = handler;

        // fluent chain
        return handler;
    }

    protected abstract bool CanHandle(Request request);

    protected Request PassToNextHandler(Request request)
    {
        if (this._nextHandler != null)
        {
            return this._nextHandler.Handle(request);
        }
        else
        {
            return null;
        }
    }

    public abstract Request Handle(Request request);
}
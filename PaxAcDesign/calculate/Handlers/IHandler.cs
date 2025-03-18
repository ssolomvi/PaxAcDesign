namespace Pax_AC_Design.ModuleCalculate.Handlers;
using Request;

public interface IHandler
{
    IHandler SetNext(IHandler handler);

    Request Handle(Request request);
}
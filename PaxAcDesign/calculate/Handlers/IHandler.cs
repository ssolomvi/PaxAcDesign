using PaxAcDesign.calculate.datatype;

namespace PaxAcDesign.calculate.Handlers;

public interface IHandler
{
    IHandler SetNext(IHandler handler);

    Request Handle(Request request);
}
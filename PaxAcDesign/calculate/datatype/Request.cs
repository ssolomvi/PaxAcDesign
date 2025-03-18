namespace Pax_AC_Design.ModuleCalculate.Request;

public class Request : IRequest
{

    public Request()
    {
        
    }
    
    public Request(
        RequestAircraft requestAircraft, 
        RequestEngine requestEngine, 
        RequestPrognosis requestPrognosis, 
        RequestPurpose requestPurpose)
    {
        RequestAircraft = requestAircraft;
        RequestEngine = requestEngine;
        RequestPrognosis = requestPrognosis;
        RequestPurpose = requestPurpose;
    }
    
    public RequestAircraft? RequestAircraft { get; private set; }

    public RequestEngine? RequestEngine { get; private set; }

    public RequestPrognosis? RequestPrognosis { get; private set; }

    public RequestPurpose? RequestPurpose { get; private set; }

}
namespace PaxAcDesign.calculate.datatype;

public class Request : IRequest
{
    public Request()
    {
        RequestAircraft = new RequestAircraft();
        RequestEngine = new RequestEngine(EngineType.Turbojet, 0, 0.0);
        RequestPrognosis = new RequestPrognosis();
        RequestPurpose = new RequestPurpose(AircraftType.Fighter, HighLiftDevice.Slat, 0.0, 0.0, 0.0, 0.0, 0.0);
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

    public RequestAircraft? RequestAircraft { get; set; }

    public RequestEngine? RequestEngine { get; set; }

    public RequestPrognosis? RequestPrognosis { get; set; }

    public RequestPurpose? RequestPurpose { get; set; }
}
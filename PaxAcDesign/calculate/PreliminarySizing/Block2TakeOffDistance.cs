using Pax_AC_Design.ModuleCalculate.Handlers;
using Pax_AC_Design.ModuleCalculate.Request;
using PaxAcDesign.calculate.datatype;

namespace pax_ac_design.ModuleCalculate.PreliminarySizing;

/**
 * В блоке 2 "Взлетный путь" рассчитывается минимальное значение отношения тяги к весу
 * в зависимости от нагрузки на крыло: T / (m * g) = f(m / S) с исходным значением: T_TO / (m_MTO * g).
 * Функциональное соединение T / (m * g) = f(m / S) зависит от максимального коэффициента подъемной
 * силы при использовании закрылков во взлетном положении C_L,max,TO и длины взлетной площадки s_TOFL.
 * Максимальный коэффициент подъемной силы C_L,max,TO выбран на основе литературных данных.
 */
public class Block2TakeOffDistance : AbstractHandler
{
    protected override bool CanHandle(Request request)
    {
        // check requirements
        return true;
    }

    public override Request Handle(Request request)
    {
        if (!CanHandle(request)) return PassToNextHandler(request);
        
        // коэффициент k_TO для расчета величины (T_TO / (m_MTO * g)) / (m_MTO / S_W), m^3/kg
        const double coefficientTakeOff = 2.34;

        // принимаем зависимость C_L,max,TO = 0,75 * C_L,max
        request.RequestPurpose.MaxLiftCoefficientTakeOff = 0.75 * request.RequestPurpose.MaxLiftCoefficient;
            
        if (request.RequestEngine.EngineType is not EngineType.Turbojet)
        {
            // todo: что нужно записывать в поле request.RequestPurpose.ThrustToWeightAndWingLoadingRatio?
            return request;
        }

        // todo: как считать s_TOG, если формула 5.9 используется для вывода отношения T/W ?
            
        // принимаем зависимость s_TOFL = 1,5 * s_TOG
        request.RequestPurpose.TakeOffFieldLength = 1.5 * request.RequestPurpose.TakeOffGroundRoll;
            
        // рассчет коэффициента зависимости тяговооруженности от нагрузки на крыло, (T_TO / (m_MTO * g)) / (m_MTO / S_W)
        request.RequestPurpose.ThrustToWeightRatioAndWingLoadingCoefficient = 
            coefficientTakeOff /
            (request.RequestPurpose.TakeOffFieldLength
             * request.RequestPurpose.HeightAboveSeaLevel
             * request.RequestPurpose.MaxLiftCoefficientTakeOff);

        return PassToNextHandler(request);
    }
}
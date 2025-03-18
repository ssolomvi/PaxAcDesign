using Pax_AC_Design.ModuleCalculate.Handlers;
using Pax_AC_Design.ModuleCalculate.Request;

namespace pax_ac_design.ModuleCalculate.PreliminarySizing;

// этот блок в методике на самом деле нумерован как 5.7, однако он рассчитывает величину lift-to-drag ratio E,
// которая используется в блоке 5.6, поэтому очередность их нумерации была изменена
public class Block6LiftToDragRatioDuringCruise : AbstractHandler
{
    protected override bool CanHandle(Request request)
    {
        // check requirements
        return true;
    }

    public override Request Handle(Request request)
    {
        if (!CanHandle(request)) return PassToNextHandler(request);

        // todo: заглушка! непонятно, откуда брать эти скорости и что значит скорость V
        double Vmd = 170, V = 155;
        
        // коэффициент k_E; todo: чему он равен? текущее значение из Loftin 1980
        const double liftToDragCoefficient = 14.9;
        
        // максимальное отношение подъемной силы к лобовому сопротивлению, E_max, формула 5.36 
        double maxLiftToDragRatioCruise = liftToDragCoefficient
                                       * double.Sqrt(
                                           request.RequestPurpose.WingAspectRatio
                                           / request.RequestPurpose.WingWettedArea);

        // коэффициент, взятый из Loftin 1980
        const double e = 0.85;
        
        // коэффициент подъемной силы при минимальном сопротивлении, C_L,md, формула 5.39
        double liftCoefficientWithMinimumDrag = (double.Pi * request.RequestPurpose.WingAspectRatio * e)
                                                / (2 * maxLiftToDragRatioCruise);
        
        // коэффициент подъемной силы во время круизного полета, C_L, формула 5.40
        double liftCoefficientCruise = liftCoefficientWithMinimumDrag / double.Pow(V / Vmd, 2);

        // сохраняем C_L
        request.RequestPurpose.LiftCoefficientCruise = liftCoefficientCruise;
        
        // todo: возможно, стоит переделать и выше считать не отдельно C_L,md, а отношение C_L / C_L,md ?
        // отношение подъемной силы к лобовому сопротивлению во время Cruise, E, формула 5.41
        request.RequestPurpose.LiftToDragRatioCruise =
            2 * maxLiftToDragRatioCruise
            / (1 / (liftCoefficientCruise / liftCoefficientWithMinimumDrag)
               + liftCoefficientCruise / liftCoefficientWithMinimumDrag);

        return PassToNextHandler(request);
    }
}
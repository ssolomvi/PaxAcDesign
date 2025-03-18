namespace Pax_AC_Design.ModuleCalculate;

public static class Globals
{
    // точность вычислений
    public const double Eps = 0.001;
    
    // плотность атмосферы на уровне моря [кг/м^3]
    public const double AtmosphericDensityAtSeaLevel = 1.225;

    // ускорение свободного падения, g [м/с^2] (берется среднее, так как есть высота над уровнем земли)
    public const double g = 9.80;
    
    // пример функции давления для блока 7
    public static double PressureFunction(double altitude) => 101325 * double.Pow(0.87, altitude / 1000);
}
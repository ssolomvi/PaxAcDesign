namespace Pax_AC_Design.ModuleCalculate.Atmosphere;

public class Atmosphere
{
    // МЕЖДУНАРОДНЫЕ СТАНДАРТНЫЕ АТМОСФЕРНЫЕ ПОСТОЯННЫЕ
    // пределы уровня m =======================================================
    private double level1 = 11000;
    private double level2 = 20000;
    private double level3 = 32000;
    private double level4 = 47000;
    private double level5 = 50000;
    // пределы уровня по плотности (кг/м^3)=================================
    private double dLevel1 = 0.36391700650017816;
    private double dLevel2 = 0.088034556579455497;
    private double dLevel3 = 0.013224937668421609;
    private double dLevel4 = 0.0014275295197313882;
    private double dLevel5 = 0.00097752213943149563;
    // пределы уровня по давлению (Па)=================================
    private double pLevel1 = 22632.000999016603;
    private double pLevel2 = 5474.8699475808515;
    private double pLevel3 = 868.01381950678511;
    private double pLevel4 = 110.90599734942646;
    // Постоянные ISA, основные температуры A в K, вертикальный градиент B в град/м
    // На основании таблицы 11.3, определяется как "высота давления Hp", SI Units
    // Уровень 1 ------------------------------------------------------------------
    private const double A1 = 288.15;
    private const double B1 = -6.5e-3;
    private const double C1 = 8.9619638;
    private const double D1 = -0.20216125e-3;
    private const double E1 = 5.2558797;
    private const double I1 = 1.048840;
    private const double J1 = -23.659414e-6;
    private const double L1 = 4.2558797;
    // Уровень 2 -----------------------------------------------------------------
    private const double A2 = 216.65;
    private const double B2 = 0;
    private const double F2 = 128244.5;
    private const double G2 = -0.15768852e-3;
    private const double M2 = 2.0621400;
    private const double N2 = -0.15768852e-3;
    // Уровень 3 -----------------------------------------------------------------
    private const double A3 = 196.65;
    private const double B3 = 1e-3;
    private const double C3 = 0.70551848;
    private const double D3 = 3.5876861e-6;
    private const double E3 = -34.163218;
    private const double I3 = 0.9726309;
    private const double J3 = 4.94600e-6;
    private const double L3 = -35.163218;
    // Уровень 4 -----------------------------------------------------------------
    private const double A4 = 139.05;
    private const double B4 = 2.8e-3;
    private const double C4 = 0.34926867;
    private const double D4 = 7.0330980e-6;
    private const double E4 = -12.201149;
    private const double I4 = 0.84392929;
    private const double J4 = 16.993902e-6;
    private const double L4 = -13.201149;
    // Уровень 5 -----------------------------------------------------------------
    private const double A5 = 270.65;
    private const double B5 = 0;
    private const double F5 = 41828.42;
    private const double G5 = -0.12622656e-3;
    private const double M5 = 0.53839563;
    private const double N5 = -0.12622656e-3;
    //==========================================================================


    private double AltitudeCheck(double altitudeM)
    {
        if (altitudeM > level5)
        {
            Console.WriteLine("Altitudes had to be limited to 50km where higher.");
            altitudeM = level5;
        }

        return altitudeM;
    }

    // Плотность как функция геопотенциальной высоты, в кг / м^3
    public double AirDensityKgpm3(double altitudeM)
    {
        altitudeM = AltitudeCheck(altitudeM);
        double airDensityKgpm3;
        
        // ISA
        if (altitudeM < level1)
        {
            // Тропосфера
            airDensityKgpm3 = double.Pow(I1 + J1 * altitudeM, L1);
        }
        else if (altitudeM < level2)
        {
            // Нижняя стратосфера
            airDensityKgpm3 = M2 * double.Exp(N2 * altitudeM);
        }
        else if (altitudeM < level3)
        {
            // Верхняя стратосфера
            airDensityKgpm3 = double.Pow(I3 + J3 * altitudeM, L3);
        }
        else if (altitudeM < level4)
        {
            // Между 32 и 47 км
            airDensityKgpm3 = double.Pow(I4 + J4 * altitudeM, L4);
        }
        else
        {
            // Между 47 и 51 км
            airDensityKgpm3 = M5 * double.Exp(N5 * altitudeM);
        }
        
        // todo: нужно ли регулировать температурное смещение?

        return airDensityKgpm3;
    }
    
    // Температура как функция геопотенциальной высоты, в Кельвинах
    public double AirTemperatureK(double altitudeM)
    {
        altitudeM = AltitudeCheck(altitudeM);
        
        // ISA
        double airTemperatureK;

        if (altitudeM < level1)
        {
            // Тропосфера, температура уменьшается линейно
            airTemperatureK = A1 + B1 * altitudeM;

        }
        else if (altitudeM < level2)
        {
            // Нижняя стратосфера, температура постоянна
            airTemperatureK = A2 + B2 * altitudeM;
        }
        else if (altitudeM < level3)
        {
            // Верхняя стратосфера, температура повышается
            airTemperatureK = A3 + B3 * altitudeM;
        }
        else if (altitudeM < level4)
        {
            // Между 32 и 47 км
            airTemperatureK = A4 + B4 * altitudeM;
        }
        else
        {
            // Между 47 и 51 км
            airTemperatureK = A5 + B5 * altitudeM;
        }
        
        // todo: нужно ли регулировать температурное смещение?

        return airTemperatureK;
    }
}
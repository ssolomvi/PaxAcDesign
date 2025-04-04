using PaxAcDesign.calculate.datatype;
using PaxAcDesign.calculate.Handlers;
using PaxAcDesign.Data;

// using Microsoft.Data.Sqlite;

namespace PaxAcDesign.calculate.PreliminarySizing;

/*
 * В блоке 1 "Посадочный путь" указано максимальное значение нагрузки на крыло, m/S.
 * Входными значениями для расчета являются максимальный коэффициент подъемной силы
 * при использовании закрылков в посадочном положении C_LmaxL а также длина посадочного поля s_LFL
 * в соответствии с CS/FAR.
 */
public class Block1LandingDistance : AbstractHandler
{
    // private ConnectionProvider _connectionProvider;

    public Block1LandingDistance()
    {
        // todo:
        // _connectionProvider = ConnectionProvider.create("");
    }

    protected override bool CanHandle(Request request)
    {
        // check requirements
        return true;
    }

    public override Request Handle(Request request)
    {
        if (!CanHandle(request)) return PassToNextHandler(request);

        #region Расчет m_ML/S_W

        // коэффициент для расчета максимального значения нагрузки на крыло при посадке, k_L, kg/m^3
        const double coefficientLanding = 0.107;

        // длина посадочного пути самолета
        double landingFieldLength = request.RequestPurpose.LandingDistance;

        // домножение на коэффициент безопасности, "safety factor"
        switch (request.RequestEngine.EngineType)
        {
            case EngineType.Turbojet:
                landingFieldLength *= 1.667;
                break;
            case EngineType.Turboprop:
                landingFieldLength *= 1.429;
                break;
        }

        // тип механизации крыла (закрылки и предкрылки)
        string highLiftDevices = request.RequestPurpose.HighLiftDevice.ToString();

        // коэффициент C_LmaxL, берущийся из таблицы CL_max (Dubs1987) ниже
        // todo: enable sqLite connection later
        // double maxLiftCoefficient = 0.0; 
        double maxLiftCoefficient = 1.45;

//         // взятие коэффициента C_LmaxL из таблицы в соответствии с заданным типом механизации крыла
        // var connection = _connectionProvider.getConnection();
        // using (var ts = connection.BeginTransaction())
        // {
        //     var selectCommand = connection.CreateCommand();
        //     selectCommand.Transaction = ts;
        //     selectCommand.CommandText =
        //         $"SELECT * FROM `CL_max (Dubs1987)` where high_lift_devices = {highLiftDevices}";
        //     
        //     using (var reader = selectCommand.ExecuteReader())
        //     {
        //         while (reader.Read()) // построчно считываем данные
        //         {
        //             maxLiftCoefficient = (double)reader["C_L,max"];
        //             break;
        //         }
        //     }
        // }

        // var command =
            // _dbContext.CreateCommand($"SELECT * FROM `CL_max (Dubs1987)` where high_lift_devices = {highLiftDevices}");
        // using (var reader = command.ExecuteReader())
        // {
            // while (reader.Read()) // построчно считываем данные
            // {
                // maxLiftCoefficient = (double)reader["C_L,max"];
                // break;
            // }
        // }

        // сохранение коэффициента C_L,max,L для дальнейшего использования
        request.RequestPurpose.MaxLiftCoefficient = maxLiftCoefficient;

        // нагрузка на крыло при максимальной посадочной массе, m_ML/S_W, формула 5.5
        double wingLoadingAtMaxLandingMass = coefficientLanding
                                             * request.RequestPurpose.HeightAboveSeaLevel
                                             * maxLiftCoefficient
                                             * landingFieldLength;

        #endregion

        #region Взятие значения m_ML/m_MTO из таблицы

        // todo: enable sqLite connection later
        // тип самолета
        string aircraftType = request.RequestPurpose.AircraftType.ToString();

        request.RequestPurpose.MaxLandingMassToMaxTakeOffMassRatio = 0.88;

        // взятие значения m_ML/m_MTO из таблицы Roskam I
        // using (var ts = connection.BeginTransaction())
        // {
        //     var selectCommand = connection.CreateCommand();
        //     selectCommand.Transaction = ts;
        //     selectCommand.CommandText =
        //         $"SELECT * FROM `m_L/m_MTO (Roskam I)` where aircraft_type = {aircraftType}";
        //     
        //     using (var reader = selectCommand.ExecuteReader())
        //     {
        //         while (reader.Read()) // построчно считываем данные
        //         {
        //             request.RequestPurpose.MaxLandingMassToMaxTakeOffMassRatio = (double)reader["ratio_average"];
        //             break;
        //         }
        //     }
        // }
        // command = _dbContext.CreateCommand(
        //     $"SELECT * FROM `m_L/m_MTO (Roskam I)` where aircraft_type = {aircraftType}");
        // using (var reader = command.ExecuteReader())
        // {
        //     if (reader.HasRows) // если есть данные
        //     {
        //         while (reader.Read()) // построчно считываем данные
        //         {
        //             request.RequestPurpose.MaxLandingMassToMaxTakeOffMassRatio = (double)reader["ratio_average"];
        //             break;
        //         }
        //     }
        // }

        #endregion

        // максимальная нагрузка на крыло m_MTO/S_W, формула 5.6
        request.RequestPurpose.MaxWingLoading = wingLoadingAtMaxLandingMass
                                                / request.RequestPurpose.MaxLandingMassToMaxTakeOffMassRatio;

        return PassToNextHandler(request);
    }
}
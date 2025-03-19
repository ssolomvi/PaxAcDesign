using PaxAcDesign.calculate.datatype;
using PaxAcDesign.calculate.Handlers;

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
    protected override bool CanHandle(Request request)
    {
        // check requirements
        return true;
    }

    public override Request Handle(Request request)
    {
        if (!CanHandle(request)) return PassToNextHandler(request);

        /*
        using (var connection = new SqliteConnection(
                   // "Data Source=Databases/pax-ac-design.db;"))
                   // "Data Source=C:/c_sharp/BlazorApp2/Databases/pax-ac-design.db"))
                   "Data Source=pax-ac-design.db"))
                   // "Data Source=pax-ac-design.db;"))
        {
            connection.Open();
            */

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

        /*
        // взятие коэффициента C_LmaxL из таблицы в соответствии с заданным типом механизации крыла
        // todo: добавить WHERE
        string sqlExpression = "SELECT * FROM `CL_max (Dubs1987)`";
        SqliteCommand command = new SqliteCommand(sqlExpression, connection);
        using (SqliteDataReader reader = command.ExecuteReader())
        {
            if (reader.HasRows) // если есть данные
            {
                while (reader.Read()) // построчно считываем данные
                {
                    // todo: где-то нужна проверка на существование такого поля; если его нет, то что делать - exception?
                    string highLiftDevicesTable = (string) reader["high_lift_devices"];

                    if (highLiftDevices.Equals(highLiftDevicesTable))
                    {
                        maxLiftCoefficient = (double) reader["C_L,max"];
                        break;
                    }
                }
            }
        }
        */

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

        /*
        // взятие значения m_ML/m_MTO из таблицы Roskam I
        // todo: добавить WHERE
        sqlExpression = "SELECT * FROM `m_L/m_MTO (Roskam I)`";
        command = new SqliteCommand(sqlExpression, connection);
        using (SqliteDataReader reader = command.ExecuteReader())
        {
            if (reader.HasRows) // если есть данные
            {
                while (reader.Read()) // построчно считываем данные
                {
                    string aircraftTypeTable = (string) reader["aircraft_type"];

                    if (aircraftType.Equals(aircraftTypeTable))
                    {
                        request.RequestPurpose.MaxLandingMassToMaxTakeOffMassRatio = (double) reader["ratio_average"];
                        break;
                    }
                }
            }
        }
        */

        #endregion

        // максимальная нагрузка на крыло m_MTO/S_W, формула 5.6
        request.RequestPurpose.MaxWingLoading = wingLoadingAtMaxLandingMass
                                                / request.RequestPurpose.MaxLandingMassToMaxTakeOffMassRatio;
        /*
    connection.Close();
}
*/
        return PassToNextHandler(request);
    }
}
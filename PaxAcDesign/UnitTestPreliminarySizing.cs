// using System.Data;
// using Pax_AC_Design.ModuleCalculate.Handlers.handlersAircraft;
// using Pax_AC_Design.ModuleCalculate.Request;
// using Microsoft.Data.Sqlite;
// using Pax_AC_Design.ModuleCalculate;
// using pax_ac_design.ModuleCalculate.PreliminarySizing;
// using PaxAcDesign.calculate;
// using PaxAcDesign.calculate.datatype;
// using PaxAcDesign.calculate.PreliminarySizing;
//
// namespace Tests;
// using PaxAcDesign;
//
// public class Tests
// {
//     private Request _request;
//     
//     // collateral damage with variables' names
//     private const double Block1_MaxLiftCoefficient = 1.450;
//     private const double Block1_MaxLandingMassToMaxTakeOffMassRatio = 0.88;
//     private const double Block1_MaxWingLoading = 2204.276;
//     private const double Block2_MaxLiftCoefficientTakeOff = 1.088;
//     private const double Block2_ThrustToWeightRatioAndWingLoadingCoefficient = 0.0001103;
//     private const double Block3_ClimbAngle = 0.030;
//     private const double Block4_LiftToDragRatio = 11.530;
//     private const double Block4_MinThrustToWeightRatio2Segment = 0.156;
//     private const double BLock5_MinThrustToWeightRatioMissedApproach = 0.137;
//     private const double Block6_LiftToDragRatioCruise = 17.942;
//     private const double Block7_ThrustToWeightRatioCruiseAt5000 = 0.157;
//     private const double Block7_WingLoadingCruiseAt5000 = 2438.302;
//
//     [SetUp]
//     public void Setup()
//     {
//         RequestAircraft requestAircraft = new RequestAircraft();
//         RequestPrognosis requestPrognosis = new RequestPrognosis();
//         RequestEngine requestEngine = new RequestEngine(
//             EngineType.Turbojet, 
//             4,
//             8.7);
//         RequestPurpose requestPurpose = new RequestPurpose(
//             AircraftType.BusinessJet,
//             HighLiftDevice.CleanAirfoil,
//             10,
//             750,
//             1300,
//             12,
//             8
//         );
//
//         _request = new Request(
//             requestAircraft,
//             requestEngine,
//             requestPrognosis,
//             requestPurpose);
//     }
//
//     [Test]
//     /*
//      * Выводит на экран содержимое таблицы CL_max (Dubs1987)
//      */
//     public void DatabaseReading()
//     {
//         string name = "Slat";
//         
//         // string sqlExpression = "SELECT * FROM `CL_max (Dubs1987)` WHERE high_lift_devices =" + name;
//         string sqlExpression = "SELECT * FROM `CL_max (Dubs1987)`";
//
//         using (var connection = new SqliteConnection(
//                    "Data Source=../../../../Databases/pax-ac-design.db"))
//         {
//             connection.Open();
//             SqliteCommand command = new SqliteCommand(sqlExpression, connection);
//
//             using (SqliteDataReader reader = command.ExecuteReader())
//             {
//                 if (reader.HasRows) // если есть данные
//                 {
//                     while (reader.Read())   // построчно считываем данные
//                     {
//                         // string highLiftDevicesTable = (string) reader["high_lift_devices"];
//                         
//                         string highLiftDevicesTable = (string) reader["high_lift_devices"];
//                         double maxLiftCoefficientTable = (double) reader["C_L,max"];
//                         double maxLiftCoefficientDeltaTable = (double) reader["DC_L,max"];
//                         
//                         // Console.WriteLine($"{highLiftDevicesTable}");
//                         
//                         Console.WriteLine($"{highLiftDevicesTable}\t{maxLiftCoefficientTable}\t{maxLiftCoefficientDeltaTable}");
//                     }
//                 }
//             }
//         }
//
//         Assert.Pass();
//     }
//     
//     [Test]
//     /*
//      * Тест работы с базой данных
//      */
//     public void DatabaseFiltering()
//     {
//         HighLiftDevice highLiftDevices = HighLiftDevice.Slat;
//         double maxLiftCoefficient = 0.0;
//         
//         string sqlExpression = "SELECT * FROM `CL_max (Dubs1987)`";
//         using (var connection = new SqliteConnection(
//                    "Data Source=../../../../Databases/pax-ac-design.db"))
//         {
//             connection.Open();
//             SqliteCommand command = new SqliteCommand(sqlExpression, connection);
//
//             using (SqliteDataReader reader = command.ExecuteReader())
//             {
//                 if (reader.HasRows) // если есть данные
//                 {
//                     while (reader.Read())   // построчно считываем данные
//                     {
//                         string highLiftDevicesTable = (string) reader["high_lift_devices"];
//
//                         if (highLiftDevices.ToString().Equals(highLiftDevicesTable))
//                         {
//                             maxLiftCoefficient = (double) reader["C_L,max"];
//                             break;
//                         }
//                     }
//                 }
//             }
//         }
//
//         Assert.That(maxLiftCoefficient, Is.EqualTo(2.0));
//     }
//     
//     [Test]
//     public void Block1Calculating()
//     {
//         var block1LandingDistance = new Block1LandingDistance();
//         
//         block1LandingDistance.Handle(_request);
//         
//         Console.WriteLine("--- BLOCK 1 ---"
//                           + Environment.NewLine
//                           + "Максимальный коэффициент подъемной силы, C_L,max: {0:F3}"
//                           + Environment.NewLine
//                           + "Отношение максимальной посадочной массы к максимальной взлетной массе, m_ML/m_MTO: {1:F3}"
//                           + Environment.NewLine
//                           + "Максимальная нагрузка на крыло, m/S (или m_MTO/S_W): {2:F3}"
//                           + Environment.NewLine, 
//             _request.RequestPurpose.MaxLiftCoefficient,
//             _request.RequestPurpose.MaxLandingMassToMaxTakeOffMassRatio,
//             _request.RequestPurpose.MaxWingLoading);
//         
//         Assert.Multiple(() =>
//         {
//             Assert.That(_request.RequestPurpose.MaxLiftCoefficient,
//                 Is.EqualTo(Block1_MaxLiftCoefficient).Within(Globals.Eps));
//             Assert.That(_request.RequestPurpose.MaxLandingMassToMaxTakeOffMassRatio,
//                 Is.EqualTo(Block1_MaxLandingMassToMaxTakeOffMassRatio).Within(Globals.Eps));
//             Assert.That(_request.RequestPurpose.MaxWingLoading,
//                 Is.EqualTo(Block1_MaxWingLoading).Within(Globals.Eps));
//         });
//     }
//
//     [Test]
//     public void Block2Calculating()
//     {
//         Block1Calculating();
//         
//         var block2TakeOffDistance = new Block2TakeOffDistance();
//         
//         block2TakeOffDistance.Handle(_request);
//
//         Console.WriteLine("--- BLOCK 2 ---"
//                           + Environment.NewLine
//                           + "Максимальный коэффициент подъемной силы при взлете, C_L,max,TO: {0:F3}"
//                           + Environment.NewLine
//                           + "Коэффициент зависимости тяговооруженности от нагрузки на крыло, "
//                           + "(T_TO / (m_MTO * g)) / (m_MTO / S_W): {1:F9}"
//                           + Environment.NewLine,
//             _request.RequestPurpose.MaxLiftCoefficientTakeOff,
//             _request.RequestPurpose.ThrustToWeightRatioAndWingLoadingCoefficient);
//         
//         Assert.Multiple(() =>
//         {
//             Assert.That(_request.RequestPurpose.MaxLiftCoefficientTakeOff,
//                 Is.EqualTo(Block2_MaxLiftCoefficientTakeOff).Within(Globals.Eps));
//             Assert.That(_request.RequestPurpose.ThrustToWeightRatioAndWingLoadingCoefficient,
//                 Is.EqualTo(Block2_ThrustToWeightRatioAndWingLoadingCoefficient).Within(0.000001));
//         });
//     }
//     
//     [Test]
//     public void Block3Calculating()
//     {
//         Block2Calculating();
//         
//         var block3ClimbRateDuringSecondSegment = new Block3ClimbRateDuringSecondSegment();
//         
//         block3ClimbRateDuringSecondSegment.Handle(_request);
//
//         Console.WriteLine("--- BLOCK 3 ---"
//                           + Environment.NewLine
//                           + "Угол набора высоты, gamma: {0:F3} rad, {1:F3}°"
//                           + Environment.NewLine,
//             _request.RequestPurpose.ClimbAngle, _request.RequestPurpose.ClimbAngle * 180 / double.Pi);
//         
//         Assert.Multiple(() =>
//         {
//             Assert.That(_request.RequestPurpose.ClimbAngle,
//                 Is.EqualTo(Block3_ClimbAngle).Within(Globals.Eps));
//         });
//     }
//     
//     [Test]
//     public void Block4Calculating()
//     {
//         Block3Calculating();
//         
//         var block4LiftToDragRatioWithExtendedLandingGearAndExtendedFlaps = 
//             new Block4LiftToDragRatioWithExtendedLandingGearAndExtendedFlaps();
//         
//         block4LiftToDragRatioWithExtendedLandingGearAndExtendedFlaps.Handle(_request);
//
//         Console.WriteLine("--- BLOCK 4 ---"
//                           + Environment.NewLine
//                           + "Отношение подъемной силы к лобовому сопротивлению, E: {0:F3}"
//                           + Environment.NewLine
//                           + "Минимальная тяговооруженность во время 2nd Segment, min(T_TO / (m_MTO * g)): {1:F3}"
//                           + Environment.NewLine,
//             _request.RequestPurpose.LiftToDragRatio,
//             _request.RequestPurpose.MinThrustToWeightRatio2Segment);
//         
//         Assert.Multiple(() =>
//         {
//             Assert.That(_request.RequestPurpose.LiftToDragRatio,
//                 Is.EqualTo(Block4_LiftToDragRatio).Within(Globals.Eps));
//             Assert.That(_request.RequestPurpose.MinThrustToWeightRatio2Segment,
//                 Is.EqualTo(Block4_MinThrustToWeightRatio2Segment).Within(Globals.Eps));
//         });
//     }
//     
//     [Test]
//     public void Block5Calculating()
//     {
//         Block4Calculating();
//         
//         var block5ClimbRateDuringMissedApproach = new Block5ClimbRateDuringMissedApproach();
//         
//         block5ClimbRateDuringMissedApproach.Handle(_request);
//
//         Console.WriteLine("--- BLOCK 5 ---"
//                           + Environment.NewLine
//                           + "Минимальная тяговооруженность во время Missed Approach, min(T_TO / (m_MTO * g)): {0:F3}"
//                           + Environment.NewLine,
//             _request.RequestPurpose.MinThrustToWeightRatioMissedApproach);
//         
//         Assert.Multiple(() =>
//         {
//             Assert.That(_request.RequestPurpose.MinThrustToWeightRatioMissedApproach,
//                 Is.EqualTo(BLock5_MinThrustToWeightRatioMissedApproach).Within(Globals.Eps));
//         });
//     }
//     
//     [Test]
//     public void Block6Calculating()
//     {
//         Block5Calculating();
//         
//         var block6LiftToDragRatioDuringCruise = new Block6LiftToDragRatioDuringCruise();
//         
//         block6LiftToDragRatioDuringCruise.Handle(_request);
//
//         Console.WriteLine("--- BLOCK 6 ---"
//                           + Environment.NewLine
//                           + "Отношение подъемной силы к лобовому сопротивлению во время Cruise, E: {0:F3}"
//                           + Environment.NewLine,
//             _request.RequestPurpose.LiftToDragRatioCruise);
//         
//         Assert.Multiple(() =>
//         {
//             Assert.That(_request.RequestPurpose.LiftToDragRatioCruise,
//                 Is.EqualTo(Block6_LiftToDragRatioCruise).Within(Globals.Eps));
//         });
//     }
//     
//     [Test]
//     public void Block7Calculating()
//     {
//         Block6Calculating();
//         
//         var block7Cruise = new Block7Cruise();
//         
//         block7Cruise.Handle(_request);
//         
//         Console.WriteLine("--- BLOCK 7 ---"
//                           + Environment.NewLine
//                           + "Тяговооруженность во время Cruise на высоте h = 5000 м, : {0:F3}"
//                           + Environment.NewLine
//                           + "Нагрузка на крыло во время Cruise на высоте h = 5000 м, : {1:F3}"
//                           + Environment.NewLine,
//             _request.RequestPurpose.ThrustToWeightFunctionCruise(5000.0),
//             _request.RequestPurpose.WingLoadingFunctionCruise(5000.0));
//         
//         Assert.Multiple(() =>
//         {
//             Assert.That(_request.RequestPurpose.ThrustToWeightFunctionCruise(5000.0),
//                 Is.EqualTo(Block7_ThrustToWeightRatioCruiseAt5000).Within(Globals.Eps));
//             Assert.That(_request.RequestPurpose.WingLoadingFunctionCruise(5000.0),
//                 Is.EqualTo(Block7_WingLoadingCruiseAt5000).Within(Globals.Eps));
//         });
//     }
//     
//     [Test]
//     public void Block8Calculating()
//     {
//         Block7Calculating();
//         
//         var block8MatchingChart = new Block8MatchingChart();
//         
//         block8MatchingChart.Handle(_request);
//
//         Console.WriteLine("--- BLOCK 8 ---"
//                           + Environment.NewLine
//                           + "Высота крейсерского полета, h_CR: {0:F3}"
//                           + Environment.NewLine
//                           + "Удельная нагрузка на крыло, m_MTO/S_W: {1:F3}"
//                           + Environment.NewLine
//                           + "Тяговооруженность самолета, T_TO / (m_MTO * g): {2:F3}"
//                           + Environment.NewLine,
//             _request.RequestPurpose.AltitudeCruise,
//             _request.RequestPurpose.WingLoading,
//             _request.RequestPurpose.ThrustToWeightRatio);
//         
//         Assert.Multiple(() =>
//         {
//             Assert.That(_request.RequestPurpose.ThrustToWeightFunctionCruise(5000.0),
//                 Is.EqualTo(Block7_ThrustToWeightRatioCruiseAt5000).Within(Globals.Eps));
//             Assert.That(_request.RequestPurpose.WingLoadingFunctionCruise(5000.0),
//                 Is.EqualTo(Block7_WingLoadingCruiseAt5000).Within(Globals.Eps));
//         });
//     }
//
//     [Test]
//     public void MatchingChartForm()
//     {
//         Block8MatchingChart block8MatchingChart = new Block8MatchingChart();
//
//         block8MatchingChart.MatchingChartForm(_request);
//     }
// }
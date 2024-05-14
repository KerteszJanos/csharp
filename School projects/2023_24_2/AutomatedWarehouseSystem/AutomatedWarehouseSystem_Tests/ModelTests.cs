using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutomatedWarehouseSystem_ClassLib.Model;
using AutomatedWarehouseSystem_ClassLib.Model.AStarNoDeadLockCU;
using AutomatedWarehouseSystem_ClassLib.Persistence;
using System.IO;
using AutomatedWarehouseSystem_ClassLib.Model.ModelEventArgs;

namespace AutomatedWarehouseSystem_Tests
{
    /// <summary>
    ///  The main class for the application tests.
    /// </summary>
    [TestClass]
    public class ModelTests
    {
        #region private fiels
        private WarehouseSystem ws = null!;
        #endregion



        #region InitLogDataTests
        /// <summary>
        ///  Initialize map test
        /// </summary>
        [TestMethod]
        public void InitLogDataTest()
        {
            Inicialize5x5Warehouse();
            ws.InitLogData();

            Assert.AreEqual("MAPF_T", ws.ActionModel);
            Assert.IsTrue(ws.AllValid);
            Assert.AreEqual(2, ws.TeamSize);
            Assert.AreEqual(0, ws.NumTaskFinished);
            Assert.AreEqual(0, ws.SumOfCost);
            Assert.AreEqual(0, ws.MakeSpan);
            Assert.IsNotNull(ws.PlannerTimes);
            Assert.IsNotNull(ws.Errors);
            Assert.AreEqual(2, ws.EventsArray.Length);
        }
        /// <summary>
        /// Robot start position test
        /// </summary>
        [TestMethod]
        public void InitLogData_SaveRobotStartPositionsTest()
        {
            Inicialize5x5Warehouse();
            ws.InitLogData();
            Assert.AreEqual(ws.Robots.Length, ws.RobotStartPositions.Length);
        }
        /// <summary>
        /// Destination start position test
        /// </summary>
        [TestMethod]
        public void InitLogData_SaveDestStartPositionsTest()
        {
            Inicialize5x5Warehouse();
            ws.InitLogData();
            Assert.AreEqual(ws.Dests.Length, ws.DestStart.Length);
        }
        #endregion

        #region UserChooseDestTests
        /// <summary>
        /// The user select a new target location in the empty cell
        /// </summary>
        [TestMethod]
        public void UserChooseDestTest_OnEmptyCell()
        {
            Inicialize5x5Warehouse();
            ws.InitLogData();
            ws.UserChooseDest((0, 0));
            Assert.AreEqual((-1, -1), ws.UserDest);
        }
        /// <summary>
        /// The user select a new target location in the non-empty cell
        /// </summary>
        [TestMethod]
        public void UserChooseDestTest_OnNotEmptyCell()
        {
            Inicialize5x5Warehouse();
            ws.InitLogData();
            ws.UserChooseDest((2, 1));
            Assert.AreEqual((2, 1), ws.UserDest);
        }
        #endregion

        #region RoundRobinTests
        /// <summary>
        /// Handled tasks number after roundrobin method test
        /// </summary>
        [TestMethod]
        public void RoundRobin_TaskHandledOutTest()
        {
            Inicialize5x5Warehouse();
            ws.InitLogData();

            Assert.AreEqual(0, ws.Tasks.Count);
            ws.RoundRobin();
            Assert.AreEqual(ws.NumTasksReveal, ws.Tasks.Count);
        }
        /// <summary>
        /// Added tasks number after roundrobin method test
        /// </summary>
        [TestMethod]
        public void RoundRobin_TaskAddedAsEventTests()
        {
            Inicialize5x5Warehouse();
            ws.InitLogData();

            ws.RoundRobin();
            Assert.AreEqual(1, ws.Tasks.Count);
        }
        /// <summary>
        /// Deleted destination after roundrobin method test
        /// </summary>
        [TestMethod]
        public void RoundRobin_DelDestAfterHandledOutTest()
        {
            Inicialize5x5Warehouse();
            ws.InitLogData();

            Assert.AreEqual(3, ws.Dests.Length);
            ws.RoundRobin();
            Assert.AreEqual(2, ws.Dests.Length);
        }
        /// <summary>
        /// The user choose destination after roundrobin method test
        /// </summary>
        [TestMethod]
        public void roundRobin_ChooseUserDestFstTest()
        {
            Inicialize5x5Warehouse();
            ws.InitLogData();
            ws.UserChooseDest((2, 1));
            ws.RoundRobin();

            Assert.AreEqual((2, 1), (ws.Tasks[0].TaskDestination.X, ws.Tasks[0].TaskDestination.Y));
            Assert.AreEqual(3, ws.Dests.Length);
            Assert.AreEqual((-1, -1), ws.UserDest);
        }
        #endregion

        #region StartSimulation-/Analysys-Tests
        /// <summary>
        /// Simulation start test
        /// </summary>
        [TestMethod]
        public void StartSimulationTest()
        {
            Inicialize5x5Warehouse();
            ws.InitLogData();

            int eventCtr = 0;
            ws.StartTheSimulation += (object? _, SimulationStartEventArgs _) => { eventCtr++; };
            ws.StartSimulation("5", "1");

            Assert.AreEqual(1, eventCtr);
            Assert.AreEqual(0, ws.StepNum);
            Assert.AreEqual(5, ws.TotalStepNum);
            Assert.AreEqual(1000, ws.TimeLimit);
        }
        /// <summary>
        /// Analysis start test
        /// </summary>
        [TestMethod]
        public void StartAnalysisTest()
        {
            Inicialize5x5Warehouse();
            ws.InitLogData();
            ws.Events = ws.EventsArray.ToList<List<Event>>();

            int eventCtr = 0;
            ws.RobotPositionsChanged += (object? _, RobotPositionsChangedEventArgs _) => { eventCtr++; };
            ws.StartTheAnalysis += (object? _, SimulationStartEventArgs _) => { eventCtr++; };
            ws.StartAnalysis("1");

            Assert.AreEqual(2, eventCtr);
            Assert.AreEqual(0, ws.StepNum);
            Assert.AreEqual(ws.MakeSpan, ws.TotalStepNum);
            Assert.AreEqual(1000, ws.TimeLimit);
        }
        /// <summary>
        /// Analysis start test with given robot position list
        /// </summary>
        [TestMethod]
        public void StartAnalysis_InitRobotPositionsListTest()
        {
            Inicialize5x5Warehouse();
            ws.InitLogData();
            ws.Events = ws.EventsArray.ToList<List<Event>>();

            ws.RobotPositionsChanged += (object? _, RobotPositionsChangedEventArgs _) => { };
            ws.StartTheAnalysis += (object? _, SimulationStartEventArgs _) => { };
            ws.StartAnalysis("1");

            Assert.AreEqual(1, ws.RobotPositions.Count);
        }
        /// <summary>
        /// Analysis start test with given destination position list
        /// </summary>
        [TestMethod]
        public void StartAnalysis_InitDestinationPositionListTest()
        {
            Inicialize5x5Warehouse();
            ws.InitLogData();
            ws.Events = ws.EventsArray.ToList<List<Event>>();

            ws.RobotPositionsChanged += (object? _, RobotPositionsChangedEventArgs _) => { };
            ws.StartTheAnalysis += (object? _, SimulationStartEventArgs _) => { };
            ws.StartAnalysis("1");

            Assert.AreEqual(0, ws.DestPositions.Count);
        }
        #endregion

        #region TimerIntervalChangeTests
        /// <summary>
        /// Test the time interval step forward with one sec
        /// </summary>
        [TestMethod]
        public void TimerIntervalChange_ForewardTest()
        {
            Inicialize5x5Warehouse();
            int eventCtr = 0;
            ws.TimerPerdiodChanged += (object? _, TimerPeriodChangedEventArgs _) => { eventCtr++; };

            ws.TimeLimit = 2;
            ws.TimerIntervalChange("foreward");

            Assert.AreEqual(1, eventCtr);
            Assert.AreEqual(1, ws.TimeLimit);
        }
        /// <summary>
        /// Test the time interval step backward with one sec
        /// </summary>
        [TestMethod]
        public void TimerIntervalChange_BackwardTest()
        {
            Inicialize5x5Warehouse();
            int eventCtr = 0;
            ws.TimerPerdiodChanged += (object? _, TimerPeriodChangedEventArgs _) => { eventCtr++; };

            ws.TimeLimit = 1;
            ws.TimerIntervalChange("backward");

            Assert.AreEqual(1, eventCtr);
            Assert.AreEqual(2, ws.TimeLimit);
        }
        #endregion

        #region StepRobotsTests
        /// <summary>
        /// Test the robot steps with 10 steps execution
        /// </summary>
        [TestMethod]
        public void StepRobots_GeneralTest() //testing if the robots step, when they can
        {
            Inicialize5x5Warehouse();
            ws.InitLogData();
            ws.RoundRobin();
            ws.RobotPositionsChanged += (object? _, RobotPositionsChangedEventArgs _) => { };
            ws.DestinationChanged += (object? _, DestinationChangedEventArgs _) => { };
            ws.SimulationEnded += (object? _, SimulationEndedEventArgs _) => { };

            Robot[] prevRobots = ws.Robots;
            for (int j = 0; j < 10; j++) //10 step
            {
                ws.StepRobots();
            }

            int i = 0;
            while (i < ws.Robots.Length && ws.Robots[i] != prevRobots[i]) i++;
            Assert.IsTrue(i < ws.Robots.Length); //so there is at least 1 robot that moved
        }
        /// <summary>
        /// Test the robot steps with increased stepnumber
        /// </summary>
        [TestMethod]
        public void StepRobots_StepNumIncreaseing()
        {
            Inicialize5x5Warehouse();
            ws.InitLogData();
            ws.RoundRobin();
            ws.RobotPositionsChanged += (object? _, RobotPositionsChangedEventArgs _) => { };
            ws.DestinationChanged += (object? _, DestinationChangedEventArgs _) => { };
            ws.SimulationEnded += (object? _, SimulationEndedEventArgs _) => { };

            Assert.AreEqual(0, ws.StepNum);
        }
        /// <summary>
        /// Test the robot steps lenght and number
        /// </summary>
        [TestMethod]
        public void StepRobots_FstTask()
        {
            Inicialize5x5Warehouse();
            ws.InitLogData();
            ws.RoundRobin();
            ws.RobotPositionsChanged += (object? _, RobotPositionsChangedEventArgs _) => { };
            ws.DestinationChanged += (object? _, DestinationChangedEventArgs _) => { };
            ws.SimulationEnded += (object? _, SimulationEndedEventArgs _) => { };

            for (int i = 0; i < 5; i++)
            {
                ws.StepRobots();
            }
            Assert.AreEqual(2, ws.Dests.Length);
            Assert.AreEqual(1, ws.Tasks.Count);
        }
        #endregion

        #region ControlUnitTests
        /// <summary>
        /// Calculate the correct robot step with direction turn
        /// </summary>
        [TestMethod]
        public void CUStep_1Robot_CalculateNextStepCorrectly_TurnTest()
        {
            AStarNoDeadLockControlUnit CU = new AStarNoDeadLockControlUnit();
            StepTestClass stc = StepTestClass.Inicialize5x5_1Robot_1Task();
            CU.Step(stc.map, ref stc.robots, ref stc.tasks, ref stc.numTaskFinished, ref stc.stepNum);

            Assert.AreEqual(0, stc.robots[0].X);
            Assert.AreEqual(2, stc.robots[0].Y);
            Assert.AreEqual(Direction.West, stc.robots[0].Direction);
        }
        /// <summary>
        /// Calculate the correct robot forward step
        /// </summary>
        [TestMethod]
        public void CUStep_1Robot_CalculateNextStepCorrectly_ForwardTest()
        {
            AStarNoDeadLockControlUnit CU = new AStarNoDeadLockControlUnit();
            StepTestClass stc = StepTestClass.Inicialize5x5_1Robot_1Task();
            for (int i = 0; i < 2; i++)
            {
                CU.Step(stc.map, ref stc.robots, ref stc.tasks, ref stc.numTaskFinished, ref stc.stepNum);
            }


            Assert.AreEqual(0, stc.robots[0].X);
            Assert.AreEqual(1, stc.robots[0].Y);
            Assert.AreEqual(Direction.West, stc.robots[0].Direction);
        }
        /// <summary>
        /// Creat the correct full path for 1 robot
        /// </summary>
        [TestMethod]
        public void CUStep_1Robot_FindFullPathtTest()
        {
            AStarNoDeadLockControlUnit CU = new AStarNoDeadLockControlUnit();
            int eventCtr = 0;
            CU.TaskFinished += (object? _, TaskFinishedEventArgs _) => { eventCtr++; };
            StepTestClass stc = StepTestClass.Inicialize5x5_1Robot_1Task();
            for (int i = 0; i < 6; i++)
            {
                CU.Step(stc.map, ref stc.robots, ref stc.tasks, ref stc.numTaskFinished, ref stc.stepNum);
            }

            Assert.AreEqual(2, stc.robots[0].X);
            Assert.AreEqual(0, stc.robots[0].Y);
            Assert.AreEqual(Direction.South, stc.robots[0].Direction);
            Assert.AreEqual(1, eventCtr);
            Assert.AreEqual(0, stc.tasks.Count);
        }
        /// <summary>
        /// Find the correct next step for robot with blocked path
        /// </summary>
        [TestMethod]
        public void CUStep_1Robot_FindNextStepWhenAfkBlockingSortestPathTest()
        {
            AStarNoDeadLockControlUnit CU = new AStarNoDeadLockControlUnit();
            StepTestClass stc = StepTestClass.Inicialize5x5_1Robot_1Task_AfkOnSortertPath();
            CU.Step(stc.map, ref stc.robots, ref stc.tasks, ref stc.numTaskFinished, ref stc.stepNum);

            Assert.AreEqual(1, stc.robots[0].X);
            Assert.AreEqual(2, stc.robots[0].Y);
            Assert.AreEqual(Direction.South, stc.robots[0].Direction);
        }
        /// <summary>
        /// Find the correct full path for robot with blocked path
        /// </summary>
        [TestMethod]
        public void CUStep_1Robot_FindFullPathWhenAfkBlockingSortestPathTest()
        {
            AStarNoDeadLockControlUnit CU = new AStarNoDeadLockControlUnit();
            int eventCtr = 0;
            CU.TaskFinished += (object? _, TaskFinishedEventArgs _) => { eventCtr++; };
            StepTestClass stc = StepTestClass.Inicialize5x5_1Robot_1Task_AfkOnSortertPath();
            for (int i = 0; i < 10; i++)
            {
                CU.Step(stc.map, ref stc.robots, ref stc.tasks, ref stc.numTaskFinished, ref stc.stepNum);
            }

            Assert.AreEqual(2, stc.robots[0].X);
            Assert.AreEqual(0, stc.robots[0].Y);
            Assert.AreEqual(Direction.North, stc.robots[0].Direction);
            Assert.AreEqual(1, eventCtr);
            Assert.AreEqual(0, stc.tasks.Count);
        }
        /// <summary>
        /// Test the calculation of 2 robots next correct step with turn direction change
        /// </summary>
        [TestMethod]
        public void CUStep_2Robot_CalculateNextStepCorrectly_TurnTest()
        {
            AStarNoDeadLockControlUnit CU = new AStarNoDeadLockControlUnit();
            StepTestClass stc = StepTestClass.Inicialize5x5_2Robot_2Task();
            CU.Step(stc.map, ref stc.robots, ref stc.tasks, ref stc.numTaskFinished, ref stc.stepNum);

            Assert.AreEqual(0, stc.robots[0].X);
            Assert.AreEqual(2, stc.robots[0].Y);
            Assert.AreEqual(Direction.West, stc.robots[0].Direction);

            Assert.AreEqual(4, stc.robots[1].X);
            Assert.AreEqual(2, stc.robots[1].Y);
            Assert.AreEqual(Direction.East, stc.robots[1].Direction);
        }
        /// <summary>
        /// Test the calculation of 2 robots next correct step with forward direction change
        /// </summary>
        [TestMethod]
        public void CUStep_2Robot_CalculateNextStepCorrectly_ForwardTest()
        {
            AStarNoDeadLockControlUnit CU = new AStarNoDeadLockControlUnit();
            StepTestClass stc = StepTestClass.Inicialize5x5_2Robot_2Task();
            for (int i = 0; i < 2; i++)
            {
                CU.Step(stc.map, ref stc.robots, ref stc.tasks, ref stc.numTaskFinished, ref stc.stepNum);
            }

            Assert.AreEqual(0, stc.robots[0].X);
            Assert.AreEqual(1, stc.robots[0].Y);
            Assert.AreEqual(Direction.West, stc.robots[0].Direction);
            Assert.AreEqual(4, stc.robots[1].X);
            Assert.AreEqual(3, stc.robots[1].Y);
            Assert.AreEqual(Direction.East, stc.robots[1].Direction);
        }
        /// <summary>
        /// Find the correct full path for 2 robots test
        /// </summary>
        [TestMethod]
        public void CUStep_2Robot_FindFullPathtTest()
        {
            AStarNoDeadLockControlUnit CU = new AStarNoDeadLockControlUnit();
            int eventCtr = 0;
            CU.TaskFinished += (object? _, TaskFinishedEventArgs _) => { eventCtr++; };
            StepTestClass stc = StepTestClass.Inicialize5x5_2Robot_2Task();
            for (int i = 0; i < 7; i++)
            {
                CU.Step(stc.map, ref stc.robots, ref stc.tasks, ref stc.numTaskFinished, ref stc.stepNum);
            }

            Assert.AreEqual(2, eventCtr);
            Assert.AreEqual(0, stc.tasks.Count);
        }
        /// <summary>
        /// Increase the finished tasks number test
        /// </summary>
        [TestMethod]
        public void CUStep_NumTaskFinishedIncreazedTest()
        {
            AStarNoDeadLockControlUnit CU = new AStarNoDeadLockControlUnit();
            CU.TaskFinished += (object? _, TaskFinishedEventArgs _) => { };
            StepTestClass stc = StepTestClass.Inicialize5x5_1Robot_1Task();
            for (int i = 0; i < 6; i++)
            {
                CU.Step(stc.map, ref stc.robots, ref stc.tasks, ref stc.numTaskFinished, ref stc.stepNum);
            }

            Assert.AreEqual(1, stc.numTaskFinished);
        }
        /// <summary>
        /// Increase the robot steps number test
        /// </summary>
        [TestMethod]
        public void CUStep_StepNumIncreazedTest()
        {
            AStarNoDeadLockControlUnit CU = new AStarNoDeadLockControlUnit();
            CU.TaskFinished += (object? _, TaskFinishedEventArgs _) => { };
            StepTestClass stc = StepTestClass.Inicialize5x5_1Robot_1Task();
            for (int i = 0; i < 3; i++)
            {
                CU.Step(stc.map, ref stc.robots, ref stc.tasks, ref stc.numTaskFinished, ref stc.stepNum);
            }

            Assert.AreEqual(3, stc.stepNum);
        }
        /// <summary>
        /// Test the robots deadlock situation solving method
        /// </summary>
        [TestMethod]
        public void CUStep_RobotsSolveDeadlockTest()
        {
            AStarNoDeadLockControlUnit CU = new AStarNoDeadLockControlUnit();
            CU.TaskFinished += (object? _, TaskFinishedEventArgs _) => { };
            StepTestClass stc = StepTestClass.Inicialize5x5_2Robot_2Task_DEADLOCK();
            for (int i = 0; i < 2; i++)
            {
                CU.Step(stc.map, ref stc.robots, ref stc.tasks, ref stc.numTaskFinished, ref stc.stepNum);
            }

            Assert.AreEqual(Direction.North, stc.robots[0].Direction);
            Assert.AreEqual(Direction.South, stc.robots[1].Direction);
        }
        /// <summary>
        /// Test the robots deadlock situation solving method and than find the destination
        /// </summary>
        [TestMethod]
        public void CUStep_RobotsSolveDeadlock_AndFindDestTest()
        {
            AStarNoDeadLockControlUnit CU = new AStarNoDeadLockControlUnit();
            int eventCtr = 0;
            CU.TaskFinished += (object? _, TaskFinishedEventArgs _) => { eventCtr++; };
            StepTestClass stc = StepTestClass.Inicialize5x5_2Robot_2Task_DEADLOCK();
            foreach (var robot in stc.robots)
            {
                robot.GenerateError += (object? _, GenerateErrorEventArgs _) => { };
            }
            for (int i = 0; i < 14; i++)
            {
                CU.Step(stc.map, ref stc.robots, ref stc.tasks, ref stc.numTaskFinished, ref stc.stepNum);
            }

            Assert.AreEqual(2, eventCtr);
            Assert.AreEqual(0, stc.tasks.Count);
        }
        /// <summary>
        /// Test the robot step creation method in blocked path situation
        /// </summary>
        [TestMethod]
        public void CUStep_RobotClearPathIfAfkBlockingAll()
        {
            AStarNoDeadLockControlUnit CU = new AStarNoDeadLockControlUnit();
            int eventCtr = 0;
            CU.TaskFinished += (object? _, TaskFinishedEventArgs _) => { eventCtr++; };
            StepTestClass stc = StepTestClass.Inicialize5x5_1Robot_1Task_AfkBlockingAllPath();
            foreach (var robot in stc.robots)
            {
                robot.GenerateError += (object? _, GenerateErrorEventArgs _) => { };
            }
            for (int i = 0; i < 9; i++)
            {
                CU.Step(stc.map, ref stc.robots, ref stc.tasks, ref stc.numTaskFinished, ref stc.stepNum);
            }

            Assert.AreEqual(1, eventCtr);
            Assert.AreEqual(0, stc.tasks.Count);

        }
        #endregion

        #region Other tests
        /// <summary>
        /// Test the robot step creation in specific situation
        /// </summary>
        [TestMethod]
        public void LoadSpecificStepTest()
        {
            Inicialize5x5Warehouse();
            ws.DestPositions = new List<Destination[]> { new Destination[] { new Destination(-1, -1) } };
            ws.RobotPositions = new List<Robot[]> { new Robot[] { new Robot(-1, -1) }, new Robot[] { new Robot(-1, -1) } };
            int eventCtr = 0;
            ws.DestinationChanged += (object? _, DestinationChangedEventArgs _) => { eventCtr++; };
            ws.RobotPositionsChanged += (object? _, RobotPositionsChangedEventArgs _) => { eventCtr++; };

            ws.LoadSpecificStep(0);

            Assert.AreEqual(2, eventCtr);
            Assert.AreEqual(0, ws.StepNum);
        }
        /// <summary>
        /// Test the robot next step creation when steps number is less than total steps number
        /// </summary>
        [TestMethod]
        public void LoadNextStep_StepNumLessThanTotalStepNumTest()
        {
            Inicialize5x5Warehouse();
            ws.InitLogData();
            ws.DestPositions = new List<Destination[]> { new Destination[] { new Destination(-1, -1) } };
            ws.RobotPositions = new List<Robot[]> { new Robot[] { new Robot(-1, -1) }, new Robot[] { new Robot(-1, -1) } };
            int eventCtr = 0;
            ws.DestinationChanged += (object? _, DestinationChangedEventArgs _) => { eventCtr++; };
            ws.RobotPositionsChanged += (object? _, RobotPositionsChangedEventArgs _) => { eventCtr++; };
            ws.AnalysisEnded += (object? _, AnalysisEndedEventArgs _) => { eventCtr++; };
            ws.StepNum = 0;
            ws.TotalStepNum = -1;

            ws.LoadNextStep();
            Assert.AreEqual(2, eventCtr);
            Assert.AreEqual(1, ws.StepNum);
        }
        /// <summary>
        /// Test the robot next step creation when steps number is equal with total steps number
        /// </summary>
        [TestMethod]
        public void LoadNextStep_StepNumEqTotalStepNumTest()
        {
            Inicialize5x5Warehouse();
            ws.InitLogData();
            ws.DestPositions = new List<Destination[]> { new Destination[] { new Destination(-1, -1) } };
            ws.RobotPositions = new List<Robot[]> { new Robot[] { new Robot(-1, -1) }, new Robot[] { new Robot(-1, -1) } };
            int eventCtr = 0;
            ws.DestinationChanged += (object? _, DestinationChangedEventArgs _) => { eventCtr++; };
            ws.RobotPositionsChanged += (object? _, RobotPositionsChangedEventArgs _) => { eventCtr++; };
            ws.AnalysisEnded += (object? _, AnalysisEndedEventArgs _) => { eventCtr++; };
            ws.StepNum = 0;
            ws.TotalStepNum = 0;

            ws.LoadNextStep();
            Assert.AreEqual(1, eventCtr);
            Assert.AreEqual(0, ws.StepNum);
        }
        #endregion



        #region private methods/classes
        private void Inicialize5x5Warehouse()
        {
            ws = new WarehouseSystem();
            ws.Map = new Map("octile", 5, 5, new bool[5, 5] {{ false, false, true, false, false },
                                                             { false, false, true, false, false },
                                                             { true, true, true, true, true },
                                                             { false, false, true, false, false },
                                                             { false, false, true, false, false }});
            ws.Robots = new Robot[2] { new Robot(0, 2, 'S', new List<char>(), new List<char>()), new Robot(4, 2, 'N', new List<char>(), new List<char>()) };
            ws.Dests = new Destination[3] { new Destination(2, 0), new Destination(2, 2), new Destination(2, 4) };
            ws.NumTasksReveal = 1;
            ws.TaskAssignmentStrategy = "roundrobin";
        }

        /// <summary>
        /// Inicialize complete maps with tasks, robots and paths for testmethods
        /// </summary>
        private class StepTestClass
        {
            public Map map;
            public Robot[] robots;
            public List<Mytask> tasks;
            public int numTaskFinished;
            public int stepNum;

            private StepTestClass()
            {
                this.map = null!;
                this.robots = null!;
                this.tasks = null!;
            }

            /// <summary>
            /// Inicialize the 5x5 map with 1 tasks, 1 robot for testmethods
            /// </summary>
            public static StepTestClass Inicialize5x5_1Robot_1Task()
            {
                StepTestClass stc = new StepTestClass();
                stc.map = new Map("octile", 5, 5, new bool[5, 5] {{ true, true, true, true, true },
                                                                  { true, false, true, false, true },
                                                                  { true, false, true, false, true },
                                                                  { true, false, true, false, true },
                                                                  { true, true, true, true, true }});
                stc.robots = new Robot[2] { new Robot(0, 2, 'S', new List<char>(), new List<char>()), new Robot(4, 2, 'N', new List<char>(), new List<char>()) };
                stc.tasks = new List<Mytask> { new Mytask(0, stc.robots[0], new Destination(2, 0)) };
                stc.numTaskFinished = 0;
                stc.stepNum = 0;
                return stc;
            }
            /// <summary>
            /// Inicialize the 5x5 map with 1 tasks, 1 robot and sorter path test
            /// </summary>
            public static StepTestClass Inicialize5x5_1Robot_1Task_AfkOnSortertPath()
            {
                StepTestClass stc = new StepTestClass();
                stc.map = new Map("octile", 5, 5, new bool[5, 5] {{ true, true, true, true, true },
                                                                  { true, false, true, false, true },
                                                                  { true, false, true, false, true },
                                                                  { true, false, true, false, true },
                                                                  { true, true, true, true, true }});
                stc.robots = new Robot[2] { new Robot(0, 2, 'S', new List<char>(), new List<char>()), new Robot(0, 0, 'N', new List<char>(), new List<char>()) };
                stc.tasks = new List<Mytask> { new Mytask(0, stc.robots[0], new Destination(2, 0)) };
                stc.numTaskFinished = 0;
                stc.stepNum = 0;
                return stc;
            }
            /// <summary>
            /// Inicialize the 5x5 map with 1 tasks, 1 robot for the blocked path test
            /// </summary>
            public static StepTestClass Inicialize5x5_1Robot_1Task_AfkBlockingAllPath()
            {
                StepTestClass stc = new StepTestClass();
                stc.map = new Map("octile", 5, 5, new bool[5, 5] {{ true, true, true, true, false },
                                                                  { false, false, true, false, false },
                                                                  { false, false, true, false, false },
                                                                  { false, false, true, false, false },
                                                                  { false, false, true, false, false }});
                stc.robots = new Robot[2] { new Robot(1, 2, 'N', new List<char>(), new List<char>()), new Robot(0, 1, 'E', new List<char>(), new List<char>()) };
                stc.tasks = new List<Mytask> { new Mytask(0, stc.robots[0], new Destination(0, 0)) };
                stc.numTaskFinished = 0;
                stc.stepNum = 0;
                return stc;
            }
            /// <summary>
            /// Inicialize the 5x5 map with 2 tasks, 2 robot for testmethods
            /// </summary>
            public static StepTestClass Inicialize5x5_2Robot_2Task()
            {
                StepTestClass stc = new StepTestClass();
                stc.map = new Map("octile", 5, 5, new bool[5, 5] {{ true, true, true, true, true },
                                                                  { true, false, true, false, true },
                                                                  { true, false, true, false, true },
                                                                  { true, false, true, false, true },
                                                                  { true, true, true, true, true }});
                stc.robots = new Robot[2] { new Robot(0, 2, 'S', new List<char>(), new List<char>()), new Robot(4, 2, 'N', new List<char>(), new List<char>()) };
                stc.tasks = new List<Mytask> { new Mytask(0, stc.robots[0], new Destination(2, 0)), new Mytask(1, stc.robots[1], new Destination(2, 4)) };
                stc.numTaskFinished = 0;
                stc.stepNum = 0;
                return stc;
            }
            /// <summary>
            /// Inicialize the 5x5 map with 2 tasks, 2 robot for deadlock situation test
            /// </summary>
            public static StepTestClass Inicialize5x5_2Robot_2Task_DEADLOCK()
            {
                StepTestClass stc = new StepTestClass();
                stc.map = new Map("octile", 5, 5, new bool[5, 5] {{ true, true, true, true, true },
                                                                  { true, false, true, false, true },
                                                                  { true, false, true, false, true },
                                                                  { true, false, true, false, true },
                                                                  { true, true, true, true, true }});
                stc.robots = new Robot[2] { new Robot(2, 2, 'S', new List<char>(), new List<char>()), new Robot(3, 2, 'N', new List<char>(), new List<char>()) };
                stc.tasks = new List<Mytask> { new Mytask(0, stc.robots[0], new Destination(4, 2)), new Mytask(1, stc.robots[1], new Destination(0, 2)) };
                stc.numTaskFinished = 0;
                stc.stepNum = 0;
                return stc;
            }
        }
        #endregion
    }
}
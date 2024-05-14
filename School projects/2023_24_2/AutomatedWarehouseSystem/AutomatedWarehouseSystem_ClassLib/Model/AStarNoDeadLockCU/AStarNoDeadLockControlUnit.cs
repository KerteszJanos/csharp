using AutomatedWarehouseSystem_ClassLib.Model.ModelEventArgs;
using AutomatedWarehouseSystem_ClassLib.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedWarehouseSystem_ClassLib.Model.AStarNoDeadLockCU
{
    /// <summary>
    /// A* path finding algorithm without deadlock situation
    /// </summary>
    public class AStarNoDeadLockControlUnit : IControlUnit
    {
        #region fields/properties
        private int robotStepCtr;
        #endregion

        #region constructors
        /// <summary>
        /// A* path finding algorithm without deadlock situation constructor
        /// </summary>
        public AStarNoDeadLockControlUnit()
        {
            robotStepCtr = 1;
        }
        #endregion

        #region public methods
        /// <summary>
        /// Implementation of the step method related to A* algorithm
        /// </summary>
        public void Step(in Map map, ref Robot[] robots, ref List<Mytask> tasks, ref int numTaskFinished, ref int stepNum)
        {
            //no need to else here, bc if there is multiple robots they should move
            (int, int) xy;
            Graph Graph = new Graph(in map, in robots);

            List<(int, int)> Listxy = new List<(int, int)>();
            GraphWithAfks GgraphWithAfks;

            List<Mytask> newTaskList = tasks;
            for (int i = 0; i < tasks.Count; i++) //loop trough robots whos have tasks
            {
                //add current robot to graph
                Graph.AddNode(robots[tasks[i].RobotInd].X, robots[tasks[i].RobotInd].Y);

                //run pathfinging algorythm fot the specified robot
                xy = Graph.AStarStep((robots[tasks[i].RobotInd].X, robots[tasks[i].RobotInd].Y), (tasks[i].TaskDestination.X, tasks[i].TaskDestination.Y));

                if (xy == (-1, -1))
                {
                    //the current robot waiting to clear down a path
                    robots[tasks[i].RobotInd].LogWait();
                    robots[tasks[i].RobotInd].ErrorWaitForOthers();

                    GgraphWithAfks = new(in map, in robots);
                    Listxy = GgraphWithAfks.AStarStep((robots[tasks[i].RobotInd].X, robots[tasks[i].RobotInd].Y), (tasks[i].TaskDestination.X, tasks[i].TaskDestination.Y));
                    StepDownAfkRobotsFromPath(ref robots, in Listxy, in map);
                }
                else
                {
                    if (tasks[i].TaskDestination.X == xy.Item1 && tasks[i].TaskDestination.Y == xy.Item2) //should empty the 
                    {
                        //    step the robot (also add path to actualpath)
                        if (StepRobotWhatAreUDoing(ref robots[tasks[i].RobotInd], xy)) //finished only if stepped on the destination
                        {
                            tasks[i].IsFinished = true;
                            OnTaskFinished(new TaskFinishedEventArgs(robots[tasks[i].RobotInd].Id, tasks[i].TaskDestination.Id));
                            numTaskFinished++;
                        }
                    }
                    else //is just a normal empty cell
                    {
                        StepRobotWhatAreUDoing(ref robots[tasks[i].RobotInd], xy);
                    }
                }
                //del current robot to graph after schearch
                Graph.DelNode(robots[tasks[i].RobotInd].X, robots[tasks[i].RobotInd].Y);
            }
            stepNum++;
            foreach (Robot robot in robots)
            {
                if (robot.CurrentDestination == null && robot.ActualPath.Count < stepNum)
                {
                    robot.LogWait();
                }
            }
            //clearing the tasks if robot finished
            for (int i = 0; i < tasks.Count; i++)
            {
                if (tasks[i].IsFinished)
                {
                    //sets the robot.CurrentDestination to null
                    robots[tasks[i].RobotInd].CurrentDestination = null!;
                    //delete the specified task from task list
                    newTaskList.Remove(tasks[i]);
                }
            }
            tasks = newTaskList;
        }
        #endregion

        #region private methods
        private bool RobotCanMoveInThatDir(int dir, Robot robot, in Map map, in Robot[] robots)
        {
            bool IsEmptyCell(in Map map, in Robot[] robots, int x, int y)
            {
                int i = 0;
                while (i < robots.Length && (robots[i].X != x || robots[i].Y != y)) i++;

                return map[x, y] && i == robots.Length;
            }
            switch (dir)
            {
                case 1: //up
                    return robot.X - 1 >= 0 && IsEmptyCell(map, robots, robot.X - 1, robot.Y);
                case 2: //right
                    return robot.Y + 1 < map.Width && IsEmptyCell(map, robots, robot.X, robot.Y + 1);
                case 3: //down
                    return robot.X + 1 < map.Height && IsEmptyCell(map, robots, robot.X + 1, robot.Y);
                case 4: //left
                    return robot.Y - 1 >= 0 && IsEmptyCell(map, robots, robot.X, robot.Y - 1);
                default:
                    return false;
            }
        }

        /// <summary>
        /// Stepping a robot with a sus name, and set robot.CurrentDestination to null
        /// </summary>
        private bool StepRobotWhatAreUDoing(ref Robot robot, (int, int) xy)
        {
            //need to set the robot in the direction first and it if looks the good we then step
            if (robot.X > xy.Item1) //up
            {
                if (robot.Direction == Direction.North)
                {
                    robot.X = xy.Item1;
                    robot.Y = xy.Item2;
                    robot.LogForwardStep();
                    return true;
                }
                else if (robot.Direction == Direction.South)
                {
                    robot.Direction = Direction.West;
                    robot.LogRightTurn();
                }
                else if (robot.Direction == Direction.West)
                {
                    robot.Direction = Direction.North;
                    robot.LogRightTurn();
                }
                else
                {
                    robot.Direction = Direction.North;
                    robot.LogLeftTurn();
                }
            }
            else if (robot.X < xy.Item1) //down
            {
                if (robot.Direction == Direction.South)
                {
                    robot.X = xy.Item1;
                    robot.Y = xy.Item2;
                    robot.LogForwardStep();
                    return true;
                }
                else if (robot.Direction == Direction.North)
                {
                    robot.Direction = Direction.East;
                    robot.LogRightTurn();
                }
                else if (robot.Direction == Direction.East)
                {
                    robot.Direction = Direction.South;
                    robot.LogRightTurn();
                }
                else
                {
                    robot.Direction = Direction.South;
                    robot.LogLeftTurn();
                }
            }
            else if (robot.Y > xy.Item2) //left
            {
                if (robot.Direction == Direction.West)
                {
                    robot.X = xy.Item1;
                    robot.Y = xy.Item2;
                    robot.LogForwardStep();
                    return true;
                }
                else if (robot.Direction == Direction.East)
                {
                    robot.Direction = Direction.South;
                    robot.LogRightTurn();
                }
                else if (robot.Direction == Direction.South)
                {
                    robot.Direction = Direction.West;
                    robot.LogRightTurn();
                }
                else
                {
                    robot.Direction = Direction.West;
                    robot.LogLeftTurn();
                }
            }
            else if (robot.Y < xy.Item2) //right
            {
                if (robot.Direction == Direction.East)
                {
                    robot.X = xy.Item1;
                    robot.Y = xy.Item2;
                    robot.LogForwardStep();
                    return true;
                }
                else if (robot.Direction == Direction.West)
                {
                    robot.Direction = Direction.North;
                    robot.LogRightTurn();
                }
                else if (robot.Direction == Direction.North)
                {
                    robot.Direction = Direction.East;
                    robot.LogRightTurn();
                }
                else
                {
                    robot.Direction = Direction.East;
                    robot.LogLeftTurn();
                }
            }
            else
            {
                //we are here if the robot standing on the position already, nothing to do then
                robot.LogWait();
            }
            return false;
        }
        private void StepAfkRobot(ref Robot robot, in Map map, in Robot[] robots)
        {
            //could be a random generator here bc of the direction
            //if cant step in the direction its facing, get a random one or something
            if (robotStepCtr < 11) //up logic
            {
                if (RobotCanMoveInThatDir(1, robot, map, robots)) //up
                {
                    StepRobotWhatAreUDoing(ref robot, (robot.X - 1, robot.Y));
                }
                else if (RobotCanMoveInThatDir(2, robot, map, robots)) //right
                {
                    StepRobotWhatAreUDoing(ref robot, (robot.X, robot.Y + 1));
                }
                else if (RobotCanMoveInThatDir(3, robot, map, robots)) //down
                {
                    StepRobotWhatAreUDoing(ref robot, (robot.X + 1, robot.Y));
                }
                else if (RobotCanMoveInThatDir(4, robot, map, robots)) //left
                {
                    StepRobotWhatAreUDoing(ref robot, (robot.X, robot.Y - 1));
                }
                robotStepCtr++;
            }
            else if (10 < robotStepCtr && robotStepCtr < 20) //down logic
            {
                if (RobotCanMoveInThatDir(3, robot, map, robots)) //down
                {
                    StepRobotWhatAreUDoing(ref robot, (robot.X + 1, robot.Y));
                }
                else if (RobotCanMoveInThatDir(4, robot, map, robots)) //left
                {
                    StepRobotWhatAreUDoing(ref robot, (robot.X, robot.Y - 1));
                }
                else if (RobotCanMoveInThatDir(1, robot, map, robots)) //up
                {
                    StepRobotWhatAreUDoing(ref robot, (robot.X - 1, robot.Y));
                }
                else if (RobotCanMoveInThatDir(2, robot, map, robots)) //right
                {
                    StepRobotWhatAreUDoing(ref robot, (robot.X, robot.Y + 1));
                }
                robotStepCtr++;
            }
            else //last from down logic
            {
                if (RobotCanMoveInThatDir(3, robot, map, robots)) //down
                {
                    StepRobotWhatAreUDoing(ref robot, (robot.X + 1, robot.Y));
                }
                else if (RobotCanMoveInThatDir(4, robot, map, robots)) //left
                {
                    StepRobotWhatAreUDoing(ref robot, (robot.X, robot.Y - 1));
                }
                else if (RobotCanMoveInThatDir(1, robot, map, robots)) //up
                {
                    StepRobotWhatAreUDoing(ref robot, (robot.X - 1, robot.Y));
                }
                else if (RobotCanMoveInThatDir(2, robot, map, robots)) //right
                {
                    StepRobotWhatAreUDoing(ref robot, (robot.X, robot.Y + 1));
                }
                robotStepCtr = 1;
            }
        }
        private void StepDownAfkRobotsFromPath(ref Robot[] robots, in List<(int, int)> listxy, in Map map)
        {
            for (int i = 0; i < listxy.Count; i++)
            {
                if (IsRobot(listxy[i], in robots, out int robotInd))
                {
                    if (robots[robotInd].CurrentDestination == null)
                    {
                        //we could handle the deadlock situation here somehow
                        StepAfkRobot(ref robots[robotInd], map, robots);
                    }
                }
            }
        }
        //CODE QUALITY: removed the "in" from "in (int,int) xy"
        private bool IsRobot((int, int) xy, in Robot[] robots, out int robotInd)
        {
            robotInd = -1;
            for (int i = 0; i < robots.Length; i++)
            {
                if (robots[i].X == xy.Item1 && robots[i].Y == xy.Item2)
                {
                    robotInd = i;
                    return true;
                }
            }
            return false;
        }
        //CODE QUALITY: removed the "in" from "in (int,int) xy"
        private bool IsOtherTaskRobot((int, int) xy, in List<Mytask> tasks, in Robot[] robots, in int actualRobotInd)
        {
            for (int i = 0; i < tasks.Count; i++)
            {
                if (xy == (robots[tasks[i].RobotInd].X, robots[tasks[i].RobotInd].Y) && i != actualRobotInd)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region events, event methods
        public event EventHandler<TaskFinishedEventArgs>? TaskFinished;

        private void OnTaskFinished(TaskFinishedEventArgs e)
        {
            TaskFinished!.Invoke(this, e);
        }

        #endregion
    }
}

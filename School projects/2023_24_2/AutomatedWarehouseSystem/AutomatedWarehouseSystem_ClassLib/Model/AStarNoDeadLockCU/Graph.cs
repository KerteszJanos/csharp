using AutomatedWarehouseSystem_ClassLib.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedWarehouseSystem_ClassLib.Model.AStarNoDeadLockCU
{
    /// <summary>
    /// Grapg class
    /// </summary>
    public class Graph
    {
        #region fields / properties
        private Dictionary<(int, int), List<(int, int)>> adjacencyList; //indexed by a Node, contain the list of edges (connections to other nodes)
        #endregion

        #region Constructors
        /// <summary>
        /// //make a graph from the map, and robots
        /// We see cells with robots  as an obstacle, also walls (false vauels in map) are obstacles
        /// We see empty cells just the real empty ones
        /// </summary>
        /// <param name="map"></param>
        public Graph(in Map map, in Robot[] robots)
        {
            adjacencyList = new Dictionary<(int, int), List<(int, int)>>();

            //instatly build the graph
            for (int i = 0; i < map.Height; i++)
            {
                for (int j = 0; j < map.Width; j++)
                {
                    if (EmptyAndValidCell(in map, in robots, i, j))
                    {
                        adjacencyList[(i, j)] = new List<(int, int)>();

                        if (EmptyAndValidCell(in map, in robots, i - 1, j)) //up
                        {
                            adjacencyList[(i, j)].Add((i - 1, j));
                        }
                        if (EmptyAndValidCell(in map, in robots, i + 1, j))//down
                        {
                            adjacencyList[(i, j)].Add((i + 1, j));
                        }
                        if (EmptyAndValidCell(in map, in robots, i, j - 1))//left
                        {
                            adjacencyList[(i, j)].Add((i, j - 1));
                        }
                        if (EmptyAndValidCell(in map, in robots, i, j + 1)) //right
                        {
                            adjacencyList[(i, j)].Add((i, j + 1));
                        }
                    }
                }
            }
        }
        #endregion

        #region public methods
        /// <summary>
        /// Add new node to the adjacencyList by its coordinate
        /// </summary>
        public void AddNode(int i, int j)
        {
            adjacencyList[(i, j)] = new List<(int, int)>();
            if (adjacencyList.ContainsKey((i - 1, j))) //up
            {
                adjacencyList[(i, j)].Add((i - 1, j));
                adjacencyList[(i - 1, j)].Add((i, j));
            }
            if (adjacencyList.ContainsKey((i + 1, j)))//down
            {
                adjacencyList[(i, j)].Add((i + 1, j));
                adjacencyList[(i + 1, j)].Add((i, j));
            }
            if (adjacencyList.ContainsKey((i, j - 1)))//left
            {
                adjacencyList[(i, j)].Add((i, j - 1));
                adjacencyList[(i, j - 1)].Add((i, j));
            }
            if (adjacencyList.ContainsKey((i, j + 1))) //right
            {
                adjacencyList[(i, j)].Add((i, j + 1));
                adjacencyList[(i, j + 1)].Add((i, j));
            }
        }
        /// <summary>
        /// Delete exist node to the adjacencyList by its coordinate
        /// </summary>
        public void DelNode(int i, int j)
        {
            if (adjacencyList.ContainsKey((i - 1, j))) //up
            {
                adjacencyList[(i - 1, j)].Remove((i, j));
            }
            if (adjacencyList.ContainsKey((i + 1, j)))//down
            {
                adjacencyList[(i + 1, j)].Remove((i, j));
            }
            if (adjacencyList.ContainsKey((i, j - 1)))//left
            {
                adjacencyList[(i, j - 1)].Remove((i, j));
            }
            if (adjacencyList.ContainsKey((i, j + 1))) //right
            {
                adjacencyList[(i, j + 1)].Remove((i, j));
            }
            adjacencyList.Remove((i, j));
        }
        #endregion

        #region methods related to A* pathfind method
        /// <summary>
        /// The A* algorith step function implementation
        /// </summary>
        public (int, int) AStarStep((int, int) start, (int, int) goal)
        {
            HashSet<(int, int)> closedSet = new HashSet<(int, int)>(); // Stores the vertices that have been processed.
            Dictionary<(int, int), (int, int)> cameFrom = new Dictionary<(int, int), (int, int)>(); // Maps current vertices to their previous vertices.
            Dictionary<(int, int), int> gScore = new Dictionary<(int, int), int>(); // Stores distances from start vertices.
            Dictionary<(int, int), int> fScore = new Dictionary<(int, int), int>(); // Stores estimates of the best known path lengths.


            // Kezdeti értékek beállítása
            foreach (var vertex in adjacencyList.Keys)
            {
                gScore[vertex] = int.MaxValue;
                fScore[vertex] = int.MaxValue;
            }

            gScore[start] = 0;
            fScore[start] = Heuristic(start, goal);

            HashSet<(int, int)> openSet = new HashSet<(int, int)>();
            openSet.Add(start);

            while (openSet.Count > 0)
            {
                var current = GetLowestFScore(openSet, fScore);
                if (current == goal)
                {
                    return ReconstructPath(cameFrom, current);
                }

                openSet.Remove(current);
                closedSet.Add(current);

                foreach (var neighbor in adjacencyList[current])
                {
                    if (closedSet.Contains(neighbor))
                        continue;

                    var tentativeGScore = gScore[current] + 1; // Assuming each edge has a weight of 1

                    if (!openSet.Contains(neighbor))
                        openSet.Add(neighbor);
                    else if (tentativeGScore >= gScore[neighbor])
                        continue;

                    // This path is the best until now. Record it!
                    cameFrom[neighbor] = current;
                    gScore[neighbor] = tentativeGScore;
                    fScore[neighbor] = gScore[neighbor] + Heuristic(neighbor, goal);
                }
            }

            // No path found
            return (-1, -1);
        }

        private int Heuristic((int, int) a, (int, int) b)
        {
            // Implement your heuristic function here (e.g., Manhattan distance)
            return Math.Abs(a.Item1 - b.Item1) + Math.Abs(a.Item2 - b.Item2);
        }
        /// <summary>
        /// Return the lowest node cost
        /// </summary>
        private (int, int) GetLowestFScore(HashSet<(int, int)> openSet, Dictionary<(int, int), int> fScore)
        {
            var min = int.MaxValue;
            (int, int) minNode = (-1, -1);
            foreach (var node in openSet)
            {
                if (fScore[node] < min)
                {
                    min = fScore[node];
                    minNode = node;
                }
            }
            return minNode;
        }
        /// <summary>
        ///  Reconstruct the pathfinding algorith path
        /// </summary>
        private (int, int) ReconstructPath(Dictionary<(int, int), (int, int)> cameFrom, (int, int) current)
        {
            // Reconstruct the path from start to goal by following the cameFrom links
            var totalPath = new List<(int, int)>();
            totalPath.Add(current);
            while (cameFrom.ContainsKey(current))
            {
                current = cameFrom[current];
                totalPath.Insert(0, current);
            }
            if (totalPath.Count == 1)
            {
                return totalPath[0];
            }
            return totalPath[1]; // Return the next step in the path
        }
        #endregion

        #region private methods
        private bool EmptyAndValidCell(in Map map, in Robot[] robots, int i, int j) //epmty if not a wall or a robot that doesnt have a task
        {
            if (!(i >= 0 && j >= 0 && i < map.Height && j < map.Width)) //if its not a valid cell (out indexed from the mx)
            {
                return false;
            }
            foreach (Robot robot in robots)
            {
                if (robot.X == i && robot.Y == j)
                {
                    return false;
                }
            }
            return map[i, j];
        }
        #endregion
    }
}

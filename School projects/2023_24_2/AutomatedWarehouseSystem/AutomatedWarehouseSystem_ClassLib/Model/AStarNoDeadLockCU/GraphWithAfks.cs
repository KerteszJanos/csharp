using AutomatedWarehouseSystem_ClassLib.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedWarehouseSystem_ClassLib.Model.AStarNoDeadLockCU
{
    /// <summary>
    /// Graphic with afks class
    /// </summary>
    public class GraphWithAfks
    {
        #region fields / properties
        private Dictionary<(int, int), List<(int, int)>> adjacencyList; //indexed by a Node, contain the list of edges (connections to other nodes)
        #endregion

        #region Constructors
        /// <summary>
        /// //make a graph from the map, and robots
        /// We see only wall cells as an obstacle
        /// </summary>
        /// <param name="map"></param>
        public GraphWithAfks(in Map map, in Robot[] robots)
        {
            adjacencyList = new Dictionary<(int, int), List<(int, int)>>();

            //instatly build the graph
            for (int i = 0; i < map.Height; i++)
            {
                for (int j = 0; j < map.Width; j++)
                {
                    if (map[i, j])
                    {
                        adjacencyList[(i, j)] = new List<(int, int)>();

                        if (i > 0 && map[i - 1, j]) //up
                        {
                            adjacencyList[(i, j)].Add((i - 1, j));
                        }
                        if (i < map.Height - 1 && map[i + 1, j])//down
                        {
                            adjacencyList[(i, j)].Add((i + 1, j));
                        }
                        if (j > 0 && map[i, j - 1])//left
                        {
                            adjacencyList[(i, j)].Add((i, j - 1));
                        }
                        if (j < map.Width - 1 && map[i, j + 1]) //right
                        {
                            adjacencyList[(i, j)].Add((i, j + 1));
                        }
                    }
                }
            }
        }
        #endregion

        #region methods related to pathfind method
        /// <summary>
        /// The A* algorith step function implementation
        /// </summary>
        public List<(int, int)> AStarStep((int, int) start, (int, int) goal)
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
            return new List<(int, int)>(); //epmty
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
        private List<(int, int)> ReconstructPath(Dictionary<(int, int), (int, int)> cameFrom, (int, int) current)
        {
            // Reconstruct the path from start to goal by following the cameFrom links
            var totalPath = new List<(int, int)>();
            totalPath.Add(current);
            while (cameFrom.ContainsKey(current))
            {
                current = cameFrom[current];
                totalPath.Insert(0, current);
            }
            return totalPath; // Return the next step in the path
        }
        #endregion
    }
}

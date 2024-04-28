using TeacherComputerRetrieval.Repository.Interfaces;

namespace TeacherComputerRetrieval.Repository
{
    public class Route : IRoute
    {
        private readonly Dictionary<char, Dictionary<char, int>> _routeGraph;

        public Route(string[] routes)
        {
            _routeGraph = new Dictionary<char, Dictionary<char, int>>();
            InitializeRoutes(routes);
        }

        /// <summary>
        /// To initialize the input routes
        /// </summary>
        /// <param name="routes">array of string</param>
        private void InitializeRoutes(string[] routes)
        {
            try
            {
                var routelist = new List<string>();
                foreach (var route in routes)
                {

                    char start = route[0];
                    char end = route[1];

                    string routeValue = route.Substring(0, 2);

                    // handle route appearing more than once.
                    if (routelist.Contains(routeValue))
                    {
                        throw new FormatException("Route should not appear more than once. Invalid route format: " + route);
                    }

                    routelist.Add(routeValue);

                    // handle route starting and ending node same.
                    if (start.Equals(end))
                    {
                        throw new FormatException("Route can not start and end at same node. Invalid route format: " + route);
                    }


                    int distance;
                    if (!int.TryParse(route.Substring(2), out distance))
                    {
                        throw new FormatException("Invalid distance format: " + route);
                    }

                    // hande when distance is zero.
                    if (distance == 0)
                    {
                        throw new FormatException("Distance can not be zero. Invalid distance format for: " + route);
                    }

                    if (!_routeGraph.ContainsKey(start))
                    {
                        _routeGraph[start] = new Dictionary<char, int>();
                    }
                    _routeGraph[start][end] = distance;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Invalid : " + ex.Message);
            }
        }

        /// <summary>
        /// The distance of the route. Eg: A to B to C
        /// </summary>
        /// <param name="route">Eg - ABC</param>
        /// <returns>Distance</returns>
        public int GetDistance(string route)
        {
            try
            {
                int distance = 0;

                // Iterate through the route characters
                for (int i = 0; i < route.Length - 1; i++)
                {
                    char start = route[i];
                    char end = route[i + 1];

                    // Check if both start and end nodes are present in the graph
                    if (_routeGraph.TryGetValue(start, out var startConnections) && startConnections.TryGetValue(end, out var edgeDistance))
                    {
                        distance += edgeDistance; // Add the distance between start and end nodes to the total distance
                    }
                    else
                    {
                        return -1; // No such route
                    }
                }
                return distance;
            }
            catch (Exception ex)
            {
                throw new Exception("Invalid : " + ex.Message);
            }
        }

        /// <summary>
        /// Count trips with max stop
        /// </summary>
        /// <param name="current"></param>
        /// <param name="end"></param>
        /// <param name="maxStops"></param>
        /// <param name="stops"></param>
        /// <returns>max stops</returns>
        public int CountTripsWithMaxStops(char current, char end, int maxStops, int stops = 0)
        {
            try
            {
                if (stops > maxStops)
                {
                    return 0;
                }
                if (current == end && stops > 0)
                {
                    return 1;
                }

                int count = 0;
                if (_routeGraph.ContainsKey(current))
                {
                    foreach (var neighbor in _routeGraph[current])
                    {
                        count += CountTripsWithMaxStops(neighbor.Key, end, maxStops, stops + 1);
                    }
                }
                return count;
            }
            catch (Exception ex)
            {
                throw new Exception("Invalid : " + ex.Message);
            }
        }

        /// <summary>
        /// Count trip with exact stops
        /// </summary>
        /// <param name="current"></param>
        /// <param name="end"></param>
        /// <param name="exactStops"></param>
        /// <param name="stops"></param>
        /// <returns>Exact stops</returns>
        public int CountTripsWithExactStops(char current, char end, int exactStops, int stops = 0)
        {
            try
            {
                if (stops == exactStops && current == end)
                {
                    return 1;
                }
                if (stops >= exactStops)
                {
                    return 0;
                }

                int count = 0;
                if (_routeGraph.ContainsKey(current))
                {
                    foreach (var neighbor in _routeGraph[current])
                    {
                        count += CountTripsWithExactStops(neighbor.Key, end, exactStops, stops + 1);
                    }
                }
                return count;
            }
            catch (Exception ex)
            {
                throw new Exception("Invalid : " + ex.Message);
            }
        }

        /// <summary>
        /// Find shortest route
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns>Shortest route</returns>
        public int FindShortestRoute(char start, char end)
        {
            try
            {

                var count = CountTripsWithMaxStops(start, end, 10);

                // If there is no route available, return 0
                if (count == 0)
                {
                    return 0;
                }

                var distances = new Dictionary<char, int>();
                foreach (var node in _routeGraph.Keys)
                {
                    distances[node] = int.MaxValue;

                    if (_routeGraph[node].Keys.Contains(end))
                    {
                        distances[end] = int.MaxValue;
                    }
                }

                distances[start] = 0;

                var visited = new HashSet<char>();
                while (visited.Count < _routeGraph.Count)
                {
                    char minNode = ' ';
                    int minDistance = int.MaxValue;
                    foreach (var node in _routeGraph.Keys)
                    {
                        if (!visited.Contains(node) && distances[node] < minDistance)
                        {
                            minNode = node;
                            minDistance = distances[node];
                        }
                    }

                    if (minNode == ' ')
                    {
                        break;
                    }

                    visited.Add(minNode);
                    foreach (var neighbor in _routeGraph[minNode])
                    {
                        int newDistance = distances[minNode] + neighbor.Value;

                        if (distances.ContainsKey(neighbor.Key) && (newDistance < distances[neighbor.Key]))
                        {
                            distances[neighbor.Key] = newDistance;
                        }
                    }
                }

                // If start and end nodes are the same
                if (start == end)
                {
                    int shortestDistance = int.MaxValue;

                    foreach (var node in _routeGraph[start])
                    {
                        if (node.Key != end)
                        {
                            int distance = FindShortestRoute(node.Key, end) + _routeGraph[start][node.Key];
                            shortestDistance = Math.Min(shortestDistance, distance);
                        }
                    }

                    if (shortestDistance == int.MaxValue)
                    {
                        return 0;
                    }

                    return shortestDistance;
                }

                if (distances[end] == int.MaxValue)
                {
                    return 0;
                }

                return distances[end];
            }
            catch (Exception ex)
            {
                throw new Exception("Invalid : " + ex.Message);
            }
        }

        /// <summary>
        /// Count routes within distance
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="maxDistance"></param>
        /// <returns>Routes within distance</returns>
        public int CountRoutesWithinDistance(char start, char end, int maxDistance)
        {
            try
            {
                var queue = new Queue<(char, int)>();
                queue.Enqueue((start, 0));

                int count = 0;
                while (queue.Count > 0)
                {
                    var (node, distance) = queue.Dequeue();

                    if (node == end && distance > 0 && distance < maxDistance)
                    {
                        count++;
                    }

                    if (distance >= maxDistance)
                    {
                        continue;
                    }

                    if (_routeGraph.ContainsKey(node))
                    {
                        foreach (var neighbor in _routeGraph[node])
                        {
                            int newDistance = distance + neighbor.Value;
                            queue.Enqueue((neighbor.Key, newDistance));
                        }
                    }
                }

                return count;
            }
            catch (Exception ex)
            {
                throw new Exception("Invalid : " + ex.Message);
            }
        }
    }
}

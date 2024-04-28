using TeacherComputerRetrieval.Repository.Interfaces;

namespace TeacherComputerRetrieval.Repository
{
    class Execute : IExecute
    {
        private readonly IRoute _route;
        public const string NoSuchRoute = "NO SUCH ROUTE";

        public Execute(IRoute route)
        {
            _route = route;
        }

        public void Start()
        {
            int result = -1;
            //1.The distance of the route A - B - C
            result = _route.GetDistance("ABC");
            Console.WriteLine("1. " + (result == -1 ? NoSuchRoute : result));

            // 2. The distance of the route A-E-B-C-D
            result = _route.GetDistance("AEBCD");
            Console.WriteLine("2. " + (result == -1 ? NoSuchRoute : result));

            // 3. The distance of the route A-E-D
            result = _route.GetDistance("AED");
            Console.WriteLine("3. " + (result == -1 ? NoSuchRoute : result));

            // 4. The number of trips starting at C and ending at C with a maximum of 3 stops
            result = _route.CountTripsWithMaxStops('C', 'C', 3);
            Console.WriteLine("4. " + (result));

            // 5. The number of trips starting at A and ending at C with exactly 4 stops
            result = _route.CountTripsWithExactStops('A', 'C', 4);
            Console.WriteLine("5. " + (result));

            // 6. The length of the shortest route (in terms of distance to travel) from A to C
            result = _route.FindShortestRoute('A', 'C');
            Console.WriteLine("6. " + (result == 0 ? NoSuchRoute : result));

            // 7. The length of the shortest route (in terms of distance to travel) from B to B
            result = _route.FindShortestRoute('B', 'B');
            Console.WriteLine("7. " + (result == 0 ? NoSuchRoute : result));

            // 8. The number of different routes from C to C with a distance of less than 30
            result = _route.CountRoutesWithinDistance('C', 'C', 30);
            Console.WriteLine("8. " + (result));
        }
    }
}

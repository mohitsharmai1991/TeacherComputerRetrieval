namespace TeacherComputerRetrieval.Repository.Interfaces
{
    interface IRoute
    {
        int GetDistance(string route);
        int CountTripsWithMaxStops(char current, char end, int maxStops, int stops = 0);
        int CountTripsWithExactStops(char current, char end, int exactStops, int stops = 0);
        int FindShortestRoute(char start, char end);
        int CountRoutesWithinDistance(char start, char end, int maxDistance);
    }
}

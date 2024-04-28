using TeacherComputerRetrieval.Repository;

[TestFixture]
public class RouteTests
{
    private Route _route;

    [SetUp]
    public void SetUp()
    {
        // Initialize Route with sample routes
        string[] routes = { "AB5", "BC4", "CD8", "DC8", "DE6", "AD5", "CE2", "EB3", "AE7" };
        _route = new Route(routes);
    }

    [Test]
    public void InitializeRoutes_FailureScenarioWithZeroDistance_ReturnsErrorMessage()
    {
        try
        {
            // Arrange
            string[] routesWithZero = { "AB5", "BC0" };
            // Act
            _route = new Route(routesWithZero);
        }
        catch (Exception ex)
        {
            // Assert
            Assert.That(ex.Message, Is.EqualTo("Invalid : Distance can not be zero. Invalid distance format for: BC0"));
        }
    }

    [Test]
    public void InitializeRoutes_FailureScenarioWithSameStartEnd_ReturnsErrorMessage()
    {
        try
        {
            // Arrange
            string[] routesWithSameStartEnd = { "AB5", "BB4" };
            // Act
            _route = new Route(routesWithSameStartEnd);
        }
        catch (Exception ex)
        {
            // Assert
            Assert.That(ex.Message, Is.EqualTo("Invalid : Route can not start and end at same node. Invalid route format: BB4"));
        }
    }

    [Test]
    public void InitializeRoutes_FailureScenarioWithDuplicateNodes_ReturnsErrorMessage()
    {
        try
        {
            // Arrange
            string[] routesWithDuplicateNodes = { "AB5", "AB4" };
            // Act
            _route = new Route(routesWithDuplicateNodes);
        }
        catch (Exception ex)
        {
            // Assert
            Assert.That(ex.Message, Is.EqualTo("Invalid : Route should not appear more than once. Invalid route format: AB4"));
        }
    }

    [Test]
    public void GetDistance_PositiveScenario1_ReturnsDistance()
    {
        // Arrange & Act
        int distance = _route.GetDistance("ABC");

        // Assert
        Assert.That(distance, Is.EqualTo(9));
    }

    [Test]
    public void GetDistance_PositiveScenario2_ReturnsDistance()
    {
        // Arrange & Act
        int distance = _route.GetDistance("AEBCD");

        // Assert
        Assert.That(distance, Is.EqualTo(22));
    }

    [Test]
    public void GetDistance_NegativeScenario_ReturnsMinusOne()
    {
        // Arrange & Act
        int distance = _route.GetDistance("AED");

        // Assert
        Assert.That(distance, Is.EqualTo(-1));
    }

    [Test]
    public void CountTripsWithMaxStops_PositiveScenario_ReturnsCount()
    {
        // Arrange & Act
        int count = _route.CountTripsWithMaxStops('C', 'C', 3);

        // Assert
        Assert.That(count, Is.EqualTo(2));
    }

    [Test]
    public void CountTripsWithExactStops_PositiveScenario_ReturnsCount()
    {
        // Arrange & Act
        int count = _route.CountTripsWithExactStops('A', 'C', 4);

        // Assert
        Assert.That(count, Is.EqualTo(3));
    }

    [Test]
    public void FindShortestRoute_PositiveScenario1_ReturnsShortestRoute()
    {
        // Arrange & Act
        int shortestRoute = _route.FindShortestRoute('A', 'C');

        // Assert
        Assert.That(shortestRoute, Is.EqualTo(9));
    }

    [Test]
    public void FindShortestRoute_PositiveScenario2_ReturnsShortestRoute()
    {
        // Arrange & Act
        int shortestRoute = _route.FindShortestRoute('B', 'B');

        // Assert
        Assert.That(shortestRoute, Is.EqualTo(9));
    }

    [Test]
    public void FindShortestRoute_NegativeScenario_ReturnsZero()
    {
        // Arrange & Act
        int shortestRoute = _route.FindShortestRoute('D', 'A');

        // Assert
        Assert.That(shortestRoute, Is.EqualTo(0));
    }

    [Test]
    public void CountRoutesWithinDistance_PositiveScenario_ReturnsCount()
    {
        // Arrange & Act
        int count = _route.CountRoutesWithinDistance('C', 'C', 30);

        // Assert
        Assert.That(count, Is.EqualTo(7));
    }
}

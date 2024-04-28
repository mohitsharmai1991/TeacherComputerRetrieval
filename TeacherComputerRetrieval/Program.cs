using System.Text.RegularExpressions;
using TeacherComputerRetrieval.Repository;
using TeacherComputerRetrieval.Repository.Interfaces;

namespace TeacherComputerRetrieval
{
    class Program
    {
        static void Main(string[] args)
        {

            // Keep running until "exit" is typed
            while (true)
            {
                try
                {
                    Console.WriteLine("\nType 'exit' to quit:\n");

                    Console.WriteLine("Please provide input routes (case sensitive) :\nExample format: AB5,BC4,CD8,DC8,DE6,AD5,CE2,EB3,AE7\n");

                    var inputData = Console.ReadLine()?.Trim();
                    if (inputData?.ToLower() == "exit")
                    {
                        break;
                    }

                    string pattern = @"^[A-Z]{2}\d+((,[A-Z]{2}\d+)*)?$";

                    if (Regex.IsMatch(inputData, pattern))
                    {
                        string[] inputRoutes = inputData.Split(',');

                        IRoute _route = new Route(inputRoutes);
                        IExecute execute = new Execute(_route);
                        execute.Start();
                    }
                    else
                    {
                        Console.WriteLine("INVALID FORMAT");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            Console.WriteLine("Exiting...");
        }
    }
}

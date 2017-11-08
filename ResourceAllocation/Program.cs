using System;
using AllocationService;

namespace ResourceAllocation
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = "Dummy.txt";

            if (args != null)
                path = args[0];

            Console.WriteLine("program started");

            var f = new FileReader(path);

            try
            {
                var epi = f.ReadFile();

                var baf = new BestAllocationFinder(epi);

                var resGraph = baf.CalculateMaxFlow();

                var res = resGraph.GraphToAllocationResult(epi.ProjectCount, epi.SkillCount, epi.ExpertCount);

                res.GetResourceWasted(baf.ExpertProjectInformation.ExpertCount);

                Console.WriteLine("Dopasowanie:");
                Console.WriteLine(res);
            }
            catch (System.IO.FileNotFoundException)
            {
                Console.WriteLine("Plik nie jest znaleziony, upewnij sie ze jest w folderze TestCases/");
            }
            catch
            {
                Console.WriteLine("Nienznany blad");
            }

            Console.WriteLine("Nacisnij klawisz");
            Console.ReadKey();
        }
    }
}

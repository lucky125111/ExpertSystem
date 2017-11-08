using System;
using System.IO;
using AllocationService;

namespace ResourceAllocation
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = "Dummy.txt";
            var silet = false;

            if (args != null)
            {
                if (args.Length > 1 && args[1] == "-s")
                {
                    Console.SetOut(new StreamWriter(Stream.Null));
                    silet = true;
                }
                if (args.Length > 0)
                    path = args[0];
                
            }

            Console.WriteLine("program started");

            try
            {
                var f = new FileReader(path);

                var epi = f.ReadFile();

                var baf = new BestAllocationFinder(epi);

                var resGraph = baf.CalculateMaxFlow();

                var res = resGraph.GraphToAllocationResult(epi.ProjectCount, epi.SkillCount, epi.ExpertCount);

                var waste = res.GetResourceWasted(baf.ExpertProjectInformation.ExpertCount);

                f.SaveResult(res, waste);

                Console.WriteLine("zmarnowane: " + waste);
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

            Console.Write("Nacisnij klawisz: ");

            if(!silet)
                Console.ReadKey();
        }
    }
}

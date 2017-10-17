using AllocationService;

namespace ResourceAllocation
{
    class Program
    {
        static void Main()
        {
            var x = FileReader.ReadFile("dupa");
            var g = GraphConverter.ExpertProjectInformationToGraph(x);
            
        }
    }
}

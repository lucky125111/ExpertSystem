using AllocationService;

namespace ResourceAllocation
{
    class Program
    {
        static void Main(string[] args)
        {
            var x = FileReader.ReadFile("dupa");
            var g = GraphConverter.ExpertProjectInformationToGraph(x);
            
        }
    }
}

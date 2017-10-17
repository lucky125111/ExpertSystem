using ASD.Graphs;

namespace AllocationService
{
    public class BestAllocationFinder
    {
        public ExpertProjectInformation ExpertProjectInformation { get; }
        public Graph FlowGraph { get; set; }
        
        public BestAllocationFinder(ExpertProjectInformation expertProjectInformation)
        {
            ExpertProjectInformation = expertProjectInformation;
            FlowGraph = GraphConverter.ExpertProjectInformationToGraph(expertProjectInformation);
        }

        public AllocationResult CalculateBestAllocation()
        {
            var resGraph = FlowGraph.IsolatedVerticesGraph();

            FlowGraph.FordFulkersonDinicMaxFlow(0, FlowGraph.VerticesCount - 1, out resGraph, MaxFlowGraphExtender.BFPath);

            //convert graph to AllocationResult
            //to jest tak naprawde rozparsowanie krawedzi

            var res = resGraph.GraphToAllocationResult(ExpertProjectInformation.ProjectCount, ExpertProjectInformation.SkillCount, ExpertProjectInformation.ExpertCount);

            return res;
        }
    }
}
using System.Collections.Generic;
using AllocationService;
using ASD.Graphs;
using Xunit;

namespace Tests
{
    public class CalculateMaxFlowTest
    {
        [Fact]
        public void CalculateMaxFlow_BasicTest()
        {
            //arrange
            var g = new AdjacencyMatrixGraph(true, 11);

            g.AddEdge(0, 1, 2);
            g.AddEdge(0, 2);

            g.AddEdge(2, 6);
            g.AddEdge(1, 4);
            g.AddEdge(1, 3);

            g.AddEdge(6, 9);
            g.AddEdge(4, 8);
            g.AddEdge(3, 7);
            g.AddEdge(2, 5);
            g.AddEdge(5, 8);


            g.AddEdge(8, 10);
            g.AddEdge(9, 10);
            g.AddEdge(8, 10);
            g.AddEdge(7, 10);

            //act
            var ge = new GraphExport();

            ge.Export(g);

            //assert


        }
    }
}

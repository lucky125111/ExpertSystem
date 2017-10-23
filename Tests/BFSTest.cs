using AllocationService;
using ASD.Graphs;
using FluentAssertions;
using Xunit;

namespace Tests
{
    public class BFSTest
    {
        [Fact]
        public void BFSSearchTest_Basic()
        {
            //arange
            var g = new AdjacencyMatrixGraph(true, 4);

            g.AddEdge(0,3,0);
            g.AddEdge(0,1,0);
            g.AddEdge(1,2,0);
            g.AddEdge(2,3,0);

            var test = new BestAllocationFinder(g);
            //act
            var prev = test.BFS(0, 3);

            //assert

            prev.ShouldAllBeEquivalentTo(new int[] {-1, 0, 1, 0});
        }

        [Fact]
        public void BFSSearchTest_CycleInGraph()
        {
            //arange
            var g = new AdjacencyMatrixGraph(true, 4);

            g.AddEdge(3, 0);
            g.AddEdge(0, 1);
            g.AddEdge(1, 2);
            g.AddEdge(2, 3);

            var test = new BestAllocationFinder(g);

            //act
            var prev = test.BFS(0, 3);

            //assert
            prev.ShouldAllBeEquivalentTo(new int[] { -1, 0, 1, 2});
        }

        [Fact]
        public void BFSSearchTest_CycleInGraph_TwoGraphs()
        {
            //arange
            var g = new AdjacencyMatrixGraph(true, 4);

            g.AddEdge(3, 0);
            g.AddEdge(0, 1);
            g.AddEdge(1, 2);
            g.AddEdge(2, 3);

            var g1 = new AdjacencyMatrixGraph(true, 4);

            g1.AddEdge(3, 0, 0);
            g1.AddEdge(0, 1, 0);
            g1.AddEdge(1, 2, 0);
            g1.AddEdge(2, 3, 0);


            var test = new BestAllocationFinder(g);

            //act
            var prev = test.BFS(0, 3, g1);

            //assert
            prev.ShouldAllBeEquivalentTo(new int[] { -1, 0, 1, 2 });
        }

        [Fact]
        public void BFSSearchTest_GraphInExpertSystem_Simple()
        {
            //arrange
            var g = new AdjacencyMatrixGraph(true, 11);

            g.AddEdge(0, 1);
            g.AddEdge(0, 2);
            g.AddEdge(0, 3);

            g.AddEdge(1, 6);
            g.AddEdge(2, 5);
            g.AddEdge(3, 4);

            g.AddEdge(4, 8);
            g.AddEdge(4, 9);
            g.AddEdge(5, 8);
            g.AddEdge(5, 9);
            g.AddEdge(6, 8);
            g.AddEdge(6, 9);
            g.AddEdge(7, 8);
            g.AddEdge(7, 9);

            g.AddEdge(8, 10);
            g.AddEdge(9, 10);

            var test = new BestAllocationFinder(g);

            ////act
            var prev = test.BFS(0, 3);

            //assert
            prev.ShouldAllBeEquivalentTo(new int[] { -1, 0, 0, 0, 3, 2, 1, -2, 6, 6, 8 });
        }
    }
}

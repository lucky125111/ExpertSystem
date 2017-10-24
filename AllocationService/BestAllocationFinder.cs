using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
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
        public BestAllocationFinder(Graph g)
        {
            FlowGraph = g;
        }

        public int[] BFS(int start, int finish, Graph g)
        {
            //uwaga techniczna
            //-1 w prev oznacza ze to pierwszy wierzcholek
            //-2 w prev oznacza ze wierzcholek nie nalezy do sciezki przeszukiwania
            //jezeli algorytm zwroci prev[finish]==-2 to znaczy ze sciezka nie zostala znaleziona

            var q = new Queue<int>();

            var prev = new int[g.VerticesCount];

            q.Enqueue(start);

            for (int i = 0; i < prev.Length; i++)
            {
                prev[i] = -2;
            }

            prev[start] = -1;

            while (q.Count > 0)
            {
                var s = q.Dequeue();
                foreach (var e in g.OutEdges(s))
                {
                    if (prev[e.To] == -2 && e.Weight > 0)
                    {
                        prev[e.To] = e.From;
                        q.Enqueue(e.To);
                    }
                }
            }

            return prev;
        }

        public Graph CalculateMaxFlow()
        {
            //res graph trzyma aktualny przeplyw przez krawedz
            Graph residualGraph = FlowGraph.IsolatedVerticesGraph();

            for (int i = 0; i < FlowGraph.VerticesCount; i++)
            {
                foreach (var e in FlowGraph.OutEdges(i))
                {
                    residualGraph.AddEdge(e.From, e.To, e.Weight);
                    residualGraph.AddEdge(e.To, e.From, 0);
                }
            }

            //start v 0
            //finish v FlowGraph.VerticesCount - 1
            var path = BFS(0, residualGraph.VerticesCount - 1, residualGraph);

            //ge.Export(residualGraph);

            while (path[residualGraph.VerticesCount - 1] != -2)     //to znaczy ze nie ma sciezki
            {
                var df = 1; //zawsze szukamy tylko przypisania jednego eksperta do projektu
                
                var tmp = residualGraph.VerticesCount - 1;

                //update przeplywow
                while (path[tmp] != -1)
                {
                    var w = residualGraph.GetEdgeWeight(path[tmp], tmp);
                    residualGraph.DelEdge(path[tmp], tmp);
                    residualGraph.AddEdge(path[tmp], tmp, w - df);
                    w = residualGraph.GetEdgeWeight(tmp, path[tmp]);
                    residualGraph.DelEdge(tmp, path[tmp]);
                    residualGraph.AddEdge(tmp, path[tmp], w + df);
                    tmp = path[tmp];
                }

                path = BFS(0, residualGraph.VerticesCount - 1, residualGraph);
            }

            //narazie na cele testow samego pomyslu kozystamy z gotowego rozwiazania
            //FlowGraph.FordFulkersonDinicMaxFlow(0, FlowGraph.VerticesCount - 1, out resGraph, MaxFlowGraphExtender.MaxFlowPath);

            var result = residualGraphToMaxFlow(residualGraph);

            return result;
        }

        private Graph residualGraphToMaxFlow(Graph residualGraph)
        {
            var g = residualGraph.IsolatedVerticesGraph();

            for (int i = 0; i < residualGraph.VerticesCount; i++)
            {
                foreach (var e in residualGraph.OutEdges(i))
                {
                    if (e.From < e.To)
                    {
                        var t = FlowGraph.GetEdgeWeight(e.From, e.To) - e.Weight;
                        if(t > 0)
                            g.AddEdge(e.From, e.To, t);
                    }
                }
            }

            return g;
        }

        public AllocationResult CalculateBestAllocation()
        {
            var resGraph = CalculateMaxFlow();

            var res = resGraph.GraphToAllocationResult(ExpertProjectInformation.ProjectCount, ExpertProjectInformation.SkillCount, ExpertProjectInformation.ExpertCount);

            return res;
        }
    }
}
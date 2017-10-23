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

        public int[] BFS(int start, int finish)
        {
            //uwaga techniczna
            //-1 w prev oznacza ze to pierwszy wierzcholek
            //-2 w prev oznacza ze wierzcholek nie nalezy do sciezki przeszukiwania
            //jezeli algorytm zwroci prev[finish]==-2 to znaczy ze sciezka nie zostala znaleziona

            var q = new Queue<int>();

            var prev = new int[FlowGraph.VerticesCount];

            q.Enqueue(start);

            for (int i = 0; i < prev.Length; i++)
            {
                prev[i] = -2;
            }

            prev[start] = -1;

            while (q.Count > 0)
            {
                var s = q.Dequeue();
                foreach (var e in FlowGraph.OutEdges(s))
                {
                    if (prev[e.To] == -2)
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
            Graph resGraph = FlowGraph.IsolatedVerticesGraph();

            for (int i = 0; i < resGraph.VerticesCount; i++)
            {
                foreach (var e in resGraph.OutEdges(i))
                {
                    resGraph.AddEdge(e.From, e.To, 0);
                }
            }

            //start v 0
            //finish v FlowGraph.VerticesCount - 1
            var path = BFS(0, FlowGraph.VerticesCount - 1);

            while (path[FlowGraph.VerticesCount - 1] != -2)     //to znaczy ze nie ma sciezki
            {
                //we found augmenting path

                var df = double.MaxValue;

                var tmp = FlowGraph.VerticesCount - 1;

                //szukamy w sciezce ile tego przeplywu mozemy dodac
                while (tmp != -1)   
                {
                    df = Math.Min(df, FlowGraph.GetEdgeWeight(path[tmp], tmp) - resGraph.GetEdgeWeight(path[tmp], tmp));
                    tmp = path[tmp];
                }

                tmp = FlowGraph.VerticesCount - 1;

                //update przeplywow
                while (tmp != -1)
                {
                    var w = resGraph.GetEdgeWeight(path[tmp], tmp);
                    resGraph.DelEdge(path[tmp], tmp);
                    resGraph.AddEdge(path[tmp], tmp, df + w);
                    tmp = path[tmp];
                }

                path = BFS(0, FlowGraph.VerticesCount - 1);
            }

            //narazie na cele testow samego pomyslu kozystamy z gotowego rozwiazania
            //FlowGraph.FordFulkersonDinicMaxFlow(0, FlowGraph.VerticesCount - 1, out resGraph, MaxFlowGraphExtender.MaxFlowPath);

            return resGraph;
        }

        public AllocationResult CalculateBestAllocation()
        {
            var resGraph = CalculateMaxFlow();

            var res = resGraph.GraphToAllocationResult(ExpertProjectInformation.ProjectCount, ExpertProjectInformation.SkillCount, ExpertProjectInformation.ExpertCount);

            return res;
        }
    }
}
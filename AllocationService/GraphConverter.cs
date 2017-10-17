using System.Linq;
using ASD.Graphs;

namespace AllocationService
{
    public static class GraphConverter
    {
        public static Graph ExpertProjectInformationToGraph(ExpertProjectInformation expertProjectInformation)
        {
            var g = new AdjacencyMatrixGraph(true, expertProjectInformation.GetVerticesCount());

            //indeksowanie wierzcholkow
            //0 wejscie
            //expertProjectInformation.GetVerticesCount() - 1 ujscie
            //od 1 do ProjectCount wierzcholki projektow
            //od ProjectCount + 1 do ProjectCount + SkillCount wierzcholki skillsow
            //od ProjectCount + SkillCount + 1 do ProjectCount + SkillCount + ExpertCount wierzcholki eksperow

            for (int i = 0; i < expertProjectInformation.ProjectCount; i++)
            {
                g.AddEdge(0, i + 1, expertProjectInformation.ProjectRequirements[i].Sum());      //waga to suma wszystkich skillow potrzebnych w projekcie
            }

            for (int i = 0; i < expertProjectInformation.ProjectCount; i++)
            {
                for (int j = 0; j < expertProjectInformation.SkillCount; j++)
                {
                    if (expertProjectInformation.ProjectRequirements[i][j] != 0)
                        g.AddEdge(i + 1, expertProjectInformation.ProjectCount + 1 + j, expertProjectInformation.ProjectRequirements[i][j]);
                }
            }

            for (int i = 0; i < expertProjectInformation.ExpertCount; i++)
            {
                for (int j = 0; j < expertProjectInformation.SkillCount; j++)
                {
                    if (expertProjectInformation.ProjectRequirements[i][j] != 0)
                        g.AddEdge(expertProjectInformation.ProjectCount + 1 + j,
                            expertProjectInformation.ProjectCount + expertProjectInformation.SkillCount + 1 + i, expertProjectInformation.ExpertSkills[i][j]);
                }
            }

            for (int i = 0; i < expertProjectInformation.ExpertCount; i++)
            {
                g.AddEdge(expertProjectInformation.ProjectCount + expertProjectInformation.SkillCount + 1 + i, expertProjectInformation.GetVerticesCount() - 1);
            }

            return g;
        }

        public static AllocationResult GraphToAllocationResult(this Graph g, int projectCount, int skillCount, int expertCount)
        {
            var res = new AllocationResult();

            for (int i = 1; i <= projectCount; i++)     //lecimy po wszystkich projektach (wierzcholkach ktore sa projektami)
            {
                foreach (var e in g.OutEdges(i))
                {
                    //dla kazdej krawedzi, patrzymy jescze ile z niej odchodzi
                    foreach (var e1 in g.OutEdges(e.To))
                    {
                        res.ExpertToProjects.Add(new ExpertToProject()
                        {
                            ProjectId = i - 1,
                            SkillId = e1.From - projectCount - 1,
                            ExpertId = e1.To - projectCount - skillCount - 1
                        });
                    }
                }
            }

            return res;
        }
    }
}
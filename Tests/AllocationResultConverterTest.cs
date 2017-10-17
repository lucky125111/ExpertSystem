using System.Linq;
using AllocationService;
using ASD.Graphs;
using FluentAssertions;
using Xunit;

namespace Tests
{
    public class AllocationResultConverterTest
    {

        //jezeli chcemy zobaczyc gra wystarczy dodac linijke
        //var ge = new GraphExport();
        //ge.Export(g);

        [Fact]
        public void ConvertToAllocationRestul_BasicTest()
        {
            //arrange
            var g = new AdjacencyMatrixGraph(true, 5);

            for (int i = 1; i < g.VerticesCount; i++)
            {
                g.AddEdge(i - 1, i);
            }

            //act
            var sut = g.GraphToAllocationResult(1, 1, 1);

            //assert
            sut.ExpertToProjects.Should().HaveCount(1);
            sut.ExpertToProjects.FirstOrDefault(x => x.ExpertId == 0 && x.ProjectId == 0 && x.SkillId == 0).Should().NotBeNull();
        }

        [Fact]
        public void ConvertToAllocationRestul_StandardCaseOneOfEachSkill()
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

            g.AddEdge(9, 10);
            g.AddEdge(8, 10);
            g.AddEdge(7, 10);


            //act
            var sut = g.GraphToAllocationResult(2, 4, 3);

            //assert
            sut.ExpertToProjects.Should().HaveCount(3);
            sut.ExpertToProjects.FirstOrDefault(x => x.ExpertId == 0 && x.ProjectId == 0 && x.SkillId == 0).Should().NotBeNull();
            sut.ExpertToProjects.FirstOrDefault(x => x.ExpertId == 1 && x.ProjectId == 0 && x.SkillId == 1).Should().NotBeNull();
            sut.ExpertToProjects.FirstOrDefault(x => x.ExpertId == 2 && x.ProjectId == 1 && x.SkillId == 3).Should().NotBeNull();
        }

        [Fact]
        public void ConvertToAllocationRestul_OneProjectMultipleExpertyWithOneSkill()
        {

            //arrange
            var g = new AdjacencyMatrixGraph(true, 9);

            g.AddEdge(0, 1, 4);

            g.AddEdge(1, 2, 2);
            g.AddEdge(1, 3, 2);

            g.AddEdge(2, 4);
            g.AddEdge(2, 5);
            g.AddEdge(3, 6);
            g.AddEdge(3, 7);

            g.AddEdge(7, 8);
            g.AddEdge(6, 8);
            g.AddEdge(5, 8);
            g.AddEdge(4, 8);


            //act
            var sut = g.GraphToAllocationResult(1, 2, 4);


            //assert
            sut.ExpertToProjects.Should().HaveCount(4);
            sut.ExpertToProjects.FirstOrDefault(x => x.ExpertId == 0 && x.ProjectId == 0 && x.SkillId == 0).Should().NotBeNull();
            sut.ExpertToProjects.FirstOrDefault(x => x.ExpertId == 1 && x.ProjectId == 0 && x.SkillId == 0).Should().NotBeNull();
            sut.ExpertToProjects.FirstOrDefault(x => x.ExpertId == 3 && x.ProjectId == 0 && x.SkillId == 1).Should().NotBeNull();
            sut.ExpertToProjects.FirstOrDefault(x => x.ExpertId == 4 && x.ProjectId == 0 && x.SkillId == 1).Should().NotBeNull();
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllocationService;
using ASD.Graphs;
using FluentAssertions;
using Xunit;

namespace Tests
{
    public class AllocationResultConverterTest
    {
        [Fact]
        public void ConvertToAllocationRestul_BasicTest()
        {

            //arrange
            var g = new AdjacencyMatrixGraph(true, 5);

            for (int i = 1; i < g.VerticesCount; i++)
            {
                g.AddEdge(i - 1, i, 1);
            }


            //act
            var sut = g.ConvertToAllocationResult(1, 1, 1);

            //assert
            sut.ExpertToProjects.Should().HaveCount(1);

            sut.ExpertToProjects[0].ExpertId.Should().Be(0);
            sut.ExpertToProjects[0].ProjectId.Should().Be(0);
            sut.ExpertToProjects[0].SkillId.Should().Be(0);
        }

        [Fact]
        public void ConvertToAllocationRestul_StandardCase()
        {

            //arrange
            var g = new AdjacencyMatrixGraph(true, 11);

            g.AddEdge(0, 1, 2);
            g.AddEdge(0, 2, 1);

            g.AddEdge(2, 6, 1);
            g.AddEdge(1, 4, 1);
            g.AddEdge(1, 3, 1);

            g.AddEdge(6, 9, 1);
            g.AddEdge(4, 8, 1);
            g.AddEdge(3, 7, 1);

            g.AddEdge(9, 10, 1);
            g.AddEdge(8, 10, 1);
            g.AddEdge(7, 10, 1);


            //act
            var sut = g.ConvertToAllocationResult(2, 4, 3);


            //assert
            sut.ExpertToProjects.Should().HaveCount(3);

            //TODO
            //sut.ExpertToProjects[0].ExpertId.Should().Be(0);
            //sut.ExpertToProjects[0].ProjectId.Should().Be(0);
            //sut.ExpertToProjects[0].SkillId.Should().Be(0);
        }
    }
}

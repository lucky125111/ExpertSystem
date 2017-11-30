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
    public class EndToEndTests
    {
        //to jest juz bardziej smoke test
        [Fact]
        public void FullTest_Simple()
        {
            var f = new FileReader("Simple.txt");

            var epi = f.ReadFile();

            var baf = new BestAllocationFinder(epi);

            var resGraph = baf.CalculateMaxFlow();

            var res = resGraph.GraphToAllocationResult(epi.ProjectCount, epi.SkillCount, epi.ExpertCount);

            res.ExpertToProjects.Should().HaveCount(2);
            res.ExpertToProjects.FirstOrDefault(x => x.ExpertId == 0 && x.ProjectId == 0 && x.SkillId == 1).Should().NotBeNull();
            res.ExpertToProjects.FirstOrDefault(x => x.ExpertId == 1 && x.ProjectId == 1 && x.SkillId == 0).Should().NotBeNull();
        }

        [Fact]
        public void FullTest_SimpleCase1()
        {
            var f = new FileReader("SimpleCase1.txt");

            var epi = f.ReadFile();

            var baf = new BestAllocationFinder(epi);

            var resGraph = baf.CalculateMaxFlow();

            var e = new GraphExport();

            var res = resGraph.GraphToAllocationResult(epi.ProjectCount, epi.SkillCount, epi.ExpertCount);

            res.ExpertToProjects.Should().HaveCount(3);
            res.ExpertToProjects.FirstOrDefault(x => x.ExpertId == 0 && x.ProjectId == 0 && x.SkillId == 0).Should().NotBeNull();
            res.ExpertToProjects.FirstOrDefault(x => x.ExpertId == 1 && x.ProjectId == 0 && x.SkillId == 0).Should().NotBeNull();
            res.ExpertToProjects.FirstOrDefault(x => x.ExpertId == 2 && x.ProjectId == 1 && x.SkillId == 1).Should().NotBeNull();
            res.ExpertToProjects.FirstOrDefault(x => x.ExpertId == 3 && x.ProjectId == 1 && x.SkillId == 1).Should().NotBeNull();
            res.ExpertToProjects.FirstOrDefault(x => x.ExpertId == 4 && x.ProjectId == 1 && x.SkillId == 1).Should().NotBeNull();
            res.ExpertToProjects.FirstOrDefault(x => x.ExpertId == 5 && x.ProjectId == 1 && x.SkillId == 1).Should().NotBeNull();
            res.ExpertToProjects.FirstOrDefault(x => x.ExpertId == 6 && x.ProjectId == 1 && x.SkillId == 1).Should().NotBeNull();
        }

        [Fact]
        public void FullTest_DocsExample()
        {
            var f = new FileReader("DocsExample.txt");

            var epi = f.ReadFile();

            var baf = new BestAllocationFinder(epi);

            var resGraph = baf.CalculateMaxFlow();

            //TODO
            //ten sposob wywolan jest na pewno do refactoru
            var res = resGraph.GraphToAllocationResult(epi.ProjectCount, epi.SkillCount, epi.ExpertCount);

            res.ExpertToProjects.Should().HaveCount(3);
            res.ExpertToProjects.FirstOrDefault(x => x.ExpertId == 0 && x.ProjectId == 0 && x.SkillId == 0).Should().NotBeNull();
            res.ExpertToProjects.FirstOrDefault(x => x.ExpertId == 1 && x.ProjectId == 0 && x.SkillId == 0).Should().NotBeNull();
            res.ExpertToProjects.FirstOrDefault(x => x.ExpertId == 2 && x.ProjectId == 1 && x.SkillId == 1).Should().NotBeNull();
        }

        [Fact]
        public void FullTest_duzy1()
        {
            var f = new FileReader("duzy1.txt");

            var epi = f.ReadFile();

            var baf = new BestAllocationFinder(epi);

            var resGraph = baf.CalculateMaxFlow();

            //TODO
            //ten sposob wywolan jest na pewno do refactoru
            var res = resGraph.GraphToAllocationResult(epi.ProjectCount, epi.SkillCount, epi.ExpertCount);

            res.ExpertToProjects.Should().HaveCount(1);
            res.ExpertToProjects.FirstOrDefault(x => x.ExpertId == 0 && x.ProjectId == 9 && x.SkillId == 10).Should().NotBeNull();
        }

        [Fact]
        public void FullTest_duzy2()
        {
            var f = new FileReader("duzy2.txt");

            var epi = f.ReadFile();

            var baf = new BestAllocationFinder(epi);

            var resGraph = baf.CalculateMaxFlow();

            //TODO
            //ten sposob wywolan jest na pewno do refactoru
            var res = resGraph.GraphToAllocationResult(epi.ProjectCount, epi.SkillCount, epi.ExpertCount);

            res.ExpertToProjects.Should().HaveCount(1);
            res.ExpertToProjects.FirstOrDefault(x => x.ExpertId == 0 && x.ProjectId == 9 && x.SkillId == 9).Should().NotBeNull();
        }

        [Fact]
        public void FullTest_duzy3()
        {
            var f = new FileReader("duzy3.txt");

            var epi = f.ReadFile();

            var baf = new BestAllocationFinder(epi);

            var resGraph = baf.CalculateMaxFlow();

            //TODO
            //ten sposob wywolan jest na pewno do refactoru
            var res = resGraph.GraphToAllocationResult(epi.ProjectCount, epi.SkillCount, epi.ExpertCount);

            res.ExpertToProjects.Should().HaveCount(1);
            res.ExpertToProjects.FirstOrDefault(x => x.ExpertId == 0 && x.ProjectId == 9 && x.SkillId == 9).Should().NotBeNull();
        }

        [Fact]
        public void FullTest_duzy4()
        {
            var f = new FileReader("duzy4.txt");

            var epi = f.ReadFile();

            var baf = new BestAllocationFinder(epi);

            var resGraph = baf.CalculateMaxFlow();

            //TODO
            //ten sposob wywolan jest na pewno do refactoru
            var res = resGraph.GraphToAllocationResult(epi.ProjectCount, epi.SkillCount, epi.ExpertCount);

            res.ExpertToProjects.Should().HaveCount(1);
            res.ExpertToProjects.FirstOrDefault(x => x.ExpertId == 0 && x.ProjectId == 7 && x.SkillId == 7).Should().NotBeNull();
        }
    }
}

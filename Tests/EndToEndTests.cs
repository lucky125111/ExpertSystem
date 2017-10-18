using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllocationService;
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

            //TODO
            //ten sposob wywolan jest na pewno do refactoru
            var res = resGraph.GraphToAllocationResult(epi.ProjectCount, epi.SkillCount, epi.ExpertCount);

            res.ExpertToProjects.Should().HaveCount(2);
            res.ExpertToProjects.FirstOrDefault(x => x.ExpertId == 0 && x.ProjectId == 0 && x.SkillId == 1).Should().NotBeNull();
            res.ExpertToProjects.FirstOrDefault(x => x.ExpertId == 1 && x.ProjectId == 1 && x.SkillId == 0).Should().NotBeNull();
        }
    }
}

using System.Collections.Generic;
using AllocationService;
using ASD.Graphs;
using Xunit;

namespace Tests
{
    public class GraphCreatorTest
    {
        [Fact]
        public void TestGraphCreator_OneProjectOneExpert()
        {
            //arrange
            var x = new ExpertProjectInformation()
            {
                ExpertCount = 1,
                ProjectCount = 1,
                SkillCount = 1,
                ProjectRequirements = new List<List<int>>()
                {
                    new List<int>()
                    {
                        1
                    }
                },
                ExpertSkills = new List<List<int>>()
                {
                    new List<int>()
                    {
                        1
                    }
                }
            };

            //act
            var g = GraphConverter.ExpertProjectInformationToGraph(x);

            //assert

            GraphExport ge = new GraphExport();
            ge.Export(g);
        }
        [Fact]
        public void TestGraphCreator_StandardCase()
        {
            //arrange
            var x = new ExpertProjectInformation()
            {
                ExpertCount = 2,
                ProjectCount = 3,
                SkillCount = 4,
                ProjectRequirements = new List<List<int>>()
                {
                    new List<int>()
                    {
                        1,
                        2,
                        3,
                        4
                    },
                    new List<int>()
                    {
                        1,
                        2,
                        3,
                        4
                    },
                    new List<int>()
                    {
                        1,
                        2,
                        3,
                        4
                    }
                },
                ExpertSkills = new List<List<int>>()
                {
                    new List<int>()
                    {
                        1,
                        1,
                        1,
                        1
                    },
                    new List<int>()
                    {
                        1,
                        1,
                        1,
                        1
                    },
                    new List<int>()
                    {
                        1,
                        1,
                        1,
                        1
                    },
                    new List<int>()
                    {
                        1,
                        1,
                        1,
                        1
                    }
                }
            };

            //act
            var g = GraphConverter.ExpertProjectInformationToGraph(x);

            //assert

            GraphExport ge = new GraphExport();
            ge.Export(g);
        }

        [Fact]
        public void TestGraphCreator_OneSkillPerProjectOneSkillPerExpert()
        {
            //arrange
            var x = new ExpertProjectInformation()
            {
                ExpertCount = 2,
                ProjectCount = 3,
                SkillCount = 4,
                ProjectRequirements = new List<List<int>>()
                {
                    new List<int>()
                    {
                        1,
                        0,
                        0,
                        0
                    },
                    new List<int>()
                    {
                        0,
                        1,
                        0,
                        0
                    },
                    new List<int>()
                    {
                        0,
                        0,
                        1,
                        0
                    }
                },
                ExpertSkills = new List<List<int>>()
                {
                    new List<int>()
                    {
                        1,
                        0,
                        0,
                        0
                    },
                    new List<int>()
                    {
                        0,
                        1,
                        0,
                        0
                    },
                    new List<int>()
                    {
                        0,
                        0,
                        1,
                        0
                    },
                    new List<int>()
                    {
                        0,
                        0,
                        0,
                        1
                    }
                }
            };

            //act
            var g = GraphConverter.ExpertProjectInformationToGraph(x);

            //assert

            GraphExport ge = new GraphExport();
            ge.Export(g);
        }
    }
}

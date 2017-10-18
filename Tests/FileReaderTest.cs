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
    public class FileReaderTest
    {
        [Fact]
        public void FileReader_BasicTest()
        {
            //arrange
            var fileName = "Dummy.txt";
            var f = new FileReader(fileName);

            //act
            var sut = f.ReadFile();

            //assert
            sut.ProjectCount.Should().Be(1);
            sut.ExpertCount.Should().Be(1);
            sut.SkillCount.Should().Be(1);

            sut.ProjectRequirements.ShouldAllBeEquivalentTo(new List<List<int>>()
            {
                new List<int>()
                {
                    1
                }
            });

            sut.ExpertSkills.ShouldAllBeEquivalentTo(new List<List<int>>()
            {
                new List<int>()
                {
                    1
                }
            });
        }

        [Fact]
        public void FileReader_StandardTest()
        {
            //arrange
            var fileName = "Simple.txt";
            var f = new FileReader(fileName);

            //act
            var sut = f.ReadFile();

            //assert
            sut.ProjectCount.Should().Be(3);
            sut.ExpertCount.Should().Be(2);
            sut.SkillCount.Should().Be(4);

            sut.ProjectRequirements.ShouldAllBeEquivalentTo(new List<List<int>>()
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
                }
            );

            sut.ExpertSkills.ShouldAllBeEquivalentTo(new List<List<int>>()
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
                }
            }
            );
        }



    }
}

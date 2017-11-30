using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using AllocationService;
using FluentAssertions;
using Xunit; 
using Xunit.Abstractions;

namespace Tests
{
    [Collection("Sequential")]
    public class PerformanceTests
    {
        private readonly ITestOutputHelper _output;

        public PerformanceTests(ITestOutputHelper output)
        {
            this._output = output;
        }


        [Fact]
        public void SaveOneToOneTestResult()
        {
            for(int i=10; i < 300; i += 20)
                PerformanceTest_OneToOneMatch(i);
        }


        [Fact]
        public void SaveFullGraphTestResult()
        {
            for(int i = 10; i < 300; i += 20)
                PerformanceTest_FullGraphBetweenLayers(i);
        }


        [Theory]
        [InlineData(10)]
        [InlineData(30)]
        [InlineData(50)]
        [InlineData(70)]
        [InlineData(100)]
        [InlineData(110)]
        [InlineData(130)]
        [InlineData(150)]
        [InlineData(170)]
        [InlineData(200)]
        public void PerformanceTest_OneToOneMatch(int count)
        {
            //setup
            var path = "OneToOne" + count + ".txt";
            GenerateOneToOneFile(count, path);

            var sw = new Stopwatch();
            sw.Start();

            //arrange
            var f = new FileReader(path);

            var epi = f.ReadFile();

            var baf = new BestAllocationFinder(epi);

            var resGraph = baf.CalculateMaxFlow();

            var time = sw.ElapsedMilliseconds.ToString();
            _output.WriteLine(time);
            sw.Stop();

            var res = resGraph.GraphToAllocationResult(epi.ProjectCount, epi.SkillCount, epi.ExpertCount);

            res.ExpertToProjects.Should().HaveCount(count);
            for (int i = 0; i < count; i++)
            {
                res.ExpertToProjects.FirstOrDefault(x => x.ExpertId == i && x.ProjectId == i && x.SkillId == i).Should().NotBeNull();
            }

            //save
            File.AppendAllText("../../../ExampleResults/OneToOne.csv", count + "," + time + Environment.NewLine);
        }


        private void GenerateOneToOneFile(int count, string path)
        {
            path = "../../../TestCases/" + path;
            using (var f = File.Create(path)) ;

            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(count + "," + count + "," + count);
                var l = new int[count];
                for (int i = 0; i < count; i++)
                {
                    if (i > 0)
                        l[i - 1] = 0;
                    l[i] = 1;
                    sw.WriteLine(String.Join(",", l));
                }
                for (int i = 0; i < count; i++)
                {
                    if (i > 0)
                        l[i - 1] = 0;
                    else
                        l[l.Length - 1] = 0;
                    l[i] = 1;
                    sw.WriteLine(String.Join(",", l));
                }
            }
        }

        [Theory]
        [InlineData(10)]
        [InlineData(30)]
        [InlineData(50)]
        [InlineData(70)]
        [InlineData(100)]
        [InlineData(110)]
        [InlineData(130)]
        [InlineData(150)]
        [InlineData(170)]
        [InlineData(200)]
        public void PerformanceTest_FullGraphBetweenLayers(int count)
        {
            //setup
            var path = "Full" + count + ".txt";
            GenerateFullFile(count, path);

            var sw = new Stopwatch();
            sw.Start();

            //arrange
            var f = new FileReader(path);

            var epi = f.ReadFile();

            var baf = new BestAllocationFinder(epi);

            var resGraph = baf.CalculateMaxFlow();


            var time = sw.ElapsedMilliseconds.ToString();
            _output.WriteLine(time);
            sw.Stop();

            var res = resGraph.GraphToAllocationResult(epi.ProjectCount, epi.SkillCount, epi.ExpertCount);

            res.ExpertToProjects.Should().HaveCount(count);
            for (int i = 0; i < count; i++)
            {
                res.ExpertToProjects.FirstOrDefault(x => x.ExpertId == i && x.ProjectId == i && x.SkillId == i).Should().NotBeNull();
            }

            //save
            File.AppendAllText("../../../ExampleResults/Full.csv", count + "," + time + Environment.NewLine);
        }

        private void GenerateFullFile(int count, string path)
        {
            path = "../../../TestCases/" + path;
            using (var f = File.Create(path)) ;

            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(count + "," + count + "," + count);
                var l = Enumerable.Repeat(1, count).ToArray();
                for (int i = 0; i < count; i++)
                {
                    sw.WriteLine(String.Join(",", l));
                }
                for (int i = 0; i < count; i++)
                {
                    sw.WriteLine(String.Join(",", l));
                }
            }
        }
    }
}

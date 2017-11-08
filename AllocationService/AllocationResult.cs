using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace AllocationService
{
    public class AllocationResult
    {
        public List<ExpertToProject> ExpertToProjects { get; set; }

        public AllocationResult()
        {
            ExpertToProjects = new List<ExpertToProject>();
        }

        public int GetResourceWasted(int expoertCount)
        {
            return expoertCount - ExpertToProjects.Count;
        }
        
        public override string ToString()
        {
            return string.Join(System.Environment.NewLine, ExpertToProjects);
        }
    }

    public struct ExpertToProject
    {
        public int ExpertId { get; set; }
        public int ProjectId { get; set; }
        public int SkillId { get; set; }
        public override string ToString()
        {
            return "ExpertId: " + ExpertId + ",ProjectId: " + ProjectId + ",Skill used: " + SkillId;
        }
    }
}

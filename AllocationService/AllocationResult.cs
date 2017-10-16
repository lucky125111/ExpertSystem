using System.Collections.Generic;

namespace AllocationService
{
    public class AllocationResult
    {
        public List<ExpertToProject> ExpertToProjects { get; set; }

        public AllocationResult()
        {
            ExpertToProjects = new List<ExpertToProject>();
        }
    }

    public struct ExpertToProject
    {
        public int ExpertId { get; set; }
        public int ProjectId { get; set; }
        public int SkillId { get; set; }
    }
}

using System.Collections.Generic;

namespace AllocationService
{
    public class ExpertProjectInformation
    {
        public int ExpertCount { get; set; }
        public int ProjectCount { get; set; }
        public int SkillCount { get; set; }
        public List<List<int>> ExpertSkills { get; set; }            //tablice o wymiarze ExpertCount x SkillCount
        public List<List<int>> ProjectRequirements { get; set; }     //tablice o wymiarze ProjectCount x SkillCount

        public int GetVerticesCount()
        {
            //graf przeplywu ma wierzcholki w liczbie ExpertCount + ProjectCount + SkillCount + jednen jako superpozycja ujscia + jeden jako superpozycja wejscia
            return ExpertCount + ProjectCount + SkillCount + 2;
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AllocationService
{
    public class FileReader
    {
        private readonly string _readFileName;
        private readonly string _fileName;
        private readonly string _saveFileName;

        public FileReader(string fileName)
        {
            _fileName = fileName;
            _readFileName = "../../../TestCases/" + fileName;
            _saveFileName = "../../../Results/" + fileName;
        }
        public ExpertProjectInformation ReadFile()
        {
            ExpertProjectInformation result = new ExpertProjectInformation();

            using (var filestream = new FileStream(_readFileName,
                FileMode.Open,
                FileAccess.Read,
                FileShare.ReadWrite))
            {
                using (var file = new StreamReader(filestream, System.Text.Encoding.UTF8, true, 128))
                {
                    string lineOfText;

                    int projectCount = -1, skillCount = -1, expertCount = -1, counter = 0;

                    while ((lineOfText = file.ReadLine()) != null)
                    {
                        //pierwsza linijka mowi nam ile jest rodzajow projektow, skillsow i ekspertow
                        if (counter == 0)
                        {
                            var tmp = lineOfText.Split(',').Select(x => int.Parse(x)).ToList();
                            result.ProjectCount = projectCount = tmp[0];
                            result.SkillCount = skillCount = tmp[1];
                            result.ExpertCount = expertCount = tmp[2];
                        }
                        else if (counter < projectCount + 1)
                        {
                            var projectRequierements = StringToList(lineOfText);
                            if (projectRequierements.Count != skillCount)
                                throw new Exception("projekt ma za malo skillsow podanych");
                            result.ProjectRequirements.Add(projectRequierements);
                        }
                        else
                        {
                            var expertSkills = StringToList(lineOfText);
                            if (expertSkills.Count != skillCount)
                                throw new Exception("projekt ma za malo skillsow podanych");
                            result.ExpertSkills.Add(expertSkills);
                        }
                        counter++;
                    }
                }
            }

            if (result.ExpertCount != result.ExpertSkills.Count || result.ProjectCount != result.ProjectRequirements.Count)
                throw new Exception("nie zgada sie liczba linijek");

            return result;
        }

        public void SaveResult(AllocationResult res, int waste)
        {
            using (var filestream = new System.IO.FileStream(_saveFileName,
                System.IO.FileMode.OpenOrCreate,
                System.IO.FileAccess.Write,
                System.IO.FileShare.ReadWrite))
            {
                using (var file = new System.IO.StreamWriter(filestream))
                {
                    file.WriteLine("eksperci nie przypisani: " + waste);
                    file.Write(res);
                }
            }
        }

        private List<int> StringToList(string text)
        {
            return text.Split(',').Select(x => int.Parse(x)).ToList();
        }
    }
}

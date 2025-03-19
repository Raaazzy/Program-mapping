using System.ComponentModel.DataAnnotations;

namespace Program_mapping.Data.Models
{
    public class Discipline
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public string Faculty { get; set; }
        public string Branch { get; set; }
        public string EducationType { get; set; }

        public ICollection<Course> Courses { get; set; }

        public ICollection<ControlElement> ControlElements { get; set; }
        public ICollection<ProgramDiscipline> ProgramDisciplines { get; set; }
    }
}

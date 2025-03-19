using System.ComponentModel.DataAnnotations;

namespace Program_mapping.Data.Models
{
    public class EducationProgram
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public string Annotation { get; set; }
        public string Department { get; set; }

        public ICollection<ProgramDiscipline> ProgramDisciplines { get; set; }
        public ICollection<ProgramSection> ProgramSections { get; set; }
    }
}

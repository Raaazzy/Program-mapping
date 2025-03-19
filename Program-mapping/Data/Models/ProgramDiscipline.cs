namespace Program_mapping.Data.Models
{
    public class ProgramDiscipline
    {
        public long ProgramId { get; set; }
        public EducationProgram EducationProgram { get; set; }

        public long DisciplineId { get; set; }
        public Discipline Discipline { get; set; }
    }
}

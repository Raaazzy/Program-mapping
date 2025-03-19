using System.Reflection;

namespace Program_mapping.Data.Models
{
    public class Course
    {
        public long Id { get; set; }
        public long Number { get; set; }

        public long DisciplineId { get; set; }
        public Discipline Discipline { get; set; }

        public ICollection<Module> Modules { get; set; }
    }
}

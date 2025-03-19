using System.ComponentModel.DataAnnotations;

namespace Program_mapping.Data.Models
{
    public class ControlElement
    {
        [Key]
        public long Id { get; set; }
        public long DisciplineId { get; set; }
        public Discipline Discipline { get; set; }

        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public bool IsExam { get; set; }
        public bool IsBlocking { get; set; }
        public string Format { get; set; }

        public ICollection<ControlElementResultProgram> ControlElementResultPrograms { get; set; }
    }
}

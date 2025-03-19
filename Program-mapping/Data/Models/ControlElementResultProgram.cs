using System.ComponentModel.DataAnnotations;

namespace Program_mapping.Data.Models
{
    public class ControlElementResultProgram
    {
        [Key]
        public long Id { get; set; }

        public long ControlElementId { get; set; }
        public ControlElement ControlElement { get; set; }

        public long ResultId { get; set; }
        public Result Result { get; set; }

        public long ProgramId { get; set; }
        public EducationProgram EducationProgram { get; set; }
    }
}

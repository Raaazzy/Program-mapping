using static System.Collections.Specialized.BitVector32;
using System.ComponentModel.DataAnnotations;

namespace Program_mapping.Data.Models
{
    public class ProgramSection
    {
        [Key]
        public long Id { get; set; }
        public long SectionId { get; set; }
        public Section Section { get; set; }

        public long ProgramId { get; set; }
        public EducationProgram EducationProgram { get; set; }
    }
}

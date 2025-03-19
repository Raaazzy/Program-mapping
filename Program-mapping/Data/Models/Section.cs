using System.ComponentModel.DataAnnotations;

namespace Program_mapping.Data.Models
{
    public class Section
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<ProgramSection> ProgramSections { get; set; }
    }
}

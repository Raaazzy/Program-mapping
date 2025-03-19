using System.ComponentModel.DataAnnotations;

namespace Program_mapping.Data.Models
{
    public class Result
    {
        [Key]
        public long Id { get; set; }
        public string Text { get; set; }

        public ICollection<ControlElementResultProgram> ControlElementResultPrograms { get; set; }
    }
}

namespace Program_mapping.Data.Models
{
    public class Module
    {
        public long Id { get; set; }
        public long Number { get; set; } // 1 модуль, 2 модуль и т.д.

        public long CourseId { get; set; }
        public Course Course { get; set; }
    }
}

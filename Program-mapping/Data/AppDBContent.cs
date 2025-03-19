using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Program_mapping.Data.Models;
using Tamak.Data.Enum;
using Tamak.Data.Helpers;
using Tamak.Data.Models;

namespace Tamak.Data
{
    public class AppDBContent : DbContext
    {
        public AppDBContent(DbContextOptions<AppDBContent> options) : base(options)
        {

            try
            {
                var databaseCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                if (databaseCreator != null)
                {
                    if (!databaseCreator.CanConnect()) databaseCreator.Create();
                    if (!databaseCreator.HasTables()) databaseCreator.CreateTables();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Time> Times { get; set; }
        public DbSet<Assortiment> Assortements { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<Order> Orders { get; set; }


        public DbSet<ProgramDiscipline> ProgramDisciplines { get; set; }
        public DbSet<Discipline> Disciplines { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<EducationProgram> EducationPrograms { get; set; }
        public DbSet<ProgramSection> ProgramSections { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<ControlElement> ControlElements { get; set; }
        public DbSet<ControlElementResultProgram> ControlElementResultPrograms { get; set; }
        public DbSet<Result> Results { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("public");

            modelBuilder.Entity<ProgramDiscipline>(entity =>
            {
                entity.ToTable("ProgramDisciplines");

                entity.HasKey(pd => new { pd.ProgramId, pd.DisciplineId });

                entity.HasOne(pd => pd.EducationProgram)
                      .WithMany(p => p.ProgramDisciplines)
                      .HasForeignKey(pd => pd.ProgramId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Discipline
            modelBuilder.Entity<Discipline>(entity =>
            {
                entity.ToTable("Disciplines");
                entity.HasKey(d => d.Id);

                entity.Property(d => d.Name).IsRequired().HasMaxLength(1000);
                entity.Property(d => d.Department).HasMaxLength(1000);
                entity.Property(d => d.Faculty).HasMaxLength(1000);
                entity.Property(d => d.Branch).HasMaxLength(1000);
                entity.Property(d => d.EducationType).HasMaxLength(1000);

                entity.HasMany(d => d.Courses)
                      .WithOne(c => c.Discipline)
                      .HasForeignKey(c => c.DisciplineId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Курс
            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("Courses");
                entity.HasKey(c => c.Id);

                entity.Property(c => c.Number).IsRequired();

                entity.HasMany(c => c.Modules)
                      .WithOne(m => m.Course)
                      .HasForeignKey(m => m.CourseId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Модуль
            modelBuilder.Entity<Module>(entity =>
            {
                entity.ToTable("Modules");
                entity.HasKey(m => m.Id);

                entity.Property(m => m.Number).IsRequired();
            });

            // Program
            modelBuilder.Entity<EducationProgram>(entity =>
            {
                entity.ToTable("EducationProgram");
                entity.HasKey(p => p.Id);

                entity.Property(p => p.Name).IsRequired().HasMaxLength(1000);
                entity.Property(p => p.Annotation).HasColumnType("text");
                entity.Property(p => p.Department).HasMaxLength(1000);
            });

            // ProgramSection
            modelBuilder.Entity<ProgramSection>(entity =>
            {
                entity.ToTable("ProgramSections");
                entity.HasKey(ps => ps.Id);

                entity.HasOne(ps => ps.EducationProgram)
                      .WithMany(p => p.ProgramSections)
                      .HasForeignKey(ps => ps.ProgramId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(ps => ps.Section)
                      .WithMany(s => s.ProgramSections)
                      .HasForeignKey(ps => ps.SectionId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Section
            modelBuilder.Entity<Section>(entity =>
            {
                entity.ToTable("Sections");
                entity.HasKey(s => s.Id);

                entity.Property(s => s.Name).IsRequired().HasMaxLength(1000);
                entity.Property(s => s.Description).HasColumnType("text");
            });

            // ControlElement
            modelBuilder.Entity<ControlElement>(entity =>
            {
                entity.ToTable("ControlElements");
                entity.HasKey(ce => ce.Id);

                entity.Property(ce => ce.Name).IsRequired().HasMaxLength(1000);
                entity.Property(ce => ce.Type).HasMaxLength(1000);
                entity.Property(ce => ce.Description).HasColumnType("text");
                entity.Property(ce => ce.Format).HasMaxLength(1000);

                entity.HasOne(ce => ce.Discipline)
                      .WithMany(d => d.ControlElements)
                      .HasForeignKey(ce => ce.DisciplineId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // ControlElementResultProgram
            modelBuilder.Entity<ControlElementResultProgram>(entity =>
            {
                entity.ToTable("ControlElementResultPrograms");
                entity.HasKey(cerp => cerp.Id);

                entity.HasOne(cerp => cerp.ControlElement)
                      .WithMany(ce => ce.ControlElementResultPrograms)
                      .HasForeignKey(cerp => cerp.ControlElementId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(cerp => cerp.Result)
                      .WithMany(r => r.ControlElementResultPrograms)
                      .HasForeignKey(cerp => cerp.ResultId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(cerp => cerp.EducationProgram)
                      .WithMany()
                      .HasForeignKey(cerp => cerp.ProgramId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Result
            modelBuilder.Entity<Result>(entity =>
            {
                entity.ToTable("Results");
                entity.HasKey(r => r.Id);

                entity.Property(r => r.Text).IsRequired();
            });

            //modelBuilder.Entity<User>(builder =>
            //{
            //    builder.ToTable("User").HasKey(x => x.Id);
            //    builder.Property(x => x.Id).ValueGeneratedOnAdd();
            //    builder.Property(x => x.Password).IsRequired();
            //    builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
            //    builder.Property(x => x.Email).HasMaxLength(100).IsRequired();

            //    builder.HasOne(x => x.Assortiment).WithOne(x => x.User).HasPrincipalKey<User>(x => x.Id).OnDelete(DeleteBehavior.Cascade);

            //    builder.HasOne(x => x.Basket)
            //        .WithOne(x => x.User)
            //        .HasPrincipalKey<User>(x => x.Id)
            //        .OnDelete(DeleteBehavior.Cascade);
            //});

            //modelBuilder.Entity<Assortiment>(builder =>
            //{
            //    builder.ToTable("Assortiments").HasKey(x => x.Id);
            //});

            //modelBuilder.Entity<Product>(builder =>
            //{
            //    builder.ToTable("Products").HasKey(x => x.Id);
            //    builder.Property(x => x.Id).ValueGeneratedOnAdd();
            //    builder.HasOne(r => r.Assortiment).WithMany(t => t.Products).HasForeignKey(x => x.AssortimentId);
            //});

            //modelBuilder.Entity<Basket>(builder =>
            //{
            //    builder.ToTable("Baskets").HasKey(x => x.Id);
            //});

            //modelBuilder.Entity<Order>(builder =>
            //{
            //    builder.ToTable("Orders").HasKey(x => x.Id);
            //    builder.HasOne(r => r.Basket).WithMany(t => t.Orders)
            //        .HasForeignKey(r => r.BasketId);
            //});

            //modelBuilder.Entity<Time>(builder =>
            //{
            //    builder.ToTable("Times").HasKey(x => x.Id);
            //    builder.Property(x => x.Id).ValueGeneratedOnAdd();
            //    builder.HasOne(r => r.Assortiment).WithMany(t => t.Times)
            //        .HasForeignKey(r => r.AssortimentId);
            //});
        }
    }
}

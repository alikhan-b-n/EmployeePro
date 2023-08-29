using EmployeePro.Dal.Entities;
using EmployeePro.Dal.Entities.BridgeTables;
using Microsoft.EntityFrameworkCore;

namespace EmployeePro.Dal;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions options) : base(options)
    {
        Database.Migrate();
    }

    #region Adding Entitites to DB
    public DbSet<EmployeeEntity> EmployeeEntities { get; set; }
    public DbSet<EducationEntity> EducationEntities { get; set; }
    public DbSet<DepartmentEntity> DepartmentEntities { get; set; }
    public DbSet<SkillEntity> SkillEntities { get; set; }
    public DbSet<ExperienceEntity> ExperienceEntities { get; set; }
    public DbSet<EmployeeSkillEntity> EmployeesSkills { get; set; }
    #endregion
   

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EmployeeSkillEntity>().HasKey(es => new
        {
            es.EmployeeId,
            es.SkillId
        });

        modelBuilder.Entity<EmployeeSkillEntity>()
            .HasOne(es => es.EmployeeEntity)
            .WithMany(y => y.EmployeesSkills)
            .HasForeignKey(es => es.EmployeeId);
        
        modelBuilder.Entity<EmployeeSkillEntity>()
            .HasOne(es => es.SkillEntity)
            .WithMany(y => y.EmployeesSkills)
            .HasForeignKey(es => es.SkillId);
        
        modelBuilder.Entity<EmployeeLanguageEntity>().HasKey(es => new
        {
            es.EmployeeId,
            es.LanguageId
        });

        modelBuilder.Entity<EmployeeLanguageEntity>()
            .HasOne(es => es.EmployeeEntity)
            .WithMany(y => y.EmployeeLanguages)
            .HasForeignKey(es => es.EmployeeId);
        
        modelBuilder.Entity<EmployeeLanguageEntity>()
            .HasOne(es => es.LanguageEntity)
            .WithMany(y => y.EmployeeLanguages)
            .HasForeignKey(es => es.LanguageId);
    }
}
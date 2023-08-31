using EmployeePro.Bll.Dtos;
using EmployeePro.Bll.Services;
using EmployeePro.Dal;
using EmployeePro.Dal.Entities;
using EmployeePro.Dal.Entities.BridgeTables;
using EmployeePro.Dal.Providers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;

namespace EmployeePro.Bll.Test;

public class EmployeeCreatorTests
{
    private readonly ApplicationContext _applicationContext;
    private readonly Mock<ILinkedinService> _linkedinService;
    private readonly Repository<EmployeeEntity> _employeeProvider;
    private readonly Repository<ExperienceEntity> _experienceProvider;
    private readonly Repository<EducationEntity> _educationProvider;
    private readonly Repository<SkillEntity> _skillProvider;
    private readonly Repository<LanguageEntity> _languageProvider;
    private readonly Mock<ITokenService> _authentificationService;
    private readonly Repository<EmployeeLanguageEntity> _employeeLanguageProvider;
    private readonly Repository<EmployeeSkillEntity> _employeeSkillProvider;

    public EmployeeCreatorTests()
    {
        var builder = new DbContextOptionsBuilder<ApplicationContext>();
        builder.UseInMemoryDatabase(GetType().Name);

        _applicationContext = new ApplicationContext(builder.Options);
        _applicationContext.Database.EnsureDeleted();
        _applicationContext.Database.EnsureCreated();

        _employeeProvider = new Repository<EmployeeEntity>(_applicationContext);
        _experienceProvider = new Repository<ExperienceEntity>(_applicationContext);
        _educationProvider = new Repository<EducationEntity>(_applicationContext);
        _skillProvider = new Repository<SkillEntity>(_applicationContext);
        _languageProvider = new Repository<LanguageEntity>(_applicationContext);
        _employeeLanguageProvider = new Repository<EmployeeLanguageEntity>(_applicationContext);
        _employeeSkillProvider = new Repository<EmployeeSkillEntity>(_applicationContext);
        _authentificationService = new Mock<ITokenService>();
        _linkedinService = new Mock<ILinkedinService>();
    }

    // [Fact]
    // public async Task CreateEmployee_ShouldCreateEmployeeAndConnectedToItEntitiesInDb_PositiveTest()
    // {
    //     // Arrange
    //     _linkedinService
    //         .Setup(x => x.GetInfoFromLinkedinUrl(It.IsAny<string>()))
    //         .ReturnsAsync(new TotalInfoFromApis
    //         {
    //             FullName = "Alikhan",
    //             ProfilePicUrl = "some_url",
    //             Summary = "Summary about me",
    //             Languages = new List<string>{"Kazakh", "Russian", "English"},
    //             Skills = null,
    //             Experiences = new List<Experience>
    //             {
    //                 new Experience
    //                 {
    //                     Company = "Kazakhtelecom",
    //                     Description = "I intershiped here",
    //                     Title = "Intern"
    //                 }
    //             },
    //             Education = new List<Education>
    //             {
    //                 new Education
    //                 {
    //                     
    //                 }
    //             }
    //         })
    //     // Act
    //     // Assert
    // }
}
using EmployeePro.Bll.Dtos;
using EmployeePro.Dal.Entities;
using EmployeePro.Dal.Entities.BridgeTables;
using EmployeePro.Dal.Providers;
using EmployeePro.Dal.Providers.Interfaces;

namespace EmployeePro.Bll.Services;

public class EmployeeCreator : IEmployeeCreatorAndUpdater
{
    private readonly ILinkedinService _linkedinService;
    private readonly ICrudProvider<EmployeeEntity> _employeeProvider;
    private readonly ICrudProvider<ExperienceEntity> _experienceProvider;
    private readonly ICrudProvider<EducationEntity> _educationProvider;
    private readonly ICrudProvider<SkillEntity> _skillProvider;
    private readonly ICrudProvider<LanguageEntity> _languageProvider;
    private readonly IAuthentificationService _authentificationService;
    private readonly ICrudProvider<EmployeeLanguageEntity> _employeeLanguageProvider;
    private readonly ICrudProvider<EmployeeSkillEntity> _employeeSkillProvider;

    public EmployeeCreator(ILinkedinService linkedinService, ICrudProvider<EmployeeEntity> employeeProvider,
        ICrudProvider<ExperienceEntity> experienceProvider, ICrudProvider<EducationEntity> educationProvider,
        ICrudProvider<SkillEntity> skillProvider, ICrudProvider<LanguageEntity> languageProvider,
        IAuthentificationService authentificationService,
        ICrudProvider<EmployeeLanguageEntity> employeeLanguageProvider,
        ICrudProvider<EmployeeSkillEntity> employeeSkillProvider)
    {
        _linkedinService = linkedinService;
        _employeeProvider = employeeProvider;
        _experienceProvider = experienceProvider;
        _educationProvider = educationProvider;
        _skillProvider = skillProvider;
        _languageProvider = languageProvider;
        _authentificationService = authentificationService;
        _employeeLanguageProvider = employeeLanguageProvider;
        _employeeSkillProvider = employeeSkillProvider;
    }
    
    public async Task CreateEmployee(string url, string email)
    {
        var linkedinResponse = await _linkedinService.GetInfoFromLinkedinUrl(url);

        var employeeEntity = new EmployeeEntity
        {
            Email = email,
            Fullname = linkedinResponse.FullName,
            Summary = linkedinResponse.Summary,
            PasswordHash = _authentificationService.GenerateHashPassword(email, linkedinResponse.FullName),
            ProfilePicUrl = linkedinResponse.ProfilePicUrl
        };
        await _employeeProvider.Create(employeeEntity);
        await CreateEducation(employeeEntity, linkedinResponse);
        await CreateExperience(employeeEntity, linkedinResponse);
        await CreateEmployeeLanguage(employeeEntity, linkedinResponse);
        await CreateEmployeeSkills(employeeEntity, linkedinResponse);
    }

    private async Task CreateEducation(EmployeeEntity employeeEntity, TotalInfoFromApis data)
    {
        foreach (var education in data.Education)
        {
            var startDate = CreateNullableDateTime(education.StartAt?.Year, education.StartAt?.Month, education.StartAt?.Day);
            var endDate = CreateNullableDateTime(education.EndsAt?.Year, education.EndsAt?.Month, education.EndsAt?.Day);
            var educationEntity = new EducationEntity
            {
                SchoolName = education.School,
                Description = education.Description,
                StartDate = startDate?.ToUniversalTime(),
                EndDate = endDate?.ToUniversalTime(),
                EmployeeId = employeeEntity.Id,
                EmployeeEntity = employeeEntity
            };

            await _educationProvider.Create(educationEntity);
        };

    }

    private async Task CreateExperience(EmployeeEntity employeeEntity, TotalInfoFromApis data)
    {
        foreach (var x in data.Experiences)
        {
            var startDate = CreateNullableDateTime(x.StartAt?.Year, x.StartAt?.Month, x.StartAt?.Day);
            var endDate = CreateNullableDateTime(x.EndsAt?.Year, x.EndsAt?.Month, x.EndsAt?.Day);
            var experienceEntity = new ExperienceEntity
            {
                JobTitle = x.Title,
                StartDate = startDate?.ToUniversalTime(),
                EndDate = endDate?.ToUniversalTime(),
                CompanyName = x.Company,
                Description = x.Description,
                EmployeeId = employeeEntity.Id,
                EmployeeEntity = employeeEntity
            };

            await _experienceProvider.Create(experienceEntity);
        }
    }

    /// <summary>
    /// Creating bridge table for Employee and Language entities
    /// </summary>
    private async Task CreateEmployeeLanguage(EmployeeEntity employeeEntity, TotalInfoFromApis data)
    {
        var languageEntities = await _languageProvider.GetAll();
        var newLanguages = data.Languages.Except(languageEntities.Select(x => x.Language)).ToList();

        foreach (var language in newLanguages)
        {
            var newLanguageEntity = new LanguageEntity
            {
                Language = language
            };

            var employeeLanguageEntity = new EmployeeLanguageEntity
            {
                EmployeeEntity = employeeEntity,
                LanguageEntity = newLanguageEntity,
                EmployeeId = employeeEntity.Id,
                LanguageId = newLanguageEntity.Id
            };
            
            // Save changes
            await _languageProvider.Create(newLanguageEntity);
            await _employeeLanguageProvider.Create(employeeLanguageEntity);
            
            // Add employeeLanguageEntity to the collections
            newLanguageEntity.EmployeeLanguages.Add(employeeLanguageEntity);
            employeeEntity.EmployeeLanguages?.Add(employeeLanguageEntity);

        }
    }

    /// <summary>
    /// Creating bridge table for Employee and Skill entities
    /// </summary>
    private async Task CreateEmployeeSkills(EmployeeEntity employeeEntity, TotalInfoFromApis data)
    {
        var skillEntities = await _skillProvider.GetAll();
        
        //List of new skills that haven't been added to db
        var newSkills = data.Skills.Skills
            .Select(x => x.Name)
            .Except(skillEntities
                .Select(x => x.Skill)).ToList();

        foreach (var skill in newSkills)
        {
            var skillEntity = new SkillEntity
            {
                Skill = skill
            };

            var employeeSkillEntity = new EmployeeSkillEntity
            {
                SkillEntity = skillEntity,
                EmployeeEntity = employeeEntity,
                SkillId = skillEntity.Id,
                EmployeeId = employeeEntity.Id
            };

            // Creating new entities
            await _skillProvider.Create(skillEntity);
            await _employeeSkillProvider.Create(employeeSkillEntity);

            skillEntity.EmployeesSkills.Add(employeeSkillEntity);
            employeeEntity.EmployeesSkills?.Add(employeeSkillEntity);

            await _skillProvider.Update(skillEntity);
            await _employeeProvider.Update(employeeEntity);
        }
    }

    private DateTime? CreateNullableDateTime(int? year, int? month, int? day)
    {
        if (year.HasValue && month.HasValue && day.HasValue)
        {
            try
            {
                return new DateTime(year.Value, month.Value, day.Value).ToUniversalTime();
            }
            catch (ArgumentOutOfRangeException)
            {
                // Handle invalid date values, if needed.
                return null;
            }
        }

        // Return null if any of the input values are null.
        return null;
    }
}
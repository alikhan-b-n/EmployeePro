using EmployeePro.Bll.Dtos;
using EmployeePro.Dal.Entities;
using EmployeePro.Dal.Entities.BridgeTables;
using EmployeePro.Dal.Providers.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace EmployeePro.Bll.Services;

public class EmployeeManager : IEmployeeManager
{
    private readonly IEmployeeCreatorAndUpdater _employeeCreatorAndUpdater;
    private readonly ICrudProvider<EmployeeEntity> _employeeProvider;
    private readonly ICrudProvider<EmployeeLanguageEntity> _employeeLanguageProvider;
    private readonly ICrudProvider<EmployeeSkillEntity> _employeeSkillProvider;
    private readonly ICrudProvider<ExperienceEntity> _experienceProvider;
    private readonly ICrudProvider<EducationEntity> _educationProvider;
    private readonly ICrudProvider<DepartmentEntity> _departmentProvider;


    public EmployeeManager(IEmployeeCreatorAndUpdater employeeCreatorAndUpdater,
        ICrudProvider<EmployeeEntity> employeeProvider,
        ICrudProvider<EmployeeLanguageEntity> employeeLanguageProvider,
        ICrudProvider<EmployeeSkillEntity> employeeSkillProvider,
        ICrudProvider<ExperienceEntity> experienceProvider,
        ICrudProvider<EducationEntity> educationProvider,
        ICrudProvider<DepartmentEntity> departmentProvider)
    {
        _employeeCreatorAndUpdater = employeeCreatorAndUpdater;
        _employeeProvider = employeeProvider;
        _employeeLanguageProvider = employeeLanguageProvider;
        _employeeSkillProvider = employeeSkillProvider;
        _experienceProvider = experienceProvider;
        _educationProvider = educationProvider;
        _departmentProvider = departmentProvider;
    }

    public async Task CreateEmployee(string url, string email)
    {
        await _employeeCreatorAndUpdater.CreateEmployee(url, email);
    }

    public async Task<EmployeeDto> GetByIdEmployeeProfile(Guid id)
    {
        var employeeEntity = await _employeeProvider.GetById(id);
        var department = await _departmentProvider.GetById(employeeEntity?.DepartmentId);

        return new EmployeeDto
        {
            Fullname = employeeEntity.Fullname,
            Email = employeeEntity.Email,
            Summary = employeeEntity.Summary,
            ProfilePicUrl = employeeEntity.ProfilePicUrl,
            DepartmentId = employeeEntity.DepartmentId,
            Skills = await GetEmployeeSkills(employeeEntity.Id),
            Languages = await GetEmployeeLanguages(employeeEntity.Id),
            Id = employeeEntity.Id,
            Department = department?.Title
        };
    }

    public async Task DeleteByIdEmployeeProfile(Guid id)
    {
        await DeleteEmployeeEducation(id);
        await DeleteEmployeeExperience(id);
        await UntieLanguagesFromEmployee(id);
        await UntieSkillsFromEmployee(id);
        await _employeeProvider.Delete(id);
    }

    public async Task<List<EmployeeDto>> GetAllEmployeeProfiles()
    {
        var employeeEntities = await _employeeProvider.GetAll();
        List<EmployeeDto> employeeDtos = new List<EmployeeDto>();
        foreach (var employeeEntity in employeeEntities)
        {
            var skill = await GetEmployeeSkills(employeeEntity.Id);
            var languages = await GetEmployeeLanguages(employeeEntity.Id);
            var department = await _departmentProvider.GetById(employeeEntity.DepartmentId);

            employeeDtos.Add(new EmployeeDto
            {
                Fullname = employeeEntity.Fullname,
                Email = employeeEntity.Email,
                Summary = employeeEntity.Summary,
                ProfilePicUrl = employeeEntity.ProfilePicUrl,
                DepartmentId = employeeEntity.DepartmentId,
                Skills = skill,
                Languages = languages,
                Id = employeeEntity.Id,
                Department = department?.Title
            });
        }

        return employeeDtos;
    }

    /// <summary>
    /// Changes concrete properties of existing employee profile
    /// </summary>
    /*TODO: Redo update method to check if values are null*/
    public async Task UpdateEmployeeProfile(EmployeeDto employeeDto)
    {
        var employeeEntity = await _employeeProvider.GetById(employeeDto.Id);

        if (employeeDto.Fullname != null)
        {
            employeeEntity.Fullname = employeeDto.Fullname;
        }

        if (employeeDto.Summary != null)
        {
            employeeEntity.Summary = employeeDto.Summary;
        }

        if (employeeDto.ProfilePicUrl != null)
        {
            employeeEntity.ProfilePicUrl = employeeDto.ProfilePicUrl;
        }

        if (employeeDto.DepartmentId != null)
        {
            employeeEntity.DepartmentId = employeeDto.DepartmentId;
        }

        // Save the changes to the database
        await _employeeProvider.Update(employeeEntity);
    }


    private async Task<List<string>?> GetEmployeeSkills(Guid employeeId)
    {
        var employeeEntities = _employeeProvider
            .Query()
            .Include(x => x.EmployeesSkills)!
            .ThenInclude(es => es.SkillEntity);

        var employeeEntity = await employeeEntities.FirstAsync(x => x.Id == employeeId);

        var skills = employeeEntity.EmployeesSkills?
            .Select(x => x.SkillEntity.Skill).ToList();

        return skills;
    }

    private async Task<List<string>?> GetEmployeeLanguages(Guid employeeId)
    {
        var employeeEntities = _employeeProvider
            .Query()
            .Include(x => x.EmployeeLanguages)!
            .ThenInclude(el => el.LanguageEntity);

        var employeeEntity = await employeeEntities.FirstAsync(x => x.Id == employeeId);

        var languages = employeeEntity.EmployeeLanguages?
            .Select(x => x.LanguageEntity.Language)
            .ToList();
        
        return languages;
    }

    private async Task UntieLanguagesFromEmployee(Guid employeeId)
    {
        var employeeEntity = await _employeeProvider.GetById(employeeId);
        if (employeeEntity.Equals(null) || employeeEntity.EmployeeLanguages.Equals(null))
        {
            return;
        }

        foreach (var employeeLanguageId in employeeEntity.EmployeeLanguages.Select(x => x.Id))
        {
            await _employeeLanguageProvider.Delete(employeeLanguageId);
        }
    }

    private async Task UntieSkillsFromEmployee(Guid employeeId)
    {
        var employeeEntity = await _employeeProvider.GetById(employeeId);
        if (employeeEntity.Equals(null) || employeeEntity.EmployeesSkills.Equals(null))
        {
            return;
        }

        foreach (var employeeSkillId in employeeEntity.EmployeesSkills.Select(x => x.Id))
        {
            await _employeeSkillProvider.Delete(employeeSkillId);
        }
    }

    private async Task DeleteEmployeeExperience(Guid employeeId)
    {
        var experienceEntities = await _experienceProvider.GetAll();
        var experienceOfEmployee = experienceEntities
            .Where(x => x.EmployeeId == employeeId)
            .Select(x => x.Id);
        foreach (var id in experienceOfEmployee)
        {
            await _experienceProvider.Delete(id);
        }
    }

    private async Task DeleteEmployeeEducation(Guid employeeId)
    {
        var educationEntities = await _educationProvider.GetAll();
        var educationOfEmployee = educationEntities
            .Where(x => x.EmployeeId == employeeId)
            .Select(x => x.Id);
        foreach (var id in educationOfEmployee)
        {
            await _educationProvider.Delete(id);
        }
    }
}
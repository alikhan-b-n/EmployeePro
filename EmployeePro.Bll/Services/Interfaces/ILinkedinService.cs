using System.Collections;
using EmployeePro.Bll.Dtos;

namespace EmployeePro.Bll.Services;

public interface ILinkedinService
{
    Task<TotalInfoFromApis> GetInfoFromLinkedinUrl(string url);
    Task<ApisWorldLinkedinApiDto?> FetchDataFromLinkedin(string url);

    Task<RockDevsLinkedInApiDto?> FetchSkillsFromLinkedin(string username);
}
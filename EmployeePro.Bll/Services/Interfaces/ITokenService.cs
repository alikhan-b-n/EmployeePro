using EmployeePro.Bll.Dtos;
using EmployeePro.Dal.Entities;

namespace EmployeePro.Bll.Services;

public interface ITokenService
{
    public string GenerateToken(string nameIdentifier, string actor, Guid id);
    public (string nameIdentifier, string actor, string id) DecryptToken(string token);
}
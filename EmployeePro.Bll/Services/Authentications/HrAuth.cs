using System.Text;
using EmployeePro.Bll.Dtos;
using EmployeePro.Bll.Services.Interfaces;
using EmployeePro.Contract.Options;
using EmployeePro.Dal.Entities;
using EmployeePro.Dal.Providers.Interfaces;
using Microsoft.Extensions.Options;

namespace EmployeePro.Bll.Services.Authentications;

public class HrAuth : IHrAuth
{
    private readonly ITokenService _tokenService;
    private readonly ICrudProvider<HrEntity> _hrProvider;
    private readonly IOptions<SecretOptions> _secretOptions;

    public HrAuth(
        ITokenService tokenService,
        ICrudProvider<HrEntity> hrProvider,
        IOptions<SecretOptions> secretOptions)
    {
        _tokenService = tokenService;
        _hrProvider = hrProvider;
        _secretOptions = secretOptions;
    }

    private string GenerateHashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(new StringBuilder($"{password}").ToString());
    }

    public async Task<string> SignIn(HrSignInDto hrSignInDto)
    {
        var hrEntities = await _hrProvider.GetAll();
        var hrEntity = hrEntities.First(x => x.Login == hrSignInDto.Login);
        if (hrEntity == null) throw new ArgumentException("error, login is not found");

        if (BCrypt.Net.BCrypt.Verify(hrSignInDto.Password, hrEntity.PasswordHash))
        {
            return _tokenService.GenerateToken(hrEntity.Login, "HR", hrEntity.Id);
        }

        throw new ArgumentException($"error, password is not correct");
    }

    public async Task<string> SignUp(HrSignUpDto hrSignUpDto)
    {
        if (hrSignUpDto.MasterKey != _secretOptions.Value.MasterKey)
            throw new ArgumentException("Not correct master key");

        var hrEntity = new HrEntity
        {
            Login = hrSignUpDto.Login,
            PasswordHash = GenerateHashPassword(hrSignUpDto.Password)
        };

        await _hrProvider.Create(hrEntity);
        
        return _tokenService.GenerateToken(hrEntity.Login, "HR", hrEntity.Id);
    }
}
using EmployeePro.Bll.Dtos;
using EmployeePro.Bll.Services.Interfaces;
using EmployeePro.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EmployeePro.Controllers.Hr;

public class HrController : ControllerBase
{
    private readonly IHrAuth _auth;

    public HrController(IHrAuth auth)
    {
        _auth = auth;
    }

    [HttpPost("/api/hr/signup")]
    public async Task<IActionResult> SignUp([FromBody] HrSignUpViewModel hrSignUpViewModel)
    {
        return Ok(await _auth.SignUp(new HrSignUpDto
        {
            Login = hrSignUpViewModel.Login,
            Password = hrSignUpViewModel.Password,
            MasterKey = hrSignUpViewModel.MasterKey
        }));
    }

    [HttpPost("/api/hr/signin")]
    public async Task<IActionResult> SignIn([FromBody] HrSignInViewModel hrSignInViewModel)
    {
        return Ok(await _auth.SignIn(new HrSignInDto
        {
            Login = hrSignInViewModel.Login,
            Password = hrSignInViewModel.Password,
        }));
    }
}
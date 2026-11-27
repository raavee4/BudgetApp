using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BudgetApp.Features.Profile;

[ApiController]
[Authorize]
[Route("api/profile")]
public class ProfileController : ControllerBase
{
    private readonly IMediator _mediator;
    public ProfileController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<GetProfileResponse>> GetUserProfile()
    {
        var result = await _mediator.Send(new GetProfileQuery());
        return Ok(result);
    }

    [HttpPut]
    public async Task<ActionResult<UpdateProfileResponse>> UpdateUserProfile([FromBody] UpdateProfileCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}

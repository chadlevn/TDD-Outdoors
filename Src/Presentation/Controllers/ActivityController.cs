using Application.Activities.Commands.AddActivity;
using Application.Activities.Queries.GetUpcomingActivities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public class ActivityController : ControllerBase
{
    private readonly IMediator _mediator;

    public ActivityController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("UpcomingActivites")]
    public async Task<IActionResult> UpcomingActivities(CancellationToken cancellationToken = default)
    {
        var upcomingActivities = await _mediator.Send(new GetUpcomingActivitiesQuery(), cancellationToken);

        return Ok(upcomingActivities);
    }

    [HttpPost("AddActivity")]
    public async Task<IActionResult> AddActivity([FromServices] IValidator<AddActivityModel> validator, [FromBody] AddActivityModel addActivityModel, CancellationToken cancellationToken = default)
    {
        var validationResult = await validator.ValidateAsync(addActivityModel, cancellationToken);

        if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

        var newActivityId = await _mediator.Send(new AddActivityCommand(addActivityModel), cancellationToken);

        return Ok(newActivityId);
    }
}

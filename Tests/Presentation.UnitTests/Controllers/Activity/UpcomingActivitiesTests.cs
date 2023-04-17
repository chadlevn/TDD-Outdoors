using Application.Activities.Queries.GetUpcomingActivities;
using Domain.Constants;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Presentation.Controllers;

namespace Presentation.UnitTests.Controllers.Activity;

public sealed class UpcomingActivitiesTests
{
    private readonly Mock<IMediator> _mockMediator;
    private readonly ActivityController _controller;

    public UpcomingActivitiesTests()
    {
        _mockMediator = new Mock<IMediator>();
        _controller = new ActivityController(_mockMediator.Object);
    }

    [Fact]
    public async Task Returns_OkResult_With_GetUpcomingActivitiesQuery_ReturnValue()
    {
        var testUpcomingActivites = new List<UpcomingActivityDto>()
        {
            new UpcomingActivityDto()
            {
                Name = Guid.NewGuid().ToString(),
                Type = ActivityTypeConstants.Biking,
                Date = new DateTime(3000, 1, 1)
            }
        };

        _mockMediator.Setup(m => m.Send(It.IsAny<GetUpcomingActivitiesQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(testUpcomingActivites);

        var result = await _controller.UpcomingActivities();

        result.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeEquivalentTo(testUpcomingActivites);
    }
}
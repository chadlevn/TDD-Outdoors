using Application.Activities.Commands.AddActivity;
using Application.Common.Interfaces;
using Domain.Constants;
using FluentAssertions;
using Xunit;

namespace Application.UnitTests.Activities.Commands.AddActivity;

public sealed class HandlerTests
{
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly AddActivityCommand.AddActivityCommandHandler _handler;

    public HandlerTests()
    {
        var applicationTestFixture = new ApplicationTestFixture();
        _applicationDbContext = applicationTestFixture.ApplicationDbContext;
        _handler = new AddActivityCommand.AddActivityCommandHandler(_applicationDbContext);
    }

    [Fact]
    public async Task NewActivity_IsAddedToContext_WithValuesFromModel()
    {
        var testAddActivityModel = new AddActivityModel()
        {
            Name = Guid.NewGuid().ToString(),
            Type = ActivityTypeConstants.Biking,
            Description = Guid.NewGuid().ToString(),
            Date = new DateTime(3000, 1, 1)
        };

        await _handler.Handle(new AddActivityCommand(testAddActivityModel));

        var newActivity = _applicationDbContext.Activities.First();

        newActivity.Name.Should().Be(testAddActivityModel.Name);
        newActivity.Type.Should().Be(testAddActivityModel.Type);
        newActivity.Description.Should().Be(testAddActivityModel.Description);
        newActivity.Date.Should().Be(testAddActivityModel.Date);
    }

    [Fact]
    public async Task Returns_ActivityId_FromNewlyCreatedActivity()
    {
        var testAddActivityModel = new AddActivityModel()
        {
            Name = Guid.NewGuid().ToString(),
            Type = ActivityTypeConstants.Fishing,
            Date = new DateTime(3000, 1, 1)
        };

        var returnValue = await _handler.Handle(new AddActivityCommand(testAddActivityModel));

        var newActivity = _applicationDbContext.Activities.First();

        returnValue.Should().Be(newActivity.ActivityId);
    }
}
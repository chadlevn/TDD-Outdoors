using Application.Activities.Commands.AddActivity;
using Domain.Constants;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Presentation.Controllers;

namespace Presentation.UnitTests.Controllers.Activity;

public sealed class AddActivityTests
{
    private readonly Mock<IMediator> _mockMediator;
    private readonly Mock<IValidator<AddActivityModel>> _mockValidator;
    private readonly ActivityController _controller;

    public AddActivityTests()
    {
        _mockMediator = new Mock<IMediator>();
        _mockValidator = new Mock<IValidator<AddActivityModel>>();
        _controller = new ActivityController(_mockMediator.Object);
    }

    [Fact]
    public async Task When_AddActivityModel_IsInvalid_Returns_BadRequest_WithValidationErrors()
    {
        var testValidationResult = new ValidationResult()
        {
            Errors = new List<ValidationFailure>()
            {
                new ValidationFailure()
            }
        };

        _mockValidator.Setup(m => m.ValidateAsync(It.IsAny<AddActivityModel>(), It.IsAny<CancellationToken>())).ReturnsAsync(testValidationResult);

        var result = await _controller.AddActivity(_mockValidator.Object, new AddActivityModel());

        result.Should().BeOfType<BadRequestObjectResult>().Which.Value.Should().BeEquivalentTo(testValidationResult.Errors);
    }

    [Fact]
    public async Task When_AddActivityModel_IsValid_Returns_Ok_WithNewActivityId()
    {
        var testValidationResult = new ValidationResult();

        var testAddActivityModel = new AddActivityModel()
        {
            Name = Guid.NewGuid().ToString(),
            Type = ActivityTypeConstants.Biking,
            Date = new DateTime(3000, 1, 1)
        };

        _mockValidator.Setup(m => m.ValidateAsync(testAddActivityModel, It.IsAny<CancellationToken>())).ReturnsAsync(testValidationResult);

        var expectedNewActivityId = int.MaxValue;

        _mockMediator.Setup(m => m.Send(new AddActivityCommand(testAddActivityModel), It.IsAny<CancellationToken>())).ReturnsAsync(expectedNewActivityId);

        var result = await _controller.AddActivity(_mockValidator.Object, testAddActivityModel);

        result.Should().BeOfType<OkObjectResult>().Which.Value.Should().Be(expectedNewActivityId);
    }
}
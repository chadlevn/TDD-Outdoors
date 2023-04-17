using Application.Activities.Commands.AddActivity;
using Application.Common.Interfaces;
using Domain.Constants;
using FluentValidation.TestHelper;
using Moq;
using Xunit;

namespace Application.UnitTests.Activities.Commands.AddActivity;

public sealed class ValidatorTests
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly AddActivityModelValidator _validator;

    public ValidatorTests()
    {
        var mockDateTimeProvider = new Mock<IDateTimeProvider>();
        mockDateTimeProvider.Setup(m => m.Now).Returns(new DateTime(3000, 1, 1));
        _dateTimeProvider = mockDateTimeProvider.Object;

        _validator = new AddActivityModelValidator(_dateTimeProvider);
    }

    [Fact]
    public void Name_WhenDefault_IsInvalid() =>
        _validator.TestValidate(new AddActivityModel())
            .ShouldHaveValidationErrorFor(m => m.Name)
            .WithErrorMessage("Name is required.");

    [Fact]
    public void Name_WhenNotDefault_IsValid() =>
        _validator.TestValidate(new AddActivityModel() { Name = Guid.NewGuid().ToString() })
            .ShouldNotHaveValidationErrorFor(m => m.Name);

    [Fact]
    public void Type_WhenDefault_IsInvalid() =>
        _validator.TestValidate(new AddActivityModel())
            .ShouldHaveValidationErrorFor(m => m.Type)
            .WithErrorMessage("Type is required.");

    [Fact]
    public void Type_WhenNotAnActivityTypeConstant_IsInalid() =>
        _validator.TestValidate(new AddActivityModel() { Type = Guid.NewGuid().ToString() })
            .ShouldHaveValidationErrorFor(m => m.Type)
            .WithErrorMessage("Type must be one of the available options.");

    [Theory]
    [ClassData(typeof(ActivityTypeConstantTheoryData))]
    public void Type_WhenAnActivityTypeConstant_IsValid(string testActivityTypeConstant) =>
        _validator.TestValidate(new AddActivityModel() { Type = testActivityTypeConstant })
            .ShouldNotHaveValidationErrorFor(m => m.Type);

    [Fact]
    public void Description_WhenDefault_IsValid() =>
        _validator.TestValidate(new AddActivityModel())
            .ShouldNotHaveValidationErrorFor(m => m.Description);

    [Fact]
    public void Date_WhenDefault_IsInvalid() =>
        _validator.TestValidate(new AddActivityModel())
            .ShouldHaveValidationErrorFor(m => m.Date)
            .WithErrorMessage("Date is required.");

    [Fact]
    public void Date_WhenIsTheCurentDate_IsValid() =>
        _validator.TestValidate(new AddActivityModel() { Date = _dateTimeProvider.Now })
            .ShouldNotHaveValidationErrorFor(m => m.Date);

    [Fact]
    public void Date_WhenIsFutureDate_IsValid() =>
        _validator.TestValidate(new AddActivityModel() { Date = _dateTimeProvider.Now.AddDays(1) })
            .ShouldNotHaveValidationErrorFor(m => m.Date);

    [Fact]
    public void Date_WhenIsPastDate_IsInvalid() =>
        _validator.TestValidate(new AddActivityModel() { Date = _dateTimeProvider.Now.AddDays(-1) })
            .ShouldHaveValidationErrorFor(m => m.Date)
            .WithErrorMessage("Date can't be in the past.");
}

internal class ActivityTypeConstantTheoryData : TheoryData<string>
{
    public ActivityTypeConstantTheoryData()
    {
        foreach (var activityTypeConstant in ActivityTypeConstants.AllActivityTypes)
            Add(activityTypeConstant);
    }
}
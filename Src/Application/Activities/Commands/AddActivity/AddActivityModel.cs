using Application.Common.Interfaces;
using Domain.Constants;
using FluentValidation;

namespace Application.Activities.Commands.AddActivity;

public sealed record AddActivityModel
{
    public string Name { get; set; }
    public string Type { get; set; }
    public string Description { get; set; }
    public DateTime? Date { get; set; }
}

public sealed class AddActivityModelValidator : AbstractValidator<AddActivityModel>
{
    public AddActivityModelValidator(IDateTimeProvider dateTimeProvider)
    {
        RuleFor(m => m.Name)
            .NotEmpty()
            .WithMessage("Name is required.");

        RuleFor(m => m.Type)
            .NotEmpty()
            .WithMessage("Type is required.");

        RuleFor(m => m.Type)
            .Must((model, type) => ActivityTypeConstants.AllActivityTypes.Contains(type))
            .WithMessage("Type must be one of the available options.");

        RuleFor(m => m.Date)
            .NotEmpty()
            .WithMessage("Date is required.");

        RuleFor(m => m.Date)
            .GreaterThanOrEqualTo(dateTimeProvider.Now)
            .WithMessage("Date can't be in the past.");
    }
}
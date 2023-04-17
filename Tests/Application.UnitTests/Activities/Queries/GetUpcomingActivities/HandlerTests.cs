using Application.Activities.Queries.GetUpcomingActivities;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Models;
using FluentAssertions;
using Xunit;

namespace Application.UnitTests.Activities.Queries.GetUpcomingActivities;

public sealed class HandlerTests
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly GetUpcomingActivitiesQuery.GetUpcomingActivitiesQueryHandler _handler;

    public HandlerTests()
    {
        var applicationTestFixture = new ApplicationTestFixture();
        _dateTimeProvider = applicationTestFixture.DateTimeProvider;
        _mapper = applicationTestFixture.Mapper;
        _applicationDbContext = applicationTestFixture.ApplicationDbContext;
        _handler = new GetUpcomingActivitiesQuery.GetUpcomingActivitiesQueryHandler(_dateTimeProvider, _mapper, _applicationDbContext);
    }

    [Fact]
    public async Task ReturnsAllActivites_HavingDateGreaterThanOrEqualTo_DateTimeProviderNow_ProjectedTo_UpcomingActivityDtos()
    {
        var testActivities = new List<Activity>()
        {
            new Activity()
            {
                Name = Guid.NewGuid().ToString(),
                Description = Guid.NewGuid().ToString(),
                Type = Guid.NewGuid().ToString(),
                Date = _dateTimeProvider.Now
            },
            new Activity()
            {
                Name = Guid.NewGuid().ToString(),
                Description = Guid.NewGuid().ToString(),
                Type = Guid.NewGuid().ToString(),
                Date = _dateTimeProvider.Now.AddDays(1)
            },
            new Activity()
            {
                Name = Guid.NewGuid().ToString(),
                Description = Guid.NewGuid().ToString(),
                Type = Guid.NewGuid().ToString(),
                Date = _dateTimeProvider.Now.AddYears(1)
            }
        };
        _applicationDbContext.Activities.AddRange(testActivities);
        await _applicationDbContext.SaveChangesAsync();

        var expectedDtos = testActivities
            .Select(a => new UpcomingActivityDto()
            {
                ActivityId = a.ActivityId,
                Name = a.Name,
                Description = a.Description,
                Type = a.Type,
                Date = a.Date,
            });

        (await _handler.Handle(new GetUpcomingActivitiesQuery()))
            .Should()
            .BeEquivalentTo(expectedDtos);
    }

    [Fact]
    public async Task ReturnsNoActivities_HavingDateLessThan_DateTimeProviderNow()
    {
        var testActivities = new List<Activity>()
        {
            new Activity()
            {
                Name = Guid.NewGuid().ToString(),
                Description = Guid.NewGuid().ToString(),
                Type = Guid.NewGuid().ToString(),
                Date = _dateTimeProvider.Now.AddDays(-1)
            },
            new Activity()
            {
                Name = Guid.NewGuid().ToString(),
                Description = Guid.NewGuid().ToString(),
                Type = Guid.NewGuid().ToString(),
                Date = _dateTimeProvider.Now.AddYears(-1)
            }
        };
        _applicationDbContext.Activities.AddRange(testActivities);
        await _applicationDbContext.SaveChangesAsync();

        (await _handler.Handle(new GetUpcomingActivitiesQuery()))
            .Should()
            .BeEmpty();
    }
}
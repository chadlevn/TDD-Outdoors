using Application.Activities.Queries.GetUpcomingActivities;
using Domain.Models;
using FluentAssertions;
using Xunit;

namespace Application.UnitTests.Activities.Queries.GetUpcomingActivities;

public sealed class MapperTests
{
    private readonly Activity _testActivity;
    private readonly UpcomingActivityDto _mappedUpcomingActivityDto;

    public MapperTests()
    {
        _testActivity = new Activity()
        {
            ActivityId = int.MaxValue,
            Name = Guid.NewGuid().ToString(),
            Description = Guid.NewGuid().ToString(),
            Type = Guid.NewGuid().ToString(),
            Date = new DateTime(3000, 1, 1)
        };

        var mapper = new ApplicationTestFixture().Mapper;

        _mappedUpcomingActivityDto = mapper.Map<UpcomingActivityDto>(_testActivity);
    }

    [Fact]
    public void ActivityId_MapsFrom_ActivityId() =>
        _mappedUpcomingActivityDto.ActivityId.Should().Be(_testActivity.ActivityId);

    [Fact]
    public void Name_MapsFrom_Name() =>
        _mappedUpcomingActivityDto.Name.Should().Be(_testActivity.Name);

    [Fact]
    public void Description_MapsFrom_Description() =>
        _mappedUpcomingActivityDto.Description.Should().Be(_testActivity.Description);

    [Fact]
    public void Type_MapFrom_Type() =>
        _mappedUpcomingActivityDto.Type.Should().Be(_testActivity.Type);

    [Fact]
    public void Date_MapsFrom_Date() =>
        _mappedUpcomingActivityDto.Date.Should().Be(_testActivity.Date);
}
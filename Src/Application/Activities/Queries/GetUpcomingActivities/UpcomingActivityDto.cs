using Application.Common.Mappings;
using AutoMapper;
using Domain.Models;

namespace Application.Activities.Queries.GetUpcomingActivities;

public sealed record UpcomingActivityDto : IMappingProfile
{
    public int ActivityId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }
    public DateTime Date { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Activity, UpcomingActivityDto>();
    }
}
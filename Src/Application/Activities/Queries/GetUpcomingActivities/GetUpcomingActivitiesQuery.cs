using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Activities.Queries.GetUpcomingActivities;

public sealed record GetUpcomingActivitiesQuery : IRequest<IReadOnlyCollection<UpcomingActivityDto>>
{
    internal sealed record GetUpcomingActivitiesQueryHandler : IRequestHandler<GetUpcomingActivitiesQuery, IReadOnlyCollection<UpcomingActivityDto>>
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _applicationDbContext;

        public GetUpcomingActivitiesQueryHandler(IDateTimeProvider dateTimeProvider, IMapper mapper, IApplicationDbContext applicationDbContext)
        {
            _dateTimeProvider = dateTimeProvider;
            _mapper = mapper;
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IReadOnlyCollection<UpcomingActivityDto>> Handle(GetUpcomingActivitiesQuery request, CancellationToken cancellationToken = default)
        {
            var upcomingActivities = await _applicationDbContext.Activities
                .Where(a => a.Date.Date >= _dateTimeProvider.Now.Date)
                .ProjectTo<UpcomingActivityDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return upcomingActivities;
        }
    }
}
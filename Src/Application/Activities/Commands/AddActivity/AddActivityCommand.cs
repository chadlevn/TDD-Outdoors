using Application.Common.Interfaces;
using Domain.Models;
using MediatR;

namespace Application.Activities.Commands.AddActivity;

public sealed record AddActivityCommand(AddActivityModel Model) : IRequest<int>
{
    internal sealed record AddActivityCommandHandler : IRequestHandler<AddActivityCommand, int>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public AddActivityCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<int> Handle(AddActivityCommand request, CancellationToken cancellationToken = default)
        {
            var newActivity = new Activity()
            {
                Name = request.Model.Name,
                Type = request.Model.Type,
                Description = request.Model.Description,
                Date = request.Model.Date.Value
            };
            _applicationDbContext.Activities.Add(newActivity);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return newActivity.ActivityId;
        }
    }
}
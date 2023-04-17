using Application.Common.Interfaces;
using Application.Common.Mappings;
using AutoMapper;
using Infrastructure;
using Moq;

namespace Application.UnitTests;

public sealed class ApplicationTestFixture : IDisposable
{
    public ApplicationDbContext ApplicationDbContext { get; }
    public IDateTimeProvider DateTimeProvider { get; }
    public IMapper Mapper { get; }

    public ApplicationTestFixture()
    {
        ApplicationDbContext = ApplicationDbContextFactory.Create();

        var mockDateTime = new Mock<IDateTimeProvider>();
        mockDateTime.Setup(dtp => dtp.Now).Returns(new DateTime(3000, 1, 1));
        DateTimeProvider = mockDateTime.Object;

        var configurationProvider = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });

        Mapper = configurationProvider.CreateMapper();
    }

    public void Dispose()
    {
        ApplicationDbContextFactory.Destroy(ApplicationDbContext);
    }
}
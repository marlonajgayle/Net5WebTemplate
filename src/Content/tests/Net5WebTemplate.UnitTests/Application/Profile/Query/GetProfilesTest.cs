using Net5WebTemplate.Application.Common.Models;
using Net5WebTemplate.Application.Profiles.Queries.GetProfiles;
using Net5WebTemplate.Persistence;
using Net5WebTemplate.UnitTests.Common;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Net5WebTemplate.UnitTests.Application.Profile.Query
{
    public class GetProfilesTest
    {
        public Net5WebTemplateDbContext context = Net5WebTemplatesDbContextFactory.Create();

        [Fact]
        public async Task ShouldGetProfiles()
        {
            //Arrange                                                      
            var sut = new GetProfilesQueryHandler(context);

            // Act
            var handleResult = await sut.Handle(new GetProfilesQuery { }
            , CancellationToken.None);

            // Assert
            Assert.IsType<PaginatedList<Net5WebTemplate.Application.Profiles.Queries.GetProfiles.ProfileDto>>(handleResult);

            //Clean up
            Net5WebTemplatesDbContextFactory.Destroy(context);
        }
    }
}

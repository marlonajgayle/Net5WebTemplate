using Microsoft.EntityFrameworkCore;
using Moq;
using Net5WebTemplate.Application.Common.Interfaces;
using Net5WebTemplate.Application.Profiles.Queries.GetProfiles;
using Net5WebTemplate.Persistence;
using Net5WebTemplate.UnitTests.Common;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
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
            Assert.IsType<List<Net5WebTemplate.Application.Profiles.Queries.GetProfiles.ProfileDto>> (handleResult);

            //Clean up
            Net5WebTemplatesDbContextFactory.Destroy(context);
    }
    }
}

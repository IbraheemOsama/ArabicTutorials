using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArabicTutorials.Common;
using ArabicTutorials.Queries.Controllers;
using NSubstitute;
using Xunit;

namespace ArabicTutorials.Queries.UnitTests.Controllers
{
    public class VideosControllerTests
    {
        //[UnitOfWorkName]_[ScenarioUnderTest]_[ExpectedBehavior]
        [Fact]
        public void GetAll_ReturnResults()
        {
            // Arrange
            var logger = Substitute.For<ILogger>();
            var controller = new VideosController(logger);

            // Act
            var result = controller.GetAll();

            //Assert
            Assert.NotNull(result);
            Assert.NotEqual(0, result.Count());
        }
    }
}

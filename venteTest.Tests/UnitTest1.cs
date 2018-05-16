using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using venteTest.Controllers;
using venteTest.Models;
using venteTest.Services;
using Xunit;

namespace venteTest.Tests
{


    //packages to add:
    //    <PackageReference Include = "Microsoft.AspNetCore.All" Version="2.0.0" />
    //    <PackageReference Include = "Microsoft.AspNetCore.TestHost" Version="2.0.0" />
    //    <PackageReference Include = "FluentAssertions" Version="4.19.2" />
    //    <PackageReference Include = "Moq" Version="4.7.63" />

    public class UnitTest1
    {
        //[Fact]
        //public void HomeTest1()
        //{
        //    HomeController sut = new venteTest.Controllers.HomeController();

        //    IActionResult result = sut.Contact();

        //    Assert.IsType<ViewResult>(result);
        //}

        //[Fact]
        //public void HomeTest2()
        //{
        //    HomeController sut = new venteTest.Controllers.HomeController();

        //    IActionResult result = sut.Faq();

        //    Assert.IsType<ViewResult>(result);
        //}
        //[Fact]
        //public void HomeTest3()
        //{
        //    HomeController sut = new venteTest.Controllers.HomeController();

        //    IActionResult result = sut.Sent();

        //    Assert.IsType<ViewResult>(result);
        //}

        ////THIS TEST WILL FAIL ON PURPOSE
        //[Fact]
        //public void HomeTest4()
        //{
        //    HomeController sut = new venteTest.Controllers.HomeController();

        //    IActionResult result = sut.Error();

        //    Assert.IsType<ViewResult>(result);
        //}

        //THIS TEST WILL FAIL ON PURPOSE
        [Fact]
        public void AdminTest1()
        {
            AdminController sut = new venteTest.Controllers.AdminController();

            IActionResult result = (IActionResult)sut.Index();

            Assert.IsType<ViewResult>(result);
        }

        //[Fact]
        //public async Task Create_ReturnsBadRequest_GivenInvalidModel()
        //{
        //    // Arrange & Act
        //    var mockRepo = new Mock<IBrainstormSessionRepository>();
        //    var controller = new IdeasController(mockRepo.Object);
        //    controller.ModelState.AddModelError("error", "some error");

        //    // Act
        //    var result = await controller.Create(model: null);

        //    // Assert
        //    Assert.IsType<BadRequestObjectResult>(result);
        //}

        //[Fact]
        //public async Task Create_ReturnsHttpNotFound_ForInvalidSession()
        //{
        //    // Arrange
        //    int testSessionId = 123;
        //    var mockRepo = new Mock<IBrainstormSessionRepository>();
        //    mockRepo.Setup(repo => repo.GetByIdAsync(testSessionId))
        //        .Returns(Task.FromResult((BrainstormSession)null));
        //    var controller = new IdeasController(mockRepo.Object);

        //    // Act
        //    var result = await controller.Create(new NewIdeaModel());

        //    // Assert
        //    Assert.IsType<NotFoundObjectResult>(result);
        //}

        //[Fact]
        //public async Task Create_ReturnsNewlyCreatedIdeaForSession()
        //{
        //    // Arrange
        //    int testSessionId = 123;
        //    string testName = "test name";
        //    string testDescription = "test description";
        //    var testSession = GetTestSession();
        //    var mockRepo = new Mock<IBrainstormSessionRepository>();
        //    mockRepo.Setup(repo => repo.GetByIdAsync(testSessionId))
        //        .Returns(Task.FromResult(testSession));
        //    var controller = new IdeasController(mockRepo.Object);

        //    var newIdea = new NewIdeaModel()
        //    {
        //        Description = testDescription,
        //        Name = testName,
        //        SessionId = testSessionId
        //    };
        //    mockRepo.Setup(repo => repo.UpdateAsync(testSession))
        //        .Returns(Task.CompletedTask)
        //        .Verifiable();

        //    // Act
        //    var result = await controller.Create(newIdea);

        //    // Assert
        //    var okResult = Assert.IsType<OkObjectResult>(result);
        //    var returnSession = Assert.IsType<BrainstormSession>(okResult.Value);
        //    mockRepo.Verify();
        //    Assert.Equal(2, returnSession.Ideas.Count());
        //    Assert.Equal(testName, returnSession.Ideas.LastOrDefault().Name);
        //    Assert.Equal(testDescription, returnSession.Ideas.LastOrDefault().Description);
        //}

        //private BrainstormSession GetTestSession()
        //{
        //    var session = new BrainstormSession()
        //    {
        //        DateCreated = new DateTime(2016, 7, 2),
        //        Id = 1,
        //        Name = "Test One"
        //    };

        //    var idea = new Idea() { Name = "One" };
        //    session.AddIdea(idea);
        //    return session;
        //}
    }
}

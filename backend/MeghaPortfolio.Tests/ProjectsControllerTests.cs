using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using MeghaPortfolio.API.Controllers;
using MeghaPortfolio.API.Core.Application.DTOs;
using MeghaPortfolio.API.Core.Application.Interfaces;

namespace MeghaPortfolio.Tests;

[TestFixture]
public class ProjectsControllerTests
{
    private Mock<IPortfolioService> _mockService;
    private ProjectsController _controller;

    [SetUp]
    public void Setup()
    {
        _mockService = new Mock<IPortfolioService>();
        _controller = new ProjectsController(_mockService.Object);
    }

    [Test]
    public async Task GetProjects_ShouldReturnOkObjectResult_WithProjectList()
    {
        // Arrange
        var sampleProjects = new List<ProjectDto>
        {
            new ProjectDto { Id = 1, Title = "SmartStore — Enterprise Microservices Platform", IsFlagship = true }
        };

        _mockService.Setup(s => s.GetProjectsAsync(It.IsAny<CancellationToken>()))
                    .ReturnsAsync(sampleProjects);

        // Act
        var actionResult = await _controller.GetProjects(CancellationToken.None);

        // Assert
        Assert.That(actionResult.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = actionResult.Result as OkObjectResult;
        Assert.That(okResult, Is.Not.Null);

        var projects = okResult!.Value as IEnumerable<ProjectDto>;
        Assert.That(projects, Is.Not.Null);
        Assert.That(projects!.Count(), Is.EqualTo(1));
    }

    [Test]
    public async Task GetProjectById_ShouldReturnNotFound_WhenProjectDoesNotExist()
    {
        // Arrange
        _mockService.Setup(s => s.GetProjectByIdAsync(999, It.IsAny<CancellationToken>()))
                    .ReturnsAsync((ProjectDto?)null);

        // Act
        var actionResult = await _controller.GetProjectById(999, CancellationToken.None);

        // Assert
        Assert.That(actionResult.Result, Is.InstanceOf<NotFoundObjectResult>());
    }
}

using Moq;
using NUnit.Framework;
using MeghaPortfolio.API.Core.Application.DTOs;
using MeghaPortfolio.API.Core.Application.Interfaces;
using MeghaPortfolio.API.Core.Application.Services;
using MeghaPortfolio.API.Core.Domain.Entities;

namespace MeghaPortfolio.Tests;

[TestFixture]
public class PortfolioServiceTests
{
    private Mock<IPortfolioRepository> _mockRepo;
    private PortfolioService _service;

    [SetUp]
    public void Setup()
    {
        _mockRepo = new Mock<IPortfolioRepository>();
        _service = new PortfolioService(_mockRepo.Object);
    }

    [Test]
    public async Task GetProfileAsync_ShouldReturnMappedProfileDto_WhenProfileExists()
    {
        // Arrange (AAA Pattern)
        var sampleProfile = new ProfileEntity
        {
            Id = 1,
            FullName = "Megha Syam Reddy Badhuri",
            Title = "Senior .NET & Backend Engineer",
            Summary = "Senior .NET Engineer with 5 YOE",
            LatencyReductionMetric = "95% Reduction",
            TestCoverageMetric = "85%+ NUnit Coverage"
        };

        _mockRepo.Setup(r => r.GetProfileAsync(It.IsAny<CancellationToken>()))
                 .ReturnsAsync(sampleProfile);

        // Act
        var result = await _service.GetProfileAsync();

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result!.FullName, Is.EqualTo("Megha Syam Reddy Badhuri"));
        Assert.That(result.Title, Is.EqualTo("Senior .NET & Backend Engineer"));
        Assert.That(result.LatencyReductionMetric, Is.EqualTo("95% Reduction"));
        _mockRepo.Verify(r => r.GetProfileAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task GetProjectsAsync_ShouldReturnProjectsList_WithFlagshipFirst()
    {
        // Arrange
        var projects = new List<ProjectEntity>
        {
            new ProjectEntity { Id = 1, Title = "SmartStore Platform", IsFlagship = true },
            new ProjectEntity { Id = 2, Title = "Admin Portal", IsFlagship = false }
        };

        _mockRepo.Setup(r => r.GetProjectsAsync(It.IsAny<CancellationToken>()))
                 .ReturnsAsync(projects);

        // Act
        var results = await _service.GetProjectsAsync();

        // Assert
        Assert.That(results, Is.Not.Null);
        var projectList = results.ToList();
        Assert.That(projectList.Count, Is.EqualTo(2));
        Assert.That(projectList.First().Title, Is.EqualTo("SmartStore Platform"));
        Assert.That(projectList.First().IsFlagship, Is.True);
    }

    [Test]
    public async Task SubmitContactMessageAsync_ShouldPersistMessageAndReturnSuccessStatus()
    {
        // Arrange
        var createDto = new ContactMessageCreateDto
        {
            SenderName = "Hiring Manager",
            SenderEmail = "hiring@enterprise.com",
            Subject = "Senior .NET Engineer Role",
            Message = "Impressive architecture and microservices background."
        };

        var savedEntity = new ContactMessageEntity
        {
            Id = 101,
            SenderName = createDto.SenderName,
            SenderEmail = createDto.SenderEmail,
            Subject = createDto.Subject,
            Message = createDto.Message,
            CreatedAt = DateTime.UtcNow,
            IsRead = false
        };

        _mockRepo.Setup(r => r.AddContactMessageAsync(It.IsAny<ContactMessageEntity>(), It.IsAny<CancellationToken>()))
                 .ReturnsAsync(savedEntity);

        // Act
        var response = await _service.SubmitContactMessageAsync(createDto);

        // Assert
        Assert.That(response, Is.Not.Null);
        Assert.That(response.Id, Is.EqualTo(101));
        Assert.That(response.SenderEmail, Is.EqualTo("hiring@enterprise.com"));
        Assert.That(response.Status, Is.EqualTo("Message Received Successfully"));
    }
}

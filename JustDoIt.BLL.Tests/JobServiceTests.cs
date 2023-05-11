using AutoMapper;
using JustDoIt.BLL.Implementations;
using JustDoIt.BLL.Implementations.Services;
using JustDoIt.BLL.Models.Response;
using JustDoIt.DAL.Entities.Response;
using JustDoIt.DAL.Interfaces;
using JustDoIt.Shared;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace JustDoIt.BLL.Tests;

[TestClass]
public class JobServiceTests
{
    private readonly IMapper _mockMapper;

    public JobServiceTests()
    {
        var mockMapperConfiguration = new MapperConfiguration(
            configure => configure.AddProfile(new BllMappingProfile()));

        _mockMapper = mockMapperConfiguration.CreateMapper();
    }

    [TestMethod]
    public async Task GetByCategory_Returns_Collection_Of_JobModelResponse()
    {
        // Arrange
        var storageFactory = new Mock<IRepositoryFactory>();
        var categoryRepositoryMock = new Mock<ICategoryRepository>();
        var jobRepositoryMock = new Mock<IJobRepository>();

        var categoryId = Guid.Parse("0E984725-C41C-4BF4-9960-E1C80E27ABA0");
        var category = new CategoryEntityResponse { Id = categoryId };

        var jobsDllResponse = new List<JobEntityResponse>
        {
            new() { Id = It.IsAny<Guid>(), CategoryId = categoryId, Name = "Task1" },
            new() { Id = It.IsAny<Guid>(), CategoryId = categoryId, Name = "Task2" },
            new() { Id = It.IsAny<Guid>(), CategoryId = categoryId, Name = "Task3" }
        };

        var jobsBllResponse = new List<JobModelResponse>
        {
            new() { Id = It.IsAny<Guid>(), CategoryId = categoryId, Name = "Task1" },
            new() { Id = It.IsAny<Guid>(), CategoryId = categoryId, Name = "Task2" },
            new() { Id = It.IsAny<Guid>(), CategoryId = categoryId, Name = "Task3" }
        };

        categoryRepositoryMock.Setup(repo => repo.GetOneById(categoryId)).ReturnsAsync(category);
        jobRepositoryMock.Setup(repo => repo.GetByCategory(categoryId, It.IsAny<bool>())).ReturnsAsync(jobsDllResponse);

        storageFactory.Setup(repo => repo.GetJobRepository(It.IsAny<StorageType>()))
            .Returns(jobRepositoryMock.Object);
        storageFactory.Setup(repo => repo.GetCategoryRepository(It.IsAny<StorageType>()))
            .Returns(categoryRepositoryMock.Object);

        var jobService = new JobService(storageFactory.Object, _mockMapper);

        // Act
        var actualJobs = await jobService.GetByCategory(categoryId, It.IsAny<StorageType>());

        // Assert
        Assert.AreEqual(actualJobs.Count, jobsBllResponse.Count);
    }

    [TestMethod]
    public async Task GetByCategory_Returns_Valid_Type()
    {
        // Arrange
        var storageFactory = new Mock<IRepositoryFactory>();
        var categoryRepositoryMock = new Mock<ICategoryRepository>();
        var jobRepositoryMock = new Mock<IJobRepository>();

        var categoryId = Guid.Parse("0E984725-C41C-4BF4-9960-E1C80E27ABA0");
        var category = new CategoryEntityResponse { Id = categoryId };

        var jobsDllResponse = new List<JobEntityResponse>
        {
            new() { Id = It.IsAny<Guid>(), CategoryId = categoryId, Name = "Task1" },
            new() { Id = It.IsAny<Guid>(), CategoryId = categoryId, Name = "Task2" },
            new() { Id = It.IsAny<Guid>(), CategoryId = categoryId, Name = "Task3" }
        };

        categoryRepositoryMock.Setup(repo => repo.GetOneById(categoryId)).ReturnsAsync(category);
        jobRepositoryMock.Setup(repo => repo.GetByCategory(categoryId, It.IsAny<bool>())).ReturnsAsync(jobsDllResponse);

        storageFactory.Setup(repo => repo.GetJobRepository(It.IsAny<StorageType>()))
            .Returns(jobRepositoryMock.Object);
        storageFactory.Setup(repo => repo.GetCategoryRepository(It.IsAny<StorageType>()))
            .Returns(categoryRepositoryMock.Object);

        var jobService = new JobService(storageFactory.Object, _mockMapper);

        // Act
        var actualJobs = await jobService.GetByCategory(categoryId, It.IsAny<StorageType>());

        // Assert
        Assert.IsInstanceOfType(actualJobs, typeof(ICollection<JobModelResponse>));
    }

    [TestMethod]
    public async Task GetByCategory_Throws_ArgumentNullException()
    {
        // Arrange
        var storageFactory = new Mock<IRepositoryFactory>();
        var categoryRepositoryMock = new Mock<ICategoryRepository>();

        var categoryId = Guid.Parse("0E984725-C41C-4BF4-9960-E1C80E27ABA0");

        categoryRepositoryMock.Setup(repo => repo.GetOneById(categoryId))!.ReturnsAsync(null as CategoryEntityResponse);

        storageFactory.Setup(repo => repo.GetCategoryRepository(It.IsAny<StorageType>()))
            .Returns(categoryRepositoryMock.Object);

        var jobService = new JobService(storageFactory.Object, _mockMapper);

        // Act
        // Assert
        await Assert.ThrowsExceptionAsync<ArgumentNullException>(() =>
            jobService.GetByCategory(categoryId, It.IsAny<StorageType>()));
    }

    [TestMethod]
    public async Task Remove_Deletes_Job()
    {
        // Arrange
        var storageFactory = new Mock<IRepositoryFactory>();
        var jobRepositoryMock = new Mock<IJobRepository>();

        var jobId = Guid.Parse("0E984725-C41C-4BF4-9960-E1C80E27ABA0");
        var job = new JobEntityResponse { Id = jobId, CategoryId = It.IsAny<Guid>(), Name = "Task1" };
        var jobsDll = new List<JobEntityResponse> { job };

        jobRepositoryMock.Setup(repo => repo.GetOneById(jobId)).ReturnsAsync(job);
        jobRepositoryMock.Setup(repo => repo.Remove(jobId)).Callback((Guid id) => jobsDll.Remove(job));

        var jobService = new JobService(storageFactory.Object, _mockMapper);

        storageFactory.Setup(repo => repo.GetJobRepository(It.IsAny<StorageType>()))
            .Returns(jobRepositoryMock.Object);

        // Act
        await jobService.Remove(jobId, It.IsAny<StorageType>());

        // Assert
        Assert.IsNull(jobsDll.FirstOrDefault(j => j.Id == jobId));
    }

    [TestMethod]
    public async Task Check_Changes_Job()
    {
        // Arrange
        var storageFactory = new Mock<IRepositoryFactory>();
        var jobRepositoryMock = new Mock<IJobRepository>();

        var jobId = Guid.Parse("0E984725-C41C-4BF4-9960-E1C80E27ABA0");
        var isCompleted = true;

        var job = new JobEntityResponse
            { Id = jobId, CategoryId = It.IsAny<Guid>(), Name = "Task1", IsCompleted = isCompleted };

        jobRepositoryMock.Setup(repo => repo.GetOneById(jobId)).ReturnsAsync(job);
        jobRepositoryMock.Setup(repo => repo.Check(jobId)).Callback((Guid id) => job.IsCompleted = !isCompleted);
        jobRepositoryMock.Setup(repo => repo.Uncheck(jobId)).Callback((Guid id) => job.IsCompleted = !isCompleted);

        storageFactory.Setup(repo => repo.GetJobRepository(It.IsAny<StorageType>()))
            .Returns(jobRepositoryMock.Object);

        var jobService = new JobService(storageFactory.Object, _mockMapper);

        // Act
        await jobService.Check(jobId, It.IsAny<StorageType>());

        // Assert
        Assert.AreEqual(job.IsCompleted, !isCompleted);
    }

    [TestMethod]
    public async Task Check_Throws_ArgumentNullException()
    {
        // Arrange
        var storageFactory = new Mock<IRepositoryFactory>();
        var jobRepositoryMock = new Mock<IJobRepository>();
        var jobId = Guid.Parse("0E984725-C41C-4BF4-9960-E1C80E27ABA0");

        jobRepositoryMock.Setup(repo => repo.GetOneById(jobId))!.ReturnsAsync(null as JobEntityResponse);
        storageFactory.Setup(repo => repo.GetJobRepository(It.IsAny<StorageType>()))
            .Returns(jobRepositoryMock.Object);

        var jobService = new JobService(storageFactory.Object, _mockMapper);

        // Act
        // Assert
        await Assert.ThrowsExceptionAsync<ArgumentNullException>(() =>
            jobService.Check(jobId, It.IsAny<StorageType>()));
    }
}
using AutoMapper;
using JustDoIt.BLL.Implementations;
using JustDoIt.BLL.Implementations.Services;
using JustDoIt.BLL.Models.Request;
using JustDoIt.DAL.Entities.Request;
using JustDoIt.DAL.Entities.Response;
using JustDoIt.DAL.Interfaces;
using JustDoIt.Shared;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace JustDoIt.BLL.Tests;

[TestClass]
public class CategoryServiceTests
{
    private readonly IMapper _mockMapper;

    public CategoryServiceTests()
    {
        var mockMapperConfiguration = new MapperConfiguration(
            configure => configure.AddProfile(new BllMappingProfile()));

        _mockMapper = mockMapperConfiguration.CreateMapper();
    }

    [TestMethod]
    public async Task Add_Throws_ArgumentException()
    {
        // Arrange
        var categoryRepositoryFactory = new Mock<Func<StorageType, ICategoryRepository>(repositoryTyo);
        var categoryRepositoryMock = categoryRepositoryFactory.Object(It.IsAny<StorageType>());

        var categoryId = Guid.Parse("0E984725-C41C-4BF4-9960-E1C80E27ABA0");
        var categoryName = "Task1";

        var categoryRequest = new CategoryModelRequest { Name = categoryName };
        var category = new CategoryEntityResponse { Id = categoryId };

        categoryRepositoryFactory.Setup(repo => repo.GetOneByName(categoryName)).ReturnsAsync(category);
        categoryFactory.Setup(repo => repo.GetCategoryRepository(It.IsAny<StorageType>()))
            .Returns(categoryRepositoryMock.Object);

        var categoryService = new CategoryService(categoryFactory.Object, _mockMapper);

        // Act
        // Assert
        await Assert.ThrowsExceptionAsync<ArgumentException>(() =>
            categoryService.Add(categoryRequest, It.IsAny<StorageType>()));
    }

    [TestMethod]
    public async Task Add_Inserts_Category()
    {
        // Arrange
        var storageFactory = new Mock<IRepositoryFactory>();
        var categoryRepositoryMock = new Mock<ICategoryRepository>();

        var categoryName = "Task1";

        var categoryEntityRequest = new CategoryEntityRequest { Name = categoryName };
        var categoryModelRequest = new CategoryModelRequest { Name = categoryName };

        var categories = new List<CategoryEntityRequest>();

        categoryRepositoryMock.Setup(repo => repo.GetOneByName(categoryName))
            .ReturnsAsync(null as CategoryEntityResponse);
        categoryRepositoryMock.Setup(repo => repo.Add(It.IsAny<CategoryEntityRequest>()))
            .Callback((CategoryEntityRequest c) => categories.Add(categoryEntityRequest));

        storageFactory.Setup(repo => repo.GetCategoryRepository(It.IsAny<StorageType>()))
            .Returns(categoryRepositoryMock.Object);

        var categoryService = new CategoryService(storageFactory.Object, _mockMapper);

        // Act
        await categoryService.Add(categoryModelRequest, It.IsAny<StorageType>());

        // Assert
        Assert.IsNotNull(categories.FirstOrDefault(c => c.Name == categoryName));
    }

    [TestMethod]
    public async Task Remove_Deletes_Category()
    {
        // Arrange
        var storageFactory = new Mock<IRepositoryFactory>();
        var categoryRepositoryMock = new Mock<ICategoryRepository>();

        var categoryId = Guid.Parse("0E984725-C41C-4BF4-9960-E1C80E27ABA0");

        var category = new CategoryEntityResponse { Id = categoryId };
        var categoriesDll = new List<CategoryEntityResponse> { category };

        categoryRepositoryMock.Setup(repo => repo.GetOneById(categoryId)).ReturnsAsync(category);
        categoryRepositoryMock.Setup(repo => repo.Remove(categoryId))
            .Callback((Guid id) => categoriesDll.Remove(category));

        storageFactory.Setup(repo => repo.GetCategoryRepository(It.IsAny<StorageType>()))
            .Returns(categoryRepositoryMock.Object);

        var categoryService = new CategoryService(storageFactory.Object, _mockMapper);

        // Act
        await categoryService.Remove(categoryId, It.IsAny<StorageType>());

        // Assert
        Assert.IsNull(categoriesDll.FirstOrDefault(c => c.Id == categoryId));
    }
}
using BookAPI.Controllers;
using BookAPI.Models.Entities;
using BookAPI.Service.AuthorServices;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BookAPITests;
public class AuthorControllerTests {
    private readonly Mock<IAuthorService> _mock;
    private readonly AuthorController _controller;

    public AuthorControllerTests() {
        _mock = new Mock<IAuthorService>();
        _controller = new AuthorController(_mock.Object);
    }

    private static List<Author> GetTestAuthors() {
        return new List<Author> {
            new() {
                AuthorId = 1,
                FirstName = "Jack",
                LastName = "Author"
            },
            new() {
                AuthorId = 2,
                FirstName = "Sheila",
                LastName = "Auteur"
            },
            new() {
                AuthorId = 3,
                FirstName = "Authmanda",
                LastName = "Tor"
            },
            new() {
                AuthorId = 4,
                FirstName = "Authin",
                LastName = "Eur"
            },
        };

    }

    // GET
    [Fact]
    public async Task GetAll_ReturnsCorrectType() {
        // Arrange
        _mock.Setup(service => service.GetAll()).ReturnsAsync(GetTestAuthors);

        // Act
        var result = await _controller.GetAll();

        // Assert
        Assert.IsType<List<Author>>(result);
        if (result != null) Assert.Equal(4, result.Count); // Not necessary for 100% coverage.
    }

    [Fact]
    public async void Get_ReturnsAuthor_WhenAuthorExists() {
        // Arrange
        var author = new Author {
            AuthorId = 1,
            FirstName = "Jack",
            LastName = "Author"
        };
        _mock.Setup(service => service.Get(1)).ReturnsAsync(author);

        // Act
        var result = await _controller.Get(1) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        if (result != null) Assert.Equal(author, result.Value);
    }

    [Fact]
    public async void Get_ReturnsNotFound_WhenAuthorDoesNotExist() {
        // Arrange
        _mock.Setup(service => service.Get(1)).ReturnsAsync(null as Author);

        // Act
        var result = await _controller.Get(1);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }

    //  DELETE
    [Fact]
    public async void Delete_ReturnsOk_WhenAuthorExists() {
        // Arrange
        var author = new Author {
            AuthorId = 1,
            FirstName = "Jack",
            LastName = "Author"
        };
        _mock.Setup(service => service.Get(1)).ReturnsAsync(author);
        _mock.Setup(service => service.Delete(1));

        // Act
        var result = await _controller.Delete(1) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        if (result != null) Assert.Equal($"Author with id: {author.AuthorId} has been deleted", result.Value);
    }

    [Fact]
    public async void Delete_ReturnsNotFound_WhenAuthorDoesNotExist() {
        // Arrange
        _mock.Setup(service => service.Get(1)).ReturnsAsync(null as Author);

        // Act
        var result = await _controller.Delete(1);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }
}
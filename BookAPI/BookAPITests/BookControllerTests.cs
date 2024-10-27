using BookAPI.Controllers;
using BookAPI.Models.Entities;
using BookAPI.Service.AuthorServices;
using BookAPI.Service.BookServices;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BookAPITests;
public class BookControllerTests {
    private readonly Mock<IBookService> _mock;
    private readonly Mock<IAuthorService> _mockAuthor;
    private readonly BookController _controller;

    public BookControllerTests() {
        _mock = new Mock<IBookService>();
        _mockAuthor = new Mock<IAuthorService>();
        _controller = new BookController(_mock.Object, _mockAuthor.Object);
    }

    private static List<Book> GetTestBooks() {
        return new List<Book> {
            new() {
                BookId = 1,
                Title = "A Great Book",
                Description = "One for the ages.",
                Year = 2024,
                AuthorId = 1
            },
            new() {
                BookId = 2,
                Title = "A Great Book, part 2",
                Description = "This one is not so great.",
                Year = 2024,
                AuthorId = 1
            },
            new() {
                BookId = 3,
                Title = "A Silly Book",
                Description = "A silly book indeed.",
                Year = 2024,
                AuthorId = 2
            },
            new() {
                BookId = 4,
                Title = "Alpha et Omega al.",
                Description = "So dense its stupidly briliant.",
                Year = 2024,
                AuthorId = 3
            },
        };

    }

    // GET
    [Fact]
    public async Task GetAll_ReturnsCorrectType() {
        // Arrange
        _mock.Setup(service => service.GetAll()).ReturnsAsync(GetTestBooks);

        // Act
        var result = await _controller.GetAll();

        // Assert
        Assert.IsType<List<Book>>(result);
        if (result != null) Assert.Equal(4, result.Count); // Not necessary for 100% coverage.
    }

    [Fact]
    public async void Get_ReturnsBook_WhenBookExists() {
        // Arrange
        var book = new Book {
            BookId = 1,
            Title = "A Great Book",
            Description = "One for the ages.",
            Year = 2024,
            AuthorId = 1
        };
        _mock.Setup(service => service.Get(1)).ReturnsAsync(book);

        // Act
        var result = await _controller.Get(1) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        if (result != null) Assert.Equal(book, result.Value);
    }

    [Fact]
    public async void Get_ReturnsNotFound_WhenBookDoesNotExist() {
        // Arrange
        _mock.Setup(service => service.Get(1)).ReturnsAsync(null as Book);

        // Act
        var result = await _controller.Get(1);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }

    //  DELETE
    [Fact]
    public async void Delete_ReturnsOk_WhenBookExists() {
        // Arrange
        var book = new Book {
            BookId = 1,
            Title = "A Great Book",
            Description = "One for the ages.",
            Year = 2024,
            AuthorId = 1
        };
        _mock.Setup(service => service.Get(1)).ReturnsAsync(book);
        _mock.Setup(service => service.Delete(1));

        // Act
        var result = await _controller.Delete(1) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        if (result != null) Assert.Equal($"Book with id: {book.BookId} has been deleted", result.Value);
    }

    [Fact]
    public async void Delete_ReturnsNotFound_WhenBookDoesNotExist() {
        // Arrange
        _mock.Setup(service => service.Get(1)).ReturnsAsync(null as Book);

        // Act
        var result = await _controller.Delete(1);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }
}
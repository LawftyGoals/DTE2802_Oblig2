using BookAPI.Controllers;
using BookAPI.Models;
using BookAPI.Service;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BookAPITests;
public class BookControllerTests {
    private readonly Mock<IBookService> _mock;
    private readonly BookController _controller;

    public BookControllerTests() {
        _mock = new Mock<IBookService>();
        _controller = new BookController(_mock.Object);
    }

    private static List<Book> GetTestBooks() {
        return new List<Book> {
            new Book() {
                BookId = 1,
                Title = "A Great Book",
                Description = "One for the ages.",
                Year = 2024,
                AuthorId = 1
            },
            new Book() {
                BookId = 2,
                Title = "A Great Book, part 2",
                Description = "This one is not so great.",
                Year = 2024,
                AuthorId = 1
            },
            new Book() {
                BookId = 3,
                Title = "A Silly Book",
                Description = "A silly book indeed.",
                Year = 2024,
                AuthorId = 2
            },
            new Book() {
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
        if (result != null) Assert.Equal(3, result.Count); // Not necessary for 100% coverage.
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
        _mock.Setup(service => service.Get(1)).ReturnsAsync((Book)null);

        // Act
        var result = _controller.Get(1);

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
        if (result != null) Assert.Equal("Book successfully deleted.", result.Value);
    }

    [Fact]
    public async void Delete_ReturnsNotFound_WhenBookDoesNotExist() {
        // Arrange
        _mock.Setup(service => service.Get(1)).ReturnsAsync((Book)null);

        // Act
        var result = _controller.Delete(1);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }
}
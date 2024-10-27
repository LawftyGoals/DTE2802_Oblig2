const baseUrl = "https://localhost:7008/api/";

$(document).ready(function () {
  function closeUpdateOverlay() {
    $(".overlayUpdate").css("display", "none");
    fetchBooks();
  }

  $("[id='choice Books']").on("click", function () {
    $("#dataContainer").empty();
    fetchBooks();
  });

  $("[id='choice Authors']").on("click", function () {
    $("#dataContainer").empty();
    fetchAuthors();
  });

  $("#hideOverlay").on("click", function () {
    $(".overlayAdd").css("display", "none");
  });

  $("#hideUpdateOverlay").on("click", function () {
    closeUpdateOverlay();
  });

  $("#addBookButton").on("click", function () {
    $(".overlayAdd").css("display", "flex");
  });

  $(".buttonCloseNotification").on("click", function () {
    $(".notification").css("display", "none");
  });

  $("#submitAddBook").on("click", function (event) {
    event.preventDefault();

    let bookTitle = $("#inputBookTitle").val();
    let bookDescription = $("#inputBookDescription").val();
    let bookYear = $("#inputBookYear").val();
    let bookAuthorFirstName = $("#inputBookAuthorFirstName").val();
    let bookAuthorLastName = $("#inputBookAuthorLastName").val();

    $.ajax({
      url: baseUrl + "Book",
      type: "POST",
      contentType: "application/json",
      data: JSON.stringify({
        title: bookTitle,
        description: bookDescription,
        year: bookYear,
        authorFirstName: bookAuthorFirstName,
        authorLastName: bookAuthorLastName,
      }),
      success: function (response) {
        notification(`Added the book ${response.title} successfully!`);
      },
      error: function (response) {
        errorNotification(response.responseText);
      },
    });
  });

  function fetchBooks() {
    $("#dataContainer").empty();

    $.ajax({
      url: baseUrl + "Book",
      type: "GET",
      dataType: "json",
      success: function (response) {
        response.forEach((book) => {
          $.ajax({
            url: baseUrl + "Author/" + book.authorId,
            type: "GET",
            dataType: "json",
            success: function (authorOfBook) {
              $("#dataContainer").append(
                `
                <div class="bookContainer">
                    <div id="bookAuthorContainer">
                        <div id="title">Book Title</div>
                        <div class="content">${book.title}</div>
                        <div id="title">First Name</div>
                        <div class="content">${authorOfBook.firstName}</div>
                        <div id="title">Last Name</div>
                        <div class="content">${authorOfBook.lastName}</div>
                        <div id="title">Year</div>
                        <div class="content">${book.year}</div>
                    </div>
                    <div>
                        <span><strong>Description:</strong> ${book.description}</span>
                    </div>
                        <button id="buttonDelete${book.bookId}">Delete</button>
                        <button id="buttonUpdate${book.bookId}">Update</button>
                </div>
                `
              );

              $(`#buttonDelete${book.bookId}`).on("click", function () {
                $.ajax({
                  url: baseUrl + "Book/" + book.bookId,
                  type: "DELETE",
                  success: function (response) {
                    notification(`Deleted ${response.title} successfully`);
                    fetchBooks();
                  },
                  error: function (response) {
                    errorNotification(response.responseText);
                  },
                });
              });

              $(`#buttonUpdate${book.bookId}`).on("click", function () {
                $(".overlayUpdate").css("display", "flex");
                updateBook(book, authorOfBook);
              });
            },
            error: function (xhr, status, error) {
              console.error("Error when fetching author", error);
            },
          });
        });
      },
      error: function (xhr, status, error) {
        console.error("Error fetching data", error);
      },
    });
  }

  function fetchAuthors() {
    $.ajax({
      url: baseUrl + "Author",
      type: "GET",
      dataType: "json",
      success: function (response) {
        response.forEach((author) => {
          $.ajax({
            url: baseUrl + "Book",
            type: "GET",
            dataType: "json",
            success: function (books) {
              let bookElements = "";
              books
                .filter((book) => book.authorId === author.authorId)
                .forEach((book) => {
                  bookElements += `<p id="authorBookTitle">${book.title}</p>`;
                });

              $("#dataContainer").append(
                `<div id="authorContainer">
                <div id="title">Author</div>
                <div id="title">Books</div>
                <div>${author.firstName} ${author.lastName}</div>
                <div>${bookElements}</div>
                </div>`
              );
            },
            error: function (xhr, status, error) {
              console.error("Error when fetching author", error);
            },
          });
        });
      },
      error: function (xhr, status, error) {
        console.error("Error fetching data", error);
      },
    });
  }

  function updateBook(book, author) {
    $("#inputBookUpdateTitle").val(book.title);
    $("#inputBookUpdateDescription").val(book.description);
    $("#inputBookUpdateYear").val(book.year);
    $("#inputBookUpdateAuthorFirstName").val(author.firstName);
    $("#inputBookUpdateAuthorLastName").val(author.lastName);

    $("#submitUpdateBook").on("click", function (event) {
      event.preventDefault();

      const bookId = book.bookId;
      const title = $("#inputBookUpdateTitle").val();
      const description = $("#inputBookUpdateDescription").val();
      const year = Number($("#inputBookUpdateYear").val());
      const authorFirstName = $("#inputBookUpdateAuthorFirstName").val();
      const authorLastName = $("#inputBookUpdateAuthorLastName").val();

      $.ajax({
        url: baseUrl + "Author",
        type: "GET",
        dataType: "json",
        success: function (response) {
          let existingAuthor = response.find((eA) => {
            return (
              eA.firstName === authorFirstName && eA.lastName === authorLastName
            );
          });

          if (existingAuthor) {
            $.ajax({
              url: baseUrl + "Book/" + bookId,
              type: "PUT",
              contentType: "application/json",
              data: JSON.stringify({
                bookId: bookId,
                title: title,
                description: description,
                year: year,
                authorId: existingAuthor.authorId,
              }),
              success: function (response) {
                notification(response);
                closeUpdateOverlay();
              },
              error: function (response) {
                console.error(response);
                errorNotification(response);
              },
            });
          } else {
            $.ajax({
              url: baseUrl + "Author",
              type: "POST",
              contentType: "application/json",
              data: JSON.stringify({
                firstName: authorFirstName,
                lastName: authorLastName,
              }),
              success: function (createdAuthor) {
                $.ajax({
                  url: baseUrl + "Book/" + bookId,
                  type: "PUT",
                  contentType: "application/json",
                  data: JSON.stringify({
                    bookId: bookId,
                    title: title,
                    description: description,
                    year: year,
                    authorId: createdAuthor.authorId,
                  }),
                  success: function (response) {
                    notification(response);
                    closeUpdateOverlay();
                  },
                  error: function (response) {
                    console.error(response);
                    errorNotification(response);
                  },
                });
              },
              error: function (response) {
                errorNotification(response);
                console.error(response);
              },
            });
          }
        },
        error: function (response) {
          errorNotification(response);
          console.log(response);
        },
      });
    });
  }
});

function notification(response) {
  $("#notificationMessage").text(response);
  $(".notification").css("display", "flex");
  $(".notificationContainer").css({
    "background-color": "green",
  });

  setTimeout(() => {
    $(".notification").css("display", "none");
  }, 5000);
}

function errorNotification(response) {
  $("#notificationMessage").text(response);
  $(".notification").css("display", "flex");
  $(".notificationContainer").css({
    "background-color": "red",
  });

  setTimeout(() => {
    $(".notification").css("display", "none");
  }, 5000);
}

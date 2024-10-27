using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookAPI.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    AuthorId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.AuthorId);
                });

            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Year = table.Column<int>(type: "INTEGER", nullable: false),
                    AuthorId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.BookId);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "AuthorId", "FirstName", "LastName" },
                values: new object[] { 1, "Jack", "Author" });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "AuthorId", "FirstName", "LastName" },
                values: new object[] { 2, "Sheila", "Auteur" });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "AuthorId", "FirstName", "LastName" },
                values: new object[] { 3, "Authmanda", "Tor" });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "AuthorId", "FirstName", "LastName" },
                values: new object[] { 4, "Authin", "Eur" });

            migrationBuilder.InsertData(
                table: "Book",
                columns: new[] { "BookId", "AuthorId", "Description", "Title", "Year" },
                values: new object[] { 1, 1, "One for the ages.", "A Great Book", 2024 });

            migrationBuilder.InsertData(
                table: "Book",
                columns: new[] { "BookId", "AuthorId", "Description", "Title", "Year" },
                values: new object[] { 2, 1, "This one is not so great.", "A Great Book, part 2", 2024 });

            migrationBuilder.InsertData(
                table: "Book",
                columns: new[] { "BookId", "AuthorId", "Description", "Title", "Year" },
                values: new object[] { 3, 2, "A silly book indeed.", "A Silly Book", 2024 });

            migrationBuilder.InsertData(
                table: "Book",
                columns: new[] { "BookId", "AuthorId", "Description", "Title", "Year" },
                values: new object[] { 4, 3, "So dense its stupidly briliant.", "Alpha et Omega al.", 2024 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Book");
        }
    }
}

using BookAPI.Data;
using BookAPI.Service.AuthorServices;
using BookAPI.Service.BookServices;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration
                           .GetConnectionString("BookContextConnection") ??
                       throw new InvalidOperationException(
                           "Connection string 'BookContextConnection' not found.");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//add db
builder.Services.AddDbContext<BookDbContext>(options =>
    options.UseSqlite(connectionString));

//Custom
builder.Services.AddTransient<IBookService, BookService>();
builder.Services.AddTransient<IAuthorService, AuthorService>();
builder.Services.AddCors(options => {
    options.AddPolicy("AllowAllRequests", policyBuilder => {
        policyBuilder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();

//Custom
app.UseCors("AllowAllRequests");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

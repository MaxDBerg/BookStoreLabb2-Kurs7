using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using MinimalAPI_Books.Data;
using MinimalAPI_Books.Models;
using MinimalAPI_Books.Repositories;
using MinimalAPI_Books.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddDbContext<BookstoreDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/books", async (IBookRepository repository) =>
{
    var book = await repository.GetAllAsync();

    if (book == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(book);
});

app.MapGet("/books/{id}", async (IBookRepository repository, int id) =>
{
    var book = await repository.GetByIdAsync(id);

    if (book == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(book);
});

app.MapPost("/books", async (IValidator<Book> validator, IBookRepository repository, Book book, int genreId) =>
{
    var validationResult = await validator.ValidateAsync(book);
    
    if (!validationResult.IsValid)
    {
        return Results.BadRequest(validationResult.Errors.ToString());
    }
    await repository.AddAsync(book, genreId);
    return Results.Created($"/books/{book.Id}", book);
});

app.MapPut("/books", async (IValidator<Book> validator, IBookRepository repository, BookUpdateValidator updateValidator, Book book) =>
{
    var validationResult = await validator.ValidateAsync(book);
    var validationUpdateResult = await updateValidator.ValidateAsync(book);
    if (!validationUpdateResult.IsValid)
    {
        var errorMessages = validationUpdateResult.Errors
            .Select(error => $"{error.PropertyName}: {error.ErrorMessage}")
            .ToList();
        return Results.NotFound(errorMessages);
    }
    if (!validationResult.IsValid)
    {
        var errorMessages = validationResult.Errors
            .Select(error => $"{error.PropertyName}: {error.ErrorMessage}")
            .ToList();
        return Results.BadRequest(errorMessages);
    }

    await repository.UpdateAsync(book);
    return Results.Ok(book);
});

app.MapDelete("/books/{id}", async (IBookRepository repository, int id) =>
{
    await repository.DeleteAsync(id);
    return Results.Ok();
});

app.Run();

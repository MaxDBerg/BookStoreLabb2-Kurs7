using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using MinimalAPI_Books;
using MinimalAPI_Books.Data;
using MinimalAPI_Books.Models;
using MinimalAPI_Books.Models.DTO.AuthorDTO;
using MinimalAPI_Books.Models.DTO.BookDTO;
using MinimalAPI_Books.Models.DTO.GenreDTO;
using MinimalAPI_Books.Models.DTO.LanguageDTO;
using MinimalAPI_Books.Repositories;
using MinimalAPI_Books.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IRepository<BookReadDTO>, BookRepository<BookReadDTO>>();
builder.Services.AddScoped<IRepository<BookCreateDTO>, BookRepository<BookCreateDTO>>();
builder.Services.AddScoped<IRepository<BookUpdateDTO>, BookRepository<BookUpdateDTO>>();
builder.Services.AddScoped<IRepository<AuthorReadDTO>, AuthorRepository<AuthorReadDTO>>();
builder.Services.AddScoped<IRepository<AuthorWithBooksReadDTO>, AuthorRepository<AuthorWithBooksReadDTO>>();
builder.Services.AddScoped<IRepository<AuthorCreateDTO>, AuthorRepository<AuthorCreateDTO>>();
builder.Services.AddScoped<IRepository<AuthorUpdateDTO>, AuthorRepository<AuthorUpdateDTO>>();
builder.Services.AddScoped<IRepository<LanguageReadDTO>, LanguageRepository<LanguageReadDTO>>();
builder.Services.AddScoped<IRepository<LanguageWithBooksReadDTO>, LanguageRepository<LanguageWithBooksReadDTO>>();
builder.Services.AddScoped<IRepository<LanguageCreateDTO>, LanguageRepository<LanguageCreateDTO>>();
builder.Services.AddScoped<IRepository<LanguageUpdateDTO>, LanguageRepository<LanguageUpdateDTO>>();
builder.Services.AddScoped<IRepository<GenreReadDTO>, GenreRepository<GenreReadDTO>>();
builder.Services.AddScoped<IRepository<GenreWithBooksReadDTO>, GenreRepository<GenreWithBooksReadDTO>>();
builder.Services.AddScoped<IRepository<GenreCreateDTO>, GenreRepository<GenreCreateDTO>>();
builder.Services.AddScoped<IRepository<GenreUpdateDTO>, GenreRepository<GenreUpdateDTO>>();

builder.Services.AddAutoMapper(typeof(MappingProfile));
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

app.MapGet("/books", async (IRepository<BookReadDTO> repository) =>
{
    var book = await repository.GetAllAsync();

    if (book == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(book);
});

app.MapGet("/books/{id}", async (IRepository<BookReadDTO> repository, int id) =>
{
    var book = await repository.GetByIdAsync(id);

    if (book == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(book);
});

app.MapPost("/books", async (IRepository<BookCreateDTO> repository, IValidator < BookCreateDTO> validator, BookCreateDTO book) =>
{
    var validationResult = await validator.ValidateAsync(book);

    if (!validationResult.IsValid)
    {
        var errorMessages = validationResult.Errors
            .Select(error => $"{error.PropertyName}: {error.ErrorMessage}")
            .ToList();
        return Results.BadRequest(errorMessages);
    }
    await repository.AddAsync(book);

    var location = $"/books/{Guid.NewGuid()}";
    return Results.Created(location, book);
});

app.MapPut("/books", async (IRepository<BookUpdateDTO> repository, BookUpdateDTOValidator updateValidator, BookUpdateDTO book) =>
{
    var validationUpdateResult = await updateValidator.ValidateAsync(book);
    if (!validationUpdateResult.IsValid)
    {
        var errorMessages = validationUpdateResult.Errors
            .Select(error => $"{error.PropertyName}: {error.ErrorMessage}")
            .ToList();
        if (errorMessages.Any(error => error == $"Book with Id: {book.Id} does not exist"))
        {
            return Results.NotFound(errorMessages);
        }
        return Results.BadRequest(errorMessages);
    }

    await repository.UpdateAsync(book);
    return Results.Ok(book);
});

app.MapDelete("/books/{id}", async (IRepository<BookReadDTO> repository, int id) =>
{
    await repository.DeleteAsync(id);
    return Results.Ok();
});

app.MapGet("/authors", async (IRepository<AuthorReadDTO> repository) =>
{
    var author = await repository.GetAllAsync();

    if (author == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(author);
});

app.MapGet("/authors/{id}", async (IRepository<AuthorWithBooksReadDTO> repository, int id) =>
{
    var author = await repository.GetByIdAsync(id);

    if (author == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(author);
});

app.MapPost("/authors", async (IRepository<AuthorCreateDTO> repository, AuthorCreateDTO author) =>
{
    if (author == null)
    {
        return Results.BadRequest();
    }
    await repository.AddAsync(author);

    var location = $"/authors/{Guid.NewGuid()}";
    return Results.Created(location, author);
});

app.MapPut("/authors", async (IRepository<AuthorUpdateDTO> repository, AuthorUpdateDTO author) =>
{
    if (author == null)
    {
        return Results.BadRequest();
    }
    await repository.UpdateAsync(author);
    return Results.Ok(author);
});

app.MapDelete("/authors/{id}", async (IRepository<AuthorWithBooksReadDTO> repository, int id) =>
{
    await repository.DeleteAsync(id);
    return Results.Ok();
});

app.MapGet("/languages", async (IRepository<LanguageReadDTO> repository) =>
{
    var language = await repository.GetAllAsync();

    if (language == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(language);
});

app.MapGet("/languages/{id}", async (IRepository<LanguageWithBooksReadDTO> repository, int id) =>
{
    var language = await repository.GetByIdAsync(id);

    if (language == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(language);
});

app.MapPost("/languages", async (IRepository<LanguageCreateDTO> repository, LanguageCreateDTO language) =>
{
    if (language == null)
    {
        return Results.BadRequest();
    }
    await repository.AddAsync(language);

    var location = $"/languages/{Guid.NewGuid()}";
    return Results.Created(location, language);
});

app.MapPut("/languages", async (IRepository<LanguageUpdateDTO> repository, LanguageUpdateDTO language) =>
{
    if (language == null)
    {
        return Results.BadRequest();
    }
    await repository.UpdateAsync(language);
    return Results.Ok(language);
});

app.MapDelete("/languages/{id}", async (IRepository<LanguageWithBooksReadDTO> repository, int id) =>
{
    await repository.DeleteAsync(id);
    return Results.Ok();
});

app.MapGet("/genres", async (IRepository<GenreReadDTO> repository) =>
{
    var genre = await repository.GetAllAsync();

    if (genre == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(genre);
});

app.MapGet("/genres/{id}", async (IRepository<GenreWithBooksReadDTO> repository, int id) =>
{
    var genre = await repository.GetByIdAsync(id);

    if (genre == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(genre);
});

app.MapPost("/genres", async (IRepository<GenreCreateDTO> repository, GenreCreateDTO genre) =>
{
    if (genre == null)
    {
        return Results.BadRequest();
    }
    await repository.AddAsync(genre);

    var location = $"/genres/{Guid.NewGuid()}";
    return Results.Created(location, genre);
});

app.MapPut("/genres", async (IRepository<GenreUpdateDTO> repository, GenreUpdateDTO genre) =>
{
    if (genre == null)
    {
        return Results.BadRequest();
    }
    await repository.UpdateAsync(genre);
    return Results.Ok(genre);
});

app.MapDelete("/genres/{id}", async (IRepository<GenreWithBooksReadDTO> repository, int id) =>
{
    await repository.DeleteAsync(id);
    return Results.Ok();
});

app.Run();

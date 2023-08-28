using MinimalAPI_Books.Data;
using MinimalAPI_Books.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IBookRepository, BookRepository>();

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

app.MapPost("/books", async (IBookRepository repository, Book book) =>
{
    await repository.AddAsync(book);
    return Results.Created($"/books/{book.Id}", book);
});

app.MapPut("/books/{id}", async (IBookRepository repository, int id, Book book) =>
{
    if (id != book.Id)
    {
        return Results.BadRequest();
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

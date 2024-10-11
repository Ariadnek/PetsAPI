using Microsoft.EntityFrameworkCore;
using PetsApi.Models;
using PetsApi.Data;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AnimalContext>(options =>
    options.UseInMemoryDatabase("AnimaisDb"));


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapGet("/animais", async (AnimalContext db) => await db.Animais.ToListAsync());


app.MapGet("/animais/{id}", async (int id, AnimalContext db) =>
{
    var animal = await db.Animais.FindAsync(id);
    return animal is not null ? Results.Ok(animal) : Results.NotFound();
});


app.MapPost("/animais", async (Animal novoAnimal, AnimalContext db) =>
{
    db.Animais.Add(novoAnimal);
    await db.SaveChangesAsync();
    return Results.Created($"/animais/{novoAnimal.Id}", novoAnimal);
});


app.MapPut("/animais/{id}", async (int id, Animal animalAtualizado, AnimalContext db) =>
{
    var animal = await db.Animais.FindAsync(id);
    if (animal is null) return Results.NotFound();

    animal.Nome = animalAtualizado.Nome;
    animal.Idade = animalAtualizado.Idade;
    animal.Cor = animalAtualizado.Cor;
    animal.Tipo = animalAtualizado.Tipo;
    animal.Peso = animalAtualizado.Peso;

    await db.SaveChangesAsync();
    return Results.Ok(animal);
});


app.MapDelete("/animais/{id}", async (int id, AnimalContext db) =>
{
    var animal = await db.Animais.FindAsync(id);
    if (animal is null) return Results.NotFound();

    db.Animais.Remove(animal);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.Run();

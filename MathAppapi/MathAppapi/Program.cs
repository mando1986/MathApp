//using Microsoft.EntityFrameworkCore;


//var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddDbContext<ComputationDb>(opt => opt.UseInMemoryDatabase("ListComputation"));
//builder.Services.AddDatabaseDeveloperPageExceptionFilter();
//var app = builder.Build();

//app.MapGet("/computationitems", async (ComputationDb db) =>
//    await db.Computations.ToListAsync());

//app.MapGet("/computationitems/complete", async (ComputationDb db) =>
//    await db.Computations.Where(t => t.IsComplete).ToListAsync());

//app.MapGet("/computationitems/{id}", async (int id, ComputationDb db) =>
//    await db.Computations.FindAsync(id)
//        is Computation computation
//            ? Results.Ok(computation)
//            : Results.NotFound());

//app.MapPost("/computationitems", async (Computation computation, ComputationDb db) =>
//{
//    db.Computations.Add(computation);
//    await db.SaveChangesAsync();

//    return Results.Created($"/computationitems/{computation.Id}", computation);
//});

//app.MapPut("/computationitems/{id}", async (int id, Computation inputComputation, ComputationDb db) =>
//{
//    var computation = await db.Computations.FindAsync(id);

//    if (computation is null) return Results.NotFound();

//    computation.Pregunta = inputComputation.Pregunta;
//    computation.IsComplete = inputComputation.IsComplete;

//    await db.SaveChangesAsync();

//    return Results.NoContent();
//});

//app.MapDelete("/computationitems/{id}", async (int id, ComputationDb db) =>
//{
//    if (await db.Computations.FindAsync(id) is Computation computation)
//    {
//        db.Computations.Remove(computation);
//        await db.SaveChangesAsync();
//        return Results.NoContent();
//    }

//    return Results.NotFound();
//});


//app.Run();

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ComputationDb>(opt => opt.UseInMemoryDatabase("ListComputation"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
var app = builder.Build();

app.MapGet("/computationitems", async (ComputationDb db) =>
    await db.Computations.ToListAsync());

app.MapGet("/computationitems/complete", async (ComputationDb db) =>
    await db.Computations.Where(t => t.IsComplete).ToListAsync());

app.MapGet("/computationitems/{id}", async (int id, ComputationDb db) =>
    await db.Computations.FindAsync(id) is Computation computation
        ? Results.Ok(computation)
        : Results.NotFound());

app.MapPost("/computationitems", async (Computation computation, ComputationDb db) =>
{
    db.Computations.Add(computation);
    await db.SaveChangesAsync();

    return Results.Created($"/computationitems/{computation.Id}", computation);
});

app.MapPut("/computationitems/{id}", async (int id, Computation inputComputation, ComputationDb db) =>
{
    var computation = await db.Computations.FindAsync(id);

    if (computation is null) return Results.NotFound();

    computation.Pregunta = inputComputation.Pregunta;
    computation.Hint = inputComputation.Hint;
    computation.Answer = inputComputation.Answer;
    computation.IsComplete = inputComputation.IsComplete;
    computation.StringList = inputComputation.StringList;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("/computationitems/{id}", async (int id, ComputationDb db) =>
{
    if (await db.Computations.FindAsync(id) is Computation computation)
    {
        db.Computations.Remove(computation);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }

    return Results.NotFound();
});

// Define an array of random hint sentences
string[] hintSentences = {
    "Think carefully about the operation.",
    "Remember the basic rules of addition and subtraction.",
    "Consider breaking down the problem into smaller parts.",
    "Double-check your calculations for accuracy.",
    "Visualize the problem to help find the solution.",
    "Try to find patterns or shortcuts in the numbers.",
    "Take your time and work through the problem step by step.",
    "Don't forget to carry over when adding or borrowing when subtracting."
};

// Endpoint to generate random addition and subtraction exercises
app.MapGet("/generate-exercise", () =>
{
    Random random = new Random();
    List<Computation> exercises = new List<Computation>();
    int counter = 1;

    for (int i = 0; i < 8; i++)
    {
        int number1 = random.Next(0, 21); // Random number between 0 and 20
        int number2 = random.Next(0, number1 + 1); // Random number between 0 and number1 to ensure subtraction result is non-negative

        string operation = random.Next(0, 2) == 0 ? "+" : "-"; // Randomly choose addition or subtraction
        int correctAnswer = operation == "+" ? number1 + number2 : number1 - number2;

        // Randomly select a hint sentence
        string hint = hintSentences[random.Next(hintSentences.Length)];

        // Create a Computation object with the exercise information
        Computation generatedExercise = new Computation
        {
            Id = counter++,
            Pregunta = $"{number1} {operation} {number2}",
            Hint = hint,
            Answer = correctAnswer.ToString(),
            StringList = new List<string> { number1.ToString(), number2.ToString(), correctAnswer.ToString() }
        };

        exercises.Add(generatedExercise);
    }

    return Results.Ok(exercises);
});


app.Run();




var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

/*
*
Use – Adiciona um middleware ao pipeline e passa o controle para o próximo middleware (via chamada a next). É o método mais comum para encadear middlewares.
Run – Adiciona um middleware "terminal", ou seja, que não chama o próximo middleware. Ele encerra o pipeline naquele ponto.
Map – Cria um ramo (branch) no pipeline baseado no caminho da requisição (path). Se a URL corresponder, a requisição é desviada para aquele pipeline específico.
MapWhen – Similar ao Map, mas o desvio é baseado em uma condição personalizada (um predicado), não apenas no path. Permite ramificar o pipeline com base em qualquer critério da requisição (header, query string, etc).
Em resumo: Use e Run controlam a sequência do pipeline (continuar ou parar), enquanto Map e MapWhen controlam a ramificação (branching) do pipeline com base em condições.
*/

app.Use(async (context, next) =>
{
    Console.WriteLine($"Logic before executing the next delegate in the Use method");
    await next.Invoke();
    Console.WriteLine($"Logic after executing the next delegate in the Use method");
});

app.Map("/usingmapbranch", builder =>
{
    builder.Use(async (context, next) =>
    {
        Console.WriteLine("Map branch logic in the Use method before the next delegate");
        await next.Invoke();
        Console.WriteLine("Map branch logic in the Use method after the next delegate");
    });

    builder.Run(async context =>
    {
        Console.WriteLine("Map branch response to the client in the Run method");
        await context.Response.WriteAsync("Hello from the map branch.");
    });
});

app.MapWhen(context => context.Request.Query.ContainsKey("testquerystring"), builder =>
{
    builder.Run(async context =>
    {
        await context.Response.WriteAsync("Hello from the MapWhen branch.");
    });
});

/*
Utilizamos o método `Run`, que adiciona um componente terminal ao pipeline da aplicação. Podemos observar que não utilizamos o delegado `next`, pois o método `Run` é sempre terminal e encerra o pipeline.
*/
app.Run(async context =>
{
    Console.WriteLine($"Writing the response to the client in the Run method");
    await context.Response.WriteAsync("Hello from the middleware component.");
});

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

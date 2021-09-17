var app = new ApplicationBuilder(args)
    .AddApplicationServices()
    .AddCors()
    .AddOpenApi()
    //.AddAuth()
    .Build();

new ApplicationMiddlewareBuilder(app).UseSwagger().UseCors();

app.MapGet("/", (int limit, int offset, IPersonRepository repository, ILogger<Person> logger, CancellationToken token) => repository.GetAllAsync(limit, offset, token));
app.MapPost("/", (Person person, IPersonRepository repository, ILogger<Person> logger, CancellationToken token) => repository.SaveAsync(person, token));

app.Run();


public record Person(string Name);


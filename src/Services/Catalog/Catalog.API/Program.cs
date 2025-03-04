using BuildingBlocks.Exceptions.Handler;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

var assembly = typeof(Program).Assembly;
builder.Services
    .AddExceptionHandler<CustomExceptionHandler>()
    .AddCarter()
    .AddMediatR(config =>
    {
        config.RegisterServicesFromAssembly(assembly);
        config.AddOpenBehavior(typeof(ValidationBehavior<,>));
        config.AddOpenBehavior(typeof(LoggingBehavior<,>));
    })
    .AddValidatorsFromAssembly(assembly)
    .AddMarten(opts =>
    {
        opts.Connection(builder.Configuration.GetConnectionString("Database")!);
    })
    .UseLightweightSessions();

var app = builder.Build();
// Configure the HTTP request pipeline.

app.MapCarter();

app.UseExceptionHandler(options => { });

await app.RunAsync();

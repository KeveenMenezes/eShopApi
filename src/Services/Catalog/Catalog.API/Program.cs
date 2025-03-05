using BuildingBlocks.Exceptions.Handler;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

var assembly = typeof(Program).Assembly;

builder.Services
    .AddCors(options =>
    {
        options.AddPolicy("AllowAngularApp",
            policy =>
            {
                policy.WithOrigins("https://localhost:4200")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
    })
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

app.UsePathBase("/api");
app.UseCors("AllowAngularApp");
app.MapCarter();
app.UseExceptionHandler(options => { });

await app.RunAsync();

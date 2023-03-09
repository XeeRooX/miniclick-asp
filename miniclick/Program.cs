var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => Results.Json((new {message = "Ok"}), statusCode:200));

app.Run();

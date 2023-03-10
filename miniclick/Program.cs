using Microsoft.EntityFrameworkCore;
using miniclick.Models;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;
builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseSqlite(connectionString)
    );

var app = builder.Build();
app.UseDefaultFiles();
app.UseStaticFiles();


app.MapGet("/", async (ApplicationDbContext db, HttpContext context) => {
    var u = new Url() { Id = 1, Link = "12", Uuid = "123" };
    var url = await db.Urls.FirstOrDefaultAsync();
    await Results.Json(url).ExecuteAsync(context);
});

app.Run();

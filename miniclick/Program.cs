using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using miniclick.Dtos;
using miniclick.Models;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;
builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseSqlite(connectionString)
    );


var app = builder.Build();
app.UseStaticFiles();
app.UseDefaultFiles();

app.MapGet("/", async (HttpContext context) =>
{
    context.Response.ContentType = "text/html; charset=utf-8";
    await context.Response.SendFileAsync("wwwroot/index.html");
});

app.MapGet("/{uuid:length(8)}", async (ApplicationDbContext db, HttpContext context, string uuid) => {
    Url? url = await db.Urls.Where(url => url.Uuid == uuid).FirstOrDefaultAsync();
    if (url == null)
    {
        await Results.NotFound(new {message = "Not found"}).ExecuteAsync(context);
        return;
    }

    if (!url!.Link.Contains("http://") && !url.Link.Contains("https://"))
    {
        
        var path = string.Join(
        "/",
        url.Link.Split("/").Select(s => System.Net.WebUtility.UrlEncode(s))
        );
        var str_url = "http://" + path;
        await Results.Redirect(str_url).ExecuteAsync(context);
    }
    else
    {
        var path = string.Join(
        "/",
        url.Link.Split("/").Select(s => System.Net.WebUtility.UrlEncode(s))
        );
        var str_url = path;
        await Results.Redirect(str_url).ExecuteAsync(context);
    }
});

app.MapPost("/", async (ApplicationDbContext db, HttpContext context, [FromBody] UrlDto url, IConfiguration conf ) => {
    if(url.Url == null)
    {
        await Results.BadRequest(new { message = "Incorrent parametr"}).ExecuteAsync(context);
        return;
    }

    string uuid = Guid.NewGuid().ToString().Substring(0, 8);
    Url? generatedUrl = null;

    for(int i = 0; i < 10; i++)
    {
        if (db.Urls.Where(url=> url.Uuid == uuid).FirstOrDefault() != null)
        {
            uuid = Guid.NewGuid().ToString().Substring(0, 8);
        }
        else
        {
            generatedUrl = new Url() { Link = url.Url!, Uuid = uuid };
        }
    }

    if (generatedUrl == null)
    {
        await Results.Json(new { message = "Internal Server Error" }, statusCode: 500).ExecuteAsync(context);
        return;
    }

    await db.Urls.AddAsync(generatedUrl!);
    await db.SaveChangesAsync();

    await Results.Json(new { url = "http://"+ conf["MY_HOSTNAME"]+ "/"+generatedUrl!.Uuid}).ExecuteAsync(context);
});

app.Run();

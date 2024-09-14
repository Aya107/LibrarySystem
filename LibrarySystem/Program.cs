using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using LibrarySystem.Data;
using LibrarySystem.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<LibraryDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
));

builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<AuthorServices>();
builder.Services.AddScoped<BookServices>();
builder.Services.AddRazorPages();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Library System API",
        Version = "v1",
        Description = "An API for managing books and authors in a library system"
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Library System API V1");
    c.RoutePrefix = string.Empty; // Sets Swagger UI at the root
});

app.MapRazorPages();
app.MapControllers();

app.Run();

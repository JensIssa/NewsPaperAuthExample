using Microsoft.EntityFrameworkCore;
using NewsPaperAuthExample.Repo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//TODO : add the actual connection string
builder.Services.AddDbContext<RepoContext>(options =>
{
    options.UseSqlServer("WHEN WE HAVE A DB!");
});

//TODO add auto migration when a migration is pending
using (var context = new RepoContext(builder.Services.BuildServiceProvider().GetService<DbContextOptions<RepoContext>>()))
{
    //check if there are any pending migrations
    if (context.Database.GetPendingMigrations().Any())
    {
        //apply any pending migrations
        context.Database.Migrate();
    }
}


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

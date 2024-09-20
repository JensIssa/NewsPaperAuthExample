using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NewsPaperAuthExample.Entities;
using NewsPaperAuthExample.Entities.DTO.Users;
using NewsPaperAuthExample.Repo;
using NewsPaperAuthExample.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<RepoContext>(options => options.UseNpgsql("Server=localhost:5432;Database=NewsPaper;Username=postgres;Password=SuperSecret7!;"));

builder.Services.AddScoped<IUserRepo,  UserRepo>();

builder.Services.AddScoped<IRepo, Repo>();
builder.Services.AddScoped<IService, Service>();

builder.Services.AddScoped<IUserService, UserService>();

var mapper = new MapperConfiguration(options =>
{

    options.CreateMap<UserAddDTO, User>();
    options.CreateMap<UserEditDTO, User>();
    options.CreateMap<User, UserGetDTO>();
}).CreateMapper();


builder.Services.AddIdentity<User, Role>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
    .AddEntityFrameworkStores<RepoContext>()
    .AddDefaultTokenProviders();

var app = builder.Build();

//apply any migrations if any
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<RepoContext>();

    //check if any pending migrations
    if (context.Database.GetPendingMigrations().Any())
    {
        context.Database.Migrate();
    }
}

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

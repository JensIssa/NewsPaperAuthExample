using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NewsPaperAuthExample.Entities;
using NewsPaperAuthExample.Entities.DTO;
using NewsPaperAuthExample.Entities.DTO.Users;
using NewsPaperAuthExample.Repo;
using NewsPaperAuthExample.Service;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<RepoContext>(options => options.UseNpgsql("Server=postgres;Database=NewsPaper;Username=postgres;Password=SuperSecret7!;"));

builder.Services.AddScoped<IUserRepo,  UserRepo>();

builder.Services.AddScoped<IRepo, Repo>();

builder.Services.AddScoped<IService, Service>();

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<ICommentService, CommentService>();

builder.Services.AddScoped<ICommentRepo, CommentRepo>();

var mapper = new MapperConfiguration(options =>
{

    options.CreateMap<UserAddDTO, User>();
    options.CreateMap<UserEditDTO, User>();
    options.CreateMap<User, UserGetDTO>();
    options.CreateMap<Article, ArticleDTO>();
    options.CreateMap<ArticleDTO, Article>();
    options.CreateMap<Comment, CommentDTO>();
    options.CreateMap<CommentDTO, Comment>();


}).CreateMapper();

builder.Services.AddSingleton(mapper);

builder.Services.AddIdentity<User, Role>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
    .AddEntityFrameworkStores<RepoContext>()
    .AddDefaultTokenProviders();


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
  .AddJwtBearer(options =>
  {
      options.TokenValidationParameters = new TokenValidationParameters
      {
          ValidateIssuer = true,
          ValidateAudience = true,
          ValidateLifetime = true,
          ValidateIssuerSigningKey = true,
          ValidIssuer = builder.Configuration["Jwt:Issuer"],
          ValidAudience = builder.Configuration["Jwt:Audience"],
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
      };
  });



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

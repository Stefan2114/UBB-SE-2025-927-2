using AppCommonClasses.Interfaces;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Repos;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SocialAppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SocialAppDb")));

// Add services to the container.
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<IMealRepository, MealRepository>();
builder.Services.AddScoped<IReactionRepository, ReactionRepository>();


builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<IBodyMetricRepository, BodyMetricRepository>();
builder.Services.AddScoped<ICalorieRepository, CalorieRepository>();
builder.Services.AddScoped<IWaterIntakeRepository, WaterIntakeRepository>();
builder.Services.AddScoped<IGroupRepository, GroupRepository>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

using AppCommonClasses.Data;
using AppCommonClasses.Interfaces;
using AppCommonClasses.Repos;
using AppCommonClasses.Services;
using Microsoft.EntityFrameworkCore;
using Server.Interfaces;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SocialAppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SocialAppDb")));

// Add services to the container.
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<IGroceryListRepository, GroceryListRepository>();
builder.Services.AddScoped<IMealRepository, MealRepository>();
builder.Services.AddScoped<IReactionRepository, ReactionRepository>();
builder.Services.AddScoped<IMacrosRepository, MacrosRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<IBodyMetricRepository, BodyMetricRepository>();
builder.Services.AddScoped<ICalorieRepository, CalorieRepository>();
builder.Services.AddScoped<IWaterIntakeRepository, WaterIntakeRepository>();
builder.Services.AddScoped<IGroupRepository, GroupRepository>();
builder.Services.AddScoped<IMealRepository, MealRepository>();
builder.Services.AddScoped<IIngredientRepository, IngredientRepository>();
builder.Services.AddScoped<IBodyMetricService, BodyMetricService>();


builder.Services.AddScoped<IGroupService, GroupService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IMealService, MealService>();
builder.Services.AddScoped<IMacrosService, MacrosService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IMealService, MealService>();

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
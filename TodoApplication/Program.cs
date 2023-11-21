using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TodoApplication.Entities;
using TodoApplication.Identity;
using TodoApplication.Infrastructure;
using TodoApplication.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ToDoConnectionString")));

builder.Services.AddTransient(typeof(IRepositoryBase<>), typeof(BaseRepository<>));
builder.Services.AddTransient(typeof(IRepositoryBase<TodoItem>), typeof(TodoRepository));
builder.Services.AddTransient(typeof(ITodoRepository), typeof(TodoRepository));



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



//builder.Services.AddAuthentication("Bearer")
//    .AddJwtBearer("Bearer", options =>
//    {
        
//    }
//    );
//builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
//    .AddEntityFrameworkStores<DataContext>()
//    .AddDefaultTokenProviders();
//builder.Services.AddIdentityServer()
//    .AddApiAuthorization<ApplicationUser, DataContext>();

//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("ADMIN", policy => policy.RequireRole("ADMIN"));
//});



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

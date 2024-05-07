global using Microsoft.EntityFrameworkCore;
using Bank_app_api.Data;
using Bank_app_api.Services.AccountService;
using Bank_app_api.Services.TransactionService;
using Bank_app_api.Services.UserService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
          builder => builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SimpleBankAPI.Data;
using SimpleBankAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<IBank, Bank>();

builder.Services.AddCors(opt =>
{
    opt.AddDefaultPolicy(
        builder =>
        {
            builder.AllowAnyOrigin();
        });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var conn = new SqliteConnection("DataSource=:memory:");
conn.Open();
builder.Services.AddDbContext<BankContext>(opt => opt.UseSqlite(conn));

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

CreateDbIfNotExist(app);

app.Run();

conn.Close();

void CreateDbIfNotExist(IHost host)
{
    using (var scope = host.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<BankContext>();
            context.Database.EnsureCreated();
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred creating the DB.");
        }
    }
}
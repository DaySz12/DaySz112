using MiniProject.Backend.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// --------------------------------------------------------------
// 1. อ่าน ConnectionString แบบปกติจาก appsettings.json
// --------------------------------------------------------------
var connectionString = builder.Configuration.GetConnectionString("PostgreSqlConnection")
    ?? throw new InvalidOperationException("Connection string 'PostgreSqlConnection' not found.");

// --------------------------------------------------------------
// 2. Register DbContext
// --------------------------------------------------------------
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

// --------------------------------------------------------------
// 3. Services อื่น ๆ
// --------------------------------------------------------------
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// --------------------------------------------------------------
// 4. CORS
// --------------------------------------------------------------
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins(
            "https://localhost:7001",
            "http://localhost:7097"
        )
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

var app = builder.Build();

// --------------------------------------------------------------
// 5. Swagger เปิดเฉพาะ Development
// --------------------------------------------------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// --------------------------------------------------------------
// 6. Middlewares
// --------------------------------------------------------------
app.UseHttpsRedirection();

app.UseCors("AllowFrontend");

app.UseAuthorization();

app.MapControllers();

app.Run();

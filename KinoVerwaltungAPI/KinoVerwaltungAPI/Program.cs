using KinoVerwaltungAPI.Data;
using KinoVerwaltungAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();


// Register your DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register your services
builder.Services.AddScoped<IKinoService, KinoService>();
//builder.Services.AddScoped<ISaalService, SaalService>();
//builder.Services.AddScoped<IReiheService, ReiheService>();
//builder.Services.AddScoped<ISitzService, SitzService>();
//builder.Services.AddScoped<IVorführungService, VorführungService>();
//registering other services...

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

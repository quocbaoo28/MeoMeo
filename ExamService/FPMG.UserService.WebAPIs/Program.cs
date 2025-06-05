using FPMG.ExamService.DataAccessLayers;
using FPMG.ExamService.Services.Background;
using FPMG.ExamService.Services.Messaging;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddSingleton<IEventPushlisher, EventPublisher>();
builder.Services.AddSingleton<IEventConsumer, EventConsumer>();
builder.Services.AddHostedService<EventConsumerBackgroundService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<ExamManagementDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
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

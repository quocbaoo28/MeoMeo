using FPMG.GradingService.DataAccessLayers;
using FPMG.GradingService.Repositories;
using FPMG.GradingService.Repositories.AutoMapper;
using FPMG.GradingService.Repositories.Interfaces;
using FPMG.GradingService.Services;
using FPMG.GradingService.Services.Background;
using FPMG.GradingService.Services.Interfaces;
using FPMG.GradingService.Services.Messaging;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSingleton<IEventPushlisher, EventPublisher>();
builder.Services.AddSingleton<IEventConsumer, EventConsumer>();
builder.Services.AddHostedService<EventConsumerBackgroundService>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
builder.Services.AddScoped<IPaymentsRepository, PaymentsRepository>();
builder.Services.AddScoped<IPaymentService, PaymentService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<GradingManagementDBContext>(options =>
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

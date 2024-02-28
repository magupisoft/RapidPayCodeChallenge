using FluentValidation;
using Microsoft.EntityFrameworkCore;
using RapidPay.CardManagement;
using RapidPay.Data;
using RapidPay.Data.Repositories;
using RapidPay.Domain.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<RapidPayDbContext>(
        options => options.UseSqlServer(builder.Configuration.GetConnectionString("RapidPayDb"),
        x => x.MigrationsAssembly("RapidPay.Data")));
builder.Services.AddScoped<ICardManagementService, CardManagementService>();
builder.Services.AddScoped<ICardRepository, CardRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IPaymentFeeRepository, PaymentFeeRepository>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateCardValidator>();
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

InitializeDatabase(builder, app);

app.Run();

static void InitializeDatabase(WebApplicationBuilder builder, WebApplication app)
{
    // Create Database if configured flag is true, otherwise it can be created with ef core migration
    if (builder.Configuration.GetValue<bool>("CreateDbOnRuntimeExecution"))
    {
        using IServiceScope serviceScope = app.Services.GetService<IServiceScopeFactory>().CreateScope();
        var context = serviceScope.ServiceProvider.GetRequiredService<RapidPayDbContext>();
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
    }
}
using Hangfire;
using Hangfire.SqlServer;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = "Data Source=PAL-5CG2114M0C;Initial Catalog=hangfire;User ID=sa;Password=Megg96Megg;";
GlobalConfiguration.Configuration.UseSqlServerStorage(connectionString);

builder.Services.AddHangfire(configuration => configuration
           .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
           .UseSimpleAssemblyNameTypeSerializer()
           .UseRecommendedSerializerSettings()
           .UseSqlServerStorage(connectionString, new SqlServerStorageOptions
           {
               CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
               SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
               QueuePollInterval = TimeSpan.Zero,
               UseRecommendedIsolationLevel = true,
               DisableGlobalLocks = true
           }));
// Other configurations and services registration can go here if needed


builder.Services.AddHangfireServer();

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

app.UseHangfireDashboard("/dashboard");

app.UseAuthorization();

app.MapControllers();

app.Run();

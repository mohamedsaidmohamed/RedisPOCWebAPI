
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Redis configuration


builder.Services.AddStackExchangeRedisCache(options =>
{
    //options.ConfigurationOptions = new StackExchange.Redis.ConfigurationOptions
    //{
    //    AbortOnConnectFail = false,
    //    EndPoints = { "redis-k8s-stg.moe.gov.sa:6379" },
    //    Password = "unifiedportal-stg@2024",
    //    User= "unifiedportal-stg",
    //    //Ssl = false
    //};
    
    options.Configuration=  builder.Configuration.GetConnectionString("Redis");
    
    options.InstanceName = "UnifiedPortal";
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

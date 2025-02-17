
using EasyCaching.Core.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Redis configuration


//builder.Services.AddStackExchangeRedisCache(options =>
//{
//    options.ConfigurationOptions = new StackExchange.Redis.ConfigurationOptions
//    {
//        AbortOnConnectFail = false,
//        EndPoints = { "redis-k8s-stg.moe.gov.sa:6379" },
//        Password = "safrstg@2024@",
//        User = "safeer",
//        //Ssl = false
//    };

//    //options.Configuration=  builder.Configuration.GetConnectionString("Redis");

//    options.InstanceName = "safeer";
//});


builder.Services.AddEasyCaching(options =>
{
    
   options.WithJson("safeer-redis");
    // local
    options.UseInMemory(config => { 
    //config.DBConfig.ExpirationScanFrequency = 60;

    },"safeer-memory");

    
    options.UseRedis(config =>
    {
        config.DBConfig.Endpoints.Add(new EasyCaching.Core.Configurations.ServerEndPoint("127.0.0.1", 6379));
       //  config.DBConfig.Endpoints.Add(new EasyCaching.Core.Configurations.ServerEndPoint("redis-k8s-stg.moe.gov.sa", 6379));
        //config.DBConfig.Password = "safrstg@2024@";
        //config.DBConfig.Username = "safeer";
        config.DBConfig.Password = ""; // If no password is set
        config.DBConfig.IsSsl = false; // Disable SSL
        config.DBConfig.AbortOnConnectFail = false;
        config.DBConfig.ConnectionTimeout = 10000;
        config.DBConfig.AsyncTimeout = 60000; // Increase timeout to 60 seconds
        config.DBConfig.SyncTimeout = 60000;  // Increase sync timeout

    }, "safeer-redis");
    // combine local and distributed
    options.UseHybrid(config =>
    {
        config.TopicName = "safeer-topic";
        config.EnableLogging = false;
        
        // specify the local cache provider name after v0.5.4
        config.LocalCacheProviderName = "safeer-memory";
        // specify the distributed cache provider name after v0.5.4
        config.DistributedCacheProviderName = "safeer-redis";
     
    }, "SafeerAll")
    // use redis bus
    .WithRedisBus(busConf =>
    {
        busConf.Endpoints.Add(new ServerEndPoint("127.0.0.1", 6379));
        busConf.SerializerName = "safeer-redis";
       

    });


});
    var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

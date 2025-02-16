
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
    
   options.WithJson("myredis");
    // local
    options.UseInMemory("m1");

    options.UseRedis(config =>
    {
        config.DBConfig.Endpoints.Add(new EasyCaching.Core.Configurations.ServerEndPoint("redis-k8s-stg.moe.gov.sa", 6379));
        config.DBConfig.Password = "safrstg@2024@";
        config.DBConfig.Username = "safeer";
        config.DBConfig.AbortOnConnectFail = false;
        config.DBConfig.ConnectionTimeout = 10000;

    }, "myredis");
    // combine local and distributed
    options.UseHybrid(config =>
    {
        config.TopicName = "test-topic";
        config.EnableLogging = false;

        // specify the local cache provider name after v0.5.4
        config.LocalCacheProviderName = "m1";
        // specify the distributed cache provider name after v0.5.4
        config.DistributedCacheProviderName = "myredis";
    })
    // use redis bus
    .WithRedisBus(busConf =>
    {
        busConf.Endpoints.Add(new ServerEndPoint("redis-k8s-stg.moe.gov.sa", 6380));
        busConf.SerializerName = "myredis";

    });


});
    var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

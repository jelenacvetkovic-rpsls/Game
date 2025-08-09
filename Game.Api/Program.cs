using Game.Core.Interfaces;
using Game.Core.Services;
using Game.Infrastructure.Clients;
using Game.Infrastructure.Configuration;
using Microsoft.Extensions.Options;
using static ChoiceService.Choice;

namespace Game.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Required so app listens on all interfaces inside Docker
            builder.WebHost.UseUrls("http://0.0.0.0:5000");

            //builder.Services.Configure<RabbitMqSettings>(builder.Configuration.GetSection("RabbitMq"));
            builder.Services.Configure<GrpcSettings>(builder.Configuration.GetSection("Grpc"));
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IChoiceGrpcClient, ChoiceGrpcClient>();
            builder.Services.AddScoped<IGameService, GameService>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy
                        .WithOrigins("http://localhost:5173")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });


            builder.Services.AddGrpcClient<ChoiceClient>((provider, options) =>
            {
                var grpcSettings = provider
                    .GetRequiredService<IOptions<GrpcSettings>>()
                    .Value;

                options.Address = new Uri(grpcSettings.ChoiceServiceUrl);
            }).ConfigurePrimaryHttpMessageHandler(() => new SocketsHttpHandler
            {
                EnableMultipleHttp2Connections = true
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("AllowFrontend");

            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
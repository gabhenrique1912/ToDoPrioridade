using ClassLibrary1;
using ClassLibrary1.Repositories;
using ClassLibrary3;
using System;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<SqlContext>();
            builder.Services.AddControllers();
            builder.Services.AddScoped<IMensagemRepository, MensagemRepository>();
            builder.Services.AddScoped<IRabbitMQProducer, RabbitMQProducer>();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }

}


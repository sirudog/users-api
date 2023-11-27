using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using UsersApi.Config;
using UsersApi.Models.Validation;
using UsersApi.Services;

namespace UsersApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            // Add services to the container.
            builder.Services.Configure<UsersDatabaseSettings>(builder.Configuration.GetSection("UsersDatabase"));
            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true; // avoid sending default 400 bad request on validation errors, use fluent validation instead
            });
            builder.Services.AddValidatorsFromAssemblyContaining<UserValidator>(ServiceLifetime.Transient);
            builder.Services.AddSingleton<IUsersService, MongoUsersService>();

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
            else
            {
                app.UseExceptionHandler("/error");
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}

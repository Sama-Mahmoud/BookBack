
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BookTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()  // Allows requests from any origin
                          .AllowAnyMethod()  // Allows any HTTP method (GET, POST, PUT, DELETE, etc.)
                          .AllowAnyHeader(); // Allows any headers
                });
            });
            var con = builder.Configuration["ConnectionString:sama"];
            builder.Services.AddDbContext<BookContext>(
                (options) => options.UseSqlServer(con)
                );
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("AllowAll");


            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

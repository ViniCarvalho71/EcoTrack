using EcoTrack.Data;
using EcoTrack.Servicos;
using Microsoft.EntityFrameworkCore;
using EcoTrack.Interfaces;

namespace EcoTrack {
    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddDbContext<DataContext>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Adicione os serviços aqui
            builder.Services.AddScoped<IServicoCasa, ServicoCasa>();
            builder.Services.AddScoped<IServicoAgua, ServicoAgua>();
            builder.Services.AddScoped<IServicoLuz, ServicoLuz>();
            builder.Services.AddScoped<IServicoResiduo, ServicoResiduo>();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment()) {
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

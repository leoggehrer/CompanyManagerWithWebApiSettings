namespace CompanyManager.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers()
                            .AddNewtonsoftJson();       // Add this to the controllers for PATCH-operation.

            // Add ContextAccessor to the services.
            builder.Services.AddScoped<Contracts.IContextAccessor, Controllers.ContextAccessor>();

            // Add AppSettings to the services.
            builder.Services.AddSingleton<Logic.Contracts.ISettings>(Logic.Modules.Configuration.AppSettings.Instance);

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}

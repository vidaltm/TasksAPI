using System.Text;
using System;
using TasksAPI.Data;
using Microsoft.OpenApi.Models;
using TasksAPI.Repositories.Interfaces;
using TasksAPI.Repositories;
using TasksAPI.Services.Interfaces;
using TasksAPI.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Reflection;

namespace TasksAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers().AddJsonOptions(options =>
                  options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())); ;

            services.AddDbContext<AppDbContext>(options =>
                  options.UseSqlite(Configuration.GetConnectionString("ProjectConnectionSqlite")));

            services.AddSingleton<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IProjetoRepository, ProjetoRepository>();
            services.AddScoped<ITarefaRepository, TarefaRepository>();
            services.AddScoped<IHistoricoTarefaRepository, HistoricoTarefaRepository>();
            services.AddScoped<IComentarioRepository, ComentarioRepository>();
            services.AddScoped<IProjetoService, ProjetoService>();
            services.AddScoped<ITarefaServices, TarefaServices>();
            services.AddScoped<IHistoricoTarefaService, HistoricoTarefaService>();
            services.AddScoped<IComentarioService, ComentarioService>();

            services.AddCors(options =>
            {
                options.AddPolicy("CORSPolicy",
                    builder => builder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .SetIsOriginAllowed((hosts) => true));
            });

            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("V1", new OpenApiInfo
                {
                    Title = "Prova EclipseWorks",
                    Version = "V1",
                    Description = "CRUD Projeto",
                    Contact = new OpenApiContact
                    {
                        Name = "Thiago Moço Vidal",
                        Email = "thiago.tmv@gmail.com"
                    }
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                x.IncludeXmlComments(xmlPath);
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors("CORSPolicy");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");                
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/V1/swagger.json", "Projetos");
            });
        }
    }
}

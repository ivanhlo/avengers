using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using avengers.Models;

namespace avengers
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // Este método es llamado por el tiempo de ejecución. Use este método para agregar servicios al contenedor.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AvengerContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionString")));
            
            /*
             * Estamos obteniendo de uno de los proveedores de configuración, el ConnectionString.
             * La idea es no tener el ConnectionString hard-codeado acá porque es mala práctica,
             * sino que lo estamos teniendo en una fuente externa en nuestra aplicación. En el módulo
             * de configuraciones vamos a ver distintos lugares en donde podemos guardar el
             * ConnectionString o cualquier otro tipo de información que queramos que se mantenga segura.
             * Por ahora nos vamos a conformar con utilizar el appsettings.json
             */
            
            //UseInMemoryDatabase("AvengerList")); // esta instancia sirve para utilizar una base de datos en memoria


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // Este método es llamado por el tiempo de ejecución. Use este método para configurar la canalización de solicitudes HTTP.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}

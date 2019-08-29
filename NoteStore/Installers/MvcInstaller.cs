using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace NoteStore.Installers
{
    public class MvcInstaller : IInstaller
    {
        public MvcInstaller()
        {
        }

        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(x =>
            {
                var swaggerInfo = new Info();
                swaggerInfo.Description = "Note Store API";
                swaggerInfo.Version = "v1";
                x.SwaggerDoc("v1", swaggerInfo);
            });
        }
    }
}

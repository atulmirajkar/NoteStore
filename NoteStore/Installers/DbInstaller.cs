using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NoteStore.Services;
using NoteStore.Options;
using Microsoft.Extensions.Options;

namespace NoteStore.Installers
{
    public class DbInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<NoteStoreSetting>(configuration.GetSection(nameof(NoteStoreSetting)));
            services.AddSingleton<INoteStoreSettings>(sp =>
            sp.GetRequiredService<IOptions<NoteStoreSetting>>().Value);


            //services.AddSingleton<INoteService,NoteService>();
            services.AddSingleton<INoteService,MongoNoteService>();
        }
    }
}

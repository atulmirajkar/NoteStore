using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NoteStore.Services;
using NoteStore.Options;
using Microsoft.Extensions.Options;
using AspNetCore.Identity.MongoDbCore.Infrastructure;
using NoteStore.Model;

namespace NoteStore.Installers
{
    public class DbInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            // services.Configure<NoteStoreSetting>(configuration.GetSection(nameof(NoteStoreSetting)));
            // services.AddSingleton<INoteStoreSettings>(sp =>
            // sp.GetRequiredService<IOptions<NoteStoreSetting>>().Value);
         
            //mongo
            var noteStoreSetting = configuration.GetSection(nameof(NoteStoreSetting)).Get<NoteStoreSetting>();
            services.AddSingleton<INoteStoreSettings>(noteStoreSetting);

            //identity
            services.AddIdentity<User,Role>()
            .AddMongoDbStores<User,Role,Guid>(noteStoreSetting.ConnectionString,noteStoreSetting.DatabaseName);

            //note service
            //services.AddSingleton<INoteService,NoteService>();
            services.AddSingleton<INoteService,MongoNoteService>();

        }
    }
}

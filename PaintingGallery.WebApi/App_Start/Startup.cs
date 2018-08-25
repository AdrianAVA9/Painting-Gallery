using Autofac;
using Autofac.Integration.WebApi;
using Microsoft.Owin;
using Owin;
using PaintindGallery.Dtos;
using PaintingGallery.Repository.Entities;
using PaintingGallery.WebApi.AutoFac.Modules;
using System.Reflection;
using System.Web.Http;

[assembly: OwinStartup(typeof(PaintingGallery.WebApi.App_Start.Startup))]

namespace PaintingGallery.WebApi.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = GlobalConfiguration.Configuration;

            var autoMapperConfigurtaion = new AutoMapper.MapperConfiguration(configuration =>
            {
                configuration.CreateMap<PatronDto, Patron>();
                configuration.CreateMap<Patron, PatronDto>();
            });

            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterModule<PaintingGalleryModule>();
            builder.RegisterInstance(autoMapperConfigurtaion.CreateMapper());
            builder.RegisterInstance(System.Configuration.ConfigurationManager.ConnectionStrings["PaintingGalleryConnection"].ConnectionString);

            var container = builder.Build();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}

using Autofac;
using PaintingGallery.Manager;

namespace PaintingGallery.WebApi.AutoFac.Modules
{
    public class PaintingGalleryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PatronManager>().AsImplementedInterfaces().AsSelf();

            base.Load(builder);
        }
    }
}
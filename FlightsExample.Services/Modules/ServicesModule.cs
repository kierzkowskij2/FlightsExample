using Autofac;
using FlightsExample.Services.Services;

namespace FlightsExample.Services.Modules
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MailService>()
                .AsImplementedInterfaces()
                .InstancePerDependency();
            builder.RegisterType<PassengerCodeService>()
                .AsImplementedInterfaces()
                .InstancePerDependency();
            builder.RegisterType<QRCodeService>()
                .AsImplementedInterfaces()
                .InstancePerDependency();
            builder.RegisterType<RegistrationService>()
                .AsImplementedInterfaces()
                .InstancePerDependency();
            builder.RegisterType<UserService>()
                .AsImplementedInterfaces()
                .InstancePerDependency();
        }
    }
}
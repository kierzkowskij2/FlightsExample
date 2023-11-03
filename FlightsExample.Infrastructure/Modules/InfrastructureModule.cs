using Autofac;
using FlightsExample.Infrastructure.Context;
using FlightsExample.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FlightsExample.Infrastructure.Modules
{
    public class InfrastructureModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c =>
            {
                var config = c.Resolve<IConfiguration>();
                var opt = new DbContextOptionsBuilder<PassengersContext>();
                opt.UseInMemoryDatabase("FlightsDb"); // TODO: changed to inmemory according to "non-configurable" requirements
                //opt.UseSqlServer(config.GetConnectionString("DefaultConnection")); //TODO: if we want to change back to "real" db we will need to create & apply migrations & add configuration key
                return new PassengersContext(opt.Options);
            })
            .AsSelf()
            .InstancePerLifetimeScope();
            builder.RegisterType<PassengersRepostitory>()
                .AsImplementedInterfaces()
                .InstancePerDependency();
        }
    }
}
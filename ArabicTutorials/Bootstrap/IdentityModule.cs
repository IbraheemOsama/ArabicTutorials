using ArabicTutorials.Common.Config;
using ArabicTutorials.Data.Models;
using Autofac;
using Microsoft.AspNetCore.Identity;

namespace ArabicTutorials.Bootstrap
{
    public class IdentityModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<IdentityMarkerService>().AsSelf().SingleInstance();
            builder.RegisterType<UserValidator<User>>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<PasswordValidator<User>>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<PasswordHasher<User>>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<UpperInvariantLookupNormalizer>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<IdentityErrorDescriber>().AsSelf().SingleInstance();
            builder.RegisterType<SecurityStampValidator<User>>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<UserClaimsPrincipalFactory<User>>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder.RegisterType<UserManager<User>>().AsSelf().SingleInstance();
            builder.RegisterType<SignInManager<User>>().AsSelf().InstancePerLifetimeScope();
        }
    }
}

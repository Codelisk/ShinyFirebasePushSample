
using DryIoc;
using DryIoc.Microsoft.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Shiny;
using System;

namespace LocalNotificationsSample
{
    public partial class MyStartup : ShinyStartup
    {
        public static IContainer? Container { get; private set; }

        public override void ConfigureServices(IServiceCollection services, IPlatform platform)
        {
            services.UseNotifications();
            services.UseFirebaseMessaging<PushDelegate>();
        }

        public override IServiceProvider CreateServiceProvider(IServiceCollection services)
        {
            var container = new Container(Rules
                .Default
                .WithConcreteTypeDynamicRegistrations(reuse: Reuse.Transient)
                .With(Made.Of(FactoryMethod.ConstructorWithResolvableArguments))
                .WithFuncAndLazyWithoutRegistration()
                .WithTrackingDisposableTransients()
                .WithoutFastExpressionCompiler()
                .WithFactorySelector(Rules.SelectLastRegisteredFactory())
            );
            DryIocAdapter.Populate(container, services);
            Container = container;
            return container.GetServiceProvider();
        }
    }
}

using DryIoc;
using Microsoft.Extensions.DependencyInjection;
using Shiny;
using System;
using DryIoc;
using DryIoc.Microsoft.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace LocalNotificationsSample
{
    public partial class MyStartup : ShinyStartup
    {
        public static IContainer? Container { get; private set; }
        public override void ConfigureServices(IServiceCollection services, IPlatform platform)
        {
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
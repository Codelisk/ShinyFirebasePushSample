
using DryIoc;
using DryIoc.Microsoft.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Prism.DryIoc;
using Prism.Ioc;
using Shiny;
using Shiny.Notifications;
using System;
using System.Collections.Generic;

namespace LocalNotificationsSample
{
    public partial class MyStartup : ShinyStartup
    {
        public static IContainer? Container { get; private set; }

        public override void ConfigureServices(IServiceCollection services, IPlatform platform)
        {
            var channels = new[] {
                Channel.Create(
                    "Test",
                    ChannelAction.Create("Reply", ChannelActionType.TextReply),
                    ChannelAction.Create("Yes"),
                    ChannelAction.Create("No", ChannelActionType.Destructive)
                ),
                Channel.Create(
                    "ChatName",
                    ChannelAction.Create("Answer", ChannelActionType.TextReply)
                ),
                Channel.Create(
                    "ChatAnswer",
                    ChannelAction.Create("Yes"),
                    ChannelAction.Create("No", ChannelActionType.Destructive)
                )
            };

            services.UseNotifications<PushDelegate>();
            // only pass channels to push or here, not both - technically you don't need this with push
            services.UsePush<PushDelegate>();
        }

        public override IServiceProvider CreateServiceProvider(IServiceCollection services)
        {
            // This registers and initializes the Container with Prism ensuring
            // that both Shiny & Prism use the same container
            ContainerLocator.SetContainerExtension(() => new DryIocContainerExtension());
            var container = ContainerLocator.Container.GetContainer();
            DryIocAdapter.Populate(container, services);
            return container.GetServiceProvider();
        }
    }
}
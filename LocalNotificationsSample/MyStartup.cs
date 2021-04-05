
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
            var c = new Channel();
            c.Identifier = "benach";
            c.Importance = ChannelImportance.Critical;
            c.Actions = new List<ChannelAction>
            {
                new ChannelAction
                {
                    Identifier="a",
                    Title="OKAY",
                    ActionType= ChannelActionType.OpenApp
                },
                new ChannelAction
                {
                    Identifier="o",
                    Title="OKAY",
                    ActionType= ChannelActionType.TextReply
                }
            };
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
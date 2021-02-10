using DryIoc;
using Prism;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Services;
using Shiny;
using Shiny.Push;
using System.Threading.Tasks;

namespace LocalNotificationsSample
{
    public partial class App : PrismApplication
    {
        public App()
        {
            InitializeComponent();
        }

        protected override void OnInitialized()
        {
            Container.Resolve<IDeviceService>().BeginInvokeOnMainThread(async () =>
            {
                await Task.Delay(2000);
                var result = await NavigationService.NavigateAsync("MainPage");
            });
        }
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IDeviceService, DeviceService>();
            containerRegistry.RegisterForNavigation<MainPage,MainPageViewModel>();
        }
        protected override IContainerExtension CreateContainerExtension()
        {
            var container = new Container(this.CreateContainerRules());
            ShinyHost.Populate((serviceType, func, lifetime) =>
                container.RegisterDelegate(
                    serviceType,
                    _ => func(),
                    Reuse.Singleton // I know everything is singleton
                )
            );
            return new DryIocContainerExtension(container);
        }
    }
}

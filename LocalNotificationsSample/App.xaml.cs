using DryIoc;
using Prism;
using Prism.DryIoc;
using Prism.Ioc;
using Shiny;
using Shiny.Push;

namespace LocalNotificationsSample
{
    public partial class App : PrismApplication
    {
        public App()
        {
            InitializeComponent();
        }

        protected override async void OnInitialized()
        {
            var result = await NavigationService.NavigateAsync("MainPage");
        }
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
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

using DryIoc;
using Prism;
using Prism.DryIoc;
using Prism.Ioc;
using Shiny;
using Shiny.Push;
using System.Threading.Tasks;

namespace LocalNotificationsSample
{
    public partial class App : PrismApplication
    {
        public App()
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            var result = await NavigationService.NavigateAsync("MainPage");
            //if (PushDelegate.test && false)
            //{
            //    var result = await NavigationService.NavigateAsync("TestPage");
            //}
            //else
            //{
            //    var result = await NavigationService.NavigateAsync("MainPage");
            //}
        }
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MainPage,MainPageViewModel>();
            containerRegistry.RegisterForNavigation<TestPage>();
        }
    }
}

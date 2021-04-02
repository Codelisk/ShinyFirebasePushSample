﻿using DryIoc;
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
            InitializeComponent();
        }

        protected override async void OnInitialized()
        {
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
            containerRegistry.RegisterSingleton<IService1, Service1>();
            containerRegistry.RegisterForNavigation<MainPage,MainPageViewModel>();
            containerRegistry.RegisterForNavigation<TestPage>();
        }
        public static void RegisterBackgroundServices(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IService1, Service1>();
        }
    }
}

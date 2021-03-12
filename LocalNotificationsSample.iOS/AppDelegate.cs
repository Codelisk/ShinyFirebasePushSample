﻿using Foundation;
using Shiny;
using UIKit;

namespace LocalNotificationsSample.iOS
{
    [Register(nameof(AppDelegate))]
    public class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            iOSShinyHost.Init(platformBuild: services => services.UseNotifications());
            FirebaseMessaging.Instance.SubscribeToTopic("all");

            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}

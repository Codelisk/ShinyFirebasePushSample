using Foundation;
using ObjCRuntime;
using Shiny;
using System;
using UIKit;

namespace LocalNotificationsSample.iOS
{
    [Register(nameof(AppDelegate))]
    public class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            //iOSShinyHost.Init(platformBuild: services => services.UseNotifications());
            //Shiny.Push.FirebaseMessaging.Instance.SubscribeToTopic("all");

            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
        public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler)
        => DidReceiveRemoteNotification(application, userInfo, completionHandler);
        public override void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error)
        => this.ShinyFailedToRegisterForRemoteNotifications(error);
        public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
        => this.ShinyRegisteredForRemoteNotifications(deviceToken);
    }
}

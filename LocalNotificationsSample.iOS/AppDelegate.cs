using Firebase.CloudMessaging;
using Firebase.InstanceID;
using Foundation;
using ObjCRuntime;
using Shiny;
using System;
using System.Runtime.InteropServices;
using UIKit;
using UserNotifications;

namespace LocalNotificationsSample.iOS
{
    [Register(nameof(AppDelegate))]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate, IUNUserNotificationCenterDelegate, IMessagingDelegate
    {

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            RegisterNotification();
            this.ShinyFinishedLaunching(new MyStartup());
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }

        private void RegisterNotification()
        {
            try
            {
                if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
                {
                    var authOptions = UNAuthorizationOptions.Alert | UNAuthorizationOptions.Badge | UNAuthorizationOptions.Sound;
                    UNUserNotificationCenter.Current.RequestAuthorization(authOptions, (granted, error) =>
                    {
                    });

                    UNUserNotificationCenter.Current.Delegate = this;
                }
                else
                {
                    var allNotificationType = UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound;
                    var settings = UIUserNotificationSettings.GetSettingsForTypes(allNotificationType, null);

                    UIApplication.SharedApplication.RegisterUserNotificationSettings(settings);
                }

                UIApplication.SharedApplication.RegisterForRemoteNotifications();


                Firebase.InstanceID.InstanceId.Notifications.ObserveTokenRefresh((sender, e) =>
                {
                    Firebase.InstanceID.InstanceId.SharedInstance.GetInstanceId(handlerd);
                });

                UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;

                //Firebase.Core.App.Configure();
            }
            catch (Exception ex)
            {
            }
        }

        public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler)
        {
            this.ShinyDidReceiveRemoteNotification(userInfo, completionHandler);
        }
            private void handlerd(InstanceIdResult result, NSError error)
        {
            var tokk=result.Token;
        }

        public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
        {
            this.ShinyRegisteredForRemoteNotifications(deviceToken);
#if DEBUG
            //Messaging.SharedInstance.SetApnsToken(deviceToken, ApnsTokenType.Sandbox);
#else
            Messaging.SharedInstance.SetApnsToken(deviceToken, ApnsTokenType.Production);
#endif
        }


    }
}
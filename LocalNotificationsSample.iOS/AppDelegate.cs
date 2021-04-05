using Firebase.CloudMessaging;
using Foundation;
using ObjCRuntime;
using Shiny;
using System;
using System.Linq;
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
            this.ShinyFinishedLaunching(new MyStartup());
            LoadApplication(new App());
           
            if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
            {
                // For iOS 10 display notification (sent via APNS)
                UNUserNotificationCenter.Current.Delegate = this;

                var authOptions = UNAuthorizationOptions.Alert | UNAuthorizationOptions.Badge | UNAuthorizationOptions.Sound;
                UNUserNotificationCenter.Current.RequestAuthorization(authOptions, (granted, error) =>
                {
                    Console.WriteLine(granted);
                });

                // For iOS 10 display notification (sent via APNS)
                UNUserNotificationCenter.Current.Delegate = this;

            }
            else
            {
                // iOS 9 or before

                var settings = UIUserNotificationSettings.GetSettingsForTypes(UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound, new NSSet());
                UIApplication.SharedApplication.RegisterUserNotificationSettings(settings);
            }

            UIApplication.SharedApplication.RegisterForRemoteNotifications();

            

            //Messaging.SharedInstance.Delegate = this;

            return base.FinishedLaunching(app, options);
        }

        public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
        {
            var token=ExtractToken(deviceToken);
            Console.Write(token);
            this.ShinyRegisteredForRemoteNotifications(deviceToken);

        }
        private string ExtractToken(NSData deviceToken)
        {
            if (deviceToken.Length == 0)
                return null;
            var result = new byte[deviceToken.Length];
            System.Runtime.InteropServices.Marshal.Copy(deviceToken.Bytes, result, 0, (int)deviceToken.Length);
            return BitConverter.ToString(result).Replace("-", "");
        }
        public override void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error)
        {
            this.ShinyFailedToRegisterForRemoteNotifications(error);
        }



        public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler)
        {

            this.ShinyDidReceiveRemoteNotification(userInfo, completionHandler);
        }


        public void ApplicationReceivedRemoteMessage(RemoteMessage remoteMessage)
        {
            System.Diagnostics.Debug.WriteLine(remoteMessage.AppData);
            var title = remoteMessage.AppData.ValueForKey(new NSString("title"));
            var text = remoteMessage.AppData.ValueForKey(new NSString("text"));
            debugAlert("" + title, "" + text);
        }
        [Export("messaging:didRefreshRegistrationToken:")]
        public void DidRefreshRegistrationToken(Messaging mess, string token)
        {
            Console.WriteLine(token);
        }


        private void connectFCM()
        {
            // In the older version the code below was required, now I don know if in the new version the code below has a
            // replacement or not, but I can only assume the ".Connect" is the missing piece to this.
            //Messaging.SharedInstance.Connect((error) =>
            //{
            //    if (error == null)
            //    {
            //        // Messaging.SharedInstance.Subscribe("/topics/all");
            //    }
            //    System.Diagnostics.Debug.WriteLine(error != null ? "error occured" : "connect success");
            //});
        }

        private void debugAlert(string title, string message)
        {

            var alert = new UIAlertView(title ?? "Title", message ?? "Message", null, "OK", null);
            alert.Show();

        }

    }
}
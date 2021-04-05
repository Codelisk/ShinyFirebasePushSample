using Firebase.CloudMessaging;
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

            // Firebase component initialize
            try
            {
                Firebase.Core.App.Configure();
            }
            catch(Exception e)
            {

            }
            Messaging.SharedInstance.Delegate = this;

            Firebase.InstanceID.InstanceId.Notifications.ObserveTokenRefresh((sender, e) =>
            {
                Firebase.InstanceID.InstanceId.SharedInstance.GetInstanceId((result, error) =>
                {
                    if (error == null)
                    {
                        string token = result.Token;
                        Console.WriteLine("Got a notification token: " + token);

                    }
                    else
                    {
                        Console.WriteLine("couldn't get Firebase Token: " + error);
                    }
                });

                connectFCM();
            });
            return base.FinishedLaunching(app, options);
        }

        public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
        {
            Messaging.SharedInstance.ApnsToken = deviceToken;
            var tok=deviceToken.ToString();
            var bytes = deviceToken.ToArray();
            byte[] result = new byte[deviceToken.Length];
            Marshal.Copy(deviceToken.Bytes, result, 0, (int)deviceToken.Length);

            var token = BitConverter.ToString(result).Replace("-", "");
            // Seems that there is no equivalent to SetApnsToken in the new version
            //Firebase.InstanceID.InstanceId.SharedInstance.SetApnsToken(deviceToken, Firebase.InstanceID.ApnsTokenType.Unknown);

        }

        public override void OnActivated(UIApplication uiApplication)
        {
            global::Xamarin.Forms.Forms.Init();

            base.OnActivated(uiApplication);
        }

        public override void DidEnterBackground(UIApplication uiApplication)
        {

        }


        public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler)
        {

            Messaging.SharedInstance.AppDidReceiveMessage(userInfo);

            // Generate custom event
            NSString[] keys = { new NSString("Event_type") };
            NSObject[] values = { new NSString("Recieve_Notification") };
            var parameters = NSDictionary<NSString, NSObject>.FromObjectsAndKeys(keys, values, keys.Length);

            // Send custom event
            //Firebase.Analytics.Analytics.LogEvent("CustomEvent", parameters);

            if (application.ApplicationState == UIApplicationState.Active)
            {
                System.Diagnostics.Debug.WriteLine(userInfo);
                var aps_d = userInfo["aps"] as NSDictionary;
                var alert_d = aps_d["alert"] as NSDictionary;
                var body = alert_d["body"] as NSString;
                var title = alert_d["title"] as NSString;
                var category_d = aps_d["category"] as NSDictionary;
            }
        }


        // iOS 10, fire when recieve notification foreground
        [Export("userNotificationCenter:willPresentNotification:withCompletionHandler:")]
        public void WillPresentNotification(UNUserNotificationCenter center, UNNotification notification, Action<UNNotificationPresentationOptions> completionHandler)
        {
            var title = notification.Request.Content.Title;
            var body = notification.Request.Content.Body;
            var view = notification.Request.Content.CategoryIdentifier;

            debugAlert("Notification: " + title, body);

        }

        public void ApplicationReceivedRemoteMessage(RemoteMessage remoteMessage)
        {
            System.Diagnostics.Debug.WriteLine(remoteMessage.AppData);
            var title = remoteMessage.AppData.ValueForKey(new NSString("title"));
            var text = remoteMessage.AppData.ValueForKey(new NSString("text"));
            debugAlert("" + title, "" + text);
        }

        [Export("userNotificationCenter:didReceiveNotificationResponse:withCompletionHandler:")]
        public void DidReceiveNotificationResponse(UNUserNotificationCenter center, UNNotificationResponse response, Action completionHandler)
        {
            System.Diagnostics.Debug.WriteLine(response.Notification); // this is used when app is in background and comes to foreground

            var title = response.Notification.Request.Content.Title;
            var body = response.Notification.Request.Content.Body;

            var redirectView = "NotificationsListView";

            if (redirectView != null || redirectView != "")
            {
                //App.loadView(redirectView);

            }
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

        //[Export("messaging:didRefreshRegistrationToken")]
        public void DidRefreshRegistrationToken(Messaging messaging, string fcmToken)
        {
            // Obsolete interface
            Console.WriteLine(messaging.FcmToken + " | " + fcmToken);
        }
        string token;
        [Export("messaging:didReceiveRegistrationToken:")]
        public void DidReceiveRegistrationToken(Messaging messaging, string fcmToken)
        {
            token = fcmToken.ToString();
            
        }
    }
}
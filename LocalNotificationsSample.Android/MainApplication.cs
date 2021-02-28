using System;
using Android.App;
using Android.Runtime;
using Firebase.Messaging;
using Shiny;
using Shiny.Notifications;

namespace LocalNotificationsSample.Droid
{
    [Application]
    public class MainApplication : Application
    {
        public MainApplication(IntPtr handle, JniHandleOwnership transfer) : base(handle, transfer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();
            this.ShinyOnCreate(new MyStartup());

            AndroidOptions.DefaultLaunchActivityFlags = AndroidActivityFlags.FromBackground;
            AndroidOptions.DefaultSmallIconResourceName = "icon";
            AndroidOptions.DefaultLargeIconResourceName = "icon";

            FirebaseMessaging.Instance.SubscribeToTopic("all");
        }

    }
}

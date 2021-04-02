using System;
using Android.App;
using Android.Runtime;
using Firebase.Messaging;
using Shiny;

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
            this.ShinyOnCreate(new MyStartup());
            base.OnCreate();
        }

    }
}

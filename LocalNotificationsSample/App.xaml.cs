using System;
using System.Threading.Tasks;
using Shiny;
using Shiny.Notifications;
using Xamarin.Forms;

namespace LocalNotificationsSample
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override async void OnStart()
        {
           // await SendNotificationNow();
            //await ScheduleLocalNotification(DateTimeOffset.UtcNow.AddSeconds(2));
        }

        async Task SendNotificationNow()
        {
            var notification = new Notification
            {
                Id=1,
                Title = "Testing Immediate Local Notifications",
                Message = "It's working",
                Channel = "ch1",
                Android = new AndroidOptions()
            };
            var n = ShinyHost.Resolve<INotificationManager>();
            var access=await n.RequestAccess();
            await n.CreateChannel(new Channel { Identifier = "ch1" });
            await n.Send(notification);
        }

        async Task ScheduleLocalNotification(DateTimeOffset scheduleDate)
        {
            var notification = new Notification
            {
                Title = "Testing Scheduled Local Notifications",
                Message = $"Scheduled for {scheduleDate}",
                ScheduleDate = scheduleDate,
            };

            var n = ShinyHost.Resolve<INotificationManager>();
            var access =await n.RequestAccess();
            await n.Send(notification);
        }
    }
}

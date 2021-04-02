using Prism.Ioc;
using Prism.Navigation;
using Shiny;
using Shiny.Notifications;
using Shiny.Push;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace LocalNotificationsSample
{
    public class PushDelegate : IPushDelegate, INotificationDelegate
    {

        IService1 Service1;
        INotificationManager NotificationManager1;
        public PushDelegate(INotificationManager notificationManager)
        {
            NotificationManager1 = notificationManager;
           // Service1 = service1;
        }

        public async Task OnEntry(PushEntryArgs args)
        {
        }

        public async Task OnEntry(NotificationResponse response)
        {
            Console.WriteLine("TEST");
        }

        public async Task OnReceived(IDictionary<string, string> data, Notification? notification)
        {
            var notification1 = new Notification
            {
                Title = "title",
                Message = "message",
            };
            await NotificationManager1.Send(notification1);
        }

        public Task OnTokenChanged(string token)
        {
            return Task.CompletedTask;
        }
    }
}
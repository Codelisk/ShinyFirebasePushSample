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
    public class PushDelegate : IPushDelegate
    {

        public PushDelegate()
        {
        }

        public Task OnEntry(PushNotificationResponse response)
        {
            throw new NotImplementedException();
        }

        public async Task OnReceived(IDictionary<string, string> data)
        {
        }

        public async Task OnReceived(PushNotification notification)
        {
            Console.WriteLine("TEST");
        }

        public Task OnTokenChanged(string token)
        {
            return Task.CompletedTask;
        }
    }
}
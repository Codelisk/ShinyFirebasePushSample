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
        private readonly Lazy<INotificationManager> _notificationManager;
        public static bool test = false;

        public PushDelegate(Lazy<INotificationManager> notificationManager)
        {
            _notificationManager = notificationManager;
        }

        public async Task OnEntry(PushEntryArgs args)
        {
            test = true;
            MainPage.BColor();
            var count = Preferences.Get("counter", 0);
            Preferences.Set("counter", count + 1);
        }

        public async Task OnReceived(IDictionary<string, string> data)
        {
            // We can check to show a notification if needed
            var showNotification = false;
            if (data.ContainsKey("show_notification"))
            {
                _ = bool.TryParse(data["show_notification"], out showNotification);
            }

            var title = "default notification title";
            var message = "default notification message";

            // we can write complex json data and deserialize it, if needed
            if (data.ContainsKey("title"))
                title = data["title"];

            if (data.ContainsKey("title"))
                message = data["message"];

            var notification = new Notification
            {
                Title = title,
                Message = message,
                // recast this as implementation types aren't serializing well
                Payload = data?.ToDictionary(
                        x => x.Key,
                        x => x.Value
                    )
            };

            await _notificationManager.Value.Send(notification);
        }

        public Task OnTokenChanged(string token)
        {
            return Task.CompletedTask;
        }
    }
}
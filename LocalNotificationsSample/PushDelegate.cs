using Prism.Navigation;
using Shiny;
using Shiny.Notifications;
using Shiny.Push;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace LocalNotificationsSample
{
    public class PushDelegate : IPushDelegate
    {
        public static bool test = false;
        private readonly INavigationService navigationService1;
        public PushDelegate()
        {
            //navigationService1 = navigationService;
        }
        public async Task OnEntry(PushEntryArgs args)
        {
            test = true;
            MainPage.BColor();
            var count=Preferences.Get("counter", 0);
            Preferences.Set("counter", count + 1);
        }

        public async Task OnReceived(IDictionary<string, string> data)
        {
        }

        public async Task OnTokenChanged(string token)
        {
        }
    }
}
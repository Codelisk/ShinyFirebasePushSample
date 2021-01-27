using Shiny;
using Shiny.Notifications;
using Shiny.Push;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocalNotificationsSample
{
    public class PushDelegate : IPushDelegate
    {
        public async Task OnEntry(PushEntryArgs args)
        {
            
        }

        public async Task OnReceived(IDictionary<string, string> data)
        {
        }

        public async Task OnTokenChanged(string token)
        {
        }
    }
}
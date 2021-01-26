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
            var notification = new Notification
            {
                Title = "Testing Immediate Local Notifications",
                Message = "It's working",
                Channel = "102",
                Android = new AndroidOptions { LargeIconResourceName = "icon", SmallIconResourceName = "icon", AutoCancel = false, UseBigTextStyle = true }
            };
            var n = ShinyHost.Resolve<INotificationManager>();
            var access = await n.RequestAccess();
            var channelsNow = await n.GetChannels();
            if (channelsNow.FirstOrDefault(x => x.Identifier == "102") == null)
            {
                await n.CreateChannel(new Channel { Identifier = "102", Importance = ChannelImportance.Critical, CustomSoundPath = "alarm" });
            }
            var getChannels = await n.GetChannels();
            var touse = getChannels.Last();
            await n.SetChannels(touse);
            await n.Send(notification);
        }

        public async Task OnReceived(IDictionary<string, string> data)
        {
            var notification = new Notification
            {
                Title = "Testing Immediate Local Notifications",
                Message = "It's working",
                Channel = "102",
                Android = new AndroidOptions { LargeIconResourceName = "icon", SmallIconResourceName = "icon", AutoCancel = false, UseBigTextStyle = true }
            };
            var n = ShinyHost.Resolve<INotificationManager>();
            var access = await n.RequestAccess();
            var channelsNow = await n.GetChannels();
            if (channelsNow.FirstOrDefault(x => x.Identifier == "102") == null)
            {
                await n.CreateChannel(new Channel { Identifier = "102", Importance = ChannelImportance.Critical, CustomSoundPath = "alarm" });
            }
            var getChannels = await n.GetChannels();
            var touse = getChannels.Last();
            await n.SetChannels(touse);
            await n.Send(notification);
        }

        public async Task OnTokenChanged(string token)
        {
        }
    }
}
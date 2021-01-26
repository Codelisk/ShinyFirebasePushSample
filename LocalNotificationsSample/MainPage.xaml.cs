using Shiny;
using Shiny.Notifications;
using Shiny.Push;
using System;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LocalNotificationsSample
{
    public partial class MainPage : ContentPage
    {
        public MainPage() => InitializeComponent();

        private async void Button_Clicked(object sender, System.EventArgs e)
        {
            //var ble=DependencyService.Get<IBleManager>();
            //var n = ShinyHost.Resolve<IBleManager>();
            //var access=await n.RequestAccess();

            //n.Scan().Subscribe(x =>
            //{
            //    Console.WriteLine(x?.Peripheral?.Name);
            //});

            var n = ShinyHost.Resolve<IPushManager>();
            var acc=await n.RequestAccess();
            return;
            await SendNotificationNow();
        }
        async Task SendNotificationNow()
        {
            var notification = new Notification
            {
                Title = "Testing Immediate Local Notifications",
                Message = "It's working",
                Channel="102",
                Android = new AndroidOptions {LargeIconResourceName="icon",SmallIconResourceName="icon",AutoCancel=false,UseBigTextStyle=true }
            };
            var n = ShinyHost.Resolve<INotificationManager>();
            var access = await n.RequestAccess();
            var channelsNow= await n.GetChannels();
            if(channelsNow.FirstOrDefault(x=>x.Identifier == "102") == null)
            {
                await n.CreateChannel(new Channel { Identifier = "102", Importance = ChannelImportance.Critical,CustomSoundPath="alarm" });
            }
            var getChannels = await n.GetChannels();
            var touse=getChannels.Last();
            await n.SetChannels(touse);
            await n.Send(notification);

            var pendings = await n.GetPending();
        }
    }
}

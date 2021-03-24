using Prism.Commands;
using Shiny.Notifications;
using Shiny.Push;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalNotificationsSample
{
    public class MainPageViewModel
    {
        IPushManager? pushManager;
        INotificationManager NotificationManager;
        public MainPageViewModel(IPushManager pushManager)
        {
            this.pushManager = pushManager;
            //Init();
        }
        private async Task Init()
        {
            var r=await pushManager.RequestAccess();
            //var c = new Channel();
            //c.Identifier = "benach";
            //c.Importance = ChannelImportance.Critical;
            //c.Actions = new List<ChannelAction>
            //{
            //    new ChannelAction
            //    {
            //        Identifier="o",
            //        Title="OKAY",
            //        ActionType= ChannelActionType.OpenApp
            //    },
            //    new ChannelAction
            //    {
            //        Identifier="t",
            //        Title="Text Reply",
            //        ActionType= ChannelActionType.TextReply
            //    }
            //};
            //await NotificationManager.CreateChannel(c);
            //await NotificationManager.SetChannels(c);
            //var allÖChannels=await NotificationManager.GetChannels();
            //var test = await this.pushManager.RequestAccess();
        }

        private DelegateCommand _clickCommand;
        public DelegateCommand ClickCommand =>
            _clickCommand ?? (_clickCommand = new DelegateCommand(ExecuteClickCommand));

        async void ExecuteClickCommand()
        {
            var res = await this.pushManager.RequestAccess();
            return;
            var allChannels = await NotificationManager.GetChannels();
            await NotificationManager.Send(new Notification
            {
                Channel = "Test",
                
                Title = "Test",
                Message="Hallo",
            });

        }
    }
}

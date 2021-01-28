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
        public MainPageViewModel(IPushManager? pushManager = null)
        {
            this.pushManager = pushManager;
            Init();
        }
        private async Task Init()
        {
            var test = await this.pushManager.RequestAccess();
        }
    }
}

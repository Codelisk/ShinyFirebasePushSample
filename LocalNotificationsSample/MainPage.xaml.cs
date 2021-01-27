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
        private readonly IPushManager _pushManager;
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, System.EventArgs e)
        {
            var acc=await this._pushManager.RequestAccess();
            return;
        }
    }
}

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
       
    }
}

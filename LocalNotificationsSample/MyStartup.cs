using Android.App.Roles;
using Microsoft.Extensions.DependencyInjection;
using Shiny;
using Shiny.Logging;
using Shiny.Push;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalNotificationsSample
{
    public partial class MyStartup : ShinyStartup
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.UseFirebaseMessaging<PushDelegate>();
        }
    }
}
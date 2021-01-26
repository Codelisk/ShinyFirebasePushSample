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
            Shiny.Logging.Log.UseConsole();
            Shiny.Logging.Log.UseDebug();
            services.AddSingleton<ILogger, Shiny.Logging.ConsoleLogger>();
            services.UseNotifications();
            //services.AddSingleton<AppNotifications>();
            services.UsePush<PushDelegate>();
            var fbs = services.UseFirebaseMessaging<PushDelegate>();
            //services.UseTestMotionActivity(Shiny.Locations.MotionActivityType.Automotive);

            //services.UseAppCenterLogging(Constants.AppCenterTokens, true, false);

            //services.UseSqliteSettings();
            //services.UseSqliteStorage();

        }
    }
}
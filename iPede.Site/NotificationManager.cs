using Microsoft.Azure.NotificationHubs;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;

namespace iPede.Site
{
    public class NotificationManager
    {
        private static NotificationManager _instance;
        private NotificationHubClient _hub;

        private NotificationManager()
        {
            var appSettings = WebConfigurationManager.AppSettings;
            _hub = NotificationHubClient
                .CreateClientFromConnectionString(appSettings["NotificationHubConnectionString"], appSettings["NotificationHubName"]);
        }

        static NotificationManager()
        {
            _instance = new NotificationManager();
        }

        public static NotificationManager Instance => _instance;

        public async Task SendNotificationAsync(string eventName, object item)
        {
            var jObject = JObject.FromObject(
                new
                {
                    EventName = eventName,
                    Item = item
                });
            var json = jObject.ToString();
            try
            {
                var windowsNotification = new WindowsNotification(json)
                {
                    Headers = new Dictionary<string, string> { { "X-WNS-Type", "wns/raw" } }
                };
                var outcome = await _hub.SendNotificationAsync(windowsNotification);
                System.Diagnostics.Debug.WriteLine(outcome.State.ToString());
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
        }

    }
}
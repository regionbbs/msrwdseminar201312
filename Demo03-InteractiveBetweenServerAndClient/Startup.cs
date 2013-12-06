using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Owin;
using Microsoft.Owin;
using Microsoft.AspNet.SignalR;

[assembly: OwinStartup(typeof(Demo03_InteractiveBetweenServerAndClient.Startup), "Configuration")]
namespace Demo03_InteractiveBetweenServerAndClient
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // custom SignalR path and Hub configurations.
            app.MapSignalR("/mySignalR", new HubConfiguration()
                {
                    EnableDetailedErrors = true,
                    EnableJavaScriptProxies = true,
                    EnableJSONP =true
                });
        }
    }
}
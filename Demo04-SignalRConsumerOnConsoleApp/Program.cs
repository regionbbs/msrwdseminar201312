using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNet.SignalR.Infrastructure;
using System.Net;

namespace Demo04_SignalRConsumerOnConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceConnection = new HubConnection("http://localhost:8080");
            var hub = serviceConnection.CreateHubProxy("MathHub");

            //serviceConnection.StateChanged += e =>
            //    {
            //        Console.WriteLine(
            //            "Connection state from {0} to {1}",
            //            e.OldState.ToString(), e.NewState.ToString());
            //    };

            // register event handler.
            hub.On("notifyUserJoined", () =>
                {
                    Console.WriteLine("A new user is joined.");
                    hub.Invoke("GetUsers");
                });

            hub.On("notifyUserLeaved", () =>
                {
                    Console.WriteLine("A user is leaved.");
                    hub.Invoke("GetUsers");
                });

            hub.On("privateMethodCallback", (m) =>
                {
                    Console.WriteLine(m);
                });

            hub.On<IEnumerable<OnlineUser>>("createUsers", (c) =>
                {
                    var users = new StringBuilder();

                    foreach (var user in c)
                    {
                        if (users.Length == 0)
                            users.Append(user.Name);
                        else
                            users.Append(", ").Append(user.Name);
                    }

                    Console.WriteLine("Currently online users: {0}", users);
                });

            // use Windows Authentication account to connect.
            serviceConnection.Credentials = CredentialCache.DefaultCredentials;

            // begin connection.
            serviceConnection.Start().Wait();
            
            // invoke method needs authorization.
            hub.Invoke("PrivateMethod").Wait();

            Console.WriteLine("Please provide your name to join:");
            string userName = Console.ReadLine();

            hub.Invoke("Join", userName);
            
            Console.WriteLine("Please press ENTER to exit.");
                        
            while (true)
            {
                // press enter to exit.
                if (string.IsNullOrEmpty(Console.ReadLine()))
                    break;
            }

            serviceConnection.Stop();
        }
    }
}

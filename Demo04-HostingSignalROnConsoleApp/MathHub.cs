using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo04_HostingSignalROnConsoleApp
{
    public class MathHub : Hub
    {
        public static List<OnlineUser> UserList { get; private set; }

        public MathHub()
        {
            if (MathHub.UserList == null)
                MathHub.UserList = new List<OnlineUser>();
        }

        // broadcast method.
        public void CalculateSummaryB(int X, int Y)
        {
            this.Clients.All.getCalculateSummaryResult("broadcast", X + Y);
        }

        // unicast method.
        public void CalculateSummaryU(string ConnectionId, int X, int Y)
        {
            this.Clients.Client(ConnectionId).getCalculateSummaryResult("unicast", X + Y);
        }

        // multicast method.
        public void CalculateSummaryM(IList<string> ConnectionIds, int X, int Y)
        {
            this.Clients.Clients(ConnectionIds).getCalculateSummaryResult("multicast", X + Y);
        }
        
        public void PrivateMethod()
        {
            if (this.Context.User != null && this.Context.User.Identity.IsAuthenticated)
                this.Clients.Caller.privateMethodCallback("invoked.");
            else
                this.Clients.Caller.privateMethodCallback("Unauthorized.");
        }

        public void Join(string UserName)
        {
            // check user is exist.
            if (MathHub.UserList.Where(c => c.Name == UserName).Any())
            {
                MathHub.UserList.Where(c => c.Name == UserName)
                    .First().ConnectionId = this.Context.ConnectionId;
            }
            else
            {
                MathHub.UserList.Add(new OnlineUser()
                {
                    Name = UserName,
                    ConnectionId = this.Context.ConnectionId
                });

                this.Clients.All.getNewUserPrompt(this.Context.ConnectionId, UserName);
            }
        }

        public void GetUsers()
        {
            Clients.Client(this.Context.ConnectionId).createUsers(this.GetUsersInternal());
        }

        private IEnumerable<object> GetUsersInternal()
        {
            foreach (var item in MathHub.UserList.Where(c => !string.IsNullOrEmpty(c.Name)))
                yield return new { id = item.ConnectionId, name = item.Name };
        }

        // Demo 04 - connection events.

        public override Task OnConnected()
        {
            if (!MathHub.UserList.Where(c => c.ConnectionId == this.Context.ConnectionId).Any())
                MathHub.UserList.Add(new OnlineUser()
                {
                    ConnectionId = this.Context.ConnectionId
                });

            // broadcast to all user a new participant is in.
            this.Clients.All.notifyUserJoined();

            return base.OnConnected();
        }

        public override Task OnDisconnected()
        {
            MathHub.UserList.Remove(
                MathHub.UserList.Where(c => c.ConnectionId == this.Context.ConnectionId).First());

            // broadcast to all user a new participant is in.
            this.Clients.All.notifyUserLeaved();

            return base.OnDisconnected();
        }

        public override Task OnReconnected()
        {
            return base.OnReconnected();
        }
    }
}

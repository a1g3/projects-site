using Microsoft.AspNetCore.SignalR;
using System.Linq;
using System.Threading.Tasks;

namespace Alge.Domain.Patterns.SignalR
{
    public abstract class BaseHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            var groupNames = Context.GetHttpContext().Request.Query["groups"].SingleOrDefault();
            if (groupNames != null)
            {
                foreach (var groupName in groupNames.Split(','))
                    await Groups.AddToGroupAsync(Context.ConnectionId, groupName.ToLowerInvariant());
            }

            await base.OnConnectedAsync();
        }

    }
}

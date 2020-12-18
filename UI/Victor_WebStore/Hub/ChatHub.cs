using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Victor_WebStore.Hub
{
    public class ChatHub : Microsoft.AspNetCore.SignalR.Hub
    {
        public async Task SendMessage(string message,string userName) => await Clients.All.SendAsync("MessageFromClient", message, userName);
    }
}

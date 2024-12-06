using Microsoft.AspNetCore.SignalR;

public class ChatHub : Hub
{
    public async Task SendMessage(string chatId, string userId, string message)
    {
        await Clients.Group(chatId).SendAsync("ReceiveMessage", userId, message);
    }

    public override async Task OnConnectedAsync()
    {
        var chatId = Context.GetHttpContext()?.Request.Query["chatId"].ToString();
        if (!string.IsNullOrEmpty(chatId))
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatId);
            Console.WriteLine($"User connected to chatId: {chatId}");
        }
        else
        {
            Console.WriteLine("No chatId provided, connection rejected.");
        }
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var chatId = Context.GetHttpContext()?.Request.Query["chatId"].ToString();
        if (!string.IsNullOrEmpty(chatId))
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatId);
            Console.WriteLine($"User disconnected from chatId: {chatId}");
        }
        else
        {
            Console.WriteLine("User disconnected, no chatId to remove.");
        }
        await base.OnDisconnectedAsync(exception);
    }
}
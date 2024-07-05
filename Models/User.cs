using System;
using System.Collections.Generic;

namespace WebChat.Models;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Token { get; set; } = null!;

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual ICollection<UserChat> UserChats { get; set; } = new List<UserChat>();

    public virtual ICollection<Chat> Chats
    {
        get => UserChats.Select(uc => uc.Chat).ToList();
    }
}

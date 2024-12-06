using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WebChat.Models;

public partial class Chat
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public bool IsGroup { get; set; }

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual ICollection<UserChat> UserChats { get; set; } = new List<UserChat>();

    public virtual ICollection<User> Users
    {
        get => UserChats.Select(uc => uc.User).ToList();
    }
}

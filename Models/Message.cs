using System;
using System.Collections.Generic;

namespace WebChat.Models;

public partial class Message
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int ChatId { get; set; }

    public string Content { get; set; } = null!;

    public DateTime Timestamp { get; set; }

    public virtual Chat Chat { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}

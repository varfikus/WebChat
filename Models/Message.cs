using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using WebChat.Services.Implementations;

namespace WebChat.Models;

public partial class Message
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int ChatId { get; set; }

    public string Content { get; set; } = null!;

    public DateTime Timestamp { get; set; }

    //public string userName {get
    //    {
    //        if (string.IsNullOrEmpty(userName) && UserId != 0)
    //        {
    //            using (ChatContext context = new ChatContext())
    //            {
    //                var user = context.Users.FirstOrDefault(u => u.Id == UserId);
    //                userName = userName ?? "Unknown User";
    //            }
    //        }

    //        return userName;
    //    }
    //    set
    //    {
    //        userName = value;
    //    }
    //}
}
    //public string userName;

    //public string UserName
    //{
    //    
    //} 


    //public virtual Chat Chat { get; set; }

    //public virtual User User { get; set; }
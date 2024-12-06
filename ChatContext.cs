using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebChat.Models;

namespace WebChat;

public partial class ChatContext : DbContext
{
    public ChatContext()
    {
    }

    public ChatContext(DbContextOptions<ChatContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Chat> Chats { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public DbSet<UserChat> UserChats { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=127.0.0.1;Port=5432;Database=chat;Username=postgres;Password=admin");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Chat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Chat_pkey");
            entity.ToTable("Chat");
            entity.Property(e => e.IsGroup).HasDefaultValue(false);
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Message_pkey");
            entity.ToTable("Message");
            entity.Property(e => e.Timestamp)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            //entity.HasOne(d => d.Chat).WithMany(p => p.Messages)
            //    .HasForeignKey(d => d.ChatId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("Message_ChatId_fkey");
            //entity.HasOne(d => d.User).WithMany(p => p.Messages)
            //    .HasForeignKey(d => d.UserId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("Message_UserId_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("User_pkey");
            entity.ToTable("User");
            entity.HasIndex(e => e.Email, "User_Email_key").IsUnique();
            entity.HasIndex(e => e.Token, "User_Token_key").IsUnique();
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.Token).HasMaxLength(255);
            entity.Property(e => e.Username).HasMaxLength(255);

            entity.HasMany(d => d.Chats).WithMany(p => p.Users)
                .UsingEntity<UserChat>(
                    r => r.HasOne(uc => uc.Chat).WithMany(c => c.UserChats)
                        .HasForeignKey(uc => uc.ChatId)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("UserChat_ChatId_fkey"),
                    l => l.HasOne(uc => uc.User).WithMany(u => u.UserChats)
                        .HasForeignKey(uc => uc.UserId)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("UserChat_UserId_fkey"),
                    j =>
                    {
                        j.HasKey(uc => new { uc.UserId, uc.ChatId }).HasName("UserChat_pkey");
                        j.ToTable("UserChat");
                    });
        });

        //modelBuilder.Entity<MessageView>(entity =>
        //{
        //    entity.HasKey(e => e.Id); 
        //    entity.HasIndex(e => new { e.UserId, e.MessageId }).IsUnique(); 
        //    entity.Property(e => e.IsViewed)
        //        .IsRequired()
        //        .HasDefaultValue(false); 
        //    entity.HasOne(e => e.User)
        //        .WithMany()
        //        .HasForeignKey(e => e.UserId)
        //        .OnDelete(DeleteBehavior.Cascade);
        //    entity.HasOne(e => e.Message)
        //        .WithMany()
        //        .HasForeignKey(e => e.MessageId)
        //        .OnDelete(DeleteBehavior.Cascade);
        //});

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

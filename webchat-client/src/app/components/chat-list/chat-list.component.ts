import { Component, OnInit } from '@angular/core';
import { ChatService } from '../../services/chat.service';

@Component({
  selector: 'app-chat-list',
  templateUrl: './chat-list.component.html',
  styleUrls: ['./chat-list.component.css']
})
export class ChatListComponent implements OnInit {

  chats: any[] = [];

  constructor(private chatService: ChatService) { }

  ngOnInit(): void {
    this.chatService.getChats().subscribe(data => {
      this.chats = data;
    });
  }
}

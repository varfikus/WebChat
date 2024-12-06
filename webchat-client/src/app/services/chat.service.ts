import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ChatService {

  private apiUrl = 'https://localhost:5001/api'; // Adjust API URL as needed

  constructor(private http: HttpClient) { }

  getChats(): Observable<any> {
    return this.http.get(`${this.apiUrl}/chats`);
  }

  // Add other chat-related methods
}

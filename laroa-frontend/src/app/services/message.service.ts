import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class MessageService {
  private messageSubject = new Subject<string>();

  // Observable string stream
  message$ = this.messageSubject.asObservable();

  // Service message commands
  showMessage(message: string) {
    this.messageSubject.next(message);
  }
}

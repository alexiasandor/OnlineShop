import { Component, Input } from '@angular/core';
import { MessageService } from '../../services/message.service';

@Component({
  selector: 'app-message',
  template: `
    <div *ngIf="message" class="message">
      {{ message }}
    </div>
  `

})
export class MessageComponent {
  @Input() message: string | null = null;

  constructor(private messageService: MessageService) {
    this.messageService.message$.subscribe((message) => {
      this.message = message;
      setTimeout(() => (this.message = null), 3000); // Auto-dismiss after 3 seconds
    });
  }
}

import { Injectable } from '@angular/core';
import { MessageService } from 'primeng/api';

@Injectable({
  providedIn: 'root'
})

export class ToastService {

  constructor(
    private messageService: MessageService
  ) { }

  addToast(type: any, summary: any, detail: string) {
    this.messageService.add({ severity: type, summary, detail });
  }

  clear() {
    this.messageService.clear();
  }
}

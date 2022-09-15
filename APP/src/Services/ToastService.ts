import { MessageService } from 'primeng/api';

export class ToastsService {

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
